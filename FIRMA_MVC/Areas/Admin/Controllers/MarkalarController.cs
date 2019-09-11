using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using FIRMA_MVC.Models;

namespace FIRMA_MVC.Areas.Admin.Controllers
{
    public class MarkalarController : Controller
    {
        FIRMAMODEL db = new FIRMAMODEL();
        // GET: Admin/Markalar
        public ActionResult Index()
        {
            List<MARKA> liste = db.MARKAs.ToList();
            return View();
        }

        public ActionResult Delete(int? id)
        {

            if (id != null)
            {
                MARKA m = db.MARKAs.Find(id);
                if (m != null)
                {
                    db.MARKAs.Remove(m);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

    }
}