using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;


namespace FIRMA_MVC.Controllers
{
    public class UrunListeController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        
        // GET: Urunler
        public ActionResult Index(int? kategoriid)
        {
            ViewData["kategori"] = db.KATEGORIs.ToList();

            if (kategoriid == null)
            {
                kategoriid = db.KATEGORIs.ToList()[0].KATEGORI_REFNO;
            }

            ViewData["urun"] = db.URUNs.Where(u => u.KATEGORI_REFNO == kategoriid).ToList();
            return View();
        }

        public ActionResult Detay(int id)
        {
            URUN urun = db.URUNs.Find(id);

            if (urun == null)
            {
                return RedirectToAction("Goster", "Mesaj", new { m = "Ürün bulunamadı" });
            }

            return View(urun);
        }
    }
}