using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_ticaret_MVC_projesi.Models;

namespace E_ticaret_MVC_projesi.Controllers
{
    public class uyelersController : Controller
    {
        private E_ticaretEntities db = new E_ticaretEntities();

        // GET: uyelers
        public async Task<ActionResult> Index()
        {
            if (Session["admin"] != null)
            {
                var uyeler = db.uyeler.Include(u => u.meslek).Include(u => u.sehirler);
                return View(await uyeler.ToListAsync());
            }
            else
                return HttpNotFound();
        }

        // GET: uyelers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uyeler uyeler = await db.uyeler.FindAsync(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            return View(uyeler);
        }

        // GET: uyelers/Create
        public ActionResult Create()
        {
            ViewBag.meslekid = new SelectList(db.meslek, "meslekid", "meslekad");
            ViewBag.plaka = new SelectList(db.sehirler, "plaka", "sehiradi");
            return View();
        }

        // POST: uyelers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "uyeid,kuladi,sifre,adsoyad,dogtar,cinsiyet,adres,plaka,email,meslekid,onay")] uyeler uyeler)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.uyeler.Add(uyeler);
                    await db.SaveChangesAsync();
                    ViewBag.sonuc = "Kayıt başarılı";
                }
            }
            catch (Exception)
            {
                ViewBag.sonuc = "Aynı mail kuladi var ...Kayıt yapılamıyor!!!";
            }

            ViewBag.meslekid = new SelectList(db.meslek, "meslekid", "meslekad", uyeler.meslekid);
            ViewBag.plaka = new SelectList(db.sehirler, "plaka", "sehiradi", uyeler.plaka);
            return View(uyeler);
        }

        // GET: uyelers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uyeler uyeler = await db.uyeler.FindAsync(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            ViewBag.meslekid = new SelectList(db.meslek, "meslekid", "meslekad", uyeler.meslekid);
            ViewBag.plaka = new SelectList(db.sehirler, "plaka", "sehiradi", uyeler.plaka);
            return View(uyeler);
        }

        // POST: uyelers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "uyeid,kuladi,sifre,adsoyad,dogtar,cinsiyet,adres,plaka,email,meslekid,onay")] uyeler uyeler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uyeler).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.meslekid = new SelectList(db.meslek, "meslekid", "meslekad", uyeler.meslekid);
            ViewBag.plaka = new SelectList(db.sehirler, "plaka", "sehiradi", uyeler.plaka);
            return View(uyeler);
        }

        // GET: uyelers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uyeler uyeler = await db.uyeler.FindAsync(id);
            if (uyeler == null)
            {
                return HttpNotFound();
            }
            return View(uyeler);
        }

        // POST: uyelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            uyeler uyeler = await db.uyeler.FindAsync(id);
            db.uyeler.Remove(uyeler);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
