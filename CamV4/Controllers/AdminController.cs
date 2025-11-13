using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using CamV4.Helper;
using CamV4.Models;
using FirebaseAdmin;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Newtonsoft.Json;

namespace CamV4.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                AdminDashboardGraphViewModel graph = new AdminDashboardGraphViewModel();
                //string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //Response.Write(host.ToString());
                var userId = Convert.ToInt32(Session["LoggedInUserId"]);
                var year = DateTime.Now.Year;
                //var pie = DatabaseHelper.getInspectionCountGraph(userId);
                //var doneLine = db.sp_getEmpInspection_Count().ToList();
                graph.Pie = DatabaseHelper.getInspectionCountGraphByYear(0, year);
                graph.InspectionDueCount = 0;
                graph.InProgressCount = 0;
                graph.SentforApprovalCount = 0;
                graph.ApprovedCompletedCount = 0;
                graph.QuotationRequestedCount = 0;
                graph.AwaitingApprovalCount = 0;
                graph.QuotationApprovedCount = 0;
                graph.RepairCompletedCount = 0;
                graph.InspectionFinishedCount = 0;
                graph.DashboardActiveUserCount = 0;
                graph.DashboardActiveUserAdminCount = 0;
                graph.DashboardActiveUserEmployeeCount = 0;
                graph.DashboardActiveCompanyCount = 0;


                //graph.PieYear = DatabaseHelper.getInspectionCountGraphByYear(userId, 2023);
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var objDashboardResult = db.SP_Get_AdminDashboardCount(year);

                    foreach (var item in objDashboardResult)
                    {
                        switch (item.InspectionStatusId)
                        {
                            case 1:
                                graph.InspectionDueCount =(long) item.cnt;
                                break;
                            case 2:
                                graph.InProgressCount = (long)item.cnt;
                                break;
                            case 3:
                                graph.SentforApprovalCount = (long)item.cnt;
                                break;
                            case 4:
                                graph.ApprovedCompletedCount = (long)item.cnt;
                                break;
                            case 5:
                                graph.QuotationRequestedCount = (long)item.cnt;
                                break;
                            case 6:
                                graph.AwaitingApprovalCount = (long)item.cnt;
                                break;
                            case 7:
                                graph.QuotationApprovedCount = (long)item.cnt;
                                break;
                            case 8:
                                graph.RepairCompletedCount = (long)item.cnt;
                                break;
                            case 9:
                                graph.InspectionFinishedCount = (long)item.cnt;
                                break;
                            case 90:
                                graph.DashboardActiveUserCount = (long)item.cnt;
                                break;
                            case 91:
                                graph.DashboardActiveUserAdminCount = (long)item.cnt;
                                break;
                            case 92:
                                graph.DashboardActiveUserEmployeeCount = (long)item.cnt;
                                break;
                            case 93:
                                graph.DashboardActiveCompanyCount = (long)item.cnt;
                                break;
                            case 94:
                                graph.DashboardActiveInventoryCount = (long)item.cnt;
                                break;
                        }
                    }
                }
                graph.LineDone = DatabaseHelper.getDoneEmpInspectionCountByYear(year);
                graph.LineApproved = DatabaseHelper.getApprovedInspectionCountByYear(year);
                //graph.DueCount = DatabaseHelper.getAllDueInspectionbyYear(year);
                //graph.InProgressCount = DatabaseHelper.getAllInProgressInspectionbyYear(year);
                //graph.SentForApprovalCount = DatabaseHelper.getAllSentToApprovalInspectionbyYear(year);
                //graph.AppAndCompletedCount = DatabaseHelper.getAllApprovedAndCompleteInspectionbyYear(year);

                return View(graph);
            }
        }


        //public ActionResult ProcessImages()
        //{
        //    DatabaseHelper.processImages();
        //    return null;
        //}

        public ActionResult ManageEmployee()
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

        // GET: /Account/Create
        [AllowAnonymous]
        [HttpGet]
        public ActionResult EmpCreate()
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
        public ActionResult EmpEdit(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getUserEmployeeById(id);
                return View(itm);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult EmpDelete(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getUserEmployeeDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult ManageDamageClassification()
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

        public ActionResult ManageDrawings()
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

        public ActionResult ManageShelving()
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

        public ActionResult ManageFacilitiesArea()
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

        public ActionResult ManageProcessOverview()
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

        public ActionResult ManageDeficiencySummary()
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

        public ActionResult ManageDeficiencyCategory()
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

        public ActionResult ManageDeficiency()
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

        public ActionResult ManageManufacturer()
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

        public ActionResult ManageComponent()
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

        public ActionResult ManageRackingType()
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
        public ActionResult ManageInspectionNew()
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
        public ActionResult ManageInspectionFiles(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var data = db.Inspections.Where(x => x.InspectionId == id).FirstOrDefault();
                if (data.InspectionDocumentNo != null)
                {
                    ViewData["InspectionDocumentNo"] = data.InspectionDocumentNo;
                }

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

        public ActionResult ManageEmployeePasswordByAdmin(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                UserEmployeeViewModel userEmployee = new UserEmployeeViewModel();
                var user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    var employee = db.Employees.Where(q => q.UserID == user.UserId).FirstOrDefault();
                    if (employee != null)
                    {
                        userEmployee.EmployeeID = employee.EmployeeID;
                        if (user != null)
                        {
                            userEmployee.UserName = user.UserName;
                            userEmployee.EmployeeID = employee.EmployeeID;
                            userEmployee.UserID = user.UserId;
                            return View(userEmployee);
                        }
                        return View(userEmployee);
                    }
                }
                return View();
            }
        }

        public ActionResult ManageCustomerPasswordByAdmin(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                UserEmployeeViewModel userEmployee = new UserEmployeeViewModel();
                var user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    //user.UserPassword = user.UserPassword + "XXXXXXX";
                    return View(user);
                }
                return View(user);
            }
        }

        public ActionResult ManageConclusionRecommendations()
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

        public ActionResult GeneratePDF()
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

        public ActionResult ManageCustomer()
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

        public ActionResult ManageDocumentTitle()
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

        public ActionResult InspectionSheet(int id)
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

        public ActionResult AddCustLocation()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //Session["CustomerId"] = id;
                return View();
            }
        }

        public ActionResult EditCustLocation(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getCustomerLocationById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteCustLocation(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getCustomerLocationDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult AddCustomer()
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

        public ActionResult EditCustomer(string id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Session["CustomerId"] = id;
                var nId = Convert.ToInt16(id);
                var itm = DatabaseHelper.getCustAndCustLocationDetailsById(nId);
                return View(itm);
            }
        }

        public ActionResult DeleteCustomer(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getCustomerDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult AddCustomerArea(int id)
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

        public ActionResult DeleteCustomerArea(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getAreaDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult AddLocationContact(int? id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return RedirectToAction("Index", "Admin"); // Redirect if id is missing
            }
            ViewData["CustD"] = id.Value;
            var loc = DatabaseHelper.getCustomerById(id.Value);
            ViewData["CustName"] = loc.CustomerName;
            return View();
        }


        public ActionResult DeleteLocationContact(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getLocationContactDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult AddConclusionRecommendations()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Session["ConclusionRecommendationsID"] = 0;
                return View();
            }
        }

        public ActionResult EditConclusionRecommendations(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Session["ConclusionRecommendationsID"] = id;
                var itm = DatabaseHelper.getConclusionRecommendationsById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteConclusionRecommendations(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getConclusionRecommendationsById(id);
                return View(itm);
            }
        }

        public ActionResult AddFacilitiesArea()
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

        public ActionResult EditFacilitiesArea(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getFacilitiesAreaById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteFacilitiesArea(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getFacilitiesAreaById(id);
                return View(itm);
            }
        }


        public ActionResult AddProcessOverview()
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

        public ActionResult EditProcessOverview(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getProcessOverviewById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteProcessOverview(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == 9 || id == 10)
            {
                return RedirectToAction("ManageProcessOverview", "Admin");
            }
            var itm = DatabaseHelper.getProcessOverviewById(id);
            return View(itm);
        }

        public ActionResult AddDeficiencySummary()
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

        public ActionResult EditDeficiencySummary(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDeficiencySummaryById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteDeficiencySummary(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDeficiencySummaryById(id);
                return View(itm);
            }
        }

        public ActionResult AddDeficiencyCategory()
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

        public ActionResult EditDeficiencyCategory(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDeficiencyCategoryById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteDeficiencyCategory(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDeficiencyCategoryById(id);
                return View(itm);
            }
        }

        public ActionResult AddDeficiency()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Session["LoggedInUserName"] = Session["LoggedInUserName"];
                return View();
            }
        }

        public ActionResult EditDeficiency(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDeficiencyById(id);
                itm.DeficiencyDescription = itm.DeficiencyDescription.Replace("'", "");
                return View(itm);
            }
        }

        public ActionResult DeleteDeficiency(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDeficiencyById(id);
                return View(itm);
            }
        }

        public ActionResult ManageIdentifyRackingProfile()
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


        public ActionResult AddIdentifyRackingProfile()
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

        public ActionResult EditIdentifyRackingProfile(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getIdentifyRackingProfileById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteIdentifyRackingProfile(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getIdentifyRackingProfileById(id);
                return View(itm);
            }
        }



        public ActionResult AddManufacturer()
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

        public ActionResult EditManufacturer(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getManufacturerById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteManufacturer(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getManufacturerById(id);
                return View(itm);
            }
        }

        public ActionResult AddComponent()
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
        public ActionResult ImportantSettings()
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
        public ActionResult EditImportantSettings(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getImportantSettingById(id);
                return View(itm);
            }
        }

        public ActionResult EditComponent(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteComponent(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult ManageComponentPrice(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentDetailsById(id);
                return View(itm);
            }
        }
        public ActionResult EditComponentPrice(long id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentPriceDetailsById(id);
                return View(itm);
            }
        }
        public ActionResult DeleteComponentPrice(long id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentPriceDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult AddRackingType()
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

        public ActionResult EditRackingType(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getRackingTypeById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteRackingType(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getRackingTypeById(id);
                return View(itm);
            }
        }

        public ActionResult AddInspectionDue()
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

        public ActionResult EditInspectionDue(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getInspectionById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteInspectionDue(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getInspectionDetailsById(id);
                return View(itm);
            }
        }

        public ActionResult ManageComponentPropertyType()
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

        public ActionResult AddComponentPropertyType()
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

        public ActionResult EditComponentPropertyType(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Session["ComponentPropertyTypeId"] = id;
                var nId = Convert.ToInt16(id);
                var itm = DatabaseHelper.getComponentPropertyTypeById(nId);
                return View(itm);
            }
        }

        public ActionResult DeleteComponentPropertyType(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentPropertyTypeById(id);
                return View(itm);
            }
        }

        public ActionResult AddComponentPropertyValue()
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

        public ActionResult EditComponentPropertyValue(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentPropertyValuesById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteComponentPropertyValue(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getComponentPropertyValuesById(id);
                return View(itm);
            }
        }

        public ActionResult AddDocumentTitle()
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

        public ActionResult EditDocumentTitle(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDocumentTitleById(id);
                return View(itm);
            }
        }


        public ActionResult DeleteDocumentTitle(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getDocumentTitleById(id);
                return View(itm);
            }
        }

        public ActionResult DeleteInspectionFile(int id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var itm = DatabaseHelper.getInspectionFileDrawingById(id);
                return View(itm);
            }
        }

        public ActionResult NotificationPage()
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

        public ActionResult ImportPrice()
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

        //[HttpPost]
        //public ActionResult UploadExcel(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        try
        //        {
        //            // Ensure the file is an Excel file
        //            if (file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) ||
        //                file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        //            {
        //                // Process the uploaded file
        //                using (var stream = file.InputStream)
        //                {
        //                    var dataSet = ReadExcelFile(stream);
        //                    var table = dataSet.Tables[0]; // Get the first table   

        //                    // Example of processing data and passing it to the view
        //                    ViewBag.Data = table;
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Please upload a valid Excel file.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", "An error occurred while processing the file: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Please upload a file.");
        //    }

        //    return View();
        //}


        public string SessionAddCustomerInfo(Customer model)
        {
            if (ModelState.IsValid)
            {
                Session["SessionAddCustomerInfo"] = model;
                return "Ok";
            }
            return null;
        }

        public string SessionAddCustomerLocInfo(CustomerLocation model)
        {
            List<CustomerLocation> clList = new List<CustomerLocation>();
            if (Session["SessionAddCustomerLocInfo"] != null)
            {
                clList = (List<CustomerLocation>)Session["SessionAddCustomerLocInfo"];
            }
            if (ModelState.IsValid)
            {
                clList.Add(model);
                Session["SessionAddCustomerLocInfo"] = clList;
                return "Ok";
            }
            return null;
        }


        [HttpGet]
        public async Task<string> GetSessionAddCustomerLocInfo(CustomerLocation model)
        {
            List<CustomerLocation> clList = new List<CustomerLocation>();
            List<CustomerLocationViewModel> cLVM = new List<CustomerLocationViewModel>();
            if (Session["SessionAddCustomerLocInfo"] != null)
            {
                clList = (List<CustomerLocation>)Session["SessionAddCustomerLocInfo"];
                foreach (var d in clList)
                {
                    CustomerLocationViewModel custLoc = new CustomerLocationViewModel();
                    custLoc.CustomerID = d.CustomerId;
                    custLoc.CustomerLocationID = d.CustomerLocationID;
                    custLoc.LocationName = d.LocationName;
                    custLoc.CustomerAddress = d.CustomerAddress;
                    custLoc.PinCode = d.Pincode;
                    custLoc.CreatedDate = d.CreatedDate;
                    if (d.CityID != null) { custLoc.City = DatabaseHelper.getCitybyId(d.CityID).CityName; }
                    if (d.ProvinceID != null) { custLoc.Province = DatabaseHelper.getProvincebyId(d.ProvinceID).ProvinceName; }
                    if (d.CountryID != null) { custLoc.Country = DatabaseHelper.getCountrybyId(d.CountryID).CountryName; }
                    cLVM.Add(custLoc);
                }
                var list = JsonConvert.SerializeObject(cLVM);
                return list;
            }
            return null;
        }

        public string SessionAddComponentPropertyType(ComponentPropertyTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session["SessionAddComponentPropertyType"] = model;
                return "Ok";
            }
            return null;
        }

        public string SessionAddComponentPropertyValue(ComponentPropertyValue model)
        {
            List<ComponentPropertyValue> cpvList = new List<ComponentPropertyValue>();
            if (Session["SessionAddComponentPropertyValue"] != null)
            {
                cpvList = (List<ComponentPropertyValue>)Session["SessionAddComponentPropertyValue"];
            }
            if (ModelState.IsValid)
            {
                cpvList.Add(model);
                Session["SessionAddComponentPropertyValue"] = cpvList;
                return "Ok";
            }
            return null;
        }

        [HttpGet]
        public async Task<string> GetSessionComponentPropertyValue(ComponentPropertyValue model)
        {
            List<ComponentPropertyValue> listObj = new List<ComponentPropertyValue>();
            List<ComponentPropertyValueViewModel> vmListObj = new List<ComponentPropertyValueViewModel>();
            if (Session["SessionAddComponentPropertyValue"] != null)
            {
                listObj = (List<ComponentPropertyValue>)Session["SessionAddComponentPropertyValue"];
                foreach (var d in listObj)
                {
                    ComponentPropertyValueViewModel vModel = new ComponentPropertyValueViewModel();
                    vModel.ComponentPropertyValue = d.ComponentPropertyValue1;
                    if (d.ComponentPropertyTypeId != 0)
                    {
                        vModel.ComponentPropertyType = DatabaseHelper.getComponentPropertyTypeById(d.ComponentPropertyTypeId).ComponentPropertyTypeName;
                    }
                    vModel.ComponentPropertyValueId = d.ComponentPropertyValueId;
                    vmListObj.Add(vModel);
                }
                var list = JsonConvert.SerializeObject(vmListObj);
                return list;
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetInspectionsForDataTable(DataTableAjaxPostModel model)
        {
            using (var db = new DatabaseEntities())
            {
                string searchTerm = model.search?.value?.ToLower();

                var query = (from i in db.Inspections
                             join c in db.Customers on i.CustomerId equals c.CustomerId into custJoin
                             from c in custJoin.DefaultIfEmpty()

                             join e in db.Employees on i.EmployeeId equals e.EmployeeID into empJoin
                             from e in empJoin.DefaultIfEmpty()

                             join loc in db.CustomerLocations on i.CustomerLocationId equals loc.CustomerLocationID into locJoin
                             from loc in locJoin.DefaultIfEmpty()

                             join area in db.CustomerAreas on i.CustomerAreaID equals area.AreaID into areaJoin
                             from area in areaJoin.DefaultIfEmpty()

                             where i.IsActive == true
                             select new
                             {
                                 i.InspectionId,
                                 i.InspectionDocumentNo,
                                 i.InspectionDate,
                                 i.InspectionStatus,
                                 Customer = c.CustomerName,
                                 Employee = e.EmployeeName,
                                 Area = area.AreaName,
                                 Location = loc.LocationName,
                                 i.InspectionPDFPath
                             }).AsQueryable();

                int totalRecords = query.Count();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(x =>
                        x.InspectionDocumentNo.ToLower().Contains(searchTerm) ||
                        (x.Customer != null && x.Customer.ToLower().Contains(searchTerm)) ||
                        (x.Employee != null && x.Employee.ToLower().Contains(searchTerm)));
                }

                int filteredRecords = query.Count();

                // Default sorting: status → customer → date
                query = query.OrderBy(x => x.InspectionStatus)
                             .ThenBy(x => x.Customer)
                             .ThenByDescending(x => x.InspectionDate);

                // Dynamic ordering
                if (model.order != null && model.order.Count > 0)
                {
                    var sortColumn = model.columns[model.order[0].column].data;
                    var sortDir = model.order[0].dir;

                    switch (sortColumn)
                    {
                        case "InspectionDocumentNo":
                            query = sortDir == "asc" ? query.OrderBy(x => x.InspectionDocumentNo) : query.OrderByDescending(x => x.InspectionDocumentNo);
                            break;
                        case "InspectionDate":
                            query = sortDir == "asc" ? query.OrderBy(x => x.InspectionDate) : query.OrderByDescending(x => x.InspectionDate);
                            break;
                        case "Customer":
                            query = sortDir == "asc" ? query.OrderBy(x => x.Customer) : query.OrderByDescending(x => x.Customer);
                            break;
                        case "Employee":
                            query = sortDir == "asc" ? query.OrderBy(x => x.Employee) : query.OrderByDescending(x => x.Employee);
                            break;
                        case "InspectionStatus":
                            query = sortDir == "asc" ? query.OrderBy(x => x.InspectionStatus) : query.OrderByDescending(x => x.InspectionStatus);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.InspectionDate);
                            break;
                    }
                }

                // Paging
                var pagedData = query
                    .Skip(model.start)
                    .Take(model.length)
                    .ToList();

                // Projection AFTER data is materialized (for formatting)
                var resultData = pagedData.Select(d => new InspectionViewModel
                {
                    InspectionId = d.InspectionId,
                    InspectionDocumentNo = d.InspectionDocumentNo,
                    InspectionDate = d.InspectionDate,
                    InspectionDateFormatted = d.InspectionDate.ToString("yyyy-MM-ddTHH:mm:ss"), 
                    InspectionStatus = d.InspectionStatus,
                    Customer = d.Customer,
                    Employee = d.Employee,
                    CustomerArea = d.Area,
                    CustomerLocation = d.Location,
                    InspectionPDFPath = d.InspectionPDFPath
                    // You can add StatusText or CSSClass here if needed
                }).ToList();

                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = filteredRecords,
                    data = resultData
                }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult Export(string GridHtml)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(GridHtml, stream);
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }

            //var list = DatabaseHelper.getAllEmployeeUser();
            //var studList = list;
            //var HTMLViewStr = RenderViewToString(ControllerContext,
            //"~/Views/Admin/GeneratePDF.cshtml",
            //studList);

            //using (MemoryStream stream = new System.IO.MemoryStream())
            //{
            //StringReader sr = new StringReader(HTMLViewStr);
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            //pdfDoc.Open();
            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            //pdfDoc.Close();
            //return File(stream.ToArray(), "application/pdf", "PDFUsingiTextSharp.pdf");
            //}
        }

        //[Obsolete]
        public ActionResult ToPdfV2(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 2;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

                //strVar += "<section style='width:783px;height:1113px;background:blue;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' align='center' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' align='center' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";
                strVar += "<div><div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width:300px;' /></div> ";
                strVar += "<div style='font-size: 35px;text-decoration: underline;text-transform: uppercase;'>RACKING INSPECTION REPORT</div></div> ";
                strVar += "<div class='' style='height:150px;padding-top: 50px;font-size: 24px;line-height:35px;position: relative;'> ";
                //strVar += "<div>" + iDetails.Customer + "</div> <div style='font-size:14px;'>" + iDetails.CustomerArea + " " + iDetails.CustomerLocation + "</div><div style='font-size:14px;'>" + iDetails.custModel.CustomerAddress + "</div><div class='customer-logo'><img src='" + iDetails.custModel.CustomerLogo + "' style='width:150px;height:auto;' /></div></div> ";
                strVar += "<div>" + iDetails.Customer + "</div> <div style='font-size:14px;'>" + CustomerFullAddress + "</div><div class='customer-logo'><img src='" + iDetails.custModel.CustomerLogo + "' style='width:150px;height:auto;' /></div></div> ";
                strVar += "<div><img src='" + host + "Content/V2/images/mid-logo.jpg' style='width: 250px;margin:50px 0px 50px 0px;' /></div> <div class=''> ";
                strVar += "<div style='float:left;text-align:left;color:#005aab;font-weight:bold;line-height:30px;'><p style='margin: 0px;'>Inspection & Report By</p> <p style='margin: 0px;'>" + iDetails.Employee + ", " + iDetails.empModel.TitleDegrees + "</p> ";
                strVar += "<p style='margin: 0px;'>" + iDetails.empModel.EmployeeEmail + "</p> <p style='color: #999;margin: 0px;'>" + iDetails.empModel.MobileNo + "</p> </div> ";
                strVar += "<div style='float: right;text-align: left;color: #005aab;font-weight: bold;line-height: 30px;'> <p style='margin: 0px;'>Inspection Date:" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</p> <p style='margin: 0px;'>Report Date:" + Convert.ToDateTime(iDetails.Reportdate).ToString("dd MMM yyyy") + "</p> </div> <div style='clear: both'></div> </div> ";
                strVar += "<div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> <div style='margin: 0px auto;float: none;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width:250px;'/></div><div style='clear: both'></div></div> </div> ";
                strVar += "</td> </tr> </tbody> </table> ";
                //strVar += "</section> ";

                //strVar += "<div style='page-break-after: always;'></div> ";

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0mm auto;padding:-25mm 0mm;;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";
                strVar += "<div> ";
                strVar += "<div style='font-size: 30px;text-transform: uppercase;border-bottom: 3px solid #212121;display: inline-block;margin: 20px 0px;font-weight: bold;text-align: center;'>Table of Contents</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 12px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 10px;font-weight: bold;position: relative;bottom: -2px;'>1A. INTRODUCTION</span> ";
                strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 10px;font-weight: bold;position: relative;bottom: -2px;'>2</span> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom:5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;' style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1B. SCOPE OF WORK</span> ";
                strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2</span> ";
                strVar += "</div> ";
                //strVar += "<div style='padding-left: 50px;'> ";
                //strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                //strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>Inspection Locations</span> ";
                //strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>3</span> ";
                //strVar += "</div> ";
                //strVar += "</div> ";
                strVar += "</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += " <span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1C. DAMAGE CLASSIFICATION</span> ";
                strVar += "	 <span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i1CPageNo</span> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Frame Post Damage</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Frame Brace Damage</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Beam Damage</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Safety Recommendations</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += "</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1D. ENGINEERING REVIEW</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i1DPageNo</span></div> ";
                strVar += "</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2A. DEFICIENCY PICTURE REFERENCES</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i2APageNo</span></div> ";
                strVar += "</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2B. REPAIR OR REPLACEMENT BASED ON DEFICIENCIES</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i2BPageNo</span></div> ";
                strVar += "</div> ";
                
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2C. FACILITIES AREA</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i2CPageNo</span> ";
                strVar += "</div> ";

                string fAreaName = " ";
                string[] items = iDetails.FacilitiesAreasIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                strVar += "	<div> ";
                foreach (var f in items)
                {
                    //FacilitiesArea objFacilitiesArea = new FacilitiesArea();
                    if (f != null)
                    {
                        int fId = Convert.ToInt16(f);
                        var fac = db.FacilitiesAreas.Where(x => x.FacilitiesAreaId == fId && x.IsActive == true).FirstOrDefault();
                        if (fac != null)
                        {
                            //objFacilitiesArea = fac;
                            fAreaName = fac.FacilitiesAreaName.Trim();
                            strVar += "<div style='padding-left: 50px;'> ";
                            strVar += "<div> ";
                            strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 2px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'><span style='float:left;padding-right:5px;background:#fff;height:17px;font-weight:bold;position:relative;bottom:-2px;'>" + fAreaName + "</span><span style='float: right;padding-left: 5px;background: #fff;height: 15px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                            strVar += "</div> ";
                            strVar += "</div> ";
                        }
                    }
                }
                strVar += "	</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>3A. CONCLUSION AND RECOMMENDATIONS</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i3APageNo</span></div> ";
                strVar += "</div> ";

                strVar += "</div> ";
                strVar += "	</div> ";
                strVar += "</div> ";
                strVar += "	</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";


                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 15px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1A. Introduction</h2> ";
                strVar += "<p style='font-size: 15px;font-family: Arial, Helvetica, sans-serif;line-height: 24px;margin: 10px 0px;'>" + iDetails.Customer + " has requested Cam Industrial to perform a detailed inspection to identify, report, and address all the damages and deficiencies within their " + iDetails.CustomerArea + " " + iDetails.CustomerLocation + " facility. The following report fulfills this request and additionally provides suggestions that could be used to prevent future damages.</p> ";
                strVar += "<p style='font-size: 15px;font-weight: bold;font-family: Arial, Helvetica, sans-serif;line-height: 24px;margin: 10px 0px;'>Pallet Racking Damage Inspection Report for the purpose of providing detailed information of the condition of the existing pallet racking systems. According to A344-24 Section 5.5.6 \"Users should retain and maintain documents that establish the capacity of racking structures. Various regulations (i.e., OHS acts, codes, and regulations) require the capacity of equipment be known. Given that the structural adequacy of the rack affects the safety of the user and the workplace, the pallet rack capacity should be established by an engineer who is familiar with this Guide.\"</p> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1B. Scope of Work</h2> ";
                strVar += "<h4 style='font-size: 15px;font-weight: bold;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>Process Overview</h4> ";
                if (iDetails.InspectionProcessOverview != null)
                {
                    strVar += "<ul style='margin: 0;padding: 0;'> ";
                    foreach (var Process in iDetails.InspectionProcessOverview)
                    {
                        strVar += "<li style='list-style: decimal;font-size: 14px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 40px;line-height: 12px;'> ";
                        strVar += "" + Process.ProcessOverviewDesc + "  </li> ";
                    }
                    strVar += "</ul> ";
                }
                //if (iDetails.InspectionProcessOverview.Count < 3)
                //{
                //strVar += "<div style='margin: 700px 0px 0px 0px;'> ";
                //}
                //else if (iDetails.InspectionProcessOverview.Count > 4 && iDetails.InspectionProcessOverview.Count < 8)
                //{
                //strVar += "<div style='margin: 400px 0px 0px 0px;'> ";
                //}
                //else
                //{
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                //}

                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: always;'></div> ";


                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "						<div> ";
                strVar += "							<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "						</div> ";
                strVar += "						<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;padding-bottom:500px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "							The observation was performed with specific focus on the conditions visible from the access aisle viewpoint. ";
                strVar += "							In certain cases, it is possible that existing damages were not visible due to obstacles that may have obstructed the view of the ";
                strVar += "							inspector.<strong> Cam Industrial is not responsible for the omission of noteworthy items that are ";
                strVar += "							a result of the accuracy limitations or any incidents that may occur as a result of ";
                strVar += "							omissions.</strong> The inspection and report will provide the customer with up to date information ";
                strVar += "							regarding the condition of their racking system. Damage is measured by tolerable levels of ";
                strVar += "							damage outlined by engineering review. This program can be used as a systematic pallet racking ";
                strVar += "							inspection program done quarterly, semi-annually, or annually. ";
                strVar += "						</p> ";
                strVar += "                     <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "                     <div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "                     <div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "                     <div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "                     <div style='clear: both'></div> ";
                strVar += "						</div> ";
                strVar += "					</div> ";
                strVar += "				</td> ";
                strVar += "			</tr> ";
                strVar += "		</tbody> ";
                strVar += "	</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: always;'></div> ";

                strVar = strVar.Replace("i1CPageNo", pageNo.ToString());

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-15mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "			<div> ";
                strVar += "				<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "			</div> ";
                strVar += "			<h2 style='text-align: left;font-family: Arial, Helvetica, sans-serif;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;'>1C. Damage Classification</h2> ";
                strVar += "			<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "				Damage classification is based on a scale of 1 to 10. (1-3) is considered Minor, which should be ";
                strVar += "				monitored in subsequent inspections. (4-7) is considered Intermediate, which should be repaired or replaced as soon as possible. ";
                strVar += "				Finally, (8-10) is considered Major, these items require immediate action such as offloading the area and quarantining the rack until such time that it can be safely dismantled and repaired.  ";
                strVar += "				Each component has different thresholds for each classification, but these thresholds are not the only determining factors in damage classification.  ";
                strVar += "				Other examples of factors that must be included in damage classification are rust/discoloration, shearing of metal, or multiple damage locations.  ";
                strVar += "				Upright or frame damage can be classified into two categories; damage to frame posts and damage to frame bracing.  ";
                strVar += "				Below the Racking Damage Classification Table and Figure 1 depict the classifications for frame damage. ";
                strVar += "			</p> ";
                strVar += "			<p style='font-size: 14px;line-height: 15px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "				*Adopted from Section 10.7, Rules for the Measurement and Classification of Damage to Uprights and Bracing ";
                strVar += "				Members, published by the Fédération Européenne de la Manutention, Section X, FEM 10.2.04, Guidelines for the Safe Use of Static Steel Racking and Shelving, User Code, November 2001 ";
                strVar += "			</p> ";
                strVar += "			<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 0;color: #000;font-size: 12px;font-family: Arial, Helvetica, sans-serif;line-height: normal;background: #e0e0e0;border-width:1px;'> ";
                strVar += "				<tr> ";
                strVar += "					<td height='30' colspan='3' align='center' bgcolor='#c6d9f1' style='border-width: 1px;font-family: Arial, Helvetica, sans-serif;'><strong>Racking Inspection – Frame Damage Classification</strong></td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Classification</strong></td> ";
                strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Damage Threshold</strong></td> ";
                strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Action</strong></td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td align='center' bgcolor='#00cc00' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Minor (1-3)</td> ";
                strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "						<p>1. < or = 5mm</p> ";
                strVar += "						<p>2. < or = 3mm</p> ";
                strVar += "						<p>3. < or = 10mm</p> ";
                strVar += "					</td> ";
                strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for observation in subsequent inspections to ensure damage does not worsen or affect other areas</td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td align='center' bgcolor='#ffff00' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Intermediate (4-7)</td> ";
                strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "						<p>1. 6mm to 10mm</p> ";
                strVar += "						<p>2. 4mm to 6mm</p> ";
                strVar += "						<p>3. 11mm to 20mm</p> ";
                strVar += "					</td> ";
                strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for replacement of component. Replace or repair component as soon as possible.</td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td align='center' bgcolor='#ff0000' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Major (8-10)</td> ";
                strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "						<p>1. > 10mm</p> ";
                strVar += "						<p>2. > 6mm</p> ";
                strVar += "						<p>3. > 20mm</p> ";
                strVar += "					</td> ";
                strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for replacement of component. Evaluate for immediate action such as offloading affected area or quarantine connecting areas</td> ";
                strVar += "				</tr> ";
                strVar += "			</table> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "				<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "							<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += " <div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "							<div style='clear: both'></div> ";
                strVar += "			</div> ";
                strVar += "		</div> ";
                strVar += "	</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: always;'></div> ";



                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-20mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "	<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 16px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>Frame Post Damage</h2> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "	Damage to frame posts is not acceptable (refer to Figure 1). Posts of storage rack frames are ";
                strVar += "	performance structural members and altering their shape with damage can have a significant ";
                strVar += "	effect on their ability to carry compressive loads. As a general rule, frame posts should be ";
                strVar += "	maintained in a “like new” condition. Therefore, any damage to frame posts should warrant ";
                strVar += "	replacement of the frame post if it is a bolted type or an entire frame if it is a welded type. ";
                strVar += "</p> ";
                strVar += "<p style='text-align: center;'> ";
                strVar += "	<img src='" + host + "Content/V2/images/Farme-img.png' width='520' /> ";
                strVar += "</p> ";
                strVar += "<div style='font-size: 15px;line-height: 24px;margin: 10px 0px;text-align: center;font-family: Arial, Helvetica, sans-serif;'>Figure 1: Measurement method of damages to the frame components</div> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";



                strVar = strVar.Replace("i1DPageNo", pageNo.ToString());

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-50mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1C. Damage Classification (Continued)</h2> ";
                strVar += "<br/> ";
                strVar += "<h6 style='text-align:left;margin:10px 0px 0px 0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Frame Brace Damage</h6> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "The second type of frame damage classification is damage to frame bracing (See item 3 in  ";
                strVar += "<span style='font-weight: bold;'>Figure-1</span>). Damage to diagonal or horizontal braces is not acceptable because they are critical ";
                strVar += "structural members. When a brace is damaged, it becomes crippled and can no longer resist the ";
                strVar += "compressive forces for which they were designed. Damaged braces can be repaired by unbolting ";
                strVar += "and replacing the brace if the frame is kitted or welding in a new brace if it is a welded frame.  ";
                strVar += "Damage to bracing does not necessarily warrant entire frame replacement. ";
                strVar += "</p> ";
                strVar += "<h6 style='text-align:left;margin:0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Beam Damage</h6> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;padding-top:0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Similarly to frame posts and bracing, damage to horizontal beams is not acceptable and can also ";
                strVar += "affect the structural integrity of the component. Damage to beams can be observed as connector ";
                strVar += "damage, dents in the face of the beam and yielded sections of beam where the box or channel is ";
                strVar += "separated. Any type of damage to beams requires replacement of the component. ";
                strVar += "</p> ";
                strVar += "<h6 style='text-align:left;margin:0px;padding-bottom:0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Safety Recommendations</h6> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Every racking system, regardless of the manufacturer, comes equipped with essential components that are set as minimum requirements. These components include frame posts, frame bracing, frame footpads, beams - [step, box or channels], beam connectors, safety pins or bolts, and anchors, and it is essential that they are strictly adhered to. ";
                strVar += "</p> ";
                strVar += "<h2 style='text-align: left;margin:0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1D. Engineering Review</h2> ";
                strVar += "<p style='font-size: 15px;font-family: Arial, Helvetica, sans-serif;line-height: 24px;'> ";
                strVar += "Approval by an engineer [stamped/sealed] indicates that the report has been reviewed and confirms the following:  ";
                strVar += "</p> ";
                strVar += "<ul style='margin: 0;padding: 0;'> ";
                strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>The work was conducted by a trained inspector.</li> ";
                strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>The inspection was conducted using a documented process.</li> ";
                strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'> ";
                strVar += "The decisions, recommendations, or comments that were documented are reasonable, ";
                strVar += "based on the information being reviewed. The engineer will rely on the Inspector’s on-site ";
                strVar += "evaluation and accept it as being accurate where photographs do not fully capture the ";
                strVar += "severity of the damage. ";
                strVar += "</li> ";
                strVar += "</ul> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";


                strVar = strVar.Replace("i2APageNo", pageNo.ToString());
                strVar = strVar.Replace("i2BPageNo", pageNo.ToString());
                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size: 15px;line-height: 24px;'> ";
                strVar += "The actual loads being placed within the system have not been obtained or considered in this ";
                strVar += "report, except where excessive beam deflection has been noted in the deficiency report. The ";
                strVar += "report assumes that the system components have been manufactured according to adequate ";
                strVar += "engineering and manufacturing standards and as such the integrity of the construction of the ";
                strVar += "components has not been tested or verified. ";
                strVar += "</p> ";
                strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size: 15px;line-height: 24px;;margin: 10px 0px 0px 0px;'> ";
                strVar += "Where possible the manufacturer’s published capacities have been used to establish the system ";
                strVar += "capacity, modified in this report as required to reflect the current condition of the components. In lieu of the published capacities, the rack is analyzed based on the information and capacities are established. ";
                strVar += "</p> ";
                strVar += "<h2 style='text-align: left;margin: 15px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2A. Deficiency Picture References</h2> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Pictures are included in this report to provide a visual confirmation of the deficiencies/action items. In some cases, it is possible that existing damages may not have been captured due to them being at high elevation obstructed by the stored products or even in lower levels where pallets or product obstructed the view of the inspector.  ";
                //strVar += "it is possible that pictures of existing damages were not attainable due to height restrictions or ";
                //strVar += "even in lower levels where pallets or product obstructed the view of the inspector. Some pictures ";
                //strVar += "may be omitted from the report if the engineer discovers additional deficiencies or findings after ";
                //strVar += "the initial review completed by the racking inspector. ";
                strVar += "</p> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Pictures represent the condition of the racking as well as indicate the deficiencies/action items that were documented during the racking inspection. ";
                //strVar += "deficiencies/action items that were documented during the racking inspection time of review. ";
                strVar += "</p> ";
                strVar += "<h2 style='text-align: left;margin: 15px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2B. Repair or Replacement Based on Deficiencies (Material Take-Off)</h2> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Based on the racking layout at the time of inspection, the following deficiencies were documented and recommendations made accordingly for repair or replacement. For a detailed list of all deficiencies, comments and recommendations, please view the Racking Inspection Deficiency List. ";
                //strVar += "that are recommended for repair or replacement. For a detailed list of all deficiencies, comments ";
                //strVar += "and recommendations, please view the Racking Inspection Deficiency List. ";
                strVar += "</p> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";

                strVar = strVar.Replace("i2CPageNo", pageNo.ToString());

                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 10px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2C. FACILITIES AREA</h2> ";
                strVar += "<ul style='margin: 0;padding: 0;'> ";
                foreach (var facility in iDetails.InspectionFacilitiesArea)
                {
                    strVar += "<li style='list-style: decimal;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "" + facility.FacilitiesAreaName + " <p>" + facility.FacilitiesAreaDesc + "</p> ";
                    strVar += "</li> ";
                }

                strVar += "</ul> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";


                ////strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                //strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;'> ";
                //strVar += "<tbody> ";
                //strVar += "<tr> ";
                //strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                //strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                //strVar += "<div> ";
                //strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                //strVar += "</div> ";
                //strVar += "<h2 style='text-align: left;margin: 10px 0px 10px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'></h2> ";
                ////strVar += "<ul style='margin: 0;padding: 0;'> ";
                ////foreach (var facility in iDetails.InspectionFacilitiesArea)
                ////{
                ////    strVar += "<li style='list-style: decimal;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                ////    strVar += "" + facility.FacilitiesAreaName + " <p>" + facility.FacilitiesAreaDesc + "</p> ";
                ////    strVar += "</li> ";
                ////}

                ////strVar += "</ul> ";
                //strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                //strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                //strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                //strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                //strVar += "<div style='clear: both'></div> ";
                //strVar += "</div> ";
                //strVar += "</div> ";
                //strVar += "</td> ";
                //strVar += "</tr> ";
                //strVar += "</tbody> ";
                //strVar += "</table> ";
                ////strVar += "</section> ";

                //strVar += "<div style='page-break-after: always;'></div> ";

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";

                //strVar = strVar.Replace("i3APageNo", pageNo.ToString());
                //List<List<Deficiency>> sets = new List<List<Deficiency>>();
                //List<Deficiency> selectedDeficiency = iDetails.ListConclusionandRecommendationsViewModel;
                //int iCount = 0;
                //int iSrNo = 1;
                //if (selectedDeficiency != null)
                //{
                //    for (int i = 0; i < selectedDeficiency.Count; i += 3)
                //    {
                //        List<Deficiency> set = new List<Deficiency>();
                //        for (int j = i; j < i + 3 && j < selectedDeficiency.Count; j++)
                //        {
                //            set.Add(selectedDeficiency[j]);
                //        }
                //        sets.Add(set);
                //    }

                //    foreach (var mainSet in sets)
                //    {
                //        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                //        strVar += "<tbody> ";
                //        strVar += "<tr> ";
                //        strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                //        strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                //        if (iCount == 0)
                //        {
                //            strVar += "<div> ";
                //            strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                //            strVar += "</div> ";
                //            strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>3A. CONCLUSION AND RECOMMENDATIONS</h2> ";
                //        }
                //        iCount += 1;

                //        strVar += "<ul style='margin: 10px 0px 0px 0px;padding: 0;'> ";
                //        foreach (var deficiency in mainSet)
                //        {
                //            strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                //            strVar += iSrNo.ToString() + ". " + deficiency.DeficiencyInfo + ". <p>" + deficiency.DeficiencyDescription + "</p> ";
                //            strVar += "</li> ";
                //            iSrNo += 1;
                //        }
                //        strVar += "</ul> ";
                //        strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                //        strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                //        strVar += "<div style='clear: both'></div> ";
                //        strVar += "</div> ";
                //        strVar += "</div> ";
                //        strVar += "</td> ";
                //        strVar += "</tr> ";
                //        strVar += "</tbody> ";
                //        strVar += "</table> ";
                //        //strVar += "</section> ";
                //    }
                //}
                strVar = strVar.Replace("i3APageNo", pageNo.ToString());
                List<Deficiency> selectedDeficiency = iDetails.ListConclusionandRecommendationsViewModel;
                int iSrNo = 1;
                bool isFirstOverallPage = true;
                int iCount = 0; // You can keep this if needed elsewhere, but it's not used here for paging

                if (selectedDeficiency != null && selectedDeficiency.Count > 0)
                {
                    // Configurable estimates - tune these based on testing your PDF output
                    const int charsPerLine = 70; // Approx chars per line based on ~600px width, 15px Arial font
                    const int maxLinesPerPage = 50; // Approx lines for non-first pages (based on ~25.6cm height, 20px line-height, minus padding/footer)
                    const int maxLinesPerFirstPage = 40; // Lower for first page due to logo + heading

                    int currentLines = 0;
                    int maxLinesForThisPage = maxLinesPerFirstPage; // Start with first page value
                    List<string> currentPageItems = new List<string>(); // Collect li content strings for the current page

                    foreach (var deficiency in selectedDeficiency)
                    {
                        // Estimate lines for this deficiency: 1 for the "number. Info." line + lines for description
                        string infoText = deficiency.DeficiencyInfo ?? "";
                        string descText = deficiency.DeficiencyDescription ?? "";
                        int defLines = 1 + (int)Math.Ceiling((double)descText.Length / charsPerLine);

                        string remainingDesc = descText;
                        bool isFirstPart = true;

                        while (defLines > 0)
                        {
                            // If adding the whole remaining part would exceed, and current page is empty, split it
                            if (currentLines + defLines > maxLinesForThisPage)
                            {
                                if (currentLines == 0)
                                {
                                    // Split the description to fit the remaining lines on this page
                                    int availableLinesForPart = maxLinesForThisPage - 1; // Subtract 1 for the info/continued line
                                    int charsToTake = availableLinesForPart * charsPerLine;

                                    // Take up to charsToTake from remainingDesc, cut at last space
                                    if (charsToTake >= remainingDesc.Length)
                                    {
                                        charsToTake = remainingDesc.Length;
                                    }
                                    int cutIndex = remainingDesc.LastIndexOf(' ', charsToTake);
                                    if (cutIndex == -1) cutIndex = charsToTake; // No space, cut anyway

                                    string chunk = remainingDesc.Substring(0, cutIndex);
                                    string liContent;
                                    if (isFirstPart)
                                    {
                                        liContent = iSrNo + ". " + infoText + ". <p>" + chunk + "</p>";
                                    }
                                    else
                                    {
                                        liContent = " (continued) <p>" + chunk + "</p>";
                                    }
                                    currentPageItems.Add(liContent);

                                    // Update remaining
                                    remainingDesc = remainingDesc.Substring(cutIndex + 1).TrimStart();
                                    defLines = 1 + (int)Math.Ceiling((double)remainingDesc.Length / charsPerLine);
                                    isFirstPart = false;

                                    // Page is now full, so finalize it
                                    AppendPageToStrVar(ref strVar, currentPageItems, isFirstOverallPage, iDetails, host, ref pageNo);
                                    isFirstOverallPage = false;
                                    currentPageItems = new List<string>();
                                    currentLines = 0;
                                    maxLinesForThisPage = maxLinesPerPage; // Subsequent pages use full max
                                }
                                else
                                {
                                    // Current page has content but can't fit whole deficiency - finalize current page and start new
                                    AppendPageToStrVar(ref strVar, currentPageItems, isFirstOverallPage, iDetails, host, ref pageNo);
                                    isFirstOverallPage = false;
                                    currentPageItems = new List<string>();
                                    currentLines = 0;
                                    maxLinesForThisPage = maxLinesPerPage;
                                    continue; // Retry adding to the new page
                                }
                            }
                            else
                            {
                                // Whole remaining part fits - add it
                                string liContent;
                                if (isFirstPart)
                                {
                                    liContent = iSrNo + ". " + infoText + ". <p>" + remainingDesc + "</p>";
                                }
                                else
                                {
                                    liContent = " (continued) <p>" + remainingDesc + "</p>";
                                }
                                currentPageItems.Add(liContent);
                                currentLines += defLines;
                                defLines = 0; // Done with this deficiency
                            }
                        }

                        iSrNo++; // Only increment after fully processing the deficiency
                    }

                    // Add the last page if any items remain
                    if (currentPageItems.Count > 0)
                    {
                        AppendPageToStrVar(ref strVar, currentPageItems, isFirstOverallPage, iDetails, host, ref pageNo);
                    }
                }

        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                if (iCount == 0)
                {
                    strVar += "<div> ";
                    strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                    strVar += "</div> ";
                    strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>3A. CONCLUSION AND RECOMMENDATIONS</h2> ";
                }
                strVar += "<ul style='margin: 0px 0px 25px 0px; padding: 0; '> ";
                strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<p>" + iSrNo.ToString() + ". In certain cases, it is possible that existing damages were not visible due to obstacles that may have obstructed the view of the inspector. We recommend keeping the aisle spacing as clear as possible. According to CSA A344-24: 8.1.4 “The inspection should also make note of poor operating practices such as:</p> ";
                strVar += "    <ul> ";
                strVar += "        <li style='list-style: none;'> ";
                strVar += "            <p><span style='font-weight:bold;'>h)</span> Housekeeping items such as shrink wrap and debris on the floor.</p> ";
                strVar += "            <p><span style='font-weight:bold;'>i)</span> Encroachment of clearance.</p> ";
                strVar += "            <p><span style='font-weight:bold;'>j)</span> Pallets encroaching into the clearance required for sprinkler deflectors.</p> ";
                strVar += "        </li> ";
                strVar += "    </ul> ";
                strVar += "</li> ";

                iSrNo += 1;

                strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<p>" + iSrNo.ToString() + ". We recommend a few  " + iDetails.Customer + " employees to be trained to perform routine internal inspections.  According to CSA A344-24: 8.1.6 “The frequency of both routine and expert inspections should be determined by a risk assessment done by a health and safety professional, the rack vendor, or engineering consultant specialized in rack inspection. In general, the routine inspections should be conducted monthly, and the expert inspections performed annually. The frequency should, as a minimum, ensure compliance with local regulations. Note: The frequency of inspections can change over time depending on the outcome and findings of successive inspections. </p> ";
                strVar += "<p>The risk assessment should consider the following items when establishing frequency:</p> ";
                strVar += "<p> a) nature of the environment in which the pallet rack is situated.</p> ";
                strVar += "<p> b) prior incidence of damage.</p> ";
                strVar += "<p> c) vulnerability to damage and failure due to damage.</p> ";
                strVar += "<p> d) nature of the operation including equipment used around the racks.</p> ";
                strVar += "<p> e) competency and training of the lift truck operators.</p> ";
                strVar += "<p> f) size of the facility;”</p> ";
                strVar += "</li> ";
                strVar += "</ul> ";
                strVar += "<div style='margin: 0px 0px 30px 0px; padding: 0; '> ";
                strVar += "<p>The recommendations and corrective actions provided are based on the inspector’s previous experience, incorporating several factors, and therefore have a subjective component. Cam Industrial Supply understands that users of rack have varying tolerance levels with respect to damages and may differ or disagree with the inspector’s findings.</p> ";
                strVar += "</div> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";

                //strVar += "</section> ";


                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;' /></div> ";
                strVar += "</div> ";

                strVar += "<div> ";
                strVar += "<div style='text-align: center;'> ";
                strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px;margin-bottom:10px;'>" + iDetails.Customer + " - " + iDetails.CustomerLocation + "</div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<div> ";
                strVar += "</div> ";

                strVar += "<div> ";
                strVar += "<div style='text-align: center;'> ";
                //strVar += "<h2 style='text-align: center;margin: 20px 0px 10px 0px;font-size: 28px;text-transform: none;display: inline-block;font-family: Arial, Helvetica, sans-serif;'>" + iDetails.Customer + "</h2> ";
                strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px'>Engineering Notes: " + iDetails.InspectionDocumentNo + "</div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size:15px'> ";
                strVar += "These reports were prepared in accordance with the recommendations outlined in the standards below. A copy of the reports is attached for your reference. ";
                strVar += "</p> ";
                strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 0; color: #000; font-size: 12px; font-family: Arial, Helvetica, sans-serif; line-height: normal; background: #e0e0e0; border-width:1px;'> ";
                if (iDetails.InspectionDocumentTitle != null)
                {
                    foreach (var t in iDetails.InspectionDocumentTitle)
                    {
                        strVar += "<tr> ";
                        strVar += "<td align='center' style='padding:10px 10px 10px 10px; border-width: 1px; background-color: #ADD8E6;'>" + t.DocumentTitle1 + "</td> ";
                        strVar += "<td bgcolor='#ffffff' style='padding:2px 2px 2px 5px; border-width: 1px; '>" + t.DocumentDescription + "</td> ";
                        strVar += "</tr> ";
                    }
                }
                strVar += "</table> ";
                strVar += "<p>Our inspection revealed structural and nonstructural deficiencies mentioned in the section 3A Conclusion and Recommendations and corrective actions are at the owner’s discretion.</p> ";

                strVar += "<p style='line-height:1.0;'>In the report to follow, you will find:</p> ";
                strVar += "<ul style='margin: 0;padding: 0;'> ";
                strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                strVar += "        <p>A detailed list of the deficiencies in the racking system</p> ";
                strVar += "    </li> ";
                strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                strVar += "        <p>Photos of the deficiencies</p> ";
                strVar += "    </li> ";
                strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                strVar += "        <p>Material needed to remedy the deficiencies</p> ";
                strVar += "    </li> ";
                if (iDetails.CapacityTable == 1)
                {
                    strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                    strVar += "        <p>Capacity table of the racking system</p> ";
                    strVar += "    </li> ";
                }
                if (iDetails.PlanElevationDrawing == 1)
                {
                    strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                    strVar += "        <p>Plan and elevation drawing of the racking system.</p> ";
                    strVar += "    </li> ";
                }
                strVar += "</ul> ";
                //strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:15px'> ";
                //strVar += iDetails.empModel.EmployeeName + "," + iDetails.empModel.TitleDegrees;
                //strVar += "</div> ";
                //strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:15px'> ";
                //strVar += "Cam Industrial ";
                //strVar += "</div> ";
                strVar += "<div class='containerEngineerNotes' style='display: flex;justify-content: space-between;align-items: center;margin-bottom: 20px;'> ";
                strVar += "<div class='sectionEngineerNotes' style='flex-basis: 55%;font-family: Arial, Helvetica, sans-serif;font-size: 15px;'> ";
                strVar += "    <p>Yours truly,</p> ";
                strVar += "    <p>Inspection & Report By</p> ";
                strVar += "    <div>" + iDetails.empModel.EmployeeName + "," + iDetails.empModel.TitleDegrees + "</div> ";
                strVar += "    <div>Cam Industrial</div> ";
                //strVar += "    <div class='logoEngineerNotes'> ";
                //strVar += "        <img src='" + host + "/Content/V2/images/logos/CamLogo.png' /> ";
                //strVar += "    </div> ";
                strVar += "</div> ";
                strVar += "<div class='sectionEngineerNotes' style='flex-basis: 45%;font-family: Arial, Helvetica, sans-serif;font-size: 15px;'> ";
                if (string.IsNullOrEmpty(iDetails.empStampingEngModel.EmployeeName) != true)
                {
                    strVar += "    <p>&nbsp;</p> ";
                    strVar += "    <p>Reviewed By</p> ";
                    strVar += "    <div>" + iDetails.empStampingEngModel.EmployeeName + "," + iDetails.empStampingEngModel.TitleDegrees + "</div> ";
                    strVar += "    <div>Cam Industrial</div> ";
                }
                //strVar += "    <div class='logoEngineerNotes'> ";
                //strVar += "        <img src='" + host + "/Content/V2/images/logos/CamLogo.png' /> ";
                //strVar += "    </div> ";
                strVar += "</div> ";
                strVar += "</div> ";

                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";



                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;' /></div> ";
                strVar += "</div> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'> ";
                strVar += "<h2 style='text-align: center;margin: 20px 0px 10px 0px;font-size: 28px;text-transform: none;display: inline-block;'font-family: Arial, Helvetica, sans-serif;'>" + iDetails.Customer + "</h2> ";
                strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px;text-align: center;'>Document Appendix</div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 20px 0px 0px 0px; color: #000; font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: normal; background: #e0e0e0; border-width:1px;'> ";
                strVar += "<tr> ";
                strVar += "<td align='center' style='padding: 20px; border-width: 1px; background-color: #ADD8E6;'> ";
                strVar += "Document Title ";
                strVar += "</td> ";
                strVar += "<td align='center' style='padding: 20px; border-width: 1px; background-color: #ADD8E6;'> ";
                strVar += "Document Number ";
                strVar += "</td> ";
                strVar += "</tr> ";
                foreach (var k in iDetails.InspectionFacilitiesArea)
                {
                    //Facilities Area
                    if (k.FacilitiesAreaId == 2 || k.FacilitiesAreaId == 3)
                    {
                        strVar += "<tr> ";
                        strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                        strVar += "" + k.FacilitiesAreaName + " ";
                        strVar += "</td> ";

                        strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '> ";
                        strVar += "" + iDetails.InspectionDocumentNo + " ";
                        strVar += "</td> ";
                        strVar += "</tr> ";
                    }
                }

                //if (iDetails.CapacityTable == 1)
                //{
                //    strVar += "<tr> ";
                //    strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                //    strVar += "Capacity Table ";
                //    strVar += "</td> ";
                //    strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '> ";
                //    strVar += "" + iDetails.InspectionDocumentNo + " ";
                //    strVar += "</td> ";
                //    strVar += "</tr> ";
                //}
                //if (iDetails.PlanElevationDrawing == 1)
                //{
                //    strVar += "<tr> ";
                //    strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                //    strVar += "Plan and Elevation Drawing ";
                //    strVar += "</td> ";
                //    strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '> ";
                //    strVar += "" + iDetails.InspectionDocumentNo + " ";
                //    strVar += "</td> ";
                //    strVar += "</tr> ";
                //}
                //InspectionFileDrawing List
                foreach (var p in iDetails.ListInspectionFileDrawing)
                {
                    List<List<InspectionFileDrawingChildViewModel>> set1 = new List<List<InspectionFileDrawingChildViewModel>>();
                    List<InspectionFileDrawingChildViewModel> selectedFile = p.inspectionFileDrawingChildViewModels;
                    int iCnt = 0;
                    int iSerNo = 1;
                    if (selectedFile != null)
                    {
                        for (int i = 0; i < selectedFile.Count; i += 4)
                        {
                            List<InspectionFileDrawingChildViewModel> st = new List<InspectionFileDrawingChildViewModel>();
                            for (int j = i; j < i + 4 && j < selectedFile.Count; j++)
                            {
                                st.Add(selectedFile[j]);
                            }
                            set1.Add(st);
                        }
                    }
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                    strVar += "" + p.FileCategory + " ";
                    strVar += "</td> ";
                    strVar += "<td bgcolor='#ffffff' style='padding: 2px;border-width: 1px;'> ";
                    strVar += "<table style='width: 100 %;'> ";
                    foreach (var m in p.inspectionFileDrawingChildViewModels)
                    {
                        strVar += "<tr> ";
                        strVar += "<td bgcolor='#ffffff' style='padding:2px;'><a href = '" + m.FileDrawingPath + "' target = '_blank'>" + m.FileDrawingName + "</a></td> ";
                        strVar += "</tr> ";
                    }
                    strVar += "</table> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                }
                if (iDetails.objQuotation != null)
                {
                    if (iDetails.objQuotation.QuotationNo != null)
                    {
                        strVar += "<tr> ";
                        strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                        strVar += "Quotation";
                        strVar += "</td> ";

                        strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '> ";
                        strVar += iDetails.objQuotation.QuotationNo;
                        strVar += "</td> ";
                        strVar += "</tr> ";
                    }
                }
                strVar += "</table> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: auto;'></div> ";

                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td class='' style='padding: 2px;'> ";
                strVar += " ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //Racking Inspection Deficiency List

                List<List<InspectionDeficiencyViewModel>> setsD = new List<List<InspectionDeficiencyViewModel>>();
                List<InspectionDeficiencyViewModel> selectedDeficiencyList = iDetails.iDefModel;

                setsD = GenerateParentSets(selectedDeficiencyList);
                setsD = UpdatePreviousListWithNextListValues(setsD);
                foreach (var mainSet in setsD)
                {
                    Random rnd = new Random();
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    //strVar += "<section style='width:19cm;height:29.7cm;background:rgba(" + randomColor.R + "," + randomColor.G + "," + randomColor.B + "," + randomColor.A + ");box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";


                    strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tbody> ";
                    strVar += "<tr> ";
                    strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                    //strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";
                    strVar += "<table width='100%' border='1' align='left' cellpadding='0' cellspacing='0'  style='border-collapse: collapse;color:#000000;font-size:11px'> ";
                    strVar += "<tbody > ";
                    strVar += "<tr > ";
                    strVar += "<td  align='center' valign='middle' class=''> ";
                    strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='border-collapse: collapse;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tr valign='middle' > ";
                    strVar += "<td colspan='6' style='font-size:14px;font-weight:600;text-align:center;padding:2px;background:#79addd;font-family: Arial, Helvetica, sans-serif;'>RACKING INSPECTION DEFICIENCY LIST</td> ";
                    strVar += "<td rowspan='4' style='width:150px;' valign='middle' align='center'><table border='0' ><tr><td style='font-size:14px;font-weight:600;text-align:center;padding:2px;'>&nbsp;</td></tr><tr><td style='font-size:9px;font-weight:600;text-align:center;padding:2px;'>&nbsp;</td></tr><tr><td style='valign:middle !important;' align='center'><img src='" + host + "Content/V2/images/table-logo.png' style='width:80%;padding:2px;' /></td></tr><tr><td></td></tr></table></td> ";
                    //strVar += "<td rowspan='4' style='width:150px; height:150px; text-align:center; vertical-align:middle;'><img src='" + host + "Content/V2/images/table-logo.png' style='max-width:100%; max-height:100%; padding:2px;'></td> ";

                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Client:</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.Customer + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Document Number:</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.InspectionDocumentNo + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Type of Racking:</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.InspectionType + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Location/ Address</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + CustomerFullAddress + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Date of Inspection</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Total Inspection Deficiencies</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.iDefModel.Count() + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Contact</td> ";
                    if (iDetails.ListCustomerLocationContacts != null)
                    {
                        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.ListCustomerLocationContacts[0].ContactName + "</td> ";
                    }
                    else
                    {
                        strVar += "<td style='padding:2px;font-size:9px;'></td> ";
                    }

                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Inspected By</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.Employee + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Action Required</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>Yes</td> ";
                    strVar += "</tr> ";
                    strVar += "</table> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr > ";
                    strVar += "<td  align='center' valign='top' class=''> ";
                    strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='font-family: Arial, Helvetica, sans-serif;font-size:9px;'> ";
                    strVar += "<tr> ";
                    strVar += "<td colspan='5' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Racking Classification</td> ";
                    strVar += "<td colspan='2' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Deficiency Type</td> ";
                    strVar += "<td colspan='3' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Damage Assessment</td> ";
                    strVar += "<td colspan='2' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Racking Repair</td> ";
                    strVar += "<td colspan='1' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Engineer Approval</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    //strVar += "<td colspan='5' bgcolor='#0dffff' style='text-align:center;padding:2px;;font-size:9px;'>Location</td> ";
                    //strVar += "<td bgcolor='#ffc820' style='text-align:center;padding:2px;;font-size:9px;'>Category</td> ";
                    //strVar += "<td bgcolor='#ffc820' style='text-align:center;padding:2px;;font-size:9px;'>Description</td> ";
                    //strVar += "<td colspan='3' bgcolor='#ff6160' style='text-align:center;padding:2px;;font-size:9px;'>Action Required</td> ";
                    //strVar += "<td colspan='2' bgcolor='#0dffff' style='text-align:center;padding:2px;font-size:9px;'>Action Taken</td> ";
                    //strVar += "<td colspan='1' bgcolor='#0dffff' style='text-align:center;padding:2px;font-size:9px;'>Status</td> ";
                    strVar += "<td colspan='5' bgcolor='#0dffff' style='text-align:center;padding:2px;border-width: 1px;font-size:9px;'>Location</td>";
                    strVar += "<td bgcolor='#0ABFFF' style='text-align:center;padding:2px;border-width:1px;color:#000000;font-size:9px;'>Category</td>";
                    strVar += "<td bgcolor='#0080FF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Description</td>";
                    strVar += "<td colspan='3' bgcolor='#0055FF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Conclusion</td>";
                    strVar += "<td colspan='2' bgcolor='#002AFF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Action Taken</td>";
                    strVar += "<td colspan='1' bgcolor='#0000FF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Acceptance</td>";
                    strVar += "</tr> ";
                    strVar += "<tr bgcolor='#ffffcc'> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Item ID</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Row Number</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Bay ID/ Number</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Bay/ Frame Side</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Beam/ Frame Level</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>Title</td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>Title</td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Refer report for more detail</div></td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Monitor</div></td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Replace Component</div></td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Repair Component</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Proposed Action</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Severity Index Number</div></td> ";
                    strVar += "<td valign='middle' style='text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Reference Images</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Status</div></td> ";
                    strVar += "<td valign='middle' style='text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Repair Images</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Yes/No</div></td> ";
                    strVar += "</tr> ";
                    strVar += "<tbody> ";
                    foreach (var d in mainSet)
                    {
                        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm -10mm;box-sizing:border-box;'> ";
                        if (d.RowNo == 0)
                        {
                            int i = 0;
                            i = 10;
                        }

                        strVar += "<tr style='border-width:1px;'><i class='fa-solid fa-xmark'></i> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.RowNo + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.CustomerNomenclatureNo + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.CustomerNomenclatureBayNoID + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.BayFrameSide + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.BeamFrameLevel + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.DeficiencyType + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.DeficiencyInfo + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.ActionTaken + "</td> ";
                        //if (d.Action_ReferReport == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'/></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'/></td> "; }
                        //if (d.Action_Monitor == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'/></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td> "; }
                        //if (d.Action_Replace == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td> "; }
                        //if (d.Action_Repair == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td> "; }
                        if (d.Severity_IndexNo >= 1 && d.Severity_IndexNo <= 3)
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#00CC00;font-size:9px;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        if (d.Severity_IndexNo >= 4 && d.Severity_IndexNo <= 7)
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#FFFF00;font-size:9px;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        if (d.Severity_IndexNo >= 8 && d.Severity_IndexNo <= 10)
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#FF0000;font-size:9px;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        strVar += "<td valign='middle' style='width:65px;text-align:left;padding:2px;font-size:9px;'> ";
                        //strVar += "<ul style='list-style: none;'> ";

                        if (d.InspectionDeficiencyPhotoViewModel != null)
                        {

                            strVar += "<table> ";
                            //strVar += "<ul style='display: flex;flex-direction: column;padding-left: 0;margin-bottom: 0;list-style: none;'> ";
                            foreach (var photo in d.InspectionDeficiencyPhotoViewModel)
                            {
                                ////strVar += "<li style='text-align:left;background:#FF0000;> ";
                                strVar += "<tr><td> ";
                                ////strVar += "<p> ";
                                if (d.InspectionDeficiencyPhotoViewModel.Count >= 4)
                                {
                                    strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhotoThumb + "' style='width:64px!important;height:64px!important;' alt=''/></a> ";
                                }
                                else
                                {
                                    strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhotoThumb + "' style='width:64px!important;height:64px!important;' alt=''/></a> ";
                                }
                                strVar += "</td></tr> ";
                            }
                            if (d.InspectionDeficiencyPhotoViewModel.Count == 0)
                            {
                                strVar += "<tr><td> ";
                                strVar += "";
                                strVar += "</td></tr> ";
                            }
                            strVar += "</table> ";
                        }
                        //strVar += "</ul> ";
                        strVar += "</td> ";
                        if (d.InspectionDeficiencyAdminStatus == 0 && d.InspectionDeficiencyTechnicianStatus == 0) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Not Repaired & Not approved</td> "; }
                        else if (d.InspectionDeficiencyAdminStatus == 0 && d.InspectionDeficiencyTechnicianStatus == 1) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Repaired & Not approved</td> "; }
                        else if (d.InspectionDeficiencyAdminStatus == 1 && d.InspectionDeficiencyTechnicianStatus == 0) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Not Repaired & Approved</td> "; }
                        else if (d.InspectionDeficiencyAdminStatus == 1 && d.InspectionDeficiencyTechnicianStatus == 1) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Repaired & Approved</td> "; }
                        else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'></td> "; }
                        //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.InspectionDeficiencyTechnicianStatusText + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'> ";
                        //strVar += "<ul style='list-style: none;'> ";
                        if (d.InspectionDeficiencyPhotoTechnicianViewModel != null)
                        {
                            strVar += "<table> ";
                            foreach (var photo in d.InspectionDeficiencyPhotoTechnicianViewModel)
                            {
                                strVar += "<tr><td> ";
                                strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhotoThumb + "' style='width:64px!important;height:64px!important;' alt=''></a> ";
                                strVar += "</td></tr> ";
                            }
                            if (d.InspectionDeficiencyPhotoTechnicianViewModel.Count == 0)
                            {
                                strVar += "<tr><td> ";
                                strVar += "";
                                strVar += "</td></tr> ";
                            }
                            strVar += "</table> ";
                        }
                        strVar += "</td> ";
                        if (d.InspectionDeficiencyAdminStatus == 1) //&& d.InspectionDeficiencyTechnicianStatus == 1
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>Yes</td> ";
                        }
                        else
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>No</td> ";
                        }
                        //if (d.InspectionDeficiencyAdminStatus == 1) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/active-icon-green.png' style='width:12px;'></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'></td> "; }
                        strVar += "</tr> ";
                    }
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "</tbody> ";
                    strVar += "</table> ";

                    //strVar += "</div> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    //strVar += "</section> ";
                    //strVar += "<div style='page-break-after: auto;'></div> ";
                    //iDSrNo += 1;                    
                }


                //strVar += "<div style='page-break-after: auto;'></div> ";



                //Material Take-off List


                List<List<InspectionDeficiencyMTOViewModel>> setsDMTO = new List<List<InspectionDeficiencyMTOViewModel>>();
                List<InspectionDeficiencyMTOViewModel> selectedDeficiencyListMTO = iDetails.iMTOModel;
                //int iMCount = 0;
                //int iMSrNo = 1;
                if (selectedDeficiencyListMTO != null)
                {

                    for (int i = 0; i < selectedDeficiencyListMTO.Count; i += 8)
                    {
                        List<InspectionDeficiencyMTOViewModel> set = new List<InspectionDeficiencyMTOViewModel>();
                        for (int j = i; j < i + 8 && j < selectedDeficiencyList.Count; j++)
                        {
                            set.Add(selectedDeficiencyListMTO[j]);
                        }
                        setsDMTO.Add(set);
                    }
                }
                //iMCount += 1;

                foreach (var mainSet in setsDMTO)
                {
                    Random rnd = new Random();
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    //strVar += "<section style='width:19cm;height:29.7cm;background:rgba(" + randomColor.R + "," + randomColor.G + "," + randomColor.B + "," + randomColor.A + ");box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                    strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tbody> ";
                    strVar += "<tr> ";
                    strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                    //strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";

                    strVar += "<table width='100%' border='1' align='left' cellpadding='0' cellspacing='0'  style='border-collapse: collapse;color:#000000;font-size:11px;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tr> ";
                    strVar += "<td height='30' colspan='9' align='center' bgcolor='#80b1de' style='font-family: Arial, Helvetica, sans-serif;'>RACKING INSPECTION - MATERIAL TAKE OFF LIST</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td width='100' bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Client:</td> ";
                    strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + iDetails.Customer + "</td> ";
                    strVar += "<td width='100' align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Type of Racking:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + iDetails.InspectionType + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Location & Address:</td> ";
                    strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + CustomerFullAddress + "</td> ";
                    strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Date of Inspection:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Contact:</td> ";
                    if (iDetails.ListCustomerLocationContacts != null)
                    {
                        strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + iDetails.ListCustomerLocationContacts[0].ContactName + "</td> ";
                    }
                    else
                    {
                        strVar += "<td colspan='4' style='padding:2px;font-size:9px;'></td> ";
                    }
                    strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Inspection By:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + iDetails.Employee + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Project Number:</td> ";
                    strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + iDetails.InspectionDocumentNo + "</td> ";
                    strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Report/ BOM By:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + iDetails.Employee + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td colspan='9' align='center' valign='middle'> ";
                    strVar += "<div style='float:left;padding-left:30px;'><img src='" + host + "/Content/V2/images/table-logo.png' style='width:120px;margin-top:5px;'></div> ";
                    strVar += "<div style='float:right;padding-right:30px;'><img src='" + host + "/Content/V2/images/footer-logo.jpg' style='width:250px;margin:10px 0px;'></div> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Severity<br />Index</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Action Item Reference</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Component</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Manufacturer</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Vendor ID</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Type</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>CAM ID</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Size/ Description</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Quantity<br />Required</td> ";
                    strVar += "</tr> ";
                    strVar += "<tbody> ";
                    foreach (var d in mainSet)
                    {
                        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm -10mm;box-sizing:border-box;'> ";
                        InspectionDeficiencyMTODetailViewModel objMTODetails = new InspectionDeficiencyMTODetailViewModel();
                        var iMTOdetails = db.InspectionDeficiencyMTODetails.Where(h => h.InspectionDeficiencyMTOId == d.InspectionDeficiencyMTOId).ToList();
                        var fType = " ";
                        if (iMTOdetails.Count != 0)
                        {
                            foreach (var mtoDetail in iMTOdetails)
                            {
                                if (mtoDetail.ComponentPropertyTypeId != 0)
                                {
                                    var type = DatabaseHelper.getComponentPropertyTypeById(mtoDetail.ComponentPropertyTypeId);
                                    if (type != null)
                                    {
                                        if (type.ComponentPropertyTypeName.Contains("Type"))
                                        {
                                            var value = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == mtoDetail.ComponentPropertyValueId && x.ComponentPropertyTypeId == mtoDetail.ComponentPropertyTypeId).ToList();
                                            if (value.Count != 0)
                                            {
                                                foreach (var v in value)
                                                {
                                                    fType += v.ComponentPropertyValue1 + ", ";
                                                    fType = fType.Trim();
                                                    fType = fType.Remove(fType.Length - 1);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        if (fType.Contains(","))
                        {
                            fType = fType.Replace(",", ", ");
                        }
                        strVar += "<tr style='border-width:1px;'> ";
                        if (d.Severity_IndexNo >= 1 && d.Severity_IndexNo <= 3)
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#00CC00;color:#000000;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        else if (d.Severity_IndexNo >= 4 && d.Severity_IndexNo <= 7)
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#FFFF00;color:#000000;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        else if (d.Severity_IndexNo >= 8 && d.Severity_IndexNo <= 10)
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#FF0000;color:#000000;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        else
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;'></td> ";
                        }
                        strVar += "<td align='center' bgcolor='#d9e1f2' style='padding:2px'>" + d.DeficiencyRowNo + "</td> ";
                        strVar += "<td style='padding:2px 8px'>" + d.ComponentName + "</td> ";
                        strVar += "<td style='padding:2px 8px'>" + d.ManufacturerName + "</td> ";
                        strVar += "<td style='padding:2px 8px'></td> ";
                        strVar += "<td style='padding:2px 8px'>" + fType.ToString() + "</td> ";
                        strVar += "<td style='padding:2px 8px'></td> ";
                        strVar += "<td style='padding:2px 8px; font-size: 10px;'>" + d.Size_Description + "</td> ";
                        strVar += "<td style='padding:2px 8px'>" + d.QuantityReq + "</td> ";
                        strVar += "</tr> ";
                    }
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    //strVar += "</div> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    //strVar += "</section> ";
                }
                if (iDetails.objQuotation != null)
                {
                    if (iDetails.objQuotation.QuotationNo != null && iDetails.objQuotation.QuotationStatus > 5)
                    {
                        List<List<QuotationItem>> objQuotationItemsInner = new List<List<QuotationItem>>();
                        if (iDetails.objQuotation.objQuotationItems.Count > 0)
                        {
                            objQuotationItemsInner = SplitList(iDetails.objQuotation.objQuotationItems, 10);
                        }
                        int i = 1;
                        foreach (var itemSet in objQuotationItemsInner)
                        {
                            strVar += "<table style='width:18.5cm; height:29.7cm; background:white; margin:2mm auto; padding:0; box-sizing:border-box;font-family: Arial, Helvetica, sans-serif;' border='0' align='center' cellpadding='0' cellspacing='0'>";
                            strVar += "    <thead>";
                            strVar += "        <tr class='header'>";
                            strVar += "            <td style='text-align: center;'>";
                            strVar += "                <img src='" + host + "Content/V2/images/quoteheader.png' style='width: 100%; max-width: 100%; height: auto;'>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "    </thead>";
                            strVar += "    <tbody>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 10px 20px;font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                <div style='font-size: 16px; font-weight: bold;font-family: Arial, Helvetica, sans-serif;'>Sales Quote " + iDetails.objQuotation.QuotationNo + " </div>";
                            strVar += "                <div style='font-size: 16px; line-height: 22px; padding-top: 5px;font-family: Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(iDetails.objQuotation.QuotationDate).ToString("dd MMM yyyy") + "<br>Page " + Convert.ToString(i) + "/" + Convert.ToString(objQuotationItemsInner.Count) + "</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 10px 20px;font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                <div style='width: 40%; float: left; line-height: 22px;font-size: 16px;'>" + iDetails.Customer + "<br>" + iDetails.CustomerArea + "<br>" + iDetails.CustomerLocation + "</div>";
                            strVar += "                <div style='width: 40%; float: left; line-height: 22px;font-size: 16px;'><b>Ship to:</b><br>" + iDetails.Customer + "<br>" + iDetails.custModel.CustomerAddress + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: right;font-size: 16px;'></div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 5px 5px;;font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                <div style='width: 20%; float: left; line-height: 22px;font-size: 16px;'><b>Your Reference</b><br>" + iDetails.objQuotation.YourReference + "</div>";
                            strVar += "                <div style='width: 20%; float: left; line-height: 22px;font-size: 16px;'><b>Valid to</b><br>" + iDetails.objQuotation.ValidTo + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: left;font-size: 16px;'><b>Salesperson</b><br>" + iDetails.objQuotation.QuotationSalesPersonName + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: left;font-size: 16px;'><b>Payment Terms</b><br>" + iDetails.objQuotation.PaymentTerms + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: left;font-size: 16px;'><b>Shipment Method</b><br> " + iDetails.objQuotation.ShipmentMethod + "</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 5px 5px;'>";
                            strVar += "                <div style='font-size: 16px;font-weight: bold;'>Rack Inspection Repair - " + Convert.ToDateTime(iDetails.InspectionDate).ToString("yyyy") + "</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td style='padding: 10px 10px;vertical-align: top;'>";
                            strVar += "                <table border='0' cellpadding='0' cellspacing='0' class='amount' width='100%' style='font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                    <thead>";
                            strVar += "                        <tr>";
                            strVar += "                            <th align='left' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Item No.</th>";
                            strVar += "                            <th align='left' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:20%'>Description</th>";
                            strVar += "                            <th align='right' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Item Price($)</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Weight</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:5%'>Quantity</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Total Weight</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Line Amount($)</th>";
                            strVar += "                        </tr>";
                            strVar += "                    </thead>";
                            strVar += "                    <tbody>";
                            if (iDetails.objQuotation.objQuotationItems.Count > 0)
                            {
                                //foreach (var item in iDetails.objQuotation.objQuotationItems)
                                //{
                                //    strVar += "<tr>";
                                //    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPartNo + "</td>";
                                //    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemDescription + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPrice + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeight + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemQuantity + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeightTotal + "</td>";
                                //    strVar += "<td align='right' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.LineTotal + "</td>";
                                //    strVar += "</tr>";
                                //}
                                foreach (var item in itemSet)
                                {
                                    strVar += "<tr>";
                                    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPartNo + "</td>";
                                    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemDescription + "</td>";
                                    if (item.IsTBD == true)
                                    {
                                        strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>TBD</td>";
                                    }
                                    else
                                    {
                                        strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPrice + "</td>";
                                    }

                                    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeight + "</td>";
                                    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemQuantity + "</td>";
                                    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeightTotal + "</td>";
                                    strVar += "<td align='right' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.LineTotal + "</td>";
                                    strVar += "</tr>";
                                }
                            }
                            strVar += "<tr>";
                            strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>Labour Cost</td>";
                            strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>"; //iDetails.objQuotation.TotalLabour
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='right' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + iDetails.objQuotation.TotalUnitPrice + "</td>";
                            strVar += "</tr>";
                            //strVar += "                        <tr>";
                            //strVar += "                            <td style='font-size: 16px; padding: 10px 0;'>68982</td>";
                            //strVar += "                            <td align='left' style='font-size: 16px; padding: 10px 0; line-height: 26px;'>ANCHOR BOLT - WEDGE 0.5' X 4.5' KB1, EACH</td>";
                            //strVar += "                            <td align='center' style='font-size: 16px; padding: 10px 0;'>60 Each</td>";
                            //strVar += "                            <td align='center' style='font-size: 16px; padding: 10px 0;'>2.40</td>";
                            //strVar += "                            <td align='center' style='font-size: 16px; padding: 10px 0;'>144.00</td>";
                            //strVar += "                        </tr>";
                            strVar += "                    </tbody>";
                            if (i == objQuotationItemsInner.Count)
                            {
                                strVar += "                    <tfoot >";
                                strVar += "                        <tr><td colspan='7' style='border-top:1px solid #000; font-size: 9px; text-transform: uppercase;'></td></tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4' rowspan='4' style='font-size: 9px; text-transform: uppercase;'>" + iDetails.objQuotation.QuotationNotes.Replace("\n", "<br/>") + "</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                //strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size: 12px;padding:5px 0;' align='center'><b>Subtotal</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size: 12px; padding: 5px 0;'><b>$" + iDetails.objQuotation.Subtotal + "</b></td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                //strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'><b>TAX(" + iDetails.objQuotation.GSTPer + "%)</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding: 5px 0;'><b>$" + iDetails.objQuotation.GSTValue + "</b></td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                //strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'><b>Total</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding:5px 0;'><b>$" + iDetails.objQuotation.Total + "</b></td>";
                                strVar += "                        </tr>";
                                strVar += "                    </tfoot>";
                            }
                            else
                            {
                                strVar += "                    <tfoot>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='7' style='border-bottom:1px solid #000; font-size: 9px; text-transform: uppercase;padding: 20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size: 12px;padding:5px 0;' align='center'>&nbsp;</td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size: 12px; padding: 20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'>&nbsp;</td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding: 20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'><b>&nbsp;</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding:20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                    </tfoot>";
                            }
                            strVar += "                </table>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 10px 20px;'>";
                            strVar += "                <div style='width: 25%; float: left; line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>VAT Registration No.</span><br>R100741693</div>";
                            strVar += "                <div style='width: 25%; float: left; line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>Home Page</span><br>www.camindustrial.net</div>";
                            strVar += "                <div style='width: 25%; float: left; text-align: left;line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>Phone No.</span><br>(403) 720-0076</div>";
                            strVar += "                <div style='width: 25%; float: left; text-align: left;line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>Email</span><br>info@camindustrial.net</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "    </tbody>";
                            strVar += "    <tfoot>";
                            strVar += "        <tr class='footer'>";
                            strVar += "            <td style='text-align: center;'>";
                            strVar += "                <img src='" + host + "Content/V2/images/quotefooter.png' style='width: 100%; max-width: 100%; height: auto;'>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "    </tfoot>";
                            strVar += "</table>";

                            i += 1;
                        }
                    }
                }
                //byte[] output;

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_RackInspection.pdf");
                }
            }
            return null;
        }
        private void AppendPageToStrVar(ref string strVar, List<string> pageItems, bool isFirstPage, InspectionViewModel iDetails, string host, ref int pageNo)
        {
            //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
            strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
            strVar += "<tbody> ";
            strVar += "<tr> ";
            strVar += "<td valign='top' class='' style='padding: 2px;'> ";
            strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";

            if (isFirstPage)
            {
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>3A. CONCLUSION AND RECOMMENDATIONS</h2> ";
            }

            strVar += "<ul style='margin: 10px 0px 0px 0px;padding: 0;'> ";
            foreach (var item in pageItems)
            {
                strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += item;
                strVar += "</li> ";
            }
            strVar += "</ul> ";
            strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
            strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
            strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
            strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
            strVar += "<div style='clear: both'></div> ";
            strVar += "</div> ";
            strVar += "</div> ";
            strVar += "</td> ";
            strVar += "</tr> ";
            strVar += "</tbody> ";
            strVar += "</table> ";
            //strVar += "</section> ";
        }
        public ActionResult ToPdfV2Introduction(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 2;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

                //strVar += "<section style='width:783px;height:1113px;background:blue;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' align='center' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' align='center' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";
                strVar += "<div><div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width:300px;' /></div> ";
                strVar += "<div style='font-size: 35px;text-decoration: underline;text-transform: uppercase;'>RACKING INSPECTION REPORT</div></div> ";
                strVar += "<div class='' style='height:150px;padding-top: 50px;font-size: 24px;line-height:35px;position: relative;'> ";
                //strVar += "<div>" + iDetails.Customer + "</div> <div style='font-size:14px;'>" + iDetails.CustomerArea + " " + iDetails.CustomerLocation + "</div><div style='font-size:14px;'>" + iDetails.custModel.CustomerAddress + "</div><div class='customer-logo'><img src='" + iDetails.custModel.CustomerLogo + "' style='width:150px;height:auto;' /></div></div> ";
                strVar += "<div>" + iDetails.Customer + "</div> <div style='font-size:14px;'>" + CustomerFullAddress + "</div><div class='customer-logo'><img src='" + iDetails.custModel.CustomerLogo + "' style='width:150px;height:auto;' /></div></div> ";
                strVar += "<div><img src='" + host + "Content/V2/images/mid-logo.jpg' style='width: 250px;margin:50px 0px 50px 0px;' /></div> <div class=''> ";
                strVar += "<div style='float:left;text-align:left;color:#005aab;font-weight:bold;line-height:30px;'><p style='margin: 0px;'>Inspection & Report By</p> <p style='margin: 0px;'>" + iDetails.Employee + ", " + iDetails.empModel.TitleDegrees + "</p> ";
                strVar += "<p style='margin: 0px;'>" + iDetails.empModel.EmployeeEmail + "</p> <p style='color: #999;margin: 0px;'>" + iDetails.empModel.MobileNo + "</p> </div> ";
                strVar += "<div style='float: right;text-align: left;color: #005aab;font-weight: bold;line-height: 30px;'> <p style='margin: 0px;'>Inspection Date:" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</p> <p style='margin: 0px;'>Report Date:" + Convert.ToDateTime(iDetails.Reportdate).ToString("dd MMM yyyy") + "</p> </div> <div style='clear: both'></div> </div> ";
                strVar += "<div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> <div style='margin: 0px auto;float: none;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width:250px;'/></div><div style='clear: both'></div></div> </div> ";
                strVar += "</td> </tr> </tbody> </table> ";
                //strVar += "</section> ";

                //strVar += "<div style='page-break-after: always;'></div> ";

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0mm auto;padding:-25mm 0mm;;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";
                strVar += "<div> ";
                strVar += "<div style='font-size: 30px;text-transform: uppercase;border-bottom: 3px solid #212121;display: inline-block;margin: 20px 0px;font-weight: bold;text-align: center;'>Table of Contents</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 12px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 10px;font-weight: bold;position: relative;bottom: -2px;'>1A. INTRODUCTION</span> ";
                strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 10px;font-weight: bold;position: relative;bottom: -2px;'>2</span> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom:5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;' style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1B. SCOPE OF WORK</span> ";
                strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2</span> ";
                strVar += "</div> ";
                //strVar += "<div style='padding-left: 50px;'> ";
                //strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                //strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>Inspection Locations</span> ";
                //strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>3</span> ";
                //strVar += "</div> ";
                //strVar += "</div> ";
                strVar += "</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += " <span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1C. DAMAGE CLASSIFICATION</span> ";
                strVar += "	 <span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i1CPageNo</span> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Frame Post Damage</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Frame Brace Damage</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Beam Damage</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " <div> ";
                strVar += " <div style='padding-left: 50px;'> ";
                strVar += " <div> ";
                strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'> ";
                strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'>Safety Recommendations</span> ";
                strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += " </div> ";
                strVar += "</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1D. ENGINEERING REVIEW</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i1DPageNo</span></div> ";
                strVar += "</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2A. DEFICIENCY PICTURE REFERENCES</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i2APageNo</span></div> ";
                strVar += "</div> ";
                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2B. REPAIR OR REPLACEMENT BASED ON DEFICIENCIES</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i2BPageNo</span></div> ";
                strVar += "</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'> ";
                strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2C. FACILITIES AREA</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i2CPageNo</span> ";
                strVar += "</div> ";

                string fAreaName = " ";
                string[] items = iDetails.FacilitiesAreasIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                strVar += "	<div> ";
                foreach (var f in items)
                {
                    //FacilitiesArea objFacilitiesArea = new FacilitiesArea();
                    if (f != null)
                    {
                        int fId = Convert.ToInt16(f);
                        var fac = db.FacilitiesAreas.Where(x => x.FacilitiesAreaId == fId && x.IsActive == true).FirstOrDefault();
                        if (fac != null)
                        {
                            //objFacilitiesArea = fac;
                            fAreaName = fac.FacilitiesAreaName.Trim();
                            strVar += "<div style='padding-left: 50px;'> ";
                            strVar += "<div> ";
                            strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 2px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'><span style='float:left;padding-right:5px;background:#fff;height:17px;font-weight:bold;position:relative;bottom:-2px;'>" + fAreaName + "</span><span style='float: right;padding-left: 5px;background: #fff;height: 15px;font-weight: bold;position: relative;bottom: -2px;'></span></div> ";
                            strVar += "</div> ";
                            strVar += "</div> ";
                        }
                    }
                }
                strVar += "	</div> ";

                strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'> ";
                strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 10px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>3A. CONCLUSION AND RECOMMENDATIONS</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>i3APageNo</span></div> ";
                strVar += "</div> ";

                strVar += "</div> ";
                strVar += "	</div> ";
                strVar += "</div> ";
                strVar += "	</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";


                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 15px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1A. Introduction</h2> ";
                strVar += "<p style='font-size: 15px;font-family: Arial, Helvetica, sans-serif;line-height: 24px;margin: 10px 0px;'>" + iDetails.Customer + " has requested Cam Industrial to perform a detailed inspection to identify, report, and address all the damages and deficiencies within their " + iDetails.CustomerArea + " " + iDetails.CustomerLocation + " facility. The following report fulfills this request and additionally provides suggestions that could be used to prevent future damages.</p> ";
                strVar += "<p style='font-size: 15px;font-weight: bold;font-family: Arial, Helvetica, sans-serif;line-height: 24px;margin: 10px 0px;'>Pallet Racking Damage Inspection Report for the purpose of providing detailed information of the condition of the existing pallet racking systems. According to A344-24 Section 5.5.6 \"Users should retain and maintain documents that establish the capacity of racking structures. Various regulations (i.e., OHS acts, codes, and regulations) require the capacity of equipment be known. Given that the structural adequacy of the rack affects the safety of the user and the workplace, the pallet rack capacity should be established by an engineer who is familiar with this Guide.\"</p> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1B. Scope of Work</h2> ";
                strVar += "<h4 style='font-size: 15px;font-weight: bold;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>Process Overview</h4> ";
                if (iDetails.InspectionProcessOverview != null)
                {
                    strVar += "<ul style='margin: 0;padding: 0;'> ";
                    foreach (var Process in iDetails.InspectionProcessOverview)
                    {
                        strVar += "<li style='list-style: decimal;font-size: 14px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 40px;line-height: 12px;'> ";
                        strVar += "" + Process.ProcessOverviewDesc + "  </li> ";
                    }
                    strVar += "</ul> ";
                }
                //if (iDetails.InspectionProcessOverview.Count < 3)
                //{
                //strVar += "<div style='margin: 700px 0px 0px 0px;'> ";
                //}
                //else if (iDetails.InspectionProcessOverview.Count > 4 && iDetails.InspectionProcessOverview.Count < 8)
                //{
                //strVar += "<div style='margin: 400px 0px 0px 0px;'> ";
                //}
                //else
                //{
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                //}

                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: always;'></div> ";


                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "						<div> ";
                strVar += "							<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "						</div> ";
                strVar += "						<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;padding-bottom:500px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "							The observation was performed with specific focus on the conditions visible from the access aisle viewpoint. ";
                strVar += "							In certain cases, it is possible that existing damages were not visible due to obstacles that may have obstructed the view of the ";
                strVar += "							inspector.<strong> Cam Industrial is not responsible for the omission of noteworthy items that are ";
                strVar += "							a result of the accuracy limitations or any incidents that may occur as a result of ";
                strVar += "							omissions.</strong> The inspection and report will provide the customer with up to date information ";
                strVar += "							regarding the condition of their racking system. Damage is measured by tolerable levels of ";
                strVar += "							damage outlined by engineering review. This program can be used as a systematic pallet racking ";
                strVar += "							inspection program done quarterly, semi-annually, or annually. ";
                strVar += "						</p> ";
                strVar += "                     <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "                     <div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "                     <div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "                     <div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "                     <div style='clear: both'></div> ";
                strVar += "						</div> ";
                strVar += "					</div> ";
                strVar += "				</td> ";
                strVar += "			</tr> ";
                strVar += "		</tbody> ";
                strVar += "	</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: always;'></div> ";

                strVar = strVar.Replace("i1CPageNo", pageNo.ToString());

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-15mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "			<div> ";
                strVar += "				<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "			</div> ";
                strVar += "			<h2 style='text-align: left;font-family: Arial, Helvetica, sans-serif;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;'>1C. Damage Classification</h2> ";
                strVar += "			<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "				Damage classification is based on a scale of 1 to 10. (1-3) is considered Minor, which should be ";
                strVar += "				monitored in subsequent inspections. (4-7) is considered Intermediate, which should be repaired or replaced as soon as possible. ";
                strVar += "				Finally, (8-10) is considered Major, these items require immediate action such as offloading the area and quarantining the rack until such time that it can be safely dismantled and repaired.  ";
                strVar += "				Each component has different thresholds for each classification, but these thresholds are not the only determining factors in damage classification.  ";
                strVar += "				Other examples of factors that must be included in damage classification are rust/discoloration, shearing of metal, or multiple damage locations.  ";
                strVar += "				Upright or frame damage can be classified into two categories; damage to frame posts and damage to frame bracing.  ";
                strVar += "				Below the Racking Damage Classification Table and Figure 1 depict the classifications for frame damage. ";
                strVar += "			</p> ";
                strVar += "			<p style='font-size: 14px;line-height: 15px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "				*Adopted from Section 10.7, Rules for the Measurement and Classification of Damage to Uprights and Bracing ";
                strVar += "				Members, published by the Fédération Européenne de la Manutention, Section X, FEM 10.2.04, Guidelines for the Safe Use of Static Steel Racking and Shelving, User Code, November 2001 ";
                strVar += "			</p> ";
                strVar += "			<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 0;color: #000;font-size: 12px;font-family: Arial, Helvetica, sans-serif;line-height: normal;background: #e0e0e0;border-width:1px;'> ";
                strVar += "				<tr> ";
                strVar += "					<td height='30' colspan='3' align='center' bgcolor='#c6d9f1' style='border-width: 1px;font-family: Arial, Helvetica, sans-serif;'><strong>Racking Inspection – Frame Damage Classification</strong></td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Classification</strong></td> ";
                strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Damage Threshold</strong></td> ";
                strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Action</strong></td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td align='center' bgcolor='#00cc00' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Minor (1-3)</td> ";
                strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "						<p>1. < or = 5mm</p> ";
                strVar += "						<p>2. < or = 3mm</p> ";
                strVar += "						<p>3. < or = 10mm</p> ";
                strVar += "					</td> ";
                strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for observation in subsequent inspections to ensure damage does not worsen or affect other areas</td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td align='center' bgcolor='#ffff00' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Intermediate (4-7)</td> ";
                strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "						<p>1. 6mm to 10mm</p> ";
                strVar += "						<p>2. 4mm to 6mm</p> ";
                strVar += "						<p>3. 11mm to 20mm</p> ";
                strVar += "					</td> ";
                strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for replacement of component. Replace or repair component as soon as possible.</td> ";
                strVar += "				</tr> ";
                strVar += "				<tr> ";
                strVar += "					<td align='center' bgcolor='#ff0000' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Major (8-10)</td> ";
                strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "						<p>1. > 10mm</p> ";
                strVar += "						<p>2. > 6mm</p> ";
                strVar += "						<p>3. > 20mm</p> ";
                strVar += "					</td> ";
                strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for replacement of component. Evaluate for immediate action such as offloading affected area or quarantine connecting areas</td> ";
                strVar += "				</tr> ";
                strVar += "			</table> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "				<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "							<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += " <div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "							<div style='clear: both'></div> ";
                strVar += "			</div> ";
                strVar += "		</div> ";
                strVar += "	</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: always;'></div> ";



                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-20mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "	<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 16px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>Frame Post Damage</h2> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "	Damage to frame posts is not acceptable (refer to Figure 1). Posts of storage rack frames are ";
                strVar += "	performance structural members and altering their shape with damage can have a significant ";
                strVar += "	effect on their ability to carry compressive loads. As a general rule, frame posts should be ";
                strVar += "	maintained in a “like new” condition. Therefore, any damage to frame posts should warrant ";
                strVar += "	replacement of the frame post if it is a bolted type or an entire frame if it is a welded type. ";
                strVar += "</p> ";
                strVar += "<p style='text-align: center;'> ";
                strVar += "	<img src='" + host + "Content/V2/images/Farme-img.png' width='520' /> ";
                strVar += "</p> ";
                strVar += "<div style='font-size: 15px;line-height: 24px;margin: 10px 0px;text-align: center;font-family: Arial, Helvetica, sans-serif;'>Figure 1: Measurement method of damages to the frame components</div> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";



                strVar = strVar.Replace("i1DPageNo", pageNo.ToString());

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-50mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1C. Damage Classification (Continued)</h2> ";
                strVar += "<br/> ";
                strVar += "<h6 style='text-align:left;margin:10px 0px 0px 0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Frame Brace Damage</h6> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "The second type of frame damage classification is damage to frame bracing (See item 3 in  ";
                strVar += "<span style='font-weight: bold;'>Figure-1</span>). Damage to diagonal or horizontal braces is not acceptable because they are critical ";
                strVar += "structural members. When a brace is damaged, it becomes crippled and can no longer resist the ";
                strVar += "compressive forces for which they were designed. Damaged braces can be repaired by unbolting ";
                strVar += "and replacing the brace if the frame is kitted or welding in a new brace if it is a welded frame.  ";
                strVar += "Damage to bracing does not necessarily warrant entire frame replacement. ";
                strVar += "</p> ";
                strVar += "<h6 style='text-align:left;margin:0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Beam Damage</h6> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;padding-top:0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Similarly to frame posts and bracing, damage to horizontal beams is not acceptable and can also ";
                strVar += "affect the structural integrity of the component. Damage to beams can be observed as connector ";
                strVar += "damage, dents in the face of the beam and yielded sections of beam where the box or channel is ";
                strVar += "separated. Any type of damage to beams requires replacement of the component. ";
                strVar += "</p> ";
                strVar += "<h6 style='text-align:left;margin:0px;padding-bottom:0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Safety Recommendations</h6> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Every racking system, regardless of the manufacturer, comes equipped with essential components that are set as minimum requirements. These components include frame posts, frame bracing, frame footpads, beams - [step, box or channels], beam connectors, safety pins or bolts, and anchors, and it is essential that they are strictly adhered to. ";
                strVar += "</p> ";
                strVar += "<h2 style='text-align: left;margin:0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1D. Engineering Review</h2> ";
                strVar += "<p style='font-size: 15px;font-family: Arial, Helvetica, sans-serif;line-height: 24px;'> ";
                strVar += "Approval by an engineer [stamped/sealed] indicates that the report has been reviewed and confirms the following:  ";
                strVar += "</p> ";
                strVar += "<ul style='margin: 0;padding: 0;'> ";
                strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>The work was conducted by a trained inspector.</li> ";
                strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>The inspection was conducted using a documented process.</li> ";
                strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'> ";
                strVar += "The decisions, recommendations, or comments that were documented are reasonable, ";
                strVar += "based on the information being reviewed. The engineer will rely on the Inspector’s on-site ";
                strVar += "evaluation and accept it as being accurate where photographs do not fully capture the ";
                strVar += "severity of the damage. ";
                strVar += "</li> ";
                strVar += "</ul> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";


                strVar = strVar.Replace("i2APageNo", pageNo.ToString());
                strVar = strVar.Replace("i2BPageNo", pageNo.ToString());
                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size: 15px;line-height: 24px;'> ";
                strVar += "The actual loads being placed within the system have not been obtained or considered in this ";
                strVar += "report, except where excessive beam deflection has been noted in the deficiency report. The ";
                strVar += "report assumes that the system components have been manufactured according to adequate ";
                strVar += "engineering and manufacturing standards and as such the integrity of the construction of the ";
                strVar += "components has not been tested or verified. ";
                strVar += "</p> ";
                strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size: 15px;line-height: 24px;;margin: 10px 0px 0px 0px;'> ";
                strVar += "Where possible the manufacturer’s published capacities have been used to establish the system ";
                strVar += "capacity, modified in this report as required to reflect the current condition of the components. In lieu of the published capacities, the rack is analyzed based on the information and capacities are established. ";
                strVar += "</p> ";
                strVar += "<h2 style='text-align: left;margin: 15px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2A. Deficiency Picture References</h2> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Pictures are included in this report to provide a visual confirmation of the deficiencies/action items. In some cases, it is possible that existing damages may not have been captured due to them being at high elevation obstructed by the stored products or even in lower levels where pallets or product obstructed the view of the inspector.  ";
                //strVar += "it is possible that pictures of existing damages were not attainable due to height restrictions or ";
                //strVar += "even in lower levels where pallets or product obstructed the view of the inspector. Some pictures ";
                //strVar += "may be omitted from the report if the engineer discovers additional deficiencies or findings after ";
                //strVar += "the initial review completed by the racking inspector. ";
                strVar += "</p> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Pictures represent the condition of the racking as well as indicate the deficiencies/action items that were documented during the racking inspection. ";
                //strVar += "deficiencies/action items that were documented during the racking inspection time of review. ";
                strVar += "</p> ";
                strVar += "<h2 style='text-align: left;margin: 15px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2B. Repair or Replacement Based on Deficiencies (Material Take-Off)</h2> ";
                strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "Based on the racking layout at the time of inspection, the following deficiencies were documented and recommendations made accordingly for repair or replacement. For a detailed list of all deficiencies, comments and recommendations, please view the Racking Inspection Deficiency List. ";
                //strVar += "that are recommended for repair or replacement. For a detailed list of all deficiencies, comments ";
                //strVar += "and recommendations, please view the Racking Inspection Deficiency List. ";
                strVar += "</p> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";

                strVar = strVar.Replace("i2CPageNo", pageNo.ToString());

                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                strVar += "</div> ";
                strVar += "<h2 style='text-align: left;margin: 10px 0px 10px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2C. FACILITIES AREA</h2> ";
                strVar += "<ul style='margin: 0;padding: 0;'> ";
                foreach (var facility in iDetails.InspectionFacilitiesArea)
                {
                    strVar += "<li style='list-style: decimal;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "" + facility.FacilitiesAreaName + " <p>" + facility.FacilitiesAreaDesc + "</p> ";
                    strVar += "</li> ";
                }

                strVar += "</ul> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                strVar = strVar.Replace("i3APageNo", pageNo.ToString());
                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_Introduction_RackInspection.pdf");
                }
            }
            return null;
        }

        public ActionResult ToPdfV2ConclusionRecommendations(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 1;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

              

                List<List<Deficiency>> sets = new List<List<Deficiency>>();
                List<Deficiency> selectedDeficiency = iDetails.ListConclusionandRecommendationsViewModel;
                int iCount = 0;
                int iSrNo = 1;
                if (selectedDeficiency != null)
                {
                    for (int i = 0; i < selectedDeficiency.Count; i += 3)
                    {
                        List<Deficiency> set = new List<Deficiency>();
                        for (int j = i; j < i + 3 && j < selectedDeficiency.Count; j++)
                        {
                            set.Add(selectedDeficiency[j]);
                        }
                        sets.Add(set);
                    }

                    foreach (var mainSet in sets)
                    {
                        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                        strVar += "<tbody> ";
                        strVar += "<tr> ";
                        strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                        strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                        if (iCount == 0)
                        {
                            strVar += "<div> ";
                            strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                            strVar += "</div> ";
                            strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>3A. CONCLUSION AND RECOMMENDATIONS</h2> ";
                        }
                        iCount += 1;

                        strVar += "<ul style='margin: 10px 0px 0px 0px;padding: 0;'> ";
                        foreach (var deficiency in mainSet)
                        {
                            strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                            strVar += iSrNo.ToString() + ". " + deficiency.DeficiencyInfo + ". <p>" + deficiency.DeficiencyDescription + "</p> ";
                            strVar += "</li> ";
                            iSrNo += 1;
                        }
                        strVar += "</ul> ";
                        strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                        strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                        strVar += "<div style='clear: both'></div> ";
                        strVar += "</div> ";
                        strVar += "</div> ";
                        strVar += "</td> ";
                        strVar += "</tr> ";
                        strVar += "</tbody> ";
                        strVar += "</table> ";
                        //strVar += "</section> ";
                    }
                }

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                if (iCount == 0)
                {
                    strVar += "<div> ";
                    strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div> ";
                    strVar += "</div> ";
                    strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>3A. CONCLUSION AND RECOMMENDATIONS</h2> ";
                }
                strVar += "<ul style='margin: 0px 0px 25px 0px; padding: 0; '> ";
                strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<p>" + iSrNo.ToString() + ". In certain cases, it is possible that existing damages were not visible due to obstacles that may have obstructed the view of the inspector. We recommend keeping the aisle spacing as clear as possible. According to CSA A344-24: 8.1.4 “The inspection should also make note of poor operating practices such as:</p> ";
                strVar += "    <ul> ";
                strVar += "        <li style='list-style: none;'> ";
                strVar += "            <p><span style='font-weight:bold;'>h)</span> Housekeeping items such as shrink wrap and debris on the floor.</p> ";
                strVar += "            <p><span style='font-weight:bold;'>i)</span> Encroachment of clearance.</p> ";
                strVar += "            <p><span style='font-weight:bold;'>j)</span> Pallets encroaching into the clearance required for sprinkler deflectors.</p> ";
                strVar += "        </li> ";
                strVar += "    </ul> ";
                strVar += "</li> ";

                iSrNo += 1;

                strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<p>" + iSrNo.ToString() + ". We recommend a few  " + iDetails.Customer + " employees to be trained to perform routine internal inspections.  According to CSA A344-24: 8.1.6 “The frequency of both routine and expert inspections should be determined by a risk assessment done by a health and safety professional, the rack vendor, or engineering consultant specialized in rack inspection. In general, the routine inspections should be conducted monthly, and the expert inspections performed annually. The frequency should, as a minimum, ensure compliance with local regulations. Note: The frequency of inspections can change over time depending on the outcome and findings of successive inspections. </p> ";
                strVar += "<p>The risk assessment should consider the following items when establishing frequency:</p> ";
                strVar += "<p> a) nature of the environment in which the pallet rack is situated.</p> ";
                strVar += "<p> b) prior incidence of damage.</p> ";
                strVar += "<p> c) vulnerability to damage and failure due to damage.</p> ";
                strVar += "<p> d) nature of the operation including equipment used around the racks.</p> ";
                strVar += "<p> e) competency and training of the lift truck operators.</p> ";
                strVar += "<p> f) size of the facility;”</p> ";
                strVar += "</li> ";
                strVar += "</ul> ";
                strVar += "<div style='margin: 0px 0px 30px 0px; padding: 0; '> ";
                strVar += "<p>The recommendations and corrective actions provided are based on the inspector’s previous experience, incorporating several factors, and therefore have a subjective component. Cam Industrial Supply understands that users of rack have varying tolerance levels with respect to damages and may differ or disagree with the inspector’s findings.</p> ";
                strVar += "</div> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
              


                //byte[] output;

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_2C_ConclusionRecommendations.pdf");
                }
            }
            return null;
        }

        public ActionResult ToPdfV2EngineeringNotes(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 1;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

               

                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;' /></div> ";
                strVar += "</div> ";

                strVar += "<div> ";
                strVar += "<div style='text-align: center;'> ";
                strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px;margin-bottom:10px;'>" + iDetails.Customer + " - " + iDetails.CustomerLocation + "</div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<div> ";
                strVar += "</div> ";

                strVar += "<div> ";
                strVar += "<div style='text-align: center;'> ";
                //strVar += "<h2 style='text-align: center;margin: 20px 0px 10px 0px;font-size: 28px;text-transform: none;display: inline-block;font-family: Arial, Helvetica, sans-serif;'>" + iDetails.Customer + "</h2> ";
                strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px'>Engineering Notes: " + iDetails.InspectionDocumentNo + "</div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size:15px'> ";
                strVar += "These reports were prepared in accordance with the recommendations outlined in the standards below. A copy of the reports is attached for your reference. ";
                strVar += "</p> ";
                strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 0; color: #000; font-size: 12px; font-family: Arial, Helvetica, sans-serif; line-height: normal; background: #e0e0e0; border-width:1px;'> ";
                if (iDetails.InspectionDocumentTitle != null)
                {
                    foreach (var t in iDetails.InspectionDocumentTitle)
                    {
                        strVar += "<tr> ";
                        strVar += "<td align='center' style='padding:10px 10px 10px 10px; border-width: 1px; background-color: #ADD8E6;'>" + t.DocumentTitle1 + "</td> ";
                        strVar += "<td bgcolor='#ffffff' style='padding:2px 2px 2px 5px; border-width: 1px; '>" + t.DocumentDescription + "</td> ";
                        strVar += "</tr> ";
                    }
                }
                strVar += "</table> ";
                strVar += "<p>Our inspection revealed structural and nonstructural deficiencies mentioned in the section 3A Conclusion and Recommendations and corrective actions are at the owner’s discretion.</p> ";

                strVar += "<p style='line-height:1.0;'>In the report to follow, you will find:</p> ";
                strVar += "<ul style='margin: 0;padding: 0;'> ";
                strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                strVar += "        <p>A detailed list of the deficiencies in the racking system</p> ";
                strVar += "    </li> ";
                strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                strVar += "        <p>Photos of the deficiencies</p> ";
                strVar += "    </li> ";
                strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                strVar += "        <p>Material needed to remedy the deficiencies</p> ";
                strVar += "    </li> ";
                if (iDetails.CapacityTable == 1)
                {
                    strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                    strVar += "        <p>Capacity table of the racking system</p> ";
                    strVar += "    </li> ";
                }
                if (iDetails.PlanElevationDrawing == 1)
                {
                    strVar += "    <li style='list-style: decimal;padding-left: 0px; margin-left: 17px;'> ";
                    strVar += "        <p>Plan and elevation drawing of the racking system.</p> ";
                    strVar += "    </li> ";
                }
                strVar += "</ul> ";
                //strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:15px'> ";
                //strVar += iDetails.empModel.EmployeeName + "," + iDetails.empModel.TitleDegrees;
                //strVar += "</div> ";
                //strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:15px'> ";
                //strVar += "Cam Industrial ";
                //strVar += "</div> ";
                strVar += "<div class='containerEngineerNotes' style='display: flex;justify-content: space-between;align-items: center;margin-bottom: 20px;'> ";
                strVar += "<div class='sectionEngineerNotes' style='flex-basis: 55%;font-family: Arial, Helvetica, sans-serif;font-size: 15px;'> ";
                strVar += "    <p>Yours truly,</p> ";
                strVar += "    <p>Inspection & Report By</p> ";
                strVar += "    <div>" + iDetails.empModel.EmployeeName + "," + iDetails.empModel.TitleDegrees + "</div> ";
                strVar += "    <div>Cam Industrial</div> ";
                //strVar += "    <div class='logoEngineerNotes'> ";
                //strVar += "        <img src='" + host + "/Content/V2/images/logos/CamLogo.png' /> ";
                //strVar += "    </div> ";
                strVar += "</div> ";
                strVar += "<div class='sectionEngineerNotes' style='flex-basis: 45%;font-family: Arial, Helvetica, sans-serif;font-size: 15px;'> ";
                if (string.IsNullOrEmpty(iDetails.empStampingEngModel.EmployeeName) != true)
                {
                    strVar += "    <p>&nbsp;</p> ";
                    strVar += "    <p>Reviewed By</p> ";
                    strVar += "    <div>" + iDetails.empStampingEngModel.EmployeeName + "," + iDetails.empStampingEngModel.TitleDegrees + "</div> ";
                    strVar += "    <div>Cam Industrial</div> ";
                }
                //strVar += "    <div class='logoEngineerNotes'> ";
                //strVar += "        <img src='" + host + "/Content/V2/images/logos/CamLogo.png' /> ";
                //strVar += "    </div> ";
                strVar += "</div> ";
                strVar += "</div> ";

                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
              

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_EngineeringNotes.pdf");
                }
            }
            return null;
        }

        public ActionResult ToPdfV2DocumentAppendix(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 1;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

               



                //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;position: relative;'> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;' /></div> ";
                strVar += "</div> ";
                strVar += "<div> ";
                strVar += "<div style='text-align: center;'> ";
                strVar += "<h2 style='text-align: center;margin: 20px 0px 10px 0px;font-size: 28px;text-transform: none;display: inline-block;'font-family: Arial, Helvetica, sans-serif;'>" + iDetails.Customer + "</h2> ";
                strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px;text-align: center;'>Document Appendix</div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 20px 0px 0px 0px; color: #000; font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: normal; background: #e0e0e0; border-width:1px;'> ";
                strVar += "<tr> ";
                strVar += "<td align='center' style='padding: 20px; border-width: 1px; background-color: #ADD8E6;'> ";
                strVar += "Document Title ";
                strVar += "</td> ";
                strVar += "<td align='center' style='padding: 20px; border-width: 1px; background-color: #ADD8E6;'> ";
                strVar += "Document Number ";
                strVar += "</td> ";
                strVar += "</tr> ";
                foreach (var k in iDetails.InspectionFacilitiesArea)
                {
                    //Facilities Area
                    if (k.FacilitiesAreaId == 2 || k.FacilitiesAreaId == 3)
                    {
                        strVar += "<tr> ";
                        strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                        strVar += "" + k.FacilitiesAreaName + " ";
                        strVar += "</td> ";

                        strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '> ";
                        strVar += "" + iDetails.InspectionDocumentNo + " ";
                        strVar += "</td> ";
                        strVar += "</tr> ";
                    }
                }

               
                foreach (var p in iDetails.ListInspectionFileDrawing)
                {
                    List<List<InspectionFileDrawingChildViewModel>> set1 = new List<List<InspectionFileDrawingChildViewModel>>();
                    List<InspectionFileDrawingChildViewModel> selectedFile = p.inspectionFileDrawingChildViewModels;
                    int iCnt = 0;
                    int iSerNo = 1;
                    if (selectedFile != null)
                    {
                        for (int i = 0; i < selectedFile.Count; i += 4)
                        {
                            List<InspectionFileDrawingChildViewModel> st = new List<InspectionFileDrawingChildViewModel>();
                            for (int j = i; j < i + 4 && j < selectedFile.Count; j++)
                            {
                                st.Add(selectedFile[j]);
                            }
                            set1.Add(st);
                        }
                    }
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                    strVar += "" + p.FileCategory + " ";
                    strVar += "</td> ";
                    strVar += "<td bgcolor='#ffffff' style='padding: 2px;border-width: 1px;'> ";
                    strVar += "<table style='width: 100 %;'> ";
                    foreach (var m in p.inspectionFileDrawingChildViewModels)
                    {
                        strVar += "<tr> ";
                        strVar += "<td bgcolor='#ffffff' style='padding:2px;'><a href = '" + m.FileDrawingPath + "' target = '_blank'>" + m.FileDrawingName + "</a></td> ";
                        strVar += "</tr> ";
                    }
                    strVar += "</table> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                }
                if (iDetails.objQuotation != null)
                {
                    if (iDetails.objQuotation.QuotationNo != null)
                    {
                        strVar += "<tr> ";
                        strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '> ";
                        strVar += "Quotation";
                        strVar += "</td> ";

                        strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '> ";
                        strVar += iDetails.objQuotation.QuotationNo;
                        strVar += "</td> ";
                        strVar += "</tr> ";
                    }
                }
                strVar += "</table> ";
                strVar += " <div style='margin: 0px 0px 18px 0px; position: absolute; bottom: 0px; width: 95%;'> ";
                strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div> ";
                strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div> ";
                strVar += "<div style='float: right;width: 5%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div> ";
                strVar += "<div style='clear: both'></div> ";
                strVar += "</div> ";
                strVar += "</div> ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //strVar += "</section> ";
                //strVar += "<div style='page-break-after: auto;'></div> ";

                strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                strVar += "<tbody> ";
                strVar += "<tr> ";
                strVar += "<td class='' style='padding: 2px;'> ";
                strVar += " ";
                strVar += "</td> ";
                strVar += "</tr> ";
                strVar += "</tbody> ";
                strVar += "</table> ";
                //Racking Inspection Deficiency List

               

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_DocumentAppendix.pdf");
                }
            }
            return null;
        }

        public ActionResult ToPdfV2DeficiencyList(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 1;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

                //Racking Inspection Deficiency List

                List<List<InspectionDeficiencyViewModel>> setsD = new List<List<InspectionDeficiencyViewModel>>();
                List<InspectionDeficiencyViewModel> selectedDeficiencyList = iDetails.iDefModel;

                setsD = GenerateParentSets(selectedDeficiencyList);
                setsD = UpdatePreviousListWithNextListValues(setsD);
                foreach (var mainSet in setsD)
                {
                    Random rnd = new Random();
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    //strVar += "<section style='width:19cm;height:29.7cm;background:rgba(" + randomColor.R + "," + randomColor.G + "," + randomColor.B + "," + randomColor.A + ");box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";


                    strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tbody> ";
                    strVar += "<tr> ";
                    strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                    //strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";
                    strVar += "<table width='100%' border='1' align='left' cellpadding='0' cellspacing='0'  style='border-collapse: collapse;color:#000000;font-size:11px'> ";
                    strVar += "<tbody > ";
                    strVar += "<tr > ";
                    strVar += "<td  align='center' valign='middle' class=''> ";
                    strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='border-collapse: collapse;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tr valign='middle' > ";
                    strVar += "<td colspan='6' style='font-size:14px;font-weight:600;text-align:center;padding:2px;background:#79addd;font-family: Arial, Helvetica, sans-serif;'>RACKING INSPECTION DEFICIENCY LIST</td> ";
                    strVar += "<td rowspan='4' style='width:150px;' valign='middle' align='center'><table border='0' ><tr><td style='font-size:14px;font-weight:600;text-align:center;padding:2px;'>&nbsp;</td></tr><tr><td style='font-size:9px;font-weight:600;text-align:center;padding:2px;'>&nbsp;</td></tr><tr><td style='valign:middle !important;' align='center'><img src='" + host + "Content/V2/images/table-logo.png' style='width:80%;padding:2px;' /></td></tr><tr><td></td></tr></table></td> ";
                    //strVar += "<td rowspan='4' style='width:150px; height:150px; text-align:center; vertical-align:middle;'><img src='" + host + "Content/V2/images/table-logo.png' style='max-width:100%; max-height:100%; padding:2px;'></td> ";

                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Client:</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.Customer + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Document Number:</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.InspectionDocumentNo + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Type of Racking:</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.InspectionType + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Location/ Address</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + CustomerFullAddress + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Date of Inspection</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Total Inspection Deficiencies</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.iDefModel.Count() + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Contact</td> ";
                    if (iDetails.ListCustomerLocationContacts != null)
                    {
                        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.ListCustomerLocationContacts[0].ContactName + "</td> ";
                    }
                    else
                    {
                        strVar += "<td style='padding:2px;font-size:9px;'></td> ";
                    }

                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Inspected By</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.Employee + "</td> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Action Required</td> ";
                    strVar += "<td style='padding:2px;font-size:9px;'>Yes</td> ";
                    strVar += "</tr> ";
                    strVar += "</table> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr > ";
                    strVar += "<td  align='center' valign='top' class=''> ";
                    strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='font-family: Arial, Helvetica, sans-serif;font-size:9px;'> ";
                    strVar += "<tr> ";
                    strVar += "<td colspan='5' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Racking Classification</td> ";
                    strVar += "<td colspan='2' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Deficiency Type</td> ";
                    strVar += "<td colspan='3' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Damage Assessment</td> ";
                    strVar += "<td colspan='2' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Racking Repair</td> ";
                    strVar += "<td colspan='1' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Engineer Approval</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    //strVar += "<td colspan='5' bgcolor='#0dffff' style='text-align:center;padding:2px;;font-size:9px;'>Location</td> ";
                    //strVar += "<td bgcolor='#ffc820' style='text-align:center;padding:2px;;font-size:9px;'>Category</td> ";
                    //strVar += "<td bgcolor='#ffc820' style='text-align:center;padding:2px;;font-size:9px;'>Description</td> ";
                    //strVar += "<td colspan='3' bgcolor='#ff6160' style='text-align:center;padding:2px;;font-size:9px;'>Action Required</td> ";
                    //strVar += "<td colspan='2' bgcolor='#0dffff' style='text-align:center;padding:2px;font-size:9px;'>Action Taken</td> ";
                    //strVar += "<td colspan='1' bgcolor='#0dffff' style='text-align:center;padding:2px;font-size:9px;'>Status</td> ";
                    strVar += "<td colspan='5' bgcolor='#0dffff' style='text-align:center;padding:2px;border-width: 1px;font-size:9px;'>Location</td>";
                    strVar += "<td bgcolor='#0ABFFF' style='text-align:center;padding:2px;border-width:1px;color:#000000;font-size:9px;'>Category</td>";
                    strVar += "<td bgcolor='#0080FF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Description</td>";
                    strVar += "<td colspan='3' bgcolor='#0055FF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Conclusion</td>";
                    strVar += "<td colspan='2' bgcolor='#002AFF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Action Taken</td>";
                    strVar += "<td colspan='1' bgcolor='#0000FF' style='text-align:center;padding:2px;border-width:1px;color:#FFFFFF;font-size:9px;'>Acceptance</td>";
                    strVar += "</tr> ";
                    strVar += "<tr bgcolor='#ffffcc'> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Item ID</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Row Number</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Bay ID/ Number</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Bay/ Frame Side</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Beam/ Frame Level</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>Title</td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>Title</td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Refer report for more detail</div></td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Monitor</div></td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Replace Component</div></td> ";
                    //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Repair Component</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Proposed Action</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Severity Index Number</div></td> ";
                    strVar += "<td valign='middle' style='text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Reference Images</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Status</div></td> ";
                    strVar += "<td valign='middle' style='text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Repair Images</div></td> ";
                    strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:9px'>Yes/No</div></td> ";
                    strVar += "</tr> ";
                    strVar += "<tbody> ";
                    foreach (var d in mainSet)
                    {
                        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm -10mm;box-sizing:border-box;'> ";
                        if (d.RowNo == 0)
                        {
                            int i = 0;
                            i = 10;
                        }

                        strVar += "<tr style='border-width:1px;'><i class='fa-solid fa-xmark'></i> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.RowNo + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.CustomerNomenclatureNo + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.CustomerNomenclatureBayNoID + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.BayFrameSide + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.BeamFrameLevel + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.DeficiencyType + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.DeficiencyInfo + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.ActionTaken + "</td> ";
                        //if (d.Action_ReferReport == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'/></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'/></td> "; }
                        //if (d.Action_Monitor == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'/></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td> "; }
                        //if (d.Action_Replace == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td> "; }
                        //if (d.Action_Repair == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td> "; }
                        if (d.Severity_IndexNo >= 1 && d.Severity_IndexNo <= 3)
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#00CC00;font-size:9px;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        if (d.Severity_IndexNo >= 4 && d.Severity_IndexNo <= 7)
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#FFFF00;font-size:9px;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        if (d.Severity_IndexNo >= 8 && d.Severity_IndexNo <= 10)
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#FF0000;font-size:9px;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        strVar += "<td valign='middle' style='width:65px;text-align:left;padding:2px;font-size:9px;'> ";
                        //strVar += "<ul style='list-style: none;'> ";

                        if (d.InspectionDeficiencyPhotoViewModel != null)
                        {

                            strVar += "<table> ";
                            //strVar += "<ul style='display: flex;flex-direction: column;padding-left: 0;margin-bottom: 0;list-style: none;'> ";
                            foreach (var photo in d.InspectionDeficiencyPhotoViewModel)
                            {
                                ////strVar += "<li style='text-align:left;background:#FF0000;> ";
                                strVar += "<tr><td> ";
                                ////strVar += "<p> ";
                                if (d.InspectionDeficiencyPhotoViewModel.Count >= 4)
                                {
                                    strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhotoThumb + "' style='width:64px!important;height:64px!important;' alt=''/></a> ";
                                }
                                else
                                {
                                    strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhotoThumb + "' style='width:64px!important;height:64px!important;' alt=''/></a> ";
                                }
                                strVar += "</td></tr> ";
                            }
                            if (d.InspectionDeficiencyPhotoViewModel.Count == 0)
                            {
                                strVar += "<tr><td> ";
                                strVar += "";
                                strVar += "</td></tr> ";
                            }
                            strVar += "</table> ";
                        }
                        //strVar += "</ul> ";
                        strVar += "</td> ";
                        if (d.InspectionDeficiencyAdminStatus == 0 && d.InspectionDeficiencyTechnicianStatus == 0) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Not Repaired & Not approved</td> "; }
                        else if (d.InspectionDeficiencyAdminStatus == 0 && d.InspectionDeficiencyTechnicianStatus == 1) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Repaired & Not approved</td> "; }
                        else if (d.InspectionDeficiencyAdminStatus == 1 && d.InspectionDeficiencyTechnicianStatus == 0) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Not Repaired & Approved</td> "; }
                        else if (d.InspectionDeficiencyAdminStatus == 1 && d.InspectionDeficiencyTechnicianStatus == 1) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'>Repaired & Approved</td> "; }
                        else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'></td> "; }
                        //strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.InspectionDeficiencyTechnicianStatusText + "</td> ";
                        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'> ";
                        //strVar += "<ul style='list-style: none;'> ";
                        if (d.InspectionDeficiencyPhotoTechnicianViewModel != null)
                        {
                            strVar += "<table> ";
                            foreach (var photo in d.InspectionDeficiencyPhotoTechnicianViewModel)
                            {
                                strVar += "<tr><td> ";
                                strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhotoThumb + "' style='width:64px!important;height:64px!important;' alt=''></a> ";
                                strVar += "</td></tr> ";
                            }
                            if (d.InspectionDeficiencyPhotoTechnicianViewModel.Count == 0)
                            {
                                strVar += "<tr><td> ";
                                strVar += "";
                                strVar += "</td></tr> ";
                            }
                            strVar += "</table> ";
                        }
                        strVar += "</td> ";
                        if (d.InspectionDeficiencyAdminStatus == 1) //&& d.InspectionDeficiencyTechnicianStatus == 1
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>Yes</td> ";
                        }
                        else
                        {
                            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>No</td> ";
                        }
                        //if (d.InspectionDeficiencyAdminStatus == 1) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/active-icon-green.png' style='width:12px;'></td> "; }
                        //else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'></td> "; }
                        strVar += "</tr> ";
                    }
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "</tbody> ";
                    strVar += "</table> ";

                    //strVar += "</div> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    //strVar += "</section> ";
                    //strVar += "<div style='page-break-after: auto;'></div> ";
                    //iDSrNo += 1;                    
                }

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_DeficiencyList.pdf");
                }
            }
            return null;
        }

        public ActionResult ToPdfV2MaterialTakeOffList(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";
                int pageNo = 1;
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);

                
                List<List<InspectionDeficiencyViewModel>> setsD = new List<List<InspectionDeficiencyViewModel>>();
                List<InspectionDeficiencyViewModel> selectedDeficiencyList = iDetails.iDefModel;

                setsD = GenerateParentSets(selectedDeficiencyList);
                setsD = UpdatePreviousListWithNextListValues(setsD);
                //Material Take-off List

                List<List<InspectionDeficiencyMTOViewModel>> setsDMTO = new List<List<InspectionDeficiencyMTOViewModel>>();
                List<InspectionDeficiencyMTOViewModel> selectedDeficiencyListMTO = iDetails.iMTOModel;
                //int iMCount = 0;
                //int iMSrNo = 1;
                if (selectedDeficiencyListMTO != null)
                {

                    for (int i = 0; i < selectedDeficiencyListMTO.Count; i += 8)
                    {
                        List<InspectionDeficiencyMTOViewModel> set = new List<InspectionDeficiencyMTOViewModel>();
                        for (int j = i; j < i + 8 && j < selectedDeficiencyList.Count; j++)
                        {
                            set.Add(selectedDeficiencyListMTO[j]);
                        }
                        setsDMTO.Add(set);
                    }
                }
                //iMCount += 1;

                foreach (var mainSet in setsDMTO)
                {
                    Random rnd = new Random();
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    //strVar += "<section style='width:19cm;height:29.7cm;background:rgba(" + randomColor.R + "," + randomColor.G + "," + randomColor.B + "," + randomColor.A + ");box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'> ";
                    strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0'style='width:18.5cm;height:29.7cm;background:white;margin:0mm 0mm 0mm 0mm;padding:0mm 0mm 0mm 0mm;box-sizing:border-box;border:5px solid #0070c0;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tbody> ";
                    strVar += "<tr> ";
                    strVar += "<td valign='top' class='' style='padding: 2px;'> ";
                    //strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;height:25.6cm;'> ";

                    strVar += "<table width='100%' border='1' align='left' cellpadding='0' cellspacing='0'  style='border-collapse: collapse;color:#000000;font-size:11px;font-family: Arial, Helvetica, sans-serif;'> ";
                    strVar += "<tr> ";
                    strVar += "<td height='30' colspan='9' align='center' bgcolor='#80b1de' style='font-family: Arial, Helvetica, sans-serif;'>RACKING INSPECTION - MATERIAL TAKE OFF LIST</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td width='100' bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Client:</td> ";
                    strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + iDetails.Customer + "</td> ";
                    strVar += "<td width='100' align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Type of Racking:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + iDetails.InspectionType + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Location & Address:</td> ";
                    strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + CustomerFullAddress + "</td> ";
                    strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Date of Inspection:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Contact:</td> ";
                    if (iDetails.ListCustomerLocationContacts != null)
                    {
                        strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + iDetails.ListCustomerLocationContacts[0].ContactName + "</td> ";
                    }
                    else
                    {
                        strVar += "<td colspan='4' style='padding:2px;font-size:9px;'></td> ";
                    }
                    strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Inspection By:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + iDetails.Employee + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px;font-size:9px;'>Project Number:</td> ";
                    strVar += "<td colspan='4' style='padding:2px;font-size:9px;'>" + iDetails.InspectionDocumentNo + "</td> ";
                    strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px;font-size:9px;'>Report/ BOM By:</td> ";
                    strVar += "<td colspan='3' style='padding:2px 10px;font-size:9px;'>" + iDetails.Employee + "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td colspan='9' align='center' valign='middle'> ";
                    strVar += "<div style='float:left;padding-left:30px;'><img src='" + host + "/Content/V2/images/table-logo.png' style='width:120px;margin-top:5px;'></div> ";
                    strVar += "<div style='float:right;padding-right:30px;'><img src='" + host + "/Content/V2/images/footer-logo.jpg' style='width:250px;margin:10px 0px;'></div> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "<tr> ";
                    strVar += "<td align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Severity<br />Index</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Action Item Reference</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Component</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Manufacturer</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Vendor ID</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Type</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>CAM ID</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Size/ Description</td> ";
                    strVar += "<td height='30' align='center' bgcolor='#dbe5f1' style='font-size:9px;'>Quantity<br />Required</td> ";
                    strVar += "</tr> ";
                    strVar += "<tbody> ";
                    foreach (var d in mainSet)
                    {
                        //strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm -10mm;box-sizing:border-box;'> ";
                        InspectionDeficiencyMTODetailViewModel objMTODetails = new InspectionDeficiencyMTODetailViewModel();
                        var iMTOdetails = db.InspectionDeficiencyMTODetails.Where(h => h.InspectionDeficiencyMTOId == d.InspectionDeficiencyMTOId).ToList();
                        var fType = " ";
                        if (iMTOdetails.Count != 0)
                        {
                            foreach (var mtoDetail in iMTOdetails)
                            {
                                if (mtoDetail.ComponentPropertyTypeId != 0)
                                {
                                    var type = DatabaseHelper.getComponentPropertyTypeById(mtoDetail.ComponentPropertyTypeId);
                                    if (type != null)
                                    {
                                        if (type.ComponentPropertyTypeName.Contains("Type"))
                                        {
                                            var value = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == mtoDetail.ComponentPropertyValueId && x.ComponentPropertyTypeId == mtoDetail.ComponentPropertyTypeId).ToList();
                                            if (value.Count != 0)
                                            {
                                                foreach (var v in value)
                                                {
                                                    fType += v.ComponentPropertyValue1 + ", ";
                                                    fType = fType.Trim();
                                                    fType = fType.Remove(fType.Length - 1);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        if (fType.Contains(","))
                        {
                            fType = fType.Replace(",", ", ");
                        }
                        strVar += "<tr style='border-width:1px;'> ";
                        if (d.Severity_IndexNo >= 1 && d.Severity_IndexNo <= 3)
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#00CC00;color:#000000;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        else if (d.Severity_IndexNo >= 4 && d.Severity_IndexNo <= 7)
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#FFFF00;color:#000000;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        else if (d.Severity_IndexNo >= 8 && d.Severity_IndexNo <= 10)
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#FF0000;color:#000000;'>" + d.Severity_IndexNo + "</td> ";
                        }
                        else
                        {
                            strVar += "<td valign='middle' style='width:1rem;text-align:center;'></td> ";
                        }
                        strVar += "<td align='center' bgcolor='#d9e1f2' style='padding:2px'>" + d.DeficiencyRowNo + "</td> ";
                        strVar += "<td style='padding:2px 8px'>" + d.ComponentName + "</td> ";
                        strVar += "<td style='padding:2px 8px'>" + d.ManufacturerName + "</td> ";
                        strVar += "<td style='padding:2px 8px'></td> ";
                        strVar += "<td style='padding:2px 8px'>" + fType.ToString() + "</td> ";
                        strVar += "<td style='padding:2px 8px'></td> ";
                        strVar += "<td style='padding:2px 8px; font-size: 10px;'>" + d.Size_Description + "</td> ";
                        strVar += "<td style='padding:2px 8px'>" + d.QuantityReq + "</td> ";
                        strVar += "</tr> ";
                    }
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    //strVar += "</div> ";
                    strVar += "</td> ";
                    strVar += "</tr> ";
                    strVar += "</tbody> ";
                    strVar += "</table> ";
                    //strVar += "</section> ";
                }             
                //byte[] output;

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_MaterialTakeOffList.pdf");
                }
            }
            return null;
        }

        public ActionResult ToPdfV2RepairQuotation(int id)
        {

            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
            if (iDetails != null)
            {
                string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                //host = host.Replace("https", "http");
                string strVar = " ";                
                string CustomerFullAddress = " ";
                List<string> FullAddress = new List<string>();
                //Index Page                
                if (iDetails.CustomerArea != null)
                {
                    FullAddress.Add(iDetails.CustomerArea);
                }
                if (iDetails.CustomerLocation != null)
                {
                    FullAddress.Add(iDetails.CustomerLocation);
                }
                if (iDetails.custModel.CustomerAddress != null)
                {
                    FullAddress.Add(iDetails.custModel.CustomerAddress);
                }
                CustomerFullAddress = string.Join(",", FullAddress);
                strVar = "";
                if (iDetails.objQuotation != null)
                {
                    if (iDetails.objQuotation.QuotationNo != null && iDetails.objQuotation.QuotationStatus > 5)
                    {
                        List<List<QuotationItem>> objQuotationItemsInner = new List<List<QuotationItem>>();
                        if (iDetails.objQuotation.objQuotationItems.Count > 0)
                        {
                            objQuotationItemsInner = SplitList(iDetails.objQuotation.objQuotationItems, 10);
                        }
                        int i = 1;
                        foreach (var itemSet in objQuotationItemsInner)
                        {
                            strVar += "<table style='width:18.5cm; height:29.7cm; background:white; margin:2mm auto; padding:0; box-sizing:border-box;font-family: Arial, Helvetica, sans-serif;' border='0' align='center' cellpadding='0' cellspacing='0'>";
                            strVar += "    <thead>";
                            strVar += "        <tr class='header'>";
                            strVar += "            <td style='text-align: center;'>";
                            strVar += "                <img src='" + host + "Content/V2/images/quoteheader.png' style='width: 100%; max-width: 100%; height: auto;'>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "    </thead>";
                            strVar += "    <tbody>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 10px 20px;font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                <div style='font-size: 16px; font-weight: bold;font-family: Arial, Helvetica, sans-serif;'>Sales Quote " + iDetails.objQuotation.QuotationNo + " </div>";
                            strVar += "                <div style='font-size: 16px; line-height: 22px; padding-top: 5px;font-family: Arial, Helvetica, sans-serif;'>" + Convert.ToDateTime(iDetails.objQuotation.QuotationDate).ToString("dd MMM yyyy") + "<br>Page " + Convert.ToString(i) + "/" + Convert.ToString(objQuotationItemsInner.Count) + "</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 10px 20px;font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                <div style='width: 40%; float: left; line-height: 22px;font-size: 16px;'>" + iDetails.Customer + "<br>" + iDetails.CustomerArea + "<br>" + iDetails.CustomerLocation + "</div>";
                            strVar += "                <div style='width: 40%; float: left; line-height: 22px;font-size: 16px;'><b>Ship to:</b><br>" + iDetails.Customer + "<br>" + iDetails.custModel.CustomerAddress + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: right;font-size: 16px;'></div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 5px 5px;;font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                <div style='width: 20%; float: left; line-height: 22px;font-size: 16px;'><b>Your Reference</b><br>" + iDetails.objQuotation.YourReference + "</div>";
                            strVar += "                <div style='width: 20%; float: left; line-height: 22px;font-size: 16px;'><b>Valid to</b><br>" + iDetails.objQuotation.ValidTo + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: left;font-size: 16px;'><b>Salesperson</b><br>" + iDetails.objQuotation.QuotationSalesPersonName + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: left;font-size: 16px;'><b>Payment Terms</b><br>" + iDetails.objQuotation.PaymentTerms + "</div>";
                            strVar += "                <div style='width: 20%; float: left; text-align: left;font-size: 16px;'><b>Shipment Method</b><br> " + iDetails.objQuotation.ShipmentMethod + "</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 5px 5px;'>";
                            strVar += "                <div style='font-size: 16px;font-weight: bold;'>Rack Inspection Repair - " + Convert.ToDateTime(iDetails.InspectionDate).ToString("yyyy") + "</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td style='padding: 10px 10px;vertical-align: top;'>";
                            strVar += "                <table border='0' cellpadding='0' cellspacing='0' class='amount' width='100%' style='font-family: Arial, Helvetica, sans-serif;'>";
                            strVar += "                    <thead>";
                            strVar += "                        <tr>";
                            strVar += "                            <th align='left' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Item No.</th>";
                            strVar += "                            <th align='left' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:20%'>Description</th>";
                            strVar += "                            <th align='right' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Item Price($)</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Weight</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:5%'>Quantity</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Total Weight</th>";
                            strVar += "                            <th align='center' style='border-bottom: 1px solid #000; font-size: 9px; text-transform: uppercase;width:10%'>Line Amount($)</th>";
                            strVar += "                        </tr>";
                            strVar += "                    </thead>";
                            strVar += "                    <tbody>";
                            if (iDetails.objQuotation.objQuotationItems.Count > 0)
                            {
                                //foreach (var item in iDetails.objQuotation.objQuotationItems)
                                //{
                                //    strVar += "<tr>";
                                //    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPartNo + "</td>";
                                //    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemDescription + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPrice + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeight + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemQuantity + "</td>";
                                //    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeightTotal + "</td>";
                                //    strVar += "<td align='right' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.LineTotal + "</td>";
                                //    strVar += "</tr>";
                                //}
                                foreach (var item in itemSet)
                                {
                                    strVar += "<tr>";
                                    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPartNo + "</td>";
                                    strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemDescription + "</td>";
                                    if (item.IsTBD == true)
                                    {
                                        strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>TBD</td>";
                                    }
                                    else
                                    {
                                        strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemPrice + "</td>";
                                    }

                                    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeight + "</td>";
                                    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemQuantity + "</td>";
                                    strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.ItemWeightTotal + "</td>";
                                    strVar += "<td align='right' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + item.LineTotal + "</td>";
                                    strVar += "</tr>";
                                }
                            }
                            strVar += "<tr>";
                            strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>Labour Cost</td>";
                            strVar += "<td align='left' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>"; //iDetails.objQuotation.TotalLabour
                            strVar += "<td align='center' style='font-size: 9px; padding: 5px 0; line-height: 10px;'></td>";
                            strVar += "<td align='right' style='font-size: 9px; padding: 5px 0; line-height: 10px;'>" + iDetails.objQuotation.TotalUnitPrice + "</td>";
                            strVar += "</tr>";
                            //strVar += "                        <tr>";
                            //strVar += "                            <td style='font-size: 16px; padding: 10px 0;'>68982</td>";
                            //strVar += "                            <td align='left' style='font-size: 16px; padding: 10px 0; line-height: 26px;'>ANCHOR BOLT - WEDGE 0.5' X 4.5' KB1, EACH</td>";
                            //strVar += "                            <td align='center' style='font-size: 16px; padding: 10px 0;'>60 Each</td>";
                            //strVar += "                            <td align='center' style='font-size: 16px; padding: 10px 0;'>2.40</td>";
                            //strVar += "                            <td align='center' style='font-size: 16px; padding: 10px 0;'>144.00</td>";
                            //strVar += "                        </tr>";
                            strVar += "                    </tbody>";
                            if (i == objQuotationItemsInner.Count)
                            {
                                strVar += "                    <tfoot >";
                                strVar += "                        <tr><td colspan='7' style='border-top:1px solid #000; font-size: 9px; text-transform: uppercase;'></td></tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4' rowspan='4' style='font-size: 9px; text-transform: uppercase;'>" + iDetails.objQuotation.QuotationNotes.Replace("\n", "<br/>") + "</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                //strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size: 12px;padding:5px 0;' align='center'><b>Subtotal</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size: 12px; padding: 5px 0;'><b>$" + iDetails.objQuotation.Subtotal + "</b></td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                //strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'><b>TAX(" + iDetails.objQuotation.GSTPer + "%)</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding: 5px 0;'><b>$" + iDetails.objQuotation.GSTValue + "</b></td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                //strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'><b>Total</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding:5px 0;'><b>$" + iDetails.objQuotation.Total + "</b></td>";
                                strVar += "                        </tr>";
                                strVar += "                    </tfoot>";
                            }
                            else
                            {
                                strVar += "                    <tfoot>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='7' style='border-bottom:1px solid #000; font-size: 9px; text-transform: uppercase;padding: 20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size: 12px;padding:5px 0;' align='center'>&nbsp;</td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size: 12px; padding: 20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'>&nbsp;</td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding: 20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                        <tr>";
                                strVar += "                            <td colspan='4'>&nbsp;</td>";
                                strVar += "                            <td colspan='2' style='font-size:12px;padding:5px 0;' align='center'><b>&nbsp;</b></td>";
                                strVar += "                            <td colspan='1' align='right' style='font-size:12px; padding:20px 0;'>&nbsp;</td>";
                                strVar += "                        </tr>";
                                strVar += "                    </tfoot>";
                            }
                            strVar += "                </table>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "        <tr>";
                            strVar += "            <td align='left' valign='top' style='padding: 10px 20px;'>";
                            strVar += "                <div style='width: 25%; float: left; line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>VAT Registration No.</span><br>R100741693</div>";
                            strVar += "                <div style='width: 25%; float: left; line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>Home Page</span><br>www.camindustrial.net</div>";
                            strVar += "                <div style='width: 25%; float: left; text-align: left;line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>Phone No.</span><br>(403) 720-0076</div>";
                            strVar += "                <div style='width: 25%; float: left; text-align: left;line-height: 16px;'><span style='font-size: 14px; font-weight: 600;'>Email</span><br>info@camindustrial.net</div>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "    </tbody>";
                            strVar += "    <tfoot>";
                            strVar += "        <tr class='footer'>";
                            strVar += "            <td style='text-align: center;'>";
                            strVar += "                <img src='" + host + "Content/V2/images/quotefooter.png' style='width: 100%; max-width: 100%; height: auto;'>";
                            strVar += "            </td>";
                            strVar += "        </tr>";
                            strVar += "    </tfoot>";
                            strVar += "</table>";

                            i += 1;
                        }
                    }
                }
                //byte[] output;

                using (var workStream = new MemoryStream())
                using (var pdfWriter = new PdfWriter(workStream))
                {
                    using (var document = HtmlConverter.ConvertToDocument(strVar, pdfWriter))
                    {
                    }
                    //Returns the written-to MemoryStream containing the PDF.   
                    return File(workStream.ToArray(), "application/pdf", "" + iDetails.Customer?.Trim().Replace(" ", "_") + "_" + iDetails.InspectionDocumentNo.Trim() + "_RepairQuotation.pdf");
                }
            }
            return null;
        }
        //     if (parent.RowNo == 242)
        //                {
        //                    int i = 0;
        //}
        public static List<List<QuotationItem>> SplitList(List<QuotationItem> originalList, int chunkSize)
        {
            var splitLists = new List<List<QuotationItem>>();

            for (int i = 0; i < originalList.Count; i += chunkSize)
            {
                var chunk = new List<QuotationItem>();
                for (int j = i; j < i + chunkSize && j < originalList.Count; j++)
                {
                    chunk.Add(originalList[j]);
                }
                splitLists.Add(chunk);
            }

            return splitLists;
        }
        public static List<List<InspectionDeficiencyViewModel>> UpdatePreviousListWithNextListValues(List<List<InspectionDeficiencyViewModel>> allLists)
        {
            for (int i = 0; i < allLists.Count - 1; i++)
            {
                var currentList = allLists[i];
                var nextList = allLists[i + 1];

                // Check if both lists have elements
                if (currentList.Count > 0 && nextList.Count > 0)
                {

                    InspectionDeficiencyViewModel lastObjectCurrentList = currentList[currentList.Count - 1];
                    InspectionDeficiencyViewModel firstObjectNextList = nextList[0];

                    if (lastObjectCurrentList.RowNo == 0)
                    {
                        lastObjectCurrentList.RowNo = firstObjectNextList.RowNo;
                        lastObjectCurrentList.InspectionDeficiencyId = firstObjectNextList.InspectionDeficiencyId;
                        lastObjectCurrentList.InspectionId = firstObjectNextList.InspectionId;
                        lastObjectCurrentList.IsDelete = firstObjectNextList.IsDelete;
                        lastObjectCurrentList.CustomerNomenclatureNo = firstObjectNextList.CustomerNomenclatureNo;
                        lastObjectCurrentList.CustomerNomenclatureBayNoID = firstObjectNextList.CustomerNomenclatureBayNoID;
                        lastObjectCurrentList.BayFrameSide = firstObjectNextList.BayFrameSide;
                        lastObjectCurrentList.BeamFrameLevel = firstObjectNextList.BeamFrameLevel;
                        lastObjectCurrentList.ConclusionRecommendationsID = firstObjectNextList.ConclusionRecommendationsID;
                        lastObjectCurrentList.ConclusionRecommendationsTitle = firstObjectNextList.ConclusionRecommendationsTitle;
                        lastObjectCurrentList.DeficiencyID = firstObjectNextList.DeficiencyID;
                        lastObjectCurrentList.DeficiencyType = firstObjectNextList.DeficiencyType;
                        lastObjectCurrentList.DeficiencyInfo = firstObjectNextList.DeficiencyInfo;
                        lastObjectCurrentList.DeficiencyDesc = firstObjectNextList.DeficiencyDesc;
                        lastObjectCurrentList.Action_ReferReport = firstObjectNextList.Action_ReferReport;
                        lastObjectCurrentList.Action_Monitor = firstObjectNextList.Action_Monitor;
                        lastObjectCurrentList.Action_Replace = firstObjectNextList.Action_Replace;
                        lastObjectCurrentList.Action_Repair = firstObjectNextList.Action_Repair;
                        lastObjectCurrentList.Severity_IndexNo = firstObjectNextList.Severity_IndexNo;
                        lastObjectCurrentList.ActionTaken = firstObjectNextList.ActionTaken;
                        lastObjectCurrentList.InspectionDeficiencyTechnicianStatus = firstObjectNextList.InspectionDeficiencyTechnicianStatus;
                        lastObjectCurrentList.InspectionDeficiencyTechnicianRemark = firstObjectNextList.InspectionDeficiencyTechnicianRemark;
                        lastObjectCurrentList.InspectionDeficiencyAdminStatus = firstObjectNextList.InspectionDeficiencyAdminStatus;
                        lastObjectCurrentList.InspectionDeficiencyTechnicianStatusText = firstObjectNextList.InspectionDeficiencyTechnicianStatusText;
                        lastObjectCurrentList.InspectionDeficiencyAdminStatusText = firstObjectNextList.InspectionDeficiencyAdminStatusText;
                        lastObjectCurrentList.CreatedDate = firstObjectNextList.CreatedDate;
                        lastObjectCurrentList.CreatedBy = firstObjectNextList.CreatedBy;
                        lastObjectCurrentList.ModifiedDate = firstObjectNextList.ModifiedDate;
                        lastObjectCurrentList.ModifiedBy = firstObjectNextList.ModifiedBy;
                        lastObjectCurrentList.InspectionDeficiencyMTO = firstObjectNextList.InspectionDeficiencyMTO;
                    }
                }
            }
            return allLists;
        }

        public List<List<InspectionDeficiencyViewModel>> GenerateParentSets(List<InspectionDeficiencyViewModel> allParents)
        {
            List<List<InspectionDeficiencyViewModel>> parentSets = new List<List<InspectionDeficiencyViewModel>>();
            List<InspectionDeficiencyViewModel> currentSet = new List<InspectionDeficiencyViewModel>();
            int currentChildrenCount = 0;
            //if (allParents != null)
            //{
            //    foreach (InspectionDeficiencyViewModel parent in allParents)
            //    {

            //        if (parent.InspectionDeficiencyPhotoViewModel != null)
            //        {
            //            if (parent.InspectionDeficiencyPhotoViewModel.Count > 10)
            //            {
            //                if (currentSet.Count > 0)
            //                {
            //                    parentSets.Add(currentSet);
            //                    currentSet = new List<InspectionDeficiencyViewModel>();
            //                    currentChildrenCount = 0;
            //                }
            //                parentSets.Add(new List<InspectionDeficiencyViewModel> { parent });
            //            }
            //            else
            //            {
            //                if (currentChildrenCount + parent.InspectionDeficiencyPhotoViewModel.Count <= 10)
            //                {
            //                    currentSet.Add(parent);
            //                    currentChildrenCount += parent.InspectionDeficiencyPhotoViewModel.Count;
            //                }
            //                else
            //                {
            //                    parentSets.Add(currentSet);
            //                    currentSet = new List<InspectionDeficiencyViewModel> { parent };
            //                    currentChildrenCount = parent.InspectionDeficiencyPhotoViewModel.Count;
            //                }
            //            }
            //        }
            //    }
            //}                


            if (allParents != null)
            {
                foreach (InspectionDeficiencyViewModel parent in allParents)
                {
                    int imageCount = parent.InspectionDeficiencyPhotoViewModel?.Count ?? 0;

                    if (imageCount > 10)
                    {
                        // Split this parent if it has more than 10 images
                        while (imageCount > 10)
                        {
                            // Create a new parent view model with the first 10 images
                            var subset = new InspectionDeficiencyViewModel
                            {
                                InspectionDeficiencyPhotoViewModel = parent.InspectionDeficiencyPhotoViewModel.Take(10).ToList()
                            };

                            // If currentSet is not empty, finalize it
                            if (currentSet.Count > 0)
                            {
                                parentSets.Add(currentSet);
                                currentSet = new List<InspectionDeficiencyViewModel>();
                                currentChildrenCount = 0;
                            }

                            // Add the subset to parentSets
                            parentSets.Add(new List<InspectionDeficiencyViewModel> { subset });

                            // Update the remaining images for the current parent
                            parent.InspectionDeficiencyPhotoViewModel = parent.InspectionDeficiencyPhotoViewModel.Skip(10).ToList();
                            imageCount -= 10;
                        }

                        // After the loop, if there are remaining images, handle them
                        if (imageCount > 0)
                        {
                            // Add the remainder of the parent to the current set
                            currentSet.Add(parent);
                            currentChildrenCount += imageCount;
                        }
                    }
                    else
                    {
                        // Handle the case where the parent has 10 or fewer images
                        if (currentChildrenCount + imageCount <= 10)
                        {
                            currentSet.Add(parent);
                            currentChildrenCount += imageCount;
                        }
                        else
                        {
                            // Finalize the current set if adding this parent would exceed the limit
                            parentSets.Add(currentSet);
                            currentSet = new List<InspectionDeficiencyViewModel> { parent };
                            currentChildrenCount = imageCount;
                        }
                    }
                }
            }

            if (currentSet.Count > 0)
            {
                parentSets.Add(currentSet);
            }

            return parentSets;

        }

        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        List<Parent> allParents = new List<Parent>
        //        {
        //            new Parent("Parent 1", 3),
        //            new Parent("Parent 2", 5),
        //            new Parent("Parent 3", 2),
        //            new Parent("Parent 4", 7),
        //            new Parent("Parent 5", 1),
        //            new Parent("Parent 6", 4),
        //            new Parent("Parent 7", 6),
        //            new Parent("Parent 8", 3),
        //            new Parent("Parent 9", 5)
        //        };

        //        List<List<Parent>> parentSets = GenerateParentSets(allParents);
        //        PrintParentSets(parentSets);
        //    }

        //    static List<List<Parent>> GenerateParentSets(List<Parent> allParents)
        //    {
        //        List<List<Parent>> parentSets = new List<List<Parent>>();
        //        List<Parent> currentSet = new List<Parent>();
        //        int currentChildrenCount = 0;

        //        foreach (Parent parent in allParents)
        //        {
        //            if (parent.ChildrenCount > 4)
        //            {
        //                if (currentSet.Count > 0)
        //                {
        //                    parentSets.Add(currentSet);
        //                    currentSet = new List<Parent>();
        //                    currentChildrenCount = 0;
        //                }
        //                parentSets.Add(new List<Parent> { parent });
        //            }
        //            else
        //            {
        //                if (currentChildrenCount + parent.ChildrenCount <= 10)
        //                {
        //                    currentSet.Add(parent);
        //                    currentChildrenCount += parent.ChildrenCount;
        //                }
        //                else
        //                {
        //                    parentSets.Add(currentSet);
        //                    currentSet = new List<Parent> { parent };
        //                    currentChildrenCount = parent.ChildrenCount;
        //                }
        //            }
        //        }

        //        if (currentSet.Count > 0)
        //        {
        //            parentSets.Add(currentSet);
        //        }

        //        return parentSets;
        //    }

        //    static void PrintParentSets(List<List<Parent>> parentSets)
        //    {
        //        int setCount = 1;
        //        foreach (List<Parent> set in parentSets)
        //        {
        //            Console.WriteLine($"Parent Set {setCount}:");
        //            foreach (Parent parent in set)
        //            {
        //                Console.WriteLine($"{parent.Name} - Children: {parent.ChildrenCount}");
        //            }
        //            Console.WriteLine();
        //            setCount++;
        //        }
        //    }
        //}

        //class Parent
        //{
        //    public string Name { get; set; }
        //    public int ChildrenCount { get; set; }

        //    public Parent(string name, int childrenCount)
        //    {
        //        Name = name;
        //        ChildrenCount = childrenCount;
        //    }
        //}

        //public void ConvertHtmlToPdf(string xHtml, string css)
        //{
        //using (var stream = new FileStream(OUTPUT_FILE, FileMode.Create))
        //{
        //using (var document = new Document())
        //{
        //var writer = PdfWriter.GetInstance(document, stream);
        //document.Open();

        //// instantiate custom tag processor and add to `HtmlPipelineContext`.
        //var tagProcessorFactory = Tags.GetHtmlTagProcessorFactory();
        //tagProcessorFactory.AddProcessor(
        //new TableDataProcessor(),
        //new string[] { HTML.Tag.TD }
        //);
        //var htmlPipelineContext = new HtmlPipelineContext(null);
        //htmlPipelineContext.SetTagFactory(tagProcessorFactory);

        //var pdfWriterPipeline = new PdfWriterPipeline(document, writer);
        //var htmlPipeline = new HtmlPipeline(htmlPipelineContext, pdfWriterPipeline);

        //// get an ICssResolver and add the custom CSS
        //var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
        //cssResolver.AddCss(css, "utf-8", true);
        //var cssResolverPipeline = new CssResolverPipeline(
        //cssResolver, htmlPipeline
        //);

        //var worker = new XMLWorker(cssResolverPipeline, true);
        //var parser = new XMLParser(worker);
        //using (var stringReader = new StringReader(xHtml))
        //{
        //parser.Parse(stringReader);
        //}
        //}
        //}
        //}

        static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                context.Controller.ViewData,
                context.Controller.TempData,
                sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        public static bool ShowRegistration()
        {
            bool bIsRegistration = false;
            bIsRegistration = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsRegistration"]);
            return bIsRegistration;
        }

        public ActionResult IncidentReportList(int id)
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

        public ActionResult AddHistoryLegacyDocuments()
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //Session["CustomerId"] = id;
                return View();
            }
        }

        public ActionResult DeleteHistoryLegacyDocuments(long id)
        {
            if (Session["LoggedInUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //Session["CustomerId"] = id;
                var itm = DatabaseHelper.getCustomerLocationHistoryLegacyFile(id);
                return View(itm);                
            }
        }
    }

    //public class RotateTextEventHelper : PdfPageEventHelper
    //{
    //public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, Document document)
    //{
    //// Get the current page
    //PdfContentByte contentByte = writer.DirectContent;

    //// Rotate the text 90 degrees
    //contentByte.AddTemplate(contentByte.CreateTemplate(document.PageSize.Height, document.PageSize.Width), 0, 0, -1, 0, 0, document.PageSize.Height);
    //}
    //}
}

//if ((k.FacilitiesAreaId == 4 || k.FacilitiesAreaId == 5) || (k.FacilitiesAreaId == 4 && k.FacilitiesAreaId == 5))
//{
//    strVar += "T.B.D";
//}
//else
//{
//    strVar += "" + iDetails.InspectionDocumentNo + "";
//}