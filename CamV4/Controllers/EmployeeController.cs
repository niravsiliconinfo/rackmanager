using CamV4.Helper;
using CamV4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{
    public class EmployeeController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AdminDashboardGraphViewModel count = new AdminDashboardGraphViewModel();
                var userId = Convert.ToInt64(Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var empId = db.Employees.Where(x => x.UserID == userId).FirstOrDefault();
                    if (empId.EmployeeID != 0)
                    {
                        count.InspectionDueCount = 0;
                        count.InProgressCount = 0;
                        count.SentforApprovalCount = 0;
                        count.ApprovedCompletedCount = 0;
                        count.QuotationRequestedCount = 0;
                        count.AwaitingApprovalCount = 0;
                        count.QuotationApprovedCount = 0;
                        count.RepairCompletedCount = 0;
                        count.InspectionFinishedCount = 0;
                        count.DashboardActiveUserCount = 0;
                        count.DashboardActiveUserAdminCount = 0;
                        count.DashboardActiveUserEmployeeCount = 0;
                        count.DashboardActiveCompanyCount = 0;
                        //count.InspectionDueCount = DatabaseHelper.getDueInspectionCountByEmployeeId(empId.EmployeeID);
                        //count.InProgressCount = DatabaseHelper.getInProgressInspectionCountByEmployeeId(empId.EmployeeID);
                        //count.SentforApprovalCount = DatabaseHelper.getSentForApprovalInspectionCountByEmployeeId(empId.EmployeeID);
                        //count.ApprovedCompletedCount = DatabaseHelper.getAppAndCompletedInspectionCountByEmployeeId(empId.EmployeeID);

                        using (DatabaseEntities db = new DatabaseEntities())
                        {
                            var year = DateTime.Now.Year;
                            var objDashboardResult = db.SP_Get_AllUserDashboardCount(empId.EmployeeID, year);
                            foreach (var item in objDashboardResult)
                            {
                                switch (item.InspectionStatusId)
                                {
                                    case 1:
                                        count.InspectionDueCount = (long) item.cnt;
                                        break;
                                    case 2:
                                        count.InProgressCount = (long) item.cnt;
                                        break;
                                    case 3:
                                        count.SentforApprovalCount = (long) item.cnt;
                                        break;
                                    case 4:
                                        count.ApprovedCompletedCount = (long) item.cnt;
                                        break;
                                    case 5:
                                        count.QuotationRequestedCount = (long) item.cnt;
                                        break;
                                    case 6:
                                        count.AwaitingApprovalCount = (long) item.cnt;
                                        break;
                                    case 7:
                                        count.QuotationApprovedCount = (long) item.cnt;
                                        break;
                                    case 8:
                                        count.RepairCompletedCount = (long) item.cnt;
                                        break;
                                    case 9:
                                        count.InspectionFinishedCount = (long) item.cnt;
                                        break;
                                    case 90:
                                        count.DashboardActiveUserCount = (long) item.cnt;
                                        break;
                                    case 91:
                                        count.DashboardActiveUserAdminCount = (long) item.cnt;
                                        break;
                                    case 92:
                                        count.DashboardActiveUserEmployeeCount = (long) item.cnt;
                                        break;
                                    case 93:
                                        count.DashboardActiveCompanyCount = (long) item.cnt;
                                        break;
                                    case 94:
                                        count.DashboardActiveInventoryCount = (long) item.cnt;
                                        break;
                                }
                            }
                        }
                        return View(count);
                    }
                }

                return View(count);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult MyProfile()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var id = Convert.ToInt16(Session["LoggedInUserId"]);
                var itm = DatabaseHelper.getUserEmployeeByUserId(id);
                return View(itm);
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

        public ActionResult InspectionDetail(long id)
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

        public ActionResult CustomerLocationDetails(int id)
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

        public ActionResult CustomerAreaDetails(int id)
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