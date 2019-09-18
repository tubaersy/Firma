using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class MarkalarController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Markalar
        int sayfadakisatirsayisi = 5;
        public ActionResult Index(string arama, int aktifsayfa = 0)
        {
            List<MARKA> liste = new List<MARKA>();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.MARKAs.Count());
                liste = db.MARKAs.OrderBy(u => u.MARKA_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else
            {
                Sayfalama(db.MARKAs.Where(s => s.MARKA_ADI.Contains(arama)).Count());
                liste = db.MARKAs.Where(s => s.MARKA_ADI.Contains(arama))
                                .OrderBy(u => u.MARKA_REFNO)
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

        public ActionResult Delete(int id)
        {
            MARKA m = db.MARKAs.Find(id);

            if (m != null)
            {
                db.MARKAs.Remove(m);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]   // link üzerinden geliyor
        public ActionResult Create(int? id)     // int? id null olabilir demek
        {
            if (id == null)
            {
                MARKA m = new MARKA();
                return View(m);
            }
            else
            {
                MARKA m = db.MARKAs.Find(id);
                return View(m);
            }
        }

        [HttpPost]
        public ActionResult Create(MARKA m)
        {
            if (m.MARKA_REFNO == 0)
            {
                db.MARKAs.Add(m);
            }
            else
            {
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string txtAra)
        {

            return RedirectToAction("Index", "Markalar", new { arama = txtAra });
        }
    }
}