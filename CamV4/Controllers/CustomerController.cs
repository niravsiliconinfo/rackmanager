using CamV4.Helper;
using CamV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{
    public class CustomerController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        // GET: Customer

        
        public ActionResult Index()
        {
            

            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                CustomerDashboardGraphViewModel count = new CustomerDashboardGraphViewModel();
                var userId = Convert.ToInt64(Session["LoggedInUserId"]);
                var customer = db.Customers.Where(x => x.UserID == userId && x.IsActive == true).FirstOrDefault();
                if (customer != null)
                {
                    Session["CustomerId"] = customer.CustomerId;

                    count.InspectionDueCount = 0;
                    count.SentforApprovalCount = 0;
                    count.ApprovedCompletedCount = 0;
                    count.InProgressCount = 0;
                    count.QuotationRequestedCount = 0;
                    count.AwaitingApprovalCount = 0;
                    count.QuotationApprovedCount = 0;
                    count.RepairCompletedCount = 0;
                    count.InspectionFinishedCount = 0;

                    //string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    //Response.Write(host.ToString());                
                    var year = DateTime.Now.Year;
                    //var pie = DatabaseHelper.getInspectionCountGraph(userId);
                    //var doneLine = db.sp_getEmpInspection_Count().ToList();
                    count.PieCusomter = db.Get_DeficienciesBySeverityCustomerNew(userId, year).ToList();

                    using (DatabaseEntities db = new DatabaseEntities())
                    {
                        var objDashboardResult = db.SP_Get_CustomerDashboardCount(year, customer.CustomerId);
                        //var objDashboardResult = db.SP_Get_AdminDashboardCount(year);
                        var list = objDashboardResult.ToList();
                        foreach (var item in list)
                        {
                            switch (item.InspectionStatusId)
                            {
                                case 1:
                                    count.InspectionDueCount = item.cnt;
                                    break;
                                case 2:
                                    count.InProgressCount = item.cnt;
                                    break;
                                case 3:
                                    count.SentforApprovalCount = item.cnt;
                                    break;
                                case 4:
                                    count.ApprovedCompletedCount = item.cnt;
                                    break;
                                case 5:
                                    count.QuotationRequestedCount = item.cnt;
                                    break;
                                case 6:
                                    count.AwaitingApprovalCount = item.cnt;
                                    break;
                                case 7:
                                    count.QuotationApprovedCount = item.cnt;
                                    break;
                                case 8:
                                    count.RepairCompletedCount = item.cnt;
                                    break;
                                case 9:
                                    count.InspectionFinishedCount = item.cnt;
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    var customerContact = db.CustomerLocationContacts.Where(x => x.UserID == userId && x.IsActive == true).FirstOrDefault();
                    if (customerContact != null)
                    {
                        Session["CustomerId"] = customerContact.CustomerId;
                        count.InspectionDueCount = 0;
                        count.SentforApprovalCount = 0;
                        count.ApprovedCompletedCount = 0;
                        count.InProgressCount = 0;
                        count.QuotationRequestedCount = 0;
                        count.AwaitingApprovalCount = 0;
                        count.QuotationApprovedCount = 0;
                        count.RepairCompletedCount = 0;
                        count.InspectionFinishedCount = 0;

                        //string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                        //Response.Write(host.ToString());                
                        var year = DateTime.Now.Year;
                        //var pie = DatabaseHelper.getInspectionCountGraph(userId);
                        //var doneLine = db.sp_getEmpInspection_Count().ToList();
                        count.PieCusomter = db.Get_DeficienciesBySeverityCustomerNew(userId, year).ToList();

                        using (DatabaseEntities db = new DatabaseEntities())
                        {
                            var objDashboardResult = db.SP_Get_CustomerDashboardCount(year, customerContact.LocationContactId);
                            //var objDashboardResult = db.SP_Get_AdminDashboardCount(year);
                            var list = objDashboardResult.ToList();
                            foreach (var item in list)
                            {
                                switch (item.InspectionStatusId)
                                {
                                    case 1:
                                        count.InspectionDueCount = item.cnt;
                                        break;
                                    case 2:
                                        count.InProgressCount = item.cnt;
                                        break;
                                    case 3:
                                        count.SentforApprovalCount = item.cnt;
                                        break;
                                    case 4:
                                        count.ApprovedCompletedCount = item.cnt;
                                        break;
                                    case 5:
                                        count.QuotationRequestedCount = item.cnt;
                                        break;
                                    case 6:
                                        count.AwaitingApprovalCount = item.cnt;
                                        break;
                                    case 7:
                                        count.QuotationApprovedCount = item.cnt;
                                        break;
                                    case 8:
                                        count.RepairCompletedCount = item.cnt;
                                        break;
                                    case 9:
                                        count.InspectionFinishedCount = item.cnt;
                                        break;
                                }
                            }
                        }
                    }
                }
                return View(count);
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
        public ActionResult ManageInspectionDue()
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

        public ActionResult InspectionDetails(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var insp = db.Inspections.Where(x => x.InspectionId == id).FirstOrDefault();
                if (insp != null)
                {
                    if (insp.InspectionStatus > 3) //|| insp.InspectionStatus == 4
                    {
                        return View();
                    }
                }
                return null;
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

        public ActionResult ManageAllUserContacts(string id)
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
        public ActionResult ManageCustomerUsers(string id)
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

        public ActionResult GenerateQuotation(long id)
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

        public ActionResult GenerateQuotationSuccess(long id)
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
        public ActionResult AddLocationContact(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewData["CustLocID"] = id;
                var loc = DatabaseHelper.getCustomerLocationDetailsById(id);
                ViewData["CustLocName"] = loc.LocationName;
                return View();
            }
        }

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

        public ActionResult ManageHistoryLegacyDocuments()
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

        public ActionResult IncidentReport()
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

        public ActionResult IncidentReportNew()
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

        public ActionResult IncidentReportView(long id)
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
        //public ActionResult IncidentReportView()
        //{
        //    if (Session["LoggedInUserId"] == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        ViewBag.LoggedInUserName = Session["LoggedInUserName"] ?? "";
        //        return View();
        //    }
        //}

        public ActionResult InternalInspections()
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

        public ActionResult TrainingCenter()
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

        public ActionResult TrainingCenterRegistration()
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
        public ActionResult TrainingCenterCourses()
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
        public ActionResult TrainingCenterAdditionalResources()
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