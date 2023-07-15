using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_ticaret_MVC_projesi.Models;
using System.Data.Entity;
using System.IO;
using PagedList;
namespace E_ticaret_MVC_projesi.Controllers
{
    public class UrunlerController : Controller
    {
        E_ticaretEntities db = new E_ticaretEntities();
     

        public async Task<ActionResult> urun_goster(int? sayfa,int kateno)
        {
            ViewBag.kateno = kateno;
            var sayfa_no = sayfa ?? 1;
            //var urunlerim = db.urunler.ToList().ToPagedList (sayfa_no, 5);
            var urunlerim = db.urunler.Where(x => x.kateno == kateno).ToList().ToPagedList(sayfa_no, 8);
            return View(urunlerim);
        }


        public async Task<ActionResult> urun_kaydet()
        {
            ViewBag.kateno = new SelectList(await db.kategoriler.ToListAsync(), "kateno", "kateadi");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> urun_kaydet(urunler yeni_urun, HttpPostedFileBase file)
        {
            string dosya_adi = "resimyok.jpg";
            if (file!=null)
            {
               int urun_id= db.urunler.Max(x => x.urunid)+1;
                
                dosya_adi = Path.GetFileName(file.FileName);//sadece ad ve uzantıyı alıyoruz
                dosya_adi = urun_id + dosya_adi;
                string tam_yol = Path.Combine(Server.MapPath("~/urun_resimleri"), dosya_adi);
                file.SaveAs(tam_yol);
                yeni_urun.resim = dosya_adi;
                db.urunler.Add(yeni_urun);
                await db.SaveChangesAsync();
                ViewBag.sonuc = "Resim yüklendi ve ürün vtye kayıt oldu";
            }
            else
            {
                yeni_urun.resim = dosya_adi;
                db.urunler.Add(yeni_urun);
                await db.SaveChangesAsync();
                ViewBag.sonuc = " ürün vtye kayıt oldu";
            }

            ViewBag.kateno = new SelectList(db.kategoriler, "kateno", "kateadi");
            return View();
        }

    }
}