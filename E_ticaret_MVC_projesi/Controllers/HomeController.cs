using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_ticaret_MVC_projesi.Models;
using System.Data.Entity;
namespace E_ticaret_MVC_projesi.Controllers
{
    public class HomeController : Controller
    {
        E_ticaretEntities db = new E_ticaretEntities();
        public ActionResult Index(string msj)
        {
            ViewBag.giris_sonuc = msj;
            return View();
        }


        public ActionResult kategori_doldur()
        {
            var kategorilerim = db.kategoriler.OrderBy(x => x.kateadi).ToList();
            return PartialView(kategorilerim);
        }

        public  ActionResult deneme()
        {
            return View();
        }
            
  
        public ActionResult login_giris()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> login_giris(uyeler giris_bilgisi)
        {
            string msj="";
            var uyem = db.uyeler.FirstOrDefault(x => x.kuladi == giris_bilgisi.kuladi && x.sifre == giris_bilgisi.sifre);
            if (uyem != null)
            {
                Session["uyem"] = uyem;
            }
            else
                msj = "kullanıcı adı veya şifre yanlış";

            return RedirectToAction("index",new { msj });
        }

        public ActionResult guv_cikis()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("index");
        }
    }
}