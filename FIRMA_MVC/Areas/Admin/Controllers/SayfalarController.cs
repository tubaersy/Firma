using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class SayfalarController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Sayfalar

        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            List<SAYFA> liste = new List<SAYFA>();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.SAYFAs.Count());
                liste = db.SAYFAs.OrderBy(u => u.SAYFA_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else
            {
                Sayfalama(db.SAYFAs.Where(s => s.BASLIK.Contains(arama)).Count());
                liste = db.SAYFAs.Where(s => s.BASLIK.Contains(arama))
                                .OrderBy(u => u.SAYFA_REFNO)
                                .Skip(aktifsayfa * sayfadakisatirsayisi).
                                 Take(sayfadakisatirsayisi).ToList();
            }

            ViewData["veri"] = arama;

            ViewData["arama"] = arama;
            ViewData["aktifsayfa"] = aktifsayfa;

            return View(liste);

        }

        public void Sayfalama(int satirsayisi)
        {
            int toplamsatir = satirsayisi;
            int toplamsayfa = toplamsatir / sayfadakisatirsayisi;

            if (toplamsatir % sayfadakisatirsayisi != 0)
            {
                toplamsayfa++;
            }
            ViewData["toplamsatir"] = toplamsatir;
            ViewData["toplamsayfa"] = toplamsayfa;
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