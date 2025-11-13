using CamV4.Helper;
using CamV4.Models;
using ExcelDataReader;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Controllers
{

    public class AccountController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //// Create the cookie object.
            //HttpCookie cookie = new HttpCookie("CAM1");
            //cookie["CAMIndu"] = "CamIndustrial";
            //// This cookie will remain  for one month.
            //cookie.Expires = DateTime.Now.AddMonths(1);
            //cookie.SameSite = SameSiteMode.Lax;
            //// Add it to the current web response.
            //Response.Cookies.Add(cookie);

            //HttpCookie cookie1 = new HttpCookie("CAM2");
            //cookie1["CAMIndu1"] = "CamIndustrial1";
            //// This cookie will remain  for one month.
            //cookie1.Expires = DateTime.Now.AddMonths(1);
            //cookie1.SameSite = SameSiteMode.Lax;
            //// Add it to the current web response.
            //Response.Cookies.Add(cookie1);

            Session.Remove("LoggedInUserId");
            Session.Remove("LoggedInUserName");
            Session.Remove("LoggedInUserType");
            //ViewBag.Title = ConfigurationManager.AppSettings["PageTitle"];
            return View();
        }

        //
        // POST: /Account/Login
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {

                //FirebaseHelper firebaseService = new FirebaseHelper();
                //FirebaseHelper.InitializeFirebase();
                //firebaseService.SendAndroidNotificationAsync("ePldgA9ATo6xh4IC_qmGDd:APA91bHhbvnOLMJa3HDYZ_j4xW1X226YtnH7RuFrOyNBp9sDprfDQBBuaJd8b-BVJNTbW40YxCT6o4L0fmwan9SxmhddQB78dNJhaOGpZ_af5714C8H6pns", "CAM Industrial ", "Dummy Message");

                var _password = MD5Hash(model.UserPassword);
                var user = db.Users.Where(x => x.UserName == model.UserName && x.UserPassword == _password).FirstOrDefault();
                if (user != null)
                {
                    if (user.UserType == 9)
                    {
                        Session["LoggedInUserId"] = user.UserId;
                        var customerContact = db.CustomerLocationContacts.Where(y => y.UserID == user.UserId).FirstOrDefault();
                        if (customerContact != null)
                        {
                            Session["LoggedInUserName"] = customerContact.ContactName;
                            Session["LoggedInUserType"] = user.UserType;
                        }
                    }
                    else
                    {
                        Session["LoggedInUserId"] = user.UserId;
                        Session["LoggedInUserName"] = user.UserName;
                        Session["LoggedInUserType"] = user.UserType;
                    }
                    if (model.RememberMe)
                    {
                        HttpCookie mycookie = new HttpCookie("LoginDetail");
                        mycookie.Values["Username"] = user.UserName;
                        mycookie.Values["Password"] = user.UserPassword;
                        mycookie.Expires = System.DateTime.Now.AddDays(365);
                        Response.Cookies.Add(mycookie);
                    }

                    if (user.UserType == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user.UserType == 2)
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    else if (user.UserType == 4)
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                    else if (user.UserType == 9)
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                    else if (user.UserType == 5)
                    {
                        return RedirectToAction("Index", "CustomerLocationContact");
                    }
                    else
                    {
                        //return RedirectToAction("Index", "User");
                        ViewBag.Message = string.Format("You are not authorized.");
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = string.Format("Login failed. User doesn't exist.");
                    return View();
                }                
            }
            catch (Exception ex)
            {
                ViewBag.Message = "User Name or Password incorrect. " +  ex.ToString();
                return View();
            }
        }

        [HttpPost]
        [CustomAntiForgeryToken]
        [AllowAnonymous]
        public async Task<JsonResult> EmpCreate(UserEmployeeViewModel model)
        {
            DatabaseEntities db = new DatabaseEntities();
            if (Session["LoggedInUserId"] == null)
            {
                return Json("Error");
            }
            else
            {
                try
                {
                    var message = new StringBuilder();
                    //if (ModelState.IsValid)
                    //{
                    using (DbContextTransaction objTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            User usr = new User();
                            if (model.UserName != null)
                            {
                                var exist = db.Users.Where(x => x.UserName == model.UserName).FirstOrDefault();
                                if (exist == null)
                                {
                                    usr.UserName = model.UserName;
                                    usr.UserPassword = MD5Hash(model.UserPassword);
                                    usr.IsActive = true;
                                    usr.UserType = model.UserType;
                                    usr.CreatedDate = DateTime.Now;
                                    usr.CreatedBy = Session["LoggedInUserId"].ToString();
                                    db.Users.Add(usr);
                                    db.SaveChanges();

                                    Employee emp = new Employee();
                                    emp.EmployeeName = model.EmployeeName;
                                    emp.EmployeeEmail = model.EmployeeEmail;
                                    emp.EmployeeAddress = model.EmployeeAddress;
                                    emp.MobileNo = model.MobileNo;
                                    emp.CityID = model.CityID;
                                    emp.CountryID = model.CountryID;
                                    emp.ProvinceID = model.ProvinceID;
                                    emp.TitleDegrees = model.TitleDegrees;
                                    emp.IsActive = true;
                                    emp.Gender = model.Gender;
                                    emp.Pincode = model.PinCode;
                                    emp.UserID = usr.UserId;
                                    emp.CreatedBy = Session["LoggedInUserId"].ToString();
                                    emp.CreatedDate = DateTime.Now;
                                    emp.IsStampingEngineer = model.IsStampingEngineer;
                                    db.Employees.Add(emp);
                                    db.SaveChanges();
                                    objTrans.Commit();
                                    return Json("Ok");
                                }
                                return Json("User already exist.");
                            }
                            return Json("Please enter user name.");
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            throw;
                        }
                    }

                    // }
                    //var mssge = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    //return Json(mssge);
                    //return Json(ModelState.Values.First().Errors[0].ErrorMessage);
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessage = "";
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            errorMessage = validationError.ErrorMessage;
                        }
                    }
                    return Json(errorMessage);
                }
            }
        }


        [HttpPost]
        [CustomAntiForgeryToken]
        [AllowAnonymous]
        public async Task<JsonResult> EmpEdit(UserEmployeeViewModel model)
        {
            bool bisExits = false;

            if (Session["LoggedInUserId"] == null)
            {
                return Json("Error");
            }
            else
            {
                try
                {
                    //var message = new StringBuilder();
                    using (DbContextTransaction objTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var userlist = db.Users.Where(x => x.UserName == model.UserName).ToList();
                            if (userlist.Count > 0)
                            {
                                foreach (var item in userlist)
                                {
                                    if (item.UserId != model.UserID && item.UserName == model.UserName)
                                    {
                                        bisExits = true;
                                    }
                                }
                            }
                            if (bisExits == true)
                            {
                                return Json("UserName already exist. Please try another UserName.");
                            }
                            else
                            {
                                var uName = db.Users.Where(x => x.UserName == model.UserName && x.IsActive == true).FirstOrDefault();
                                //var usr = db.Users.Where(x => x.UserId == model.UserID).FirstOrDefault();
                                if (uName == null)
                                {
                                    var userName = db.Users.Where(x => x.UserId == model.UserID).FirstOrDefault();
                                    if (userName != null)
                                    {
                                        userName.UserName = model.UserName;
                                        userName.IsActive = true;
                                        if (model.UserPassword != null) { userName.UserPassword = MD5Hash(model.UserPassword); }
                                        if (model.UserType != 0) { userName.UserType = model.UserType; }
                                        db.Entry(userName).State = EntityState.Modified;
                                        db.SaveChanges();

                                        var emp = db.Employees.Where(x => x.UserID == model.UserID).FirstOrDefault();
                                        emp.EmployeeName = model.EmployeeName;
                                        emp.EmployeeEmail = model.EmployeeEmail;
                                        emp.EmployeeAddress = model.EmployeeAddress;
                                        emp.CityID = model.CityID;
                                        emp.CountryID = model.CountryID;
                                        emp.ProvinceID = model.ProvinceID;
                                        emp.TitleDegrees = model.TitleDegrees;
                                        emp.MobileNo = model.MobileNo;
                                        emp.IsActive = true;
                                        emp.Gender = model.Gender;
                                        emp.Pincode = model.PinCode;
                                        emp.UserID = model.UserID;
                                        emp.ModifiedBy = Session["LoggedInUserId"].ToString();
                                        emp.ModifiedDate = DateTime.Now;
                                        emp.IsStampingEngineer = model.IsStampingEngineer;
                                        db.Entry(emp).State = EntityState.Modified;
                                        db.SaveChanges();
                                        objTrans.Commit();
                                        return Json("Ok");
                                    }

                                }
                                else
                                {
                                    if (uName.UserId == model.UserID)
                                    {
                                        uName.UserName = model.UserName;
                                        uName.IsActive = true;
                                        if (model.UserPassword != null) { uName.UserPassword = MD5Hash(model.UserPassword); }
                                        if (model.UserType != 0) { uName.UserType = model.UserType; }
                                        db.Entry(uName).State = EntityState.Modified;
                                        db.SaveChanges();

                                        var emp = db.Employees.Where(x => x.UserID == model.UserID).FirstOrDefault();
                                        emp.EmployeeName = model.EmployeeName;
                                        emp.EmployeeEmail = model.EmployeeEmail;
                                        emp.EmployeeAddress = model.EmployeeAddress;
                                        emp.CityID = model.CityID;
                                        emp.CountryID = model.CountryID;
                                        emp.ProvinceID = model.ProvinceID;
                                        emp.TitleDegrees = model.TitleDegrees;
                                        emp.MobileNo = model.MobileNo;
                                        emp.IsActive = true;
                                        emp.Gender = model.Gender;
                                        emp.Pincode = model.PinCode;
                                        emp.UserID = model.UserID;
                                        emp.ModifiedBy = Session["LoggedInUserId"].ToString();
                                        emp.IsStampingEngineer = model.IsStampingEngineer;
                                        emp.ModifiedDate = DateTime.Now;
                                        db.Entry(emp).State = EntityState.Modified;
                                        db.SaveChanges();
                                        objTrans.Commit();
                                        return Json("Ok");
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            throw;
                        }
                    }

                    //}
                    return Json("Something went wrong, Please enter relevant information.");
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessage = "";
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            // get the error message 
                            errorMessage = validationError.ErrorMessage;
                        }
                    }
                    return Json(errorMessage);
                }
            }
        }


        [HttpPost]
        [CustomAntiForgeryToken]
        [AllowAnonymous]
        public async Task<JsonResult> EmpEditMyProfile(UserEmployeeViewModel model)
        {

            if (Session["LoggedInUserId"] == null)
            {
                return Json("Error");
            }
            else
            {
                try
                {
                    using (DbContextTransaction objTrans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var emp = db.Employees.Where(x => x.UserID == model.UserID).FirstOrDefault();
                            if (emp != null)
                            {
                                emp.EmployeeName = model.EmployeeName;
                                emp.EmployeeEmail = model.EmployeeEmail;
                                emp.EmployeeAddress = model.EmployeeAddress;
                                emp.CityID = model.CityID;
                                emp.CountryID = model.CountryID;
                                emp.ProvinceID = model.ProvinceID;
                                emp.TitleDegrees = model.TitleDegrees;
                                emp.MobileNo = model.MobileNo;
                                emp.IsActive = true;
                                emp.Gender = model.Gender;
                                emp.Pincode = model.PinCode;
                                emp.UserID = model.UserID;
                                emp.ModifiedBy = Session["LoggedInUserId"].ToString();
                                emp.ModifiedDate = DateTime.Now;
                                emp.IsStampingEngineer = model.IsStampingEngineer;
                                db.Entry(emp).State = EntityState.Modified;
                                db.SaveChanges();
                                objTrans.Commit();
                                return Json("Ok");
                            }
                            return Json("Something went wrong.");
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            throw;
                        }
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessage = "";
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            // get the error message 
                            errorMessage = validationError.ErrorMessage;
                        }
                    }
                    return Json(errorMessage);
                }
            }
        }

        public static string MD5Hash(string password)
        {
            if (password != null)
            {
                return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(password)));
            }
            return null;
        }

        [Route("uploadImagestoItmFolder")]
        [HttpPost]
        public async Task<int> UploadFiles(Images_Upload model)
        {
            try
            {
                if (model != null)
                {
                    var img = UploadPathOfFile(model);
                    model.CreatedBy = Session["LoggedInUserId"].ToString();
                    var _ret = DatabaseHelper.saveImages(model);
                    return 1;
                }
                else { return -1; }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadPathOfFile(Images_Upload file)
        {
            string path = Server.MapPath("~/img/items/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];
                postedFile.SaveAs(path + postedFile.FileName);
            }
            return null;
        }

        //SaveCustomer
        [Route("uploadImagetoLogoFolder")]
        [HttpPost]
        public async Task<string> UploadLogo(Customer model)
        {
            try
            {
                var customer = db.Customers.Where(x => x.CustomerName == model.CustomerName).FirstOrDefault();
                if (customer != null)
                {
                    if (model.CustomerId == 0)
                    {
                        return "Company already exist.";
                    }
                    if (model.CustomerId != 0 && customer.CustomerId != model.CustomerId)
                    {
                        return "Company already exist.";
                    }
                }
                if (model.CustomerName == null)
                {
                    return "Please enter customer name.";
                }
                if (model.user == null)
                {
                    return "Please enter user information.";
                }
                else
                {
                    if (model.user.UserName == null)
                    {
                        return "Please enter user name.";
                    }
                    if (model.CustomerId == 0)
                    {
                        if (model.user.UserPassword == null)
                        {
                            return "Please enter user password.";
                        }
                    }
                }
                StringBuilder msg = new StringBuilder();
                Customer cModel = new Customer();
                List<CustomerLocation> cLModel = new List<CustomerLocation>();
                var _createdById = Session["LoggedInUserId"].ToString();
                var img = UploadPathOfLogoImage(model);
                if (model.CustomerId != 0)
                {
                    var _ret = DatabaseHelper.editCustomer(model);
                    return _ret;
                }
                else
                {
                    if (Session["SessionAddCustomerInfo"] != null)
                    {
                        cModel = (Customer)Session["SessionAddCustomerInfo"];
                        var found = db.Users.Where(x => x.UserName == model.user.UserName).FirstOrDefault();
                        if (found == null)
                        {
                            cModel.CreatedBy = _createdById;
                            var _cModel = DatabaseHelper.saveCustomer(cModel);
                            if (_cModel != null)
                            {
                                if (Session["SessionAddCustomerLocInfo"] != null)
                                {
                                    cLModel = (List<CustomerLocation>)Session["SessionAddCustomerLocInfo"];
                                    foreach (var item in cLModel)
                                    {
                                        item.CustomerId = Convert.ToInt64(_cModel);
                                        item.CreatedBy = _createdById;
                                        var _cLModel = DatabaseHelper.saveCustomerLocation(item);
                                    }
                                    Session.Remove("SessionAddCustomerLocInfo");
                                }
                                return "Ok";
                            }
                            Session.Remove("SessionAddCustomerInfo");
                            Session.Remove("SessionAddCustomerLocInfo");
                            return "Something went wrong.";
                        }
                        Session.Remove("SessionAddCustomerInfo");
                        return "User already exist. Please try another user name.";
                    }
                    else
                    {
                        model.CreatedBy = _createdById;
                        var _model = DatabaseHelper.saveCustomer(model);
                        return "Ok";
                    }

                }
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessage = "";
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        errorMessage = validationError.ErrorMessage;
                    }
                }
                return errorMessage;
            }
        }

        [Route("UploadDrawingFiles")]
        [HttpPost]
        public HttpResponseMessage UploadDrawingFiles(object obj)
        {
            var length = Request.ContentLength;
            var bytes = new byte[length];
            Request.InputStream.Read(bytes, 0, length);

            var fileName = Request.Headers["X-File-Name"];
            var fileSize = Request.Headers["X-File-Size"];
            var fileType = Request.Headers["X-File-Type"];

            var saveToFileLoc = fileName;

            string path = Server.MapPath("~/DrawingFiles/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // save the file.            
            //foreach (string key in Request.Files)
            //{
            //    HttpPostedFileBase postedFile = Request.Files[key];
            //    postedFile.SaveAs(path + postedFile.FileName);
            //}
            //saveToFileLoc
            var fileStream = new FileStream(path + saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bytes, 0, length);
            fileStream.Close();


            //return string.Format("{0} bytes uploaded", bytes.Length);
            return null;
        }



        [Route("UploadCustHistoryFile")]
        [HttpPost]
        public ActionResult UploadCustHistoryFile()
        {
            try
            {
                // Check if there are any files in the request
                if (Request.Files.Count == 0)
                {
                    return Json(new { success = false, message = "No file was uploaded." }, JsonRequestBehavior.AllowGet);
                }

                // Get the first file from the request
                HttpPostedFileBase file = Request.Files[0];
                if (file == null || file.ContentLength == 0)
                {
                    return Json(new { success = false, message = "Uploaded file is empty or invalid." }, JsonRequestBehavior.AllowGet);
                }

                // Get the file name from the Content-Disposition header
                string fileName = file.FileName; // This retrieves the file name sent by the client (finalFileName)
                if (string.IsNullOrEmpty(fileName))
                {
                    return Json(new { success = false, message = "File name is missing." }, JsonRequestBehavior.AllowGet);
                }

                // Define the path to save the file
                string path = Server.MapPath("~/CustFilesHistory/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Sanitize the file name to prevent path traversal
                fileName = Path.GetFileName(fileName); // Ensures no directory traversal
                string saveToFileLoc = Path.Combine(path, fileName);

                // Save the file
                file.SaveAs(saveToFileLoc);

                // Return success response
                return Json(new { success = true, message = $"File {fileName} uploaded successfully", fileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Return error response
                return Json(new { success = false, message = $"File upload failed: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }



        //[Route("UploadCustHistoryFile")]
        //[HttpPost]
        //public HttpResponseMessage UploadCustHistoryFile(object obj)
        //{
        //    var length = Request.ContentLength;
        //    var bytes = new byte[length];
        //    Request.InputStream.Read(bytes, 0, length);

        //    var fileName = Request.Headers["file"];
        //    var fileSize = Request.Headers["X-File-Size"];
        //    var fileType = Request.Headers["X-File-Type"];

        //    var saveToFileLoc = fileName;

        //    string path = Server.MapPath("~/CustFilesHistory/");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    // save the file.            
        //    //foreach (string key in Request.Files)
        //    //{
        //    //    HttpPostedFileBase postedFile = Request.Files[key];
        //    //    postedFile.SaveAs(path + postedFile.FileName);
        //    //}
        //    //saveToFileLoc
        //    var fileStream = new FileStream(path + saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
        //    fileStream.Write(bytes, 0, length);
        //    fileStream.Close();


        //    //return string.Format("{0} bytes uploaded", bytes.Length);
        //    return null;
        //}

        //[Route("UploadCustHistoryFile")]
        //[HttpPost]
        //public HttpResponseMessage UploadCustHistoryFile(DeleteFileModel obj)
        //{
        //    try
        //    {
        //        var length = Request.ContentLength;
        //        var bytes = new byte[length];
        //        Request.InputStream.Read(bytes, 0, length);

        //        // use the file name passed from AngularJS (final name)
        //        var finalFileName = obj.FileName;

        //        string path = Server.MapPath("~/CustFilesHistory/");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        // save file
        //        var filePath = Path.Combine(path, finalFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
        //        {
        //            fileStream.Write(bytes, 0, length);
        //        }

        //        var result = new
        //        {
        //            SavedFileName = finalFileName
        //        };

        //        //return CreateResponse(HttpStatusCode.OK, result);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //return CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
        //        return null;
        //    }
        //}


        [HttpPost]
        public HttpResponseMessage UploadPathOfLogoImage(Customer file)
        {
            string path = Server.MapPath("~/img/logos/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];
                postedFile.SaveAs(path + postedFile.FileName);
            }
            return null;
        }

        [Route("uploadImagetoComponentFolder")]
        [HttpPost]
        public async Task<string> UploadImagetoComponentFolder(Component model)
        {
            try
            {
                if (model != null)
                {
                    var img = UploadPathOfComponentImage(model);
                    model.CreatedBy = Session["LoggedInUserId"].ToString();
                    if (model.ComponentId != 0)
                    {
                        var data = DatabaseHelper.editComponent(model);
                        return "Ok";
                    }
                    else
                    {
                        var _ret = DatabaseHelper.saveComponent(model);
                        return "Ok";
                    }
                }
                else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Route("importComponentPriceList")]
        [HttpPost]
        public async Task<ActionResult> ImportComponentPriceList(HttpPostedFileBase file, string ComponentId, string Component)
        {
            Logger.Info("Start the process for Import Price list at " + System.DateTime.Now);
            object obj = new object();
            DataTable dataTable = new DataTable();
            DatabaseEntities db = new DatabaseEntities();
            long lComponentId = 0;
            lComponentId = Convert.ToInt32(ComponentId);
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filePath = Path.Combine(Server.MapPath("~/pricelist/"), Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);

                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Use the AsDataSet extension method
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true // Use first row as column names
                                }
                            });
                            // Get the first DataTable
                            dataTable = result.Tables[0];
                        }
                    }
                    var column = new DataColumn("IsAlreadyExists", typeof(bool));
                    column.DefaultValue = false;
                    dataTable.Columns.Add(column);
                    //var columnDuplicate = new DataColumn("IsDuplicateItemNo", typeof(bool));
                    //columnDuplicate.DefaultValue = false;
                    //dataTable.Columns.Add(columnDuplicate);
                    var resultHtml = "";
                    if (dataTable.Rows.Count > 0)
                    {
                        DataTable dtOutput = new DataTable();
                        dtOutput = InsertData(dataTable, lComponentId, Component, false);
                        resultHtml = await ProcessExcelFile(filePath, dtOutput);
                    }

                    Logger.Info("completed the import process " + System.DateTime.Now);
                    return Content(resultHtml, "text/html");
                }
                return new HttpStatusCodeResult(400, "No file uploaded");
            }
            catch (Exception ex)
            {
                Logger.Info("Error in the import ImportComponentPriceList in Account Controller : The error is - " + ex.Message.ToString() + ex.StackTrace.ToString() + " at " + System.DateTime.Now);
                return null;
            }
        }
        [Route("verifyComponentPriceList")]
        [HttpPost]
        public async Task<ActionResult> VerifyComponentPriceList(HttpPostedFileBase file, string ComponentId, string Component)
        {
            Logger.Info("Start the process for Import Price list at " + System.DateTime.Now);
            object obj = new object();
            DataTable dataTable = new DataTable();
            DatabaseEntities db = new DatabaseEntities();
            long lComponentId = 0;
            lComponentId = Convert.ToInt32(ComponentId);
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var filePath = Path.Combine(Server.MapPath("~/pricelist/"), Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);

                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Use the AsDataSet extension method
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true // Use first row as column names
                                }
                            });
                            // Get the first DataTable
                            dataTable = result.Tables[0];
                        }
                    }
                    var column = new DataColumn("IsAlreadyExists", typeof(bool));
                    column.DefaultValue = false;
                    dataTable.Columns.Add(column);
                    var resultHtml = "";
                    if (dataTable.Rows.Count > 0)
                    {
                        DataTable dtOutput = new DataTable();
                        dtOutput = InsertData(dataTable, lComponentId, Component, true);
                        resultHtml = await ProcessExcelFile(filePath, dtOutput);
                    }

                    Logger.Info("completed the import process " + System.DateTime.Now);
                    return Content(resultHtml, "text/html");
                }
                return new HttpStatusCodeResult(400, "No file uploaded");
            }
            catch (Exception ex)
            {
                Logger.Info("Error in the import ImportComponentPriceList in Account Controller : The error is - " + ex.Message.ToString() + ex.StackTrace.ToString() + " at " + System.DateTime.Now);
                return null;
            }
        }
        public DataTable InsertData(DataTable dataTable, long lComponentId, string Component, bool bVerify)
        {
            Logger.Info("In function insert data after reading excel file at " + System.DateTime.Now);
            bool bIsAlreadyExists = false;
            DataTable dtOutput = new DataTable();
            dtOutput = dataTable.Clone();
            try
            {
                string sItemNumber = "";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    sItemNumber = dataTable.Rows[i]["ITEMPARTNO"].ToString();
                    if (!string.IsNullOrEmpty(sItemNumber))
                    {
                        sItemNumber = dataTable.Rows[i]["ITEMPARTNO"].ToString().Trim();
                        var ComponentPriceDuplicateItem = db.ComponentPriceLists.Where(x => x.ItemPartNo == sItemNumber).FirstOrDefault();
                        if (ComponentPriceDuplicateItem != null)
                        {
                            dataTable.Rows[i]["IsAlreadyExists"] = true;
                            bIsAlreadyExists = true;
                        }
                    }
                    sItemNumber = "";
                }
                if (bVerify == true)
                {
                    return dataTable;
                }
                using (var context = new DatabaseEntities())
                {
                    decimal dSurcharge = 0;
                    decimal dMarkup = 0;
                    List<ComponentPropertyType> lstComponentPropertyType = new List<ComponentPropertyType>();
                    lstComponentPropertyType = DatabaseHelper.getComponentPropertyType();
                    int rowIndex = 0;

                    List<ImpSettingsViewModel> objImpSettingsViewModel = (from Is in db.ImpSettings
                                                                          select new ImpSettingsViewModel
                                                                          {
                                                                              SettingID = Is.SettingID,
                                                                              SettingType = Is.SettingType, // Handle null cases
                                                                              SettingValue = Is.SettingValue
                                                                          }).ToList();

                    foreach (var item in objImpSettingsViewModel)
                    {
                        if (item.SettingType == "Surcharge")
                        {
                            if (item.SettingValue != "")
                            {
                                dSurcharge = Convert.ToDecimal(item.SettingValue);
                            }
                            else
                            {
                                dSurcharge = 0;
                            }
                        }
                        if (item.SettingType == "Markup")
                        {
                            if (item.SettingValue != "")
                            {
                                dMarkup = Convert.ToDecimal(item.SettingValue);
                            }
                            else
                            {
                                dMarkup = 0;
                            }
                        }
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Manufacturer objManufacturer = new Manufacturer();
                        long ComponentPriceId = 0;
                        long ManufacturerId = 0;

                        string ComponentPriceDesc = "";
                        if (Convert.ToString(row["MANUFACTURERS"]).Trim() != "")
                        {
                            objManufacturer = DatabaseHelper.getManufacturerByName(Convert.ToString(row["MANUFACTURERS"]).Trim());
                            if (objManufacturer != null)
                            {
                                ManufacturerId = objManufacturer.ManufacturerId;
                            }
                        }
                        var itemPartNo = row["ITEMPARTNO"].ToString();
                        if (itemPartNo != "")
                        {
                            var ComponentPriceListItem = db.ComponentPriceLists.Where(x => x.ItemPartNo == itemPartNo).FirstOrDefault();
                            decimal dLabour = 0;
                            if (row["LABOUR"].ToString() != "")
                            {
                                dLabour = Convert.ToDecimal(row["LABOUR"]);
                            }
                            if (ComponentPriceListItem != null)
                            {


                                ComponentPriceId = ComponentPriceListItem.ComponentPriceId;
                                ComponentPriceListItem.ManufacturerId = Convert.ToInt32(ManufacturerId);
                                ComponentPriceListItem.ComponentWeight = Convert.ToDecimal(row["WEIGHT"]);
                                ComponentPriceListItem.ComponentPrice = Convert.ToDecimal(row["PRICE"]);
                                ComponentPriceListItem.ComponentLabourTime = dLabour;// Convert.ToDecimal(row["LABOUR"]);
                                ComponentPriceListItem.ComponentPriceDescription = ComponentPriceDesc;
                                ComponentPriceListItem.Surcharge = dSurcharge;
                                ComponentPriceListItem.Markup = dMarkup;
                                ComponentPriceListItem.TotalPrice = Convert.ToDecimal(row["PRICE"]) * dSurcharge * dMarkup;
                                ComponentPriceListItem.ModifiedBy = Session["LoggedInUserId"].ToString();
                                ComponentPriceListItem.ModifiedDate = DateTime.Now;
                                db.Entry(ComponentPriceListItem).State = EntityState.Modified;
                                db.SaveChanges();


                                var ComponentPriceListDetailslistingDelete = db.ComponentPriceListDetails.Where(y => y.ComponentPriceId == ComponentPriceId).ToList();
                                db.ComponentPriceListDetails.RemoveRange(ComponentPriceListDetailslistingDelete);
                                db.SaveChanges();
                            }
                            else
                            {
                                // Create parent entity
                                var componentPrice = new ComponentPriceList
                                {
                                    ComponentId = lComponentId,
                                    ItemPartNo = row["ITEMPARTNO"].ToString(),
                                    ManufacturerId = Convert.ToInt32(ManufacturerId), // Ensure this maps to your actual ID
                                    ComponentWeight = Convert.ToDecimal(row["WEIGHT"]),
                                    ComponentPrice = Convert.ToDecimal(row["PRICE"]),
                                    ComponentLabourTime = dLabour,// Convert.ToDecimal(row["LABOUR"]),
                                    ComponentPriceDescription = ComponentPriceDesc,
                                    Surcharge = dSurcharge,
                                    Markup = dMarkup,
                                    TotalPrice = Convert.ToDecimal(row["PRICE"]) * dSurcharge * dMarkup,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = Session["LoggedInUserId"].ToString(),
                                    ModifiedBy = Session["LoggedInUserId"].ToString(),
                                    ModifiedDate = DateTime.Now
                                };

                                // Add the parent entity to the context
                                context.ComponentPriceLists.Add(componentPrice);
                                context.SaveChanges();
                                ComponentPriceId = componentPrice.ComponentPriceId;
                            }

                            var columnsToExclude = new HashSet<string>
                                {
                                    "ITEMPARTNO",
                                    "MANUFACTURERS",
                                    "WEIGHT",
                                    "PRICE",
                                    "LABOUR",
                                    "IsAlreadyExists"
                                };

                            var rowListAllColum = ConvertRowToList(dataTable, rowIndex);

                            var filteredList = rowListAllColum.Where(item => !columnsToExclude.Contains(item.Key)).ToList();
                            ComponentPriceDesc = Component + " " + Convert.ToString(row["MANUFACTURERS"]).Trim();
                            foreach (var item in filteredList)
                            {
                                if (item.Key.IndexOf("ISALREADYEXISTS", StringComparison.OrdinalIgnoreCase) == -1)
                                {
                                    string strComponentPropertyTypeDesctiption = "";
                                    strComponentPropertyTypeDesctiption = item.Key.ToUpper().Trim();
                                    var getId = lstComponentPropertyType.Where(x => x.ComponentPropertyTypeDesctiption.Contains(strComponentPropertyTypeDesctiption)).FirstOrDefault();
                                    if (getId == null)
                                    {
                                        ComponentPropertyType objComponentPropertyType = new ComponentPropertyType();
                                        objComponentPropertyType.ComponentPropertyTypeName = strComponentPropertyTypeDesctiption;
                                        objComponentPropertyType.ComponentPropertyTypeDesctiption = strComponentPropertyTypeDesctiption;
                                        objComponentPropertyType.IsActive = true;
                                        objComponentPropertyType.CreatedDate = DateTime.Now;
                                        objComponentPropertyType.CreatedBy = Session["LoggedInUserId"].ToString();
                                        objComponentPropertyType.ModifiedDate = DateTime.Now;
                                        objComponentPropertyType.ModifiedBy = Session["LoggedInUserId"].ToString();
                                        db.ComponentPropertyTypes.Add(objComponentPropertyType);
                                        db.SaveChanges();

                                        lstComponentPropertyType = DatabaseHelper.getComponentPropertyType();
                                        getId = lstComponentPropertyType.Where(x => x.ComponentPropertyTypeDesctiption.Contains(strComponentPropertyTypeDesctiption)).FirstOrDefault();
                                    }

                                    if (getId != null)
                                    {
                                        string strVal = "";
                                        strVal = Convert.ToString(item.Value).Trim();
                                        strVal = strVal.Replace("NULL", "");
                                        strVal = strVal.Replace("null", "");
                                        if (Convert.ToString(strVal) != "")
                                        {
                                            if (strVal != "0")
                                            {
                                                var objComponentPropertyValues = db.ComponentPropertyValues.Where(y => y.ComponentPropertyValue1 == strVal).FirstOrDefault();
                                                ComponentPriceDesc += " " + Convert.ToString(strVal).Trim();
                                                ComponentPriceListDetail objComponentPriceListDetail = new ComponentPriceListDetail();
                                                objComponentPriceListDetail.ComponentPriceId = ComponentPriceId;
                                                objComponentPriceListDetail.ComponentPropertyTypeId = getId.ComponentPropertyTypeId;
                                                objComponentPriceListDetail.ComponentPricePropertyTypeDescription = strComponentPropertyTypeDesctiption;
                                                if (objComponentPropertyValues != null)
                                                {
                                                    objComponentPriceListDetail.ComponentPricePropertyValueId = objComponentPropertyValues.ComponentPropertyValueId;
                                                }
                                                objComponentPriceListDetail.ComponentPricePropertyValue = Convert.ToString(item.Value).Trim();
                                                objComponentPriceListDetail.IsActive = true;
                                                objComponentPriceListDetail.CreatedDate = DateTime.Now;
                                                objComponentPriceListDetail.CreatedBy = Session["LoggedInUserId"].ToString();
                                                objComponentPriceListDetail.ModifiedDate = DateTime.Now;
                                                objComponentPriceListDetail.ModifiedBy = Session["LoggedInUserId"].ToString();
                                                db.ComponentPriceListDetails.Add(objComponentPriceListDetail);
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            var objComponentPriceLists = db.ComponentPriceLists.Where(x => x.ComponentPriceId == ComponentPriceId).FirstOrDefault();
                            if (objComponentPriceLists != null)
                            {
                                objComponentPriceLists.ComponentPriceDescription = ComponentPriceDesc;
                                objComponentPriceLists.ModifiedDate = DateTime.Now;
                                db.Entry(objComponentPriceLists).State = EntityState.Modified;
                                db.SaveChanges();
                                ComponentPriceDesc = "";
                            }
                            ComponentPriceId = 0;
                            rowIndex += 1;
                            dtOutput.ImportRow(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Info("error in insert data from import process:  " + ex.Message.ToString() + " : " + ex.StackTrace.ToString() + System.DateTime.Now);
            }
            return dtOutput;
        }
        public static List<KeyValuePair<string, object>> ConvertRowToList(DataTable table, int rowIndex)
        {
            // Check if rowIndex is valid
            if (rowIndex < 0 || rowIndex >= table.Rows.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Invalid row index.");
            }

            // Get the row
            DataRow row = table.Rows[rowIndex];

            // Create a list to hold column name and value pairs
            var list = new List<KeyValuePair<string, object>>();

            foreach (DataColumn column in table.Columns)
            {
                // Add each column name and corresponding value to the list
                list.Add(new KeyValuePair<string, object>(column.ColumnName, row[column]));
            }

            return list;
        }
        public static object[] ConvertRowToArray(DataTable table, int rowIndex)
        {
            // Check if rowIndex is valid
            if (rowIndex < 0 || rowIndex >= table.Rows.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Invalid row index.");
            }

            // Get the row
            DataRow row = table.Rows[rowIndex];

            // Create an array to hold the column names and values
            object[] rowArray = new object[table.Columns.Count * 2];

            int index = 0;
            foreach (DataColumn column in table.Columns)
            {
                rowArray[index++] = column.ColumnName;
                rowArray[index++] = row[column];
            }

            return rowArray;
        }
        private async Task<string> ProcessExcelFile(string filePath, DataTable dtInput)
        {

            var sb = new StringBuilder();
            // Assuming dt is your DataTable
            sb.Append("<table class='table align-items-center mb-0'>");

            bool bFirst = true;
            int isDuplicatePOS = 0;
            // Process each row in the DataTable
            foreach (DataRow row in dtInput.Rows)
            {
                if (bFirst)
                {
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    // Process each column in the DataTable
                    foreach (DataColumn column in dtInput.Columns)
                    {
                        sb.Append($"<th class='text-dark text-uppercase text-secondary text-sm font-weight-bolder opacity-7 ps-2'>{column.ColumnName}</th>");
                        if (column.ColumnName == "IsAlreadyExists")
                        {
                            isDuplicatePOS = dtInput.Columns.IndexOf(column);
                        }
                    }
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    bFirst = false;
                }

                if (Convert.ToBoolean(row[isDuplicatePOS].ToString()) == true)
                {
                    sb.Append("<tr style='background:#FFA500;color:#000 !important;'>");
                }
                else
                {
                    sb.Append("<tr>");
                }
                // Process each cell in the row
                foreach (var item in row.ItemArray)
                {
                    sb.Append($"<td>{item}</td>");
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            // Return the HTML content
            return await Task.FromResult(sb.ToString());

        }
        private DataSet ReadExcelFile(Stream stream)
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                return reader.AsDataSet();
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadPathOfComponentImage(Component file)
        {
            string path = Server.MapPath("~/img/component/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];
                postedFile.SaveAs(path + postedFile.FileName);
            }
            return null;
        }

        [Route("uploadImagetoIdentifyRackingProfileFolder")]
        [HttpPost]
        public async Task<string> UploadImagetoIdentifyRackingProfileFolder(IdentifyRackingProfile model)
        {
            try
            {
                if (model != null)
                {
                    var img = UploadPathOfIdentifyRackingProfileImage(model);
                    model.CreatedBy = Session["LoggedInUserId"].ToString();
                    if (model.IdentifyRackingProfileID != 0)
                    {
                        var data = DatabaseHelper.editIdentifyRackingProfile(model);
                        return "Ok";
                    }
                    else
                    {
                        var _ret = DatabaseHelper.saveIdentifyRackingProfile(model);
                        return "Ok";
                    }
                }
                else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }


        [HttpPost]
        public HttpResponseMessage UploadPathOfIdentifyRackingProfileImage(IdentifyRackingProfile file)
        {
            string path = Server.MapPath("~/img/IdentifyRackingProfile/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string key in Request.Files)
            {
                HttpPostedFileBase postedFile = Request.Files[key];
                postedFile.SaveAs(path + postedFile.FileName);
            }
            return null;
        }

        [HttpPost]
        [CustomAntiForgeryToken]
        [AllowAnonymous]
        //[Route("SendEmailOfPDF")]
        public async Task<JsonResult> SendEmailOfPDF(MailViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var _se = DatabaseHelper.GetEmailInformation();

                try
                {
                    //FileContentResult attachmentFile = null;
                    string strMSG = "";
                    var iDetails = DatabaseHelper.getInspectionDetailsForSheet(model.InspectionId);
                    if (iDetails != null)
                    {
                        string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                        List<string> strCCEmailslist = new List<string>();
                        List<string> toCustContact = new List<string>();
                        strCCEmailslist.Add(iDetails.empModel.EmployeeEmail);
                        strCCEmailslist.Add("b.trivedi@camindustrial.net");
                        //strCCEmailslist.Add("nirav.m@siliconinfo.com");

                        if (model.LocationContactId != null)
                        {
                            string[] lContact = model.LocationContactId.Split(',');
                            toCustContact.Add(iDetails.custModel.CustomerEmail);
                            foreach (var item in lContact)
                            {
                                if (item != "")
                                {
                                    if (item != "0")
                                    {
                                        toCustContact.Add(DatabaseHelper.getLocationContactDetailsById(Convert.ToInt16(item)).ContactEmail);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (iDetails.custModel.CustomerEmail != "")
                            {
                                toCustContact.Add(iDetails.custModel.CustomerEmail);
                            }
                        }

                        //var subject = "" + iDetails.InspectionDocumentNo + "-" + iDetails.Customer + "";
                        var subject = "Racking Inspection Report Available for Review and Selection of the Deficiencies for quotation";
                        var toEmail = iDetails.custModel.CustomerEmail;
                        strMSG = "<html>";
                        strMSG += "<head>";
                        strMSG += "<style>";
                        strMSG += "p{margin:0px}";
                        strMSG += "</style>";
                        strMSG += "</head>";
                        strMSG += "<body>";
                        strMSG += "<div style='width:1200px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                        if (iDetails.custModel.CustomerEmail == "")
                        {
                            strMSG += "<p style='color:red;'>Customer email has been missing from system. Please update customer's email. </p>";
                            toEmail = iDetails.empModel.EmployeeEmail;
                        }

                        if (iDetails.custModel.CustomerContactName == null)
                        {
                            iDetails.custModel.CustomerContactName = "";
                        }

                        if (iDetails.custModel.CustomerContactName != "")
                        {
                            strMSG += "<p>Attention: " + iDetails.custModel.CustomerContactName + " [" + iDetails.Customer + "]</p>";
                        }
                        else
                        {
                            strMSG += "<p>Attention: " + iDetails.Customer + "</p>";
                        }
                        strMSG += "<br/>";
                        strMSG += "<br/>";
                        strMSG += "<p>We hope this message finds you well.</p>";
                        strMSG += "<p>We are pleased to inform you that the racking inspection report is now available on the Rack Auditor platform <a href='https://rack-manager.com/'>(rack-manager.com)</a> for your review.</p>";
                        strMSG += "<p>The outcome of the inspection, and the detailed findings are now documented in the report. We understand the importance of this report in providing valuable insights into the condition and any necessary actions regarding the pallet racking.</p>";
                        strMSG += "<p>Additionally, you can access the deficiency list and select red and/or yellow deficiencies that you would like us to provide repair/replace quotation.</p>";
                        strMSG += "<br/>";
                        strMSG += "<p>There are two ways to select the deficiencies:</p>";
                        strMSG += "<p>1) Click “Select Deficiency For Quotation” to select all red and/or yellow deficiencies, located at the top-right corner of the Deficiency List.</p>";
                        strMSG += "<p><span><img alt='SelectDeficiencyQuotationEmail' src = 'https://rack-manager.com/img/SelectDeficiencyQuotationEmail.png' /></span></p>";
                        strMSG += "<p>2) You can select checkboxes under “Quotation” → “Request Quotation”, which allows to select specific deficiencies.</p>";
                        strMSG += "<p><span><img alt='QuotationSelectionEmail' src = 'https://rack-manager.com/img/QuotationSelectionEmail.png' /></span></p>";
                        strMSG += "<p>Once you have made your selections, please click “Request Quotation”.</p>";
                        strMSG += "<p><span><img alt='QuotationButtonEmail' src = 'https://rack-manager.com/img/QuotationButtonEmail.png' /></span></p>";
                        strMSG += "<br/>";
                        strMSG += "<p>Please don’t hesitate to contact the assigned engineer :" + iDetails.empModel.EmployeeName + ", " + iDetails.empModel.MobileNo + ", " + iDetails.empModel.EmployeeEmail + " or office+1 800 772 3213 for any queries.</p>";
                        strMSG += "<br/>";
                        strMSG += "<p>It has been a pleasure working with you and we look forward to assisting you with any upcoming opportunities.</p>";
                        strMSG += "<p>Should you have any questions or require further clarification on any aspect of the report, please do not hesitate to reach out to us. Our team is available to discuss the findings and provide any assistance you may need.</p>";
                        strMSG += "<br/>";
                        strMSG += "<div><div></div></div><br/><br/><div><div>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Best regards,</span></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'> P.Eng, M.Tech, PMP</span></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                        strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='ES'>E ~ &nbsp;</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                        strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='ES'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='ES'>C ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='ES'>(403) 690-2976</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<p><span><img style='width:2.618in;height:.6458in'";
                        strMSG += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted=''";
                        strMSG += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                        strMSG += "</div>";
                        strMSG += "</div>";
                        strMSG += "</div>";
                        strMSG += "</body>";
                        strMSG += "</html>";

                        //string _emailTemplate = "<html><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta name='viewport' content='width=device-width, initial-scale=1'><meta http-equiv='X-UA-Compatible' content='IE=edge' /> "
                        //    + " <style>/* -- BODY & CONTAINER -- */ .body{background-color:#f6f6f6;width:100%}.container{display:block;margin:0 auto!important;max-width:580px;padding:10px;width:580px}.content{box-sizing:border-box;display:block;margin:0 auto;max-width:580px;padding:10px} "
                        //    + " /* -- HEADER, FOOTER, MAIN -- */ .main{background:#fff;border-radius:3px;width:100%}.wrapper{box-sizing:border-box;padding:20px} "
                        //    + " /* -- TYPOGRAPHY -- */ p,ul{font-family:sans-serif;font-size:16px;font-weight:400;margin:0 0 15px}p li,ul li{list-style-position:inside;margin-left:-20px;color:#666}ul.dashed{list-style-type:'- '}"
                        //    + " /* -- OTHER STYLES  -- */ .closing{font-size:13px;color:#003DA6}.message{font-size:15px;color:#666}.lastheader{margin-bottom:0;font-size:15px}"
                        //    + " /* -- RESPONSIVE AND MOBILE FRIENDLY STYLES  -- */ @media only screen and (max-width:620px){table[class=body] h1{font-size:28px!important;margin-bottom:10px!important}table[class=body] a,table[class=body] ol,table[class=body] p,table[class=body] span,table[class=body] td,table[class=body] ul{font-size:16px!important}table[class=body] .article,table[class=body] "
                        //    + " .wrapper{padding:10px!important}table[class=body] .content{padding:0!important}table[class=body] .container{padding:0!important;width:100%!important}table[class=body] .main{border-left-width:0!important;border-radius:0!important;border-right-width:0!important}table[class=body] "
                        //    + " .btn a,table[class=body] .btn table{width:100%!important}table[class=body] .img-responsive{height:auto!important;max-width:100%!important;width:auto!important}}</style></head>"

                        //    + " <body><table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'><tr><td>&nbsp;</td><td class='container'><div class='content'>"
                        //    + " <!-- START CENTERED WHITE CONTAINER --> <table role='presentation' class='main'>"
                        //    + " <!-- START MAIN CONTENT AREA --> <tr><td class='wrapper'><table role='presentation' border='0' cellpadding='0' cellspacing='0'><tr><td><p>Hello " + iDetails.Customer + ",</p><p class='message'>Please see the attached file.</p>"
                        //    + " <p class='lastheader'>Thanks & Regards,</p><p class='closing'>CAM Industrial.</p></td></tr></table></td></tr>"
                        //    + " <!-- END MAIN CONTENT AREA --></table> <!-- END CENTERED WHITE CONTAINER --></div></td><td>&nbsp;</td></tr></table></body></html>";
                        //start email Thread
                        var tEmail = new Thread(() => EmailHelper.SendPdfEmail(toCustContact, subject, null, strMSG, model.InspectionId, strCCEmailslist)); //attachmentFile
                        tEmail.Start();
                        return Json("Ok");
                    }
                    //var pdf = AdminController.ToPdfV2(model.InspectionId);
                }
                catch (Exception e)
                {
                    return Json("There has been an error: " + e.Message);
                }

            }
            var message = new StringBuilder();

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    message.Append(error.ErrorMessage + "\n\n");
                }
            }
            return Json(message);
        }

        [Route("SaveComponentPropertyType")]
        [HttpPost]
        public async Task<long> SaveComponentPropertyType(ComponentPropertyTypeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ComponentPropertyTypeViewModel typeModel = new ComponentPropertyTypeViewModel();
                    List<ComponentPropertyValue> valueModel = new List<ComponentPropertyValue>();
                    var _createdById = Session["LoggedInUserId"].ToString();
                    if (model.ComponentPropertyTypeId != 0)
                    {
                        var _ret = DatabaseHelper.editComponentPropertyType(model);
                        return 1;
                    }
                    else
                    {
                        if (Session["SessionAddComponentPropertyType"] != null)
                        {
                            typeModel = (ComponentPropertyTypeViewModel)Session["SessionAddComponentPropertyType"];
                            typeModel.CreatedBy = _createdById;
                            var _cModel = DatabaseHelper.saveComponentPropertyType(typeModel);

                            if (Session["SessionAddComponentPropertyValue"] != null)
                            {
                                valueModel = (List<ComponentPropertyValue>)Session["SessionAddComponentPropertyValue"];
                                foreach (var item in valueModel)
                                {
                                    item.ComponentPropertyTypeId = _cModel;
                                    item.CreatedBy = _createdById;
                                    var _cLModel = DatabaseHelper.saveComponentPropertyValues(item);
                                }
                                Session.Remove("SessionAddComponentPropertyValue");
                            }
                            Session.Remove("SessionAddComponentPropertyType");
                        }
                        else
                        {
                            model.CreatedBy = _createdById;
                            var _model = DatabaseHelper.saveComponentPropertyType(model);
                        }
                        return 1;
                    }
                }
                else { return 0; }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetComponentPriceDescription(string strSuggestion)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var suggestions = db.ComponentPriceLists.Where(i => i.ComponentPriceDescription.Contains(strSuggestion))
                .Select(i => new { i.ComponentPriceId, i.ComponentPriceDescription }).ToList();

                return Json(suggestions, JsonRequestBehavior.AllowGet);//, JsonRequestBehavior.AllowGet);
            }
        }


    }
}