using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class KullanicilarController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Kullanicilar
        public ActionResult Index(string arama)
        {
            List<KULLANICI> liste = db.KULLANICIs.ToList();
            if (arama == null)
            {
                liste = db.KULLANICIs.ToList();
            }
            else
            {
                liste = db.KULLANICIs.Where(k => k.KULLANICI_ADI.Contains(arama)).ToList();
            }
            return View(liste);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)     // id istek içerisinde varsa
            {
                KULLANICI k = db.KULLANICIs.Find(id);
                if (k != null)  // id kullanıcılarda varsa
                {
                    db.KULLANICIs.Remove(k);    // listeden siler
                    db.SaveChanges();       // veritabanına kaydeder
                }
            }

            return RedirectToAction("Index");
        }


        // Admin/Kullanicilar/Create   yeni
        // Admin/Kullanicilar/Create/5    güncelleme
        public ActionResult Create(int? id)
        {
            KULLANICI k = new KULLANICI();
            if (id != null)
            {
                k = db.KULLANICIs.Find(id);
                if (k == null)
                {
                    k = new KULLANICI();
                }
            }

            return View(k);     // model binding
        }

        [HttpPost]
        public ActionResult Create(KULLANICI kullanici)
        {
            if (ModelState.IsValid)
            {
                if (kullanici.KULLANICI_REFNO == 0)
                {
                    db.KULLANICIs.Add(kullanici);
                }
                else
                {
                    db.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");   // listeleme yapılıyor
            }
            // hata var kayıt ekranı açılacak

            return View(kullanici);     // model binding
        }

        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", new { arama = txtAra });
        }
    }
}