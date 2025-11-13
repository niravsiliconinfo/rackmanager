using CamV4.Helper;
using CamV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{
    public class CustomerLocationContactController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        // GET: CustomerLocationContact
        public ActionResult Index()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                MailViewModel count = new MailViewModel();
                var userId = Convert.ToInt64(Session["LoggedInUserId"]);

                count.InspectionId = DatabaseHelper.getInspectionByContactId().Count();
                    var contact = db.CustomerLocationContacts.Where(x => x.IsActive == true && x.UserID == userId).ToList();
                    return View(count);
                //count.InspectionId = 0;
                //count.LocationContactId = "0";
                //return View(count);
            }
        }

        public ActionResult ManageInspection()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ManagePassword()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var id = Convert.ToInt16(Session["LoggedInUserId"]);
                var user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null) { return View(user); }
                return View();
            }
        }

        public ActionResult ManageContacts(string id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }
    }
}