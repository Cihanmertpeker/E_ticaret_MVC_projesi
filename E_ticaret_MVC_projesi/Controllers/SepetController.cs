using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using E_ticaret_MVC_projesi.Models;
namespace E_ticaret_MVC_projesi.Controllers
{
    public class SepetController : Controller
    {
        E_ticaretEntities db = new E_ticaretEntities();

        public Sepet sepeti_getir()
        {
            var sepetimiz = (Sepet)Session["sepetimiz"];
            if (sepetimiz ==null)//sepet sessionda yok ilk ürün
            {
                sepetimiz = new Sepet();
                Session["sepetimiz"] = sepetimiz;
            }
            return sepetimiz;
        }

        public ActionResult Index(int? yeni_sip_no)
        {
            if (yeni_sip_no != null)
            {
                ViewBag.msj = "Siparişiniz alındı .Siparis no=" + yeni_sip_no;
                sepeti_getir().Sepeti_temizle();
            }

            return View(sepeti_getir());
        }

        public async Task<ActionResult> sepete_ekle(int urunid,byte? adet)
        {
            var _adet = adet ?? 0;//linkle gelinirse urunler sayfasından adet 0
            var eklenecek_urun = await db.urunler.FirstOrDefaultAsync(x => x.urunid == urunid);
            if (eklenecek_urun!=null)
            {
                sepeti_getir().Sepete_ekle(eklenecek_urun, _adet);
            }
            return RedirectToAction("index");
        }

        public async Task<ActionResult> sepetten_sil(int urunid)
        {
          
            var silinecek_urun = await db.urunler.FirstOrDefaultAsync(x => x.urunid == urunid);
            if (silinecek_urun != null)
            {
                sepeti_getir().Sepeten_sil(silinecek_urun);
            }
            return RedirectToAction("index");
        }
        public async Task<ActionResult> sepeti_temizle()
        {
            sepeti_getir().Sepeti_temizle();
            return RedirectToAction("index");
        }

        }
    }