using E_ticaret_MVC_projesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_ticaret_MVC_projesi.Controllers
{
    public class SiparislerController : Controller
    {
        // GET: Siparisler
        E_ticaretEntities db = new E_ticaretEntities();
        public async Task<ActionResult> siparis_gec()
        {
            var sepetimiz= (Sepet)Session["sepetimiz"];
            var yeni_sip_no = db.siparisler.Max(x => x.sipno);
            yeni_sip_no += 1;

            foreach (var sepetteki_urun in sepetimiz.Sepetim)
            {
                siparisler yeni_sip_kaydi = new siparisler()
                {
                    urunid = sepetteki_urun.urun.urunid,
                    adet = sepetteki_urun.adet,
                    sipno = yeni_sip_no,
                    uyeid = ((uyeler)Session["uyem"]).uyeid,
                    sip_tarihi = DateTime.Now
                };
                db.siparisler.Add(yeni_sip_kaydi);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("index", "Sepet", new { yeni_sip_no });
        }
    }
}