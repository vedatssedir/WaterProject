using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.DataModels;

namespace WebApp.Controllers
{
    public class UrunsController : Controller
    {
        private TaskestiDataContext db = new TaskestiDataContext();

        // GET: Uruns
        public ActionResult Index()
        {
            var urun = db.Urun.Include(u => u.Site);
            return View(urun.ToList());
        }

        // GET: Uruns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Uruns/Create
        public ActionResult Create()
        {
            ViewBag.SiteId = new SelectList(db.Site, "Id", "SiteAdi");
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UrunAdi,UrunResmi,Stok,BirimFiyat,IsActive,IsDelete,SiteId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiteId = new SelectList(db.Site, "Id", "SiteAdi", urun.SiteId);
            return View(urun);
        }

        // GET: Uruns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteId = new SelectList(db.Site, "Id", "SiteAdi", urun.SiteId);
            return View(urun);
        }

        // POST: Uruns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UrunAdi,UrunResmi,Stok,BirimFiyat,IsActive,IsDelete,SiteId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteId = new SelectList(db.Site, "Id", "SiteAdi", urun.SiteId);
            return View(urun);
        }

        // GET: Uruns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);
            db.Urun.Remove(urun);
            db.SaveChanges();
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
