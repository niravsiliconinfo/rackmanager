using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{
    public class SalesManagerController : Controller
    {
        // GET: SalesManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageInspectionSales()
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

        public ActionResult ManageDocumentSales()
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
        public ActionResult ManageIncidentReportSales()
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
        public ActionResult ManageInternalInspectionSales()
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
        //public ActionResult ViewInternalIncidentReportView()
        //{
        //    if (Session["LoggedInUserId"] == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        public ActionResult ViewInternalIncidentReportView(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.Id = id;
                return View();
            }
        }    
        public ActionResult ManageSpareMaterialSales()
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
      
        public ActionResult TrainingCenterSales()
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

        public ActionResult TrainingCenterRegistrationSales()
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
        public ActionResult TrainingCenterTechnicalTalkSales()
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
        public ActionResult TrainingCenterCourseStatusSales()
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

        public ActionResult TrainingCenterCoursesSales()
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
        public ActionResult TrainingCenterAdditionalResourcesSales()
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