using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;


namespace FIRMA_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Urunler
        //public ActionResult Index()
        //{
        //    List<URUN> liste = db.URUNs.ToList();
        //    return View(liste);
        //}

        //int toplamsatir = 0;
        int sayfadakisatirsayisi = 5;
        //int toplamsayfa = 0;
        // int aktifsayfa = 0;

        public ActionResult Index(string arama, int aktifsayfa=0)
        {
            List<URUN> liste = new List<URUN>();

            if (arama == null)
            {
                arama = "";
                Sayfalama(db.URUNs.Count());
                liste = db.URUNs.OrderBy(u => u.URUN_REFNO).Skip(aktifsayfa * sayfadakisatirsayisi)
                                .Take(sayfadakisatirsayisi).ToList();

            }
            else
            {
                Sayfalama(db.URUNs.Where(s => s.URUN_ADI.Contains(arama)).Count());
                liste = db.URUNs.Where(s => s.URUN_ADI.Contains(arama))
                                .OrderBy(u => u.URUN_REFNO)
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

            ViewData["kategori"] = db.KATEGORIs.ToList();       // her şey object olarak saklanır
            ViewBag.marka = db.MARKAs.ToList();

            return View(urun);  // model binding yapılıyor

        }
        [HttpPost]
        public ActionResult Create(URUN urun, HttpPostedFileBase RESIM1, HttpPostedFileBase RESIM2, HttpPostedFileBase RESIM3, HttpPostedFileBase RESIM4)
        {

            if (ModelState.IsValid)
            {
                if (RESIM1 != null)
                {
                    urun.RESIM1 = RESIM1.FileName;
                }
                if (RESIM2 != null)
                {
                    urun.RESIM2 = RESIM2.FileName;
                }
                if (RESIM3 != null)
                {
                    urun.RESIM3 = RESIM3.FileName;
                }
                if (RESIM4 != null)
                {
                    urun.RESIM4 = RESIM4.FileName;
                }

                if (urun.URUN_REFNO == 0)
                {
                    db.URUNs.Add(urun);
                }
                else
                {
                    db.Entry(urun).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
                // dosyaları yükle

                if (RESIM1 != null)
                {
                    RESIM1.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM1.FileName);      // resim kaydediliyor
                }
                if (RESIM2 != null)
                {
                    RESIM2.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM2.FileName);
                }
                if (RESIM3 != null)
                {
                    RESIM3.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM3.FileName);
                }
                if (RESIM4 != null)
                {
                    RESIM4.SaveAs(Request.PhysicalApplicationPath + "/images/" + RESIM4.FileName);
                }
            }

            else
            {
                string hatalar = "";

                foreach (var item in ModelState.Values)
                {
                    for (int i = 0; i < item.Errors.Count; i++)
                    {
                        hatalar += item.Errors[i].ErrorMessage + "<br>";
                    }
                }
                ViewData["hatalar"] = hatalar;
                ViewData["kategori"] = db.KATEGORIs.ToList();       // her şey object olarak saklanır
                ViewBag.marka = db.MARKAs.ToList();

                return View(urun);
            }

            
            return RedirectToAction("Index");
        }
        public ActionResult Search(string txtAra)
        {
            
            return RedirectToAction("Index", "Urunler", new { arama = txtAra });
        }
    }
}