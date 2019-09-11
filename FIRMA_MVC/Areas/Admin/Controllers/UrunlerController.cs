using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;


namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class UrunlerController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Urunler
        public ActionResult Index()
        {
            List<URUN> liste = db.URUNs.ToList();
            return View(liste);
        }
        public ActionResult Delete(int? id)
        {
           
            if (id != null)
            {
                URUN u = db.URUNs.Find(id);
                if (u != null)
                {
                    db.URUNs.Remove(u);
                    db.SaveChanges();
                }
            }
            

            // var liste = db.URUNs.ToList();
            // return View("Index", liste);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            URUN urun = new URUN();
            if (id != null) // güncelleme yapılacak
            {
                urun = db.URUNs.Find(id);
                if (urun == null)
                {
                    urun = new URUN();
                }
            }

            ViewData["kategori"] = db.KATEGORIs.ToList();
            ViewBag.marka = db.MARKAs.ToList();

            return View(urun);  // model binding yapılıyor

        }
        [HttpPost]
        public ActionResult Create(URUN urun)
        {
            return View();
        }
        public ActionResult Search(string ara)
        {
            return View();
        }
    }
}