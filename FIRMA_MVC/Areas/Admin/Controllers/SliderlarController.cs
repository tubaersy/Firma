using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class SliderlarController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Sliderlar
        public ActionResult Index(string arama)
        {
            List<SLIDER> liste = db.SLIDERs.ToList();
            if (arama == null)
            {
                liste = db.SLIDERs.ToList();
            }
            else
            {
                liste = db.SLIDERs.Where(k => k.BASLIK.Contains(arama)).ToList();
            }
            return View(liste);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)     // id istek içerisinde varsa
            {
                SLIDER s = db.SLIDERs.Find(id);
                if (s != null)  
                {
                    db.SLIDERs.Remove(s);    // listeden siler
                    db.SaveChanges();       // veritabanına kaydeder
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create(int? id)
        {
            SLIDER s = new SLIDER();
            if (id != null)
            {
                s = db.SLIDERs.Find(id);
                if (s == null)
                {
                    s = new SLIDER();
                }
            }

            return View(s);     // model binding
        }

        [HttpPost]
        public ActionResult Create(SLIDER slider, HttpPostedFileBase RESIM)
        {
            if (ModelState.IsValid)
            {
                if (RESIM != null)
                {
                    slider.RESIM = RESIM.FileName;
                }

                if (slider.SLIDER_REFNO == 0)
                {
                    db.SLIDERs.Add(slider);
                }
                else
                {
                    db.Entry(slider).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                if (RESIM != null)
                {
                    RESIM.SaveAs(Request.PhysicalApplicationPath + "/images/slider/" + RESIM.FileName);//resim yükleme
                }

                return RedirectToAction("Index");   // listeleme yapılıyor
            }
            

            return View(slider);     // model binding
        }

        public ActionResult Search(string txtAra)
        {
            return RedirectToAction("Index", new { arama = txtAra });
        }
    }
}