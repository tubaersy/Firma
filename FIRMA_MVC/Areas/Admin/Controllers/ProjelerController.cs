using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjelerController : Controller
    {
        private FIRMAMODEL db = new FIRMAMODEL();

        // GET: Admin/Projeler
        public ActionResult Index(string arama)
        {
            List<PROJE> liste = db.PROJEs.ToList();

            if (arama == null)
            {
                arama = "";
                liste = db.PROJEs.ToList();
            }
            else
            {
                liste = db.PROJEs.Where(s => s.PROJE_ADI.Contains(arama)).ToList();
            }
            return View(liste);
        }

        // GET: Admin/Projeler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJE pROJE = db.PROJEs.Find(id);
            if (pROJE == null)
            {
                return HttpNotFound();
            }
            return View(pROJE);
        }

        // GET: Admin/Projeler/Create
        public ActionResult Create(int? id)
        {
            PROJE p = new PROJE();
            if (id != null)
            {
                p = db.PROJEs.Find(id);
                if (p == null)
                {
                    p = new PROJE();
                }
            }
            return View(p);
        }

        // POST: Admin/Projeler/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PROJE proje, HttpPostedFileBase RESIM)
        {
           
            if (ModelState.IsValid)
            {
               

                if (RESIM != null)
                {
                    
                    proje.RESIM = RESIM.FileName;
                }

                if (proje.PROJE_REFNO == 0)
                {
                    proje.ACIKLAMA = HttpUtility.HtmlDecode(proje.ACIKLAMA);
                    db.PROJEs.Add(proje);
                }
                else
                {
                    proje.ACIKLAMA = HttpUtility.HtmlDecode(proje.ACIKLAMA);
                    db.Entry(proje).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                if (RESIM != null)
                {

                    RESIM.SaveAs(Request.PhysicalApplicationPath + "/images/proje/" + RESIM.FileName);
                }

                return RedirectToAction("Index");
            }

            return View(proje);
        }

        // GET: Admin/Projeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROJE pROJE = db.PROJEs.Find(id);
            if (pROJE == null)
            {
                return HttpNotFound();
            }
            return View(pROJE);
        }

        // POST: Admin/Projeler/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PROJE_REFNO,PROJE_ADI,RESIM,ACIKLAMA")] PROJE pROJE)
        {
            if (ModelState.IsValid)
            {

                db.Entry(pROJE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROJE);
        }

        // GET: Admin/Projeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)//id istek içerisinde varsa
            {
                PROJE p = db.PROJEs.Find(id);
                if (p != null)//id kullanıcılarda varsa
                {
                    db.PROJEs.Remove(p);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/Projeler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROJE pROJE = db.PROJEs.Find(id);
            db.PROJEs.Remove(pROJE);
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
