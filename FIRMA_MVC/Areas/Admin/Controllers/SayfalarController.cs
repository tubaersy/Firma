using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class SayfalarController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Sayfalar
        public ActionResult Index(string arama)
        {
            List<SAYFA> liste = db.SAYFAs.ToList();
            if (arama == null)
            {
                liste = db.SAYFAs.ToList();
            }
            else
            {
                liste = db.SAYFAs.Where(k => k.BASLIK.Contains(arama)).ToList();
            }
            return View(liste);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)     // id istek içerisinde varsa
            {
                SAYFA s = db.SAYFAs.Find(id);
                if (s != null)  
                {
                    db.SAYFAs.Remove(s);    // listeden siler
                    db.SaveChanges();       // veritabanına kaydeder
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create(int? id)
        {
            SAYFA s = new SAYFA();
            if (id != null)
            {
                s = db.SAYFAs.Find(id);
                if (s == null)
                {
                    s = new SAYFA();
                }
            }

            return View(s);     // model binding
        }

        [HttpPost]
        public ActionResult Create(SAYFA sayfa)
        {
            if (ModelState.IsValid)
            {
                if (sayfa.SAYFA_REFNO == 0)
                {
                    sayfa.ICERIK = HttpUtility.HtmlDecode(sayfa.ICERIK);
                    db.SAYFAs.Add(sayfa);
                }
                else
                {
                    sayfa.ICERIK = HttpUtility.HtmlDecode(sayfa.ICERIK);
                    db.Entry(sayfa).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");   // listeleme yapılıyor
            }
            // hata var kayıt ekranı açılacak

            return View(sayfa);     // model binding
        }

        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", new { arama = txtAra });
        }

    }
}