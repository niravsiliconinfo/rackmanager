using CamV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{
    public class CitiesController : Controller
    {
        private readonly DatabaseEntities db = new DatabaseEntities();
        // GET: Cities
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllCities()
        {
            var cities = db.Cities.Include("Provinces").Where(c => c.IsActive == false)
                                  .Select(c => new
                                  {
                                      c.CityID,
                                      c.CityName,
                                      c.ProvinceID,
                                      ProvinceName = c.Province.ProvinceName
                                  }).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProvinces()
        {
            var provinces = db.Provinces.Select(p => new
            {
                p.ProvinceID,
                p.ProvinceName
            }).ToList();
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCity(City city)
        {
            city.CreatedDate = DateTime.Now;
            city.ModifiedDate = DateTime.Now;
            city.IsActive = true;
            city.CreatedBy = Session["LoggedInUserId"].ToString();
            city.ModifiedBy = Session["LoggedInUserId"].ToString();

            db.Cities.Add(city);
            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult UpdateCity(City city)
        {
            var existing = db.Cities.Find(city.CityID);
            if (existing == null)
                return Json(new { success = false });

            existing.CityName = city.CityName;
            existing.ProvinceID = city.ProvinceID;
            existing.ModifiedDate = DateTime.Now;
            existing.IsActive = true;
            existing.ModifiedBy = Session["LoggedInUserId"].ToString();

            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult DeleteCity(int id)
        {
            var existing = db.Cities.Find(id);
            if (existing == null)
                return Json(new { success = false });
            existing.IsActive = false;
            existing.ModifiedDate = DateTime.Now;
            existing.ModifiedBy = Session["LoggedInUserId"].ToString();

            db.SaveChanges();
            return Json(new { success = true });

            //var city = db.Cities.Find(id);
            //if (city == null)
            //    return Json(new { success = false });

            //db.Cities.Remove(city);
            //db.SaveChanges();
            //return Json(new { success = true });
        }
    }
}