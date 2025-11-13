using CamV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{
    public class HomeController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        public ActionResult Index()
        {
            var itm = db.Provinces.ToList();
            return View(itm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}