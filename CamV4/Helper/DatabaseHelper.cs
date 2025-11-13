using CamV4.Controllers;
using CamV4.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CamV4.Helper
{
    public class DatabaseHelper
    {
        DatabaseEntities db = new DatabaseEntities();

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static Random _random = new Random();
        internal static List<InspectionStatu> GetAllInspectionStatus()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionStatu> inspectionStatusList = new List<InspectionStatu>();
                var itm = db.InspectionStatus.OrderBy(x => x.InspectionStatusId).ToList();
                if (itm.Count != 0)
                {
                    foreach (var d in itm)
                    {
                        InspectionStatu inspectionStatus = new InspectionStatu();
                        inspectionStatus.InspectionStatusId = d.InspectionStatusId;
                        inspectionStatus.InspectionStatus = d.InspectionStatus;
                        inspectionStatus.InspectionStatusColor = d.InspectionStatusColor;
                        inspectionStatusList.Add(inspectionStatus);
                    }
                    return inspectionStatusList;
                }
                return null;
            }
        }
        internal static List<EmployeeViewModel> getAllEmployee()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
                var itm = db.Employees.Where(x => x.IsActive == true).OrderBy(x => x.EmployeeName).ToList();
                if (itm.Count != 0)
                {
                    foreach (var d in itm)
                    {
                        EmployeeViewModel empVM = new EmployeeViewModel();
                        empVM.UserID = d.UserID;
                        empVM.EmployeeID = d.EmployeeID;
                        empVM.EmployeeName = d.EmployeeName;
                        empVM.EmployeeEmail = d.EmployeeEmail;
                        empVM.EmployeeAddress = d.EmployeeAddress;
                        empVM.PinCode = d.Pincode;
                        empVM.Gender = d.Gender;
                        empVM.TitleDegrees = d.TitleDegrees;
                        empVM.CreatedDate = d.CreatedDate;
                        empVM.CreatedBy = d.EmployeeName;
                        empVM.ModifiedDate = d.CreatedDate;
                        empVM.ModifiedBy = d.EmployeeName;
                        //if (d.CityID != null || d.CityID == 0) { empVM.CityName = getCitybyId(d.CityID).CityName; }
                        //if (d.ProvinceID != null || d.ProvinceID == 0) { empVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        //if (d.CountryID != null || d.CountryID == 0) { empVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        if (d.CityID != 0)
                        {
                            if (d.CityID != null) { empVM.CityName = getCitybyId(d.CityID).CityName; }
                        }
                        else
                        {
                            empVM.CityName = "";
                        }
                        if (d.ProvinceID != 0)
                        {
                            if (d.ProvinceID != null) { empVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        }
                        else
                        {
                            empVM.ProvianceName = "";
                        }
                        if (d.CountryID != 0)
                        {
                            if (d.CountryID != null) { empVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        }
                        else
                        {
                            empVM.CountryName = "";
                        }
                        empVMList.Add(empVM);
                    }
                    return empVMList;
                }
                return null;
            }
        }

        internal static List<EmployeeViewModel> GetAllStampingEmployee()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<EmployeeViewModel> empStampingVMList = new List<EmployeeViewModel>();
                var itm = db.Employees.Where(x => x.IsActive == true && x.IsStampingEngineer == 1).OrderBy(x => x.EmployeeName).ToList();
                if (itm.Count != 0)
                {
                    foreach (var d in itm)
                    {
                        EmployeeViewModel empVM = new EmployeeViewModel();
                        empVM.UserID = d.UserID;
                        empVM.EmployeeID = d.EmployeeID;
                        empVM.EmployeeName = d.EmployeeName;
                        empVM.EmployeeEmail = d.EmployeeEmail;
                        empVM.EmployeeAddress = d.EmployeeAddress;
                        empVM.PinCode = d.Pincode;
                        empVM.Gender = d.Gender;
                        empVM.TitleDegrees = d.TitleDegrees;
                        empVM.CreatedDate = d.CreatedDate;
                        empVM.CreatedBy = d.EmployeeName;
                        empVM.ModifiedDate = d.CreatedDate;
                        empVM.ModifiedBy = d.EmployeeName;
                        //if (d.CityID != null || d.CityID == 0) { empVM.CityName = getCitybyId(d.CityID).CityName; }
                        //if (d.ProvinceID != null || d.ProvinceID == 0) { empVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        //if (d.CountryID != null || d.CountryID == 0) { empVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        if (d.CityID != 0)
                        {
                            if (d.CityID != null) { empVM.CityName = getCitybyId(d.CityID).CityName; }
                        }
                        else
                        {
                            empVM.CityName = "";
                        }
                        if (d.ProvinceID != 0)
                        {
                            if (d.ProvinceID != null) { empVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        }
                        else
                        {
                            empVM.ProvianceName = "";
                        }
                        if (d.CountryID != 0)
                        {
                            if (d.CountryID != null) { empVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        }
                        else
                        {
                            empVM.CountryName = "";
                        }
                        empStampingVMList.Add(empVM);
                    }
                    return empStampingVMList;
                }
                return null;
            }
        }

        internal static List<EmployeeSalesViewModel> GetAllSalesPerson()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<EmployeeSalesViewModel> empStampingVMList = new List<EmployeeSalesViewModel>();
                var itm = db.Employees.Where(x => x.IsActive == true).OrderBy(x => x.EmployeeName).ToList();
                if (itm.Count != 0)
                {
                    EmployeeSalesViewModel empVMSelect = new EmployeeSalesViewModel();
                    empVMSelect.UserID = 0;
                    empVMSelect.EmployeeSalesID = 0;
                    empVMSelect.EmployeeSalesName = "-----Select-----";
                    empStampingVMList.Add(empVMSelect);
                    foreach (var d in itm)
                    {
                        EmployeeSalesViewModel empVM = new EmployeeSalesViewModel();
                        empVM.UserID = d.UserID;
                        empVM.EmployeeSalesID = d.EmployeeID;
                        empVM.EmployeeSalesName = d.EmployeeName;
                        empVM.EmployeeEmail = d.EmployeeEmail;
                        empVM.EmployeeAddress = d.EmployeeAddress;
                        empVM.PinCode = d.Pincode;
                        empVM.Gender = d.Gender;
                        empVM.TitleDegrees = d.TitleDegrees;
                        empVM.CreatedDate = d.CreatedDate;
                        empVM.CreatedBy = d.EmployeeName;
                        empVM.ModifiedDate = d.CreatedDate;
                        empVM.ModifiedBy = d.EmployeeName;
                        //if (d.CityID != null || d.CityID == 0) { empVM.CityName = getCitybyId(d.CityID).CityName; }
                        //if (d.ProvinceID != null || d.ProvinceID == 0) { empVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        //if (d.CountryID != null || d.CountryID == 0) { empVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        if (d.CityID != 0)
                        {
                            if (d.CityID != null) { empVM.CityName = getCitybyId(d.CityID).CityName; }
                        }
                        else
                        {
                            empVM.CityName = "";
                        }
                        if (d.ProvinceID != 0)
                        {
                            if (d.ProvinceID != null) { empVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        }
                        else
                        {
                            empVM.ProvianceName = "";
                        }
                        if (d.CountryID != 0)
                        {
                            if (d.CountryID != null) { empVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        }
                        else
                        {
                            empVM.CountryName = "";
                        }
                        var iUser = db.Users.Where(x => x.IsActive == true && x.UserType == 5 && x.UserId == d.UserID).FirstOrDefault();
                        if (iUser != null)
                        {
                            empStampingVMList.Add(empVM);
                        }
                    }
                    return empStampingVMList;
                }
                return null;
            }
        }


        internal static List<UserEmployeeViewModel> getAllEmployeeUser()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<UserEmployeeViewModel> empUserVMList = new List<UserEmployeeViewModel>();
                var itm = db.Employees.Where(x => x.IsActive == true).ToList();
                if (itm.Count != 0)
                {
                    foreach (var d in itm)
                    {
                        UserEmployeeViewModel empUserVM = new UserEmployeeViewModel();
                        var user = db.Users.Where(x => x.UserId == d.UserID).FirstOrDefault();
                        empUserVM.UserName = user.UserName;
                        empUserVM.UserType = user.UserType ?? 0;
                        empUserVM.UserID = d.UserID;
                        empUserVM.EmployeeID = d.EmployeeID;
                        empUserVM.EmployeeName = d.EmployeeName;
                        empUserVM.EmployeeEmail = d.EmployeeEmail;
                        empUserVM.EmployeeAddress = d.EmployeeAddress;
                        empUserVM.PinCode = d.Pincode;
                        empUserVM.Gender = d.Gender;
                        empUserVM.TitleDegrees = d.TitleDegrees;
                        empUserVM.CreatedDate = d.CreatedDate;
                        empUserVM.CreatedBy = d.EmployeeName;
                        //if (d.CityID != null) { empUserVM.CityName = getCitybyId(d.CityID).CityName; }
                        //if (d.ProvinceID != null || d.ProvinceID != 0) { empUserVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        //if (d.CountryID != null || d.CountryID != 0) { empUserVM.CountryName = getCountrybyId(d.CountryID).CountryName; }

                        if (d.CityID != 0)
                        {
                            if (d.CityID != null) { empUserVM.CityName = getCitybyId(d.CityID).CityName; }
                        }
                        else
                        {
                            empUserVM.CityName = "";
                        }
                        if (d.ProvinceID != 0)
                        {
                            if (d.ProvinceID != null) { empUserVM.ProvianceName = getProvincebyId(d.ProvinceID).ProvinceName; }
                        }
                        else
                        {
                            empUserVM.ProvianceName = "";
                        }
                        if (d.CountryID != 0)
                        {
                            if (d.CountryID != null) { empUserVM.CountryName = getCountrybyId(d.CountryID).CountryName; }
                        }
                        else
                        {
                            empUserVM.CountryName = "";
                        }

                        empUserVMList.Add(empUserVM);
                    }
                    return empUserVMList;
                }
                return null;
            }
        }

        internal static UserEmployeeViewModel getUserEmployeeById(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                UserEmployeeViewModel vm = new UserEmployeeViewModel();

                var objEmp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
                if (objEmp != null)
                {
                    vm.EmployeeName = objEmp.EmployeeName;
                    vm.EmployeeEmail = objEmp.EmployeeEmail;
                    vm.EmployeeAddress = objEmp.EmployeeAddress;
                    vm.PinCode = objEmp.Pincode;
                    vm.Gender = objEmp.Gender;
                    vm.MobileNo = objEmp.MobileNo;
                    vm.CityID = objEmp.CityID ?? 0;
                    vm.ProvinceID = objEmp.ProvinceID;
                    vm.CountryID = objEmp.CountryID;
                    vm.TitleDegrees = objEmp.TitleDegrees;
                    vm.IsStampingEngineer = objEmp.IsStampingEngineer;
                    var objUser = db.Users.Where(x => x.UserId == objEmp.UserID).FirstOrDefault();
                    if (objUser != null)
                    {
                        vm.UserID = objUser.UserId;
                        vm.UserName = objUser.UserName;
                        vm.UserType = objUser.UserType ?? 0;
                        vm.UserToken = objUser.DeviceToken;
                    }
                }
                return vm;
            }
        }

        internal static UserEmployeeViewModel getUserEmployeeByUserId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                UserEmployeeViewModel vm = new UserEmployeeViewModel();

                var objEmp = db.Employees.Where(x => x.UserID == id).FirstOrDefault();
                if (objEmp != null)
                {
                    vm.EmployeeName = objEmp.EmployeeName;
                    vm.EmployeeEmail = objEmp.EmployeeEmail;
                    vm.EmployeeAddress = objEmp.EmployeeAddress;
                    vm.PinCode = objEmp.Pincode;
                    vm.Gender = objEmp.Gender;
                    vm.MobileNo = objEmp.MobileNo;
                    vm.CityID = objEmp.CityID ?? 0;
                    vm.ProvinceID = objEmp.ProvinceID;
                    vm.CountryID = objEmp.CountryID;
                    vm.TitleDegrees = objEmp.TitleDegrees;
                    vm.IsStampingEngineer = objEmp.IsStampingEngineer;
                    var objUser = db.Users.Where(x => x.UserId == objEmp.UserID).FirstOrDefault();
                    if (objUser != null)
                    {
                        vm.UserID = objUser.UserId;
                        vm.UserName = objUser.UserName;
                        vm.UserType = objUser.UserType ?? 0;
                        vm.UserToken = objUser.DeviceToken;
                    }
                }
                return vm;
            }
        }

        internal static UserEmployeeViewModel getUserEmployeeDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                UserEmployeeViewModel vm = new UserEmployeeViewModel();
                var objUser = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                var objEmp = db.Employees.Where(x => x.UserID == id).FirstOrDefault();
                if (objUser != null)
                {
                    vm.UserID = objUser.UserId;
                    vm.UserName = objUser.UserName;
                    vm.UserType = objUser.UserType ?? 0;
                }

                vm.EmployeeName = objEmp.EmployeeName;
                vm.EmployeeEmail = objEmp.EmployeeEmail;
                vm.EmployeeAddress = objEmp.EmployeeAddress;
                vm.MobileNo = objEmp.MobileNo;
                vm.PinCode = objEmp.Pincode;
                vm.Gender = objEmp.Gender;
                vm.CityID = objEmp.CityID ?? 0;
                vm.ProvinceID = objEmp.ProvinceID;
                vm.CountryID = objEmp.CountryID;
                if (objEmp.CityID != 0)
                {
                    if (objEmp.CityID != null) { vm.CityName = getCitybyId(objEmp.CityID).CityName; }
                }
                else
                {
                    vm.CityName = "";
                }
                if (objEmp.ProvinceID != 0)
                {
                    if (objEmp.ProvinceID != null) { vm.ProvianceName = getProvincebyId(objEmp.ProvinceID).ProvinceName; }
                }
                else
                {
                    vm.ProvianceName = "";
                }

                if (objEmp.CountryID != 0)
                {
                    if (objEmp.CountryID != null) { vm.CountryName = getCountrybyId(objEmp.CountryID).CountryName; }
                }
                else
                {
                    vm.CountryName = "";
                }
                vm.TitleDegrees = objEmp.TitleDegrees;
                vm.CreatedBy = objEmp.CreatedBy;
                vm.CreatedDate = objEmp.CreatedDate;
                vm.ModifiedBy = objEmp.ModifiedBy;
                vm.ModifiedDate = objEmp.ModifiedDate;
                vm.IsStampingEngineer = objEmp.IsStampingEngineer;
                vm.Active = objEmp.IsActive;
                return vm;
            }
        }

        internal static User getUserbyUserId(long? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static List<User> getAllUserbyUserId(long? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Users.Where(x => x.UserId == id).ToList();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string editUserDeviceToken(User model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var usr = db.Users.Where(x => x.UserId == model.UserId).FirstOrDefault();
                    if (usr != null)
                    {
                        usr.DeviceToken = model.DeviceToken;
                        usr.ModifiedDate = DateTime.Now;
                        usr.ModifiedBy = model.CreatedBy;
                        db.Entry(usr).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Ok";
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //internal static int getAllAdminCountInUser()
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        var itm = db.Users.Where(x => x.UserType == 1).Count();
        //        if (itm != 0) { return itm; }
        //        return 0;
        //    }
        //}
        //internal static int getInventoryCount()
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        var itm = db.ComponentPriceLists.Where(x => x.IsActive == true).Count();
        //        if (itm != 0) { return itm; }
        //        return 0;
        //    }
        //}

        //internal static int getAllEmployeeCountInUser()
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        var itm = db.Users.Where(x => x.UserType == 2).Count();
        //        if (itm != 0) { return itm; }
        //        return 0;
        //    }
        //}

        internal static Employee getEmployeeById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveUserEmployee(UserEmployeeViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    User usr = new User();
                    usr.UserName = model.UserName;
                    usr.UserPassword = AccountController.MD5Hash(model.UserPassword);
                    usr.IsActive = true;
                    usr.UserType = model.UserType;
                    usr.CreatedDate = DateTime.Now;
                    usr.CreatedBy = model.CreatedBy;
                    usr.ModifiedDate = DateTime.Now;
                    usr.ModifiedBy = model.CreatedBy;
                    db.Users.Add(usr);
                    db.SaveChanges();

                    Employee emp = new Employee();
                    emp.EmployeeName = model.EmployeeName;
                    emp.EmployeeEmail = model.EmployeeEmail;
                    emp.EmployeeAddress = model.EmployeeAddress;
                    emp.CityID = model.CityID;
                    emp.CountryID = model.CountryID;
                    emp.ProvinceID = model.ProvinceID;
                    emp.TitleDegrees = model.TitleDegrees;
                    emp.IsActive = true;
                    emp.Gender = model.Gender;
                    emp.Pincode = model.PinCode;
                    emp.UserID = usr.UserId;
                    emp.CreatedBy = model.CreatedBy;
                    emp.CreatedDate = DateTime.Now;
                    emp.ModifiedBy = model.CreatedBy;
                    emp.ModifiedDate = DateTime.Now;
                    emp.IsStampingEngineer = model.IsStampingEngineer;
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editUserEmployee(UserEmployeeViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var usr = db.Users.Where(x => x.UserId == model.UserID).FirstOrDefault();
                    if (usr != null)
                    {

                        usr.UserName = model.UserName;
                        usr.IsActive = true;
                        usr.UserType = model.UserType;
                        db.Entry(usr).State = EntityState.Modified;
                        db.SaveChanges();

                        var emp = db.Employees.Where(x => x.UserID == model.UserID).FirstOrDefault();
                        emp.EmployeeName = model.EmployeeName;
                        emp.EmployeeEmail = model.EmployeeEmail;
                        emp.EmployeeAddress = model.EmployeeAddress;
                        emp.CityID = model.CityID;
                        emp.CountryID = model.CountryID;
                        emp.ProvinceID = model.ProvinceID;
                        emp.TitleDegrees = model.TitleDegrees;
                        emp.IsActive = true;
                        emp.Gender = model.Gender;
                        emp.Pincode = model.PinCode;
                        emp.UserID = usr.UserId;
                        emp.ModifiedBy = model.ModifiedBy;
                        emp.ModifiedDate = DateTime.Now;
                        emp.IsStampingEngineer = model.IsStampingEngineer;
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Ok";
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeUserEmployee(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                UserEmployeeViewModel vm = new UserEmployeeViewModel();
                var itm = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                itm.IsActive = false;
                db.SaveChanges();
                var list = db.Employees.Where(x => x.UserID == id).FirstOrDefault();
                list.IsActive = false;
                db.SaveChanges();
                return "Ok";
            }
        }

        internal static string empEditMyProfile(UserEmployeeViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
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
                        emp.ModifiedBy = model.ModifiedBy;
                        emp.ModifiedDate = DateTime.Now;
                        //emp.IsStampingEngineer = model.IsStampingEngineer;
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Ok";
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editPassword(User model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var user = db.Users.Where(x => x.UserId == model.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        user.UserName = model.UserName;
                        if (model.UserPassword != null) { user.UserPassword = AccountController.MD5Hash(model.UserPassword); }
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        if (user.UserType == 4)
                        {
                            var cust = db.Customers.Where(x => x.UserID == model.UserId).FirstOrDefault();
                            cust.CustomerPharse = model.UserPassword.Trim();
                            cust.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            cust.ModifiedDate = DateTime.Now;
                            db.Entry(cust).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return "Ok";
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editEmployeePasswordByAdmin(UserEmployeeViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var userId = db.Users.Where(x => x.UserId == model.UserID).FirstOrDefault();
                    if (userId != null)
                    {
                        var user = db.Users.Where(x => x.UserId == userId.UserId).FirstOrDefault();
                        if (user != null)
                        {
                            //user.UserName = model.UserName;
                            if (model.UserPassword != null)
                            {
                                user.UserPassword = AccountController.MD5Hash(model.UserPassword);
                            }
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();
                            return "Ok";
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editCustomerPasswordByAdmin(User model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var user = db.Users.Where(x => x.UserId == model.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        //user.UserName = model.UserName;
                        if (model.UserPassword != null)
                        {
                            user.UserPassword = AccountController.MD5Hash(model.UserPassword);
                        }
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();

                        var cust = db.Customers.Where(x => x.UserID == model.UserId).FirstOrDefault();
                        cust.CustomerPharse = model.UserPassword.Trim();
                        cust.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        cust.ModifiedDate = DateTime.Now;
                        db.Entry(cust).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Ok";
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<CityViewModel> getAllCities()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var result = (from city in db.Cities
                              join province in db.Provinces
                              on city.ProvinceID equals province.ProvinceID
                              where city.IsActive == true
                              orderby city.CityName
                              select new CityViewModel
                              {
                                  CityID = city.CityID,
                                  CityName = city.CityName,
                                  ProvinceID = city.ProvinceID,
                                  ProvinceName = province.ProvinceName
                              }).ToList();

                return result;
            }
        }

        internal static City getCitybyId(int? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Cities.Where(x => x.CityID == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static List<City> getCitybyProvinceId(int? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Cities.Where(x => x.ProvinceID == id && x.IsActive == true).OrderBy(x => x.CityName).ToList();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static List<CustomerLocation> GetLocationbyCityIdByCustomer(long? id)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var cust = db.Customers.FirstOrDefault(x => x.UserID == userId);
                        if (cust != null)
                        {
                            long customerId = cust.CustomerId;

                            var itm = db.CustomerLocations.Where(cl => cl.CustomerId == customerId && cl.CityID == id).ToList();

                            if (itm != null)
                            {
                                return itm;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

                throw;
            }
        }
        internal static List<CustomerLocation> GetLocationbyByCustomer()
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var cust = db.Customers.FirstOrDefault(x => x.UserID == userId);
                        if (cust != null)
                        {
                            long customerId = cust.CustomerId;

                            var itm = db.CustomerLocations.Where(cl => cl.CustomerId == customerId).ToList();

                            if (itm != null)
                            {
                                return itm;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

                throw;
            }
        }

        internal static List<CustomerLocation> GetLocationbyRegionByCustomer(string region)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var cust = db.Customers.FirstOrDefault(x => x.UserID == userId);
                        if (cust != null)
                        {
                            long customerId = cust.CustomerId;

                            var itm = db.CustomerLocations.Where(cl => cl.CustomerId == customerId && cl.Region == region).ToList();

                            if (itm != null)
                            {
                                return itm;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

                throw;
            }
        }

        //
        internal static List<City> GetCitybyProvinceIdByCustomer(int? id)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var cust = db.Customers.FirstOrDefault(x => x.UserID == userId);
                        if (cust != null)
                        {
                            long customerId = cust.CustomerId;

                            var itm = db.Cities
                                .Join(
                                    db.CustomerLocations,
                                    city => city.CityID,
                                    custInner => custInner.CityID,
                                    (City, custInner) => new { City, custInner }
                                )
                                .Where(joined => joined.custInner.CustomerId == customerId && joined.City.ProvinceID == id)
                                .Select(joined => joined.City)
                                .Distinct()
                                .ToList();

                            if (itm != null)
                            {
                                return itm;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

                throw;
            }
        }
        internal static List<City> GetCitybyByCustomer()
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var cust = db.Customers.FirstOrDefault(x => x.UserID == userId);
                        if (cust != null)
                        {
                            long customerId = cust.CustomerId;

                            var itm = db.Cities
                                .Join(
                                    db.CustomerLocations,
                                    city => city.CityID,
                                    custInner => custInner.CityID,
                                    (City, custInner) => new { City, custInner }
                                )
                                .Where(joined => joined.custInner.CustomerId == customerId)
                                .Select(joined => joined.City)
                                .Distinct()
                                .ToList();

                            if (itm != null)
                            {
                                return itm;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

                throw;
            }
        }

        internal static List<Province> getAllProvinces()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Provinces.OrderBy(x => x.ProvinceName).OrderBy(x => x.ProvinceName).ToList();
                return itm;
            }
        }

        internal static Province getProvincebyId(int? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Provinces.Where(x => x.ProvinceID == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static List<Province> getProvincebyCountryId(int? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Provinces.Where(x => x.CountryID == id).ToList();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static List<Province> GetProvincebyCountryIdByCustomer()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        var provinceIds = db.Customers.Where(c => c.CustomerId == customer.CustomerId && c.ProvinceID != null).Select(c => c.ProvinceID)
                        .Union(db.CustomerLocations.Where(cl => cl.CustomerId == customer.CustomerId && cl.ProvinceID != null).Select(cl => cl.ProvinceID)).Distinct();
                        var itm = db.Provinces
                            .Where(p => provinceIds.Contains(p.ProvinceID))
                            .ToList();
                        //var itm = db.Provinces.Where(x => x.CountryID == id).ToList();
                        if (itm != null) { return itm; }
                    }
                }

                return null;
            }
        }

        internal static List<CustomerRegion> GetRegionbyCustomer()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        var itm = db.CustomerLocations.Where(cl => cl.CustomerId == customer.CustomerId && cl.Region != null).Select(cl => new CustomerRegion { CustRegion = cl.Region }).Distinct().ToList();
                        //var itm = db.Provinces.Where(x => x.CountryID == id).ToList();
                        if (itm != null) { return itm; }
                    }
                }
                return null;
            }
        }

        internal static List<Country> getAllCountries()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Countries.Where(x => x.IsActive == true).OrderBy(x => x.CountryName).ToList();
                return itm;
            }
        }

        internal static Country getCountrybyId(int? id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Countries.Where(x => x.CountryID == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static async Task<string> saveImages(Images_Upload model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    Images_Upload img = new Images_Upload();
                    long id = 0;
                    var itm = db.Images_Upload.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    if (itm != null) { id = itm.ImageID + 1 ?? 0; }
                    foreach (var d in model.imgList)
                    {
                        img.ImageID = id;
                        img.Images = "img/items/" + d.filename;
                        img.CreatedBy = model.CreatedBy;
                        img.CreatedDate = DateTime.Now;
                        img.ModifiedBy = model.CreatedBy;
                        img.ModifiedDate = DateTime.Now;
                        db.Images_Upload.Add(img);
                        db.SaveChanges();
                    }

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static UserEmployeeViewModel mobileLogin(LoginViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                model.UserPassword = AccountController.MD5Hash(model.UserPassword);
                var user = db.Users.Where(x => x.UserName == model.UserName && x.UserPassword == model.UserPassword && x.IsActive == true).FirstOrDefault();
                if (user != null)
                {
                    UserEmployeeViewModel objEmployeeViewModel = new UserEmployeeViewModel();
                    var emp = db.Employees.Where(x => x.UserID == user.UserId).FirstOrDefault();
                    emp.UserStatus = true;
                    var token = TokenManager.GenerateToken(emp.EmployeeEmail);
                    emp.UserToken = token;
                    objEmployeeViewModel.EmployeeID = emp.EmployeeID;
                    objEmployeeViewModel.UserName = user.UserName;
                    objEmployeeViewModel.UserID = emp.UserID;
                    objEmployeeViewModel.UserType = user.UserType;
                    objEmployeeViewModel.EmployeeEmail = emp.EmployeeEmail;
                    objEmployeeViewModel.EmployeeName = emp.EmployeeName;
                    objEmployeeViewModel.EmployeeAddress = emp.EmployeeAddress;
                    objEmployeeViewModel.UserStatus = true;
                    objEmployeeViewModel.UserToken = token;
                    objEmployeeViewModel.CityID = emp.CityID;
                    objEmployeeViewModel.ProvinceID = emp.ProvinceID;
                    objEmployeeViewModel.CountryID = emp.CountryID;
                    objEmployeeViewModel.PinCode = emp.Pincode;
                    objEmployeeViewModel.IsActive = emp.IsActive;
                    objEmployeeViewModel.Gender = emp.Gender;
                    objEmployeeViewModel.TitleDegrees = emp.TitleDegrees;
                    objEmployeeViewModel.CreatedDate = emp.CreatedDate;
                    objEmployeeViewModel.CreatedBy = emp.CreatedBy;
                    objEmployeeViewModel.ModifiedDate = emp.ModifiedDate;
                    objEmployeeViewModel.ModifiedBy = emp.ModifiedBy;
                    objEmployeeViewModel.MobileNo = emp.MobileNo;
                    return objEmployeeViewModel;
                }
                else
                {
                    return null;
                }
            }
        }

        internal static List<User> getAllCustomersByUserType()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var emp = db.Users.Where(x => x.UserType == 3).OrderByDescending(x => x.CreatedDate).ToList();
                return emp;
            }
        }

        internal static List<CustomerViewModel> getAllCustomers()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);

                List<CustomerViewModel> custList = new List<CustomerViewModel>();
                var customer = db.Customers.Where(x => x.IsActive == true).OrderBy(x => x.CustomerName).ToList();
                if (customer.Count != 0)
                {
                    foreach (var d in customer)
                    {
                        CustomerViewModel cust = new CustomerViewModel();
                        cust.CustomerID = d.CustomerId;
                        cust.UserID = d.UserID ?? 0;
                        cust.CustomerName = d.CustomerName;
                        cust.CustomerNAVNo = d.CustomerNAVNo;
                        if (d.CustomerLogo != null)
                        {
                            cust.CustomerLogo = d.CustomerLogo;
                        }
                        else
                        {
                            cust.CustomerLogo = "/defaultcompany.png";
                        }
                        cust.CustomerAddress = d.CustomerAddress;
                        cust.CustomerPharse = d.CustomerPharse;
                        cust.PinCode = d.Pincode;
                        if (d.CityID != null) { cust.CityID = getCitybyId(d.CityID).CityName; }
                        if (d.ProvinceID != null) { cust.ProvinceID = getProvincebyId(d.ProvinceID).ProvinceName; }
                        if (d.CountryID != null) { cust.Country = getCountrybyId(d.CountryID).CountryName; }
                        custList.Add(cust);
                    }
                    return custList;
                }
                return null;
            }
        }

        internal static Int32 getAllCustomersCount()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                int custCount = 0;
                var customer = db.Customers.Where(x => x.IsActive == true).OrderBy(x => x.CustomerName).ToList();
                if (customer.Count != 0)
                {
                    custCount = customer.Count;
                }
                return custCount;
            }
        }


        internal static List<CustomerViewModel> getAllCustomersByEmployeeId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerViewModel> custList = new List<CustomerViewModel>();
                var id = Convert.ToInt16(HttpContext.Current.Session["LoggedInUserId"]);
                var customer = db.Customers.Where(x => x.IsActive == true && x.UserID == id).OrderBy(x => x.CustomerName).ToList();
                if (customer.Count != 0)
                {
                    foreach (var d in customer)
                    {
                        CustomerViewModel cust = new CustomerViewModel();
                        cust.CustomerID = d.CustomerId;
                        cust.UserID = d.UserID ?? 0;
                        cust.CustomerName = d.CustomerName;
                        cust.CustomerNAVNo = d.CustomerNAVNo;
                        cust.CustomerLogo = d.CustomerLogo;
                        cust.CustomerAddress = d.CustomerAddress;
                        cust.CustomerPharse = d.CustomerPharse;
                        cust.PinCode = d.Pincode;
                        if (d.CityID != null) { cust.CityID = getCitybyId(d.CityID).CityName; }
                        if (d.ProvinceID != null) { cust.ProvinceID = getProvincebyId(d.ProvinceID).ProvinceName; }
                        if (d.CountryID != null) { cust.Country = getCountrybyId(d.CountryID).CountryName; }
                        custList.Add(cust);
                    }
                    return custList;
                }
                return null;
            }
        }

        internal static Customer getCustomerById(long? id)
        {
            //string host = HttpContext.Current.Request.Url.Host;
            string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
            Uri url = new Uri(tmpURL);
            string host = url.GetLeftPart(UriPartial.Authority);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var customer = db.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
                if (customer != null)
                {
                    if (customer.CustomerLogo != null)
                    {
                        customer.CustomerLogo = host + "/img/logos/" + customer.CustomerLogo.Trim();
                    }
                    else
                    {
                        customer.CustomerLogo = host + "/img/logos/defaultcompany.png";
                    }
                    return customer;
                }
                return null;
            }
        }

        internal static CustomerViewModel getCustomerDetailsById(int id)
        {
            string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
            Uri url = new Uri(tmpURL);
            string host = url.GetLeftPart(UriPartial.Authority);

            using (DatabaseEntities db = new DatabaseEntities())
            {
                var customer = db.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
                if (customer != null)
                {
                    CustomerViewModel custDetails = new CustomerViewModel();
                    custDetails.CustomerID = customer.CustomerId;
                    custDetails.CustomerName = customer.CustomerName.Trim();
                    if (customer.CustomerLogo != null)
                    {
                        custDetails.CustomerLogo = customer.CustomerLogo.Trim();
                        custDetails.CustomerFullPathLogo = host + "/img/logos/" + customer.CustomerLogo.Trim();
                    }
                    else
                    {
                        custDetails.CustomerLogo = "defaultcompany.png";
                        custDetails.CustomerFullPathLogo = host + "/img/logos/defaultcompany.png";
                    }

                    if (customer.CustomerContactName != null) { custDetails.CustomerContactName = customer.CustomerContactName.Trim(); }
                    if (customer.CustomerNAVNo != null)
                    { custDetails.CustomerNAVNo = customer.CustomerNAVNo.Trim(); }
                    if (customer.CustomerAddress != null)
                    { custDetails.CustomerAddress = customer.CustomerAddress.Trim(); }
                    if (customer.CustomerPhone != null) { custDetails.CustomerPhone = customer.CustomerPhone.Trim(); }
                    if (customer.CustomerEmail != null) { custDetails.CustomerEmail = customer.CustomerEmail.Trim(); }
                    if (customer.Pincode != null) { custDetails.PinCode = customer.Pincode.Trim(); }
                    custDetails.CreatedDate = customer.CreatedDate;
                    custDetails.CreatedBy = customer.CreatedBy;
                    if (customer.CityID != null) { custDetails.CityID = getCitybyId(customer.CityID).CityName; }
                    if (customer.ProvinceID != null) { custDetails.ProvinceID = getProvincebyId(customer.ProvinceID).ProvinceName; }
                    if (customer.CountryID != null) { custDetails.Country = getCountrybyId(customer.CountryID).CountryName; }
                    if (customer.CustomerPharse != null)
                    { custDetails.CustomerPharse = customer.CustomerPharse.Trim(); }
                    return custDetails;
                }
                return null;
            }
        }

        internal static CustAndCustomerLocationViewModel getCustAndCustLocationDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);
                CustAndCustomerLocationViewModel cust = new CustAndCustomerLocationViewModel();
                var customer = db.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
                var customerL = db.CustomerLocations.Where(x => x.CustomerId == id).ToList();
                if (customer != null)
                {
                    cust.CustomerID = customer.CustomerId;
                    cust.CustomerName = customer.CustomerName;
                    cust.CustomerNAVNo = customer.CustomerNAVNo;
                    cust.CustomerPhone = customer.CustomerPhone;
                    cust.CustomerEmail = customer.CustomerEmail;
                    cust.CustomerWebsite = customer.CustomerWebsite;
                    if (customer.CustomerLogo != null)
                    {
                        cust.CustomerLogo = host + "/img/logos/" + customer.CustomerLogo.Trim();
                    }
                    else
                    {
                        cust.CustomerLogo = host + "/img/logos/defaultcompany.png";
                    }
                    cust.CustomerAddress = customer.CustomerAddress;
                    cust.PinCode = customer.Pincode;
                    cust.CreatedDate = customer.CreatedDate;
                    cust.CreatedBy = customer.CreatedBy;
                    cust.CityID = customer.CityID;
                    cust.ProvinceID = customer.ProvinceID;
                    cust.Country = customer.CountryID;
                    cust.CustomerContactName = customer.CustomerContactName;
                    cust.CustomerPharse = customer.CustomerPharse;
                    if (customer.UserID != 0)
                    {
                        cust.UserName = getUserbyUserId(customer.UserID).UserName;
                    }
                }
                return cust;
            }
        }



        internal static string saveCustomer(Customer model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {

                    User usr = new User();
                    if (model.user.UserName != null && model.user.UserPassword != null)
                    {
                        usr.UserName = model.user.UserName;
                        usr.UserPassword = AccountController.MD5Hash(model.user.UserPassword);
                        usr.IsActive = true;
                        usr.UserType = 4; // Customer
                        usr.CreatedDate = DateTime.Now;
                        usr.CreatedBy = model.CreatedBy;
                        usr.ModifiedDate = DateTime.Now;
                        usr.ModifiedBy = model.CreatedBy;
                        db.Users.Add(usr);
                        db.SaveChanges();
                    }

                    Customer cust = new Customer();
                    cust.CustomerId = model.CustomerId;
                    cust.UserID = usr.UserId;
                    cust.CustomerName = model.CustomerName;
                    cust.CustomerAddress = model.CustomerAddress;
                    cust.CustomerNAVNo = model.CustomerNAVNo;
                    if (model.user.UserName != null && model.user.UserPassword != null)
                    {
                        cust.CustomerPharse = model.user.UserPassword;
                    }
                    cust.CustomerPhone = model.CustomerPhone;
                    cust.CustomerEmail = model.CustomerEmail;
                    cust.CustomerWebsite = model.CustomerWebsite;
                    cust.CountryID = model.CountryID;
                    cust.CityID = model.CityID;
                    cust.ProvinceID = model.ProvinceID;
                    cust.Pincode = model.Pincode;
                    cust.IsActive = true;
                    cust.CustomerLogo = model.CustomerLogo;
                    cust.CustomerContactName = model.CustomerContactName;
                    cust.CreatedDate = DateTime.Now;
                    cust.CreatedBy = model.CreatedBy;
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = model.CreatedBy;
                    db.Customers.Add(cust);
                    db.SaveChanges();

                    CustomerUser cUser = new CustomerUser();
                    cUser.UserId = usr.UserId;
                    cUser.CustomerId = cust.CustomerId;
                    cUser.IsActive = true;
                    cUser.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    cUser.CreatedDate = DateTime.Now;
                    cUser.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    cUser.ModifiedDate = DateTime.Now;
                    db.CustomerUsers.Add(cUser);
                    db.SaveChanges();
                    return cust.CustomerId.ToString();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editCustomer(Customer model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                    Uri url = new Uri(tmpURL);
                    string host = url.GetLeftPart(UriPartial.Authority);

                    var customer = db.Customers.Where(x => x.CustomerId == model.CustomerId).FirstOrDefault();
                    if (customer != null)
                    {
                        var _user = db.Users.Where(x => x.UserId == customer.UserID && x.IsActive == true).FirstOrDefault();
                        if (_user != null)
                        {
                            _user.UserName = model.user.UserName;
                            //_user.UserPassword = AccountController.MD5Hash(model.user.UserPassword);
                            _user.ModifiedDate = DateTime.Now;
                            _user.ModifiedBy = model.ModifiedBy;
                            db.Entry(_user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        customer.CustomerName = model.CustomerName;
                        customer.CustomerAddress = model.CustomerAddress;
                        customer.CustomerPhone = model.CustomerPhone;
                        customer.CustomerEmail = model.CustomerEmail;
                        customer.CustomerWebsite = model.CustomerWebsite;
                        customer.CustomerContactName = model.CustomerContactName;
                        customer.CountryID = model.CountryID;
                        customer.CityID = model.CityID;
                        customer.ProvinceID = model.ProvinceID;
                        customer.Pincode = model.Pincode;
                        customer.CustomerNAVNo = model.CustomerNAVNo;
                        if (model.CustomerLogo != null)
                        {
                            //customer.CustomerLogo = host + "/img/logos/" + model.CustomerLogo.Trim();
                            customer.CustomerLogo = model.CustomerLogo.Trim();
                        }
                        else
                        {
                            //customer.CustomerLogo = host + "/img/logos/defaultcompany.png";
                        }

                        //if (model.CustomerLogo != null) { customer.CustomerLogo = model.CustomerLogo; }
                        customer.IsActive = true;
                        customer.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        customer.ModifiedDate = DateTime.Now;
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";


                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }
        internal static string sendPassword(int CustomerID)
        {
            string strReturn = "Ok";
            var cust = getCustomerById(CustomerID);
            if (cust != null)
            {
                string strMSG = "";
                string strCustomerEmail = "", strCustomerName = "", strUsername = "";

                strCustomerName = cust.CustomerName;
                strCustomerEmail = cust.CustomerEmail;
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var _user = db.Users.Where(x => x.UserId == cust.UserID).FirstOrDefault();
                    if (_user != null)
                    {
                        strUsername = _user.UserName;
                    }
                }
                //strCustomerEmail = "nirav.m@siliconinfo.com";
                if (!string.IsNullOrEmpty(strCustomerEmail) && !string.IsNullOrEmpty(strUsername) && !string.IsNullOrEmpty(cust.CustomerPharse))
                {
                    strMSG = "";
                    strMSG = "<html>";
                    strMSG += "<head>";
                    strMSG += "<style>";
                    strMSG += "p{margin:0px}";
                    strMSG += "</style>";
                    strMSG += "</head>";
                    strMSG += "<body>";
                    strMSG += "<div style='width: 100%; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                    strMSG += "<br/>";
                    strMSG += "<p> Attention " + strCustomerName + ",";
                    strMSG += "<br/>";
                    strMSG += "<br/>";
                    strMSG += "<p>You can access and review the status of the racking inspection, as well as the final report (once it is completed), on the Rack Auditor platform <a href='https://rack-manager.com/' target='_blank'>(rack-manager.com)</a></p>";
                    strMSG += "<p>Following are the credentials to access the Rack Auditor platform.</p>";
                    strMSG += "<p>Username : " + strUsername + "</p>";
                    strMSG += "<p>Password : " + cust.CustomerPharse + "</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>Once you login using your credentials, you can change your password by going to \"Manage Password\" </p>";
                    strMSG += "<p><span><img src = 'https://rack-manager.com/img/ManagePassword.png' /></span></p>";
                    strMSG += "<br/>";
                    //strMSG += "<p>Please feel free to call +1 800 772 3213 or email the assigned engineer in case rescheduling is required.</p>";
                    //strMSG += "<p>We look forward to working with you soon.</p>";
                    strMSG += "</div></div><br/><br/><div><div>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Best regards,</span></p>";
                    strMSG += "<br/>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                    strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSG += "<br/>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                    strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
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
                    var tEmailCust = new Thread(() => EmailHelper.SendEmail(strCustomerEmail, "Your credentials to access the Rack Auditor platform.", null, strMSG, null));
                    tEmailCust.Start();
                }
                else
                {
                    strReturn = "Information is missing from customer. Please update email, username, password in customer.";
                }
            }
            return strReturn;
        }

        internal static string removeCustomer(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return "Ok";
            }
        }

        internal static List<CustomerLocationViewModel> getAllCustomerLocations()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerLocationViewModel> custList = new List<CustomerLocationViewModel>();
                var customer = db.CustomerLocations.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate).ToList();
                if (customer.Count != 0)
                {
                    foreach (var d in customer)
                    {
                        CustomerLocationViewModel cust = new CustomerLocationViewModel();
                        cust.CustomerLocationID = d.CustomerLocationID;
                        cust.CustomerID = d.CustomerId;
                        cust.LocationName = d.LocationName;
                        cust.CustomerAddress = d.CustomerAddress;
                        cust.PinCode = d.Pincode;
                        if (d.Region != null)
                        {
                            cust.Region = d.Region;
                        }
                        else
                        {
                            cust.Region = "";
                        }
                        cust.CreatedDate = d.CreatedDate;
                        cust.CreatedBy = d.CreatedBy;
                        if (d.CityID != null) { cust.City = getCitybyId(d.CityID).CityName; }
                        if (d.ProvinceID != null) { cust.Province = getProvincebyId(d.ProvinceID).ProvinceName; }
                        if (d.CountryID != null) { cust.Country = getCountrybyId(d.CountryID).CountryName; }
                        custList.Add(cust);
                    }
                    return custList;
                }
                return null;
            }
        }

        internal static CustomerLocation getCustomerLocationById(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {

                var customer = db.CustomerLocations.Where(x => x.CustomerLocationID == id).FirstOrDefault();
                if (customer != null)
                {
                    return customer;
                }
                return null;
            }
        }

        internal static CustomerLocationViewModel getCustomerLocationDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var customer = db.CustomerLocations.Where(x => x.CustomerLocationID == id && x.IsActive == true).FirstOrDefault();
                if (customer != null)
                {
                    CustomerLocationViewModel custLoc = new CustomerLocationViewModel();
                    custLoc.CustomerLocationID = customer.CustomerLocationID;
                    custLoc.CustomerName = getCustomerById(customer.CustomerId).CustomerName;
                    custLoc.LocationName = customer.LocationName;
                    custLoc.CustomerAddress = customer.CustomerAddress;
                    if (customer.Region != null)
                    {
                        custLoc.Region = customer.Region;
                    }
                    else
                    {
                        custLoc.Region = "";
                    }
                    custLoc.PinCode = customer.Pincode;
                    custLoc.CreatedDate = customer.CreatedDate;
                    custLoc.CreatedBy = customer.CreatedBy;
                    if (customer.CityID != null) { custLoc.City = getCitybyId(customer.CityID).CityName; }
                    if (customer.ProvinceID != null) { custLoc.Province = getProvincebyId(customer.ProvinceID).ProvinceName; }
                    if (customer.CountryID != null) { custLoc.Country = getCountrybyId(customer.CountryID).CountryName; }
                    return custLoc;
                }
                return null;
            }
        }

        internal static List<CustomerLocationViewModel> getCustomerLocationByCustomerId()
        {
            List<CustomerLocationViewModel> custLocList = new List<CustomerLocationViewModel>();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId && x.IsActive == true).FirstOrDefault();
                    if (customer != null)
                    {
                        custLocList = getCustomerLocationByCustomerIdCommon(customer.CustomerId);
                    }
                }
                return custLocList;
            }
        }
        internal static List<CustomerLocationViewModel> getCustomerLocationByCustomerId(long id)
        {
            List<CustomerLocationViewModel> custLocList = new List<CustomerLocationViewModel>();
            custLocList = getCustomerLocationByCustomerIdCommon(id);
            return custLocList;
        }

        internal static List<CustomerLocationViewModel> getCustomerLocationByCustomerIdCommon(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerLocationViewModel> custLocList = new List<CustomerLocationViewModel>();
                var customer = db.CustomerLocations.Where(x => x.IsActive == true && x.CustomerId == id).OrderByDescending(x => x.LocationName).ToList();
                if (customer.Count() != 0)
                {
                    foreach (var d in customer)
                    {
                        CustomerLocationViewModel custLoc = new CustomerLocationViewModel();
                        custLoc.CustomerLocationID = d.CustomerLocationID;
                        custLoc.CustomerName = getCustomerById(d.CustomerId).CustomerName;
                        custLoc.LocationName = d.LocationName;
                        custLoc.CustomerAddress = d.CustomerAddress;
                        if (d.Region != null)
                        {
                            custLoc.Region = d.Region;
                        }
                        else
                        {
                            custLoc.Region = "-";
                        }
                        custLoc.Region = d.Region;
                        custLoc.PinCode = d.Pincode;
                        custLoc.CreatedDate = d.CreatedDate;
                        custLoc.CreatedBy = d.CreatedBy;
                        if (d.CityID != null) { custLoc.City = getCitybyId(d.CityID).CityName; }
                        if (d.ProvinceID != null) { custLoc.Province = getProvincebyId(d.ProvinceID).ProvinceName; }
                        if (d.CountryID != null) { custLoc.Country = getCountrybyId(d.CountryID).CountryName; }
                        custLocList.Add(custLoc);
                    }
                    return custLocList;
                }
                return null;
            }
        }

        internal static List<CustomerLocationViewModel> getCustomerLocationByUserId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId && x.IsActive == true).FirstOrDefault();
                    if (customer != null)
                    {
                        var cLocation = getCustomerLocationByCustomerId(Convert.ToInt16(customer.CustomerId));
                        return cLocation;
                    }
                }
                return null;
            }
        }

        internal static string saveCustomerLocation(CustomerLocation model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    CustomerLocation custLoc = new CustomerLocation();
                    custLoc.CustomerId = model.CustomerId;
                    custLoc.CustomerAddress = model.CustomerAddress;
                    custLoc.CountryID = model.CountryID;
                    custLoc.CityID = model.CityID;
                    custLoc.ProvinceID = model.ProvinceID;
                    custLoc.Pincode = model.Pincode;
                    custLoc.Region = model.Region;
                    custLoc.IsActive = true;
                    custLoc.LocationName = model.LocationName;
                    custLoc.CreatedDate = DateTime.Now;
                    custLoc.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    custLoc.ModifiedDate = DateTime.Now;
                    custLoc.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.CustomerLocations.Add(custLoc);
                    db.SaveChanges();
                    return model.CustomerId.ToString();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editCustomerLocation(CustomerLocation model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    long customrId = 0;
                    var custLoc = db.CustomerLocations.Where(x => x.CustomerLocationID == model.CustomerLocationID).FirstOrDefault();
                    if (custLoc != null)
                    {
                        custLoc.CustomerId = model.CustomerId;
                        custLoc.CustomerAddress = model.CustomerAddress;
                        custLoc.CountryID = model.CountryID;
                        custLoc.CityID = model.CityID;
                        custLoc.ProvinceID = model.ProvinceID;
                        custLoc.Pincode = model.Pincode;
                        custLoc.IsActive = true;
                        custLoc.LocationName = model.LocationName;
                        custLoc.Region = model.Region;
                        //custLoc.CreatedDate = model.CreatedDate;
                        //custLoc.CreatedBy = model.CreatedBy;
                        custLoc.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        custLoc.ModifiedDate = DateTime.Now;
                        db.Entry(custLoc).State = EntityState.Modified;
                        db.SaveChanges();
                        customrId = model.CustomerId;
                    }
                    return customrId.ToString();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeCustomerLocation(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                CustomerLocation vm = new CustomerLocation();
                var itm = db.CustomerLocations.Where(x => x.CustomerLocationID == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return "Ok";
            }
        }

        internal static List<CustomerArea> getAllCustomerArea()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var custArea = db.CustomerAreas.OrderBy(x => x.CreatedDate).ToList();
                if (custArea.Count != 0)
                {
                    return custArea;
                }
                return null;
            }
        }

        internal static CustomerArea getAreaDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var _area = db.CustomerAreas.Where(x => x.AreaID == id && x.IsActive == true).FirstOrDefault();
                if (_area != null)
                {
                    return _area;
                }
                return null;
            }
        }

        internal static InspectionType getInspectionTypeByCode(string InspectionTypeCode)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var _InspectionType = db.InspectionTypes.Where(x => x.InspectionTypeCode == InspectionTypeCode).FirstOrDefault();
                if (_InspectionType != null)
                {
                    return _InspectionType;
                }
                return null;
            }
        }

        internal static List<CustomerAreaViewModel> getAreaDetailsByLocationId(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerAreaViewModel> list = new List<CustomerAreaViewModel>();
                var _area = db.CustomerAreas.Where(x => x.CustomerLocationID == id && x.IsActive == true).OrderBy(x => x.AreaName).ToList();
                if (_area.Count != 0)
                {
                    foreach (var d in _area)
                    {
                        CustomerAreaViewModel _cArea = new CustomerAreaViewModel();
                        _cArea.AreaID = d.AreaID;
                        _cArea.AreaName = d.AreaName;
                        _cArea.CustomerID = d.CustomerID;
                        _cArea.Customer = getCustomerById(d.CustomerID).CustomerName;
                        _cArea.CustomerLocation = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationID)).LocationName;
                        _cArea.CreatedDate = d.CreatedDate;
                        list.Add(_cArea);
                    }
                    return list;
                }
                return null;
            }
        }

        internal static long saveCustomerArea(CustomerArea model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                CustomerArea area = new CustomerArea();
                area.AreaName = model.AreaName;
                area.CustomerLocationID = model.CustomerLocationID;
                area.CustomerID = getCustomerLocationById(Convert.ToInt16(model.CustomerLocationID)).CustomerId;
                area.IsActive = true;
                area.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                area.CreatedDate = DateTime.Now;
                area.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                area.ModifiedDate = DateTime.Now;
                db.CustomerAreas.Add(area);
                db.SaveChanges();
                return model.CustomerLocationID;
            }
        }

        internal static int editCustomerArea(CustomerArea model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var area = db.CustomerAreas.Where(x => x.AreaID == model.AreaID).FirstOrDefault();
                    if (area != null)
                    {
                        area.AreaName = model.AreaName;
                        area.IsActive = true;
                        area.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        area.ModifiedDate = DateTime.Now;
                        db.Entry(area).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        internal static long removeCustomerArea(int id)
        {
            using (DatabaseEntities
                db = new DatabaseEntities())
            {
                var itm = db.CustomerAreas.Where(x => x.AreaID == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return itm.CustomerLocationID;
                }
                return 0;
            }
        }

        internal static CustomerLocationContact getLocationContactDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var _area = db.CustomerLocationContacts.Where(x => x.LocationContactId == id && x.IsActive == true).FirstOrDefault();
                if (_area != null)
                {
                    return _area;
                }
                return null;
            }
        }
        internal static CustomerLocationContactViewModel getLocationContactUserDetailsById(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var contact = (from c in db.CustomerLocationContacts
                               where c.LocationContactId == id && c.IsActive == true
                               select new
                               {
                                   c.LocationContactId,
                                   c.CustomerId,
                                   c.ContactName,
                                   c.ContactEmail,
                                   c.ContactPhone,
                                   UserID = c.UserID ?? 0,
                                   c.CustomerLocationID,
                                   c.CreatedDate,
                                   c.CreatedBy,
                                   c.ModifiedDate,
                                   c.ModifiedBy
                               }).FirstOrDefault();

                if (contact == null)
                    return null;

                // Get linked location ID
                var linkedLocations = db.CustomersLocationsUsers
                    .Where(clu => clu.LocationContactId == contact.LocationContactId)
                    .Select(clu => clu.CustomerLocationID)
                    .ToList();

                // Get linked CustomersLocationsUser IDs
                var linkedCLUserIds = db.CustomersLocationsUsers
                    .Where(clu => clu.LocationContactId == contact.LocationContactId)
                    .Select(clu => clu.CustomerUserLocationId)
                    .ToList();

                // Get linked location names
                var linkedLocationNames = (from clu in db.CustomersLocationsUsers
                                           join cl in db.CustomerLocations on clu.CustomerLocationID equals cl.CustomerLocationID
                                           join ct in db.Cities on cl.CityID equals ct.CityID
                                           where clu.LocationContactId == contact.LocationContactId
                                           select cl.LocationName + "[" + ct.CityName + "]").Distinct().ToList();

                // Get customer name
                var customerName = db.Customers
                    .Where(c => c.CustomerId == contact.CustomerId)
                    .Select(c => c.CustomerName)
                    .FirstOrDefault();

                // Get primary location name
                var primaryLocation = db.CustomerLocations
                    .Where(cl => cl.CustomerLocationID == contact.CustomerLocationID)
                    .Select(cl => cl.LocationName)
                    .FirstOrDefault();

                return new CustomerLocationContactViewModel
                {
                    LocationContactId = contact.LocationContactId,
                    CustomerId = contact.CustomerId,
                    ContactName = contact.ContactName,
                    ContactEmail = contact.ContactEmail,
                    ContactPhone = contact.ContactPhone,
                    UserID = contact.UserID,
                    CustomerLocationID = contact.CustomerLocationID,
                    CreatedDate = contact.CreatedDate,
                    CreatedBy = contact.CreatedBy.ToString(),
                    ModifiedDate = contact.ModifiedDate,
                    ModifiedBy = contact.ModifiedBy.ToString(),
                    IsActive = true,
                    Selected = false,
                    Customer = customerName,
                    CustomerLocation = primaryLocation,
                    LinkedCustomerLocationIDs = linkedLocations,
                    LinkedCustomerUserLocationIds = linkedCLUserIds,
                    LinkedLocationNames = string.Join(", ", linkedLocationNames)
                };
            }
        }

        //internal static CustomerLocationContactViewModel getLocationContactUserDetailsById(long id)
        //{
        //    try
        //    {
        //        using (DatabaseEntities db = new DatabaseEntities())
        //        {
        //            var customerUser = (from contact in db.CustomerLocationContacts
        //                                where contact.LocationContactId == id && contact.IsActive == true
        //                                select new CustomerLocationContactViewModel
        //                                {
        //                                    LocationContactId = contact.LocationContactId,
        //                                    CustomerId = contact.CustomerId,
        //                                    ContactName = contact.ContactName,
        //                                    ContactEmail = contact.ContactEmail,
        //                                    ContactPhone = contact.ContactPhone,
        //                                    UserID = contact.UserID ?? 0,
        //                                    CustomerLocationID = contact.CustomerLocationID,
        //                                    CreatedDate = contact.CreatedDate,
        //                                    CreatedBy = contact.CreatedBy.ToString(),
        //                                    ModifiedDate = contact.ModifiedDate,
        //                                    ModifiedBy = contact.ModifiedBy.ToString(),
        //                                    IsActive = contact.IsActive,
        //                                    Selected = false,

        //                                    // Primary location name
        //                                    CustomerLocation = db.CustomerLocations
        //                                        .Where(cl => cl.CustomerLocationID == contact.CustomerLocationID)
        //                                        .Select(cl => cl.LocationName)
        //                                        .FirstOrDefault(),

        //                                    // Customer name (assuming a method like getCustomerById)
        //                                    Customer = db.Customers
        //                                        .Where(c => c.CustomerId == contact.CustomerId)
        //                                        .Select(c => c.CustomerName)
        //                                        .FirstOrDefault(),

        //                                    // Linked location ID
        //                                    LinkedCustomerLocationIDs = db.CustomersLocationsUsers
        //                                        .Where(clu => clu.LocationContactId == contact.LocationContactId)
        //                                        .Select(clu => clu.CustomerLocationID)
        //                                        .ToList(),

        //                                    // Linked CustomersLocationsUser IDs
        //                                    LinkedCustomerUserLocationIds = db.CustomersLocationsUsers
        //                                        .Where(clu => clu.LocationContactId == contact.LocationContactId)
        //                                        .Select(clu => clu.CustomerUserLocationId)
        //                                        .ToList(),

        //                                    // Comma-separated linked location names
        //                                    LinkedLocationNames = string.Join(", ",
        //                                        (from clu in db.CustomersLocationsUsers
        //                                         join cl in db.CustomerLocations on clu.CustomerLocationID equals cl.CustomerLocationID
        //                                         where clu.LocationContactId == contact.LocationContactId
        //                                         select cl.LocationName).Distinct())
        //                                }).FirstOrDefault();

        //            if (customerUser != null)
        //            {
        //                return customerUser;
        //            }
        //            return null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;                
        //    }

        //}

        internal static CustomerLocationContact getLocationContactDetailsByUserId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var _area = db.CustomerLocationContacts.Where(x => x.UserID == userId && x.IsActive == true).FirstOrDefault();
                    if (_area != null)
                    {
                        return _area;
                    }
                }

                return null;
            }
        }

        internal static List<CustomerLocationContactViewModel> getLocationContactDetailsByLocationId()
        {
            List<CustomerLocationContactViewModel> list = new List<CustomerLocationContactViewModel>();
            var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
            if (userId != 0)
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        list = getLocationContactDetailsByLocationIdBoth(customer.CustomerId);
                    }
                }
            }
            return list;
        }
        internal static List<CustomerLocationContactViewModel> getLocationContactDetailsByLocationId(long CustomerId)
        {
            List<CustomerLocationContactViewModel> list = new List<CustomerLocationContactViewModel>();
            list = getLocationContactDetailsByLocationIdBoth(CustomerId);
            return list;
        }

        internal static List<CustomerLocationContactViewModel> getLocationContactDetailsByLocationIdBoth(long CustomerId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerLocationContactViewModel> list = new List<CustomerLocationContactViewModel>();


                var _contact = db.CustomerLocationContacts.Where(x => x.CustomerId == CustomerId && x.IsActive == true).OrderBy(x => x.ContactEmail).ToList();

                if (_contact.Count != 0)
                {
                    foreach (var d in _contact)
                    {
                        CustomerLocationContactViewModel _lContact = new CustomerLocationContactViewModel();
                        _lContact.LocationContactId = d.LocationContactId;
                        _lContact.CustomerId = d.CustomerId;
                        _lContact.ContactName = d.ContactName;
                        _lContact.ContactEmail = d.ContactEmail;
                        _lContact.ContactPhone = d.ContactPhone;
                        _lContact.Customer = getCustomerById(d.CustomerId).CustomerName;
                        _lContact.CustomerLocationID = d.CustomerLocationID;
                        _lContact.CustomerLocation = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationID)).LocationName;
                        _lContact.CreatedDate = d.CreatedDate;
                        _lContact.CreatedBy = d.CreatedBy;
                        _lContact.ModifiedDate = d.ModifiedDate;
                        _lContact.ModifiedBy = d.ModifiedBy;
                        _lContact.UserID = d.UserID ?? 0;                    

                        var linkedLocations = (
                        from clu in db.CustomersLocationsUsers
                        join cl in db.CustomerLocations on clu.CustomerLocationID equals cl.CustomerLocationID
                        join ct in db.Cities on cl.CityID equals ct.CityID into cityGroup  // LEFT JOIN
                        from ct in cityGroup.DefaultIfEmpty()
                        where clu.CustomerId == d.CustomerId && clu.LocationContactId == d.LocationContactId
                        select new
                        {
                            clu.CustomerUserLocationId,
                            cl.CustomerLocationID,
                            LocationName = cl.LocationName + (ct != null ? " (" + ct.CityName + ")" : "")  // ← only add city name if present
                        }).ToList();


                        _lContact.LinkedCustomerLocationIDs = linkedLocations.Select(x => x.CustomerLocationID).ToList();
                        _lContact.LinkedCustomerUserLocationIds = linkedLocations.Select(x => x.CustomerUserLocationId).ToList();
                        _lContact.LinkedLocationNames = string.Join(", ", linkedLocations.Select(x => x.LocationName).Distinct());

                        list.Add(_lContact);
                    }
                    return list;
                }
                return null;
            }
        }

        internal static List<CustomerLocationContactViewModel> getLocationContactDetailsByLocationIdBoth(long CustomerId, long LocationId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerLocationContactViewModel> list = new List<CustomerLocationContactViewModel>();

                // Query to get distinct contacts for the given CustomerId and LocationId
                var _contact = db.CustomersLocationsUsers
                                .Where(x => x.CustomerId == CustomerId && x.CustomerLocationID == LocationId)
                                .Join(db.CustomerLocationContacts,
                                      clu => clu.LocationContactId,
                                      clc => clc.LocationContactId,
                                      (clu, clc) => new
                                      {
                                          clc.LocationContactId,
                                          clc.CustomerId,
                                          clc.ContactName,
                                          clc.ContactEmail,
                                          clc.ContactPhone,
                                          clc.UserID,
                                          clc.CreatedDate,
                                          clc.CreatedBy,
                                          clc.ModifiedDate,
                                          clc.ModifiedBy,
                                          clc.CustomerLocationID
                                      })
                                .Distinct()  // Ensure unique contacts by LocationContactId
                                .OrderBy(x => x.ContactEmail)
                                .ToList();

                // If contacts are found
                if (_contact.Count != 0)
                {
                    // Use a HashSet to track unique LocationContactId's and avoid duplicates
                    HashSet<long> addedLocationContactIds = new HashSet<long>();

                    foreach (var d in _contact)
                    {
                        // Skip if this LocationContactId has already been added
                        if (addedLocationContactIds.Contains(d.LocationContactId))
                            continue;

                        addedLocationContactIds.Add(d.LocationContactId);

                        CustomerLocationContactViewModel _lContact = new CustomerLocationContactViewModel();
                        _lContact.LocationContactId = d.LocationContactId;
                        _lContact.CustomerId = d.CustomerId;
                        _lContact.ContactName = d.ContactName;
                        _lContact.ContactEmail = d.ContactEmail;
                        _lContact.ContactPhone = d.ContactPhone;
                        _lContact.Customer = getCustomerById(d.CustomerId).CustomerName;
                        _lContact.CustomerLocationID = d.CustomerLocationID;
                        _lContact.CustomerLocation = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationID)).LocationName;
                        _lContact.CreatedDate = d.CreatedDate;
                        _lContact.CreatedBy = d.CreatedBy;
                        _lContact.ModifiedDate = d.ModifiedDate;
                        _lContact.ModifiedBy = d.ModifiedBy;
                        _lContact.UserID = d.UserID ?? 0;

                        var linkedLocations = (
                            from clu in db.CustomersLocationsUsers
                            join cl in db.CustomerLocations on clu.CustomerLocationID equals cl.CustomerLocationID
                            join ct in db.Cities on cl.CityID equals ct.CityID
                            where clu.CustomerId == d.CustomerId && clu.LocationContactId == d.LocationContactId
                            select new
                            {
                                clu.CustomerUserLocationId,
                                cl.CustomerLocationID,
                                LocationName = cl.LocationName + " [" + ct.CityName + "]"
                            })
                            .Distinct()  // Ensure unique locations per contact
                            .ToList();

                        // Deduplicate LinkedCustomerLocationIDs and LinkedLocationNames
                        _lContact.LinkedCustomerLocationIDs = linkedLocations.Select(x => x.CustomerLocationID).Distinct().ToList();
                        _lContact.LinkedCustomerUserLocationIds = linkedLocations.Select(x => x.CustomerUserLocationId).Distinct().ToList();
                        _lContact.LinkedLocationNames = string.Join(", ", linkedLocations.Select(x => x.LocationName).Distinct());

                        // Add the contact to the list
                        list.Add(_lContact);
                    }
                    //foreach (var contact in list)
                    //{
                    //    Debug.WriteLine($"LocationContactId: {contact.LocationContactId}");
                    //    Debug.WriteLine($"CustomerId: {contact.CustomerId}");
                    //    Debug.WriteLine($"ContactName: {contact.ContactName}");
                    //    Debug.WriteLine($"ContactEmail: {contact.ContactEmail}");
                    //    Debug.WriteLine($"ContactPhone: {contact.ContactPhone}");
                    //    Debug.WriteLine($"CustomerLocationID: {contact.CustomerLocationID}");
                    //    Debug.WriteLine($"CustomerLocation: {contact.CustomerLocation}");
                    //    Debug.WriteLine($"CreatedDate: {contact.CreatedDate}");
                    //    Debug.WriteLine($"CreatedBy: {contact.CreatedBy}");
                    //    Debug.WriteLine($"ModifiedDate: {contact.ModifiedDate}");
                    //    Debug.WriteLine($"ModifiedBy: {contact.ModifiedBy}");
                    //    Debug.WriteLine($"UserID: {contact.UserID}");
                    //    Debug.WriteLine($"LinkedCustomerLocationIDs: {string.Join(", ", contact.LinkedCustomerLocationIDs)}");
                    //    Debug.WriteLine($"LinkedCustomerUserLocationIds: {string.Join(", ", contact.LinkedCustomerUserLocationIds)}");
                    //    Debug.WriteLine($"LinkedLocationNames: {contact.LinkedLocationNames}");
                    //    Debug.WriteLine("---------------------------------------------------");
                    //}
                    return list;
                }
                return null;
            }
        }


        internal object GetContactWithLocations(int customerId, int locationContactId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var query = (
                    from clu in db.CustomersLocationsUsers
                    join cl in db.CustomerLocations on clu.CustomerLocationID equals cl.CustomerLocationID
                    join contact in db.CustomerLocationContacts on clu.LocationContactId equals contact.LocationContactId
                    where clu.CustomerId == customerId && clu.LocationContactId == locationContactId
                    select new
                    {
                        contact.ContactName,
                        contact.ContactEmail,
                        contact.CustomerId,
                        clu.CustomerUserLocationId,
                        LocationId = cl.CustomerLocationID,
                        LocationName = cl.LocationName
                    }
                ).ToList();

                // Group by contact info (in case of multiple locations)
                var grouped = query
                    .GroupBy(x => new { x.ContactName, x.ContactEmail, x.CustomerId })
                    .Select(g => new
                    {
                        g.Key.ContactName,
                        g.Key.ContactEmail,
                        g.Key.CustomerId,
                        CustomerUserLocationIds = g.Select(x => x.CustomerUserLocationId).ToList(),
                        LocationIds = g.Select(x => x.LocationId).ToList(),
                        LocationNames = string.Join(", ", g.Select(x => x.LocationName).Distinct())
                    })
                    .FirstOrDefault(); // Assuming one contact

                return grouped;
            }
        }

        internal static List<CustomerLocationContactViewModel> GetLocationContactByCustomer()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    List<CustomerLocationContactViewModel> list = new List<CustomerLocationContactViewModel>();
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        var _contact = db.CustomerLocationContacts.Where(x => x.CustomerId == customer.CustomerId && x.IsActive == true).OrderBy(x => x.ContactEmail).ToList();
                        if (_contact.Count != 0)
                        {
                            foreach (var d in _contact)
                            {
                                CustomerLocationContactViewModel _lContact = new CustomerLocationContactViewModel();
                                _lContact.LocationContactId = d.LocationContactId;
                                _lContact.CustomerId = d.CustomerId;
                                _lContact.ContactName = d.ContactName;
                                _lContact.ContactEmail = d.ContactEmail;
                                _lContact.ContactPhone = d.ContactPhone;
                                _lContact.Customer = getCustomerById(d.CustomerId).CustomerName;
                                _lContact.CustomerLocationID = d.CustomerLocationID;
                                _lContact.CustomerLocation = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationID)).LocationName;
                                _lContact.CreatedDate = d.CreatedDate;
                                _lContact.CreatedBy = d.CreatedBy;
                                _lContact.ModifiedDate = d.ModifiedDate;
                                _lContact.ModifiedBy = d.ModifiedBy;
                                list.Add(_lContact);
                            }
                            return list;
                        }
                    }
                }
                return null;
            }
        }

        internal static string saveLocationContact(CustomerLocationContactViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                CustomerLocationContact contact = new CustomerLocationContact();
                User user = new User();
                //if (model.UserName == null)
                //{
                //    return "Please enter user name.";
                //}
                //if (model.UserPassword == null)
                //{
                //    return "Please enter user password.";
                //}
                if (model.ContactName == null)
                {
                    return "Please enter contact name.";
                }
                if (model.ContactEmail == null)
                {
                    return "Please enter contact email.";
                }
                //if (model.UserName != null && model.UserPassword != null)
                //{
                //    var found = db.Users.Where(x => x.UserName == model.UserName).FirstOrDefault();
                //    if(found == null)
                //    {
                //        user.UserName = model.UserName;
                //        user.UserPassword = AccountController.MD5Hash(model.UserPassword);
                //        user.UserType = 5;
                //        user.IsActive = true;
                //        user.CreatedDate = DateTime.Now;
                //        user.ModifiedDate = DateTime.Now;
                //        if (model.CreatedBy != null)
                //        {
                //            user.CreatedBy = model.CreatedBy;
                //            user.ModifiedBy = model.CreatedBy;
                //        }
                //        else
                //        {
                //            user.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                //            user.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                //        }
                //        db.Users.Add(user);
                //        db.SaveChanges();

                //        if (user.UserId != 0)
                //        {
                //            contact.UserID = user.UserId;
                contact.ContactName = model.ContactName;
                contact.ContactEmail = model.ContactEmail;
                contact.ContactPhone = model.ContactPhone;
                contact.CustomerLocationID = model.CustomerLocationID;
                if (model.CustomerLocationID != 0)
                {
                    var location = getCustomerLocationById(Convert.ToInt16(model.CustomerLocationID));
                    if (location != null) { contact.CustomerId = location.CustomerId; }

                }
                contact.IsActive = true;
                contact.CreatedDate = DateTime.Now;
                contact.ModifiedDate = DateTime.Now;
                if (model.CreatedBy != null)
                {
                    contact.CreatedBy = model.CreatedBy;
                    contact.ModifiedBy = model.CreatedBy;
                }
                else
                {
                    contact.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    contact.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                }
                db.CustomerLocationContacts.Add(contact);
                db.SaveChanges();
                //}
                return "Ok";
                //    }

                //    return "User already exist. Please try another user name.";
                //}

                //return "Please enter user information.";
            }
        }

        internal static string saveLocationContactMultiple(CustomerLocationContactViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                long CustId = 0;
                bool isNewUser = false;
                List<CustomerLocation> lstLocationName;
                List<long> locationIds = model.LocationIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => Convert.ToInt64(id.Trim())).ToList();
                lstLocationName = db.CustomerLocations.Where(loc => locationIds.Contains(loc.CustomerLocationID)).ToList();
                CustId = model.CustomerId;
                var custUser = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (custUser != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == custUser).FirstOrDefault();
                    if (customer != null)
                    {
                        CustId = customer.CustomerId;
                    }
                }
                User user = new User();
                //if (model.UserName == null)
                //{
                //    return "Please enter user name.";
                //}
                model.UserPassword = GenerateRandomPassword(8);
                if (model.UserPassword == null)
                {
                    return "Please enter user password.";
                }
                if (model.ContactName == null)
                {
                    return "Please enter contact name.";
                }
                if (model.ContactEmail == null)
                {
                    return "Please enter contact email(User Name).";
                }
                if (model.LocationIds == null)
                {
                    return "Please select Location.";
                }
                if (model.UserName != null && model.UserPassword != null)
                {
                    var found = db.Users.Where(x => x.UserName == model.ContactEmail).FirstOrDefault();
                    if (found == null)
                    {
                        user.UserName = model.ContactEmail;
                        user.UserPassword = AccountController.MD5Hash(model.UserPassword);
                        user.UserType = 9;
                        user.IsActive = true;
                        user.CreatedDate = DateTime.Now;
                        user.ModifiedDate = DateTime.Now;
                        if (model.CreatedBy != null)
                        {
                            user.CreatedBy = model.CreatedBy;
                            user.ModifiedBy = model.CreatedBy;
                        }
                        else
                        {
                            user.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            user.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        }
                        db.Users.Add(user);
                        db.SaveChanges();
                        isNewUser = true;
                        if (user.UserId != 0)
                        {


                            List<string> LocationIdslist = model.LocationIds.Split(',').ToList();
                            if (LocationIdslist.Count > 0)
                            {
                                CustomerLocationContact contact = new CustomerLocationContact();
                                contact.UserID = user.UserId;
                                contact.ContactName = model.ContactName;
                                contact.ContactEmail = model.ContactEmail;
                                contact.ContactPhone = model.ContactPhone;
                                contact.CustomerId = CustId;
                                contact.CustomerLocationID = Convert.ToInt32(LocationIdslist[0]);
                                //if (model.CustomerLocationID != 0)
                                //{
                                //    var location = getCustomerLocationById(Convert.ToInt16(model.CustomerLocationID));
                                //    if (location != null) { contact.CustomerId = location.CustomerId; }
                                //}
                                contact.IsActive = true;
                                contact.CreatedDate = DateTime.Now;
                                contact.ModifiedDate = DateTime.Now;
                                if (model.CreatedBy != null)
                                {
                                    contact.CreatedBy = "1";
                                    contact.ModifiedBy = "1";
                                }
                                else
                                {
                                    contact.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                    contact.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                }
                                db.CustomerLocationContacts.Add(contact);
                                db.SaveChanges();

                                foreach (string locationId in LocationIdslist)
                                {
                                    CustomersLocationsUser CLU = new CustomersLocationsUser();
                                    CLU.LocationContactId = contact.LocationContactId;
                                    CLU.CustomerId = CustId;
                                    CLU.CustomerLocationID = Convert.ToInt32(locationId);
                                    CLU.CreatedDate = DateTime.Now;
                                    CLU.ModifiedDate = DateTime.Now;
                                    if (model.CreatedBy != null)
                                    {
                                        CLU.CreatedBy = model.CreatedBy;
                                        CLU.ModifiedBy = model.CreatedBy;
                                    }
                                    else
                                    {
                                        CLU.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                        CLU.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                    }
                                    db.CustomersLocationsUsers.Add(CLU);
                                    db.SaveChanges();
                                }
                            }
                        }
                        db.SaveChanges();
                        if (isNewUser == true)
                        {
                            SendContactEmailWithPassword(model, lstLocationName);
                            //string StrCustName = "";
                            //var customerEmail = db.Customers.Where(x => x.CustomerId == model.CustomerId).FirstOrDefault();
                            //if (customerEmail != null)
                            //{
                            //    StrCustName = customerEmail.CustomerName;
                            //}

                            //string strMSG = "";
                            //string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                            //Uri url = new Uri(tmpURL);
                            //string host = url.GetLeftPart(UriPartial.Authority);

                            //List<string> strCCEmailslist = new List<string>();
                            //string toCustContact;
                            //strCCEmailslist.Add("b.trivedi@camindustrial.net");
                            ////strCCEmailslist.Add("nirav.m@siliconinfo.com");

                            //toCustContact = model.ContactEmail;

                            ////var subject = "" + iDetails.InspectionDocumentNo + "-" + iDetails.Customer + "";
                            //var subject = "You have been receiving this email as primary contact to access “Rack Manager” for below locations.";
                            //var toEmail = model.ContactEmail;
                            //strMSG = "<html>";
                            //strMSG += "<head>";
                            //strMSG += "<style>";
                            //strMSG += "p{margin:0px}";
                            //strMSG += "</style>";
                            //strMSG += "</head>";
                            //strMSG += "<body>";
                            //strMSG += "<div style='width:1200px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";


                            //strMSG += "<p>Attention: " + model.ContactName + " [" + StrCustName + "]</p>";

                            //strMSG += "<br/>";
                            //strMSG += "<br/>";
                            //strMSG += "<p>You have been receiving this email as primary contact to access “Rack Manager” for below locations.</p>";
                            //strMSG += "<br/>";

                            //strMSG += "<ul>";
                            //foreach (var loc in lstLocationName)
                            //{
                            //    strMSG += $"<li><p>{loc.LocationName}</p></li>";
                            //}
                            //strMSG += "</ul>";
                            //strMSG += "<br/>";
                            //strMSG += "<p>You can access  <a href='https://rack-manager.com/'>(rack-manager.com)</a> by using your email as user ID and temporary password as " + model.UserPassword + " provided by admin.</p>";
                            //strMSG += "<p>You will find the outcome of the inspection, and the detailed findings are now documented in the report. We understand the importance of this report in providing valuable insights into the condition and any necessary actions regarding the pallet racking.</p>";
                            //strMSG += "<p>Additionally, you can access the deficiency list and select red and/or yellow deficiencies that you would like us to provide repair/replace quotation.</p>";
                            //strMSG += "<br/>";
                            //strMSG += "<p>There are two ways to select the deficiencies:</p>";
                            //strMSG += "<p>1) Click “Select Deficiency For Quotation” to select all red and/or yellow deficiencies, located at the top-right corner of the Deficiency List.</p>";
                            //strMSG += "<p><span><img alt='SelectDeficiencyQuotationEmail' src = 'https://rack-manager.com/img/SelectDeficiencyQuotationEmail.png' /></span></p>";
                            //strMSG += "<p>2) You can select checkboxes under “Quotation” → “Request Quotation”, which allows to select specific deficiencies.</p>";
                            //strMSG += "<p><span><img alt='QuotationSelectionEmail' src = 'https://rack-manager.com/img/QuotationSelectionEmail.png' /></span></p>";
                            //strMSG += "<p>Once you have made your selections, please click “Request Quotation”.</p>";
                            //strMSG += "<p><span><img alt='QuotationButtonEmail' src = 'https://rack-manager.com/img/QuotationButtonEmail.png' /></span></p>";
                            //strMSG += "<br/>";
                            //strMSG += "<p>Please don’t hesitate to contact office+1 800 772 3213 for any queries.</p>";
                            //strMSG += "<br/>";
                            //strMSG += "<p>Should you have any questions or require further clarification on any aspect of the report, please do not hesitate to reach out to us. Our team is available to discuss the findings and provide any assistance you may need.</p>";
                            //strMSG += "<br/>";
                            //strMSG += "<div><div></div></div><br/><br/><div><div>";
                            //strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Best regards,</span></p>";
                            //strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                            //strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'> P.Eng, M.Tech, PMP</span></p>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                            //strMSG += "<br/>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                            //strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                            //strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                            //strMSG += "<br/>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='ES'>E ~ &nbsp;</span></b><b>";
                            //strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                            //strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='ES'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='ES'>C ~</span></b><b>";
                            //strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='ES'>(403) 690-2976</span></b></p>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                            //strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                            //strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                            //strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                            //strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                            //strMSG += "<p><span><img style='width:2.618in;height:.6458in'";
                            //strMSG += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted=''";
                            //strMSG += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                            //strMSG += "</div>";
                            //strMSG += "</div>";
                            //strMSG += "</div>";
                            //strMSG += "</body>";
                            //strMSG += "</html>";

                            //var tEmail = new Thread(() => EmailHelper.SendEmail(toCustContact, subject, null, strMSG, strCCEmailslist)); //attachmentFile
                            //tEmail.Start();
                        }
                        return "Ok";
                    }
                    else
                    {
                        return "User already exist. Please try another user name.";
                    }
                }
                return "Ok";
                //return "Please enter user information.";
            }
        }
        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                // Get a random index from the validChars string
                char randomChar = validChars[_random.Next(validChars.Length)];
                password.Append(randomChar);
            }

            return password.ToString();
        }
        internal static string SendContactEmailWithPassword(CustomerLocationContactViewModel model, List<CustomerLocation> lstLocationName)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    string StrCustName = "";
                    var customerEmail = db.Customers.Where(x => x.CustomerId == model.CustomerId).FirstOrDefault();
                    if (customerEmail != null)
                    {
                        StrCustName = customerEmail.CustomerName;
                    }

                    string strMSG = "";
                    string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                    Uri url = new Uri(tmpURL);
                    string host = url.GetLeftPart(UriPartial.Authority);

                    List<string> strCCEmailslist = new List<string>();
                    string toCustContact;
                    strCCEmailslist.Add("b.trivedi@camindustrial.net");
                    //strCCEmailslist.Add("nirav.m@siliconinfo.com");

                    toCustContact = model.ContactEmail;

                    //var subject = "" + iDetails.InspectionDocumentNo + "-" + iDetails.Customer + "";
                    var subject = "You have been receiving this email as primary contact to access “Rack Manager” for below locations.";
                    var toEmail = model.ContactEmail;
                    strMSG = "<html>";
                    strMSG += "<head>";
                    strMSG += "<style>";
                    strMSG += "p{margin:0px}";
                    strMSG += "</style>";
                    strMSG += "</head>";
                    strMSG += "<body>";
                    strMSG += "<div style='width:1200px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";


                    strMSG += "<p>Attention: " + model.ContactName + " [" + StrCustName + "]</p>";

                    strMSG += "<br/>";
                    strMSG += "<br/>";
                    strMSG += "<p>You have been receiving this email as primary contact to access “Rack Manager” for below locations.</p>";
                    strMSG += "<br/>";

                    strMSG += "<ul>";
                    foreach (var loc in lstLocationName)
                    {
                        strMSG += $"<li><p>{loc.LocationName}</p></li>";
                    }
                    strMSG += "</ul>";
                    strMSG += "<br/>";
                    strMSG += "<p>You can access  <a href='https://rack-manager.com/'>(rack-manager.com)</a> by using your email as user ID and temporary password as " + model.UserPassword + " provided by admin.</p>";
                    strMSG += "<p>You will find the outcome of the inspection, and the detailed findings are now documented in the report. We understand the importance of this report in providing valuable insights into the condition and any necessary actions regarding the pallet racking.</p>";
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
                    strMSG += "<p>Please don’t hesitate to contact office+1 800 772 3213 for any queries.</p>";
                    strMSG += "<br/>";
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

                    var tEmail = new Thread(() => EmailHelper.SendEmail(toCustContact, subject, null, strMSG, strCCEmailslist)); //attachmentFile
                    tEmail.Start();
                    return "Send";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "Send";
        }
        internal static string editLocationContactMultiple(CustomerLocationContactViewModel model)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    bool isNewUser = false;
                    long CustId = 0;
                    CustId = model.CustomerId;
                    var custUser = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (custUser != 0)
                    {
                        var customer = db.Customers.Where(x => x.UserID == custUser).FirstOrDefault();
                        if (customer != null)
                        {
                            CustId = customer.CustomerId;
                        }
                    }

                    User user = null;

                    if (model.ContactName == null)
                    {
                        return "Please enter contact name.";
                    }
                    if (model.ContactEmail == null)
                    {
                        return "Please enter contact email (User Name).";
                    }
                    if (model.LocationIds == null)
                    {
                        return "Please select Location.";
                    }

                    // 1. Handle user creation if UserID == 0
                    if (model.UserID == 0)
                    {
                        if (string.IsNullOrEmpty(model.UserPassword))
                        {
                            return "User creation incomplete. Please enter your password.";
                        }

                        var existingUser = db.Users.FirstOrDefault(x => x.UserId == model.UserID);
                        if (existingUser != null)
                        {
                            User objUser = new User();
                            objUser.UserName = model.UserName;
                            if (!string.IsNullOrEmpty(model.UserPassword))
                            {
                                objUser.UserPassword = AccountController.MD5Hash(model.UserPassword);
                            }
                            db.Entry(objUser).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            user = new User
                            {
                                UserName = model.ContactEmail,
                                UserPassword = AccountController.MD5Hash(model.UserPassword),
                                UserType = 9,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString(),
                                ModifiedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString()
                            };

                            db.Users.Add(user);
                            db.SaveChanges();
                            isNewUser = true;
                        }
                        model.UserID = user.UserId;
                    }
                    else
                    {
                        var existingUser = db.Users.FirstOrDefault(x => x.UserId == model.UserID);
                        if (existingUser != null)
                        {
                            //User objUser = new User();
                            existingUser.UserName = model.UserName;
                            if (!string.IsNullOrEmpty(model.UserPassword))
                            {
                                existingUser.UserPassword = AccountController.MD5Hash(model.UserPassword);
                            }
                            db.Entry(existingUser).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        model.UserID = existingUser.UserId;
                    }

                    // 2. Add or update CustomerLocationContact
                    CustomerLocationContact contact;
                    List<string> LocationIdsList = model.LocationIds.Split(',').ToList();
                    if (model.LocationContactId != 0)
                    {
                        // Editing existing contact
                        contact = db.CustomerLocationContacts.FirstOrDefault(c => c.LocationContactId == model.LocationContactId);
                        if (contact == null) return "Contact not found.";
                        contact.CustomerLocationID = Convert.ToInt64(LocationIdsList[0]);
                        contact.ContactName = model.ContactName;
                        contact.ContactEmail = model.ContactEmail;
                        contact.ContactPhone = model.ContactPhone;
                        contact.UserID = model.UserID;
                        contact.ModifiedDate = DateTime.Now;
                        contact.ModifiedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString();
                        db.Entry(contact).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        // Inserting new contact
                        contact = new CustomerLocationContact
                        {
                            UserID = model.UserID,
                            ContactName = model.ContactName,
                            ContactEmail = model.ContactEmail,
                            ContactPhone = model.ContactPhone,
                            CustomerId = CustId,
                            CustomerLocationID = Convert.ToInt32(model.LocationIds.Split(',')[0]),
                            IsActive = true,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            CreatedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString(),
                            ModifiedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString()
                        };
                        db.CustomerLocationContacts.Add(contact);
                    }

                    db.SaveChanges();

                    // 3. Insert new locations into CustomersLocationsUser
                    var existingCLU = db.CustomersLocationsUsers
                    .Where(x => x.LocationContactId == contact.LocationContactId)
                    .Select(x => x.CustomerLocationID)
                    .ToList(); // Get the existing CustomerLocationIDs for the current LocationContactId

                    // Remove entries where CustomerLocationID is not part of the LocationIdsList
                    var locationIdsListInt = LocationIdsList.Select(id => Convert.ToInt64(id)).ToList();
                    var toRemove = db.CustomersLocationsUsers
                                    .Where(x => x.LocationContactId == contact.LocationContactId && !locationIdsListInt.Contains(x.CustomerLocationID))
                                    .ToList();

                    if (toRemove.Any())
                    {
                        db.CustomersLocationsUsers.RemoveRange(toRemove); // Remove data not part of the new LocationIdsList
                    }

                    // Insert new CustomerLocationIDs
                    foreach (var locationId in locationIdsListInt)
                    {
                        if (!existingCLU.Contains(locationId)) // Check if the CustomerLocationID already exists
                        {
                            var clu = new CustomersLocationsUser
                            {
                                LocationContactId = contact.LocationContactId,
                                CustomerId = CustId,
                                CustomerLocationID = locationId,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                CreatedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString(),
                                ModifiedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString()
                            };

                            db.CustomersLocationsUsers.Add(clu); // Add new entry for the new CustomerLocationID
                        }
                    }

                    // Save changes to the database
                    db.SaveChanges();

                    if (!string.IsNullOrEmpty(model.UserPassword))
                    {
                        List<CustomerLocation> lstLocationName;
                        List<long> locationIds = model.LocationIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(id => Convert.ToInt64(id.Trim())).ToList();
                        lstLocationName = db.CustomerLocations.Where(loc => locationIds.Contains(loc.CustomerLocationID)).ToList();
                        SendContactEmailWithPassword(model, lstLocationName);
                    }

                    //var existingCLU = db.CustomersLocationsUsers
                    //                    .Where(x => x.LocationContactId == contact.LocationContactId)
                    //                    .Select(x => x.CustomerUserLocationId)
                    //                    .ToList();

                    //foreach (var locationIdStr in LocationIdsList)
                    //{
                    //    int locationId = Convert.ToInt32(locationIdStr);
                    //    if (!existingCLU.Contains(locationId))
                    //    {
                    //        CustomersLocationsUser clu = new CustomersLocationsUser
                    //        {
                    //            LocationContactId = contact.LocationContactId,
                    //            CustomerId = CustId,
                    //            CustomerLocationID = locationId,
                    //            CreatedDate = DateTime.Now,
                    //            ModifiedDate = DateTime.Now,
                    //            CreatedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString(),
                    //            ModifiedBy = model.CreatedBy ?? HttpContext.Current.Session["LoggedInUserId"].ToString()
                    //        };

                    //        db.CustomersLocationsUsers.Add(clu);
                    //    }
                    //}

                    //db.SaveChanges();
                    return "Ok";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        internal static int editLocationContact(CustomerLocationContact model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var contact = db.CustomerLocationContacts.Where(x => x.LocationContactId == model.CustomerLocationID).FirstOrDefault();
                    if (contact != null)
                    {
                        contact.ContactName = model.ContactName;
                        contact.ContactEmail = model.ContactEmail;
                        contact.ContactPhone = model.ContactPhone;
                        contact.IsActive = true;
                        contact.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        contact.ModifiedDate = DateTime.Now;
                        db.Entry(contact).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        internal static long removeLocationContact(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.CustomerLocationContacts.Where(x => x.LocationContactId == id).FirstOrDefault();
                if (itm != null)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(itm.UserID)))
                    {
                        var usr = db.Users.Where(y => y.UserId == itm.UserID).FirstOrDefault();
                        if (usr != null)
                        {
                            usr.IsActive = false;
                            db.Entry(usr).State = EntityState.Modified;
                        }
                    }
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return itm.CustomerLocationID;
                }
                return 0;
            }
        }

        internal static List<ConclusionRecommendation> getAllConclusionRecommendations()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.ConclusionRecommendations.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static ConclusionRecommendation getConclusionRecommendationsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ConclusionRecommendations.Where(x => x.IsActive == true && x.ConclusionRecommendationsID == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.ConclusionRecommendations = HttpUtility.JavaScriptStringEncode(itm.ConclusionRecommendations);
                    return itm;
                }
                return null;
            }
        }

        internal static string saveConclusionRecommendations(ConclusionRecommendation model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    ConclusionRecommendation conc = new ConclusionRecommendation();
                    conc.ConclusionRecommendations = model.ConclusionRecommendations;
                    conc.ConclusionRecommendationsTitle = model.ConclusionRecommendationsTitle;
                    conc.IsActive = true;
                    conc.CreatedDate = DateTime.Now;
                    conc.CreatedBy = model.CreatedBy;
                    conc.ModifiedDate = DateTime.Now;
                    conc.ModifiedBy = model.CreatedBy;
                    db.ConclusionRecommendations.Add(conc);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editConclusionRecommendations(ConclusionRecommendation model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var conclusion = db.ConclusionRecommendations.Where(x => x.ConclusionRecommendationsID == model.ConclusionRecommendationsID).FirstOrDefault();
                    if (conclusion != null)
                    {
                        conclusion.ConclusionRecommendationsTitle = model.ConclusionRecommendationsTitle;
                        conclusion.ConclusionRecommendations = model.ConclusionRecommendations;
                        conclusion.IsActive = true;
                        conclusion.ModifiedBy = model.ModifiedBy;
                        conclusion.ModifiedDate = DateTime.Now;
                        db.Entry(conclusion).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeConclusionRecommendations(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ConclusionRecommendations.Where(x => x.ConclusionRecommendationsID == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<FacilitiesArea> getAllFacilitiesArea()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.FacilitiesAreas.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static FacilitiesArea getFacilitiesAreaById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.FacilitiesAreas.Where(x => x.IsActive == true && x.FacilitiesAreaId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveFacilitiesArea(FacilitiesArea model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    FacilitiesArea obj = new FacilitiesArea();
                    obj.FacilitiesAreaName = model.FacilitiesAreaName;
                    obj.FacilitiesAreaDesc = model.FacilitiesAreaDesc;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.FacilitiesAreas.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editFacilitiesArea(FacilitiesArea model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var fac = db.FacilitiesAreas.Where(x => x.FacilitiesAreaId == model.FacilitiesAreaId).FirstOrDefault();
                    if (fac != null)
                    {
                        fac.FacilitiesAreaName = model.FacilitiesAreaName;
                        fac.FacilitiesAreaDesc = model.FacilitiesAreaDesc;
                        fac.IsActive = true;
                        fac.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        fac.ModifiedDate = DateTime.Now;
                        db.Entry(fac).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeFacilitiesArea(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.FacilitiesAreas.Where(x => x.FacilitiesAreaId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<ProcessOverview> getAllProcessOverview()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.ProcessOverviews.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static ProcessOverview getProcessOverviewById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ProcessOverviews.Where(x => x.IsActive == true && x.ProcessOverviewId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveProcessOverview(ProcessOverview model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    ProcessOverview obj = new ProcessOverview();
                    obj.ProcessOverviewDesc = model.ProcessOverviewDesc;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.ProcessOverviews.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editProcessOverview(ProcessOverview model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var pro = db.ProcessOverviews.Where(x => x.ProcessOverviewId == model.ProcessOverviewId).FirstOrDefault();
                    if (pro != null)
                    {
                        pro.ProcessOverviewDesc = model.ProcessOverviewDesc;
                        pro.IsActive = true;
                        pro.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        pro.ModifiedDate = DateTime.Now;
                        db.Entry(pro).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeProcessOverview(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ProcessOverviews.Where(x => x.ProcessOverviewId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<DeficiencySummary> getAllDeficiencySummary()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.DeficiencySummaries.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static DeficiencySummary getDeficiencySummaryById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DeficiencySummaries.Where(x => x.IsActive == true && x.DeficiencySummaryId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveDeficiencySummary(DeficiencySummary model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    DeficiencySummary obj = new DeficiencySummary();
                    obj.DeficiencySummaryDesc = model.DeficiencySummaryDesc;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.DeficiencySummaries.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editDeficiencySummary(DeficiencySummary model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.DeficiencySummaries.Where(x => x.DeficiencySummaryId == model.DeficiencySummaryId).FirstOrDefault();
                    if (def != null)
                    {
                        def.DeficiencySummaryDesc = model.DeficiencySummaryDesc;
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeDeficiencySummary(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DeficiencySummaries.Where(x => x.DeficiencySummaryId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<DeficiencyCategory> getAllDeficiencyCategory()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.DeficiencyCategories.Where(x => x.IsActive == true).OrderBy(x => x.DeficiencyCategoryName).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static DeficiencyCategory getDeficiencyCategoryById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DeficiencyCategories.Where(x => x.IsActive == true && x.DeficiencyCategoryId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveDeficiencyCategory(DeficiencyCategory model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    DeficiencyCategory obj = new DeficiencyCategory();
                    obj.DeficiencyCategoryName = model.DeficiencyCategoryName;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.DeficiencyCategories.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editDeficiencyCategory(DeficiencyCategory model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.DeficiencyCategories.Where(x => x.DeficiencyCategoryId == model.DeficiencyCategoryId).FirstOrDefault();
                    if (def != null)
                    {
                        def.DeficiencyCategoryName = model.DeficiencyCategoryName;
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeDeficiencyCategory(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DeficiencyCategories.Where(x => x.DeficiencyCategoryId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<Deficiency> getAllDeficiency()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.Deficiencies.Where(x => x.IsActive == true).OrderBy(x => x.DeficiencyCategory).ThenBy(x => x.DeficiencyInfo).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static Deficiency getDeficiencyById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Deficiencies.Where(x => x.IsActive == true && x.DeficiencyID == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static Deficiency getComponentDescriptionById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Deficiencies.Where(x => x.IsActive == true && x.ComponentId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }



        internal static List<Deficiency> getDeficiencyByDefType(string type)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Deficiencies.Where(x => x.IsActive == true && x.DeficiencyCategory == type.Trim()).ToList();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveDeficiency(Deficiency model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    Deficiency obj = new Deficiency();
                    obj.DeficiencyInfo = model.DeficiencyInfo;
                    obj.DeficiencyDescription = model.DeficiencyDescription;
                    obj.DeficiencyCategoryId = model.DeficiencyCategoryId;
                    if (model.DeficiencyCategoryId != 0)
                    {
                        var defCat = getDeficiencyCategoryById(obj.DeficiencyCategoryId ?? 0);
                        if (defCat != null) { obj.DeficiencyCategory = defCat.DeficiencyCategoryName; }
                    }
                    obj.ComponentId = model.ComponentId;
                    obj.ComponentDesc = model.ComponentDesc;
                    obj.ComponentDescShort = model.ComponentDescShort;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.Deficiencies.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editDeficiency(Deficiency model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.Deficiencies.Where(x => x.DeficiencyID == model.DeficiencyID).FirstOrDefault();
                    if (def != null)
                    {
                        def.DeficiencyInfo = model.DeficiencyInfo;
                        def.DeficiencyDescription = model.DeficiencyDescription;
                        def.DeficiencyCategoryId = model.DeficiencyCategoryId;
                        if (model.DeficiencyCategoryId != 0)
                        {
                            var defCat = getDeficiencyCategoryById(def.DeficiencyCategoryId ?? 0);
                            if (defCat != null) { def.DeficiencyCategory = defCat.DeficiencyCategoryName; }
                        }
                        def.ComponentId = model.ComponentId;
                        def.ComponentDesc = model.ComponentDesc;
                        def.ComponentDescShort = model.ComponentDescShort;
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeDeficiency(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Deficiencies.Where(x => x.DeficiencyID == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<Manufacturer> getAllManufacturer()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.Manufacturers.Where(x => x.IsActive == true).OrderBy(x => x.ManufacturerName).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }
        internal static List<Manufacturer> getAllManufacturerByComponent(Int32 ComponentId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var listManufactures = db.Manufacturers.Join(db.ComponentsManufacturers.Where(x => x.ComponentId == ComponentId), d => d.ManufacturerId, f => f.ManufacturerId, (d, f) => d).ToList();
                //var listManufactures = db.Manufacturers.Join(db.ComponentsManufacturers.Where(x => x.ComponentId == ComponentId)).ToList();

                //var list = db.Manufacturers.Where(x => x.IsActive == true).OrderBy(x => x.ManufacturerName).ToList();
                if (listManufactures.Count != 0) { return listManufactures; }
                return null;
            }
        }


        internal static Manufacturer getManufacturerById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Manufacturers.Where(x => x.IsActive == true && x.ManufacturerId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }
        internal static Manufacturer getManufacturerByName(string ManufacturerName)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Manufacturers.Where(x => x.IsActive == true && x.ManufacturerName == ManufacturerName).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveManufacturer(Manufacturer model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    Manufacturer obj = new Manufacturer();
                    obj.ManufacturerName = model.ManufacturerName;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.Manufacturers.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editManufacturer(Manufacturer model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.Manufacturers.Where(x => x.ManufacturerId == model.ManufacturerId).FirstOrDefault();
                    if (def != null)
                    {
                        def.ManufacturerName = model.ManufacturerName;
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeManufacturer(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Manufacturers.Where(x => x.ManufacturerId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static Component getComponentsManufacturerById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                string manufacturer = "";
                Component obj = new Component();
                var list = db.ComponentsManufacturers.Where(x => x.IsActive == true && x.ComponentId == id).ToList();
                if (list.Count != 0)
                {
                    foreach (var itm in list)
                    {
                        manufacturer += itm.ManufacturerId.ToString() + ',';
                    }
                    manufacturer = manufacturer.Remove(manufacturer.Length - 1, 1);
                    obj.ManufacturerId = manufacturer;
                    obj.ComponentId = id;
                    return obj;
                }
                return null;
            }
        }
        internal static ComponentPriceList GetComponentItemDetails(string ItemPartNo)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                if (ItemPartNo != null)
                {
                    if (ItemPartNo != "")
                    {
                        ItemPartNo = ItemPartNo.Trim();
                        ComponentPriceList obj = new ComponentPriceList();
                        var compnenetdata = db.ComponentPriceLists.Where(x => x.ItemPartNo == ItemPartNo).FirstOrDefault();
                        if (compnenetdata != null)
                        {
                            return compnenetdata;
                        }
                    }
                }
                return null;
            }
        }
        internal static ComponentPriceList GetComponentItemDetailsByDescription(string ComponentPriceDescription)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                if (ComponentPriceDescription != null)
                {
                    if (ComponentPriceDescription != "")
                    {
                        ComponentPriceDescription = ComponentPriceDescription.Trim();
                        ComponentPriceList obj = new ComponentPriceList();
                        var compnenetdata = db.ComponentPriceLists.Where(x => x.ComponentPriceDescription == ComponentPriceDescription).FirstOrDefault();
                        if (compnenetdata != null)
                        {
                            return compnenetdata;
                        }
                    }
                }
                return null;
            }
        }

        internal static List<ComponentViewModel> getAllComponent()
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    string manufacturer = "";
                    string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                    Uri url = new Uri(tmpURL);
                    string host = url.GetLeftPart(UriPartial.Authority);
                    List<ComponentViewModel> listObj = new List<ComponentViewModel>();
                    var list = db.Components.Where(x => x.IsActive == true).OrderBy(x => x.ComponentName).ToList();
                    if (list.Count != 0)
                    {
                        foreach (var d in list)
                        {
                            ComponentViewModel obj = new ComponentViewModel();
                            obj.ComponentId = d.ComponentId;
                            obj.ComponentName = d.ComponentName;
                            if (d.ComponentImage == "" || d.ComponentImage == null)
                            {
                                obj.ComponentImage = "";
                            }
                            else
                            {
                                obj.ComponentImage = host + "/img/component/" + d.ComponentImage;
                            }
                            var com = db.ComponentsManufacturers.Where(x => x.ComponentId == d.ComponentId && x.IsActive == true).ToList();
                            if (com.Count != 0)
                            {
                                foreach (var itm in com)
                                {
                                    var manu = getManufacturerById(Convert.ToInt16(itm.ManufacturerId));
                                    if (manu != null)
                                    {
                                        manufacturer += manu.ManufacturerName + ',';
                                    }
                                }
                                if (manufacturer != "")
                                {
                                    manufacturer = manufacturer.Remove(manufacturer.Length - 1, 1);
                                    obj.Manufacturer = manufacturer;
                                }
                                manufacturer = "";
                            }
                            listObj.Add(obj);
                        }
                        return listObj;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        internal static List<ComponentPriceListViewModel> getAllComponentPrice(long ComponentId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);
                List<ComponentPriceListViewModel> listObj = new List<ComponentPriceListViewModel>();
                var list = db.ComponentPriceLists.Where(x => x.IsActive == true && x.ComponentId == ComponentId).ToList();
                if (list.Count != 0)
                {
                    foreach (var d in list)
                    {
                        ComponentPriceListViewModel obj = new ComponentPriceListViewModel();
                        obj.ComponentPriceId = d.ComponentPriceId;
                        obj.ComponentId = d.ComponentId;
                        var objComponentName = db.Components.Where(x => x.IsActive == true && x.ComponentId == d.ComponentId).FirstOrDefault();
                        if (objComponentName != null)
                        {
                            obj.ComponentName = objComponentName.ComponentName;
                        }
                        else
                        {
                            obj.ComponentName = "";
                        }
                        obj.ManufacturerId = d.ManufacturerId;
                        obj.ComponentPrice = d.ComponentPrice;
                        obj.ComponentWeight = d.ComponentWeight;
                        obj.ComponentPriceDescription = d.ComponentPriceDescription;
                        obj.ComponentLabourTime = d.ComponentLabourTime;
                        obj.ItemPartNo = d.ItemPartNo;
                        obj.Surcharge = d.Surcharge;
                        obj.Markup = d.Markup;
                        obj.TotalPrice = d.TotalPrice;
                        var manu = getManufacturerById(Convert.ToInt16(d.ManufacturerId));
                        if (manu != null)
                        {
                            obj.ManufacturerName = manu.ManufacturerName;
                        }
                        else
                        {
                            obj.ManufacturerName = "";
                        }
                        listObj.Add(obj);
                    }
                }

                return listObj;
            }
            return null;
        }

        internal static List<ImpSettingsViewModel> getAllImportantSettings()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);
                List<ImpSettingsViewModel> listObj = new List<ImpSettingsViewModel>();
                var list = db.ImpSettings.ToList();
                if (list.Count != 0)
                {
                    foreach (var d in list)
                    {
                        ImpSettingsViewModel obj = new ImpSettingsViewModel();
                        obj.SettingID = d.SettingID;
                        obj.SettingType = d.SettingType;
                        obj.SettingValue = d.SettingValue;
                        listObj.Add(obj);
                    }
                }
                return listObj;
            }
            return null;
        }

        internal static string EditComponentPrice(ComponentPriceList model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var componentpricelist = db.ComponentPriceLists.Where(x => x.ComponentPriceId == model.ComponentPriceId).FirstOrDefault();
                    if (componentpricelist != null)
                    {
                        componentpricelist.ManufacturerId = model.ManufacturerId;
                        componentpricelist.ItemPartNo = model.ItemPartNo;
                        componentpricelist.ComponentPriceDescription = model.ComponentPriceDescription;
                        componentpricelist.ComponentPrice = model.ComponentPrice;
                        componentpricelist.ComponentWeight = model.ComponentWeight;
                        componentpricelist.Surcharge = model.Surcharge;
                        componentpricelist.Markup = model.Markup;
                        componentpricelist.TotalPrice = model.TotalPrice;
                        componentpricelist.ComponentLabourTime = model.ComponentLabourTime;
                        componentpricelist.IsActive = true;
                        componentpricelist.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        componentpricelist.ModifiedDate = DateTime.Now;
                        db.Entry(componentpricelist).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }

        internal static string DeleteComponentPrice(long Id)
        {
            string bSuccess = "";
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    db.SP_DELETE_ComponentPriceList(Id);
                    bSuccess = "Ok";
                }
                catch (Exception ex)
                {
                    bSuccess = ex.Message.ToString();
                }
            }
            return bSuccess;
        }
        internal static Component getComponentById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Components.Where(x => x.IsActive == true && x.ComponentId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static ComponentViewModel getComponentDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Components.Where(x => x.IsActive == true && x.ComponentId == id).FirstOrDefault();
                if (itm != null)
                {
                    ComponentViewModel obj = new ComponentViewModel();
                    obj.ComponentId = itm.ComponentId;
                    obj.ComponentName = itm.ComponentName;
                    obj.ComponentImage = itm.ComponentImage;
                    //if(itm.ManufacturerId != null || itm.ManufacturerId != 0)
                    //{
                    //   var manu =  getManufacturerById(Convert.ToInt16(itm.ManufacturerId)).ManufacturerName;
                    //    if (manu != null) { obj.Manufacturer = manu; }
                    //}
                    return obj;
                }
                return null;
            }
        }

        internal static ComponentPriceListViewModel getComponentPriceDetailsById(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {

                //ComponentPriceListViewModel objComponentPriceListViewModel = (from cpl in db.ComponentPriceLists
                //            join c in db.Components on cpl.ComponentId equals c.ComponentId
                //            join m in db.Manufacturers on cpl.ManufacturerId equals m.ManufacturerId
                //            where cpl.ComponentPriceId == id
                //            select new ComponentPriceListViewModel
                //            {   
                //                ComponentPriceId = cpl.ComponentPriceId,
                //                ComponentId = c.ComponentId,
                //                ComponentName = c.ComponentName,
                //                ManufacturerId = cpl.ManufacturerId,
                //                ManufacturerName = m.ManufacturerName,
                //                ItemPartNo = cpl.ItemPartNo,
                //                ComponentPriceDesc = cpl.ComponentPriceDesc,
                //                ComponentPrice =cpl.ComponentPrice,
                //                ComponentWeight =cpl.ComponentWeight
                //            }).SingleOrDefault(); 
                ComponentPriceListViewModel objComponentPriceListViewModel = (from cpl in db.ComponentPriceLists
                                                                              join c in db.Components on cpl.ComponentId equals c.ComponentId into componentGroup
                                                                              from c in componentGroup.DefaultIfEmpty()
                                                                              join m in db.Manufacturers on cpl.ManufacturerId equals m.ManufacturerId into manufacturerGroup
                                                                              from m in manufacturerGroup.DefaultIfEmpty()
                                                                              where cpl.ComponentPriceId == id
                                                                              select new ComponentPriceListViewModel
                                                                              {
                                                                                  ComponentPriceId = cpl.ComponentPriceId,
                                                                                  ComponentId = c != null ? c.ComponentId : 0, // Handle null cases
                                                                                  ComponentName = c != null ? c.ComponentName : string.Empty,
                                                                                  ManufacturerId = cpl.ManufacturerId,
                                                                                  ManufacturerName = m != null ? m.ManufacturerName : string.Empty,
                                                                                  ItemPartNo = cpl.ItemPartNo,
                                                                                  ComponentPriceDescription = cpl.ComponentPriceDescription,
                                                                                  ComponentPrice = cpl.ComponentPrice,
                                                                                  ComponentWeight = cpl.ComponentWeight,
                                                                                  Surcharge = cpl.Surcharge,
                                                                                  Markup = cpl.Markup,
                                                                                  TotalPrice = cpl.TotalPrice,
                                                                                  ComponentLabourTime = cpl.ComponentLabourTime
                                                                              }).SingleOrDefault();

                List<ComponentPriceListViewModelDetails> objComponentPriceListDetail = (from cpl in db.ComponentPriceListDetails
                                                                                        where cpl.ComponentPriceId == id
                                                                                        select new ComponentPriceListViewModelDetails
                                                                                        {
                                                                                            ComponentPriceId = cpl.ComponentPriceId,
                                                                                            ComponentPropertyTypeId = cpl.ComponentPropertyTypeId, // Handle null cases
                                                                                            ComponentPricePropertyTypeDescription = cpl.ComponentPricePropertyTypeDescription,
                                                                                            ComponentPricePropertyValue = cpl.ComponentPricePropertyValue
                                                                                        }).ToList();

                objComponentPriceListViewModel.ComponentPriceListViewModelDetails = objComponentPriceListDetail;

                return objComponentPriceListViewModel;
            }
        }

        internal static string saveComponent(Component model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    Component obj = new Component();
                    obj.ComponentName = model.ComponentName;
                    obj.ComponentImage = model.ComponentImage;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = model.CreatedBy;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = model.ModifiedBy;
                    db.Components.Add(obj);
                    db.SaveChanges();

                    ComponentsManufacturer cmObj = new ComponentsManufacturer();
                    string[] itm = model.ManufacturerId.Trim().Split(',');
                    foreach (var item in itm)
                    {
                        cmObj.ComponentId = obj.ComponentId;
                        cmObj.ManufacturerId = Convert.ToInt64(item);
                        cmObj.IsActive = true;
                        cmObj.CreatedDate = DateTime.Now;
                        cmObj.CreatedBy = model.CreatedBy;
                        cmObj.ModifiedDate = DateTime.Now;
                        cmObj.ModifiedBy = model.ModifiedBy;
                        db.ComponentsManufacturers.Add(cmObj);
                        db.SaveChanges();
                    }

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editComponent(Component model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.Components.Where(x => x.ComponentId == model.ComponentId).FirstOrDefault();
                    if (def != null)
                    {
                        def.ComponentName = model.ComponentName;
                        if (model.ComponentImage != null) { def.ComponentImage = model.ComponentImage; }
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();

                        ComponentsManufacturer cmObj = new ComponentsManufacturer();
                        string[] itm = model.ManufacturerId.Trim().Split(',');

                        var cm = db.ComponentsManufacturers.Where(x => x.ComponentId == model.ComponentId).ToList();
                        if (cm.Count != 0)
                        {
                            foreach (var d in cm)
                            {
                                d.IsActive = false;
                                d.ModifiedDate = DateTime.Now;
                                d.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                db.Entry(d).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            foreach (var item in itm)
                            {
                                if (item != null)
                                {
                                    long mId = Convert.ToInt64(item);
                                    var data = db.ComponentsManufacturers.Where(x => x.ManufacturerId == mId && x.ComponentId == model.ComponentId).FirstOrDefault();
                                    if (data != null)
                                    {
                                        data.IsActive = true;
                                        data.ModifiedDate = DateTime.Now;
                                        data.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                        db.Entry(data).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        cmObj.ComponentId = model.ComponentId;
                                        cmObj.ManufacturerId = Convert.ToInt64(item);
                                        cmObj.IsActive = true;
                                        cmObj.CreatedDate = DateTime.Now;
                                        cmObj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                        cmObj.ModifiedDate = DateTime.Now;
                                        cmObj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                        db.ComponentsManufacturers.Add(cmObj);
                                        db.SaveChanges();
                                    }
                                }

                            }
                        }
                        else
                        {
                            foreach (var item in itm)
                            {
                                if (item != null)
                                {
                                    cmObj.ComponentId = model.ComponentId;
                                    cmObj.ManufacturerId = Convert.ToInt64(item);
                                    cmObj.IsActive = true;
                                    cmObj.CreatedDate = DateTime.Now;
                                    cmObj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                    cmObj.ModifiedDate = DateTime.Now;
                                    cmObj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                    db.ComponentsManufacturers.Add(cmObj);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeComponent(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Components.Where(x => x.ComponentId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static ComponentPropertyTypeViewModel getComponentPropertyTypeById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ComponentPropertyTypes.Where(x => x.IsActive == true && x.ComponentPropertyTypeId == id).FirstOrDefault();
                if (itm != null)
                {

                    ComponentPropertyTypeViewModel obj = new ComponentPropertyTypeViewModel();
                    obj.ComponentPropertyTypeId = itm.ComponentPropertyTypeId;
                    obj.ComponentPropertyTypeName = itm.ComponentPropertyTypeName;
                    obj.ComponentPropertyTypeDesctiption = itm.ComponentPropertyTypeDesctiption;
                    obj.IsActive = itm.IsActive;
                    obj.CreatedDate = itm.CreatedDate;
                    obj.CreatedBy = itm.CreatedBy;
                    obj.ModifiedDate = itm.ModifiedDate;
                    obj.ModifiedBy = itm.ModifiedBy;
                    var compId = db.ComponentsProperties.Where(x => x.ComponentPropertyTypeId == itm.ComponentPropertyTypeId).FirstOrDefault();
                    if (compId != null)
                    {
                        obj.ComponentId = compId.ComponentId;
                    }
                    return obj;
                }
                return null;
            }
        }

        internal static int saveComponentPropertyType(ComponentPropertyTypeViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    ComponentPropertyType obj = new ComponentPropertyType();
                    obj.ComponentPropertyTypeName = model.ComponentPropertyTypeName;
                    obj.ComponentPropertyTypeDesctiption = model.ComponentPropertyTypeDesctiption;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.ComponentPropertyTypes.Add(obj);
                    db.SaveChanges();

                    ComponentsProperty cp = new ComponentsProperty();
                    cp.ComponentId = model.ComponentId;
                    cp.ComponentPropertyTypeId = obj.ComponentPropertyTypeId;
                    cp.IsActive = true;
                    cp.CreatedDate = DateTime.Now;
                    cp.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    cp.ModifiedDate = DateTime.Now;
                    cp.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.ComponentsProperties.Add(cp);
                    db.SaveChanges();

                    return obj.ComponentPropertyTypeId;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        internal static string editComponentPropertyType(ComponentPropertyTypeViewModel model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var item = db.ComponentPropertyTypes.Where(x => x.ComponentPropertyTypeId == model.ComponentPropertyTypeId).FirstOrDefault();
                    if (item != null)
                    {
                        item.ComponentPropertyTypeName = model.ComponentPropertyTypeName;
                        item.ComponentPropertyTypeDesctiption = model.ComponentPropertyTypeDesctiption;
                        item.IsActive = true;
                        item.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        item.ModifiedDate = DateTime.Now;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();

                        var cp = db.ComponentsProperties.Where(x => x.ComponentPropertyTypeId == model.ComponentPropertyTypeId).FirstOrDefault();
                        cp.ComponentId = model.ComponentId;
                        cp.ModifiedDate = DateTime.Now;
                        cp.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        db.Entry(cp).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeComponentPropertyType(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ComponentPropertyTypes.Where(x => x.ComponentPropertyTypeId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<ComponentPropertyValue> getAllComponentPropertyValues()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.ComponentPropertyValues.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static ComponentPropertyValue getComponentPropertyValuesById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ComponentPropertyValues.Where(x => x.IsActive == true && x.ComponentPropertyValueId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static ComponentPropertyValueViewModel getComponentPropertyValuesDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ComponentPropertyValues.Where(x => x.IsActive == true && x.ComponentPropertyValueId == id).FirstOrDefault();
                if (itm != null)
                {
                    ComponentPropertyValueViewModel vModel = new ComponentPropertyValueViewModel();
                    vModel.ComponentPropertyValue = itm.ComponentPropertyValue1;
                    if (itm.ComponentPropertyTypeId != 0)
                    {
                        vModel.ComponentPropertyType = getComponentPropertyTypeById(itm.ComponentPropertyTypeId).ComponentPropertyTypeName;
                    }
                    vModel.ComponentPropertyValueId = itm.ComponentPropertyValueId;
                    return vModel;
                }
                return null;
            }
        }

        internal static string saveComponentPropertyValues(ComponentPropertyValue model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    ComponentPropertyValue obj = new ComponentPropertyValue();
                    obj.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                    var found = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValue1 == model.ComponentPropertyValue1 && x.ComponentPropertyTypeId == model.ComponentPropertyTypeId).FirstOrDefault();
                    if (found == null)
                    {
                        obj.ComponentPropertyValue1 = model.ComponentPropertyValue1;
                        obj.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                        obj.IsActive = true;
                        obj.CreatedDate = DateTime.Now;
                        obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        obj.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        db.ComponentPropertyValues.Add(obj);
                        db.SaveChanges();
                        return model.ComponentPropertyTypeId.ToString();
                    }
                    return model.ComponentPropertyTypeId.ToString();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editComponentPropertyValues(ComponentPropertyValue model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var item = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == model.ComponentPropertyValueId).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.ComponentPropertyValue1 != model.ComponentPropertyValue1)
                        {
                            item.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                            item.ComponentPropertyValue1 = model.ComponentPropertyValue1;
                            item.IsActive = true;
                            item.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            item.ModifiedDate = DateTime.Now;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                            return "Ok";
                        }
                        return null;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeComponentPropertyValues(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<RackingType> getAllRackingType()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.RackingTypes.Where(x => x.IsActive == true).OrderBy(x => x.RackingTypeName).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static RackingType getRackingTypeById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.RackingTypes.Where(x => x.IsActive == true && x.RackingTypeId == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static string saveRackingType(RackingType model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    RackingType obj = new RackingType();
                    obj.RackingTypeName = model.RackingTypeName;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.RackingTypes.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editRackingType(RackingType model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.RackingTypes.Where(x => x.RackingTypeId == model.RackingTypeId).FirstOrDefault();
                    if (def != null)
                    {
                        def.RackingTypeName = model.RackingTypeName;
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeRackingType(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.RackingTypes.Where(x => x.RackingTypeId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getAllInspection()
        {
            if (HttpContext.Current.Session["LoggedInUserId"] != null)
            {
                Logger.Info("Get inspection function call from databasehelper after login from user id " + HttpContext.Current.Session["LoggedInUserId"].ToString() + " at " + System.DateTime.Now);
            }
            else
            {
                Logger.Info("Get inspection function call from databasehelper after login from user id at " + System.DateTime.Now);
            }

            using (DatabaseEntities db = new DatabaseEntities())
            {
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);

                List<InspectionViewModel> _listM = new List<InspectionViewModel>();

                var list = db.Inspections.Where(x => x.IsActive == true).OrderBy(x => x.InspectionStatus).ThenByDescending(x => x.InspectionDate).ToList();
                //var list = db.GetInspectionDetails().ToList();
                if (list.Count != 0)
                {
                    foreach (var d in list)
                    {
                        InspectionViewModel _list = new InspectionViewModel();
                        _list.InspectionId = d.InspectionId;
                        _list.InspectionDocumentNo = d.InspectionDocumentNo;
                        _list.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                        _list.InspectionType = d.InspectionType;
                        _list.InspectionDate = d.InspectionDate;
                        _list.Reportdate = d.Reportdate;
                        _list.InspectionStatus = d.InspectionStatus;
                        _list.InspectionStartedOn = d.InspectionStartedOn;
                        _list.InspectionEndOn = d.InspectionEndOn;
                        _list.EmployeeId = d.EmployeeId;
                        _list.CustomerId = d.CustomerId;
                        _list.CustomerLocationId = d.CustomerLocationId;
                        if (d.CustomerId != 0)
                        {
                            var cust = getCustomerById(d.CustomerId);
                            if (cust != null) { _list.Customer = cust.CustomerName; }
                            var customer = db.Customers.Where(x => x.CustomerId == d.CustomerId).FirstOrDefault();
                            if (customer != null)
                            {

                                if (customer.CustomerLogo != null)
                                {
                                    _list.CustomerLogo = host + "/img/logos/" + customer.CustomerLogo.Trim();
                                }
                                else
                                {
                                    _list.CustomerLogo = host + "/img/logos/defaultcompany.png";
                                }
                            }
                        }
                        if (d.CustomerLocationId != 0)
                        {
                            var loc = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationId));
                            if (loc != null) { _list.CustomerLocation = loc.LocationName; }
                        }
                        if (d.CustomerAreaID != 0)
                        {
                            var area = getAreaDetailsById(Convert.ToInt16(d.CustomerAreaID));
                            if (area != null) { _list.CustomerArea = area.AreaName; }
                        }
                        if (d.EmployeeId != 0)
                        {
                            var emp = getEmployeeById(Convert.ToInt16(d.EmployeeId));
                            if (emp != null) { _list.Employee = emp.EmployeeName; }
                        }
                        _list.InspectionPDFPath = d.InspectionPDFPath;
                        _list.CreatedDate = d.CreatedDate;
                        _listM.Add(_list);
                    }
                    return _listM;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getAllInspectionByEmployeeId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> _listM = new List<InspectionViewModel>();
                var id = Convert.ToInt16(HttpContext.Current.Session["LoggedInUserId"]);
                var employee = db.Employees.Where(x => x.UserID == id && x.IsActive == true).FirstOrDefault();
                if (employee != null)
                {
                    var list = db.Inspections.Where(x => x.IsActive == true && x.EmployeeId == employee.EmployeeID).OrderBy(x => x.InspectionStatus).ThenByDescending(x => x.InspectionDate).ToList();
                    if (list.Count != 0)
                    {
                        foreach (var d in list)
                        {
                            InspectionViewModel _list = new InspectionViewModel();
                            _list.InspectionId = d.InspectionId;
                            _list.InspectionDocumentNo = d.InspectionDocumentNo;
                            _list.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                            _list.InspectionType = d.InspectionType;
                            _list.InspectionDate = d.InspectionDate;
                            _list.Reportdate = d.Reportdate;
                            _list.InspectionStatus = d.InspectionStatus;
                            _list.InspectionStartedOn = d.InspectionStartedOn;
                            _list.InspectionEndOn = d.InspectionEndOn;
                            if (d.CustomerId != 0)
                            {
                                var cust = getCustomerById(d.CustomerId);
                                if (cust != null) { _list.Customer = cust.CustomerName; }
                            }
                            if (d.CustomerLocationId != 0)
                            {
                                var loc = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationId));
                                if (loc != null) { _list.CustomerLocation = loc.LocationName; }
                            }
                            if (d.CustomerAreaID != 0)
                            {
                                var area = getAreaDetailsById(Convert.ToInt16(d.CustomerAreaID));
                                if (area != null) { _list.CustomerArea = area.AreaName; }
                            }
                            if (d.EmployeeId != 0)
                            {
                                var emp = getEmployeeById(Convert.ToInt16(d.EmployeeId));
                                if (emp != null) { _list.Employee = emp.EmployeeName; }
                            }
                            _list.InspectionPDFPath = d.InspectionPDFPath;
                            _listM.Add(_list);
                        }
                        return _listM;
                    }
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> GetAllInspectionByCustomerIdInspectionStatus(int InspectionStatusId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> _listM = new List<InspectionViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        var list = getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();
                        foreach (var item in list)
                        {
                            if (item.Reportdate == null)
                            {
                                if (item.InspectionStartedOn != null)
                                {
                                    item.Reportdate = item.InspectionStartedOn;
                                }
                                else
                                {
                                    item.Reportdate = item.InspectionDate;
                                }
                            }
                            else
                            {
                                item.Reportdate = item.Reportdate;
                            }
                        }
                        var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus == InspectionStatusId).OrderByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4
                                                                                                                                                                                  //var itm = list.Where(x => x.CustomerId == customer.CustomerId).OrderBy(x => x.InspectionStatus).ThenByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4
                                                                                                                                                                                  //var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus != 1).OrderBy(x => x.Reportdate).ToList();
                        if (itm.Count != 0)
                        {
                            return itm;
                        }
                    }
                }
                return null;
            }
        }


        internal static List<InspectionViewModel> GetDueInspectionsCustomerFilters(FilterCustomerModel filters)
        {
            int InspectionStatusId = 3;
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> _listM = new List<InspectionViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                        Uri url = new Uri(tmpURL);
                        string host = url.GetLeftPart(UriPartial.Authority);
                        List<GetInspectionDueCustomerWithFilters_Result> list;
                        if (filters == null)
                        {
                            //5,NULL,0,NULL,0,0
                            list = db.GetInspectionDueCustomerWithFilters(customer.CustomerId, null, 0, null, 0, 0).ToList();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(filters.Location))
                            {
                                filters.Location = "0";
                            }
                            list = db.GetInspectionDueCustomerWithFilters(customer.CustomerId, filters.InspectionTypeId, filters.Province, filters.Region, filters.City, Convert.ToInt64(filters.Location)).ToList();// getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();                        
                        }

                        if (list.Count != 0)
                        {
                            foreach (var d in list)
                            {
                                InspectionViewModel _list = new InspectionViewModel();
                                _list.InspectionId = d.InspectionId;
                                _list.InspectionDocumentNo = d.InspectionDocumentNo;
                                _list.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                                _list.InspectionType = d.InspectionType;
                                _list.InspectionDate = d.InspectionDate;
                                if (d.Reportdate == null)
                                {
                                    if (d.InspectionStartedOn != null)
                                    {
                                        _list.Reportdate = d.InspectionStartedOn;
                                    }
                                    else
                                    {
                                        _list.Reportdate = d.InspectionDate;
                                    }
                                }
                                else
                                {
                                    _list.Reportdate = d.Reportdate;
                                }
                                _list.InspectionStatus = d.InspectionStatus;
                                _list.InspectionStartedOn = d.InspectionStartedOn;
                                _list.InspectionEndOn = d.InspectionEndOn;
                                _list.EmployeeId = d.EmployeeId;
                                _list.CustomerId = d.CustomerId;
                                _list.Customer = d.Customer;
                                if (d.CustomerLogo != null)
                                {
                                    _list.CustomerLogo = host + "/img/logos/" + d.CustomerLogo.Trim();
                                }
                                else
                                {
                                    _list.CustomerLogo = host + "/img/logos/defaultcompany.png";
                                }
                                _list.CustomerLocation = d.CustomerLocation;
                                _list.Region = d.Region;
                                _list.CustomerArea = d.CustomerArea;
                                _list.Employee = d.Employee;

                                _list.InspectionPDFPath = d.InspectionPDFPath;
                                _list.CreatedDate = d.CreatedDate;
                                _listM.Add(_list);
                            }
                        }


                        _listM = _listM.OrderByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                        if (_listM.Count != 0)
                        {
                            return _listM;
                        }
                    }
                    else
                    {

                    }
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> GetAllInspectionsCustomerFilters(FilterCustomerModel filters)
        {
            int InspectionStatusId = 3;
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> _listM = new List<InspectionViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                string sInSeparatedStatuses = null;
                if (filters != null)
                {
                    if (filters.SelectedStatuses.Count > 0)
                    {
                        sInSeparatedStatuses = string.Join(",", filters.SelectedStatuses);
                    }
                }

                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                        Uri url = new Uri(tmpURL);
                        string host = url.GetLeftPart(UriPartial.Authority);
                        List<GetInspectionCustomerWithFilters_Result> list;
                        if (filters == null)
                        {
                            //5,NULL,0,NULL,0,0
                            list = db.GetInspectionCustomerWithFilters(customer.CustomerId, null, 0, null, 0, 0, null).ToList();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(filters.Location))
                            {
                                filters.Location = "0";
                            }
                            list = db.GetInspectionCustomerWithFilters(customer.CustomerId, filters.InspectionTypeId, filters.Province, filters.Region, filters.City, Convert.ToInt64(filters.Location), sInSeparatedStatuses).ToList();// filters.SelectedStatuses getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();                        
                        }

                        if (list.Count != 0)
                        {
                            foreach (var d in list)
                            {
                                InspectionViewModel _list = new InspectionViewModel();
                                _list.InspectionId = d.InspectionId;
                                _list.InspectionDocumentNo = d.InspectionDocumentNo;
                                _list.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                                _list.InspectionType = d.InspectionType;
                                _list.InspectionDate = d.InspectionDate;
                                if (d.Reportdate == null)
                                {
                                    if (d.InspectionStartedOn != null)
                                    {
                                        _list.Reportdate = d.InspectionStartedOn;
                                    }
                                    else
                                    {
                                        _list.Reportdate = d.InspectionDate;
                                    }
                                }
                                else
                                {
                                    _list.Reportdate = d.Reportdate;
                                }
                                _list.InspectionStatus = d.InspectionStatus;
                                _list.InspectionStartedOn = d.InspectionStartedOn;
                                _list.InspectionEndOn = d.InspectionEndOn;
                                _list.EmployeeId = d.EmployeeId;
                                _list.CustomerId = d.CustomerId;
                                _list.Customer = d.Customer;
                                if (d.CustomerLogo != null)
                                {
                                    _list.CustomerLogo = host + "/img/logos/" + d.CustomerLogo.Trim();
                                }
                                else
                                {
                                    _list.CustomerLogo = host + "/img/logos/defaultcompany.png";
                                }
                                _list.CustomerLocation = d.CustomerLocation;
                                _list.Region = d.Region;
                                _list.CustomerArea = d.CustomerArea;
                                _list.Employee = d.Employee;

                                _list.InspectionPDFPath = d.InspectionPDFPath;
                                _list.CreatedDate = d.CreatedDate;
                                _listM.Add(_list);
                            }
                        }


                        _listM = _listM.OrderByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                        if (_listM.Count != 0)
                        {
                            return _listM;
                        }
                    }
                    else
                    {
                        string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                        Uri url = new Uri(tmpURL);
                        string host = url.GetLeftPart(UriPartial.Authority);
                        List<GetInspectionCustomerUserWithFilters_Result> list;
                        var locationContactIds = db.CustomerLocationContacts
                            .Where(clc => clc.UserID == userId)
                            .Select(clc => clc.LocationContactId)
                            .ToList();

                        if (locationContactIds.Any())
                        {
                            var customerLocationData = db.CustomersLocationsUsers
    .Where(clu => locationContactIds.Contains(clu.LocationContactId))
    .GroupBy(clu => clu.CustomerId)
    .Select(group => new
    {
        CustomerId = group.Key,
        CustomerLocationIds = group.Select(clu => clu.CustomerLocationID).Distinct().ToList()
    })
    .ToList();

                            if (customerLocationData != null)
                            {
                                string customerLocationIdsStr = customerLocationData[0].CustomerLocationIds.Count > 0 ? string.Join(",", customerLocationData[0].CustomerLocationIds) : null;
                                if (filters == null)
                                {

                                    list = db.GetInspectionCustomerUserWithFilters(customerLocationData[0].CustomerId, null, 0, null, 0, customerLocationIdsStr, null).ToList();
                                }
                                else
                                {
                                    //if (string.IsNullOrWhiteSpace(filters.Location))
                                    //{
                                    //    filters.Location = "0";
                                    //}                                    
                                    list = db.GetInspectionCustomerUserWithFilters(customerLocationData[0].CustomerId, filters.InspectionTypeId, filters.Province, filters.Region, filters.City, customerLocationIdsStr, sInSeparatedStatuses).ToList();// filters.SelectedStatuses getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();                        
                                }
                                if (list.Count != 0)
                                {
                                    foreach (var d in list)
                                    {
                                        InspectionViewModel _list = new InspectionViewModel();
                                        _list.InspectionId = d.InspectionId;
                                        _list.InspectionDocumentNo = d.InspectionDocumentNo;
                                        _list.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                                        _list.InspectionType = d.InspectionType;
                                        _list.InspectionDate = d.InspectionDate;
                                        if (d.Reportdate == null)
                                        {
                                            if (d.InspectionStartedOn != null)
                                            {
                                                _list.Reportdate = d.InspectionStartedOn;
                                            }
                                            else
                                            {
                                                _list.Reportdate = d.InspectionDate;
                                            }
                                        }
                                        else
                                        {
                                            _list.Reportdate = d.Reportdate;
                                        }
                                        _list.InspectionStatus = d.InspectionStatus;
                                        _list.InspectionStartedOn = d.InspectionStartedOn;
                                        _list.InspectionEndOn = d.InspectionEndOn;
                                        _list.EmployeeId = d.EmployeeId;
                                        _list.CustomerId = d.CustomerId;
                                        _list.Customer = d.Customer;
                                        if (d.CustomerLogo != null)
                                        {
                                            _list.CustomerLogo = host + "/img/logos/" + d.CustomerLogo.Trim();
                                        }
                                        else
                                        {
                                            _list.CustomerLogo = host + "/img/logos/defaultcompany.png";
                                        }
                                        _list.CustomerLocation = d.CustomerLocation;
                                        _list.Region = d.Region;
                                        _list.CustomerArea = d.CustomerArea;
                                        _list.Employee = d.Employee;

                                        _list.InspectionPDFPath = d.InspectionPDFPath;
                                        _list.CreatedDate = d.CreatedDate;
                                        _listM.Add(_list);
                                    }
                                }
                                _listM = _listM.OrderByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                                if (_listM.Count != 0)
                                {
                                    return _listM;
                                }
                            }
                        }
                    }
                }
                return null;
            }
        }


        internal static List<InspectionCloneViewModel> GetInspectionDataClone(int id)
        {
            List<InspectionCloneViewModel> _listFinal = new List<InspectionCloneViewModel>();
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {


                    List<GetAllInspectionTobeCloned_Result> list;
                    if (id != 0)
                    {
                        //5,NULL,0,NULL,0,0
                        list = db.GetAllInspectionTobeCloned(id).ToList();
                    }
                    else
                    {
                        return _listFinal;
                    }
                    if (list.Count != 0)
                    {
                        foreach (var d in list)
                        {
                            InspectionCloneViewModel _list = new InspectionCloneViewModel();
                            _list.InspectionId = d.InspectionId;
                            _list.InspectionDocumentNo = d.InspectionDocumentNo;
                            _list.InspectionType = d.InspectionType;
                            _list.InspectionDate = d.InspectionDate;
                            _list.InspectionDateFormatted = d.InspectionDate.ToString("yyyy-MM-ddTHH:mm:ss");

                            _list.InspectionStatus = d.InspectionStatus;
                            _list.InspectionStatusName = d.InspectionStatusName;
                            _list.CustomerLocation = Convert.ToString(d.CustomerLocationId);

                            _listFinal.Add(_list);
                        }
                    }


                    _listFinal = _listFinal.OrderByDescending(x => x.InspectionDate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                    if (_listFinal.Count != 0)
                    {
                        return _listFinal;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

                return _listFinal;

            }

        }


        internal static int InspectionClone(int idTarget, int idSource)
        {
            int iI = 0;
            using (DatabaseEntities db = new DatabaseEntities())
            {

                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    iI = db.CloneInspectionData(idSource, idTarget, userId);
                }
                return iI;
            }
        }

        internal static CustomerLocationHistoryLegacyFile getCustomerLocationHistoryLegacyFile(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.CustomerLocationHistoryLegacyFiles.Where(x => x.CustomerLocationHistoryLegacyFileId == id).FirstOrDefault();
                if (itm != null)
                {
                    CustomerLocationHistoryLegacyFile obj = new CustomerLocationHistoryLegacyFile();
                    obj.CustomerLocationHistoryLegacyFileId = itm.CustomerLocationHistoryLegacyFileId;
                    obj.FileDrawingName = itm.FileDrawingName;
                    obj.FileCategory = itm.FileCategory;
                    //if(itm.ManufacturerId != null || itm.ManufacturerId != 0)
                    //{
                    //   var manu =  getManufacturerById(Convert.ToInt16(itm.ManufacturerId)).ManufacturerName;
                    //    if (manu != null) { obj.Manufacturer = manu; }
                    //}
                    return obj;
                }
                return null;
            }
        }
        internal static List<CustomerLocationHistoryLegacyFileListing> GetAllCustomerDocumentsWithFilters(FilterFilesModel filters)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<CustomerLocationHistoryLegacyFileListing> _listM = new List<CustomerLocationHistoryLegacyFileListing>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                string sDocumentTypes = null;
                if (filters != null)
                {
                    if (filters.DocumentTypeList != null)
                    {
                        if (filters.DocumentTypeList.Count > 0)
                        {
                            sDocumentTypes = string.Join(",", filters.DocumentTypeList);
                        }
                    }
                }

                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        int iInspectionDocs = 0;
                        int iHistoricalDocs = 0;

                        string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                        Uri url = new Uri(tmpURL);
                        string host = url.GetLeftPart(UriPartial.Authority);
                        List<GetCustomerDocumentsWithFilters_Result> list;
                        if (filters == null)
                        {
                            //5,NULL,0,NULL,0,0
                            list = db.GetCustomerDocumentsWithFilters(customer.CustomerId, 0, 0, 0, null, 0, 0, null).ToList();
                        }
                        else
                        {
                            if (filters.InspectionDocs == true)
                            {
                                iInspectionDocs = 1;
                            }
                            if (filters.HistoricalDocs == true)
                            {
                                iHistoricalDocs = 1;
                            }
                            list = db.GetCustomerDocumentsWithFilters(customer.CustomerId, iInspectionDocs, iHistoricalDocs, filters.Province, filters.Region, filters.City, Convert.ToInt64(filters.Location), sDocumentTypes).ToList();// filters.SelectedStatuses getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();                        
                        }

                        if (list.Count != 0)
                        {
                            foreach (var d in list)
                            {
                                CustomerLocationHistoryLegacyFileListing _list = new CustomerLocationHistoryLegacyFileListing();
                                _list.InspectionDocumentNo = d.InspectionDocumentNo;
                                _list.CustomerLocationName = d.CustomerLocationName;
                                _list.Region = d.Region;
                                _list.FileDrawingPath = host + d.CustFilePath;
                                _list.FileDrawingName = d.CustFilename;
                                _list.FileCategory = d.CustFileCategory;
                                _listM.Add(_list);
                            }
                        }

                        _listM = _listM.OrderByDescending(x => x.InspectionDocumentNo).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                        if (_listM.Count != 0)
                        {
                            return _listM;
                        }
                    }
                    else
                    {
                        int iInspectionDocs = 0;
                        int iHistoricalDocs = 0;

                        string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                        Uri url = new Uri(tmpURL);
                        string host = url.GetLeftPart(UriPartial.Authority);
                        List<GetCustomerUsersDocumentsWithFilters_Result> list;
                        var locationContactIds = db.CustomerLocationContacts
                            .Where(clc => clc.UserID == userId)
                            .Select(clc => clc.LocationContactId)
                            .ToList();

                        if (locationContactIds.Any())
                        {
                            var customerLocationData = db.CustomersLocationsUsers
                            .Where(clu => locationContactIds.Contains(clu.LocationContactId))
                            .GroupBy(clu => clu.CustomerId)
                            .Select(group => new
                            {
                                CustomerId = group.Key,
                                CustomerLocationIds = group.Select(clu => clu.CustomerLocationID).Distinct().ToList()
                            })
                            .ToList();

                            if (customerLocationData != null)
                            {
                                string customerLocationIdsStr = customerLocationData[0].CustomerLocationIds.Count > 0 ? string.Join(",", customerLocationData[0].CustomerLocationIds) : null;
                                if (filters == null)
                                {

                                    list = db.GetCustomerUsersDocumentsWithFilters(customerLocationData[0].CustomerId, 0, 0, 0, null, 0, customerLocationIdsStr, null).ToList();
                                }
                                else
                                {
                                    if (filters.InspectionDocs == true)
                                    {
                                        iInspectionDocs = 1;
                                    }
                                    if (filters.HistoricalDocs == true)
                                    {
                                        iHistoricalDocs = 1;
                                    }
                                    //if (string.IsNullOrWhiteSpace(filters.Location))
                                    //{
                                    //    filters.Location = "0";
                                    //}                                    
                                    list = db.GetCustomerUsersDocumentsWithFilters(customerLocationData[0].CustomerId, iInspectionDocs, iHistoricalDocs, filters.Province, filters.Region, filters.City, customerLocationIdsStr, sDocumentTypes).ToList();// filters.SelectedStatuses getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();                        
                                }
                                if (list.Count != 0)
                                {
                                    foreach (var d in list)
                                    {
                                        CustomerLocationHistoryLegacyFileListing _list = new CustomerLocationHistoryLegacyFileListing();
                                        _list.InspectionDocumentNo = d.InspectionDocumentNo;
                                        _list.CustomerLocationName = d.CustomerLocationName;
                                        _list.Region = d.Region;
                                        _list.FileDrawingPath = host + d.CustFilePath;
                                        _list.FileDrawingName = d.CustFilename;
                                        _list.FileCategory = d.CustFileCategory;
                                        _listM.Add(_list);
                                    }
                                }

                                _listM = _listM.OrderByDescending(x => x.InspectionDocumentNo).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                                if (_listM.Count != 0)
                                {
                                    return _listM;
                                }
                            }
                        }
                    }
                }
                return null;
            }
        }
        internal static List<InspectionViewModel> getAllInspectionByCustomerId(int InspectionStatusId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> _listM = new List<InspectionViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                var loggedInUserType = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserType"]);

                // Ensure that only users with type 4 or 9 can proceed
                if (loggedInUserType != 4 && loggedInUserType != 9 || userId == 0)
                {
                    return null;
                }

                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        var list = getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();
                        foreach (var item in list)
                        {
                            if (item.Reportdate == null)
                            {
                                if (item.InspectionStartedOn != null)
                                {
                                    item.Reportdate = item.InspectionStartedOn;
                                }
                                else
                                {
                                    item.Reportdate = item.InspectionDate;
                                }
                            }
                            else
                            {
                                item.Reportdate = item.Reportdate;
                            }
                        }
                        var itm = list.Where(x => x.CustomerId == customer.CustomerId).OrderByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId
                                                                                                                                      //var itm = list.Where(x => x.CustomerId == customer.CustomerId).OrderBy(x => x.InspectionStatus).ThenByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4
                                                                                                                                      //var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus != 1).OrderBy(x => x.Reportdate).ToList();
                        if (itm.Count != 0)
                        {
                            return itm;
                        }
                    }
                    else
                    {
                        var locationContactIds = db.CustomerLocationContacts
                            .Where(clc => clc.UserID == userId)
                            .Select(clc => clc.LocationContactId)
                            .ToList();

                        if (locationContactIds.Any())
                        {
                            var customerLocationIds = db.CustomersLocationsUsers
                                .Where(clu => locationContactIds.Contains(clu.LocationContactId))
                                .Select(clu => clu.CustomerLocationID)
                                .Distinct()
                                .ToList();

                            var list = getAllInspection();
                            var inspections = list
                                .Where(i => customerLocationIds.Contains(i.CustomerLocationId)).ToList();

                            foreach (var item in inspections)
                            {
                                if (item.Reportdate == null)
                                {
                                    if (item.InspectionStartedOn != null)
                                    {
                                        item.Reportdate = item.InspectionStartedOn;
                                    }
                                    else
                                    {
                                        item.Reportdate = item.InspectionDate;
                                    }
                                }
                                else
                                {
                                    item.Reportdate = item.Reportdate;
                                }
                            }
                            var itm = list.OrderByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId
                                                                                          //var itm = list.Where(x => x.CustomerId == customer.CustomerId).OrderBy(x => x.InspectionStatus).ThenByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4
                                                                                          //var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus != 1).OrderBy(x => x.Reportdate).ToList();
                            if (itm.Count != 0)
                            {
                                return itm;
                            }
                        }
                    }
                }
                return null;
            }
        }
        internal static List<InspectionViewModel> getAllInspectionDueByCustomerId(int InspectionStatusId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> _listM = new List<InspectionViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        var list = getAllInspection();// db.GetInspectionDetailsByCustomers(customer.CustomerId, // getAllInspection();
                        foreach (var item in list)
                        {
                            if (item.Reportdate == null)
                            {
                                if (item.InspectionStartedOn != null)
                                {
                                    item.Reportdate = item.InspectionStartedOn;
                                }
                                else
                                {
                                    item.Reportdate = item.InspectionDate;
                                }
                            }
                            else
                            {
                                item.Reportdate = item.Reportdate;
                            }
                        }
                        var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus == 1).OrderByDescending(x => x.Reportdate).ToList(); // x.InspectionStatus > InspectionStatusId
                                                                                                                                                                 //var itm = list.Where(x => x.CustomerId == customer.CustomerId).OrderBy(x => x.InspectionStatus).ThenByDescending(x => x.Reportdate).ToList(); //&& x.InspectionStatus >= 4
                                                                                                                                                                 //var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus != 1).OrderBy(x => x.Reportdate).ToList();
                        if (itm.Count != 0)
                        {
                            return itm;
                        }
                    }
                }
                return null;
            }
        }

        internal static void processImages()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                string folderPath = @"D:\Projects\CamIndustrial\Code\Code\CamV4\img\in";
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);
                //List<InspectionDeficiencyPhoto> AllImages = db.InspectionDeficiencyPhotoes.ToList();
                string[] imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
            .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                           file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                           file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
            .ToArray();

                foreach (var item in imageFiles)
                {
                    string strPath = host + "/img/in/deficiencythumb/" + item;// InspectionDeficiencyPhotoPath;
                    string TempOutputPath = Path.Combine(HostingEnvironment.MapPath("~/img/out/"), item); // item.InspectionDeficiencyPhotoPath);
                                                                                                          //if (!File.Exists(TempOutputPath))
                                                                                                          //{
                    byte[] imageBytes = CreateThumbnail(item, 100, 100); //item.InspectionDeficiencyPhotoPath
                    if (imageBytes != null)
                    {
                        var outputPath = Path.Combine(host + "/img/in/deficiencythumb/", item);// item.InspectionDeficiencyPhotoPath);
                        File.WriteAllBytes(TempOutputPath, imageBytes);
                    }
                    //}
                }
            }
        }
        internal static InspectionViewModel getInspectionDetailsForSheet(long id)
        {
            //if (HttpContext.Current.Session["LoggedInUserId"] == null)
            //{
            //    return Redirect("/Account/Login");
            //    //return RedirectToAction("Login", "Account");
            //}
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    InspectionViewModel _list = new InspectionViewModel();
                    List<InspectionDeficiencyViewModel> iDefList = new List<InspectionDeficiencyViewModel>();

                    List<QuotationInspectionDeficiency> iDefQuotation = new List<QuotationInspectionDeficiency>();
                    List<InspectionDeficiencyMTOViewModel> iMTOList = new List<InspectionDeficiencyMTOViewModel>();
                    List<Deficiency> iListConclusionandRecommendationsList = new List<Deficiency>();
                    string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                    Uri url = new Uri(tmpURL);
                    string host = url.GetLeftPart(UriPartial.Authority);
                    string CustomerFullAddress = "";
                    List<string> FullAddress = new List<string>();
                    var list = db.Inspections.Where(x => x.InspectionId == id && x.IsActive == true).FirstOrDefault();
                    if (list != null)
                    {
                        _list.InspectionId = list.InspectionId;
                        _list.InspectionDocumentNo = list.InspectionDocumentNo;
                        _list.InspectionDocumentNoRef = list.InspectionDocumentNoRef;
                        list.FacilitiesAreasIds = RemoveDuplicates(list.FacilitiesAreasIds);
                        list.ProcessOverviewIds = RemoveDuplicates(list.ProcessOverviewIds);

                        var sInspectionType = getInspectionTypeByCode(list.InspectionType);
                        if (sInspectionType != null)
                        {
                            _list.InspectionType = sInspectionType.InspectionTypeName;
                        }
                        else
                        {
                            _list.InspectionType = "Rack Inspection";
                        }
                        _list.InspectionDate = list.InspectionDate;
                        _list.Reportdate = list.Reportdate;
                        _list.InspectionStatus = list.InspectionStatus;
                        var objInspStatus = db.InspectionStatus.Where(x => x.InspectionStatusId == list.InspectionStatus).FirstOrDefault();
                        if (objInspStatus != null)
                        {
                            _list.InspectionStatusName = objInspStatus.InspectionStatus;
                        }
                        else
                        {
                            _list.InspectionStatusName = "";
                        }

                        _list.CADDocuments = list.CADDocuments;
                        _list.StampingEngineerId = list.StampingEngineerId;

                        if (list.CustomerLocationId != 0)
                        {
                            _list.CustomerLocationId = list.CustomerLocationId;
                            var loc = getCustomerLocationById(Convert.ToInt16(list.CustomerLocationId));
                            if (loc != null)
                            {
                                _list.CustomerLocation = loc.LocationName;
                                FullAddress.Add(loc.LocationName);
                                if (loc.CustomerAddress != null)
                                {
                                    FullAddress.Add(loc.CustomerAddress);
                                    //FullAddress.Add(loc.Pincode);
                                    _list.custModel.CustomerAddress = loc.CustomerAddress;
                                }
                            }

                        }
                        else
                        {
                            _list.CustomerLocationId = 0;
                            _list.CustomerLocation = "";
                        }

                        if (list.CustomerAreaID != 0)
                        {
                            _list.CustomerAreaID = list.CustomerAreaID;
                            var area = getAreaDetailsById(Convert.ToInt16(list.CustomerAreaID));
                            if (area != null)
                            {
                                _list.CustomerArea = area.AreaName;
                                FullAddress.Add(area.AreaName);
                            }
                        }
                        else
                        {
                            _list.CustomerAreaID = 0;
                            _list.CustomerArea = "";
                        }

                        if (list.CustomerId != 0)
                        {
                            var cust = getCustomerById(list.CustomerId);
                            if (cust != null)
                            {
                                _list.Customer = cust.CustomerName;
                                _list.custModel.CustomerName = cust.CustomerName;
                                _list.custModel.CustomerId = cust.CustomerId;
                                if (_list.custModel.CustomerAddress == "" || _list.custModel.CustomerAddress == null)
                                {
                                    _list.custModel.CustomerAddress = cust.CustomerAddress;
                                    if (cust.CustomerAddress != null)
                                    {
                                        FullAddress.Add(cust.CustomerAddress);
                                    }
                                }
                                _list.custModel.CustomerLogo = cust.CustomerLogo;
                                _list.custModel.CustomerEmail = cust.CustomerEmail;
                                _list.custModel.CustomerContactName = cust.CustomerContactName;
                            }
                        }



                        CustomerFullAddress = string.Join(",", FullAddress);
                        _list.CustomerFullAddress = CustomerFullAddress;

                        if (list.EmployeeId != 0)
                        {
                            var emp = getEmployeeById(Convert.ToInt16(list.EmployeeId));
                            if (emp != null)
                            {
                                _list.Employee = emp.EmployeeName;
                                _list.empModel.EmployeeID = emp.EmployeeID;
                                _list.empModel.EmployeeName = emp.EmployeeName;
                                _list.empModel.TitleDegrees = emp.TitleDegrees;
                                _list.empModel.EmployeeEmail = emp.EmployeeEmail;
                                _list.empModel.MobileNo = emp.MobileNo;
                            }
                        }
                        if (list.StampingEngineerId != 0)
                        {
                            var emp = getEmployeeById(Convert.ToInt16(list.StampingEngineerId));
                            if (emp != null)
                            {
                                _list.empStampingEngModel.EmployeeID = emp.EmployeeID;
                                _list.empStampingEngModel.EmployeeName = emp.EmployeeName;
                                _list.empStampingEngModel.TitleDegrees = emp.TitleDegrees;
                                _list.empStampingEngModel.EmployeeEmail = emp.EmployeeEmail;
                                _list.empStampingEngModel.MobileNo = emp.MobileNo;
                            }
                        }
                        _list.CustomerContactIds = list.CustomerContactIds;
                        if (list.CustomerContactIds != null)
                        {
                            list.CustomerContactIds = list.CustomerContactIds.Trim();
                            if (list.CustomerContactIds.Length != 0)
                            {
                                List<CustomerLocationContactViewModel> objListContacts = new List<CustomerLocationContactViewModel>();

                                //CustomerLocationId
                                objListContacts = getLocationContactDetailsByLocationId(list.CustomerLocationId);
                                string fAreaName = "";
                                if (objListContacts != null)
                                {
                                    list.CustomerContactIds = list.CustomerContactIds.Trim();
                                    string[] items = list.CustomerContactIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var f in items)
                                    {
                                        if (f != null)
                                        {
                                            long fId = Convert.ToInt32(f);
                                            objListContacts.Where(w => w.LocationContactId == fId).ToList().ForEach(s => s.Selected = true);
                                            //if (item.LocationContactId == fId)
                                            //{
                                            //    item.Selected = true;
                                            //}
                                        }
                                    }
                                }
                                _list.ListCustomerLocationContacts = objListContacts;
                                //_list.InspectionFacilitiesArea = null;
                                _list.FacilitiesAreasIds = list.FacilitiesAreasIds;
                                _list.FacilitiesAreas = fAreaName;
                            }
                        }
                        else
                        {
                            List<CustomerLocationContactViewModel> objListContacts = new List<CustomerLocationContactViewModel>();
                            objListContacts = getLocationContactDetailsByLocationIdBoth(list.CustomerId, list.CustomerLocationId);
                            _list.ListCustomerLocationContacts = objListContacts;
                            //_list.InspectionFacilitiesArea = null;
                            _list.FacilitiesAreasIds = list.FacilitiesAreasIds;
                            //_list.FacilitiesAreas = fAreaName;
                        }

                        if (list.FacilitiesAreasIds.Length != 0)
                        {
                            List<FacilitiesArea> objListfacilitiesAreas = new List<FacilitiesArea>();
                            string fAreaName = "";
                            string[] items = list.FacilitiesAreasIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
                                        fAreaName += String.Concat(fac.FacilitiesAreaName + ",");
                                        objListfacilitiesAreas.Add(fac);

                                    }
                                }
                            }
                            _list.InspectionFacilitiesArea = objListfacilitiesAreas;
                            //_list.InspectionFacilitiesArea = null;
                            _list.FacilitiesAreasIds = list.FacilitiesAreasIds;
                            _list.FacilitiesAreas = fAreaName;
                        }
                        if (list.ProcessOverviewIds.Length != 0)
                        {
                            List<ProcessOverview> objListProcessOverview = new List<ProcessOverview>();

                            var pOverName = "";
                            string[] overview = list.ProcessOverviewIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            _list.CapacityTable = 0;
                            _list.PlanElevationDrawing = 0;
                            foreach (var h in overview)
                            {
                                if (h != null)
                                {
                                    int pId = Convert.ToInt16(h);
                                    var pro = db.ProcessOverviews.Where(x => x.ProcessOverviewId == pId && x.IsActive == true).FirstOrDefault();
                                    if (pro != null)
                                    {
                                        if (pId == 9)
                                        {
                                            _list.PlanElevationDrawing = 1;
                                        }
                                        if (pId == 10)
                                        {
                                            _list.CapacityTable = 1;
                                        }
                                        pOverName += String.Concat(pro.ProcessOverviewDesc + ";");
                                        objListProcessOverview.Add(pro);
                                    }
                                }
                            }
                            _list.InspectionProcessOverview = objListProcessOverview;
                            _list.ProcessOverviewsIds = list.ProcessOverviewIds;
                            _list.ProcessOverviews = pOverName;
                        }
                        if (list.ReferenceDocumentIds != null)
                        {
                            if (list.ReferenceDocumentIds.Length != 0)
                            {
                                List<DocumentTitle> objListDocumentTitle = new List<DocumentTitle>();

                                var dTitleName = "";
                                string[] document = list.ReferenceDocumentIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var t in document)
                                {
                                    if (t != null)
                                    {
                                        int tId = Convert.ToInt16(t);
                                        var title = db.DocumentTitles.Where(x => x.DocumentId == tId && x.IsActive == true).FirstOrDefault();
                                        if (title != null)
                                        {
                                            dTitleName += String.Concat(title.DocumentDescription + ";");
                                            objListDocumentTitle.Add(title);
                                        }
                                    }
                                }
                                _list.InspectionDocumentTitle = objListDocumentTitle;
                                _list.ReferenceDocumentIds = list.ReferenceDocumentIds;
                                _list.ReferenceDocuments = dTitleName;
                            }
                        }

                        List<InspectionFileDrawing> objDrawinglist = db.InspectionFileDrawings.Where(d => d.InspectionId == _list.InspectionId && d.IsDeleted == 0 || d.IsDeleted == null).ToList();
                        List<InspectionFileDrawingViewModel> objParentViewList = new List<InspectionFileDrawingViewModel>();

                        var listPath = new List<InspectionFileDrawingViewModel>();
                        //var path = objDrawinglist.GroupBy(x => x.FileCategory).Select(d => new { FileDrawingNames = string.Join(",", d.FileDrawingName }).ToList();
                        var path = objDrawinglist.GroupBy(x => x.FileCategory).ToList();
                        foreach (var items in path)
                        {
                            List<InspectionFileDrawingChildViewModel> objChildViewList = new List<InspectionFileDrawingChildViewModel>();
                            InspectionFileDrawingViewModel objParent = new InspectionFileDrawingViewModel();
                            objParent.FileCategory = items.Key;
                            foreach (var item in items)
                            {
                                InspectionFileDrawingChildViewModel objChild = new InspectionFileDrawingChildViewModel();
                                objChild.FileDrawingName = item.FileDrawingName;
                                objChild.FileDrawingPath = host + "/DrawingFiles/" + item.FileDrawingPath;
                                objChildViewList.Add(objChild);
                            }
                            objParent.inspectionFileDrawingChildViewModels = objChildViewList;
                            objParentViewList.Add(objParent);
                        }



                        Quotation objTempQuotation = new Quotation();
                        objTempQuotation = getQuotationDetails(id, 0);
                        if (objTempQuotation != null)
                        {
                            _list.objQuotation = objTempQuotation;
                            //_list.objQuotation.objQuotationItems = objTempQuotation.objQuotationItems;
                        }

                        _list.ListInspectionFileDrawing = objParentViewList;
                        var iQuotation = new Quotation();
                        if (_list.InspectionStatus == 5)
                        {
                            iQuotation = db.Quotations.Where(x => x.InspectionId == id && x.IsActive == true).OrderBy(x => x.QuotationId).FirstOrDefault();
                            if (iQuotation != null)
                            {
                                iDefQuotation = db.QuotationInspectionDeficiencies.Where(x => x.QuotationId == iQuotation.QuotationId).ToList();
                            }
                        }

                        var iDef = db.InspectionDeficiencies.Where(x => x.InspectionId == _list.InspectionId).ToList();
                        iDef = iDef.Where(x => x.IsDelete == false || x.IsDelete == null).ToList();

                        if (iDef != null && iDef.Count > 0)
                        {
                            Int16 i = 0;
                            var adminStatus = "";
                            foreach (var s in iDef)
                            {

                                Deficiency objDeficiency = new Deficiency();
                                i += 1;
                                InspectionDeficiencyViewModel objInsDef = new InspectionDeficiencyViewModel();
                                objInsDef.RowNo = i;
                                objInsDef.InspectionId = s.InspectionId;
                                objInsDef.InspectionDeficiencyId = s.InspectionDeficiencyId;
                                objInsDef.CustomerNomenclatureNo = s.CustomerNomenclatureNo?.Trim();
                                objInsDef.CustomerNomenclatureBayNoID = s.CustomerNomenclatureBayNoID?.Trim();
                                objInsDef.BayFrameSide = s.BayFrameSide?.Trim();
                                objInsDef.BeamFrameLevel = s.BeamFrameLevel?.Trim();
                                string strActionTaken = "";
                                List<string> actionsTaken = new List<string>();
                                if (s.Action_Monitor == true)
                                {
                                    actionsTaken.Add("Monitor");
                                }
                                if (s.Action_Repair == true)
                                {
                                    actionsTaken.Add("Repair");
                                }
                                if (s.Action_Replace == true)
                                {
                                    actionsTaken.Add("Replace");
                                }

                                strActionTaken = string.Join(",", actionsTaken);
                                objInsDef.ActionTaken = strActionTaken;
                                objInsDef.Action_Monitor = s.Action_Monitor;
                                objInsDef.Action_Repair = s.Action_Repair;
                                objInsDef.Action_Replace = s.Action_Replace;
                                objInsDef.DeficiencyInfo = s.DeficiencyInfo;
                                objInsDef.DeficiencyType = s.DeficiencyType;
                                objInsDef.Severity_IndexNo = s.Severity_IndexNo;
                                objInsDef.InspectionDeficiencyTechnicianStatus = s.InspectionDeficiencyTechnicianStatus;
                                objInsDef.InspectionDeficiencyTechnicianRemark = s.InspectionDeficiencyTechnicianRemarks;
                                if (s.InspectionDeficiencyRequestQuotation == null)
                                {
                                    objInsDef.InspectionDeficiencyRequestQuotation = 0;
                                    objInsDef.selectedReqQuote = 0;
                                }
                                else
                                {
                                    if (_list.InspectionStatus >= 7)
                                    {
                                        objInsDef.InspectionDeficiencyRequestQuotation = 0;
                                        objInsDef.selectedReqQuote = 0;                                        
                                    }
                                    else
                                    {
                                        objInsDef.InspectionDeficiencyRequestQuotation = s.InspectionDeficiencyRequestQuotation;                                        
                                        objInsDef.selectedReqQuote = s.InspectionDeficiencyRequestQuotation;
                                    }
                                }

                                if (s.InspectionDeficiencyApprovedQuotation == null)
                                {
                                    objInsDef.InspectionDeficiencyApprovedQuotation = 0;
                                }
                                else
                                {
                                    objInsDef.InspectionDeficiencyApprovedQuotation = s.InspectionDeficiencyApprovedQuotation;
                                }

                                objInsDef.InspectionDeficiencyAdminStatus = s.InspectionDeficiencyAdminStatus;
                                objInsDef.IsDelete = s.IsDelete;
                                if (s.InspectionDeficiencyTechnicianStatus == 1)
                                {
                                    objInsDef.InspectionDeficiencyTechnicianStatusText = "Repaired";
                                }
                                else if (s.InspectionDeficiencyTechnicianStatus == 2)
                                {
                                    objInsDef.InspectionDeficiencyTechnicianStatusText = "Not Repaired";
                                }
                                else
                                {
                                    objInsDef.InspectionDeficiencyTechnicianStatusText = "";
                                }
                                objInsDef.InspectionDeficiencyAdminStatus = s.InspectionDeficiencyAdminStatus;
                                if (s.InspectionDeficiencyAdminStatus == 1)
                                {
                                    objInsDef.InspectionDeficiencyAdminStatusText = "Approved";
                                }
                                else
                                {
                                    objInsDef.InspectionDeficiencyAdminStatusText = "";
                                }
                                if (s.InspectionDeficiencyAdminStatus == 1)
                                {
                                    adminStatus += s.InspectionDeficiencyId + ",";
                                }

                                if (s.DeficiencyID != null)
                                {
                                    objInsDef.DeficiencyID = Convert.ToInt32(s.DeficiencyID.ToString());
                                    var def = db.Deficiencies.Where(x => x.DeficiencyID == s.DeficiencyID).FirstOrDefault(); // && x.IsActive == true
                                    if (def != null)
                                    {

                                        objInsDef.DeficiencyDesc = def.DeficiencyDescription;

                                        var objInspectionDeficiencyPhotoViewModel = db.InspectionDeficiencyPhotoes.Where(x => x.InspectionDeficiencyId == s.InspectionDeficiencyId && x.InspectionDeficiencyIsStatus == false).ToList();
                                        var objInspectionDeficiencyPhotoTechnicianViewModel = db.InspectionDeficiencyPhotoes.Where(x => x.InspectionDeficiencyId == s.InspectionDeficiencyId && x.InspectionDeficiencyIsStatus == true).ToList();

                                        List<InspectionDeficiencyPhotoViewModel> lstInspectionDeficiencyPhotoViewModel = new List<InspectionDeficiencyPhotoViewModel>();
                                        List<InspectionDeficiencyPhotoTechnicianViewModel> lstInspectionDeficiencyPhotoTechnicianViewModel = new List<InspectionDeficiencyPhotoTechnicianViewModel>();

                                        foreach (var itemPhoto in objInspectionDeficiencyPhotoViewModel)
                                        {
                                            InspectionDeficiencyPhotoViewModel objchildInspectionDeficiencyPhotoViewModel = new InspectionDeficiencyPhotoViewModel();
                                            objchildInspectionDeficiencyPhotoViewModel.DeficiencyPhoto = host + "/img/deficiency/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            string TempOutputPath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiencythumb/"), itemPhoto.InspectionDeficiencyPhotoPath);
                                            if (!File.Exists(TempOutputPath))
                                            {
                                                try
                                                {
                                                    //byte[] imageBytes = CreateThumbnail(itemPhoto.InspectionDeficiencyPhotoPath, 100, 100);
                                                    //var outputPath = Path.Combine(host + "/img/deficiencythumb/", itemPhoto.InspectionDeficiencyPhotoPath);
                                                    //File.WriteAllBytes(TempOutputPath, imageBytes);
                                                    string fullImagePath = HostingEnvironment.MapPath("~/img/deficiency/") + itemPhoto.InspectionDeficiencyPhotoPath;

                                                    // Check if the image exists
                                                    if (!File.Exists(fullImagePath))
                                                    {
                                                        //throw new FileNotFoundException("Image not found: " + fullImagePath);
                                                    }
                                                    else
                                                    {
                                                        byte[] imageBytes = CreateThumbnail(fullImagePath, 100, 100);
                                                        var outputPath = Path.Combine(host + "/img/deficiencythumb/", itemPhoto.InspectionDeficiencyPhotoPath);
                                                        File.WriteAllBytes(TempOutputPath, imageBytes);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // Log and skip this image
                                                }
                                            }
                                            objchildInspectionDeficiencyPhotoViewModel.DeficiencyPhotoThumb = host + "/img/deficiencythumb/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            if (itemPhoto.InspectionDeficiencyIsStatus.HasValue)
                                            {
                                                objchildInspectionDeficiencyPhotoViewModel.InspectionDeficiencyIsStatus = itemPhoto.InspectionDeficiencyIsStatus.Value;
                                            }
                                            else
                                            {
                                                // Handle null explicitly, maybe throw or assign a default
                                                objchildInspectionDeficiencyPhotoViewModel.InspectionDeficiencyIsStatus = false;
                                            }
                                            lstInspectionDeficiencyPhotoViewModel.Add(objchildInspectionDeficiencyPhotoViewModel);
                                        }
                                        objInsDef.InspectionDeficiencyPhotoViewModel = lstInspectionDeficiencyPhotoViewModel;

                                        foreach (var itemPhoto in objInspectionDeficiencyPhotoTechnicianViewModel)
                                        {
                                            InspectionDeficiencyPhotoTechnicianViewModel objchildInspectionDeficiencyTechnicianPhotoViewModel = new InspectionDeficiencyPhotoTechnicianViewModel();
                                            objchildInspectionDeficiencyTechnicianPhotoViewModel.DeficiencyPhoto = host + "/img/deficiency/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            string TempOutputPath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiencythumb/"), itemPhoto.InspectionDeficiencyPhotoPath);
                                            if (!File.Exists(TempOutputPath))
                                            {
                                                try
                                                {
                                                    byte[] imageBytes = CreateThumbnail(itemPhoto.InspectionDeficiencyPhotoPath, 100, 100);
                                                    var outputPath = Path.Combine(host + "/img/deficiencythumb/", itemPhoto.InspectionDeficiencyPhotoPath);
                                                    File.WriteAllBytes(TempOutputPath, imageBytes);
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                            }
                                            objchildInspectionDeficiencyTechnicianPhotoViewModel.DeficiencyPhotoThumb = host + "/img/deficiencythumb/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            if (itemPhoto.InspectionDeficiencyIsStatus.HasValue)
                                            {
                                                objchildInspectionDeficiencyTechnicianPhotoViewModel.InspectionDeficiencyIsStatus = itemPhoto.InspectionDeficiencyIsStatus.Value;
                                            }
                                            else
                                            {
                                                // Handle null explicitly, maybe throw or assign a default
                                                objchildInspectionDeficiencyTechnicianPhotoViewModel.InspectionDeficiencyIsStatus = false;
                                            }
                                            lstInspectionDeficiencyPhotoTechnicianViewModel.Add(objchildInspectionDeficiencyTechnicianPhotoViewModel);
                                        }
                                        objInsDef.InspectionDeficiencyPhotoTechnicianViewModel = lstInspectionDeficiencyPhotoTechnicianViewModel;
                                        objInsDef.InspectionDeficiencyMTO = getAllInspectionDeficiencyMTOByID(objInsDef.InspectionDeficiencyId);
                                        //select* from[dbo].[InspectionDeficiencyPhoto]
                                        objDeficiency.DeficiencyID = def.DeficiencyID;
                                        objDeficiency.DeficiencyCategoryId = def.DeficiencyCategoryId;
                                        objDeficiency.DeficiencyCategory = def.DeficiencyCategory;
                                        objDeficiency.DeficiencyInfo = def.DeficiencyInfo;
                                        objDeficiency.DeficiencyDescription = def.DeficiencyDescription;
                                        iListConclusionandRecommendationsList.Add(objDeficiency);
                                    }

                                    var iMTO = db.InspectionDeficiencyMTOes.Where(d => d.InspectionDeficiencyId == objInsDef.InspectionDeficiencyId).ToList();
                                    if (iMTO.Count != 0)
                                    {
                                        foreach (var itemMTO in iMTO)
                                        {
                                            InspectionDeficiencyMTOViewModel objMTO = new InspectionDeficiencyMTOViewModel();
                                            objMTO.DeficiencyRowNo = i;
                                            objMTO.Severity_IndexNo = objInsDef.Severity_IndexNo;
                                            objMTO.InspectionDeficiencyId = itemMTO.InspectionDeficiencyMTOId;
                                            objMTO.InspectionDeficiencyMTOId = itemMTO.InspectionDeficiencyMTOId;
                                            if (itemMTO.InspectionDeficiencyMTOId != 0)
                                            {
                                                InspectionDeficiencyMTODetailViewModel objMTODetails = new InspectionDeficiencyMTODetailViewModel();
                                                var iMTOdetails = db.InspectionDeficiencyMTODetails.Where(d => d.InspectionDeficiencyMTOId == itemMTO.InspectionDeficiencyMTOId).ToList();
                                                if (iMTOdetails.Count != 0)
                                                {
                                                    var fType = "";
                                                    foreach (var d in iMTOdetails)
                                                    {
                                                        if (d.ComponentPropertyTypeId != 0)
                                                        {
                                                            var type = getComponentPropertyTypeById(d.ComponentPropertyTypeId);
                                                            if (type != null)
                                                            {
                                                                if (type.ComponentPropertyTypeName.Contains("Type"))
                                                                {
                                                                    var value = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == d.ComponentPropertyValueId && x.ComponentPropertyTypeId == d.ComponentPropertyTypeId).ToList();
                                                                    if (value.Count != 0)
                                                                    {
                                                                        foreach (var v in value)
                                                                        {
                                                                            fType += v.ComponentPropertyValue1 + ",";
                                                                            objMTO.Type = fType.Remove(fType.Length - 1);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                            objMTO.ComponentId = itemMTO.InspectionDeficiencyMTOId;
                                            if (objMTO.ComponentId != 0)
                                            {
                                                var componentName = db.Components.Where(x => x.ComponentId == itemMTO.ComponentId).FirstOrDefault();
                                                if (componentName != null)
                                                {
                                                    objMTO.ComponentName = componentName.ComponentName;
                                                }
                                                else
                                                {
                                                    objMTO.ComponentName = "";
                                                }
                                            }
                                            else
                                            {
                                                objMTO.ComponentName = "";
                                            }
                                            objMTO.ManufacturerId = itemMTO.ManufacturerId;
                                            if (itemMTO.ManufacturerId != 0)
                                            {
                                                var manufec = db.Manufacturers.Where(x => x.ManufacturerId == itemMTO.ManufacturerId).FirstOrDefault();
                                                if (manufec != null)
                                                {
                                                    objMTO.ManufacturerName = manufec.ManufacturerName;
                                                }
                                                else
                                                {
                                                    objMTO.ManufacturerName = "";
                                                }
                                            }
                                            else
                                            {
                                                objMTO.ManufacturerName = "";
                                            }
                                            objMTO.VendorID = itemMTO.VendorID;
                                            objMTO.CAMID = itemMTO.CAMID;
                                            objMTO.Size_Description = itemMTO.Size_Description;
                                            objMTO.Size_DescriptionShort = itemMTO.Size_DescriptionShort;
                                            objMTO.QuantityReq = itemMTO.QuantityReq;
                                            objMTO.ComponentSavedId = itemMTO.InspectionDeficiencyMTOId;
                                            iMTOList.Add(objMTO);
                                        }
                                    }
                                    else
                                    {
                                        InspectionDeficiencyMTOViewModel objMTO = new InspectionDeficiencyMTOViewModel();
                                        objMTO.DeficiencyRowNo = i;
                                        objMTO.Severity_IndexNo = objInsDef.Severity_IndexNo;
                                        objMTO.InspectionDeficiencyId = 0;
                                        objMTO.InspectionDeficiencyMTOId = 0;
                                        objMTO.ComponentName = "No Material Required/Monitor";
                                        objMTO.ManufacturerName = "";
                                        objMTO.VendorID = "";
                                        objMTO.CAMID = "";
                                        //objMTO.Type = itemMTO.Type;
                                        objMTO.Size_Description = "No material required.";
                                        objMTO.Size_DescriptionShort = "";
                                        objMTO.QuantityReq = 0;
                                        objMTO.ComponentSavedId = 0;
                                        iMTOList.Add(objMTO);
                                    }
                                }
                                iDefList.Add(objInsDef);

                                if (adminStatus != "")
                                {
                                    _list.InspectionDeficiencyAdminStatus = adminStatus.Remove(adminStatus.Length - 1, 1);
                                }

                                List<Deficiency> objDistinct = iListConclusionandRecommendationsList.GroupBy(x => x.DeficiencyID).Select(g => g.First()).ToList();
                                _list.ListConclusionandRecommendationsViewModel = objDistinct;// iListConclusionandRecommendationsList;//.Select(o => o.DeficiencyID).Distinct();
                                _list.iDefModel = iDefList;
                                _list.iMTOModel = iMTOList;


                            }

                        }



                        return _list;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static InspectionViewModel getInspectionDetailsForSheetMobile(long id)
        {   
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    InspectionViewModel _list = new InspectionViewModel();
                    List<InspectionDeficiencyViewModel> iDefList = new List<InspectionDeficiencyViewModel>();                                        
                    List<Deficiency> iListConclusionandRecommendationsList = new List<Deficiency>();
                    string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                    Uri url = new Uri(tmpURL);
                    string host = url.GetLeftPart(UriPartial.Authority);
                    string CustomerFullAddress = "";
                    List<string> FullAddress = new List<string>();
                    var list = db.Inspections.Where(x => x.InspectionId == id && x.IsActive == true).FirstOrDefault();
                    if (list != null)
                    {
                        _list.InspectionId = list.InspectionId;
                        _list.InspectionDocumentNo = list.InspectionDocumentNo;
                        _list.InspectionDocumentNoRef = list.InspectionDocumentNoRef;
                        list.FacilitiesAreasIds = RemoveDuplicates(list.FacilitiesAreasIds);
                        list.ProcessOverviewIds = RemoveDuplicates(list.ProcessOverviewIds);

                        var sInspectionType = getInspectionTypeByCode(list.InspectionType);
                        if (sInspectionType != null)
                        {
                            _list.InspectionType = sInspectionType.InspectionTypeName;
                        }
                        else
                        {
                            _list.InspectionType = "Rack Inspection";
                        }
                        _list.InspectionDate = list.InspectionDate;
                        _list.Reportdate = list.Reportdate;
                        _list.InspectionStatus = list.InspectionStatus;
                        var objInspStatus = db.InspectionStatus.Where(x => x.InspectionStatusId == list.InspectionStatus).FirstOrDefault();
                        if (objInspStatus != null)
                        {
                            _list.InspectionStatusName = objInspStatus.InspectionStatus;
                        }
                        else
                        {
                            _list.InspectionStatusName = "";
                        }

                        _list.CADDocuments = list.CADDocuments;

                        if (list.EmployeeId != 0)
                        {
                            var emp = getEmployeeById(Convert.ToInt16(list.EmployeeId));
                            if (emp != null)
                            {
                                _list.Employee = emp.EmployeeName;
                                _list.empModel.EmployeeID = emp.EmployeeID;
                                _list.empModel.EmployeeName = emp.EmployeeName;
                                _list.empModel.TitleDegrees = emp.TitleDegrees;
                                _list.empModel.EmployeeEmail = emp.EmployeeEmail;
                                _list.empModel.MobileNo = emp.MobileNo;
                            }
                        }
                        
                        _list.CustomerContactIds = list.CustomerContactIds;
                        if (list.CustomerContactIds != null)
                        {
                            list.CustomerContactIds = list.CustomerContactIds.Trim();
                            if (list.CustomerContactIds.Length != 0)
                            {
                                List<CustomerLocationContactViewModel> objListContacts = new List<CustomerLocationContactViewModel>();

                                //CustomerLocationId
                                objListContacts = getLocationContactDetailsByLocationId(list.CustomerLocationId);
                                string fAreaName = "";
                                if (objListContacts != null)
                                {
                                    list.CustomerContactIds = list.CustomerContactIds.Trim();
                                    string[] items = list.CustomerContactIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (var f in items)
                                    {
                                        if (f != null)
                                        {
                                            long fId = Convert.ToInt32(f);
                                            objListContacts.Where(w => w.LocationContactId == fId).ToList().ForEach(s => s.Selected = true);
                                            //if (item.LocationContactId == fId)
                                            //{
                                            //    item.Selected = true;
                                            //}
                                        }
                                    }
                                }
                                _list.ListCustomerLocationContacts = objListContacts;
                                //_list.InspectionFacilitiesArea = null;
                                _list.FacilitiesAreasIds = list.FacilitiesAreasIds;
                                _list.FacilitiesAreas = fAreaName;
                            }
                        }
                        else
                        {
                            List<CustomerLocationContactViewModel> objListContacts = new List<CustomerLocationContactViewModel>();
                            objListContacts = getLocationContactDetailsByLocationIdBoth(list.CustomerId, list.CustomerLocationId);
                            _list.ListCustomerLocationContacts = objListContacts;
                            //_list.InspectionFacilitiesArea = null;
                            _list.FacilitiesAreasIds = list.FacilitiesAreasIds;
                            //_list.FacilitiesAreas = fAreaName;
                        }

                        if (list.FacilitiesAreasIds.Length != 0)
                        {
                            List<FacilitiesArea> objListfacilitiesAreas = new List<FacilitiesArea>();
                            string fAreaName = "";
                            string[] items = list.FacilitiesAreasIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
                                        fAreaName += String.Concat(fac.FacilitiesAreaName + ",");
                                        objListfacilitiesAreas.Add(fac);

                                    }
                                }
                            }
                            _list.InspectionFacilitiesArea = objListfacilitiesAreas;
                            //_list.InspectionFacilitiesArea = null;
                            _list.FacilitiesAreasIds = list.FacilitiesAreasIds;
                            _list.FacilitiesAreas = fAreaName;
                        }
                        if (list.ProcessOverviewIds.Length != 0)
                        {
                            List<ProcessOverview> objListProcessOverview = new List<ProcessOverview>();

                            var pOverName = "";
                            string[] overview = list.ProcessOverviewIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            _list.CapacityTable = 0;
                            _list.PlanElevationDrawing = 0;
                            foreach (var h in overview)
                            {
                                if (h != null)
                                {
                                    int pId = Convert.ToInt16(h);
                                    var pro = db.ProcessOverviews.Where(x => x.ProcessOverviewId == pId && x.IsActive == true).FirstOrDefault();
                                    if (pro != null)
                                    {
                                        if (pId == 9)
                                        {
                                            _list.PlanElevationDrawing = 1;
                                        }
                                        if (pId == 10)
                                        {
                                            _list.CapacityTable = 1;
                                        }
                                        pOverName += String.Concat(pro.ProcessOverviewDesc + ";");
                                        objListProcessOverview.Add(pro);
                                    }
                                }
                            }
                            _list.InspectionProcessOverview = objListProcessOverview;
                            _list.ProcessOverviewsIds = list.ProcessOverviewIds;
                            _list.ProcessOverviews = pOverName;
                        }

                        var iDef = db.InspectionDeficiencies.Where(x => x.InspectionId == _list.InspectionId).ToList();
                        iDef = iDef.Where(x => x.IsDelete == false || x.IsDelete == null).ToList();

                        if (iDef != null && iDef.Count > 0)
                        {
                            Int16 i = 0;
                            var adminStatus = "";
                            foreach (var s in iDef)
                            {

                                Deficiency objDeficiency = new Deficiency();
                                i += 1;
                                InspectionDeficiencyViewModel objInsDef = new InspectionDeficiencyViewModel();
                                objInsDef.RowNo = i;
                                objInsDef.InspectionId = s.InspectionId;
                                objInsDef.InspectionDeficiencyId = s.InspectionDeficiencyId;
                                objInsDef.CustomerNomenclatureNo = s.CustomerNomenclatureNo?.Trim();
                                objInsDef.CustomerNomenclatureBayNoID = s.CustomerNomenclatureBayNoID?.Trim();
                                objInsDef.BayFrameSide = s.BayFrameSide?.Trim();
                                objInsDef.BeamFrameLevel = s.BeamFrameLevel?.Trim();
                                string strActionTaken = "";
                                List<string> actionsTaken = new List<string>();
                                if (s.Action_Monitor == true)
                                {
                                    actionsTaken.Add("Monitor");
                                }
                                if (s.Action_Repair == true)
                                {
                                    actionsTaken.Add("Repair");
                                }
                                if (s.Action_Replace == true)
                                {
                                    actionsTaken.Add("Replace");
                                }

                                strActionTaken = string.Join(",", actionsTaken);
                                objInsDef.ActionTaken = strActionTaken;
                                objInsDef.Action_Monitor = s.Action_Monitor;
                                objInsDef.Action_Repair = s.Action_Repair;
                                objInsDef.Action_Replace = s.Action_Replace;
                                objInsDef.DeficiencyInfo = s.DeficiencyInfo;
                                objInsDef.DeficiencyType = s.DeficiencyType;
                                objInsDef.Severity_IndexNo = s.Severity_IndexNo;
                                objInsDef.InspectionDeficiencyTechnicianStatus = s.InspectionDeficiencyTechnicianStatus;
                                objInsDef.InspectionDeficiencyTechnicianRemark = s.InspectionDeficiencyTechnicianRemarks;
                                if (s.InspectionDeficiencyRequestQuotation == null)
                                {
                                    objInsDef.InspectionDeficiencyRequestQuotation = 0;
                                    objInsDef.selectedReqQuote = 0;
                                }
                                else
                                {
                                    if (_list.InspectionStatus >= 7)
                                    {
                                        objInsDef.InspectionDeficiencyRequestQuotation = 0;
                                        objInsDef.selectedReqQuote = 0;
                                    }
                                    else
                                    {
                                        objInsDef.InspectionDeficiencyRequestQuotation = s.InspectionDeficiencyRequestQuotation;
                                        objInsDef.selectedReqQuote = s.InspectionDeficiencyRequestQuotation;
                                    }
                                }

                                if (s.InspectionDeficiencyApprovedQuotation == null)
                                {
                                    objInsDef.InspectionDeficiencyApprovedQuotation = 0;
                                }
                                else
                                {
                                    objInsDef.InspectionDeficiencyApprovedQuotation = s.InspectionDeficiencyApprovedQuotation;
                                }

                                objInsDef.InspectionDeficiencyAdminStatus = s.InspectionDeficiencyAdminStatus;
                                objInsDef.IsDelete = s.IsDelete;
                                if (s.InspectionDeficiencyTechnicianStatus == 1)
                                {
                                    objInsDef.InspectionDeficiencyTechnicianStatusText = "Repaired";
                                }
                                else if (s.InspectionDeficiencyTechnicianStatus == 2)
                                {
                                    objInsDef.InspectionDeficiencyTechnicianStatusText = "Not Repaired";
                                }
                                else
                                {
                                    objInsDef.InspectionDeficiencyTechnicianStatusText = "";
                                }
                                objInsDef.InspectionDeficiencyAdminStatus = s.InspectionDeficiencyAdminStatus;
                                if (s.InspectionDeficiencyAdminStatus == 1)
                                {
                                    objInsDef.InspectionDeficiencyAdminStatusText = "Approved";
                                }
                                else
                                {
                                    objInsDef.InspectionDeficiencyAdminStatusText = "";
                                }
                                if (s.InspectionDeficiencyAdminStatus == 1)
                                {
                                    adminStatus += s.InspectionDeficiencyId + ",";
                                }

                                if (s.DeficiencyID != null)
                                {
                                    objInsDef.DeficiencyID = Convert.ToInt32(s.DeficiencyID.ToString());
                                    var def = db.Deficiencies.Where(x => x.DeficiencyID == s.DeficiencyID).FirstOrDefault(); // && x.IsActive == true
                                    if (def != null)
                                    {

                                        objInsDef.DeficiencyDesc = def.DeficiencyDescription;

                                        var objInspectionDeficiencyPhotoViewModel = db.InspectionDeficiencyPhotoes.Where(x => x.InspectionDeficiencyId == s.InspectionDeficiencyId && x.InspectionDeficiencyIsStatus == false).ToList();
                                        var objInspectionDeficiencyPhotoTechnicianViewModel = db.InspectionDeficiencyPhotoes.Where(x => x.InspectionDeficiencyId == s.InspectionDeficiencyId && x.InspectionDeficiencyIsStatus == true).ToList();

                                        List<InspectionDeficiencyPhotoViewModel> lstInspectionDeficiencyPhotoViewModel = new List<InspectionDeficiencyPhotoViewModel>();
                                        List<InspectionDeficiencyPhotoTechnicianViewModel> lstInspectionDeficiencyPhotoTechnicianViewModel = new List<InspectionDeficiencyPhotoTechnicianViewModel>();

                                        foreach (var itemPhoto in objInspectionDeficiencyPhotoViewModel)
                                        {
                                            InspectionDeficiencyPhotoViewModel objchildInspectionDeficiencyPhotoViewModel = new InspectionDeficiencyPhotoViewModel();
                                            objchildInspectionDeficiencyPhotoViewModel.DeficiencyPhoto = host + "/img/deficiency/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            string TempOutputPath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiencythumb/"), itemPhoto.InspectionDeficiencyPhotoPath);
                                            if (!File.Exists(TempOutputPath))
                                            {
                                                try
                                                {
                                                    //byte[] imageBytes = CreateThumbnail(itemPhoto.InspectionDeficiencyPhotoPath, 100, 100);
                                                    //var outputPath = Path.Combine(host + "/img/deficiencythumb/", itemPhoto.InspectionDeficiencyPhotoPath);
                                                    //File.WriteAllBytes(TempOutputPath, imageBytes);
                                                    string fullImagePath = HostingEnvironment.MapPath("~/img/deficiency/") + itemPhoto.InspectionDeficiencyPhotoPath;

                                                    // Check if the image exists
                                                    if (!File.Exists(fullImagePath))
                                                    {
                                                        //throw new FileNotFoundException("Image not found: " + fullImagePath);
                                                    }
                                                    else
                                                    {
                                                        byte[] imageBytes = CreateThumbnail(fullImagePath, 100, 100);
                                                        var outputPath = Path.Combine(host + "/img/deficiencythumb/", itemPhoto.InspectionDeficiencyPhotoPath);
                                                        File.WriteAllBytes(TempOutputPath, imageBytes);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // Log and skip this image
                                                }
                                            }
                                            objchildInspectionDeficiencyPhotoViewModel.DeficiencyPhotoThumb = host + "/img/deficiencythumb/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            if (itemPhoto.InspectionDeficiencyIsStatus.HasValue)
                                            {
                                                objchildInspectionDeficiencyPhotoViewModel.InspectionDeficiencyIsStatus = itemPhoto.InspectionDeficiencyIsStatus.Value;
                                            }
                                            else
                                            {
                                                // Handle null explicitly, maybe throw or assign a default
                                                objchildInspectionDeficiencyPhotoViewModel.InspectionDeficiencyIsStatus = false;
                                            }
                                            lstInspectionDeficiencyPhotoViewModel.Add(objchildInspectionDeficiencyPhotoViewModel);
                                        }
                                        objInsDef.InspectionDeficiencyPhotoViewModel = lstInspectionDeficiencyPhotoViewModel;

                                        foreach (var itemPhoto in objInspectionDeficiencyPhotoTechnicianViewModel)
                                        {
                                            InspectionDeficiencyPhotoTechnicianViewModel objchildInspectionDeficiencyTechnicianPhotoViewModel = new InspectionDeficiencyPhotoTechnicianViewModel();
                                            objchildInspectionDeficiencyTechnicianPhotoViewModel.DeficiencyPhoto = host + "/img/deficiency/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            string TempOutputPath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiencythumb/"), itemPhoto.InspectionDeficiencyPhotoPath);
                                            if (!File.Exists(TempOutputPath))
                                            {
                                                try
                                                {
                                                    byte[] imageBytes = CreateThumbnail(itemPhoto.InspectionDeficiencyPhotoPath, 100, 100);
                                                    var outputPath = Path.Combine(host + "/img/deficiencythumb/", itemPhoto.InspectionDeficiencyPhotoPath);
                                                    File.WriteAllBytes(TempOutputPath, imageBytes);
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                            }
                                            objchildInspectionDeficiencyTechnicianPhotoViewModel.DeficiencyPhotoThumb = host + "/img/deficiencythumb/" + itemPhoto.InspectionDeficiencyPhotoPath;
                                            if (itemPhoto.InspectionDeficiencyIsStatus.HasValue)
                                            {
                                                objchildInspectionDeficiencyTechnicianPhotoViewModel.InspectionDeficiencyIsStatus = itemPhoto.InspectionDeficiencyIsStatus.Value;
                                            }
                                            else
                                            {
                                                // Handle null explicitly, maybe throw or assign a default
                                                objchildInspectionDeficiencyTechnicianPhotoViewModel.InspectionDeficiencyIsStatus = false;
                                            }
                                            lstInspectionDeficiencyPhotoTechnicianViewModel.Add(objchildInspectionDeficiencyTechnicianPhotoViewModel);
                                        }
                                        objInsDef.InspectionDeficiencyPhotoTechnicianViewModel = lstInspectionDeficiencyPhotoTechnicianViewModel;
                                        objInsDef.InspectionDeficiencyMTO = getAllInspectionDeficiencyMTOByID(objInsDef.InspectionDeficiencyId);
                                        //select* from[dbo].[InspectionDeficiencyPhoto]
                                        objDeficiency.DeficiencyID = def.DeficiencyID;
                                        objDeficiency.DeficiencyCategoryId = def.DeficiencyCategoryId;
                                        objDeficiency.DeficiencyCategory = def.DeficiencyCategory;
                                        objDeficiency.DeficiencyInfo = def.DeficiencyInfo;
                                        objDeficiency.DeficiencyDescription = def.DeficiencyDescription;
                                        iListConclusionandRecommendationsList.Add(objDeficiency);
                                    }
                                }
                                iDefList.Add(objInsDef);

                                if (adminStatus != "")
                                {
                                    _list.InspectionDeficiencyAdminStatus = adminStatus.Remove(adminStatus.Length - 1, 1);
                                }
                                List<Deficiency> objDistinct = iListConclusionandRecommendationsList.GroupBy(x => x.DeficiencyID).Select(g => g.First()).ToList();
                                _list.ListConclusionandRecommendationsViewModel = objDistinct;// iListConclusionandRecommendationsList;//.Select(o => o.DeficiencyID).Distinct();
                                _list.iDefModel = iDefList;
                            }
                        }
                        return _list;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }




        //internal static InspectionViewModel GetInspectionDetailsForSheetV2(long id)
        //{
        //    try
        //    {
        //        using (var db = new DatabaseEntities())
        //        {
        //            var inspection = db.Inspections.FirstOrDefault(x => x.InspectionId == id && x.IsActive == true);
        //            if (inspection == null) return null;

        //            var model = InitializeInspectionModel(inspection);

        //            SetCustomerDetails(inspection, model, db);
        //            SetLocationDetails(inspection, model, db);
        //            SetEmployeeDetails(inspection, model, db);

        //            SetFacilitiesAreaDetails(inspection, model, db);
        //            SetProcessOverviewDetails(inspection, model, db);
        //            SetReferenceDocumentDetails(inspection, model, db);

        //            SetDrawingFiles(id, model, db);
        //            SetQuotationAndDeficiencies(id, inspection, model, db);

        //            return model;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error here
        //        return null;
        //    }
        //}

        //private static InspectionViewModel InitializeInspectionModel(Inspection inspection)
        //{
        //    return new InspectionViewModel
        //    {
        //        InspectionId = inspection.InspectionId,
        //        InspectionDocumentNo = inspection.InspectionDocumentNo,
        //        InspectionDocumentNoRef = inspection.InspectionDocumentNoRef,
        //        InspectionDate = inspection.InspectionDate,
        //        Reportdate = inspection.Reportdate,
        //        InspectionStatus = inspection.InspectionStatus,
        //        CADDocuments = inspection.CADDocuments,
        //        StampingEngineerId = inspection.StampingEngineerId,
        //        FacilitiesAreasIds = RemoveDuplicates(inspection.FacilitiesAreasIds),
        //        ProcessOverviewsIds = RemoveDuplicates(inspection.ProcessOverviewIds)
        //    };
        //}

        //private static void SetCustomerDetails(Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    var customer = db.Customers.FirstOrDefault(x => x.CustomerId == inspection.CustomerId);
        //    if (customer != null)
        //    {
        //        model.CustomerName = customer.CustomerName;
        //        model.CustomerAddress = customer.Address + ", " + customer.City + ", " + customer.State + ", " + customer.Country;
        //    }

        //    var contact = db.CustomerContacts.FirstOrDefault(x => x.CustomerContactId == inspection.CustomerContactId);
        //    if (contact != null)
        //    {
        //        model.ContactName = contact.ContactName;
        //        model.ContactNo = contact.ContactNo;
        //        model.ContactEmail = contact.ContactEmail;
        //    }
        //}

        //private static void SetLocationDetails(Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    var location = db.Locations.FirstOrDefault(x => x.LocationId == inspection.LocationId);
        //    var area = db.Areas.FirstOrDefault(x => x.AreaId == inspection.AreaId);
        //    model.LocationName = location?.LocationName;
        //    model.AreaName = area?.AreaName;
        //}

        //private static void SetEmployeeDetails(Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    var emp = db.Employees.FirstOrDefault(x => x.EmployeeId == inspection.EmployeeId);
        //    var stamping = db.Employees.FirstOrDefault(x => x.EmployeeId == inspection.StampingEngineerId);
        //    model.InspectedBy = emp?.EmployeeName;
        //    model.StampingEngineer = stamping?.EmployeeName;
        //}

        //private static void SetFacilitiesAreaDetails(Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    if (!string.IsNullOrEmpty(model.FacilitiesAreasIds))
        //    {
        //        var ids = model.FacilitiesAreasIds.Split(',').Select(long.Parse).ToList();
        //        model.FacilitiesAreaNames = db.FacilitiesAreas.Where(x => ids.Contains(x.FacilitiesAreaId))
        //            .Select(x => x.FacilitiesAreaName).ToList();
        //    }
        //}

        //private static void SetProcessOverviewDetails(Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    if (!string.IsNullOrEmpty(model.ProcessOverviewsIds))
        //    {
        //        var ids = model.ProcessOverviewsIds.Split(',').Select(long.Parse).ToList();
        //        model.ProcessOverviewNames = db.ProcessOverviews.Where(x => ids.Contains(x.ProcessOverviewId))
        //            .Select(x => x.Description).ToList();
        //    }
        //}

        //private static void SetReferenceDocumentDetails(Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    if (!string.IsNullOrEmpty(inspection.ReferenceDocuments))
        //    {
        //        var ids = inspection.ReferenceDocuments.Split(',').Select(long.Parse).ToList();
        //        model.ReferenceDocumentNames = db.ReferenceDocuments.Where(x => ids.Contains(x.ReferenceDocumentId))
        //            .Select(x => x.DocumentName).ToList();
        //    }
        //}

        //private static void SetDrawingFiles(long id, InspectionViewModel model, DatabaseEntities db)
        //{
        //    var tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
        //    Uri url = new Uri(tmpURL);
        //    string host = url.GetLeftPart(UriPartial.Authority);

        //    var drawings = db.InspectionFileDrawings
        //        .Where(d => d.InspectionId == id && (d.IsDeleted == 0 || d.IsDeleted == null))
        //        .ToList();

        //    model.ListInspectionFileDrawing = drawings
        //        .GroupBy(x => x.FileCategory)
        //        .Select(group => new InspectionFileDrawingViewModel
        //        {
        //            FileCategory = group.Key,
        //            inspectionFileDrawingChildViewModels = group.Select(file => new InspectionFileDrawingChildViewModel
        //            {
        //                FileDrawingName = file.FileDrawingName,
        //                FileDrawingPath = host + "/DrawingFiles/" + file.FileDrawingPath
        //            }).ToList()
        //        }).ToList();
        //}

        //private static void SetQuotationAndDeficiencies(long id, Inspection inspection, InspectionViewModel model, DatabaseEntities db)
        //{
        //    var iDefList = new List<InspectionDeficiencyViewModel>();
        //    var iMTOList = new List<InspectionDeficiencyMTOViewModel>();
        //    var conclusionList = new List<Deficiency>();

        //    var deficiencies = db.InspectionDeficiencies
        //        .Where(x => x.InspectionId == id && (x.IsDelete == false || x.IsDelete == null))
        //        .ToList();

        //    string host = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).GetLeftPart(UriPartial.Authority);

        //    foreach (var def in deficiencies)
        //    {
        //        var defViewModel = MapDeficiencyToViewModel(def, model.InspectionStatus, db, host);
        //        var mtoList = MapDeficiencyMTO(def.InspectionDeficiencyId, def.RowNo, def.Severity_IndexNo, db);

        //        iDefList.Add(defViewModel);
        //        iMTOList.AddRange(mtoList);

        //        if (def.DeficiencyID.HasValue)
        //        {
        //            var baseDef = db.Deficiencies.FirstOrDefault(x => x.DeficiencyID == def.DeficiencyID.Value);
        //            if (baseDef != null) conclusionList.Add(baseDef);
        //        }
        //    }

        //    model.iDefModel = iDefList;
        //    model.iMTOModel = iMTOList;
        //    model.ListConclusionandRecommendationsViewModel = conclusionList.GroupBy(x => x.DeficiencyID).Select(x => x.First()).ToList();
        //}

        //private static InspectionDeficiencyViewModel MapDeficiencyToViewModel(InspectionDeficiency def, int status, DatabaseEntities db, string host)
        //{
        //    var viewModel = new InspectionDeficiencyViewModel
        //    {
        //        InspectionDeficiencyId = def.InspectionDeficiencyId,
        //        DeficiencyDescription = def.DeficiencyDescription,
        //        IsTechnician = def.IsTechnician,
        //        IsAdmin = def.IsAdmin,
        //        RowNo = def.RowNo,
        //        Severity_IndexNo = def.Severity_IndexNo,
        //        ActionTaken = def.ActionTaken,
        //        PhotoUrls = new List<string>()
        //    };

        //    var photos = db.InspectionDeficiencyPhotos
        //        .Where(p => p.InspectionDeficiencyId == def.InspectionDeficiencyId && (p.IsDeleted == false || p.IsDeleted == null))
        //        .ToList();

        //    foreach (var photo in photos)
        //    {
        //        try
        //        {
        //            string fullUrl = host + "/DeficiencyPhotos/" + photo.PhotoPath;
        //            viewModel.PhotoUrls.Add(fullUrl);
        //        }
        //        catch { }
        //    }

        //    return viewModel;
        //}

        //private static List<InspectionDeficiencyMTOViewModel> MapDeficiencyMTO(long defId, int rowNo, string severity, DatabaseEntities db)
        //{
        //    var result = new List<InspectionDeficiencyMTOViewModel>();
        //    var mtoList = db.InspectionDeficiencyMTOs.Where(x => x.InspectionDeficiencyId == defId).ToList();

        //    foreach (var mto in mtoList)
        //    {
        //        var item = new InspectionDeficiencyMTOViewModel
        //        {
        //            InspectionDeficiencyId = defId,
        //            RowNo = rowNo,
        //            Severity_IndexNo = severity,
        //            ComponentName = db.Components.FirstOrDefault(c => c.ComponentId == mto.ComponentId)?.ComponentName,
        //            ManufacturerName = db.Manufacturers.FirstOrDefault(m => m.ManufacturerId == mto.ManufacturerId)?.ManufacturerName,
        //            Quantity = mto.Quantity
        //        };
        //        result.Add(item);
        //    }

        //    return result;
        //}

        //private static string RemoveDuplicates(string csv)
        //{
        //    if (string.IsNullOrEmpty(csv)) return "";
        //    var set = new HashSet<string>(csv.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)));
        //    return string.Join(",", set);
        //}


        internal static Quotation getQuotationDetails(long InspectionId, long QuotationId)
        {
            Quotation tempQuotation = new Quotation();
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    List<ImpSettingsViewModel> objImpSettingsViewModel = (from Is in db.ImpSettings
                                                                          select new ImpSettingsViewModel
                                                                          {
                                                                              SettingID = Is.SettingID,
                                                                              SettingType = Is.SettingType, // Handle null cases
                                                                              SettingValue = Is.SettingValue
                                                                          }).ToList();
                    if (InspectionId > 0)
                    {
                        tempQuotation = db.Quotations.Where(x => x.InspectionId == InspectionId).OrderByDescending(x => x.QuotationId).FirstOrDefault();
                    }
                    else if (QuotationId > 0)
                    {
                        tempQuotation = db.Quotations.Where(x => x.QuotationId == QuotationId).OrderByDescending(x => x.QuotationId).FirstOrDefault();
                    }

                    if (tempQuotation != null)
                    {
                        foreach (var item in objImpSettingsViewModel)
                        {
                            if (item.SettingType == "Surcharge")
                            {
                                if (item.SettingValue != "")
                                {
                                    tempQuotation.QuotationSurcharge = Convert.ToDecimal(item.SettingValue);
                                }
                                else
                                {
                                    tempQuotation.QuotationSurcharge = 0;
                                }
                            }
                            if (item.SettingType == "Markup")
                            {
                                if (item.SettingValue != "")
                                {
                                    tempQuotation.QuotationMarkup = Convert.ToDecimal(item.SettingValue);
                                }
                                else
                                {
                                    tempQuotation.QuotationMarkup = 0;
                                }
                            }
                        }
                        var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == tempQuotation.QuotationId).OrderBy(x => x.ItemPartNo.Contains("labour")).ThenBy(x => x.QuotationInspectionItemId).ToList();
                        //var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == tempQuotation.QuotationId).ToList();


                        tempQuotation.objQuotationItems = tempQuotationComponent;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return tempQuotation;
        }
        internal static byte[] CreateThumbnail(string filename, int maxWidth, int maxHeight)
        {
            // Define the path to the image file
            var filePath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiency/"), filename);

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Image not found", filename);
            }

            // Load the image from the file
            using (var image = Image.FromFile(filePath))
            {
                // Calculate dimensions for the thumbnail
                int thumbnailWidth, thumbnailHeight;
                if (image.Width <= maxWidth && image.Height <= maxHeight)
                {
                    // If the image is already within the bounds, no need to resize
                    thumbnailWidth = image.Width;
                    thumbnailHeight = image.Height;
                }
                else
                {
                    // Calculate new dimensions while preserving the aspect ratio
                    double ratioX = (double)maxWidth / image.Width;
                    double ratioY = (double)maxHeight / image.Height;
                    double ratio = Math.Min(ratioX, ratioY);

                    thumbnailWidth = (int)(image.Width * ratio);
                    thumbnailHeight = (int)(image.Height * ratio);
                }

                // Create a new bitmap with the calculated dimensions
                using (var thumbnailImage = new Bitmap(thumbnailWidth, thumbnailHeight))
                using (var graphics = Graphics.FromImage(thumbnailImage))
                {
                    graphics.DrawImage(image, 0, 0, thumbnailWidth, thumbnailHeight);

                    using (var stream = new MemoryStream())
                    {
                        thumbnailImage.Save(stream, ImageFormat.Jpeg);
                        return stream.ToArray();
                    }
                }
            }
        }

        //internal static byte[] CreateThumbnail1(string filename, int maxWidth, int maxHeight)
        //{
        //    // Define the path to the image file
        //    var filePath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiency/"), filename);

        //    // Check if the file exists
        //    if (File.Exists(filePath))
        //    {
        //        // Load the image from the file
        //        using (var image = Image.FromFile(filePath))
        //        {
        //            // Calculate dimensions for the thumbnail
        //            int thumbnailWidth, thumbnailHeight;
        //            if (image.Width <= maxWidth && image.Height <= maxHeight)
        //            {
        //                // If the image is already within the bounds, no need to resize
        //                thumbnailWidth = image.Width;
        //                thumbnailHeight = image.Height;
        //            }
        //            else
        //            {
        //                // Calculate new dimensions while preserving the aspect ratio
        //                double ratioX = (double)maxWidth / image.Width;
        //                double ratioY = (double)maxHeight / image.Height;
        //                double ratio = Math.Min(ratioX, ratioY);

        //                thumbnailWidth = (int)(image.Width * ratio);
        //                thumbnailHeight = (int)(image.Height * ratio);
        //            }

        //            // Create a new bitmap with the calculated dimensions
        //            using (var thumbnailImage = new Bitmap(thumbnailWidth, thumbnailHeight))
        //            using (var graphics = Graphics.FromImage(thumbnailImage))
        //            {
        //                graphics.DrawImage(image, 0, 0, thumbnailWidth, thumbnailHeight);

        //                using (var stream = new MemoryStream())
        //                {
        //                    thumbnailImage.Save(stream, ImageFormat.Jpeg);
        //                    return stream.ToArray();
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}


        //internal static byte[] GetImageBytes(string filename, int width, int height)
        //{
        //    var filePath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiency/"), filename);

        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        throw new FileNotFoundException("Image not found", filename);
        //    }

        //    using (var image = System.Drawing.Image.FromFile(filePath))
        //    {
        //        var resizedImage = new Bitmap(image, new Size(width, height));
        //        using (var stream = new MemoryStream())
        //        {
        //            resizedImage.Save(stream, ImageFormat.Jpeg);
        //            return stream.ToArray();
        //        }
        //    }
        //}


        internal static Inspection getInspectionById(int InspectionId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.IsActive == true && x.InspectionId == InspectionId).FirstOrDefault();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getInspectionByContactId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> listObj = new List<InspectionViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                var cont = db.CustomerLocationContacts.Where(x => x.UserID == userId).FirstOrDefault();
                if (cont != null)
                {
                    var item = db.Inspections.Where(x => x.IsActive == true).ToList();
                    foreach (var d in item)
                    {
                        if (d.CustomerContactIds != null)
                        {
                            var contact = d.CustomerContactIds.Split(',');
                            foreach (var id in contact)
                            {
                                if (id == cont.LocationContactId.ToString())
                                {
                                    InspectionViewModel _obj = new InspectionViewModel();
                                    _obj.InspectionId = d.InspectionId;
                                    _obj.InspectionDocumentNo = d.InspectionDocumentNo;
                                    _obj.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                                    _obj.InspectionType = d.InspectionType;
                                    _obj.InspectionDate = d.InspectionDate;
                                    _obj.Reportdate = d.Reportdate;
                                    _obj.InspectionStatus = d.InspectionStatus;
                                    _obj.InspectionStartedOn = d.InspectionStartedOn;
                                    _obj.InspectionEndOn = d.InspectionEndOn;
                                    if (d.CustomerId != 0)
                                    {
                                        var cust = getCustomerById(d.CustomerId);
                                        if (cust != null) { _obj.Customer = cust.CustomerName; }
                                    }
                                    if (d.CustomerLocationId != 0)
                                    {
                                        var loc = getCustomerLocationById(Convert.ToInt16(d.CustomerLocationId));
                                        if (loc != null) { _obj.CustomerLocation = loc.LocationName; }
                                    }
                                    if (d.CustomerAreaID != 0)
                                    {
                                        var area = getAreaDetailsById(Convert.ToInt16(d.CustomerAreaID));
                                        if (area != null) { _obj.CustomerArea = area.AreaName; }
                                    }
                                    if (d.EmployeeId != 0)
                                    {
                                        var emp = getEmployeeById(Convert.ToInt16(d.EmployeeId));
                                        if (emp != null) { _obj.Employee = emp.EmployeeName; }
                                    }
                                    _obj.InspectionPDFPath = d.InspectionPDFPath;
                                    listObj.Add(_obj);
                                }
                            }
                        }

                    }
                    return listObj;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getAllApproveAndCompleteInspection()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {

                var list = getAllInspection();
                var itm = list.Where(x => x.InspectionStatus == 4).ToList();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getAllSentForApprovalInspection()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = getAllInspection();
                var itm = list.Where(x => x.InspectionStatus == 3).ToList();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getAllInProgressInspection()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = getAllInspection();
                var itm = list.Where(x => x.InspectionStatus == 2).ToList();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getAllDueInspection()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = getAllInspection();
                var itm = list.Where(x => x.InspectionStatus == 1).OrderBy(x => x.InspectionDate).ToList();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static int getAppAndCompletedInspectionCountByEmployeeId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 4 && x.EmployeeId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getSentForApprovalInspectionCountByEmployeeId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 3 && x.EmployeeId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getInProgressInspectionCountByEmployeeId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 2 && x.EmployeeId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getDueInspectionCountByEmployeeId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 1 && x.EmployeeId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getAppAndCompletedInspectionCountByCustomerId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 4 && x.CustomerId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getSentForApprovalInspectionCountByCustomerId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 3 && x.CustomerId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getInProgressInspectionCountByCustomerId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 2 && x.CustomerId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static int getDueInspectionCountByCustomerId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 1 && x.CustomerId == id && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static long getAllDueInspectionbyYear(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 1 && x.InspectionDate.Year == year && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static long getAllInProgressInspectionbyYear(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 2 && x.InspectionDate.Year == year && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static long getAllSentToApprovalInspectionbyYear(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 3 && x.InspectionDate.Year == year && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        internal static long getAllApprovedAndCompleteInspectionbyYear(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionStatus == 4 && x.InspectionDate.Year == year && x.IsActive == true).ToList().Count();
                return itm;
            }
        }

        //Inspection Due - Admin Dashboard
        internal static List<InspectionViewModel> getAllDueInspectionAdminDashboard()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = getAllInspection();
                var itm = list.Where(x => x.InspectionStatus == 1).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getDueInspectionByCustomerId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var loggedInUserId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                var loggedInUserType = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserType"]);

                // Ensure that only users with type 4 or 9 can proceed
                if (loggedInUserType != 4 && loggedInUserType != 9 || loggedInUserId == 0)
                {
                    return null;
                }

                // Retrieve customer information
                var customer = db.Customers.FirstOrDefault(x => x.UserID == loggedInUserId);

                // If customer exists, retrieve due inspections based on customer ID
                if (customer != null)
                {
                    var list = getAllDueInspection();
                    var inspections = list
                        .Where(x => x.CustomerId == customer.CustomerId)
                        .OrderByDescending(x => x.CreatedBy)
                        .Take(10)
                        .ToList();

                    return inspections.Any() ? inspections : null;
                }

                // If the user is of type 9, check for associated location contacts
                if (loggedInUserType == 9)
                {
                    var user = db.Users.FirstOrDefault(x => x.UserId == loggedInUserId);
                    if (user != null)
                    {
                        var locationContactIds = db.CustomerLocationContacts
                            .Where(clc => clc.UserID == loggedInUserId)
                            .Select(clc => clc.LocationContactId)
                            .ToList();

                        if (locationContactIds.Any())
                        {
                            var customerLocationIds = db.CustomersLocationsUsers
                                .Where(clu => locationContactIds.Contains(clu.LocationContactId))
                                .Select(clu => clu.CustomerLocationID)
                                .Distinct()
                                .ToList();

                            var list = getAllDueInspection();
                            var inspections = list
                                .Where(i => customerLocationIds.Contains(i.CustomerLocationId))
                                .OrderByDescending(i => i.CreatedBy)
                                .Take(10)
                                .ToList();

                            return inspections.Any() ? inspections : null;
                        }
                    }
                }
                return null;

                //var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                //if (Convert.ToInt64(HttpContext.Current.Session["LoggedInUserType"]) == 4 || Convert.ToInt64(HttpContext.Current.Session["LoggedInUserType"]) == 9)
                //{
                //    if (userId != 0)
                //    {
                //        var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                //        if (customer != null)
                //        {
                //            var list = getAllDueInspection();//Inspection table
                //            var itm = list.Where(x => x.CustomerId == customer.CustomerId).OrderByDescending(x => x.CreatedBy).Take(10).ToList();
                //            if (itm != null)
                //            {
                //                return itm;
                //            }
                //        }
                //        else
                //        {
                //            var usr = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
                //            if (usr != null)
                //            {
                //                if (usr.UserType == 9)
                //                {
                //                    var locationContactIds = db.CustomerLocationContacts.Where(clc => clc.UserID == userId).Select(clc => clc.LocationContactId).ToList();
                //                    var customerLocationIds = db.CustomersLocationsUsers.Where(clu => locationContactIds.Contains(clu.LocationContactId)).Select(clu => clu.CustomerLocationID).Distinct().ToList();
                //                    var list = getAllDueInspection().Where(i => customerLocationIds.Contains(i.CustomerLocationId)).OrderByDescending(i => i.CreatedBy).Take(10).ToList();
                //                    if (list != null)
                //                    {
                //                        return list;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}                
                //return null;
            }
        }

        internal static InspectionViewModel getInspectionDetailsById(int InspectionId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                InspectionViewModel obj = new InspectionViewModel();
                var itm = db.Inspections.Where(x => x.IsActive == true && x.InspectionId == InspectionId).FirstOrDefault();
                if (itm != null)
                {
                    obj.InspectionId = itm.InspectionId;
                    obj.InspectionDocumentNo = itm.InspectionDocumentNo;
                    obj.InspectionDocumentNoRef = itm.InspectionDocumentNoRef;
                    obj.InspectionType = itm.InspectionType;
                    obj.InspectionDate = itm.InspectionDate;
                    obj.Reportdate = itm.Reportdate;
                    obj.InspectionStatus = itm.InspectionStatus;
                    obj.InspectionStartedOn = itm.InspectionStartedOn;
                    obj.InspectionEndOn = itm.InspectionEndOn;
                    if (itm.CustomerId != 0)
                    {
                        obj.Customer = getCustomerById(itm.CustomerId).CustomerName;
                    }
                    if (itm.CustomerLocationId != 0)
                    {
                        obj.CustomerLocation = getCustomerLocationById(Convert.ToInt16(itm.CustomerLocationId)).LocationName;
                    }
                    if (itm.CustomerAreaID != null)
                    {
                        obj.CustomerArea = getAreaDetailsById(Convert.ToInt16(itm.CustomerAreaID)).AreaName;
                    }
                    else
                    {
                        obj.CustomerArea = "0";
                    }
                    obj.Employee = getUserEmployeeById(Convert.ToInt16(itm.EmployeeId)).EmployeeName;
                    obj.InspectionPDFPath = itm.InspectionPDFPath;
                    return obj;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getInspectionDetailsByLocationId(int CustomerId, int CustomerLocationId, bool bForTech = false)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> objList = new List<InspectionViewModel>();
                List<Inspection> iListInspection = new List<Inspection>();

                if (bForTech == true)
                {
                    iListInspection = db.Inspections.Where(x => x.CustomerId == CustomerId && x.CustomerLocationId == CustomerLocationId && x.IsActive == true && x.InspectionStatus >= 4).OrderBy(x => x.InspectionStatus).ThenBy(x => x.InspectionDate).ToList();
                }
                else
                {
                    iListInspection = db.Inspections.Where(x => x.CustomerId == CustomerId && x.CustomerLocationId == CustomerLocationId && x.IsActive == true).OrderBy(x => x.InspectionStatus).ThenBy(x => x.InspectionDate).ToList();
                }

                if (iListInspection != null)
                {
                    foreach (var d in iListInspection)
                    {

                        InspectionViewModel obj = new InspectionViewModel();
                        obj.InspectionId = d.InspectionId;
                        obj.InspectionDocumentNo = d.InspectionDocumentNo;
                        obj.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                        obj.InspectionType = d.InspectionType;
                        obj.InspectionDate = d.InspectionDate;
                        obj.Reportdate = d.Reportdate;
                        obj.InspectionStatus = d.InspectionStatus;
                        obj.InspectionStartedOn = d.InspectionStartedOn;
                        obj.InspectionEndOn = d.InspectionEndOn;
                        obj.InspectionPDFPath = d.InspectionPDFPath;
                        objList.Add(obj);
                    }
                    return objList;
                }
                return null;
            }
        }

        internal static List<InspectionViewModel> getInspectionDetailsByLocationIdAreaId(int CustomerId, int CustomerLocationId, int AreaId, bool bForTech = false)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionViewModel> objList = new List<InspectionViewModel>();
                List<Inspection> iListInspection = new List<Inspection>();

                if (bForTech == true)
                {
                    iListInspection = db.Inspections.Where(x => x.CustomerId == CustomerId && x.CustomerLocationId == CustomerLocationId && x.CustomerAreaID == AreaId && x.IsActive == true && x.InspectionStatus >= 4).OrderBy(x => x.InspectionStatus).ThenBy(x => x.InspectionDate).ToList();
                }
                else
                {
                    iListInspection = db.Inspections.Where(x => x.CustomerId == CustomerId && x.CustomerLocationId == CustomerLocationId && x.CustomerAreaID == AreaId && x.IsActive == true).OrderBy(x => x.InspectionStatus).ThenBy(x => x.InspectionDate).ToList();
                }

                if (iListInspection != null)
                {
                    foreach (var d in iListInspection)
                    {

                        InspectionViewModel obj = new InspectionViewModel();
                        obj.InspectionId = d.InspectionId;
                        obj.InspectionDocumentNo = d.InspectionDocumentNo;
                        obj.InspectionDocumentNoRef = d.InspectionDocumentNoRef;
                        obj.InspectionType = d.InspectionType;
                        obj.InspectionDate = d.InspectionDate;
                        obj.Reportdate = d.Reportdate;
                        obj.InspectionStatus = d.InspectionStatus;
                        obj.InspectionStartedOn = d.InspectionStartedOn;
                        obj.InspectionEndOn = d.InspectionEndOn;
                        obj.InspectionPDFPath = d.InspectionPDFPath;
                        objList.Add(obj);
                    }
                    return objList;
                }
                return null;
            }
        }
        // Admin Dashboard - Recent Inspection
        internal static List<InspectionViewModel> getRecentInspectionAdminDashboard()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    long userID = 0;
                    if (HttpContext.Current.Session["LoggedInUserId"] != null)
                    {
                        userID = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    }

                    List<InspectionViewModel> _list = new List<InspectionViewModel>();
                    var list = getAllInspection();
                    var itm = list.OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                    if (itm.Count != 0)
                    {
                        return itm;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionViewModel> getRecentInspectionCustomerByEmployeeId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    long userID = 0;
                    if (HttpContext.Current.Session["LoggedInUserId"] != null)
                    {
                        userID = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    }
                    var employee = db.Employees.Where(x => x.UserID == userID).FirstOrDefault();
                    if (employee != null)
                    {
                        var list = getAllInspection();
                        var itm = list.Where(x => x.EmployeeId == employee.EmployeeID && x.InspectionStatus > 1).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                        if (itm.Count != 0)
                        {
                            return itm;
                        }
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionViewModel> getRecentInspectionCustomerByEmployeeIdForMobile(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    if (id != 0)
                    {
                        var list = getAllInspection();
                        var itm = list.Where(x => x.EmployeeId == id && x.InspectionStatus > 1).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                        if (itm.Count != 0)
                        {
                            return itm;
                        }
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionViewModel> GetAllInspectionDueEmployeeIdForMobile(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    if (id != 0)
                    {
                        var list = getAllInspection();
                        var itm = list.Where(x => x.EmployeeId == id && x.InspectionStatus == 1).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                        if (itm.Count != 0)
                        {
                            return itm;
                        }
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        internal static List<InspectionViewModel> getRecentCompletedInspectionbyCustomerId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                        if (customer != null)
                        {
                            var list = getAllInspection();
                            var itm = list.Where(x => x.CustomerId == customer.CustomerId && x.InspectionStatus == 4).OrderByDescending(x => x.CreatedDate).Take(20).ToList();
                            if (itm.Count != 0)
                            {
                                return itm;
                            }
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionViewModel> getRecentInspectionbyCustomerId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    if (userId != 0)
                    {
                        var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                        if (customer != null)
                        {
                            var list = getAllInspectionByCustomerId(1);
                            var itm = list.Take(20).ToList();
                            if (itm.Count != 0)
                            {
                                return itm;
                            }
                        }
                        else
                        {
                            var usr = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
                            if (usr != null)
                            {
                                if (usr.UserType == 9)
                                {
                                    var locationContactIds = db.CustomerLocationContacts.Where(clc => clc.UserID == userId).Select(clc => clc.LocationContactId).ToList();
                                    var customerLocationIds = db.CustomersLocationsUsers.Where(clu => locationContactIds.Contains(clu.LocationContactId)).Select(clu => clu.CustomerLocationID).Distinct().ToList();
                                    var list = getAllInspectionByCustomerId(1).Where(i => customerLocationIds.Contains(i.CustomerLocationId)).OrderByDescending(i => i.CreatedBy).Take(10).ToList();
                                    if (list != null)
                                    {
                                        return list;
                                    }
                                }
                            }
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        //public virtual string UploadFiles(object obj)
        //{
        //    var length = Request.ContentLength;
        //    var bytes = new byte[length];
        //    Request.InputStream.Read(bytes, 0, length);

        //    var fileName = Request.Headers["X-File-Name"];
        //    var fileSize = Request.Headers["X-File-Size"];
        //    var fileType = Request.Headers["X-File-Type"];


        //    var saveToFileLoc = "\\\\adcyngctg\\HRMS\\Images\\" + fileName;


        //    // save the file.
        //    var fileStream = new FileStream(saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
        //    fileStream.Write(bytes, 0, length);
        //    fileStream.Close();

        //    return string.Format("{0} bytes uploaded", bytes.Length);
        //}
        internal static string saveInspection(Inspection model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("Started process for saving or updating inspection in saveInspection function with details like " + json + " done by " + model.CreatedBy + " on " + DateTime.Now);
            List<string> sDeletedInspectionDeficiency;
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itmInspection = db.Inspections.Where(x => x.InspectionId == model.InspectionId).FirstOrDefault();
                    Inspection obj = new Inspection();
                    if (itmInspection == null)
                    {
                        obj.InspectionDocumentNo = model.InspectionDocumentNo;
                        obj.InspectionDocumentNoRef = model.InspectionDocumentNoRef;
                        obj.InspectionType = model.InspectionType;
                        obj.InspectionDate = model.InspectionDate;
                        obj.Reportdate = model.Reportdate;
                        obj.InspectionStatus = model.InspectionStatus;
                        obj.InspectionStartedOn = model.InspectionStartedOn;
                        obj.InspectionEndOn = model.InspectionEndOn;
                        obj.CustomerId = model.CustomerId;
                        obj.CustomerLocationId = model.CustomerLocationId;
                        obj.CustomerAreaID = model.CustomerAreaID;
                        obj.FacilitiesAreasIds = RemoveDuplicates(model.FacilitiesAreasIds);
                        obj.ProcessOverviewIds = RemoveDuplicates(model.ProcessOverviewIds);
                        obj.ConclusionRecommendationsIds = RemoveDuplicates(model.ConclusionRecommendationsIds);
                        obj.EmployeeId = model.EmployeeId;
                        obj.InspectionPDFPath = model.InspectionPDFPath;
                        obj.CADDocuments = model.CADDocuments;
                        obj.IsActive = true;
                        obj.CreatedDate = DateTime.Now;
                        obj.CreatedBy = model.CreatedBy;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = model.CreatedBy;
                        obj.CustomerContactIds = model.CustomerContactIds;
                        db.Inspections.Add(obj);
                        db.SaveChanges();

                        sDeletedInspectionDeficiency = model.DeletedInspectionDeficiency;
                        if (sDeletedInspectionDeficiency != null)
                        {
                            foreach (var iDefi in sDeletedInspectionDeficiency)
                            {
                                var iInspectionDeficiency = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == Convert.ToInt64(iDefi)).FirstOrDefault();
                                if (iInspectionDeficiency != null)
                                {
                                    iInspectionDeficiency.IsDelete = true;
                                    iInspectionDeficiency.ModifiedBy = model.CreatedBy;
                                    iInspectionDeficiency.ModifiedDate = DateTime.Now;
                                    db.Entry(iInspectionDeficiency).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        //itmInspection.InspectionId = model.InspectionId;
                        itmInspection.InspectionDocumentNo = model.InspectionDocumentNo;
                        itmInspection.InspectionDocumentNoRef = model.InspectionDocumentNoRef;
                        itmInspection.InspectionType = model.InspectionType;
                        itmInspection.InspectionDate = model.InspectionDate;
                        itmInspection.Reportdate = model.Reportdate;
                        itmInspection.InspectionStatus = model.InspectionStatus;
                        itmInspection.InspectionStartedOn = model.InspectionStartedOn;
                        itmInspection.InspectionEndOn = model.InspectionEndOn;
                        itmInspection.CustomerId = model.CustomerId;
                        itmInspection.CustomerLocationId = model.CustomerLocationId;
                        itmInspection.CustomerAreaID = model.CustomerAreaID;
                        itmInspection.FacilitiesAreasIds = RemoveDuplicates(model.FacilitiesAreasIds);
                        itmInspection.ProcessOverviewIds = RemoveDuplicates(model.ProcessOverviewIds);
                        itmInspection.ConclusionRecommendationsIds = RemoveDuplicates(model.ConclusionRecommendationsIds);
                        //itmInspection.EmployeeId = model.EmployeeId;
                        itmInspection.InspectionPDFPath = model.InspectionPDFPath;
                        itmInspection.CADDocuments = model.CADDocuments;
                        itmInspection.IsActive = true;
                        itmInspection.ModifiedBy = model.CreatedBy;
                        itmInspection.ModifiedDate = DateTime.Now;
                        db.Entry(itmInspection).State = EntityState.Modified;
                        db.SaveChanges();
                        obj.InspectionId = itmInspection.InspectionId;

                        sDeletedInspectionDeficiency = model.DeletedInspectionDeficiency;
                        if (sDeletedInspectionDeficiency != null)
                        {
                            foreach (var iDefi in sDeletedInspectionDeficiency)
                            {
                                long lInspectionDeficiencyId = Convert.ToInt64(iDefi);
                                var iInspectionDeficiency = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == lInspectionDeficiencyId).FirstOrDefault();
                                if (iInspectionDeficiency != null)
                                {
                                    iInspectionDeficiency.IsDelete = true;
                                    db.Entry(iInspectionDeficiency).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    return obj.InspectionId.ToString();
                }
                catch (Exception ex)
                {
                    Logger.Info("Error in editing/saving Inspection saveInspection with details like " + ex.Message.ToString() + " at " + DateTime.Now);
                    return ex.Message;
                }
            }
        }

        internal static string editInspection(Inspection model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("Started process for edit inspection in editInspection function with details like " + json + " done by " + model.CreatedBy + " on " + DateTime.Now);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.Inspections.Where(x => x.InspectionId == model.InspectionId).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.InspectionId = model.InspectionId;
                        itm.InspectionDocumentNo = model.InspectionDocumentNo;
                        itm.InspectionDocumentNoRef = model.InspectionDocumentNoRef;
                        itm.InspectionType = model.InspectionType;
                        itm.InspectionDate = model.InspectionDate;
                        itm.Reportdate = model.Reportdate;
                        itm.InspectionStatus = model.InspectionStatus;
                        itm.InspectionStartedOn = model.InspectionStartedOn;
                        itm.InspectionEndOn = model.InspectionEndOn;
                        itm.CustomerId = model.CustomerId;
                        itm.CustomerLocationId = model.CustomerLocationId;
                        itm.CustomerAreaID = model.CustomerAreaID;
                        itm.FacilitiesAreasIds = RemoveDuplicates(model.FacilitiesAreasIds);
                        itm.ProcessOverviewIds = RemoveDuplicates(model.ProcessOverviewIds);
                        itm.ConclusionRecommendationsIds = RemoveDuplicates(model.ConclusionRecommendationsIds);
                        //itm.EmployeeId = model.EmployeeId;
                        itm.InspectionPDFPath = model.InspectionPDFPath;
                        itm.CADDocuments = model.CADDocuments;
                        itm.IsActive = true;
                        itm.ModifiedBy = model.CreatedBy;
                        itm.ModifiedDate = DateTime.Now;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    Logger.Info("Error in editing Inspection editInspection function with details like " + ex.Message.ToString() + " at " + DateTime.Now);
                    return null;
                }
            }
        }

        internal static string editInspectionStatus(long inspectionId, int inspectionStatus)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.Inspections.Where(x => x.InspectionId == inspectionId).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.InspectionStatus = inspectionStatus;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string UpdateInspectionStampingEngineer(long inspectionId, long iStampingEngineerId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.Inspections.Where(x => x.InspectionId == inspectionId).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.StampingEngineerId = iStampingEngineerId;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        internal static string removeInspection(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.Inspections.Where(x => x.InspectionId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }


        internal static List<InspectionType> getAllInspectionType()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionTypes.OrderByDescending(x => x.InspectionTypeCode).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static List<InspectionDeficiency> getAllInspectionDeficiency()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionDeficiencies.ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
        }

        internal static InspectionDeficiency getAllInspectionDeficiencyById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == id).FirstOrDefault();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
        }

        internal static string saveInspectionDeficiency(InspectionDeficiencyViewModel model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("Started process for save Inspection Deficiency in saveInspectionDeficiency function with details like " + json + " done by " + model.CreatedBy + " on " + DateTime.Now);
            int i = 0;
            string strReturn = "";
            bool isEdit = false;
            List<InspectionDeficiencyPhotoViewModel> objInspectionDeficiencyPhoto = new List<InspectionDeficiencyPhotoViewModel>();

            List<InspectionDeficiencyMTOViewModel> objInspectionDeficiencyMTO = new List<InspectionDeficiencyMTOViewModel>();


            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itmInspectionDeficiencies = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == model.InspectionDeficiencyId).FirstOrDefault();
                    if (itmInspectionDeficiencies == null)
                    {
                        isEdit = false;
                    }
                    else
                    {
                        isEdit = true;
                    }

                    InspectionDeficiency obj = new InspectionDeficiency();
                    obj.InspectionDeficiencyId = model.InspectionDeficiencyId;
                    obj.InspectionId = model.InspectionId;
                    obj.CustomerNomenclatureNo = model.CustomerNomenclatureNo;
                    obj.CustomerNomenclatureBayNoID = model.CustomerNomenclatureBayNoID;
                    obj.BayFrameSide = model.BayFrameSide;
                    obj.BeamFrameLevel = model.BeamFrameLevel;
                    obj.ConclusionRecommendationsID = model.ConclusionRecommendationsID;
                    obj.ConclusionRecommendationsTitle = model.ConclusionRecommendationsTitle;
                    obj.DeficiencyID = model.DeficiencyID;
                    obj.DeficiencyType = model.DeficiencyType;
                    obj.DeficiencyInfo = model.DeficiencyInfo;
                    obj.Action_Monitor = model.Action_Monitor;
                    obj.Action_ReferReport = model.Action_ReferReport;
                    obj.Action_Repair = model.Action_Repair;
                    obj.Action_Replace = model.Action_Replace;
                    obj.Severity_IndexNo = model.Severity_IndexNo;
                    obj.InspectionDeficiencyTechnicianStatus = model.InspectionDeficiencyTechnicianStatus;
                    obj.CreatedBy = model.CreatedBy;
                    obj.CreatedDate = DateTime.Now;
                    obj.ModifiedBy = model.ModifiedBy;
                    obj.ModifiedDate = DateTime.Now;
                    //obj.IsDelete = false;

                    //db.Set<InspectionDeficiency>().AddOrUpdate(obj);
                    //db.Set<InspectionDeficiency>().AddOrUpdate(obj);
                    if (isEdit == true)
                    {
                        var existingEntity = db.InspectionDeficiencies.Find(model.InspectionDeficiencyId);
                        db.Entry(existingEntity).CurrentValues.SetValues(obj);
                        db.SaveChanges();

                        //db.InspectionDeficiencies.Attach(obj);
                        ////db.Entry(obj).State = EntityState.Modified;
                        //db.SaveChanges();
                    }
                    else
                    {
                        db.InspectionDeficiencies.Add(obj);
                        db.SaveChanges();
                    }
                    strReturn = strReturn + "|" + obj.InspectionDeficiencyId.ToString();
                    objInspectionDeficiencyPhoto = model.InspectionDeficiencyPhotoViewModel;
                    objInspectionDeficiencyMTO = model.InspectionDeficiencyMTO;

                    if (objInspectionDeficiencyPhoto != null)
                    {

                        if (isEdit == true)
                        {
                            var photosToDelete = db.InspectionDeficiencyPhotoes
                                            .Where(photo => photo.InspectionDeficiencyId == model.InspectionDeficiencyId).ToList();
                            db.InspectionDeficiencyPhotoes.RemoveRange(photosToDelete);
                            db.SaveChanges();
                        }

                        foreach (var AllPhoto in objInspectionDeficiencyPhoto)
                        {
                            string imageName = "";
                            i = i + 1;
                            if (!string.IsNullOrEmpty(AllPhoto.base64DeficiencyPhotoImage))
                            {
                                //string imageName = AllPhoto.DeficiencyPhoto;

                                imageName = model.InspectionId.ToString() + "_" + obj.InspectionDeficiencyId.ToString() + "_" + i.ToString() + ".jpg";
                                //Image image = Base64ToImage(Image.base64Image);
                                String path = HttpContext.Current.Server.MapPath("~/img/deficiency/");
                                string TempOutputPath = HostingEnvironment.MapPath("~/img/deficiencythumb/");
                                //string strFilePath = "/img/deficiency/";
                                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                                Uri url = new Uri(tmpURL);
                                string host = url.GetLeftPart(UriPartial.Authority);
                                //set the image path
                                //string imgPath = host + strFilePath +  imageName;
                                string imgPath = Path.Combine(path, imageName);
                                if (AllPhoto.base64DeficiencyPhotoImage.Contains("data:image"))
                                {
                                    //Need To remove some header information at the beginning if image data contains
                                    //ImageDataUrl = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD....";
                                    //Otherwise, this will give an error.
                                    //Remove everything in front of the DataUrl and including the first comma.
                                    //ImageDataUrl = "9j/4AAQSkZJRgABAQAAAQABAAD...
                                    AllPhoto.base64DeficiencyPhotoImage = AllPhoto.base64DeficiencyPhotoImage.Substring(AllPhoto.base64DeficiencyPhotoImage.LastIndexOf(',') + 1);
                                    // removing extra header information 
                                }
                                byte[] imageBytes = Convert.FromBase64String(AllPhoto.base64DeficiencyPhotoImage);
                                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                                ms.Write(imageBytes, 0, imageBytes.Length);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                                image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                                //string TempOutputPath = Path.Combine(HostingEnvironment.MapPath("~/img/deficiencythumb/"), imageName);
                                //if (!File.Exists(TempOutputPath))
                                //{
                                byte[] imageBytesThumb = CreateThumbnail(imageName, 100, 100);
                                var outputPath = Path.Combine(host + "/img/deficiencythumb/", imageName);
                                TempOutputPath = TempOutputPath + "\\" + imageName;
                                File.WriteAllBytes(TempOutputPath, imageBytesThumb);
                                //}
                                var itmInspectionDeficienciesPhoto = db.InspectionDeficiencyPhotoes.Where(x => x.InspectionDeficiencyPhotoPath == imageName.Trim()).FirstOrDefault();
                                if (itmInspectionDeficienciesPhoto == null)
                                {
                                    InspectionDeficiencyPhoto ObjPhoto = new InspectionDeficiencyPhoto();
                                    ObjPhoto.InspectionDeficiencyId = obj.InspectionDeficiencyId;
                                    ObjPhoto.InspectionDeficiencyPhotoPath = imageName.Trim();// AllPhoto.DeficiencyPhoto;
                                    imageName = "";
                                    ObjPhoto.InspectionDeficiencyIsStatus = false;
                                    ObjPhoto.CreatedBy = model.CreatedBy;
                                    ObjPhoto.CreatedDate = DateTime.Now;
                                    ObjPhoto.ModifiedBy = model.CreatedBy;
                                    ObjPhoto.ModifiedDate = DateTime.Now;
                                    db.InspectionDeficiencyPhotoes.Add(ObjPhoto);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                imageName = "";
                                imageName = Path.GetFileName(AllPhoto.DeficiencyPhoto);
                                InspectionDeficiencyPhoto ObjPhoto = new InspectionDeficiencyPhoto();
                                ObjPhoto.InspectionDeficiencyId = obj.InspectionDeficiencyId;
                                ObjPhoto.InspectionDeficiencyPhotoPath = imageName.Trim();// AllPhoto.DeficiencyPhoto;
                                imageName = "";
                                ObjPhoto.InspectionDeficiencyIsStatus = false;
                                ObjPhoto.CreatedBy = model.CreatedBy;
                                ObjPhoto.CreatedDate = DateTime.Now;
                                ObjPhoto.ModifiedBy = model.CreatedBy;
                                ObjPhoto.ModifiedDate = DateTime.Now;
                                db.InspectionDeficiencyPhotoes.Add(ObjPhoto);
                                db.SaveChanges();
                            }
                        }
                    }


                    var iMTO = db.InspectionDeficiencyMTOes.Where(d => d.InspectionDeficiencyId == obj.InspectionDeficiencyId).ToList();
                    if (iMTO != null)
                    {
                        foreach (var itemMTO in iMTO)
                        {
                            db.InspectionDeficiencyMTODetails.RemoveRange(db.InspectionDeficiencyMTODetails.Where(x => x.InspectionDeficiencyMTOId == itemMTO.InspectionDeficiencyMTOId));
                            db.SaveChanges();
                        }

                        db.InspectionDeficiencyMTOes.RemoveRange(db.InspectionDeficiencyMTOes.Where(x => x.InspectionDeficiencyId == obj.InspectionDeficiencyId));
                        db.SaveChanges();
                    }

                    foreach (var AllInspectionDeficiencyMTO in objInspectionDeficiencyMTO)
                    {
                        List<InspectionDeficiencyMTODetailViewModel> objInspectionDeficiencyMTODetail = new List<InspectionDeficiencyMTODetailViewModel>();
                        AllInspectionDeficiencyMTO.InspectionDeficiencyId = obj.InspectionDeficiencyId;
                        objInspectionDeficiencyMTODetail = AllInspectionDeficiencyMTO.iMTOModelDetails;
                        strReturn = saveInspectionDeficiencyMTO(AllInspectionDeficiencyMTO);
                    }
                    return strReturn + "|" + obj.InspectionDeficiencyId.ToString();
                }
                catch (Exception ex)
                {                    
                    Logger.Info("Error in saving Inspection Deficiency in saveInspectionDeficiency function with details like " + ex.Message.ToString() + " at " + DateTime.Now);
                    if (strReturn != "")
                    {

                        strReturn = strReturn + ex.Message.ToString();
                    }
                    return strReturn;
                }
            }
        }

        internal static BulkSaveResult SaveInspectionDeficiencyMobile(List<InspectionDeficiencyViewModel> models)
        {
            var result = new BulkSaveResult
            {
                SuccessCount = 0,
                FailedCount = 0,
                Results = new List<DeficiencySaveResult>()
            };

            Logger.Info($"Started bulk save for {models.Count} inspection deficiencies at {DateTime.Now}");

            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    foreach (var model in models)
                    {
                        try
                        {
                            var deficiencyResult = SaveSingleDeficiency(db, model);
                            result.Results.Add(deficiencyResult);

                            if (deficiencyResult.IsSuccess)
                                result.SuccessCount++;
                            else
                                result.FailedCount++;
                        }
                        catch (Exception ex)
                        {
                            result.FailedCount++;
                            result.Results.Add(new DeficiencySaveResult
                            {
                                IsSuccess = false,
                                InspectionDeficiencyId = model.InspectionDeficiencyId,
                                ErrorMessage = ex.Message
                            });
                            Logger.Error($"Error saving deficiency ID {model.InspectionDeficiencyId}: {ex.Message}");
                        }
                    }                    
                    Logger.Info($"Bulk save completed: {result.SuccessCount} succeeded, {result.FailedCount} failed");
                }
                catch (Exception ex)
                {                   
                    Logger.Error("Transaction rolled back in bulk save: " + ex.Message);
                    throw;
                }
            }
            return result;
        }

        private static DeficiencySaveResult SaveSingleDeficiency(DatabaseEntities db, InspectionDeficiencyViewModel model)
        {
            var result = new DeficiencySaveResult { IsSuccess = false };

            try
            {
                // Check if editing existing record
                var existingDeficiency = db.InspectionDeficiencies
                    .FirstOrDefault(x => x.InspectionDeficiencyId == model.InspectionDeficiencyId);

                bool isEdit = existingDeficiency != null;

                InspectionDeficiency obj = isEdit ? existingDeficiency : new InspectionDeficiency();

                // Map properties
                obj.InspectionDeficiencyId = model.InspectionDeficiencyId;
                obj.InspectionId = model.InspectionId;
                obj.CustomerNomenclatureNo = model.CustomerNomenclatureNo;
                obj.CustomerNomenclatureBayNoID = model.CustomerNomenclatureBayNoID;
                obj.BayFrameSide = model.BayFrameSide;
                obj.BeamFrameLevel = model.BeamFrameLevel;
                obj.ConclusionRecommendationsID = model.ConclusionRecommendationsID;
                obj.ConclusionRecommendationsTitle = model.ConclusionRecommendationsTitle;
                obj.DeficiencyID = model.DeficiencyID;
                obj.DeficiencyType = model.DeficiencyType;
                obj.DeficiencyInfo = model.DeficiencyInfo;
                obj.Action_Monitor = model.Action_Monitor;
                obj.Action_ReferReport = model.Action_ReferReport;
                obj.Action_Repair = model.Action_Repair;
                obj.Action_Replace = model.Action_Replace;
                obj.Severity_IndexNo = model.Severity_IndexNo;
                obj.InspectionDeficiencyTechnicianStatus = model.InspectionDeficiencyTechnicianStatus;
                obj.ModifiedBy = model.ModifiedBy ?? model.CreatedBy;
                obj.ModifiedDate = DateTime.Now;

                if (!isEdit)
                {
                    obj.CreatedBy = model.CreatedBy;
                    obj.CreatedDate = DateTime.Now;
                    db.InspectionDeficiencies.Add(obj);
                }

                db.SaveChanges();

                // Handle Photos
                if (model.InspectionDeficiencyPhotoViewModel?.Any() == true)
                {
                    if (isEdit)
                    {
                        var existingPhotos = db.InspectionDeficiencyPhotoes
                            .Where(p => p.InspectionDeficiencyId == model.InspectionDeficiencyId)
                            .ToList();
                        db.InspectionDeficiencyPhotoes.RemoveRange(existingPhotos);
                        db.SaveChanges();
                    }

                    SaveDeficiencyPhotos(db, obj, model);
                }

                // Handle MTO
                if (model.InspectionDeficiencyMTO?.Any() == true)
                {
                    DeleteExistingMTOs(db, obj.InspectionDeficiencyId);
                    SaveDeficiencyMTOs(db, obj, model.InspectionDeficiencyMTO);
                }

                result.IsSuccess = true;
                result.InspectionDeficiencyId = obj.InspectionDeficiencyId;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                Logger.Error($"Error in SaveSingleDeficiency: " + ex.Message);
            }

            return result;
        }
        private static void SaveDeficiencyPhotos(DatabaseEntities db, InspectionDeficiency deficiency, InspectionDeficiencyViewModel model)
        {
            int photoIndex = 0;
            string basePath = HttpContext.Current.Server.MapPath("~/img/deficiency/");
            string thumbPath = HttpContext.Current.Server.MapPath("~/img/deficiencythumb/");

            foreach (var photoModel in model.InspectionDeficiencyPhotoViewModel)
            {
                photoIndex++;
                string imageName;

                if (!string.IsNullOrEmpty(photoModel.base64DeficiencyPhotoImage))
                {
                    imageName = $"{model.InspectionId}_{deficiency.InspectionDeficiencyId}_{photoIndex}.jpg";

                    // Process base64 image
                    string base64Data = photoModel.base64DeficiencyPhotoImage;
                    if (base64Data.Contains("data:image"))
                    {
                        base64Data = base64Data.Substring(base64Data.LastIndexOf(',') + 1);
                    }

                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    // Save original image
                    string fullPath = Path.Combine(basePath, imageName);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        using (var image = System.Drawing.Image.FromStream(ms))
                        {
                            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                    // Create and save thumbnail
                    byte[] thumbnailBytes = CreateThumbnail(imageName, 100, 100);
                    string thumbFullPath = Path.Combine(thumbPath, imageName);
                    File.WriteAllBytes(thumbFullPath, thumbnailBytes);
                }
                else
                {
                    imageName = Path.GetFileName(photoModel.DeficiencyPhoto);
                }

                // Save photo record
                var photoRecord = new InspectionDeficiencyPhoto
                {
                    InspectionDeficiencyId = deficiency.InspectionDeficiencyId,
                    InspectionDeficiencyPhotoPath = imageName,
                    InspectionDeficiencyIsStatus = false,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = model.CreatedBy,
                    ModifiedDate = DateTime.Now
                };

                db.InspectionDeficiencyPhotoes.Add(photoRecord);
            }

            db.SaveChanges();
        }

        private static void DeleteExistingMTOs(DatabaseEntities db, long deficiencyId)
        {
            var existingMTOs = db.InspectionDeficiencyMTOes
                .Where(m => m.InspectionDeficiencyId == deficiencyId)
                .ToList();

            foreach (var mto in existingMTOs)
            {
                var mtoDetails = db.InspectionDeficiencyMTODetails
                    .Where(d => d.InspectionDeficiencyMTOId == mto.InspectionDeficiencyMTOId)
                    .ToList();
                db.InspectionDeficiencyMTODetails.RemoveRange(mtoDetails);
            }

            db.InspectionDeficiencyMTOes.RemoveRange(existingMTOs);
            db.SaveChanges();
        }

        private static void SaveDeficiencyMTOs(DatabaseEntities db, InspectionDeficiency deficiency, List<InspectionDeficiencyMTOViewModel> mtoModels)
        {
            foreach (var mtoModel in mtoModels)
            {
                mtoModel.InspectionDeficiencyId = deficiency.InspectionDeficiencyId;
                saveInspectionDeficiencyMTO(mtoModel);
            }
        }
        internal static string saveInspectionDeficiencyAdminStatus(long inspectionId, string iAdminStatus)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    if (iAdminStatus != null)
                    {
                        string[] status = iAdminStatus.Split(',');
                        foreach (var d in status)
                        {
                            long longId = Convert.ToInt64(d);
                            var st = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == longId).FirstOrDefault();
                            if (st != null)
                            {
                                st.InspectionDeficiencyAdminStatus = 1;
                                db.Entry(st).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        internal static string SaveUpdateApproveInspectionAdmin(long inspectionId, int iInspectionStatus, string iAdminIspectionDeficiencyIdStatus, long iStampingEngineerId, string sCheckedDocument)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    if (iInspectionStatus != 9)
                    {
                        var itm = db.Inspections.Where(x => x.InspectionId == inspectionId).FirstOrDefault();
                        if (itm != null)
                        {
                            if (sCheckedDocument != null)
                            {
                                itm.ReferenceDocumentIds = sCheckedDocument;
                                db.Entry(itm).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }

                if (iInspectionStatus == 4 || iInspectionStatus == 9)
                {
                    editInspectionStatus(inspectionId, iInspectionStatus);
                }

                if (iInspectionStatus != 9)
                {
                    if (iAdminIspectionDeficiencyIdStatus != null)
                    {
                        saveInspectionDeficiencyAdminStatus(inspectionId, iAdminIspectionDeficiencyIdStatus);
                    }

                    if (iStampingEngineerId > 0)
                    {
                        UpdateInspectionStampingEngineer(inspectionId, iStampingEngineerId);
                    }
                }
                return "Ok";
            }
            catch (Exception)
            {
                return null;
            }
        }
        internal static string GenerateQuotationFromCustomerAsync(long inspectionId, string sCustomerSelectedDeficiencyIds)
        {
            try
            {
                List<string> strCCEmailslist = new List<string>();
                List<QuotationItemListPrepare> objQuotationItemListPrepare = new List<QuotationItemListPrepare>();
                List<ComponentPriceListViewModel> ObjComponentMatched = new List<ComponentPriceListViewModel>();

                string strInspectionDocNumber = "";
                string strQuotationNumber = "";
                string strCustomerName = "";
                string strCustomerLocationName = "";
                long lCustomerId = 0;
                long lCustomerLocationId = 0;
                long? lCustomerAreaID = 0;
                long iEmployeeId = 0;
                List<string> lstDeficiencyIds = null;
                decimal dSurcharge = 0, dMarkup = 0, dGSTPer = 0, dLabour = 0;
                string sQuotationNotes = "", sValidTo = "";
                if (sCustomerSelectedDeficiencyIds != "")
                {
                    lstDeficiencyIds = new List<string>(sCustomerSelectedDeficiencyIds.Split(','));
                }
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    List<ImpSettingsViewModel> objImpSettingsViewModel = (from Is in db.ImpSettings
                                                                          select new ImpSettingsViewModel
                                                                          {
                                                                              SettingID = Is.SettingID,
                                                                              SettingType = Is.SettingType, // Handle null cases
                                                                              SettingValue = Is.SettingValue
                                                                          }).ToList();
                    dSurcharge = 0;
                    dMarkup = 0;
                    dGSTPer = 0;
                    foreach (var item in objImpSettingsViewModel)
                    {
                        if (!string.IsNullOrEmpty(item.SettingValue))
                        {
                            decimal settingValue = Convert.ToDecimal(item.SettingValue);
                            switch (item.SettingType)
                            {
                                case "Surcharge":
                                    dSurcharge = settingValue;
                                    break;
                                case "Markup":
                                    dMarkup = settingValue;
                                    break;
                                case "GST":
                                    dGSTPer = settingValue;
                                    break;
                                case "LABOUR":
                                    dLabour = settingValue;
                                    break;
                            }
                        }
                    }

                    List<QuotationSetting> quotationBasicDetails = db.QuotationSettings.ToList();
                    //// (from Qs in db.QuotationSettings                                                                    select new QuotationSetting
                    //                                                {
                    //                                                    QuotationSettingType = Qs.QuotationSettingType,
                    //                                                    QuotationSettingValue = Qs.QuotationSettingValue
                    //                                                }).ToList();

                    foreach (var item in quotationBasicDetails)
                    {
                        if (!string.IsNullOrEmpty(item.QuotationSettingType))
                        {
                            string qSettingValue = item.QuotationSettingType;
                            switch (item.QuotationSettingType)
                            {
                                case "QuotationNotes":
                                    sQuotationNotes = item.QuotationSettingValue;
                                    break;
                                case "ValidTo":
                                    sValidTo = item.QuotationSettingValue;
                                    break;
                            }
                        }
                    }

                    //var inspectionDeficiency = db.InspectionDeficiencies
                    //                    .Where(x => x.InspectionId == inspectionId).ToList();
                    //foreach (var deficiency in inspectionDeficiency)
                    //{
                    //    deficiency.InspectionDeficiencyRequestQuotation = 0;
                    //    deficiency.InspectionDeficiencyApprovedQuotation = 0;
                    //}
                    //db.SaveChanges();

                    //                  db.InspectionDeficiencies
                    //.Where(x => x.InspectionId == inspectionId)
                    //.Update(x => new InspectionDeficiency
                    //{
                    //    InspectionDeficiencyRequestQuotation = 0,
                    //    InspectionDeficiencyApprovedQuotation = 0
                    //});
                    db.Database.ExecuteSqlCommand(@"UPDATE InspectionDeficiency SET InspectionDeficiencyRequestQuotation = 0,InspectionDeficiencyApprovedQuotation = 0 WHERE InspectionId = @p0", inspectionId);


                    if (sCustomerSelectedDeficiencyIds != null && sCustomerSelectedDeficiencyIds.Any())
                    {
                        // Ensure values are escaped/prepared to avoid SQL injection
                        var idList = string.Join(",", sCustomerSelectedDeficiencyIds.Select(id => $"'{id}'"));

                        var sql = $@"UPDATE InspectionDeficiency SET InspectionDeficiencyRequestQuotation = 1 WHERE InspectionId = @p0 AND CAST(InspectionDeficiencyId AS VARCHAR) IN ({sCustomerSelectedDeficiencyIds})";

                        db.Database.ExecuteSqlCommand(sql, inspectionId);
                    }
                    //sQuotationNotes = (sQuotationNotes ?? string.Empty).Trim();
                    //var deficienciesToUpdate = db.InspectionDeficiencies
                    //                  .Where(x => sCustomerSelectedDeficiencyIds.Contains(x.InspectionDeficiencyId.ToString()) && x.InspectionId == inspectionId)
                    //                  .ToList();

                    //foreach (var deficiency in deficienciesToUpdate)
                    //{
                    //    deficiency.InspectionDeficiencyRequestQuotation = 1;
                    //}

                    //db.SaveChanges();



                    //foreach (var deficiency in deficienciesToUpdate)
                    //{
                    //    deficiency.InspectionDeficiencyRequestQuotation = 1;
                    //}

                    var resultTemp = from mto in db.InspectionDeficiencyMTOes
                                     join def in db.InspectionDeficiencies on mto.InspectionDeficiencyId equals def.InspectionDeficiencyId
                                     join comp in db.Components on mto.ComponentId equals comp.ComponentId
                                     join manu in db.Manufacturers on mto.ManufacturerId equals manu.ManufacturerId into manufacturers
                                     from m in manufacturers.DefaultIfEmpty()
                                     where def.InspectionId == inspectionId
                                           && lstDeficiencyIds.Contains(mto.InspectionDeficiencyId.ToString())
                                           && mto.QuantityReq > 0
                                     select new QuotationItemListPrepare
                                     {
                                         InspectionDeficiencyMTOId = mto.InspectionDeficiencyMTOId,
                                         ComponentId = mto.ComponentId,
                                         ComponentName = comp.ComponentName,
                                         ManufacturerId = mto.ManufacturerId ?? (long?)null,
                                         ManufacturerName = m.ManufacturerName,
                                         Type = mto.Type,
                                         Size_Description = mto.Size_Description != null ? mto.Size_Description.Trim() : string.Empty,
                                         ItemQuantity = mto.QuantityReq,
                                         isFound = false,
                                         ItemPartNo = "",
                                         ItemDescription = "",
                                         ItemUnitPrice = 0,
                                         ItemSurcharge = 0,
                                         ItemMarkup = 0,
                                         ItemPrice = 0,
                                         ItemWeight = 0,
                                         ItemWeightTotal = 0,
                                         LineTotal = 0,
                                         IsTBD = false
                                     };

                    var finalResult = resultTemp.ToList();
                    foreach (var QuotationItemChild in finalResult)
                    {

                        var resultComponentDetailsDeficiencyMTO = (from mtoDetail in db.InspectionDeficiencyMTODetails
                                                                   join propertyValue in db.ComponentPropertyValues
                                                                   on mtoDetail.ComponentPropertyValueId equals propertyValue.ComponentPropertyValueId
                                                                   join propertyType in db.ComponentPropertyTypes
                                                                   on propertyValue.ComponentPropertyTypeId equals propertyType.ComponentPropertyTypeId
                                                                   where mtoDetail.InspectionDeficiencyMTOId == QuotationItemChild.InspectionDeficiencyMTOId
                                                                   select new
                                                                   {
                                                                       propertyType.ComponentPropertyTypeId,
                                                                       propertyType.ComponentPropertyTypeName,
                                                                       propertyValue.ComponentPropertyValueId,
                                                                       propertyValue.ComponentPropertyValue1
                                                                   }).ToList();

                        List<InspectionDeficiencyMTOItemDetail> objItemDetails = new List<InspectionDeficiencyMTOItemDetail>();
                        List<PropertyMatch> propertyMatches = new List<PropertyMatch>();
                        if (resultComponentDetailsDeficiencyMTO.Count > 0)
                        {
                            QuotationItemChild.ItemDetails = new List<InspectionDeficiencyMTOItemDetail>();

                            foreach (var item in resultComponentDetailsDeficiencyMTO)
                            {
                                InspectionDeficiencyMTOItemDetail objInner = new InspectionDeficiencyMTOItemDetail();
                                objInner.ComponentPropertyTypeId = item.ComponentPropertyTypeId;
                                objInner.ComponentPropertyTypeName = item.ComponentPropertyTypeName;
                                objInner.ComponentPropertyValue = item.ComponentPropertyValue1;
                                objInner.ComponentPropertyValueId = item.ComponentPropertyValueId;
                                QuotationItemChild.ItemDetails.Add(objInner);

                                PropertyMatch objPropertyMatch = new PropertyMatch();
                                objPropertyMatch.PropertyTypeId = item.ComponentPropertyTypeId;
                                objPropertyMatch.PropertyValueId = item.ComponentPropertyValueId;
                                objPropertyMatch.PropertyValue = item.ComponentPropertyValue1;
                                propertyMatches.Add(objPropertyMatch);
                            }
                        }

                        string strComponentDesc = "";

                        strComponentDesc = QuotationItemChild.ComponentName + " " + QuotationItemChild.ManufacturerName + " " + QuotationItemChild.Type + " ";
                        strComponentDesc = Convert.ToString(strComponentDesc).Trim();
                        if (QuotationItemChild.ManufacturerId == null)
                        {
                            QuotationItemChild.ManufacturerId = 0;
                        }

                        if (QuotationItemChild.ComponentId == 17)
                        {
                            var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ComponentId == QuotationItemChild.ComponentId).ToList();
                            if (itmComponentPrice != null)
                            {
                                foreach (var itmCalc in itmComponentPrice)
                                {
                                    QuotationItemChild.isFound = true;
                                    QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                    QuotationItemChild.ItemSurcharge = dSurcharge;
                                    QuotationItemChild.ItemMarkup = dMarkup;
                                    QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * dSurcharge * dMarkup;
                                    QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                    QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.ItemLabour = itmCalc.ComponentLabourTime;
                                    QuotationItemChild.ItemLabourTotal = QuotationItemChild.ItemQuantity * itmCalc.ComponentLabourTime;
                                }
                            }

                        }
                        else if (QuotationItemChild.ComponentId == 14)
                        {
                            var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ComponentId == QuotationItemChild.ComponentId).ToList();
                            if (itmComponentPrice != null)
                            {
                                foreach (var itmCalc in itmComponentPrice)
                                {
                                    QuotationItemChild.isFound = true;
                                    QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                    QuotationItemChild.ItemSurcharge = dSurcharge;
                                    QuotationItemChild.ItemMarkup = dMarkup;
                                    QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * dSurcharge * dMarkup;
                                    QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                    QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.ItemLabour = itmCalc.ComponentLabourTime;
                                    QuotationItemChild.ItemLabourTotal = QuotationItemChild.ItemQuantity * itmCalc.ComponentLabourTime;
                                }
                            }
                        }
                        else if (QuotationItemChild.Size_Description.Contains("Labour only"))
                        {
                            var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ItemPartNo == "Labour(H)").ToList();
                            if (itmComponentPrice != null)
                            {
                                foreach (var itmCalc in itmComponentPrice)
                                {
                                    QuotationItemChild.isFound = true;
                                    QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                    QuotationItemChild.ItemSurcharge = dSurcharge;
                                    QuotationItemChild.ItemMarkup = dMarkup;
                                    QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * dSurcharge * dMarkup;
                                    QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                    QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.ItemLabour = 0;
                                    QuotationItemChild.ItemLabourTotal = 0;
                                }
                            }
                        }
                        else
                        {

                            //ComponentPropertiesMatch objComponentPropertiesMatchInput = new ComponentPropertiesMatch();
                            //ComponentPropertiesMatch objComponentPropertiesMatchOutput = new ComponentPropertiesMatch();
                            //List<ComponentPropertiesMatchList> objComponentPropertiesMatchList = new List<ComponentPropertiesMatchList>();
                            //ComponentPropertiesMatchList objComponentPropertyMatch = new ComponentPropertiesMatchList();

                            //objComponentPropertyMatch.ComponentPropertyType = "ComponentId";
                            //objComponentPropertyMatch.ComponentPropertyValue = QuotationItemChild.ComponentId.ToString();
                            //objComponentPropertiesMatchList.Add(objComponentPropertyMatch);

                            //objComponentPropertyMatch = new ComponentPropertiesMatchList();
                            //objComponentPropertyMatch.ComponentPropertyType = "ManufacturerId";
                            //objComponentPropertyMatch.ComponentPropertyValue = QuotationItemChild.ManufacturerId.ToString();
                            //objComponentPropertiesMatchList.Add(objComponentPropertyMatch);

                            //objComponentPropertiesMatchInput.objComponentPropertiesMatchList = objComponentPropertiesMatchList;

                            //var resultComponentDetails = from mt in db.InspectionDeficiencyMTOes
                            //                             join imd in db.InspectionDeficiencyMTODetails on mt.InspectionDeficiencyMTOId equals imd.InspectionDeficiencyMTOId
                            //                             join cpv in db.ComponentPropertyValues on new { imd.ComponentPropertyTypeId, imd.ComponentPropertyValueId }
                            //                             equals new { cpv.ComponentPropertyTypeId, cpv.ComponentPropertyValueId }
                            //                             join cpt in db.ComponentPropertyTypes on imd.ComponentPropertyTypeId equals cpt.ComponentPropertyTypeId
                            //                             where mt.InspectionDeficiencyMTOId == QuotationItemChild.InspectionDeficiencyMTOId
                            //                             select new
                            //                             {
                            //                                 mt.ManufacturerId,
                            //                                 mt.ComponentId,
                            //                                 imd.InspectionDeficiencyMTOId,
                            //                                 imd.ComponentPropertyTypeId,
                            //                                 imd.ComponentPropertyValueId,
                            //                                 cpv.ComponentPropertyValue1,
                            //                                 cpt.ComponentPropertyTypeName
                            //                             };

                            //List<ComponentPriceListViewModel> objInnerComponentPriceListViewModelList = new List<ComponentPriceListViewModel>();
                            //if (resultComponentDetails.Any())
                            //{
                            //    StringBuilder strComponentDescBuilder = new StringBuilder();
                            //    foreach (var itemChild in resultComponentDetails)
                            //    {


                            //        //if (itemChild.ComponentPropertyTypeName == "Type")
                            //        //{
                            //        //    ComponentPropertiesMatchList objTempComponentPropertiesMatchList = new ComponentPropertiesMatchList();
                            //        //    objTempComponentPropertiesMatchList.ComponentPropertyType = "Type";
                            //        //    objTempComponentPropertiesMatchList.ComponentPropertyValue = itemChild.ComponentPropertyValue1.ToUpper();
                            //        //    strComponentDescBuilder.Append(" " + itemChild.ComponentPropertyValue1.ToUpper());
                            //        //    objComponentPropertiesMatchList.Add(objTempComponentPropertiesMatchList);
                            //        //}
                            //        //else
                            //        //{

                            //        //    //if (itemChild.ComponentPropertyValue1.Contains("/"))
                            //        //    //{
                            //        //    //    strComponentDescBuilder.Append(" " + ConvertMixedFractionToDecimal(itemChild.ComponentPropertyValue1).ToString());
                            //        //    //}
                            //        //    //else
                            //        //    //{
                            //        //    strComponentDescBuilder.Append(" " + itemChild.ComponentPropertyValue1);
                            //        //    //}

                            //        //    ComponentPropertiesMatchList newComponentPropertyMatch = new ComponentPropertiesMatchList
                            //        //    {
                            //        //        //ComponentPropertyType = itemChild.ComponentPropertyTypeName,
                            //        //        //ComponentPropertyValue = itemChild.ComponentPropertyValue1.Contains("/")
                            //        //        //    ? ConvertMixedFractionToDecimal(itemChild.ComponentPropertyValue1).ToString()
                            //        //        //    : itemChild.ComponentPropertyValue1.Replace("\"", "")
                            //        //        ComponentPropertyType = itemChild.ComponentPropertyTypeName,
                            //        //        ComponentPropertyValue = itemChild.ComponentPropertyValue1.Replace("\"", "")
                            //        //    };

                            //        //    objComponentPropertiesMatchList.Add(newComponentPropertyMatch);
                            //        //}
                            //    }
                            //}
                            //int iComponentChildCount = 0;
                            //objComponentPropertiesMatchInput.objComponentPropertiesMatchList = objComponentPropertiesMatchList;
                            //iComponentChildCount = objComponentPropertiesMatchInput.objComponentPropertiesMatchList.Count;
                            //var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ComponentId == QuotationItemChild.ComponentId && x.ManufacturerId == QuotationItemChild.ManufacturerId).ToList();
                            //foreach (var item in itmComponentPrice)
                            //{
                            //    ComponentPriceListViewModel objInnerComponentPriceListViewModel = new ComponentPriceListViewModel();
                            //    List<ComponentPriceListViewModelDetails> objInnerComponentPriceListDetail = new List<ComponentPriceListViewModelDetails>();

                            //    objInnerComponentPriceListViewModel.ComponentId = item.ComponentId;
                            //    objInnerComponentPriceListViewModel.ComponentLabourTime = item.ComponentLabourTime;
                            //    objInnerComponentPriceListViewModel.ComponentPrice = item.ComponentPrice;
                            //    objInnerComponentPriceListViewModel.ComponentPriceDescription = item.ComponentPriceDescription;
                            //    objInnerComponentPriceListViewModel.ComponentPriceId = item.ComponentPriceId;
                            //    objInnerComponentPriceListViewModel.ComponentWeight = item.ComponentWeight;
                            //    objInnerComponentPriceListViewModel.ItemPartNo = item.ItemPartNo;
                            //    objInnerComponentPriceListViewModel.ManufacturerId = item.ManufacturerId;
                            //    objInnerComponentPriceListViewModel.Markup = item.Markup;
                            //    objInnerComponentPriceListViewModel.Surcharge = item.Surcharge;

                            //    var itmComponentPriceDetail = db.ComponentPriceListDetails.Where(y => y.ComponentPriceId == item.ComponentPriceId).ToList();

                            //    foreach (var itemDetails in itmComponentPriceDetail)
                            //    {
                            //        ComponentPriceListViewModelDetails objComponentPriceListDetailTemp = new ComponentPriceListViewModelDetails();
                            //        objComponentPriceListDetailTemp.ComponentPropertyTypeId = itemDetails.ComponentPropertyTypeId;
                            //        objComponentPriceListDetailTemp.ComponentPricePropertyValue = itemDetails.ComponentPricePropertyValue;
                            //        objInnerComponentPriceListDetail.Add(objComponentPriceListDetailTemp);
                            //    }
                            //    objInnerComponentPriceListViewModel.ComponentPriceListViewModelDetails = objInnerComponentPriceListDetail;
                            //    objInnerComponentPriceListViewModelList.Add(objInnerComponentPriceListViewModel);
                            //}



                            //string matchingItemDescription = "";
                            //int i = 0;
                            //List<ComponentPriceListViewModel> objMatched = new List<ComponentPriceListViewModel>();
                            //foreach (var item1 in objInnerComponentPriceListViewModelList)
                            //{
                            //    i = 0;
                            //    foreach (var item2 in objComponentPropertiesMatchInput.objComponentPropertiesMatchList)
                            //    {
                            //        if (item2.ComponentPropertyType == "ComponentId" || item2.ComponentPropertyType == "ManufacturerId")
                            //        {
                            //            if (item2.ComponentPropertyValue == QuotationItemChild.ComponentId.ToString())
                            //            {
                            //                i += 1;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            if (item1.ComponentPriceListViewModelDetails != null)
                            //            {
                            //                foreach (var detail in item1.ComponentPriceListViewModelDetails)
                            //                {
                            //                    if (detail.ComponentPricePropertyValue == item2.ComponentPropertyValue)
                            //                    {
                            //                        matchingItemPartNo = item1.ItemPartNo;
                            //                        matchingItemDescription = item1.ComponentPriceDescription;
                            //                        i += 1;
                            //                        break;
                            //                    }
                            //                }
                            //            }
                            //        }

                            //        if (matchingItemPartNo != "")
                            //        {

                            //            //break;
                            //        }
                            //    }

                            //    if (matchingItemPartNo != "")
                            //    {
                            //        ComponentPriceListViewModel objInnerComponentPriceListViewModelTemp = new ComponentPriceListViewModel();
                            //        objInnerComponentPriceListViewModelTemp.ItemPartNo = matchingItemPartNo;
                            //        objInnerComponentPriceListViewModelTemp.iMatched = i;
                            //        objMatched.Add(objInnerComponentPriceListViewModelTemp);
                            //        //break;
                            //    }
                            //}
                            ////iComponentChildCount = iComponentChildCount - 2;
                            ////objMatched = objMatched.OrderByDescending(x => x.iMatched).ToList();
                            ///

                            long matchingComponentPriceId = GetMatchingComponentPriceId(QuotationItemChild.ComponentId, QuotationItemChild.ManufacturerId, propertyMatches);
                            string matchingItemPartNo = "";
                            var itemWithHighestMatched = db.ComponentPriceLists.Where(x => x.ComponentPriceId == matchingComponentPriceId).FirstOrDefault(); //.OrderByDescending(x => x.iMatched)
                                                                                                                                                             //var itemWithHighestMatched = objMatched.OrderByDescending(x => x.iMatched).FirstOrDefault();
                            if (itemWithHighestMatched != null)
                            {
                                matchingItemPartNo = itemWithHighestMatched.ItemPartNo;
                            }
                            else
                            {
                                matchingItemPartNo = "";
                            }
                            if (matchingItemPartNo != "")
                            {
                                matchingItemPartNo = matchingItemPartNo.Trim();
                                var itmComponentPriceMatched = db.ComponentPriceLists.Where(x => x.ItemPartNo == matchingItemPartNo).ToList();
                                if (itmComponentPriceMatched != null)
                                {
                                    foreach (var itmCalc in itmComponentPriceMatched)
                                    {
                                        QuotationItemChild.isFound = true;
                                        QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                        //if (matchingItemDescription != "")
                                        //{
                                        //    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                        //}
                                        //else 
                                        //{
                                        QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                        QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                        //}
                                        QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                        QuotationItemChild.ItemSurcharge = dSurcharge;
                                        QuotationItemChild.ItemMarkup = dMarkup;
                                        QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * dSurcharge * dMarkup;
                                        QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                        QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                        QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                        QuotationItemChild.ItemLabour = itmCalc.ComponentLabourTime;
                                        QuotationItemChild.ItemLabourTotal = itmCalc.ComponentLabourTime * QuotationItemChild.ItemQuantity;
                                    }
                                }
                            }
                            //matchingItemPartNo = "";
                            //matchingItemDescription = "";
                        }
                    }

                    var itm = db.Inspections.Where(x => x.InspectionId == inspectionId).FirstOrDefault();
                    if (itm != null)
                    {
                        iEmployeeId = itm.EmployeeId;
                        itm.InspectionStatus = 5;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                        lCustomerId = itm.CustomerId;
                        lCustomerLocationId = itm.CustomerLocationId;
                        lCustomerAreaID = itm.CustomerAreaID;
                        strInspectionDocNumber = itm.InspectionDocumentNo;
                    }

                    strQuotationNumber = GenerateInspectionDocumentNo(Convert.ToInt32(lCustomerId), Convert.ToInt32(lCustomerLocationId), "QT");

                    Quotation quoteObj = new Quotation();
                    quoteObj.QuotationDate = DateTime.Now;
                    quoteObj.InspectionId = inspectionId;
                    quoteObj.QuotationStatus = 5;
                    quoteObj.CustomerId = lCustomerId;
                    quoteObj.CustomerLocationId = lCustomerLocationId;
                    quoteObj.CustomerAreaID = lCustomerAreaID;





                    quoteObj.QuotationNotes = sQuotationNotes;

                    var objPreviousQuotation = db.Quotations.Where(y => y.InspectionId == inspectionId && (y.QuotationStatus == 5 || y.QuotationStatus == 6)).OrderByDescending(y => y.QuotationId).FirstOrDefault();
                    if (objPreviousQuotation != null)
                    {
                        quoteObj.YourReference = objPreviousQuotation.YourReference;
                        quoteObj.ValidTo = objPreviousQuotation.ValidTo;
                        quoteObj.PaymentTerms = objPreviousQuotation.PaymentTerms;
                        quoteObj.ShipmentMethod = objPreviousQuotation.ShipmentMethod;
                        quoteObj.QuotationSalesPersonId = objPreviousQuotation.QuotationSalesPersonId;
                        quoteObj.QuotationSalesPersonName = objPreviousQuotation.QuotationSalesPersonName;
                        quoteObj.LabourUnitPrice = objPreviousQuotation.LabourUnitPrice;
                        quoteObj.QuotationNotes = objPreviousQuotation.QuotationNotes;
                        dLabour = Convert.ToDecimal(objPreviousQuotation.LabourUnitPrice);
                    }
                    else
                    {
                        quoteObj.PaymentTerms = "NET 30";
                        quoteObj.ValidTo = sValidTo;
                        quoteObj.LabourUnitPrice = dLabour;
                        quoteObj.ShipmentMethod = "FOB";
                    }

                    quoteObj.QuotationNo = strQuotationNumber;
                    quoteObj.GSTPer = dGSTPer;
                    quoteObj.Subtotal = 0;
                    quoteObj.GSTValue = 0;
                    quoteObj.Total = 0;
                    quoteObj.IsActive = true;
                    quoteObj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    quoteObj.CreatedDate = DateTime.Now;
                    quoteObj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    quoteObj.ModifiedDate = DateTime.Now;
                    db.Quotations.Add(quoteObj);
                    db.SaveChanges();

                    if (quoteObj.QuotationId > 0)
                    {
                        var groupedResult = finalResult
                        .GroupBy(item => new { item.ItemPartNo, item.Size_Description })
                        .Select(group => new QuotationItemListPrepare
                        {
                            ItemPartNo = group.Key.ItemPartNo,
                            Size_Description = group.Key.Size_Description,
                            ItemQuantity = group.Sum(item => item.ItemQuantity),
                            ItemUnitPrice = group.First().ItemUnitPrice,
                            ItemMarkup = group.First().ItemMarkup,
                            ItemPrice = group.First().ItemPrice,
                            ItemSurcharge = group.First().ItemSurcharge,
                            ItemWeight = group.First().ItemWeight,
                            ItemWeightTotal = group.First().ItemWeightTotal,
                            LineTotal = group.Sum(item => item.LineTotal),
                            ManufacturerId = group.First().ManufacturerId,
                            ManufacturerName = group.First().ManufacturerName,
                            Type = group.First().Type,
                            isFound = group.First().isFound,
                            ItemDescription = group.First().ItemDescription,
                            IsTBD = false,
                            ComponentName = group.First().ComponentName,
                            ItemLabour = group.First().ItemLabour,
                            ItemLabourTotal = group.Sum(item => item.ItemLabourTotal)
                        }).ToList();

                        foreach (var QuotationItemChild in groupedResult)
                        {
                            if (!QuotationItemChild.Size_Description.Contains("Customer Attention"))
                            {
                                QuotationItem quoteObjCom = new QuotationItem();
                                quoteObjCom.InspectionId = inspectionId;
                                quoteObjCom.QuotationId = quoteObj.QuotationId;
                                quoteObjCom.ItemPartNo = QuotationItemChild.ItemPartNo;
                                quoteObjCom.ItemDescription = QuotationItemChild.Size_Description;
                                if (QuotationItemChild.ItemUnitPrice.HasValue)
                                {
                                    quoteObjCom.ItemUnitPrice = QuotationItemChild.ItemUnitPrice.Value;
                                }
                                else
                                {
                                    quoteObjCom.ItemUnitPrice = 0;
                                }

                                quoteObjCom.ItemSurcharge = dSurcharge;
                                quoteObjCom.ItemMarkup = dMarkup;
                                if (QuotationItemChild.ItemPrice.HasValue)
                                {
                                    quoteObjCom.ItemPrice = QuotationItemChild.ItemPrice.Value;
                                }
                                else
                                {
                                    quoteObjCom.ItemPrice = 0;
                                }

                                if (QuotationItemChild.ItemQuantity.HasValue)
                                {
                                    quoteObjCom.ItemQuantity = QuotationItemChild.ItemQuantity.Value;
                                }
                                else
                                {
                                    quoteObjCom.ItemQuantity = 0;
                                }
                                quoteObjCom.ItemWeight = QuotationItemChild.ItemWeight;
                                quoteObjCom.ItemWeightTotal = QuotationItemChild.ItemWeightTotal;

                                if (QuotationItemChild.LineTotal.HasValue)
                                {
                                    quoteObjCom.LineTotal = QuotationItemChild.LineTotal.Value;
                                }
                                else
                                {
                                    quoteObjCom.LineTotal = 0;
                                }
                                if (QuotationItemChild.ItemLabour.HasValue)
                                {
                                    quoteObjCom.ItemLabour = QuotationItemChild.ItemLabour.Value;
                                }
                                else
                                {
                                    quoteObjCom.ItemLabour = 0;
                                }

                                if (QuotationItemChild.ItemLabourTotal.HasValue)
                                {
                                    quoteObjCom.ItemLabourTotal = QuotationItemChild.ItemLabourTotal.Value;
                                }
                                else
                                {
                                    quoteObjCom.ItemLabourTotal = 0;
                                }

                                quoteObjCom.IsTBD = false;
                                quoteObjCom.CreatedDate = DateTime.Now;
                                quoteObjCom.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                quoteObjCom.ModifiedDate = DateTime.Now;
                                quoteObjCom.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                db.QuotationItems.Add(quoteObjCom);
                                db.SaveChanges();
                            }
                        }

                        var AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == quoteObj.QuotationId).ToList();

                        decimal dSubtotal = 0;
                        //dGSTPer = 0;
                        decimal dGSTValue = 0;
                        decimal dTotal = 0;
                        decimal dLabourMinutes = 0;
                        decimal dHours = 0;
                        decimal dTotalLabourCharges = 0;
                        foreach (var item in AllQuotationItems)
                        {
                            dSubtotal += Convert.ToDecimal(item.LineTotal);
                            dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
                        }
                        if (dLabourMinutes < 240)
                        {
                            dLabourMinutes = 240;
                        }
                        dHours = Math.Round((dLabourMinutes / 60), 2);
                        dTotalLabourCharges = dHours * dLabour;
                        dSubtotal = dSubtotal + Math.Round(dTotalLabourCharges, 2);
                        dGSTPer = Convert.ToDecimal(dGSTPer / 100);
                        decimal gstVal = dSubtotal * dGSTPer;

                        dGSTValue = Convert.ToDecimal(Math.Round(gstVal, 2));
                        dTotal = Convert.ToDecimal(dSubtotal + gstVal);

                        var objUpdateQuotation = db.Quotations.Where(y => y.QuotationId == quoteObj.QuotationId).FirstOrDefault();
                        objUpdateQuotation.Subtotal = dSubtotal;
                        objUpdateQuotation.GSTValue = dGSTValue;
                        objUpdateQuotation.Total = dTotal;
                        objUpdateQuotation.TotalLabour = dLabourMinutes;
                        objUpdateQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2); ;
                        db.Entry(objUpdateQuotation).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                #region "Emails"

                UserEmployeeViewModel objUser = new UserEmployeeViewModel();
                objUser = getUserEmployeeById(iEmployeeId);

                strCCEmailslist.Add("b.trivedi@camindustrial.net");
                //strCCEmailslist.Add("nirav.m@siliconinfo.com");
                var cust = getCustomerById(lCustomerId);
                if (cust != null)
                {
                    strCustomerName = cust.CustomerName;
                }

                var loc = getCustomerLocationById(lCustomerLocationId);
                if (loc != null)
                {
                    strCustomerLocationName = loc.LocationName;
                }
                var toEmailEmployee = objUser.EmployeeEmail;
                //var toEmail = "b.trivedi@camindustrial.net";


                //Email to Employee for quotation
                string strMSGEmployee = "";
                //strMSGEmployee = "<html>";
                //strMSGEmployee += "<head>";
                //strMSGEmployee += "<style>";
                //strMSGEmployee += "p{margin:0px}";
                //strMSGEmployee += "</style>";
                //strMSGEmployee += "</head>";
                //strMSGEmployee += "<body>";
                //strMSGEmployee += "<div style='width: 1200px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";

                //strMSGEmployee += "<br/>";
                //strMSGEmployee += "<br/>";
                //strMSGEmployee += "<p> Attention " + objUser.EmployeeName + ",";

                //strMSGEmployee += "";

                //strMSGEmployee += "<br/>";
                //strMSGEmployee += "<div><div></div></div><br/><br/><div><div>";
                //strMSGEmployee += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                //strMSGEmployee += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                //strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                //strMSGEmployee += "<br/>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                //strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                //strMSGEmployee += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                //strMSGEmployee += "<br/>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                //strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                //strMSGEmployee += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                //strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                //strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                //strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                //strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                //strMSGEmployee += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                //strMSGEmployee += "<p><span><img style='width:2.618in;height:.6458in'";
                //strMSGEmployee += "src='https://rack-manager.com/img/sigimg.png' data-image-whitelisted='' ";
                //strMSGEmployee += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                //strMSGEmployee += "</div>";
                //strMSGEmployee += "</div>";
                //strMSGEmployee += "</div>";
                //strMSGEmployee += "</body>";
                //strMSGEmployee += "</html>";

                strMSGEmployee = "";
                strMSGEmployee = "<html>";
                strMSGEmployee += "<head>";
                strMSGEmployee += "<style>";
                strMSGEmployee += "p{margin:0px}";
                strMSGEmployee += "</style>";
                strMSGEmployee += "</head>";
                strMSGEmployee += "<body>";
                strMSGEmployee += "<div style='width: 1200px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                strMSGEmployee += "<br/>";
                strMSGEmployee += "<br/>";
                strMSGEmployee += "<p> Attention " + objUser.EmployeeName + ",";
                strMSGEmployee += "<p>This is to inform you that the customer has reviewed the deficiency list and selected the red and/or yellow deficiencies. Please proceed with preparing the quotation based on these selections. </p>";
                strMSGEmployee += "<br/>";
                strMSGEmployee += "<div><div></div></div><br/><br/><div><div>";
                strMSGEmployee += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                strMSGEmployee += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                strMSGEmployee += "<br/>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                strMSGEmployee += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                strMSGEmployee += "<br/>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                strMSGEmployee += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                strMSGEmployee += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                strMSGEmployee += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                strMSGEmployee += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                strMSGEmployee += "<p><span><img style='width:2.618in;height:.6458in'";
                strMSGEmployee += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted='' ";
                strMSGEmployee += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                strMSGEmployee += "</div>";
                strMSGEmployee += "</div>";
                strMSGEmployee += "</div>";
                strMSGEmployee += "</body>";
                strMSGEmployee += "</html>";

                //toEmailEmployee
                // Use async/await for email sending (replace Thread with async method)
                //await EmailHelper.SendEmailAsync(toEmailEmployee, strCustomerName + " Deficiency Selections for Quotation", null, strMSGEmployee, strCCEmailslist);

                //toEmailEmployee

                var tEmailToEmployee = new Thread(() => EmailHelper.SendEmail(toEmailEmployee, strCustomerName + " Deficiency Selections for Quotation", null, strMSGEmployee, strCCEmailslist));
                tEmailToEmployee.Start();
                //var tEmailEmployee = new Thread(() => EmailHelper.SendEmail(toEmailEmployee, strCustomerName + " Deficiency Selections for Quotation", null, strMSGEmployee, strCCEmailslist));
                //tEmailEmployee.Start();


                ////Email to Customer for quotation
                //Customer objCustomer = new Customer();
                //objCustomer = getCustomerById(lCustomerId);

                if (cust != null)
                {
                    if (cust.CustomerEmail != null)
                    {
                        string strMSGCustomer = "";
                        var toEmailCustomer = cust.CustomerEmail.Trim();

                        strMSGCustomer = "";
                        strMSGCustomer = "<html>";
                        strMSGCustomer += "<head>";
                        strMSGCustomer += "<style>";
                        strMSGCustomer += "p{margin:0px}";
                        strMSGCustomer += "</style>";
                        strMSGCustomer += "</head>";
                        strMSGCustomer += "<body>";
                        strMSGCustomer += "<div style='width: 1200px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";

                        strMSGCustomer += "<br/>";
                        strMSGCustomer += "<br/>";

                        if (cust.CustomerContactName == null)
                        {
                            cust.CustomerContactName = "";
                        }

                        if (cust.CustomerContactName != "")
                        {
                            strMSGCustomer += "<p>Attention: " + cust.CustomerContactName + " [" + cust.CustomerName + "]</p>";
                        }
                        else
                        {
                            strMSGCustomer += "<p>Attention " + strCustomerName + ",";
                        }


                        strMSGCustomer += "<p>This is to confirm that you have successfully selected the deficiencies for the quotation. Our team is currently preparing the details, and you will be notified as soon as the quotation is ready for your review on the Rack Auditor portal.</p>";
                        strMSGCustomer += "<br/>";
                        strMSGCustomer += "<p>If you have any questions in the meantime, please feel free to reach out.</p>";

                        strMSGCustomer += "<div><div></div></div><br/><br/><div><div>";
                        strMSGCustomer += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                        strMSGCustomer += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                        strMSGCustomer += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                        strMSGCustomer += "<br/>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                        strMSGCustomer += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                        strMSGCustomer += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSGCustomer += "<br/>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                        strMSGCustomer += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                        strMSGCustomer += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                        strMSGCustomer += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                        strMSGCustomer += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                        strMSGCustomer += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                        strMSGCustomer += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                        strMSGCustomer += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSGCustomer += "<p><span><img style='width:2.618in;height:.6458in'";
                        strMSGCustomer += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted='' ";
                        strMSGCustomer += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                        strMSGCustomer += "</div>";
                        strMSGCustomer += "</div>";
                        strMSGCustomer += "</div>";
                        strMSGCustomer += "</body>";
                        strMSGCustomer += "</html>";
                        //toEmailEmployee
                        //await EmailHelper.SendEmailAsync(toEmailCustomer, strCustomerName + " Deficiency Selection Confirmed for Quotation", null, strMSGCustomer, strCCEmailslist);

                        //toEmailCustomer = "nirav.m@siliconinfo.com";
                        var tEmailToCustomer = new Thread(() => EmailHelper.SendEmail(toEmailCustomer, strCustomerName + " Deficiency Selection Confirmed for Quotation", null, strMSGCustomer, strCCEmailslist));
                        tEmailToCustomer.Start();
                        //var tEmailCustomer = new Thread(() => EmailHelper.SendEmail(toEmailCustomer, strCustomerName + " Deficiency Selection Confirmed for Quotation", null, strMSGCustomer, strCCEmailslist));
                        //tEmailCustomer.Start();
                    }
                }
                #endregion


                return "Ok";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        internal static long GetMatchingComponentPriceId(long componentId, long? manufacturerId, List<PropertyMatch> propertyMatches)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                // Get the matching components first by ComponentId and ManufacturerId
                var matchingComponents = db.ComponentPriceLists
                    .Where(c => c.ComponentId == componentId &&
                                (!manufacturerId.HasValue || c.ManufacturerId == manufacturerId) &&
                                c.IsActive == true)
                    .ToList();

                // Iterate through each matching component to check if all properties match
                foreach (var component in matchingComponents)
                {
                    long? matchedComponentPriceId = MatchComponentProperties(component.ComponentPriceId, propertyMatches, db);

                    // If we find a valid match, return the ComponentPriceId
                    if (matchedComponentPriceId.HasValue)
                    {
                        return matchedComponentPriceId.Value;
                    }
                }
            }

            // If no match is found, return 0
            return 0;
        }

        internal static long? MatchComponentProperties(long componentPriceId, List<PropertyMatch> propertyMatches, DatabaseEntities db)
        {
            // Fetch the component details related to the given ComponentPriceId
            var componentDetails = db.ComponentPriceListDetails
                .Where(cpld => cpld.ComponentPriceId == componentPriceId && cpld.IsActive == true)
                .ToList();

            // Perform the property matching directly without recursion
            bool allPropertiesMatch = true;

            foreach (var propertyMatch in propertyMatches)
            {
                // Find the matching property in the component details
                //var matchedProperty = componentDetails
                //    .FirstOrDefault(cpld => cpld.ComponentPropertyTypeId == propertyMatch.PropertyTypeId &&
                //                            cpld.ComponentPricePropertyValue == propertyMatch.PropertyValue);
                var matchedProperty = componentDetails.FirstOrDefault(cpld => cpld.ComponentPropertyTypeId == propertyMatch.PropertyTypeId &&
                             cpld.ComponentPricePropertyValue.ToLower() == propertyMatch.PropertyValue.ToLower()
                             );

                if (matchedProperty == null)
                {
                    matchedProperty = componentDetails.FirstOrDefault(cpld => cpld.ComponentPricePropertyValue.ToLower() == propertyMatch.PropertyValue.ToLower());
                }
                // If any property does not match, set the flag to false and break
                if (matchedProperty == null)
                {
                    allPropertiesMatch = false;
                    break;
                }
            }

            // If all properties matched, return the ComponentPriceId
            if (allPropertiesMatch)
            {
                return componentPriceId;
            }

            return null; // If no match found, return null
        }

        //internal static long GetMatchingComponentPriceId(long componentId, long? manufacturerId, List<PropertyMatch> propertyMatches)
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {

        //        var matchingComponents = db.ComponentPriceLists
        //        .Where(c => c.ComponentId == componentId && (!manufacturerId.HasValue || c.ManufacturerId == manufacturerId) && c.IsActive == true).ToList();


        //        foreach (var component in matchingComponents)
        //        {
        //            long? matchedComponentPriceId = MatchComponentProperties(component.ComponentPriceId, propertyMatches);

        //            if (matchedComponentPriceId.HasValue)
        //            {
        //                return matchedComponentPriceId.Value;
        //            }
        //        }
        //    }


        //    return 0;
        //}

        //internal static long? MatchComponentProperties(long componentPriceId, List<PropertyMatch> propertyMatches)
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        var componentDetails = db.ComponentPriceListDetails
        //     .Where(cpld => cpld.ComponentPriceId == componentPriceId && cpld.IsActive == true)
        //     .ToList();

        //        // If all properties match, return the ComponentPriceId
        //        if (MatchPropertiesRecursive(componentDetails, propertyMatches, 0))
        //        {
        //            return componentPriceId;
        //        }
        //    }
        //    return null; // If no match found, return null
        //}

        //internal static bool MatchPropertiesRecursive(List<ComponentPriceListDetail> componentDetails, List<PropertyMatch> propertyMatches, int matchIndex)
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        if (matchIndex == propertyMatches.Count)
        //        {
        //            // If all properties are matched, return true
        //            return true;
        //        }

        //        var propertyMatch = propertyMatches[matchIndex];

        //        // Find matching property in the component details
        //        var matchedProperty = componentDetails
        //            .FirstOrDefault(cpld => cpld.ComponentPropertyTypeId == propertyMatch.PropertyTypeId &&
        //                                    cpld.ComponentPricePropertyValueId == propertyMatch.PropertyValueId);

        //        if (matchedProperty != null)
        //        {
        //            // If found, recursively check the next property
        //            return MatchPropertiesRecursive(componentDetails, propertyMatches, matchIndex + 1);
        //        }
        //    }


        //    // If no match found for this property, return false
        //    return false;
        //}
        internal static Quotation saveQuotationAdmin(AdminQuotation model)
        {
            Quotation objQuotation = new Quotation();
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(model.InspectionId);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    decimal dTotal = 0;
                    decimal dLabourMinutes = 0;
                    decimal dHours = 0;
                    decimal dTotalLabourCharges = 0;
                    decimal dSubtotal = 0;
                    decimal dGSTPer = 0;
                    decimal dGSTValue = 0;

                    if (model.SendEmailForApproval == true)
                    {
                        model.QuotationStatus = 6;
                        //var itmInspection = db.Inspections.Where(x => x.InspectionId == model.InspectionId).FirstOrDefault();
                        //itmInspection.InspectionStatus = 6;
                        //itmInspection.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        //itmInspection.ModifiedDate = DateTime.Now;
                        //db.Entry(itmInspection).State = EntityState.Modified;
                        var itm = db.Inspections.Where(x => x.InspectionId == model.InspectionId).FirstOrDefault();
                        if (itm != null)
                        {
                            itm.InspectionStatus = 6;
                            itm.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            itm.ModifiedDate = DateTime.Now;
                            db.Entry(itm).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    var objTempQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();

                    var AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();

                    foreach (var objItem in AllQuotationItems)
                    {
                        decimal dItempPrice = 0;
                        int iQty = 0;
                        var objQuotationItem = db.QuotationItems.Where(y => y.QuotationInspectionItemId == objItem.QuotationInspectionItemId).FirstOrDefault();
                        if (objQuotationItem != null)
                        {
                            dItempPrice = objQuotationItem.ItemPrice;
                            if (objQuotationItem.ItemPartNo != "")
                            {
                                var ComponentPriceListItem = db.ComponentPriceLists.Where(x => x.ItemPartNo == objQuotationItem.ItemPartNo).FirstOrDefault();
                                if (ComponentPriceListItem != null)
                                {
                                    if (ComponentPriceListItem.ComponentPrice != null)
                                    {
                                        dItempPrice = Convert.ToDecimal(ComponentPriceListItem.ComponentPrice);
                                    }
                                }
                            }

                            iQty = Convert.ToInt16(objQuotationItem.ItemQuantity);
                            decimal surcharge = Convert.ToDecimal(model.QuotationSurcharge);
                            decimal markup = Convert.ToDecimal(model.QuotationMarkup);
                            decimal itemUnitPrice = Convert.ToDecimal(dItempPrice);
                            decimal itemPrice = (itemUnitPrice * surcharge * markup);
                            itemPrice = Math.Round(itemPrice, 2);

                            decimal lineTotal = (itemPrice * iQty);
                            lineTotal = Math.Round(lineTotal, 2);

                            objQuotationItem.QuotationInspectionItemId = objQuotationItem.QuotationInspectionItemId;
                            objQuotationItem.ItemPartNo = objQuotationItem.ItemPartNo;
                            objQuotationItem.ItemDescription = objQuotationItem.ItemDescription;
                            objQuotationItem.ItemUnitPrice = itemUnitPrice;
                            objQuotationItem.ItemSurcharge = surcharge;
                            objQuotationItem.ItemMarkup = markup;
                            objQuotationItem.ItemPrice = itemPrice;
                            objQuotationItem.ItemQuantity = iQty;
                            objQuotationItem.ItemWeight = objQuotationItem.ItemWeight;
                            objQuotationItem.ItemWeightTotal = objQuotationItem.ItemWeightTotal;
                            objQuotationItem.LineTotal = lineTotal;
                            objQuotationItem.IsTBD = objQuotationItem.IsTBD;
                            objQuotationItem.ItemLabour = objQuotationItem.ItemLabour;
                            objQuotationItem.ItemLabourTotal = objQuotationItem.ItemLabourTotal;
                            objQuotationItem.ModifiedDate = DateTime.Now;
                            objQuotationItem.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            db.Entry(objQuotationItem).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();
                    foreach (var item in AllQuotationItems)
                    {
                        if (item.IsTBD != true)
                        {
                            dSubtotal += Convert.ToDecimal(item.LineTotal);
                            dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
                        }
                    }
                    //Quotation obj = new Quotation();
                    //itmQuotation.QuotationId = model.QuotationId;

                    //dLabourMinutes = Convert.ToDecimal(model.TotalLabour);
                    if (dLabourMinutes < 240)
                    {
                        dLabourMinutes = 240;
                    }
                    dHours = Math.Round((dLabourMinutes / 60), 2);
                    dTotalLabourCharges = Convert.ToDecimal(dHours * model.LabourUnitPrice);
                    dSubtotal = Convert.ToDecimal(dSubtotal + Math.Round(dTotalLabourCharges, 2));
                    dGSTPer = Convert.ToDecimal(model.GSTPer);
                    dGSTPer = Convert.ToDecimal(dGSTPer / 100);
                    decimal gstVal = dSubtotal * dGSTPer;

                    dGSTValue = Convert.ToDecimal(Math.Round(gstVal, 2));
                    dTotal = Convert.ToDecimal(dSubtotal + gstVal);

                    objTempQuotation.Subtotal = dSubtotal;
                    objTempQuotation.GSTValue = dGSTValue;
                    objTempQuotation.Total = dTotal;
                    objTempQuotation.TotalLabour = dLabourMinutes;
                    objTempQuotation.LabourUnitPrice = model.LabourUnitPrice;
                    objTempQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2); ;
                    objTempQuotation.YourReference = model.YourReference;
                    objTempQuotation.ValidTo = model.ValidTo;
                    objTempQuotation.PaymentTerms = model.PaymentTerms;
                    objTempQuotation.ShipmentMethod = model.ShipmentMethod;
                    objTempQuotation.QuotationSalesPersonId = model.SalesPersonId;
                    objTempQuotation.QuotationSurcharge = model.QuotationSurcharge;
                    objTempQuotation.QuotationMarkup = model.QuotationMarkup;

                    if (model.SalesPersonId != 0)
                    {
                        var empl = db.Employees.Where(y => y.EmployeeID == model.SalesPersonId).FirstOrDefault();
                        if (empl != null)
                        {
                            objTempQuotation.QuotationSalesPersonName = empl.EmployeeName;
                        }
                    }
                    if (model.SendEmailForApproval == true)
                    {
                        objTempQuotation.QuotationStatus = 6;
                    }
                    //else 
                    //{
                    //    obj.SalesPersonName = "";
                    //}                    
                    //obj.Subtotal = model.Subtotal;
                    //obj.GSTValue = model.GSTValue;
                    //obj.Total = model.Total;
                    objTempQuotation.QuotationSurcharge = model.QuotationSurcharge;
                    objTempQuotation.QuotationMarkup = model.QuotationMarkup;
                    objTempQuotation.GSTPer = model.GSTPer;
                    objTempQuotation.QuotationNotes = model.QuotationNotes;
                    objTempQuotation.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    objTempQuotation.ModifiedDate = DateTime.Now;
                    db.Entry(objTempQuotation).State = EntityState.Modified;
                    db.SaveChanges();
                    objQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();
                    if (objQuotation != null)
                    {
                        //objQuotation = objQuotation;                           
                        var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == objQuotation.QuotationId).ToList();
                        objQuotation.objQuotationItems = tempQuotationComponent;
                    }

                    if (model.SendEmailForApproval == true)
                    {
                        List<string> strCCEmailslist = new List<string>();
                        List<string> toCustContact = new List<string>();

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
                        string strMSG = "";
                        //var subject = "" + iDetails.InspectionDocumentNo + "-" + iDetails.Customer + "";
                        var subject = "Racking Inspection Repair Quotation Available for Review and Approval";
                        var toEmail = iDetails.custModel.CustomerEmail;
                        strMSG = "<html>";
                        strMSG += "<head>";
                        strMSG += "<style>";
                        strMSG += "p{margin:0px}";
                        strMSG += "</style>";
                        strMSG += "</head>";
                        strMSG += "<body>";
                        strMSG += "<div style='width: 800px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                        if (iDetails.custModel.CustomerEmail == "")
                        {
                            strMSG += "<p style='color:red;'>Customer email has been missing from system. Please update customer's email. </p>";
                            toEmail = iDetails.custModel.CustomerEmail;
                        }

                        if (iDetails.custModel.CustomerContactName == null)
                        {
                            iDetails.custModel.CustomerContactName = "";
                        }

                        if (iDetails.custModel.CustomerContactName != "")
                        {
                            strMSG += "<p>Dear " + iDetails.custModel.CustomerContactName + " [" + iDetails.Customer + "]</p>";
                        }
                        else
                        {
                            strMSG += "<p>Dear  " + iDetails.Customer + "</p>";
                        }
                        strMSG += "<br/>";
                        strMSG += "<br/>";
                        strMSG += "<p>I hope you’re doing well.</p>";
                        strMSG += "<br/>";
                        strMSG += "<p>I’m pleased to inform you that the quotation for the selected deficiencies is now available for your review and approval on the Rack Auditor portal. You will find the quotation at the end of the racking inspection report.</p>";
                        strMSG += "<br/>";
                        strMSG += "<p>Once you approve the quotation, we will proceed with ordering the necessary materials and scheduling the repairs.</p>";
                        strMSG += "<br/>";
                        strMSG += "<p>If you have any questions or need further assistance, please don’t hesitate to reach out.</p>";
                        strMSG += "<br/>";
                        strMSG += "<br/>";
                        strMSG += "<div><div></div></div><br/><br/><div><div>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                        strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                        strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<p><span><img style='width:2.618in;height:.6458in'";
                        strMSG += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted='' ";
                        strMSG += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                        strMSG += "</div>";
                        strMSG += "</div>";
                        strMSG += "</div>";
                        strMSG += "</body>";
                        strMSG += "</html>";

                        var tEmail = new Thread(() => EmailHelper.SendPdfEmail(toCustContact, subject, null, strMSG, model.InspectionId, strCCEmailslist)); //attachmentFile
                        tEmail.Start();
                    }
                }
                catch (Exception ex)
                {
                    return objQuotation; //ex.Message.ToString();
                }
            }
            return objQuotation;
        }
        internal static Quotation sendQuotationtoCustomerForApproval(AdminQuotation model)
        {
            Quotation objQuotation = new Quotation();
            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(model.InspectionId);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itmInspection = db.Inspections.Where(x => x.InspectionId == iDetails.InspectionId).FirstOrDefault();
                    if (itmInspection != null)
                    {
                        itmInspection.InspectionStatus = 6;
                        itmInspection.ModifiedDate = DateTime.Now;
                        itmInspection.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        db.Entry(itmInspection).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    var itmQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();
                    if (itmQuotation != null)
                    {
                        itmQuotation.QuotationStatus = 6;
                        itmQuotation.ModifiedDate = DateTime.Now;
                        itmQuotation.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        db.Entry(itmQuotation).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    List<string> strCCEmailslist = new List<string>();
                    List<string> toCustContact = new List<string>();

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
                    string strMSG = "";
                    //var subject = "" + iDetails.InspectionDocumentNo + "-" + iDetails.Customer + "";
                    var subject = "Racking Inspection Repair Quotation Available for Review and Approval";
                    var toEmail = iDetails.custModel.CustomerEmail;
                    strMSG = "<html>";
                    strMSG += "<head>";
                    strMSG += "<style>";
                    strMSG += "p{margin:0px}";
                    strMSG += "</style>";
                    strMSG += "</head>";
                    strMSG += "<body>";
                    strMSG += "<div style='width: 800px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                    if (iDetails.custModel.CustomerEmail == "")
                    {
                        strMSG += "<p style='color:red;'>Customer email has been missing from system. Please update customer's email. </p>";
                        toEmail = iDetails.custModel.CustomerEmail;
                    }

                    if (iDetails.custModel.CustomerContactName == null)
                    {
                        iDetails.custModel.CustomerContactName = "";
                    }

                    if (iDetails.custModel.CustomerContactName != "")
                    {
                        strMSG += "<p>Dear " + iDetails.custModel.CustomerContactName + " [" + iDetails.Customer + "]</p>";
                    }
                    else
                    {
                        strMSG += "<p>Dear  " + iDetails.Customer + "</p>";
                    }
                    strMSG += "<br/>";
                    strMSG += "<br/>";
                    strMSG += "<p>I hope you’re doing well.</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>I’m pleased to inform you that the quotation for the selected deficiencies is now available for your review and approval on the Rack Auditor portal. You will find the quotation at the end of the racking inspection report.</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>Once you approve the quotation, we will proceed with ordering the necessary materials and scheduling the repairs.</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>If you have any questions or need further assistance, please don’t hesitate to reach out.</p>";
                    strMSG += "<br/>";
                    strMSG += "<br/>";
                    strMSG += "<div><div></div></div><br/><br/><div><div>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                    strMSG += "<br/>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                    strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSG += "<br/>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                    strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSG += "<p><span><img style='width:2.618in;height:.6458in'";
                    strMSG += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted='' ";
                    strMSG += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</body>";
                    strMSG += "</html>";

                    var tEmail = new Thread(() => EmailHelper.SendPdfEmail(toCustContact, subject, null, strMSG, model.InspectionId, strCCEmailslist)); //attachmentFile
                    tEmail.Start();

                    objQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();
                    if (objQuotation != null)
                    {
                        //objQuotation = objQuotation;                           
                        var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == objQuotation.QuotationId).ToList();
                        objQuotation.objQuotationItems = tempQuotationComponent;
                    }
                }
                catch (Exception)
                {
                    return objQuotation;

                }
            }
            return objQuotation;
        }
        //internal static Quotation Updateitempriceadmin(AdminQuotation model)
        //{
        //    Quotation objQuotation = new Quotation();
        //    string json = JsonConvert.SerializeObject(model, Formatting.Indented);
        //    var iDetails = DatabaseHelper.getInspectionDetailsForSheet(model.InspectionId);
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        try
        //        {
        //            decimal dTotal = 0;
        //            decimal dLabourMinutes = 0;
        //            decimal dHours = 0;
        //            decimal dTotalLabourCharges = 0;
        //            decimal dSubtotal = 0;
        //            decimal dGSTPer = 0;
        //            decimal dGSTValue = 0;

        //            var objTempQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();

        //            var AllQuotationItemsTemp = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();
        //            foreach (var objItem in AllQuotationItemsTemp)
        //            {
        //                if (objItem.ItemPartNo == "")
        //                {
        //                    var resultInspectionDeficiency = from imto in db.InspectionDeficiencyMTOes
        //                                                     join id in db.InspectionDeficiencies
        //                                                     on imto.InspectionDeficiencyId equals id.InspectionDeficiencyId
        //                                                     where id.InspectionId == model.InspectionId
        //                                                     && imto.Size_Description == objItem.ItemDescription
        //                                                     select new
        //                                                     {
        //                                                         imto.InspectionDeficiencyMTOId,
        //                                                         imto.InspectionDeficiencyId,
        //                                                         imto.ComponentId,
        //                                                         imto.ManufacturerId,
        //                                                         imto.Size_Description
        //                                                     };
        //                    if (resultInspectionDeficiency.Any())
        //                    {
        //                        foreach (var itemChild in resultInspectionDeficiency)
        //                        {

        //                            List<PropertyMatch> propertyMatches = new List<PropertyMatch>();
        //                            ComponentPropertiesMatch objComponentPropertiesMatchInput = new ComponentPropertiesMatch();
        //                            ComponentPropertiesMatch objComponentPropertiesMatchOutput = new ComponentPropertiesMatch();
        //                            List<ComponentPropertiesMatchList> objComponentPropertiesMatchList = new List<ComponentPropertiesMatchList>();
        //                            ComponentPropertiesMatchList objComponentPropertyMatch = new ComponentPropertiesMatchList();

        //                            objComponentPropertyMatch.ComponentPropertyType = "ComponentId";
        //                            objComponentPropertyMatch.ComponentPropertyValue = itemChild.ComponentId.ToString();
        //                            objComponentPropertiesMatchList.Add(objComponentPropertyMatch);

        //                            objComponentPropertyMatch = new ComponentPropertiesMatchList();
        //                            objComponentPropertyMatch.ComponentPropertyType = "ManufacturerId";
        //                            objComponentPropertyMatch.ComponentPropertyValue = itemChild.ManufacturerId.ToString();
        //                            objComponentPropertiesMatchList.Add(objComponentPropertyMatch);

        //                            objComponentPropertiesMatchInput.objComponentPropertiesMatchList = objComponentPropertiesMatchList;

        //                            var resultComponentDetails = from mt in db.InspectionDeficiencyMTOes
        //                                                         join imd in db.InspectionDeficiencyMTODetails on mt.InspectionDeficiencyMTOId equals imd.InspectionDeficiencyMTOId
        //                                                         join cpv in db.ComponentPropertyValues on new { imd.ComponentPropertyTypeId, imd.ComponentPropertyValueId }
        //                                                         equals new { cpv.ComponentPropertyTypeId, cpv.ComponentPropertyValueId }
        //                                                         join cpt in db.ComponentPropertyTypes on imd.ComponentPropertyTypeId equals cpt.ComponentPropertyTypeId
        //                                                         where mt.InspectionDeficiencyMTOId == itemChild.InspectionDeficiencyMTOId
        //                                                         select new
        //                                                         {
        //                                                             mt.ManufacturerId,
        //                                                             mt.ComponentId,
        //                                                             imd.InspectionDeficiencyMTOId,
        //                                                             imd.ComponentPropertyTypeId,
        //                                                             imd.ComponentPropertyValueId,
        //                                                             cpv.ComponentPropertyValue1,
        //                                                             cpt.ComponentPropertyTypeName
        //                                                         };

        //                            List<ComponentPriceListViewModel> objInnerComponentPriceListViewModelList = new List<ComponentPriceListViewModel>();
        //                            if (resultComponentDetails.Any())
        //                            {
        //                                StringBuilder strComponentDescBuilder = new StringBuilder();
        //                                foreach (var itemChildInner in resultComponentDetails)
        //                                {
        //                                    PropertyMatch objPropertyMatch = new PropertyMatch();
        //                                    objPropertyMatch.PropertyTypeId = itemChildInner.ComponentPropertyTypeId;
        //                                    objPropertyMatch.PropertyValueId = itemChildInner.ComponentPropertyValueId;
        //                                    propertyMatches.Add(objPropertyMatch);
        //                                }
        //                            }
        //                            long matchingComponentPriceId = GetMatchingComponentPriceId(itemChild.ComponentId, itemChild.ManufacturerId, propertyMatches);
        //                            string matchingItemPartNo = "";
        //                            string matchingItemDescription = "";

        //                            var itemWithHighestMatched = db.ComponentPriceLists.Where(x => x.ComponentPriceId == matchingComponentPriceId).FirstOrDefault();

        //                            if (itemWithHighestMatched != null)
        //                            {
        //                                matchingItemPartNo = itemWithHighestMatched.ItemPartNo;
        //                            }
        //                            else
        //                            {
        //                                matchingItemPartNo = "";
        //                            }
        //                            if (matchingItemPartNo != "")
        //                            {
        //                                matchingItemPartNo = matchingItemPartNo.Trim();
        //                                var itmComponentPriceMatched = db.ComponentPriceLists.Where(x => x.ItemPartNo == matchingItemPartNo).ToList();
        //                                foreach (var itmCalc in itmComponentPriceMatched)
        //                                {
        //                                    var itmQuotationItemNew = db.QuotationItems
        //                                                                  .Where(x => x.QuotationId == model.QuotationId && x.QuotationInspectionItemId == objItem.QuotationInspectionItemId)
        //                                                                  .FirstOrDefault();

        //                                    if (itmQuotationItemNew != null)
        //                                    {
        //                                        itmQuotationItemNew.ItemPartNo = itmCalc.ItemPartNo;
        //                                        itmQuotationItemNew.ItemDescription = itmCalc.ComponentPriceDescription;
        //                                        itmQuotationItemNew.ItemUnitPrice = itmCalc.ComponentPrice ?? 0;
        //                                        itmQuotationItemNew.ItemSurcharge = objTempQuotation.QuotationSurcharge;
        //                                        itmQuotationItemNew.ItemMarkup = objTempQuotation.QuotationMarkup;
        //                                        itmQuotationItemNew.ItemPrice = (itmCalc.ComponentPrice ?? 0) * (objTempQuotation.QuotationSurcharge ?? 0) * (objTempQuotation.QuotationMarkup ?? 0);
        //                                        itmQuotationItemNew.ItemWeight = itmCalc.ComponentWeight;
        //                                        itmQuotationItemNew.ItemWeightTotal = itmCalc.ComponentWeight * itmQuotationItemNew.ItemQuantity;
        //                                        itmQuotationItemNew.LineTotal = itmQuotationItemNew.ItemPrice * itmQuotationItemNew.ItemQuantity;
        //                                        itmQuotationItemNew.ItemLabour = itmCalc.ComponentLabourTime ?? 0;
        //                                        itmQuotationItemNew.ItemLabourTotal = (itmCalc.ComponentLabourTime ?? 0) * itmQuotationItemNew.ItemQuantity;

        //                                        db.Entry(itmQuotationItemNew).State = EntityState.Modified;
        //                                    }
        //                                }

        //                                // Call SaveChanges once after the loop
        //                                db.SaveChanges();

        //                                //if (itmComponentPriceMatched != null)
        //                                //{
        //                                //    foreach (var itmCalc in itmComponentPriceMatched)
        //                                //    {
        //                                //        //using (DatabaseEntities db1 = new DatabaseEntities())
        //                                //        //{
        //                                //        var itmQuotationItemNew = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId && x.QuotationInspectionItemId == objItem.QuotationInspectionItemId).FirstOrDefault();
        //                                //        //Quotation obj = new Quotation();
        //                                //        itmQuotationItemNew.ItemPartNo = itmCalc.ItemPartNo;
        //                                //        itmQuotationItemNew.ItemDescription = itmCalc.ComponentPriceDescription;
        //                                //        itmQuotationItemNew.ItemUnitPrice = itmCalc.ComponentPrice ?? 0;
        //                                //        itmQuotationItemNew.ItemSurcharge = objTempQuotation.QuotationSurcharge;
        //                                //        itmQuotationItemNew.ItemMarkup = objTempQuotation.QuotationMarkup;
        //                                //        itmQuotationItemNew.ItemPrice = (itmCalc.ComponentPrice ?? 0) * (objTempQuotation.QuotationSurcharge ?? 0) * (objTempQuotation.QuotationMarkup ?? 0);
        //                                //        itmQuotationItemNew.ItemWeight = itmCalc.ComponentWeight;
        //                                //        itmQuotationItemNew.ItemWeightTotal = itmCalc.ComponentWeight * itmQuotationItemNew.ItemQuantity;
        //                                //        itmQuotationItemNew.LineTotal = itmQuotationItemNew.ItemPrice * itmQuotationItemNew.ItemQuantity;
        //                                //        itmQuotationItemNew.ItemLabour = itmCalc.ComponentLabourTime ?? 0;
        //                                //        itmQuotationItemNew.ItemLabourTotal = (itmCalc.ComponentLabourTime ?? 0) * itmQuotationItemNew.ItemQuantity;
        //                                //        db.Entry(itmQuotationItemNew).State = EntityState.Modified;

        //                                //        //}
        //                                //    }
        //                                //}
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            var AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();
        //            foreach (var objItem in AllQuotationItems)
        //            {
        //                decimal dItempPrice = 0;
        //                int iQty = 0;
        //                var objQuotationItem = db.QuotationItems.Where(y => y.QuotationInspectionItemId == objItem.QuotationInspectionItemId).FirstOrDefault();
        //                if (objQuotationItem != null)
        //                {
        //                    dItempPrice = objQuotationItem.ItemPrice;
        //                    if (objQuotationItem.ItemPartNo != "")
        //                    {
        //                        var ComponentPriceListItem = db.ComponentPriceLists.Where(x => x.ItemPartNo == objQuotationItem.ItemPartNo).FirstOrDefault();
        //                        if (ComponentPriceListItem != null)
        //                        {
        //                            if (ComponentPriceListItem.ComponentPrice != null)
        //                            {
        //                                dItempPrice = Convert.ToDecimal(ComponentPriceListItem.ComponentPrice);
        //                            }
        //                        }
        //                    }
        //                    iQty = Convert.ToInt16(objQuotationItem.ItemQuantity);
        //                    decimal surcharge = Convert.ToDecimal(model.QuotationSurcharge);
        //                    decimal markup = Convert.ToDecimal(model.QuotationMarkup);
        //                    decimal itemUnitPrice = Convert.ToDecimal(dItempPrice);
        //                    decimal itemPrice = (itemUnitPrice * surcharge * markup);
        //                    if (objQuotationItem.IsTBD == true)
        //                    {
        //                        itemUnitPrice = 0;
        //                        itemPrice = 0;
        //                    }
        //                    itemPrice = Math.Round(itemPrice, 2);

        //                    decimal lineTotal = (itemPrice * iQty);
        //                    lineTotal = Math.Round(lineTotal, 2);

        //                    objQuotationItem.QuotationInspectionItemId = objQuotationItem.QuotationInspectionItemId;
        //                    objQuotationItem.ItemPartNo = objQuotationItem.ItemPartNo;
        //                    objQuotationItem.ItemDescription = objQuotationItem.ItemDescription;
        //                    objQuotationItem.ItemUnitPrice = itemUnitPrice;
        //                    objQuotationItem.ItemSurcharge = surcharge;
        //                    objQuotationItem.ItemMarkup = markup;
        //                    objQuotationItem.ItemPrice = itemPrice;
        //                    objQuotationItem.ItemQuantity = iQty;
        //                    objQuotationItem.ItemWeight = objQuotationItem.ItemWeight;
        //                    objQuotationItem.ItemWeightTotal = objQuotationItem.ItemWeightTotal;
        //                    objQuotationItem.LineTotal = lineTotal;
        //                    objQuotationItem.IsTBD = objQuotationItem.IsTBD;
        //                    objQuotationItem.ItemLabour = objQuotationItem.ItemLabour;
        //                    objQuotationItem.ItemLabourTotal = objQuotationItem.ItemLabourTotal;
        //                    objQuotationItem.ModifiedDate = DateTime.Now;
        //                    objQuotationItem.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
        //                    db.Entry(objQuotationItem).State = EntityState.Modified;
        //                    db.SaveChanges();
        //                }
        //            }

        //            AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();
        //            foreach (var item in AllQuotationItems)
        //            {
        //                dSubtotal += Convert.ToDecimal(item.LineTotal);
        //                dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
        //            }
        //            if (dLabourMinutes < 240)
        //            {
        //                dLabourMinutes = 240;
        //            }
        //            dHours = Math.Round((dLabourMinutes / 60), 2);
        //            dTotalLabourCharges = Convert.ToDecimal(dHours * model.LabourUnitPrice);
        //            dSubtotal = Convert.ToDecimal(dSubtotal + Math.Round(dTotalLabourCharges, 2));
        //            dGSTPer = Convert.ToDecimal(model.GSTPer);
        //            dGSTPer = Convert.ToDecimal(dGSTPer / 100);
        //            decimal gstVal = dSubtotal * dGSTPer;

        //            dGSTValue = Convert.ToDecimal(Math.Round(gstVal, 2));
        //            dTotal = Convert.ToDecimal(dSubtotal + gstVal);

        //            objTempQuotation.Subtotal = dSubtotal;
        //            objTempQuotation.GSTValue = dGSTValue;
        //            objTempQuotation.Total = dTotal;
        //            objTempQuotation.TotalLabour = dLabourMinutes;
        //            objTempQuotation.LabourUnitPrice = model.LabourUnitPrice;
        //            objTempQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2); ;
        //            objTempQuotation.YourReference = model.YourReference;
        //            objTempQuotation.ValidTo = model.ValidTo;
        //            objTempQuotation.PaymentTerms = model.PaymentTerms;
        //            objTempQuotation.ShipmentMethod = model.ShipmentMethod;
        //            objTempQuotation.QuotationSalesPersonId = model.SalesPersonId;
        //            objTempQuotation.QuotationSurcharge = model.QuotationSurcharge;
        //            objTempQuotation.QuotationMarkup = model.QuotationMarkup;

        //            if (model.SalesPersonId != 0)
        //            {
        //                var empl = db.Employees.Where(y => y.EmployeeID == model.SalesPersonId).FirstOrDefault();
        //                if (empl != null)
        //                {
        //                    objTempQuotation.QuotationSalesPersonName = empl.EmployeeName;
        //                }
        //            }
        //            if (model.SendEmailForApproval == true)
        //            {
        //                objTempQuotation.QuotationStatus = 6;
        //            }
        //            objTempQuotation.QuotationSurcharge = model.QuotationSurcharge;
        //            objTempQuotation.QuotationMarkup = model.QuotationMarkup;
        //            objTempQuotation.GSTPer = model.GSTPer;
        //            objTempQuotation.QuotationNotes = model.QuotationNotes;
        //            objTempQuotation.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
        //            objTempQuotation.ModifiedDate = DateTime.Now;
        //            db.Entry(objTempQuotation).State = EntityState.Modified;
        //            db.SaveChanges();
        //            objQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();
        //            if (objQuotation != null)
        //            {
        //                var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == objQuotation.QuotationId).ToList();
        //                objQuotation.objQuotationItems = tempQuotationComponent;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return objQuotation; //ex.Message.ToString();
        //        }
        //    }
        //    return objQuotation;
        //}
        internal static Quotation Updateitempriceadmin(AdminQuotation model)
        {
            decimal surcharge = Convert.ToDecimal(model.QuotationSurcharge);
            decimal markup = Convert.ToDecimal(model.QuotationMarkup);
            Quotation objQuotation = new Quotation();
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var iDetails = DatabaseHelper.getInspectionDetailsForSheet(model.InspectionId);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    List<string> lstDeficiencyIds = new List<string>();
                    var lstSelectedDefi = db.InspectionDeficiencies.Where(x => x.InspectionId == model.InspectionId && x.InspectionDeficiencyRequestQuotation == 1).ToList();
                    if (lstSelectedDefi != null)
                    {
                        foreach (var item in lstSelectedDefi)
                        {
                            lstDeficiencyIds.Add(Convert.ToString(item.InspectionDeficiencyId));
                        }
                    }

                    var resultTemp = from mto in db.InspectionDeficiencyMTOes
                                     join def in db.InspectionDeficiencies on mto.InspectionDeficiencyId equals def.InspectionDeficiencyId
                                     join comp in db.Components on mto.ComponentId equals comp.ComponentId
                                     join manu in db.Manufacturers on mto.ManufacturerId equals manu.ManufacturerId into manufacturers
                                     from m in manufacturers.DefaultIfEmpty()
                                     where def.InspectionId == model.InspectionId
                                           && lstDeficiencyIds.Contains(mto.InspectionDeficiencyId.ToString())
                                           && mto.QuantityReq > 0
                                     select new QuotationItemListPrepare
                                     {
                                         InspectionDeficiencyMTOId = mto.InspectionDeficiencyMTOId,
                                         ComponentId = mto.ComponentId,
                                         ComponentName = comp.ComponentName,
                                         ManufacturerId = mto.ManufacturerId ?? (long?)null,
                                         ManufacturerName = m.ManufacturerName,
                                         Type = mto.Type,
                                         Size_Description = mto.Size_Description != null ? mto.Size_Description.Trim() : string.Empty,
                                         ItemQuantity = mto.QuantityReq,
                                         isFound = false,
                                         ItemPartNo = "",
                                         ItemDescription = "",
                                         ItemUnitPrice = 0,
                                         ItemSurcharge = 0,
                                         ItemMarkup = 0,
                                         ItemPrice = 0,
                                         ItemWeight = 0,
                                         ItemWeightTotal = 0,
                                         LineTotal = 0,
                                         IsTBD = false
                                     };

                    var finalResult = resultTemp.ToList();
                    foreach (var QuotationItemChild in finalResult)
                    {

                        var resultComponentDetailsDeficiencyMTO = (from mtoDetail in db.InspectionDeficiencyMTODetails
                                                                   join propertyValue in db.ComponentPropertyValues
                                                                   on mtoDetail.ComponentPropertyValueId equals propertyValue.ComponentPropertyValueId
                                                                   join propertyType in db.ComponentPropertyTypes
                                                                   on propertyValue.ComponentPropertyTypeId equals propertyType.ComponentPropertyTypeId
                                                                   where mtoDetail.InspectionDeficiencyMTOId == QuotationItemChild.InspectionDeficiencyMTOId
                                                                   select new
                                                                   {
                                                                       propertyType.ComponentPropertyTypeId,
                                                                       propertyType.ComponentPropertyTypeName,
                                                                       propertyValue.ComponentPropertyValueId,
                                                                       propertyValue.ComponentPropertyValue1
                                                                   }).ToList();

                        List<InspectionDeficiencyMTOItemDetail> objItemDetails = new List<InspectionDeficiencyMTOItemDetail>();
                        List<PropertyMatch> propertyMatches = new List<PropertyMatch>();
                        if (resultComponentDetailsDeficiencyMTO.Count > 0)
                        {
                            QuotationItemChild.ItemDetails = new List<InspectionDeficiencyMTOItemDetail>();

                            foreach (var item in resultComponentDetailsDeficiencyMTO)
                            {
                                InspectionDeficiencyMTOItemDetail objInner = new InspectionDeficiencyMTOItemDetail();
                                objInner.ComponentPropertyTypeId = item.ComponentPropertyTypeId;
                                objInner.ComponentPropertyTypeName = item.ComponentPropertyTypeName;
                                objInner.ComponentPropertyValue = item.ComponentPropertyValue1;
                                objInner.ComponentPropertyValueId = item.ComponentPropertyValueId;
                                QuotationItemChild.ItemDetails.Add(objInner);

                                PropertyMatch objPropertyMatch = new PropertyMatch();
                                objPropertyMatch.PropertyTypeId = item.ComponentPropertyTypeId;
                                objPropertyMatch.PropertyValueId = item.ComponentPropertyValueId;
                                objPropertyMatch.PropertyValue = item.ComponentPropertyValue1;
                                propertyMatches.Add(objPropertyMatch);
                            }
                        }

                        string strComponentDesc = "";

                        strComponentDesc = QuotationItemChild.ComponentName + " " + QuotationItemChild.ManufacturerName + " " + QuotationItemChild.Type + " ";
                        strComponentDesc = Convert.ToString(strComponentDesc).Trim();
                        if (QuotationItemChild.ManufacturerId == null)
                        {
                            QuotationItemChild.ManufacturerId = 0;
                        }

                        if (QuotationItemChild.ComponentId == 17)
                        {
                            var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ComponentId == QuotationItemChild.ComponentId).ToList();
                            if (itmComponentPrice != null)
                            {
                                foreach (var itmCalc in itmComponentPrice)
                                {
                                    QuotationItemChild.isFound = true;
                                    QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                    QuotationItemChild.ItemSurcharge = surcharge;
                                    QuotationItemChild.ItemMarkup = markup;
                                    QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * surcharge * markup;
                                    QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                    QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.ItemLabour = itmCalc.ComponentLabourTime;
                                    QuotationItemChild.ItemLabourTotal = QuotationItemChild.ItemQuantity * itmCalc.ComponentLabourTime;
                                }
                            }

                        }
                        else if (QuotationItemChild.ComponentId == 14)
                        {
                            var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ComponentId == QuotationItemChild.ComponentId).ToList();
                            if (itmComponentPrice != null)
                            {
                                foreach (var itmCalc in itmComponentPrice)
                                {
                                    QuotationItemChild.isFound = true;
                                    QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                    QuotationItemChild.ItemSurcharge = surcharge;
                                    QuotationItemChild.ItemMarkup = markup;
                                    QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * surcharge * markup;
                                    QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                    QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.ItemLabour = itmCalc.ComponentLabourTime;
                                    QuotationItemChild.ItemLabourTotal = QuotationItemChild.ItemQuantity * itmCalc.ComponentLabourTime;
                                }
                            }
                        }
                        else if (QuotationItemChild.Size_Description.Contains("Labour only"))
                        {
                            var itmComponentPrice = db.ComponentPriceLists.Where(x => x.ItemPartNo == "Labour(H)").ToList();
                            if (itmComponentPrice != null)
                            {
                                foreach (var itmCalc in itmComponentPrice)
                                {
                                    QuotationItemChild.isFound = true;
                                    QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                    QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                    QuotationItemChild.ItemSurcharge = surcharge;
                                    QuotationItemChild.ItemMarkup = markup;
                                    QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * surcharge * markup;
                                    QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                    QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                    QuotationItemChild.ItemLabour = 0;
                                    QuotationItemChild.ItemLabourTotal = 0;
                                }
                            }
                        }
                        else
                        {



                            long matchingComponentPriceId = GetMatchingComponentPriceId(QuotationItemChild.ComponentId, QuotationItemChild.ManufacturerId, propertyMatches);
                            string matchingItemPartNo = "";
                            var itemWithHighestMatched = db.ComponentPriceLists.Where(x => x.ComponentPriceId == matchingComponentPriceId).FirstOrDefault(); //.OrderByDescending(x => x.iMatched)
                                                                                                                                                             //var itemWithHighestMatched = objMatched.OrderByDescending(x => x.iMatched).FirstOrDefault();
                            if (itemWithHighestMatched != null)
                            {
                                matchingItemPartNo = itemWithHighestMatched.ItemPartNo;
                            }
                            else
                            {
                                matchingItemPartNo = "";
                            }
                            if (matchingItemPartNo != "")
                            {
                                matchingItemPartNo = matchingItemPartNo.Trim();
                                var itmComponentPriceMatched = db.ComponentPriceLists.Where(x => x.ItemPartNo == matchingItemPartNo).ToList();
                                if (itmComponentPriceMatched != null)
                                {
                                    foreach (var itmCalc in itmComponentPriceMatched)
                                    {
                                        QuotationItemChild.isFound = true;
                                        QuotationItemChild.ItemPartNo = itmCalc.ItemPartNo;
                                        //if (matchingItemDescription != "")
                                        //{
                                        //    QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                        //}
                                        //else 
                                        //{
                                        QuotationItemChild.ItemDescription = itmCalc.ComponentPriceDescription;
                                        QuotationItemChild.Size_Description = itmCalc.ComponentPriceDescription;
                                        //}
                                        QuotationItemChild.ItemUnitPrice = itmCalc.ComponentPrice;
                                        QuotationItemChild.ItemSurcharge = surcharge;
                                        QuotationItemChild.ItemMarkup = markup;
                                        QuotationItemChild.ItemPrice = itmCalc.ComponentPrice * surcharge * markup;
                                        QuotationItemChild.ItemWeight = itmCalc.ComponentWeight;
                                        QuotationItemChild.ItemWeightTotal = itmCalc.ComponentWeight * QuotationItemChild.ItemQuantity;
                                        QuotationItemChild.LineTotal = QuotationItemChild.ItemPrice * QuotationItemChild.ItemQuantity;
                                        QuotationItemChild.ItemLabour = itmCalc.ComponentLabourTime;
                                        QuotationItemChild.ItemLabourTotal = itmCalc.ComponentLabourTime * QuotationItemChild.ItemQuantity;
                                    }
                                }
                            }
                            //matchingItemPartNo = "";
                            //matchingItemDescription = "";
                        }
                    }

                    var groupedResult = finalResult
                        .GroupBy(item => new { item.ItemPartNo, item.Size_Description })
                        .Select(group => new QuotationItemListPrepare
                        {
                            ItemPartNo = group.Key.ItemPartNo,
                            Size_Description = group.Key.Size_Description,
                            ItemQuantity = group.Sum(item => item.ItemQuantity),
                            ItemUnitPrice = group.First().ItemUnitPrice,
                            ItemMarkup = group.First().ItemMarkup,
                            ItemPrice = group.First().ItemPrice,
                            ItemSurcharge = group.First().ItemSurcharge,
                            ItemWeight = group.First().ItemWeight,
                            ItemWeightTotal = group.First().ItemWeightTotal,
                            LineTotal = group.Sum(item => item.LineTotal),
                            ManufacturerId = group.First().ManufacturerId,
                            ManufacturerName = group.First().ManufacturerName,
                            Type = group.First().Type,
                            isFound = group.First().isFound,
                            ItemDescription = group.First().ItemDescription,
                            IsTBD = false,
                            ComponentName = group.First().ComponentName,
                            ItemLabour = group.First().ItemLabour,
                            ItemLabourTotal = group.Sum(item => item.ItemLabourTotal)
                        }).ToList();

                    var AllQuotationItemsTemp = db.QuotationItems.Where(y => y.QuotationId == model.QuotationId).ToList();

                    foreach (var QuotationItemChild in groupedResult)
                    {
                        if (!QuotationItemChild.Size_Description.Contains("Customer Attention"))
                        {
                            if (QuotationItemChild.ItemPartNo != "")
                            {
                                bool itemExists = AllQuotationItemsTemp.Any(x => x.ItemPartNo == QuotationItemChild.ItemPartNo);
                                if (!itemExists)
                                {
                                    QuotationItem quoteObjCom = new QuotationItem();
                                    quoteObjCom.InspectionId = model.InspectionId;
                                    quoteObjCom.QuotationId = model.QuotationId;
                                    quoteObjCom.ItemPartNo = QuotationItemChild.ItemPartNo;
                                    quoteObjCom.ItemDescription = QuotationItemChild.Size_Description;
                                    if (QuotationItemChild.ItemUnitPrice.HasValue)
                                    {
                                        quoteObjCom.ItemUnitPrice = QuotationItemChild.ItemUnitPrice.Value;
                                    }
                                    else
                                    {
                                        quoteObjCom.ItemUnitPrice = 0;
                                    }

                                    quoteObjCom.ItemSurcharge = surcharge;
                                    quoteObjCom.ItemMarkup = markup;
                                    if (QuotationItemChild.ItemPrice.HasValue)
                                    {
                                        quoteObjCom.ItemPrice = QuotationItemChild.ItemPrice.Value;
                                    }
                                    else
                                    {
                                        quoteObjCom.ItemPrice = 0;
                                    }

                                    if (QuotationItemChild.ItemQuantity.HasValue)
                                    {
                                        quoteObjCom.ItemQuantity = QuotationItemChild.ItemQuantity.Value;
                                    }
                                    else
                                    {
                                        quoteObjCom.ItemQuantity = 0;
                                    }
                                    quoteObjCom.ItemWeight = QuotationItemChild.ItemWeight;
                                    quoteObjCom.ItemWeightTotal = QuotationItemChild.ItemWeightTotal;

                                    if (QuotationItemChild.LineTotal.HasValue)
                                    {
                                        quoteObjCom.LineTotal = QuotationItemChild.LineTotal.Value;
                                    }
                                    else
                                    {
                                        quoteObjCom.LineTotal = 0;
                                    }
                                    if (QuotationItemChild.ItemLabour.HasValue)
                                    {
                                        quoteObjCom.ItemLabour = QuotationItemChild.ItemLabour.Value;
                                    }
                                    else
                                    {
                                        quoteObjCom.ItemLabour = 0;
                                    }

                                    if (QuotationItemChild.ItemLabourTotal.HasValue)
                                    {
                                        quoteObjCom.ItemLabourTotal = QuotationItemChild.ItemLabourTotal.Value;
                                    }
                                    else
                                    {
                                        quoteObjCom.ItemLabourTotal = 0;
                                    }

                                    quoteObjCom.IsTBD = false;
                                    quoteObjCom.CreatedDate = DateTime.Now;
                                    quoteObjCom.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                    quoteObjCom.ModifiedDate = DateTime.Now;
                                    quoteObjCom.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                    db.QuotationItems.Add(quoteObjCom);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }

                    decimal dTotal = 0;
                    decimal dLabourMinutes = 0;
                    decimal dHours = 0;
                    decimal dTotalLabourCharges = 0;
                    decimal dSubtotal = 0;
                    decimal dGSTPer = 0;
                    decimal dGSTValue = 0;

                    var objTempQuotation = db.Quotations.FirstOrDefault(x => x.QuotationId == model.QuotationId);
                    AllQuotationItemsTemp = db.QuotationItems.Where(y => y.QuotationId == model.QuotationId).ToList();

                    foreach (var objItem in AllQuotationItemsTemp)
                    {
                        if (string.IsNullOrEmpty(objItem.ItemPartNo))
                        {
                            var resultInspectionDeficiency = from imto in db.InspectionDeficiencyMTOes
                                                             join id in db.InspectionDeficiencies on imto.InspectionDeficiencyId equals id.InspectionDeficiencyId
                                                             where id.InspectionId == model.InspectionId && imto.Size_Description == objItem.ItemDescription
                                                             select new
                                                             {
                                                                 imto.InspectionDeficiencyMTOId,
                                                                 imto.InspectionDeficiencyId,
                                                                 imto.ComponentId,
                                                                 imto.ManufacturerId,
                                                                 imto.Size_Description
                                                             };

                            if (resultInspectionDeficiency.Any())
                            {
                                foreach (var itemChild in resultInspectionDeficiency)
                                {
                                    List<PropertyMatch> propertyMatches = new List<PropertyMatch>();
                                    ComponentPropertiesMatch objComponentPropertiesMatchInput = new ComponentPropertiesMatch();
                                    List<ComponentPropertiesMatchList> objComponentPropertiesMatchList = new List<ComponentPropertiesMatchList>();
                                    ComponentPropertiesMatchList objComponentPropertyMatch = new ComponentPropertiesMatchList
                                    {
                                        ComponentPropertyType = "ComponentId",
                                        ComponentPropertyValue = itemChild.ComponentId.ToString()
                                    };
                                    objComponentPropertiesMatchList.Add(objComponentPropertyMatch);

                                    objComponentPropertyMatch = new ComponentPropertiesMatchList
                                    {
                                        ComponentPropertyType = "ManufacturerId",
                                        ComponentPropertyValue = itemChild.ManufacturerId.ToString()
                                    };
                                    objComponentPropertiesMatchList.Add(objComponentPropertyMatch);

                                    objComponentPropertiesMatchInput.objComponentPropertiesMatchList = objComponentPropertiesMatchList;

                                    var resultComponentDetails = from mt in db.InspectionDeficiencyMTOes
                                                                 join imd in db.InspectionDeficiencyMTODetails on mt.InspectionDeficiencyMTOId equals imd.InspectionDeficiencyMTOId
                                                                 join cpv in db.ComponentPropertyValues on new { imd.ComponentPropertyTypeId, imd.ComponentPropertyValueId } equals new { cpv.ComponentPropertyTypeId, cpv.ComponentPropertyValueId }
                                                                 join cpt in db.ComponentPropertyTypes on imd.ComponentPropertyTypeId equals cpt.ComponentPropertyTypeId
                                                                 where mt.InspectionDeficiencyMTOId == itemChild.InspectionDeficiencyMTOId
                                                                 select new
                                                                 {
                                                                     mt.ManufacturerId,
                                                                     mt.ComponentId,
                                                                     imd.InspectionDeficiencyMTOId,
                                                                     imd.ComponentPropertyTypeId,
                                                                     imd.ComponentPropertyValueId,
                                                                     cpv.ComponentPropertyValue1,
                                                                     cpt.ComponentPropertyTypeName
                                                                 };

                                    if (resultComponentDetails.Any())
                                    {
                                        foreach (var itemChildInner in resultComponentDetails)
                                        {
                                            propertyMatches.Add(new PropertyMatch
                                            {
                                                PropertyTypeId = itemChildInner.ComponentPropertyTypeId,
                                                PropertyValueId = itemChildInner.ComponentPropertyValueId,
                                                PropertyValue = itemChildInner.ComponentPropertyValue1,
                                            });
                                        }
                                    }

                                    long matchingComponentPriceId = GetMatchingComponentPriceId(itemChild.ComponentId, itemChild.ManufacturerId, propertyMatches);
                                    string matchingItemPartNo = "";

                                    var itemWithHighestMatched = db.ComponentPriceLists.FirstOrDefault(x => x.ComponentPriceId == matchingComponentPriceId);

                                    matchingItemPartNo = itemWithHighestMatched?.ItemPartNo ?? "";

                                    if (!string.IsNullOrEmpty(matchingItemPartNo))
                                    {
                                        matchingItemPartNo = matchingItemPartNo.Trim();
                                        var itmComponentPriceMatched = db.ComponentPriceLists.Where(x => x.ItemPartNo == matchingItemPartNo).FirstOrDefault();
                                        if (itmComponentPriceMatched != null)
                                        {

                                            var existingItem = db.QuotationItems.FirstOrDefault(x => x.ItemPartNo == itmComponentPriceMatched.ItemPartNo && x.QuotationId == model.QuotationId);
                                            if (existingItem != null)
                                            {

                                                existingItem.ItemQuantity += objItem.ItemQuantity;
                                                existingItem.LineTotal = existingItem.ItemPrice * existingItem.ItemQuantity;
                                                existingItem.ItemWeightTotal = itmComponentPriceMatched.ComponentWeight * existingItem.ItemQuantity;
                                                existingItem.ItemLabourTotal = (itmComponentPriceMatched.ComponentLabourTime ?? 0) * existingItem.ItemQuantity;
                                            }
                                            else
                                            {

                                                objItem.ItemPartNo = itmComponentPriceMatched.ItemPartNo;
                                                objItem.ItemDescription = itmComponentPriceMatched.ComponentPriceDescription;
                                                objItem.ItemUnitPrice = itmComponentPriceMatched.ComponentPrice ?? 0;
                                                objItem.ItemSurcharge = objTempQuotation.QuotationSurcharge;
                                                objItem.ItemMarkup = objTempQuotation.QuotationMarkup;
                                                objItem.ItemPrice = (itmComponentPriceMatched.ComponentPrice ?? 0) * (objTempQuotation.QuotationSurcharge ?? 0) * (objTempQuotation.QuotationMarkup ?? 0);
                                                objItem.ItemWeight = itmComponentPriceMatched.ComponentWeight;
                                                objItem.ItemWeightTotal = itmComponentPriceMatched.ComponentWeight * objItem.ItemQuantity;
                                                objItem.LineTotal = objItem.ItemPrice * objItem.ItemQuantity;
                                                objItem.ItemLabour = itmComponentPriceMatched.ComponentLabourTime ?? 0;
                                                objItem.ItemLabourTotal = (itmComponentPriceMatched.ComponentLabourTime ?? 0) * objItem.ItemQuantity;

                                                db.QuotationItems.Add(objItem);
                                            }

                                            db.Entry(existingItem ?? objItem).State = EntityState.Modified;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    db.SaveChanges();

                    var AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();

                    foreach (var objItem in AllQuotationItems)
                    {
                        decimal dItempPrice = 0;
                        int iQty = 0;

                        var objQuotationItem = db.QuotationItems.FirstOrDefault(y => y.QuotationInspectionItemId == objItem.QuotationInspectionItemId);
                        if (objQuotationItem != null)
                        {
                            dItempPrice = objQuotationItem.ItemUnitPrice;
                            if (!string.IsNullOrEmpty(objQuotationItem.ItemPartNo))
                            {
                                var ComponentPriceListItem = db.ComponentPriceLists.FirstOrDefault(x => x.ItemPartNo == objQuotationItem.ItemPartNo);
                                if (ComponentPriceListItem != null && ComponentPriceListItem.ComponentPrice != null)
                                {
                                    dItempPrice = Convert.ToDecimal(ComponentPriceListItem.ComponentPrice);
                                }
                            }

                            iQty = Convert.ToInt16(objQuotationItem.ItemQuantity);
                            //decimal itemUnitPrice = Convert.ToDecimal(dItempPrice);
                            //decimal itemPrice = (itemUnitPrice * surcharge * markup);

                            decimal itemUnitPrice = dItempPrice;
                            decimal itemPrice = objQuotationItem.ItemPrice;

                            // If no price is set yet, pull from ComponentPriceList
                            if (itemPrice == 0 && itemUnitPrice > 0)
                            {
                                itemPrice = itemUnitPrice * surcharge * markup;
                            }

                            if (objQuotationItem.IsTBD == true)
                            {
                                itemUnitPrice = 0;
                                itemPrice = 0;
                            }
                            itemPrice = Math.Round(itemPrice, 2);

                            decimal lineTotal = Math.Round(itemPrice * iQty, 2);

                            objQuotationItem.ItemUnitPrice = itemUnitPrice;
                            objQuotationItem.ItemSurcharge = surcharge;
                            objQuotationItem.ItemMarkup = markup;
                            objQuotationItem.ItemPrice = itemPrice;
                            objQuotationItem.ItemQuantity = iQty;
                            objQuotationItem.LineTotal = lineTotal;
                            objQuotationItem.ModifiedDate = DateTime.Now;
                            objQuotationItem.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();

                            db.Entry(objQuotationItem).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();
                    foreach (var item in AllQuotationItems)
                    {
                        if (item.IsTBD != true)
                        {
                            dSubtotal += Convert.ToDecimal(item.LineTotal);
                            dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
                        }
                    }

                    dLabourMinutes = Math.Max(dLabourMinutes, 240);
                    dHours = Math.Round(dLabourMinutes / 60, 2);
                    dTotalLabourCharges = dHours * (model.LabourUnitPrice ?? 0);
                    dSubtotal = Math.Round(dSubtotal + dTotalLabourCharges, 2);
                    dGSTPer = (model.GSTPer ?? 0) / 100;
                    dGSTValue = Math.Round(dSubtotal * dGSTPer, 2);
                    dTotal = dSubtotal + dGSTValue;

                    objTempQuotation.Subtotal = dSubtotal;
                    objTempQuotation.GSTValue = dGSTValue;
                    objTempQuotation.Total = dTotal;
                    objTempQuotation.TotalLabour = dLabourMinutes;
                    objTempQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2);

                    if (model.IsUpdateAll == true)
                    {
                        objTempQuotation.LabourUnitPrice = model.LabourUnitPrice;
                        objTempQuotation.YourReference = model.YourReference;
                        objTempQuotation.ValidTo = model.ValidTo;
                        objTempQuotation.PaymentTerms = model.PaymentTerms;
                        objTempQuotation.ShipmentMethod = model.ShipmentMethod;
                        objTempQuotation.QuotationSalesPersonId = model.SalesPersonId;
                        objTempQuotation.QuotationSurcharge = model.QuotationSurcharge;
                        objTempQuotation.QuotationMarkup = model.QuotationMarkup;
                        if (model.SalesPersonId != 0)
                        {
                            var empl = db.Employees.FirstOrDefault(y => y.EmployeeID == model.SalesPersonId);
                            if (empl != null)
                            {
                                objTempQuotation.QuotationSalesPersonName = empl.EmployeeName;
                            }
                        }
                        objTempQuotation.QuotationNotes = model.QuotationNotes;
                    }

                    if (model.SendEmailForApproval == true)
                    {
                        objTempQuotation.QuotationStatus = 6;
                    }
                    objTempQuotation.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    objTempQuotation.ModifiedDate = DateTime.Now;

                    db.Entry(objTempQuotation).State = EntityState.Modified;
                    db.SaveChanges();

                    objQuotation = db.Quotations.FirstOrDefault(x => x.QuotationId == model.QuotationId);
                    if (objQuotation != null)
                    {
                        objQuotation.objQuotationItems = db.QuotationItems.Where(x => x.QuotationId == objQuotation.QuotationId).ToList();
                    }
                }
                catch (Exception ex)
                {
                    return objQuotation;
                }
            }
            return objQuotation;
        }

        internal static string UpdateQuotationGSTAdmin(Quotation model)
        {
            //string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itmQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();
                    //Quotation obj = new Quotation();
                    itmQuotation.GSTPer = model.GSTPer;
                    itmQuotation.Subtotal = model.Subtotal;
                    itmQuotation.GSTValue = model.GSTValue;
                    itmQuotation.Total = model.Total;
                    db.Entry(itmQuotation).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            return "Ok";
        }

        internal static Quotation UpdateLabourByAdmin(Quotation model)
        {
            Quotation objQuotation = new Quotation();

            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {

                    decimal dSubtotal = 0;
                    decimal dGSTPer = 0;
                    decimal dGSTValue = 0;
                    decimal dTotal = 0;
                    decimal dLabourMinutes = 0;
                    decimal dHours = 0;
                    decimal dTotalLabourCharges = 0;

                    var objUpdateQuotation = db.Quotations.Where(y => y.QuotationId == model.QuotationId).FirstOrDefault();

                    var AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == model.QuotationId).ToList();

                    foreach (var item in AllQuotationItems)
                    {
                        dSubtotal += Convert.ToDecimal(item.LineTotal);
                        dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
                    }

                    //dSubtotal = Convert.ToDecimal(objUpdateQuotation.Subtotal);
                    //dLabourMinutes = Convert.ToDecimal(model.TotalLabour);
                    if (dLabourMinutes < 240)
                    {
                        dLabourMinutes = 240;
                    }
                    dHours = Math.Round((dLabourMinutes / 60), 2);
                    dTotalLabourCharges = Convert.ToDecimal(dHours * objUpdateQuotation.LabourUnitPrice);
                    dSubtotal = dSubtotal + Math.Round(dTotalLabourCharges, 2);
                    dGSTPer = Convert.ToDecimal(objUpdateQuotation.GSTPer);
                    dGSTPer = Convert.ToDecimal(dGSTPer / 100);
                    decimal gstVal = dSubtotal * dGSTPer;

                    dGSTValue = Convert.ToDecimal(Math.Round(gstVal, 2));
                    dTotal = Convert.ToDecimal(dSubtotal + gstVal);

                    objUpdateQuotation.Subtotal = dSubtotal;
                    objUpdateQuotation.GSTValue = dGSTValue;
                    objUpdateQuotation.Total = dTotal;
                    objUpdateQuotation.TotalLabour = dLabourMinutes;
                    objUpdateQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2); ;
                    objUpdateQuotation.QuotationNotes = model.QuotationNotes.Trim();
                    db.Entry(objUpdateQuotation).State = EntityState.Modified;
                    db.SaveChanges();

                    objQuotation = db.Quotations.Where(x => x.QuotationId == model.QuotationId).FirstOrDefault();
                    if (objQuotation != null)
                    {
                        //objQuotation = objQuotation;                           
                        var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == objQuotation.QuotationId).ToList();
                        objQuotation.objQuotationItems = tempQuotationComponent;

                        //List<ImpSettingsViewModel> objImpSettingsViewModel = (from Is in db.ImpSettings
                        //                                                      select new ImpSettingsViewModel
                        //                                                      {
                        //                                                          SettingID = Is.SettingID,
                        //                                                          SettingType = Is.SettingType, // Handle null cases
                        //                                                          SettingValue = Is.SettingValue
                        //                                                      }).ToList();
                        //objQuotation.QuotationSurcharge = 0;
                        //objQuotation.QuotationMarkup = 0;
                        //objQuotation.GSTPer = 0;
                        //foreach (var item in objImpSettingsViewModel)
                        //{
                        //    if (!string.IsNullOrEmpty(item.SettingValue))
                        //    {
                        //        decimal settingValue = Convert.ToDecimal(item.SettingValue);
                        //        switch (item.SettingType)
                        //        {
                        //            case "Surcharge":
                        //                objQuotation.QuotationSurcharge = settingValue;
                        //                break;
                        //            case "Markup":
                        //                objQuotation.QuotationMarkup = settingValue;
                        //                break;
                        //            case "GST":
                        //                objQuotation.GSTPer = settingValue;
                        //                break;
                        //        }
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    return objQuotation;
                }
            }
            return objQuotation;
        }

        internal static string ApproveQuotationCustomer(long InspectionID, long QuotationID)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    List<string> strCCEmailslist = new List<string>();
                    UserEmployeeViewModel objUser = new UserEmployeeViewModel();
                    Customer objCustomer = new Customer();

                    var itmInspection = db.Inspections.Where(x => x.InspectionId == InspectionID).FirstOrDefault();
                    objUser = getUserEmployeeById(itmInspection.EmployeeId);
                    objCustomer = getCustomerById(itmInspection.CustomerId);
                    //Quotation obj = new Quotation();
                    itmInspection.InspectionStatus = 7;
                    itmInspection.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    itmInspection.ModifiedDate = DateTime.Now;
                    db.Entry(itmInspection).State = EntityState.Modified;
                    db.SaveChanges();

                    var itmQuotation = db.Quotations.Where(x => x.QuotationId == QuotationID).FirstOrDefault();
                    itmQuotation.QuotationStatus = 7;
                    itmInspection.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    itmInspection.ModifiedDate = DateTime.Now;
                    db.Entry(itmQuotation).State = EntityState.Modified;
                    db.SaveChanges();

                    var inspectionDeficiencies = db.InspectionDeficiencies
                                        .Where(x => x.InspectionId == InspectionID && x.InspectionDeficiencyRequestQuotation == 1)
                                        .ToList();
                    foreach (var inspectionDeficiency in inspectionDeficiencies)
                    {
                        inspectionDeficiency.InspectionDeficiencyApprovedQuotation = 1;
                    }
                    db.SaveChanges();

                    var toEmail = Convert.ToString(objUser.EmployeeEmail);
                    strCCEmailslist.Add("b.trivedi@camindustrial.net");
                    //strCCEmailslist.Add("nirav.m@siliconinfo.com");
                    string strMSG = "";
                    strMSG = "<html>";
                    strMSG += "<head>";
                    strMSG += "<style>";
                    strMSG += "p{margin:0px}";
                    strMSG += "</style>";
                    strMSG += "</head>";
                    strMSG += "<body>";
                    strMSG += "<div style='width: 800px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                    strMSG += "<br/>";
                    strMSG += "<p></p>";
                    strMSG += "<p>Attention " + objUser.EmployeeName + ",</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>I hope you’re well.</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>I wanted to inform you that the customer has approved the quotation for the selected deficiencies. Please proceed with ordering the quoted materials at your earliest convenience. Additionally, kindly notify the sales coordinator about the approved quotation to ensure everything is aligned for the next steps.</p>";
                    strMSG += "<br/>";
                    strMSG += "<div><div></div><br/><div><div>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                    strMSG += "<br/>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                    strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSG += "<br/>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                    strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                    strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                    strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                    strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSG += "<p><span><img style='width:2.618in;height:.6458in'";
                    strMSG += "src='https://rack-manager.com/img/sigimg.png' alt='sig' data-image-whitelisted='' ";
                    strMSG += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</body>";
                    strMSG += "</html>";
                    var tEmail = new Thread(() => EmailHelper.SendEmail(toEmail, objCustomer.CustomerName + " Approved Quotation – Proceed with Material Order", null, strMSG, strCCEmailslist));
                    tEmail.Start();

                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            return "Ok";
        }
        internal static Quotation SaveQuotationDetails(long QuotationId, QuotationItem objQuotationComponent, bool bInline) //List<QuotationItem> objQuotationComponent
        {
            Quotation objQuotation = new Quotation();
            try
            {
                if (objQuotationComponent == null)
                {

                }
                else
                {
                    using (DatabaseEntities db = new DatabaseEntities())
                    {
                        var existingItem = db.QuotationItems
                         .FirstOrDefault(x => x.QuotationInspectionItemId == objQuotationComponent.QuotationInspectionItemId
                                              && x.QuotationId == objQuotationComponent.QuotationId);
                        if (existingItem != null)
                        {
                            Int16 iQty = 0;
                            if (bInline == true)
                            {
                                iQty = Convert.ToInt16(objQuotationComponent.ItemQuantity);
                                decimal surcharge = Convert.ToDecimal(objQuotationComponent.ItemSurcharge);
                                decimal markup = Convert.ToDecimal(objQuotationComponent.ItemMarkup);
                                decimal itemUnitPrice = Convert.ToDecimal(objQuotationComponent.ItemUnitPrice);
                                decimal itemPrice = (itemUnitPrice * surcharge * markup);
                                decimal itemLabourTotal = objQuotationComponent.ItemLabour * iQty;//  objQuotationComponent.ItemLabourTotal;
                                itemPrice = Math.Round(itemPrice, 2); // Rounding to 2 decimal places
                                if (objQuotationComponent.IsTBD == true)
                                {
                                    itemUnitPrice = 0;
                                    itemPrice = 0;
                                    itemLabourTotal = 0;
                                }

                                decimal lineTotal = (itemPrice * iQty);
                                lineTotal = Math.Round(lineTotal, 2); // Rounding to 2 decimal places

                                existingItem.QuotationInspectionItemId = objQuotationComponent.QuotationInspectionItemId;
                                existingItem.ItemPartNo = objQuotationComponent.ItemPartNo;
                                existingItem.ItemDescription = objQuotationComponent.ItemDescription;
                                existingItem.ItemUnitPrice = itemUnitPrice;
                                existingItem.ItemSurcharge = surcharge;
                                existingItem.ItemMarkup = markup;
                                existingItem.ItemPrice = itemPrice;
                                existingItem.ItemQuantity = iQty;
                                existingItem.ItemWeight = objQuotationComponent.ItemWeight;
                                existingItem.ItemWeightTotal = objQuotationComponent.ItemWeightTotal;
                                existingItem.LineTotal = lineTotal;
                                existingItem.IsTBD = objQuotationComponent.IsTBD;
                                existingItem.ItemLabour = objQuotationComponent.ItemLabour;
                                existingItem.ItemLabourTotal = itemLabourTotal; // objQuotationComponent.ItemLabourTotal;
                                existingItem.ModifiedDate = DateTime.Now;
                                existingItem.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                db.Entry(existingItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                iQty = Convert.ToInt16(existingItem.ItemQuantity);
                                iQty += Convert.ToInt16(objQuotationComponent.ItemQuantity);
                                decimal surcharge = Convert.ToDecimal(existingItem.ItemSurcharge);
                                decimal markup = Convert.ToDecimal(existingItem.ItemMarkup);
                                decimal itemUnitPrice = Convert.ToDecimal(existingItem.ItemUnitPrice);
                                decimal itemPrice = (itemUnitPrice * surcharge * markup);
                                itemPrice = Math.Round(itemPrice, 2); // Rounding to 2 decimal places

                                decimal lineTotal = (itemPrice * iQty);
                                lineTotal = Math.Round(lineTotal, 2); // Rounding to 2 decimal places

                                existingItem.QuotationInspectionItemId = objQuotationComponent.QuotationInspectionItemId;
                                existingItem.ItemPartNo = objQuotationComponent.ItemPartNo;
                                existingItem.ItemDescription = objQuotationComponent.ItemDescription;
                                existingItem.ItemUnitPrice = itemUnitPrice;
                                existingItem.ItemSurcharge = surcharge;
                                existingItem.ItemMarkup = markup;
                                existingItem.ItemPrice = itemPrice;
                                existingItem.ItemQuantity = iQty;
                                existingItem.ItemWeight = objQuotationComponent.ItemWeight;
                                existingItem.ItemWeightTotal = objQuotationComponent.ItemWeightTotal;
                                existingItem.LineTotal = lineTotal;
                                existingItem.IsTBD = objQuotationComponent.IsTBD;
                                existingItem.ItemLabour = objQuotationComponent.ItemLabour;
                                existingItem.ItemLabourTotal = objQuotationComponent.ItemLabourTotal;
                                existingItem.ModifiedDate = DateTime.Now;
                                existingItem.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                db.Entry(existingItem).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            var existingItemElse = db.QuotationItems
                        .FirstOrDefault(x => x.ItemPartNo == objQuotationComponent.ItemPartNo
                                             && x.QuotationId == objQuotationComponent.QuotationId);
                            if (existingItemElse != null)
                            {
                                Int16 iQty = 0;
                                iQty = Convert.ToInt16(existingItemElse.ItemQuantity);
                                iQty += Convert.ToInt16(objQuotationComponent.ItemQuantity);
                                decimal surcharge = Convert.ToDecimal(objQuotationComponent.ItemSurcharge);
                                decimal markup = Convert.ToDecimal(objQuotationComponent.ItemMarkup);
                                decimal itemUnitPrice = Convert.ToDecimal(objQuotationComponent.ItemUnitPrice);
                                decimal itemPrice = (itemUnitPrice * surcharge * markup);

                                itemPrice = Math.Round(itemPrice, 2); // Rounding to 2 decimal places
                                decimal itemLabourTotal = objQuotationComponent.ItemLabour * iQty;
                                //decimal itemItemWeight = objQuotationComponent.ItemWeight * iQty;
                                decimal itemItemWeight = (objQuotationComponent.ItemWeight ?? 0) * iQty;

                                if (objQuotationComponent.IsTBD == true)
                                {
                                    itemUnitPrice = 0;
                                    itemPrice = 0;
                                    itemLabourTotal = 0;
                                }

                                decimal lineTotal = (itemPrice * iQty);
                                lineTotal = Math.Round(lineTotal, 2); // Rounding to 2 decimal places

                                //existingItemElse.QuotationInspectionItemId = objQuotationComponent.QuotationInspectionItemId;
                                existingItemElse.ItemPartNo = objQuotationComponent.ItemPartNo;
                                existingItemElse.ItemDescription = objQuotationComponent.ItemDescription;
                                existingItemElse.ItemUnitPrice = itemUnitPrice;
                                existingItemElse.ItemSurcharge = surcharge;
                                existingItemElse.ItemMarkup = markup;
                                existingItemElse.ItemPrice = itemPrice;
                                existingItemElse.ItemQuantity = iQty;
                                existingItemElse.ItemWeight = objQuotationComponent.ItemWeight;
                                existingItemElse.ItemWeightTotal = itemItemWeight;
                                existingItemElse.LineTotal = lineTotal;
                                existingItemElse.IsTBD = objQuotationComponent.IsTBD;
                                existingItemElse.ItemLabour = objQuotationComponent.ItemLabour;
                                existingItemElse.ItemLabourTotal = itemLabourTotal;
                                existingItemElse.ModifiedDate = DateTime.Now;
                                existingItemElse.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                db.Entry(existingItemElse).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                QuotationItem quoteObjCom = new QuotationItem();
                                quoteObjCom.InspectionId = objQuotationComponent.InspectionId;
                                quoteObjCom.QuotationId = objQuotationComponent.QuotationId;
                                quoteObjCom.ItemPartNo = objQuotationComponent.ItemPartNo;
                                quoteObjCom.ItemDescription = objQuotationComponent.ItemDescription;
                                quoteObjCom.ItemUnitPrice = objQuotationComponent.ItemUnitPrice;
                                quoteObjCom.ItemSurcharge = objQuotationComponent.ItemSurcharge;
                                quoteObjCom.ItemMarkup = objQuotationComponent.ItemMarkup;
                                quoteObjCom.ItemPrice = objQuotationComponent.ItemPrice;
                                quoteObjCom.ItemWeightTotal = objQuotationComponent.ItemWeightTotal;
                                quoteObjCom.ItemQuantity = objQuotationComponent.ItemQuantity;
                                quoteObjCom.ItemWeight = objQuotationComponent.ItemWeight;
                                quoteObjCom.LineTotal = objQuotationComponent.LineTotal;
                                quoteObjCom.IsTBD = objQuotationComponent.IsTBD;
                                quoteObjCom.ItemLabour = objQuotationComponent.ItemLabour;
                                quoteObjCom.ItemLabourTotal = objQuotationComponent.ItemLabourTotal;
                                quoteObjCom.CreatedDate = DateTime.Now;
                                quoteObjCom.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                quoteObjCom.ModifiedDate = DateTime.Now;
                                quoteObjCom.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                                db.QuotationItems.Add(quoteObjCom);
                                db.SaveChanges();
                            }
                        }

                        var objUpdateQuotation = db.Quotations.Where(y => y.QuotationId == QuotationId).FirstOrDefault();

                        var AllQuotationItems = db.QuotationItems.Where(x => x.QuotationId == QuotationId).ToList();

                        decimal dSubtotal = 0;
                        decimal dGSTPer = 0;
                        decimal dGSTValue = 0;
                        decimal dTotal = 0;
                        decimal dLabourMinutes = 0;
                        decimal dHours = 0;
                        decimal dTotalLabourCharges = 0;
                        decimal dLabour = 0;
                        dLabour = Convert.ToDecimal(objUpdateQuotation.LabourUnitPrice);
                        foreach (var item in AllQuotationItems)
                        {
                            if (item.IsTBD != true)
                            {
                                dSubtotal += Convert.ToDecimal(item.LineTotal);
                                dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
                            }
                        }
                        if (dLabourMinutes < 240)
                        {
                            dLabourMinutes = 240;
                        }
                        dHours = Math.Round((dLabourMinutes / 60), 2);
                        dTotalLabourCharges = dHours * dLabour;
                        dSubtotal = dSubtotal + Math.Round(dTotalLabourCharges, 2);
                        dGSTPer = Convert.ToDecimal(objUpdateQuotation.GSTPer / 100);
                        decimal gstVal = dSubtotal * dGSTPer;

                        dGSTValue = Convert.ToDecimal(Math.Round(gstVal, 2));
                        dTotal = Convert.ToDecimal(dSubtotal + gstVal);

                        Quotation obj = new Quotation();
                        objUpdateQuotation.TotalLabour = dLabourMinutes;
                        objUpdateQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2);
                        objUpdateQuotation.Subtotal = dSubtotal;
                        objUpdateQuotation.GSTValue = dGSTValue;
                        objUpdateQuotation.Total = dTotal;
                        db.Entry(objUpdateQuotation).State = EntityState.Modified;
                        db.SaveChanges();

                        objQuotation = db.Quotations.Where(x => x.QuotationId == QuotationId).FirstOrDefault();
                        //var AllQuotationItemsReturn = db.QuotationItems.Where(x => x.QuotationId == QuotationId).ToList();
                        //objQuotation.objQuotationItems = AllQuotationItems;
                        if (objQuotation != null)
                        {
                            //objQuotation = objQuotation;                           
                            var tempQuotationComponent = db.QuotationItems.Where(x => x.QuotationId == objQuotation.QuotationId).ToList();
                            objQuotation.objQuotationItems = tempQuotationComponent;

                            List<ImpSettingsViewModel> objImpSettingsViewModel = (from Is in db.ImpSettings
                                                                                  select new ImpSettingsViewModel
                                                                                  {
                                                                                      SettingID = Is.SettingID,
                                                                                      SettingType = Is.SettingType, // Handle null cases
                                                                                      SettingValue = Is.SettingValue
                                                                                  }).ToList();
                            objQuotation.QuotationSurcharge = 0;
                            objQuotation.QuotationMarkup = 0;
                            //objQuotation.GSTPer = 0;
                            foreach (var item in objImpSettingsViewModel)
                            {
                                if (!string.IsNullOrEmpty(item.SettingValue))
                                {
                                    decimal settingValue = Convert.ToDecimal(item.SettingValue);
                                    switch (item.SettingType)
                                    {
                                        case "Surcharge":
                                            objQuotation.QuotationSurcharge = settingValue;
                                            break;
                                        case "Markup":
                                            objQuotation.QuotationMarkup = settingValue;
                                            break;
                                            //case "GST":
                                            //    objQuotation.GSTPer = settingValue;
                                            //    break;
                                            //case "LABOUR":
                                            //    objQuotation.LabourUnitPrice = settingValue;
                                            //    break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objQuotation;
        }

        internal static Quotation RemoveQuotationItemsByAdmin(long QuotationInspectionItemId, long QuotationId)
        {
            Quotation objQuotation = new Quotation();
            List<QuotationItem> tempQuotationComponent = new List<QuotationItem>();
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var entity = db.QuotationItems.Find(QuotationInspectionItemId);
                    if (entity != null)
                    {
                        db.QuotationItems.Remove(entity);
                        db.SaveChanges();

                        Quotation objTempQuotation = new Quotation();
                        objTempQuotation = getQuotationDetails(0, QuotationId);

                        decimal dSubtotal = 0;
                        decimal dGSTPer = 0;
                        decimal dGSTValue = 0;
                        decimal dTotal = 0;
                        decimal dLabourMinutes = 0;
                        decimal dHours = 0;
                        decimal dTotalLabourCharges = 0;
                        decimal dLabour = 0;
                        dLabour = Convert.ToDecimal(objTempQuotation.LabourUnitPrice);
                        foreach (var item in objTempQuotation.objQuotationItems)
                        {
                            if (item.IsTBD != true)
                            {
                                dSubtotal += Convert.ToDecimal(item.LineTotal);
                                dLabourMinutes += Convert.ToDecimal(item.ItemLabourTotal);
                            }
                        }
                        if (dLabourMinutes < 240)
                        {
                            dLabourMinutes = 240;
                        }
                        dHours = Math.Round((dLabourMinutes / 60), 2);
                        dTotalLabourCharges = dHours * dLabour;
                        dSubtotal = dSubtotal + Math.Round(dTotalLabourCharges, 2);
                        dGSTPer = Convert.ToDecimal(objTempQuotation.GSTPer / 100);
                        decimal gstVal = dSubtotal * dGSTPer;

                        dGSTValue = Convert.ToDecimal(Math.Round(gstVal, 2));
                        dTotal = Convert.ToDecimal(dSubtotal + gstVal);

                        Quotation obj = new Quotation();
                        objTempQuotation.TotalLabour = dLabourMinutes;
                        objTempQuotation.TotalUnitPrice = Math.Round(dTotalLabourCharges, 2);
                        objTempQuotation.Subtotal = dSubtotal;
                        objTempQuotation.GSTValue = dGSTValue;
                        objTempQuotation.Total = dTotal;
                        db.Entry(objTempQuotation).State = EntityState.Modified;
                        db.SaveChanges();
                        objTempQuotation = getQuotationDetails(0, QuotationId);
                        if (objTempQuotation != null)
                        {
                            objQuotation = objTempQuotation;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //strResult = "Error";                
            }
            return objQuotation;
        }

        internal static string UpdateInspectionStatusTechnician(long inspectionId, int iStatus)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var st = db.Inspections.Where(x => x.InspectionId == inspectionId).FirstOrDefault();
                    if (st != null)
                    {
                        st.InspectionStatus = iStatus;
                        db.Entry(st).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return "Ok";
            }
        }
        internal static string saveInspectionDeficiencyTechnician(InspectionDeficiencyViewModel model)
        {
            int i = 0;
            string strReturn = "";
            bool isEdit = false;
            List<InspectionDeficiencyPhotoTechnicianViewModel> objInspectionDeficiencyPhoto = new List<InspectionDeficiencyPhotoTechnicianViewModel>();

            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(model, Formatting.Indented);
                    Logger.Info("save Inspection Deficiency By Technician in saveInspectionDeficiencyTechnician for details  " + json + " at " + System.DateTime.Now);

                    InspectionDeficiency itmInspectionDeficiencies = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == model.InspectionDeficiencyId).FirstOrDefault();
                    //db.Customers.Where(x => x.ID == userId).UpdateFromQuery(x => new Customer { IsActive = false });
                    itmInspectionDeficiencies.InspectionDeficiencyTechnicianStatus = model.InspectionDeficiencyTechnicianStatus;
                    itmInspectionDeficiencies.InspectionDeficiencyTechnicianRemarks = model.InspectionDeficiencyTechnicianRemark;
                    itmInspectionDeficiencies.ModifiedBy = model.ModifiedBy;
                    itmInspectionDeficiencies.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    model.InspectionId = itmInspectionDeficiencies.InspectionId;
                    //InspectionDeficiency obj = new InspectionDeficiency();
                    //obj.InspectionDeficiencyStatus = model.InspectionDeficiencyStatus;
                    //obj.ModifiedBy = model.ModifiedBy;
                    //obj.ModifiedDate = DateTime.Now;

                    //db.Entry(obj).State = EntityState.Modified;
                    //db.SaveChanges();

                    strReturn = strReturn + "|" + itmInspectionDeficiencies.InspectionDeficiencyId.ToString();
                    objInspectionDeficiencyPhoto = model.InspectionDeficiencyPhotoTechnicianViewModel;
                    if (objInspectionDeficiencyPhoto != null)
                    {
                        foreach (var AllPhoto in objInspectionDeficiencyPhoto)
                        {
                            string imageName = "";
                            if (!string.IsNullOrEmpty(AllPhoto.base64DeficiencyPhotoImage))
                            {
                                i = i + 1;
                                imageName = model.InspectionId.ToString() + "_" + itmInspectionDeficiencies.InspectionDeficiencyId.ToString() + "_T_" + i.ToString() + ".jpg";
                                String path = HttpContext.Current.Server.MapPath("~/img/deficiency/");
                                string TempOutputPath = HostingEnvironment.MapPath("~/img/deficiencythumb/");

                                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                                Uri url = new Uri(tmpURL);
                                string host = url.GetLeftPart(UriPartial.Authority);
                                string imgPath = Path.Combine(path, imageName);
                                if (AllPhoto.base64DeficiencyPhotoImage.Contains("data:image"))
                                {
                                    AllPhoto.base64DeficiencyPhotoImage = AllPhoto.base64DeficiencyPhotoImage.Substring(AllPhoto.base64DeficiencyPhotoImage.LastIndexOf(',') + 1);
                                }
                                byte[] imageBytes = Convert.FromBase64String(AllPhoto.base64DeficiencyPhotoImage);
                                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                                ms.Write(imageBytes, 0, imageBytes.Length);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                                image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                                byte[] imageBytesThumb = CreateThumbnail(imageName, 100, 100);
                                var outputPath = Path.Combine(host + "/img/deficiencythumb/", imageName);
                                TempOutputPath = TempOutputPath + "\\" + imageName;
                                File.WriteAllBytes(TempOutputPath, imageBytesThumb);


                                InspectionDeficiencyPhoto ObjPhoto = new InspectionDeficiencyPhoto();
                                ObjPhoto.InspectionDeficiencyId = itmInspectionDeficiencies.InspectionDeficiencyId;
                                ObjPhoto.InspectionDeficiencyPhotoPath = imageName.Trim();
                                imageName = "";
                                ObjPhoto.InspectionDeficiencyIsStatus = true;
                                ObjPhoto.CreatedBy = model.ModifiedBy;
                                ObjPhoto.CreatedDate = DateTime.Now;
                                ObjPhoto.ModifiedBy = model.ModifiedBy;
                                ObjPhoto.ModifiedDate = DateTime.Now;
                                db.InspectionDeficiencyPhotoes.Add(ObjPhoto);
                                db.SaveChanges();
                            }
                        }
                    }
                    Logger.Info("save Inspection Deficiency By Technician in saveInspectionDeficiencyTechnician for details  " + strReturn + "|" + itmInspectionDeficiencies.InspectionDeficiencyId.ToString() + " at " + System.DateTime.Now);
                    return strReturn + "|" + itmInspectionDeficiencies.InspectionDeficiencyId.ToString();
                }
                catch (Exception ex)
                {
                    if (strReturn != "")
                    {
                        strReturn = strReturn + ex.Message.ToString();
                    }
                    Logger.Info("save Inspection Deficiency By Technician in saveInspectionDeficiencyTechnician for details error  " + strReturn + " at " + System.DateTime.Now);
                    return strReturn;
                }
            }
        }
        internal static BulkSaveResult saveInspectionDeficiencyTechnicianMobile(List<InspectionDeficiencyViewModel> models)
        {
            var result = new BulkSaveResult
            {
                SuccessCount = 0,
                FailedCount = 0,
                Results = new List<DeficiencySaveResult>()
            };

            Logger.Info($"Started bulk technician save for {models.Count} inspection deficiencies at {DateTime.Now}");

            using (DatabaseEntities db = new DatabaseEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var model in models)
                        {
                            try
                            {
                                var deficiencyResult = SaveSingleTechnicianDeficiency(db, model);
                                result.Results.Add(deficiencyResult);

                                if (deficiencyResult.IsSuccess)
                                    result.SuccessCount++;
                                else
                                    result.FailedCount++;
                            }
                            catch (Exception ex)
                            {
                                result.FailedCount++;
                                result.Results.Add(new DeficiencySaveResult
                                {
                                    IsSuccess = false,
                                    InspectionDeficiencyId = model.InspectionDeficiencyId,
                                    ErrorMessage = ex.Message
                                });
                                Logger.Error($"Error saving technician deficiency ID {model.InspectionDeficiencyId}: {ex.Message}");
                            }
                        }

                        transaction.Commit();
                        Logger.Info($"Bulk technician save completed: {result.SuccessCount} succeeded, {result.FailedCount} failed at {DateTime.Now}");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Logger.Error($"Transaction rolled back in bulk technician save: {ex.Message}", ex);
                        throw;
                    }
                }
            }

            return result;
        }

        private static DeficiencySaveResult SaveSingleTechnicianDeficiency(DatabaseEntities db, InspectionDeficiencyViewModel model)
        {
            var result = new DeficiencySaveResult { IsSuccess = false };

            try
            {
                // Find existing deficiency
                var existingDeficiency = db.InspectionDeficiencies
                    .FirstOrDefault(x => x.InspectionDeficiencyId == model.InspectionDeficiencyId);

                if (existingDeficiency == null)
                {
                    result.ErrorMessage = $"Deficiency ID {model.InspectionDeficiencyId} not found";
                    return result;
                }

                // Log the update
                string json = JsonConvert.SerializeObject(model, Formatting.Indented);
                Logger.Info($"Saving technician deficiency update for ID {model.InspectionDeficiencyId}: {json}");

                // Update technician-specific fields
                existingDeficiency.InspectionDeficiencyTechnicianStatus = model.InspectionDeficiencyTechnicianStatus;
                existingDeficiency.InspectionDeficiencyTechnicianRemarks = model.InspectionDeficiencyTechnicianRemark;
                existingDeficiency.ModifiedBy = model.ModifiedBy;
                existingDeficiency.ModifiedDate = DateTime.Now;

                db.SaveChanges();

                // Set InspectionId for photo processing
                model.InspectionId = existingDeficiency.InspectionId;

                // Handle Technician Photos
                if (model.InspectionDeficiencyPhotoTechnicianViewModel?.Any() == true)
                {
                    SaveTechnicianPhotos(db, existingDeficiency, model);
                }

                result.IsSuccess = true;
                result.InspectionDeficiencyId = existingDeficiency.InspectionDeficiencyId;

                Logger.Info($"Successfully saved technician deficiency ID {result.InspectionDeficiencyId} at {DateTime.Now}");
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                Logger.Error($"Error in SaveSingleTechnicianDeficiency for ID {model.InspectionDeficiencyId}: {ex.Message}", ex);
            }

            return result;
        }

        private static void SaveTechnicianPhotos(DatabaseEntities db, InspectionDeficiency deficiency, InspectionDeficiencyViewModel model)
        {
            int photoIndex = 0;
            string basePath = HttpContext.Current.Server.MapPath("~/img/deficiency/");
            string thumbPath = HttpContext.Current.Server.MapPath("~/img/deficiencythumb/");

            foreach (var photoModel in model.InspectionDeficiencyPhotoTechnicianViewModel)
            {
                if (string.IsNullOrEmpty(photoModel.base64DeficiencyPhotoImage))
                    continue;

                photoIndex++;

                // Technician photos have "_T_" suffix
                string imageName = $"{model.InspectionId}_{deficiency.InspectionDeficiencyId}_T_{photoIndex}.jpg";

                try
                {
                    // Process base64 image
                    string base64Data = photoModel.base64DeficiencyPhotoImage;
                    if (base64Data.Contains("data:image"))
                    {
                        base64Data = base64Data.Substring(base64Data.LastIndexOf(',') + 1);
                    }

                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    // Save original image
                    string fullPath = Path.Combine(basePath, imageName);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        using (var image = System.Drawing.Image.FromStream(ms))
                        {
                            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }

                    // Create and save thumbnail
                    byte[] thumbnailBytes = CreateThumbnail(imageName, 100, 100);
                    string thumbFullPath = Path.Combine(thumbPath, imageName);
                    File.WriteAllBytes(thumbFullPath, thumbnailBytes);

                    // Save photo record with technician status
                    var photoRecord = new InspectionDeficiencyPhoto
                    {
                        InspectionDeficiencyId = deficiency.InspectionDeficiencyId,
                        InspectionDeficiencyPhotoPath = imageName.Trim(),
                        InspectionDeficiencyIsStatus = true, // true indicates technician photo
                        CreatedBy = model.ModifiedBy,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = model.ModifiedBy,
                        ModifiedDate = DateTime.Now
                    };

                    db.InspectionDeficiencyPhotoes.Add(photoRecord);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error saving technician photo {imageName}: {ex.Message}", ex);
                    throw; // Re-throw to handle at upper level
                }
            }
        }
        internal static string saveComponentSaved(List<ComponentSavedViewModel> model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("save Component in saveComponentSaved for details  " + json + " at " + System.DateTime.Now);
            int i = 0;
            string strReturn = "";
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    if (model != null)
                    {
                        foreach (var item in model)
                        {
                            List<ComponentSavedDetailViewModel> objComponentSavedDetail = new List<ComponentSavedDetailViewModel>();
                            var itmSaved = db.ComponentSaveds.Where(x => x.ComponentSavedFullName == item.ComponentSavedFullName).FirstOrDefault();
                            if (itmSaved == null)
                            {
                                ComponentSaved obj = new ComponentSaved();
                                obj.ComponentId = item.ComponentId;
                                obj.ComponentManufacturerId = item.ComponentManufacturerId;

                                //obj.CustomerId = item.CustomerId;
                                //obj.CustomerLocationID = item.CustomerLocationID;

                                obj.ComponentSavedFullName = item.ComponentSavedFullName;
                                obj.Size_Description = item.Size_Description;
                                obj.Size_DescriptionOriginal = item.Size_DescriptionOriginal;
                                obj.Size_DescriptionShort = item.Size_DescriptionShort;
                                obj.Size_DescriptionShortOriginal = item.Size_DescriptionShortOriginal;
                                obj.CreatedBy = item.CreatedBy;
                                obj.CreatedDate = DateTime.Now;
                                obj.ModifiedBy = item.ModifiedBy;
                                obj.ModifiedDate = DateTime.Now;
                                db.ComponentSaveds.Add(obj);
                                db.SaveChanges();
                                objComponentSavedDetail = item.ComponentSavedDetailViewModel;
                                foreach (var AllComponentSavedDetail in objComponentSavedDetail)
                                {
                                    AllComponentSavedDetail.ComponentSavedId = obj.ComponentSavedId;
                                    strReturn = saveComponentSavedDetails(AllComponentSavedDetail);
                                }
                                ComponentSavedCustomer objComponentSavedCustomer = new ComponentSavedCustomer();
                                objComponentSavedCustomer.ComponentSavedId = obj.ComponentSavedId;
                                objComponentSavedCustomer.CustomerId = item.CustomerId;
                                objComponentSavedCustomer.CustomerLocationID = item.CustomerLocationID;
                                objComponentSavedCustomer.CreatedBy = item.CreatedBy;
                                objComponentSavedCustomer.CreatedDate = DateTime.Now;
                                objComponentSavedCustomer.ModifiedBy = item.ModifiedBy;
                                objComponentSavedCustomer.ModifiedDate = DateTime.Now;
                                db.ComponentSavedCustomers.Add(objComponentSavedCustomer);
                                db.SaveChanges();
                                strReturn += "|" + obj.ComponentSavedId.ToString();
                            }
                            else
                            {
                                var itmSavedCustomers = db.ComponentSavedCustomers.Where(x => x.ComponentSavedId == itmSaved.ComponentSavedId && x.CustomerId == item.CustomerId && x.CustomerLocationID == item.CustomerLocationID).FirstOrDefault();
                                if (itmSavedCustomers == null)
                                {
                                    ComponentSavedCustomer objComponentSavedCustomer = new ComponentSavedCustomer();
                                    objComponentSavedCustomer.ComponentSavedId = itmSaved.ComponentSavedId;
                                    objComponentSavedCustomer.CustomerId = item.CustomerId;
                                    objComponentSavedCustomer.CustomerLocationID = item.CustomerLocationID;
                                    objComponentSavedCustomer.CreatedBy = item.CreatedBy;
                                    objComponentSavedCustomer.CreatedDate = DateTime.Now;
                                    objComponentSavedCustomer.ModifiedBy = item.ModifiedBy;
                                    objComponentSavedCustomer.ModifiedDate = DateTime.Now;
                                    db.ComponentSavedCustomers.Add(objComponentSavedCustomer);
                                    db.SaveChanges();
                                }

                                strReturn += "|" + itmSaved.ComponentSavedId.ToString();
                            }
                        }
                    }
                    return strReturn;
                }
                catch (Exception ex)
                {
                    if (strReturn != "")
                    {
                        strReturn = strReturn + ex.Message.ToString();
                    }
                    Logger.Info("error in saveComponentSaved with details  " + ex.Message.ToString() + " at " + System.DateTime.Now);
                    return strReturn;
                }
            }
        }

        internal static List<ComponentSavedViewModel> getComponentSaved(long ComponentId, long ComponentManufacturerId, long CustomerId, long CustomerLocationID)
        {
            List<ComponentSavedViewModel> lstComponentSaved = new List<ComponentSavedViewModel>();

            using (DatabaseEntities db = new DatabaseEntities())
            {
                var lstTempSavedComponent = db.ComponentSaveds
                       .Join(db.ComponentSavedCustomers,
                             ComponentSaved => ComponentSaved.ComponentSavedId,
                             ComponentSavedCustomer => ComponentSavedCustomer.ComponentSavedId,
                             (ComponentSaved, ComponentSavedCustomer) => new { ComponentSaved, ComponentSavedCustomer })
                       .Where(result => result.ComponentSaved.ComponentId == ComponentId && result.ComponentSaved.ComponentManufacturerId == ComponentManufacturerId && result.ComponentSavedCustomer.CustomerId == CustomerId && result.ComponentSavedCustomer.CustomerLocationID == CustomerLocationID);

                foreach (var result in lstTempSavedComponent)
                {
                    ComponentSavedViewModel objComponentSavedViewModel = new ComponentSavedViewModel();
                    objComponentSavedViewModel.ComponentSavedId = result.ComponentSaved.ComponentSavedId;
                    objComponentSavedViewModel.ComponentId = result.ComponentSaved.ComponentId;
                    objComponentSavedViewModel.ComponentManufacturerId = result.ComponentSaved.ComponentManufacturerId;
                    objComponentSavedViewModel.CustomerId = result.ComponentSavedCustomer.CustomerId;
                    objComponentSavedViewModel.CustomerLocationID = result.ComponentSavedCustomer.CustomerLocationID;
                    objComponentSavedViewModel.ComponentSavedFullName = result.ComponentSaved.ComponentSavedFullName;
                    objComponentSavedViewModel.Size_Description = result.ComponentSaved.Size_Description;
                    objComponentSavedViewModel.Size_DescriptionOriginal = result.ComponentSaved.Size_DescriptionOriginal;
                    objComponentSavedViewModel.Size_DescriptionShort = result.ComponentSaved.Size_DescriptionShort;
                    objComponentSavedViewModel.Size_DescriptionShortOriginal = result.ComponentSaved.Size_DescriptionShortOriginal;
                    objComponentSavedViewModel.CreatedDate = result.ComponentSaved.CreatedDate;
                    objComponentSavedViewModel.CreatedBy = result.ComponentSaved.CreatedBy;
                    objComponentSavedViewModel.ModifiedDate = result.ComponentSaved.ModifiedDate;
                    objComponentSavedViewModel.ModifiedBy = result.ComponentSaved.ModifiedBy;
                    var lstComponentSavedDetailViewModel = db.ComponentSavedDetails.Where(x => x.ComponentSavedId == result.ComponentSaved.ComponentSavedId).ToList();
                    if (lstComponentSavedDetailViewModel.Count != 0)
                    {
                        List<ComponentSavedDetailViewModel> ComponentSavedDetailViewModel = new List<ComponentSavedDetailViewModel>();
                        foreach (var itemDetails in lstComponentSavedDetailViewModel)
                        {
                            ComponentSavedDetailViewModel objComponentSavedDetailViewModel = new ComponentSavedDetailViewModel();
                            objComponentSavedDetailViewModel.ComponentSavedDetailId = itemDetails.ComponentSavedDetailId;
                            objComponentSavedDetailViewModel.ComponentSavedId = itemDetails.ComponentSavedId;
                            objComponentSavedDetailViewModel.ComponentPropertyTypeId = itemDetails.ComponentPropertyTypeId;
                            var PropertyType = db.ComponentPropertyTypes.Where(x => x.ComponentPropertyTypeId == itemDetails.ComponentPropertyTypeId & x.IsActive == true).FirstOrDefault();
                            objComponentSavedDetailViewModel.ComponentPropertyType = PropertyType.ComponentPropertyTypeDesctiption;
                            objComponentSavedDetailViewModel.ComponentPropertyValueId = itemDetails.ComponentPropertyValueId;
                            var PropertyValue = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == itemDetails.ComponentPropertyValueId).FirstOrDefault();
                            if (PropertyValue == null)
                            {
                                objComponentSavedDetailViewModel.ComponentPropertyValue = "";
                            }
                            else
                            {
                                objComponentSavedDetailViewModel.ComponentPropertyValue = PropertyValue.ComponentPropertyValue1;
                            }

                            objComponentSavedDetailViewModel.CreatedDate = itemDetails.CreatedDate;
                            objComponentSavedDetailViewModel.CreatedBy = itemDetails.CreatedBy;
                            objComponentSavedDetailViewModel.ModifiedDate = itemDetails.ModifiedDate;
                            objComponentSavedDetailViewModel.ModifiedBy = itemDetails.ModifiedBy;
                            ComponentSavedDetailViewModel.Add(objComponentSavedDetailViewModel);
                        }
                        objComponentSavedViewModel.ComponentSavedDetailViewModel = ComponentSavedDetailViewModel;
                    }
                    lstComponentSaved.Add(objComponentSavedViewModel);
                }
                return lstComponentSaved;

                //var listPropertyType = db.ComponentPropertyTypes.Join(db.ComponentsProperties.Where(x => x.ComponentId == ComponentId), d => d.ComponentPropertyTypeId, f => f.ComponentPropertyTypeId, (d, f) => d).ToList();

                //var lstTempSavedComponent = db.ComponentSaveds.Join(db.ComponentSavedCustomers.Where(x => x.CustomerId == CustomerId && x.CustomerLocationID == CustomerLocationID), d => d.ComponentId == ComponentId && d.ComponentManufacturerId == ComponentManufacturerId, f => f.ComponentSavedId, (d, f) => d).ToList();

                //var lstTempSavedComponent = db.ComponentSaveds.Join(db.ComponentSavedCustomers.Where(d=>d.CustomerId == CustomerId && d.CustomerLocationID==CustomerLocationID ),x => x.ComponentId == ComponentId && x.ComponentManufacturerId == ComponentManufacturerId, ).ToList();
                //&& x.CustomerId == CustomerId && x.CustomerLocationID == CustomerLocationID
                return null;
            }
        }


        internal static List<ComponentSavedViewModel> getComponentSavedMaster(long ComponentId, long ComponentManufacturerId, string ComponentDesc)
        {
            List<ComponentSavedViewModel> lstComponentSaved = new List<ComponentSavedViewModel>();
            if (ComponentDesc != "")
            {
                //ComponentDesc = ComponentDesc.Replace(" ", "%");                
            }
            using (DatabaseEntities db = new DatabaseEntities())
            {

                var lstTempSavedComponent = db.ComponentSaveds.AsQueryable();
                lstTempSavedComponent = lstTempSavedComponent.Where(x => x.ComponentId == ComponentId && x.ComponentManufacturerId == ComponentManufacturerId);
                if (ComponentDesc.Length > 0)
                {
                    if (ComponentDesc != "\"\"")
                    {
                        lstTempSavedComponent = lstTempSavedComponent.Where(x => x.ComponentSavedFullName.Contains(ComponentDesc));
                    }
                }
                var resultTemp = lstTempSavedComponent.ToList();

                if (resultTemp.Count != 0)
                {
                    foreach (var result in resultTemp)
                    {
                        ComponentSavedViewModel objComponentSavedViewModel = new ComponentSavedViewModel();
                        objComponentSavedViewModel.ComponentSavedId = result.ComponentSavedId;
                        objComponentSavedViewModel.ComponentId = result.ComponentId;
                        objComponentSavedViewModel.ComponentManufacturerId = result.ComponentManufacturerId;
                        objComponentSavedViewModel.CustomerId = 0;
                        objComponentSavedViewModel.CustomerLocationID = 0;
                        objComponentSavedViewModel.ComponentSavedFullName = result.ComponentSavedFullName;
                        objComponentSavedViewModel.Size_Description = result.Size_Description;
                        objComponentSavedViewModel.Size_DescriptionOriginal = result.Size_DescriptionOriginal;
                        objComponentSavedViewModel.Size_DescriptionShort = result.Size_DescriptionShort;
                        objComponentSavedViewModel.Size_DescriptionShortOriginal = result.Size_DescriptionShortOriginal;
                        objComponentSavedViewModel.CreatedDate = result.CreatedDate;
                        objComponentSavedViewModel.CreatedBy = result.CreatedBy;
                        objComponentSavedViewModel.ModifiedDate = result.ModifiedDate;
                        objComponentSavedViewModel.ModifiedBy = result.ModifiedBy;
                        var lstComponentSavedDetailViewModel = db.ComponentSavedDetails.Where(x => x.ComponentSavedId == result.ComponentSavedId).ToList();
                        if (lstComponentSavedDetailViewModel.Count != 0)
                        {
                            List<ComponentSavedDetailViewModel> ComponentSavedDetailViewModel = new List<ComponentSavedDetailViewModel>();
                            foreach (var itemDetails in lstComponentSavedDetailViewModel)
                            {
                                ComponentSavedDetailViewModel objComponentSavedDetailViewModel = new ComponentSavedDetailViewModel();
                                objComponentSavedDetailViewModel.ComponentSavedDetailId = itemDetails.ComponentSavedDetailId;
                                objComponentSavedDetailViewModel.ComponentSavedId = itemDetails.ComponentSavedId;
                                objComponentSavedDetailViewModel.ComponentPropertyTypeId = itemDetails.ComponentPropertyTypeId;
                                var PropertyType = db.ComponentPropertyTypes.Where(x => x.ComponentPropertyTypeId == itemDetails.ComponentPropertyTypeId & x.IsActive == true).FirstOrDefault();
                                objComponentSavedDetailViewModel.ComponentPropertyType = PropertyType.ComponentPropertyTypeDesctiption;
                                objComponentSavedDetailViewModel.ComponentPropertyValueId = itemDetails.ComponentPropertyValueId;
                                var PropertyValue = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == itemDetails.ComponentPropertyValueId).FirstOrDefault();
                                if (PropertyValue == null)
                                {
                                    objComponentSavedDetailViewModel.ComponentPropertyValue = "";
                                }
                                else
                                {
                                    objComponentSavedDetailViewModel.ComponentPropertyValue = PropertyValue.ComponentPropertyValue1;
                                }

                                objComponentSavedDetailViewModel.CreatedDate = itemDetails.CreatedDate;
                                objComponentSavedDetailViewModel.CreatedBy = itemDetails.CreatedBy;
                                objComponentSavedDetailViewModel.ModifiedDate = itemDetails.ModifiedDate;
                                objComponentSavedDetailViewModel.ModifiedBy = itemDetails.ModifiedBy;
                                ComponentSavedDetailViewModel.Add(objComponentSavedDetailViewModel);
                            }
                            objComponentSavedViewModel.ComponentSavedDetailViewModel = ComponentSavedDetailViewModel;
                        }
                        lstComponentSaved.Add(objComponentSavedViewModel);
                    }
                    return lstComponentSaved;
                }

                //string strCondition; 
                //var lstTempSavedComponent;
                //if (ComponentDesc != "")
                //{
                //    lstTempSavedComponent = db.ComponentSaveds.Where(x => x.ComponentId == ComponentId && x.ComponentManufacturerId == ComponentManufacturerId).ToList();
                //}                
                //if (lstTempSavedComponent.Count != 0)
                //{



                //}

                //var listPropertyType = db.ComponentPropertyTypes.Join(db.ComponentsProperties.Where(x => x.ComponentId == ComponentId), d => d.ComponentPropertyTypeId, f => f.ComponentPropertyTypeId, (d, f) => d).ToList();

                //var lstTempSavedComponent = db.ComponentSaveds.Join(db.ComponentSavedCustomers.Where(x => x.CustomerId == CustomerId && x.CustomerLocationID == CustomerLocationID), d => d.ComponentId == ComponentId && d.ComponentManufacturerId == ComponentManufacturerId, f => f.ComponentSavedId, (d, f) => d).ToList();

                //var lstTempSavedComponent = db.ComponentSaveds.Join(db.ComponentSavedCustomers.Where(d=>d.CustomerId == CustomerId && d.CustomerLocationID==CustomerLocationID ),x => x.ComponentId == ComponentId && x.ComponentManufacturerId == ComponentManufacturerId, ).ToList();
                //&& x.CustomerId == CustomerId && x.CustomerLocationID == CustomerLocationID
                return null;
            }
        }
        //
        internal static string saveComponentSavedDetails(ComponentSavedDetailViewModel model)
        {
            string strReturn = "";
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    ComponentSavedDetail obj = new ComponentSavedDetail();
                    if (model.ComponentPropertyValueId == 0)
                    {
                        ComponentPropertyValue objNewValue = new ComponentPropertyValue();
                        var itm = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValue1 == model.ComponentPropertyValue && x.ComponentPropertyTypeId == model.ComponentPropertyTypeId).FirstOrDefault();

                        if (itm != null)
                        {
                            model.ComponentPropertyValueId = itm.ComponentPropertyValueId;
                        }
                        else
                        {
                            objNewValue.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                            objNewValue.ComponentPropertyValue1 = model.ComponentPropertyValue;
                            objNewValue.IsActive = true;
                            objNewValue.CreatedBy = model.CreatedBy;
                            objNewValue.CreatedDate = DateTime.Now;
                            objNewValue.ModifiedBy = model.CreatedBy;
                            objNewValue.ModifiedDate = DateTime.Now;
                            db.ComponentPropertyValues.Add(objNewValue);
                            db.SaveChanges();
                            model.ComponentPropertyValueId = objNewValue.ComponentPropertyValueId;
                        }
                    }
                    obj.ComponentSavedId = model.ComponentSavedId;
                    obj.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                    obj.ComponentPropertyValueId = model.ComponentPropertyValueId;
                    obj.CreatedBy = model.CreatedBy;
                    obj.CreatedDate = model.CreatedDate;
                    obj.ModifiedBy = model.CreatedBy;
                    obj.ModifiedDate = model.CreatedDate;
                    db.ComponentSavedDetails.Add(obj);
                    db.SaveChanges();
                    strReturn = strReturn + "|" + obj.ComponentSavedDetailId.ToString();
                    return strReturn;
                }
                catch (Exception ex)
                {
                    if (strReturn == "")
                    {
                        strReturn = ex.Message.ToString();
                    }
                    return strReturn;
                }
            }
        }

        internal static string editInspectionDeficiency(InspectionDeficiency model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == model.InspectionDeficiencyId).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.InspectionDeficiencyId = model.InspectionDeficiencyId;
                        itm.InspectionId = model.InspectionId;
                        itm.CustomerNomenclatureNo = model.CustomerNomenclatureNo;
                        itm.CustomerNomenclatureBayNoID = model.CustomerNomenclatureBayNoID;
                        itm.BayFrameSide = model.BayFrameSide;
                        itm.BeamFrameLevel = model.BeamFrameLevel;
                        itm.ConclusionRecommendationsID = model.ConclusionRecommendationsID;
                        itm.ConclusionRecommendationsTitle = model.ConclusionRecommendationsTitle;
                        itm.DeficiencyID = model.DeficiencyID;
                        itm.DeficiencyType = model.DeficiencyType;
                        itm.DeficiencyInfo = model.DeficiencyInfo;
                        itm.Action_Monitor = model.Action_Monitor;
                        itm.Action_ReferReport = model.Action_ReferReport;
                        itm.Action_Repair = model.Action_Repair;
                        itm.Action_Replace = model.Action_Replace;
                        itm.Severity_IndexNo = model.Severity_IndexNo;
                        itm.InspectionDeficiencyTechnicianStatus = model.InspectionDeficiencyTechnicianStatus;
                        itm.ModifiedBy = model.ModifiedBy;
                        itm.ModifiedDate = DateTime.Now;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionDeficiencyPhoto> getAllInspectionDeficiencyPhoto()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionDeficiencyPhotoes.ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
        }

        internal static string saveInspectionDeficiencyPhoto(InspectionDeficiencyPhoto model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("save in inspection image in saveInspectionDeficiencyPhoto with details  " + json + " at " + System.DateTime.Now);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    InspectionDeficiencyPhoto obj = new InspectionDeficiencyPhoto();
                    obj.InspectionDeficiencyPhotoId = model.InspectionDeficiencyPhotoId;
                    obj.InspectionDeficiencyId = model.InspectionDeficiencyId;
                    obj.InspectionDeficiencyPhotoPath = model.InspectionDeficiencyPhotoPath;
                    obj.InspectionDeficiencyIsStatus = model.InspectionDeficiencyIsStatus;
                    obj.CreatedBy = model.CreatedBy;
                    obj.CreatedDate = DateTime.Now;
                    obj.ModifiedBy = model.CreatedBy;
                    obj.ModifiedDate = DateTime.Now;
                    db.InspectionDeficiencyPhotoes.Add(obj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    Logger.Info("error for inspection image in saveInspectionDeficiencyPhoto with details  " + ex.Message.ToString() + " at " + System.DateTime.Now);
                    return null;
                }
            }
        }

        internal static string editInspectionDeficiencyPhoto(InspectionDeficiencyPhoto model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.InspectionDeficiencyPhotoes.Where(x => x.InspectionDeficiencyPhotoId == model.InspectionDeficiencyPhotoId).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.InspectionDeficiencyPhotoId = model.InspectionDeficiencyPhotoId;
                        itm.InspectionDeficiencyId = model.InspectionDeficiencyId;
                        itm.InspectionDeficiencyPhotoPath = model.InspectionDeficiencyPhotoPath;
                        itm.InspectionDeficiencyIsStatus = model.InspectionDeficiencyIsStatus;
                        itm.ModifiedBy = model.ModifiedBy;
                        itm.ModifiedDate = DateTime.Now;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionDeficiencyMTO> getAllInspectionDeficiencyMTO()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionDeficiencyMTO> listObj = new List<InspectionDeficiencyMTO>();
                var list = db.InspectionDeficiencyMTOes.ToList();
                if (list.Count != 0)
                {
                    foreach (var d in list)
                    {
                        InspectionDeficiencyMTO obj = new InspectionDeficiencyMTO();
                        obj.InspectionDeficiencyMTOId = d.InspectionDeficiencyMTOId;
                        if (d.InspectionDeficiencyId != 0)
                        {
                            var insDefId = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == d.InspectionDeficiencyId).FirstOrDefault();
                            if (insDefId != null) { obj.InspectionDeficiencyId = d.InspectionDeficiencyId; }
                        }
                        if (d.ComponentId != 0)
                        {
                            var cId = db.Components.Where(x => x.ComponentId == d.ComponentId).FirstOrDefault();
                            if (cId != null) { obj.ComponentId = d.ComponentId; }
                        }
                        if (d.ManufacturerId != 0)
                        {
                            var mId = db.Manufacturers.Where(x => x.ManufacturerId == d.ManufacturerId).FirstOrDefault();
                            if (mId != null) { obj.ManufacturerId = d.ManufacturerId; }
                        }
                        obj.VendorID = d.VendorID;
                        obj.CAMID = d.CAMID;
                        obj.Type = d.Type;
                        obj.Size_Description = d.Size_Description;
                        obj.Size_DescriptionShort = d.Size_DescriptionShort;
                        obj.QuantityReq = d.QuantityReq;
                        obj.IsActive = true;
                        listObj.Add(obj);
                    }
                    return listObj;
                }
                return null;
            }
        }

        internal static List<InspectionDeficiencyMTOViewModel> getAllInspectionDeficiencyMTOByID(long InspectionDeficiencyId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var lstInspectionDeficiencyMTO = db.InspectionDeficiencyMTOes.Where(x => x.InspectionDeficiencyId == InspectionDeficiencyId).ToList();
                List<InspectionDeficiencyMTOViewModel> listObj = new List<InspectionDeficiencyMTOViewModel>();
                //var list = db.InspectionDeficiencyMTOes.ToList();
                if (lstInspectionDeficiencyMTO.Count != 0)
                {
                    List<InspectionDeficiencyMTODetailViewModel> lstInspectionDeficiencyMTODetail = new List<InspectionDeficiencyMTODetailViewModel>();
                    foreach (var d in lstInspectionDeficiencyMTO)
                    {
                        InspectionDeficiencyMTOViewModel obj = new InspectionDeficiencyMTOViewModel();

                        obj.InspectionDeficiencyMTOId = d.InspectionDeficiencyMTOId;
                        if (d.InspectionDeficiencyId != 0)
                        {
                            var insDefId = db.InspectionDeficiencies.Where(x => x.InspectionDeficiencyId == d.InspectionDeficiencyId).FirstOrDefault();
                            if (insDefId != null) { obj.InspectionDeficiencyId = d.InspectionDeficiencyId; }
                        }
                        if (d.ComponentId != 0)
                        {
                            var vComponent = db.Components.Where(x => x.ComponentId == d.ComponentId).FirstOrDefault();
                            if (vComponent != null)
                            {
                                obj.ComponentId = d.ComponentId;
                                obj.ComponentName = vComponent.ComponentName;
                                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                                Uri url = new Uri(tmpURL);
                                string host = url.GetLeftPart(UriPartial.Authority);
                                if (vComponent.ComponentImage != "")
                                {
                                    obj.ComponentImage = host + "/img/component/" + vComponent.ComponentImage;
                                }
                                else
                                {
                                    obj.ComponentImage = "";
                                }
                            }
                        }
                        if (d.ManufacturerId != 0)
                        {
                            var mId = db.Manufacturers.Where(x => x.ManufacturerId == d.ManufacturerId).FirstOrDefault();
                            if (mId != null)
                            {
                                obj.ManufacturerId = d.ManufacturerId;
                                obj.ManufacturerName = mId.ManufacturerName;
                            }
                        }
                        obj.VendorID = d.VendorID;
                        obj.CAMID = d.CAMID;
                        obj.Type = d.Type;
                        obj.Size_Description = d.Size_Description;
                        obj.Size_DescriptionShort = d.Size_DescriptionShort;
                        obj.QuantityReq = d.QuantityReq;
                        obj.IsActive = true;
                        var lstInspectionDeficiencyMTODetails = db.InspectionDeficiencyMTODetails.Where(x => x.InspectionDeficiencyMTOId == obj.InspectionDeficiencyMTOId).ToList();
                        if (lstInspectionDeficiencyMTODetails.Count != 0)
                        {
                            foreach (var subDetails in lstInspectionDeficiencyMTODetails)
                            {
                                InspectionDeficiencyMTODetailViewModel objInspectionDeficiencyMTODetail = new InspectionDeficiencyMTODetailViewModel();
                                objInspectionDeficiencyMTODetail.InspectionDeficiencyMTODetailId = subDetails.InspectionDeficiencyMTODetailId;
                                objInspectionDeficiencyMTODetail.InspectionDeficiencyMTOId = subDetails.InspectionDeficiencyMTOId;
                                objInspectionDeficiencyMTODetail.ComponentPropertyTypeId = subDetails.ComponentPropertyTypeId;
                                if (subDetails.ComponentPropertyTypeId != 0)
                                {
                                    var mId = db.ComponentPropertyTypes.Where(x => x.ComponentPropertyTypeId == subDetails.ComponentPropertyTypeId & x.IsActive == true).FirstOrDefault();
                                    if (mId != null)
                                    {
                                        objInspectionDeficiencyMTODetail.ComponentPropertyTypeName = mId.ComponentPropertyTypeName;
                                        objInspectionDeficiencyMTODetail.ComponentPropertyTypeDesctiption = mId.ComponentPropertyTypeDesctiption;
                                    }
                                }
                                else
                                {
                                    objInspectionDeficiencyMTODetail.ComponentPropertyTypeName = "";
                                    objInspectionDeficiencyMTODetail.ComponentPropertyTypeDesctiption = "";
                                }
                                objInspectionDeficiencyMTODetail.ComponentPropertyValueId = subDetails.ComponentPropertyValueId;
                                if (subDetails.ComponentPropertyValueId != 0)
                                {
                                    var mId = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == subDetails.ComponentPropertyValueId).FirstOrDefault();
                                    if (mId != null)
                                    {
                                        objInspectionDeficiencyMTODetail.ComponentPropertyValue = mId.ComponentPropertyValue1;
                                    }
                                }
                                else
                                {
                                    objInspectionDeficiencyMTODetail.ComponentPropertyValue = "";
                                }
                                objInspectionDeficiencyMTODetail.CreatedDate = subDetails.CreatedDate;
                                objInspectionDeficiencyMTODetail.CreatedBy = subDetails.CreatedBy;
                                objInspectionDeficiencyMTODetail.ModifiedDate = subDetails.ModifiedDate;
                                objInspectionDeficiencyMTODetail.ModifiedBy = subDetails.ModifiedBy;
                                lstInspectionDeficiencyMTODetail.Add(objInspectionDeficiencyMTODetail);
                            }
                            obj.iMTOModelDetails = lstInspectionDeficiencyMTODetail;
                        }
                        listObj.Add(obj);
                    }
                    return listObj;
                }
                return null;
            }
        }

        internal static string saveInspectionDeficiencyMTO(InspectionDeficiencyMTOViewModel model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("save Component in saveInspectionDeficiencyMTO with details  " + json + " at " + System.DateTime.Now);
            string strReturn = "";
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {

                    InspectionDeficiencyMTO obj = new InspectionDeficiencyMTO();
                    obj.InspectionDeficiencyMTOId = model.InspectionDeficiencyMTOId;
                    obj.InspectionDeficiencyId = model.InspectionDeficiencyId;
                    obj.ComponentId = model.ComponentId;
                    obj.ManufacturerId = model.ManufacturerId;
                    obj.VendorID = model.VendorID;
                    obj.CAMID = model.CAMID;
                    obj.Type = model.Type;
                    obj.Size_Description = model.Size_Description;
                    obj.Size_DescriptionShort = model.Size_DescriptionShort;
                    obj.QuantityReq = model.QuantityReq;
                    obj.IsActive = true;
                    obj.CreatedBy = model.CreatedBy;
                    obj.CreatedDate = model.CreatedDate;
                    obj.ModifiedBy = model.CreatedBy;
                    obj.ModifiedDate = model.CreatedDate;
                    db.InspectionDeficiencyMTOes.Add(obj);
                    db.SaveChanges();
                    foreach (var item in model.iMTOModelDetails)
                    {
                        item.InspectionDeficiencyMTOId = obj.InspectionDeficiencyMTOId;
                        strReturn = saveInspectionDeficiencyMTODetail(item);
                    }
                    strReturn = strReturn + "|" + obj.InspectionDeficiencyMTOId.ToString();
                    return strReturn;
                }
                catch (Exception ex)
                {
                    if (strReturn == "")
                    {
                        strReturn = ex.Message.ToString();
                        Logger.Info("error in saveInspectionDeficiencyMTO for details  " + ex.Message.ToString() + " at " + System.DateTime.Now);
                    }
                    return strReturn;
                }
            }
        }

        internal static string editInspectionDeficiencyMTO(InspectionDeficiencyMTO model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.InspectionDeficiencyMTOes.Where(x => x.InspectionDeficiencyMTOId == model.InspectionDeficiencyMTOId).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.InspectionDeficiencyMTOId = model.InspectionDeficiencyMTOId;
                        itm.InspectionDeficiencyId = model.InspectionDeficiencyId;
                        itm.ComponentId = model.ComponentId;
                        itm.ManufacturerId = model.ManufacturerId;
                        itm.VendorID = model.VendorID;
                        itm.CAMID = model.CAMID;
                        itm.Type = model.Type;
                        itm.Size_Description = model.Size_Description;
                        itm.Size_DescriptionShort = model.Size_DescriptionShort;
                        itm.QuantityReq = model.QuantityReq;
                        itm.ModifiedBy = model.ModifiedBy;
                        itm.ModifiedDate = model.ModifiedDate;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static List<InspectionDeficiencyMTODetail> getAllInspectionDeficiencyMTODetail()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionDeficiencyMTODetails.ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
        }

        internal static string saveInspectionDeficiencyMTODetail(InspectionDeficiencyMTODetailViewModel model)
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            Logger.Info("save Inspection Deficiency MTO Detail  in saveInspectionDeficiencyMTODetail for details  " + json + " at " + System.DateTime.Now);
            string strReturn = "";
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    //var r = (from max in db.InspectionDeficiencyMTODetails.OrderByDescending(i => i.InspectionDeficiencyMTODetailId) select new {max}).First();
                    //Int64 iInspectionDeficiencyMTODetailId = db.InspectionDeficiencyMTODetails.Max(p => p.InspectionDeficiencyMTODetailId);
                    //if (iInspectionDeficiencyMTODetailId == 0)
                    //{
                    //    iInspectionDeficiencyMTODetailId = 1;
                    //}
                    //else {
                    //    iInspectionDeficiencyMTODetailId = iInspectionDeficiencyMTODetailId + 1;
                    //}
                    InspectionDeficiencyMTODetail obj = new InspectionDeficiencyMTODetail();
                    //obj.InspectionDeficiencyMTODetailId = model.InspectionDeficiencyMTODetailId;  //Convert.ToInt64(iInspectionDeficiencyMTODetailId);
                    if (model.ComponentPropertyValueId == 0)
                    {
                        ComponentPropertyValue objNewValue = new ComponentPropertyValue();
                        var itm = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValue1 == model.ComponentPropertyValue && x.ComponentPropertyTypeId == model.ComponentPropertyTypeId).FirstOrDefault();

                        if (itm != null)
                        {
                            model.ComponentPropertyValueId = itm.ComponentPropertyValueId;
                        }
                        else
                        {
                            objNewValue.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                            objNewValue.ComponentPropertyValue1 = model.ComponentPropertyValue;
                            objNewValue.IsActive = true;
                            objNewValue.CreatedBy = model.CreatedBy;
                            objNewValue.CreatedDate = DateTime.Now;
                            objNewValue.ModifiedBy = model.CreatedBy;
                            objNewValue.ModifiedDate = DateTime.Now;
                            db.ComponentPropertyValues.Add(objNewValue);
                            db.SaveChanges();
                            model.ComponentPropertyValueId = objNewValue.ComponentPropertyValueId;
                        }

                    }
                    obj.InspectionDeficiencyMTOId = model.InspectionDeficiencyMTOId;
                    obj.ComponentPropertyTypeId = model.ComponentPropertyTypeId;
                    obj.ComponentPropertyValueId = model.ComponentPropertyValueId;
                    obj.CreatedBy = model.CreatedBy;
                    obj.CreatedDate = DateTime.Now;
                    obj.ModifiedBy = model.CreatedBy;
                    obj.ModifiedDate = DateTime.Now;
                    db.InspectionDeficiencyMTODetails.Add(obj);
                    db.SaveChanges();
                    strReturn = "Success " + obj.InspectionDeficiencyMTOId.ToString();
                    return strReturn;
                }
                catch (Exception ex)
                {
                    if (strReturn == "")
                    {
                        strReturn = ex.Message.ToString();
                        Logger.Info("Error in saving Inspection Deficiency MTO Detail  in saveInspectionDeficiencyMTODetail with details  " + ex.Message.ToString() + " at " + System.DateTime.Now);
                    }
                    return strReturn;
                }
            }
        }

        internal static List<InspectionDueViewModel> getAllInspectionDue()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<InspectionDueViewModel> listObj = new List<InspectionDueViewModel>();
                var list = db.InspectionDues.ToList();
                if (list.Count != 0)
                {
                    foreach (var itm in list)
                    {
                        InspectionDueViewModel obj = new InspectionDueViewModel();
                        obj.InspectionDueId = itm.InspectionDueId;
                        obj.ScheduledDate = itm.ScheduledDate;
                        obj.AssignedEmployeeID = itm.AssignedEmployeeId ?? 0;
                        if (itm.CustomerId != 0)
                        {
                            var cust = getCustomerById(itm.CustomerId);
                            if (cust != null) { obj.Customer = cust.CustomerName; }
                        }
                        if (itm.AssignedEmployeeId != 0)
                        {
                            var emp = db.Employees.Where(x => x.EmployeeID == itm.AssignedEmployeeId).FirstOrDefault();
                            if (emp != null) { obj.Employee = emp.EmployeeName; }
                        }
                        if (itm.CustomerLocationId != 0)
                        {
                            var loc = getCustomerLocationById(Convert.ToInt16(itm.CustomerLocationId));
                            if (loc != null) { obj.CustomerLocation = loc.LocationName; }
                        }
                        if (itm.CustomerAreaId != 0)
                        {
                            var area = getAreaDetailsById(Convert.ToInt16(itm.CustomerAreaId));
                            if (area != null) { obj.CustomerArea = area.AreaName; }
                        }
                        obj.CreatedDate = itm.CreatedDate;
                        obj.CreatedBy = itm.CreatedBy;
                        listObj.Add(obj);
                    }
                    return listObj;
                }
                return null;
            }
        }

        internal static InspectionDue getInspectionDueById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {

                var list = db.InspectionDues.Where(x => x.InspectionDueId == id).FirstOrDefault();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
        }

        //Employee Dashboard - Inspection Due
        internal static List<InspectionViewModel> getInspectionDueByEmployeeId()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                long userID = 0;
                if (HttpContext.Current.Session["LoggedInUserId"] != null)
                {
                    userID = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                }
                var employee = db.Employees.Where(x => x.UserID == userID).FirstOrDefault();
                if (employee != null)
                {
                    var list = getAllInspection();
                    var dueList = list.Where(x => x.EmployeeId == employee.EmployeeID && x.InspectionStatus == 1).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                    //var list = getAllInspectionDue();
                    //var nlist = list.Where(x => x.AssignedEmployeeID == employee.EmployeeID).OrderByDescending(x => x.CreatedDate).Take(10).ToList();
                    if (dueList != null)
                    {
                        return dueList;
                    }
                }

                return null;
            }
        }
        internal static Int32 getDashbaordCountMobileEmployeeId(Int32 id, string sType)
        {
            Int32 iCnt = 0;
            using (DatabaseEntities db = new DatabaseEntities())
            {
                long userID = 0;
                userID = Convert.ToInt64(id);
                var employee = db.Employees.Where(x => x.UserID == userID).FirstOrDefault();
                if (employee != null)
                {
                    if (sType == "Due")
                    {
                        iCnt = db.InspectionDues.Where(x => x.AssignedEmployeeId == employee.EmployeeID).Count();
                    }
                    if (sType == "InProgress")
                    {
                        iCnt = db.Inspections.Where(x => x.EmployeeId == employee.EmployeeID && x.InspectionStatus == 2).Count();
                    }
                    if (sType == "Complete")
                    {
                        iCnt = db.Inspections.Where(x => x.EmployeeId == employee.EmployeeID && x.InspectionStatus == 3).Count();
                    }
                    return iCnt;
                }

                return iCnt;
            }
        }
        internal static InspectionDueViewModel getInspectionDueDetailsById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionDues.Where(x => x.InspectionDueId == id).FirstOrDefault();
                if (list != null)
                {
                    InspectionDueViewModel obj = new InspectionDueViewModel();
                    obj.ScheduledDate = list.ScheduledDate;
                    if (list.CustomerId != 0)
                    {
                        var cust = getCustomerById(list.CustomerId);
                        if (cust != null) { obj.Customer = cust.CustomerName; }
                    }
                    if (list.AssignedEmployeeId != 0)
                    {
                        var emp = getUserEmployeeById(Convert.ToInt16(list.AssignedEmployeeId));
                        if (emp != null) { obj.Employee = emp.EmployeeName; }
                    }
                    if (list.CustomerLocationId != 0)
                    {
                        var loc = getCustomerLocationById(Convert.ToInt16(list.CustomerLocationId));
                        if (loc != null) { obj.CustomerLocation = loc.LocationName; }
                    }
                    if (list.CustomerAreaId != 0)
                    {
                        var area = getAreaDetailsById(Convert.ToInt16(list.CustomerAreaId));
                        if (area != null) { obj.CustomerArea = area.AreaName; }
                    }
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = list.CreatedBy;
                    return obj;
                }
                return null;
            }
        }

        internal async static Task<string> saveInspectionDue(Inspection model)
        {
            FirebaseHelper firebaseService = new FirebaseHelper();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    List<string> strCCEmailslist = new List<string>();
                    if (model.InspectionType == null)
                    {
                        return "Please select Inspection Type.";
                    }
                    if (model.CustomerId == 0)
                    {
                        return "Please select Customer.";
                    }
                    if (model.CustomerLocationId == 0)
                    {
                        return "Please select Customer's Location.";
                    }
                    if (model.EmployeeId == 0)
                    {
                        return "Please select Inspector(Employee).";
                    }
                    if (model.InspectionDate == null)
                    {
                        return "Please set Inspection Date.";
                    }
                    Inspection iObj = new Inspection();
                    Notification iObjNotification = new Notification();
                    iObj.InspectionDate = model.InspectionDate;
                    iObj.CustomerId = model.CustomerId;
                    iObj.CustomerLocationId = model.CustomerLocationId;
                    iObj.CustomerAreaID = model.CustomerAreaID;
                    iObj.EmployeeId = model.EmployeeId;
                    iObj.InspectionStatus = 1; //Due
                    iObj.InspectionType = model.InspectionType.Trim();
                    iObj.CADDocuments = model.CADDocuments;
                    iObj.FacilitiesAreasIds = RemoveDuplicates(model.FacilitiesAreasIds);
                    iObj.ProcessOverviewIds = RemoveDuplicates(model.ProcessOverviewIds);
                    iObj.ReferenceDocumentIds = model.ReferenceDocumentIds;
                    if (model.CustomerId != 0 && model.CustomerLocationId != 0 && model.InspectionType != null)
                    {
                        iObj.InspectionDocumentNo = GenerateInspectionDocumentNo(Convert.ToInt32(model.CustomerId), Convert.ToInt32(model.CustomerLocationId), model.InspectionType);
                    }
                    iObj.IsActive = true;
                    iObj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    iObj.CreatedDate = DateTime.Now;
                    iObj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    iObj.ModifiedDate = DateTime.Now;
                    db.Inspections.Add(iObj);
                    db.SaveChanges();

                    //if (model.inspectionFileDrawing.Count > 0)
                    //{
                    //    foreach (var item in model.inspectionFileDrawing)
                    //    {
                    //        InspectionFileDrawing ObjFileDrawing = new InspectionFileDrawing();
                    //        ObjFileDrawing.InspectionId = iObj.InspectionId;
                    //        ObjFileDrawing.FileDrawingNamePath = item;
                    //        ObjFileDrawing.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    //        ObjFileDrawing.CreatedDate = DateTime.Now;
                    //        ObjFileDrawing.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    //        ObjFileDrawing.ModifiedDate = DateTime.Now;
                    //        db.InspectionFileDrawings.Add(ObjFileDrawing);
                    //        db.SaveChanges();
                    //    }                                                
                    //}

                    string strMessage = "";
                    Guid notificationID = Guid.NewGuid();
                    Customer objCustomer = new Customer();
                    CustomerLocation objCustomerLocation = new CustomerLocation();
                    UserEmployeeViewModel objUser = new UserEmployeeViewModel();
                    objCustomer = getCustomerById(model.CustomerId);
                    objCustomerLocation = getCustomerLocationById(model.CustomerLocationId);
                    objUser = getUserEmployeeById(model.EmployeeId);

                    iObjNotification.NotificationID = notificationID;
                    strMessage = "You have been assigned to new Inspection for " + objCustomer.CustomerName + " at " + objCustomerLocation.LocationName + " on " + model.InspectionDate.ToString("MMMM dd, yyyy") + " with Document No:" + iObj.InspectionDocumentNo + ".";
                    iObjNotification.NotificationText = strMessage;
                    iObjNotification.SenderPlatform = "Admin";
                    iObjNotification.ReceiverPlatform = "Mobile";
                    iObjNotification.Userid_SenderID = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"].ToString());
                    iObjNotification.Userid_ReceiverID = objUser.UserID;

                    strCCEmailslist.Add(objUser.EmployeeEmail);
                    strCCEmailslist.Add("b.trivedi@camindustrial.net");
                    //strCCEmailslist.Add("nirav.m@siliconinfo.com");
                    saveNotification(iObjNotification);

                    FirebaseHelper.InitializeFirebase();

                    //if (objUser.DeviceType == "AP")
                    //{
                    //    await firebaseService.SendIOSNotificationAsync(objUser.UserToken.Trim(), "CAM Industrial ", strMessage);
                    //}
                    //else
                    //{
                    await firebaseService.SendAndroidNotificationAsync(objUser.UserToken.Trim(), "CAM Industrial ", strMessage);
                    //}                    
                    if (objCustomer.CustomerEmail != null)
                    {
                        var toEmail = objCustomer.CustomerEmail.Trim();

                        string strMSG = "";
                        strMSG = "<html>";
                        strMSG += "<head>";
                        strMSG += "<style>";
                        strMSG += "p{margin:0px}";
                        strMSG += "</style>";
                        strMSG += "</head>";
                        strMSG += "<body>";
                        strMSG += "<div style='width: 800px; height: auto; border: 0px solid #e3e4e8; margin: 0px; padding: 10px; float: left;'>";
                        if (toEmail == "")
                        {
                            strMSG += "<p style='color:red;'>Customer email has been missing from system. Please update customer's email.  </p>";
                            toEmail = objUser.EmployeeEmail;
                        }

                        if (objCustomer.CustomerContactName == null)
                        {
                            objCustomer.CustomerContactName = "";
                        }

                        if (objCustomer.CustomerContactName != "")
                        {
                            strMSG += "<p>Attention " + objCustomer.CustomerContactName + " [" + objCustomer.CustomerName + "]</p>";
                        }
                        else
                        {
                            strMSG += "<p>Attention " + objCustomer.CustomerName + ", </p>";
                        }
                        strMSG += "<br/>";
                        strMSG += "<br/>";
                        strMSG += "<p>Cam industrial is pleased to inform you that the rack inspection at your facility is scheduled as per below itinerary.</p>";
                        strMSG += "<p>Assigned engineer: " + objUser.EmployeeName + "</p>";
                        strMSG += "<p>Engineer email: " + objUser.EmployeeEmail + "</p>";
                        strMSG += "<p>Date: " + model.InspectionDate.ToString("MMMM dd, yyyy") + "</p>";
                        strMSG += "<p>Customer name: " + objCustomer.CustomerName + "</p>";
                        strMSG += "<p>Location: " + objCustomerLocation.LocationName + " </p>";
                        strMSG += "<br/>";
                        strMSG += "<p>You can access and review the status of the racking inspection, as well as the final report (once it is completed), on the Rack Auditor platform <a href='https://rack-manager.com/' target='_blank'>(rack-manager.com)</a></p>";
                        strMSG += "<br/>";
                        strMSG += "<p>We look forward to working with you soon.</p>";
                        strMSG += "<br/>";
                        strMSG += "<div><div></div></div><br/><br/><div><div>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                        strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                        strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
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
                        var tEmail = new Thread(() => EmailHelper.SendEmail(toEmail, "Rack inspection at your facility is scheduled.", null, strMSG, strCCEmailslist));
                        tEmail.Start();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editInspectionDue(Inspection model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var itm = db.Inspections.Where(x => x.InspectionId == model.InspectionId).FirstOrDefault();
                    if (itm != null)
                    {
                        if (model.InspectionType == null)
                        {
                            return "Please select Inspection Type.";
                        }
                        if (model.CustomerId == 0)
                        {
                            return "Please select Customer.";
                        }
                        if (model.CustomerLocationId == 0)
                        {
                            return "Please select Customer's Location.";
                        }
                        if (model.EmployeeId == 0)
                        {
                            return "Please select Inspector(Employee).";
                        }
                        if (model.InspectionDate == null)
                        {
                            return "Please set Inspection Date.";
                        }
                        itm.InspectionDate = model.InspectionDate;
                        itm.EmployeeId = model.EmployeeId;
                        itm.CustomerId = model.CustomerId;
                        itm.CustomerLocationId = model.CustomerLocationId;
                        itm.CustomerAreaID = model.CustomerAreaID;
                        itm.InspectionType = model.InspectionType;
                        itm.InspectionStatus = model.InspectionStatus;
                        itm.CADDocuments = model.CADDocuments;
                        itm.FacilitiesAreasIds = model.FacilitiesAreasIds;
                        itm.ProcessOverviewIds = model.ProcessOverviewIds;
                        itm.ReferenceDocumentIds = model.ReferenceDocumentIds;
                        itm.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        itm.ModifiedDate = DateTime.Now;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();

                        //string strMessage = "";
                        //Guid notificationID = Guid.NewGuid();
                        //Customer objCustomer = new Customer();
                        //CustomerLocation objCustomerLocation = new CustomerLocation();
                        //UserEmployeeViewModel objUser = new UserEmployeeViewModel();
                        //objCustomer = getCustomerById(model.CustomerId);
                        //objCustomerLocation = getCustomerLocationById(model.CustomerLocationId);
                        //objUser = getUserEmployeeById(model.EmployeeId);

                        //Notification iObjNotification = new Notification();
                        //iObjNotification.NotificationID = notificationID;
                        //strMessage = "You have been assigned to new Inspection for " + objCustomer.CustomerName + " at " + objCustomerLocation.LocationName + " on " + model.InspectionDate.ToString("MMMM dd, yyyy") + ".";
                        //iObjNotification.NotificationText = strMessage;
                        //iObjNotification.SenderPlatform = "Admin";
                        //iObjNotification.ReceiverPlatform = "Mobile";
                        //iObjNotification.Userid_SenderID = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"].ToString());
                        //iObjNotification.Userid_ReceiverID = objUser.UserID;

                        //saveNotification(iObjNotification);
                        //var sendnotification = SendNotificationToApp(iObjNotification, objUser.UserToken, strMessage);
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeInspectionDue(int id)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var itm = db.Inspections.Where(x => x.InspectionId == id).FirstOrDefault();
                    if (itm != null)
                    {
                        itm.IsActive = false;
                        db.Entry(itm).State = EntityState.Modified;
                        db.SaveChanges();
                        return "Ok";
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        internal static InspectionFileDrawing getInspectionFileDrawingById(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionFileDrawings.Where(x => x.InspectionFileDrawingId == id && x.IsDeleted == 0).FirstOrDefault();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
        }

        internal static List<InspectionFileDrawing> getInspectionFileDrawingByInspectionId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.InspectionFileDrawings.Where(x => x.InspectionId == id && x.IsDeleted == 0).ToList();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
        }

        internal static long saveInspectionFileDrawing(InspectionFileDrawing model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    InspectionFileDrawing fileDrawing = new InspectionFileDrawing();
                    long iInspectionId = 0;
                    iInspectionId = model.InspectionId;
                    var name = model.FileDrawingPath.Split(',');
                    foreach (var item in name)
                    {
                        var fileName = System.IO.Path.GetFileNameWithoutExtension(item);  //item.Remove(item.Length - 4);
                        var found = db.InspectionFileDrawings.Where(x => x.FileDrawingName == fileName && x.InspectionId == iInspectionId).FirstOrDefault();
                        if (found == null)
                        {
                            fileDrawing.InspectionId = Convert.ToInt64(model.InspectionId);
                            fileDrawing.FileCategory = model.FileCategory;
                            fileDrawing.FileDrawingName = fileName;
                            fileDrawing.FileDrawingPath = item;
                            fileDrawing.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            fileDrawing.IsDeleted = 0;
                            fileDrawing.CreatedDate = DateTime.Now;
                            fileDrawing.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            fileDrawing.ModifiedDate = DateTime.Now;
                            db.InspectionFileDrawings.Add(fileDrawing);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.InspectionFileDrawings.Remove(found);
                            db.SaveChanges();

                            fileDrawing.InspectionId = Convert.ToInt64(model.InspectionId);
                            fileDrawing.FileCategory = model.FileCategory;
                            fileDrawing.FileDrawingName = fileName;
                            fileDrawing.FileDrawingPath = item;
                            fileDrawing.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            fileDrawing.IsDeleted = 0;
                            fileDrawing.CreatedDate = DateTime.Now;
                            fileDrawing.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            fileDrawing.ModifiedDate = DateTime.Now;
                            db.InspectionFileDrawings.Add(fileDrawing);
                            db.SaveChanges();
                            //if (itemToRemove != null)
                            //{
                            //    Context.Employ.Remove(itemToRemove);
                            //    Context.SaveChanges();
                            //}
                            //found.IsDeleted = 1;
                            //found.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                            //found.ModifiedDate = DateTime.Now;
                            //db.Entry(found).State = EntityState.Modified;
                            //db.SaveChanges();
                        }
                    }
                    return fileDrawing.InspectionId;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        internal static List<CustomerLocationHistoryLegacyFileListing> GetCustomerLocationHistoryLegacyFiles(int id)
        {
            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                    Uri url = new Uri(tmpURL);
                    string host = url.GetLeftPart(UriPartial.Authority);
                    var listings = (from clh in db.CustomerLocationHistoryLegacyFiles
                                    join c in db.Customers
                                        on clh.CustomerId equals c.CustomerId
                                    join cl in db.CustomerLocations
                                        on clh.CustomerLocationID equals cl.CustomerLocationID into clGroup
                                    from cl in clGroup.DefaultIfEmpty() // Left join
                                    where clh.IsDeleted == 0 && c.CustomerId == id
                                    select new CustomerLocationHistoryLegacyFileListing
                                    {
                                        CustomerLocationHistoryLegacyFileId = clh.CustomerLocationHistoryLegacyFileId,
                                        CustomerId = clh.CustomerId,
                                        CustomerLocationID = clh.CustomerLocationID,
                                        FileDrawingPath = host + "/CustFilesHistory/" + clh.FileDrawingPath,
                                        FileDrawingName = clh.FileDrawingName,
                                        FileCategory = clh.FileCategory,
                                        CustomerName = c.CustomerName,
                                        Region = cl.Region,
                                        CustomerLocationName = cl != null ? cl.LocationName : null
                                    }).ToList();


                    if (listings != null)
                    {
                        return listings;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        internal static int removeHistoryLegacyFile(long CustomerLocationHistoryLegacyFileId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.CustomerLocationHistoryLegacyFiles.Where(x => x.CustomerLocationHistoryLegacyFileId == CustomerLocationHistoryLegacyFileId).FirstOrDefault();
                if (itm != null)
                {
                    int Custid = 0;
                    Custid = Convert.ToInt32(itm.CustomerId);
                    db.CustomerLocationHistoryLegacyFiles.Remove(itm);
                    db.SaveChanges();

                    string path = HttpContext.Current.Server.MapPath("~/CustFilesHistory/" + itm.FileDrawingPath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    return Custid;
                }
                return 0;
            }
        }

        internal static long UploadHistoryLegacyFile(CustomerLocationHistoryLegacyFile model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    long iCustomerId = model.CustomerId;
                    long? iCustomerLoationId = model.CustomerLocationID;

                    // file list separated by commas (renamed files)
                    var renamedFiles = model.FileDrawingPath.Split(',');
                    // original filenames list separated by commas (frontend should send it along)
                    var originalFiles = model.FileDrawingName.Split(',');

                    for (int i = 0; i < renamedFiles.Length; i++)
                    {
                        var renamedFile = renamedFiles[i];    // customerid_location_file.pdf
                        var originalFile = originalFiles[i];  // original.pdf

                        // check duplicates based on original name + location + customer
                        var found = db.CustomerLocationHistoryLegacyFiles
                                      .FirstOrDefault(x => x.FileDrawingName == originalFile
                                                        && x.CustomerId == iCustomerId
                                                        && x.CustomerLocationID == iCustomerLoationId);

                        if (found != null)
                        {
                            db.CustomerLocationHistoryLegacyFiles.Remove(found);
                            db.SaveChanges();
                        }

                        var fileDrawing = new CustomerLocationHistoryLegacyFile
                        {
                            CustomerId = iCustomerId,
                            CustomerLocationID = iCustomerLoationId,
                            FileCategory = model.FileCategory,
                            FileDrawingName = originalFile,   // original filename
                            FileDrawingPath = renamedFile,    // renamed filename
                            CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString(),
                            IsDeleted = 0,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString(),
                            ModifiedDate = DateTime.Now
                        };

                        db.CustomerLocationHistoryLegacyFiles.Add(fileDrawing);
                        db.SaveChanges();
                    }

                    return iCustomerId;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        //internal static long UploadHistoryLegacyFile(CustomerLocationHistoryLegacyFile model)
        //{
        //    using (DatabaseEntities db = new DatabaseEntities())
        //    {
        //        try
        //        {
        //            CustomerLocationHistoryLegacyFile fileDrawing = new CustomerLocationHistoryLegacyFile();
        //            long iCustomerId = 0;
        //            long? iCustomerLoationId = 0;
        //            iCustomerId = model.CustomerId;
        //            iCustomerLoationId = model.CustomerLocationID;
        //            var name = model.FileDrawingPath.Split(',');
        //            foreach (var item in name)
        //            {
        //                var fileName = System.IO.Path.GetFileNameWithoutExtension(item);
        //                var found = db.CustomerLocationHistoryLegacyFiles.Where(x => x.FileDrawingName == fileName && x.CustomerId == iCustomerId && x.CustomerLocationID == iCustomerLoationId).FirstOrDefault();
        //                if (found == null)
        //                {
        //                    fileDrawing.CustomerId = Convert.ToInt64(model.CustomerId);
        //                    fileDrawing.CustomerLocationID = Convert.ToInt64(model.CustomerLocationID);
        //                    fileDrawing.FileCategory = model.FileCategory;
        //                    fileDrawing.FileDrawingName = fileName;
        //                    fileDrawing.FileDrawingPath = item;
        //                    fileDrawing.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
        //                    fileDrawing.IsDeleted = 0;
        //                    fileDrawing.CreatedDate = DateTime.Now;
        //                    fileDrawing.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
        //                    fileDrawing.ModifiedDate = DateTime.Now;
        //                    db.CustomerLocationHistoryLegacyFiles.Add(fileDrawing);
        //                    db.SaveChanges();
        //                }
        //                else
        //                {
        //                    db.CustomerLocationHistoryLegacyFiles.Remove(found);
        //                    db.SaveChanges();

        //                    fileDrawing.CustomerId = Convert.ToInt64(model.CustomerId);
        //                    fileDrawing.CustomerLocationID = Convert.ToInt64(model.CustomerLocationID);
        //                    fileDrawing.FileCategory = model.FileCategory;
        //                    fileDrawing.FileDrawingName = fileName;
        //                    fileDrawing.FileDrawingPath = item;
        //                    fileDrawing.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
        //                    fileDrawing.IsDeleted = 0;
        //                    fileDrawing.CreatedDate = DateTime.Now;
        //                    fileDrawing.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
        //                    fileDrawing.ModifiedDate = DateTime.Now;
        //                    db.CustomerLocationHistoryLegacyFiles.Add(fileDrawing);
        //                    db.SaveChanges();
        //                }
        //            }
        //            return fileDrawing.CustomerId;
        //        }
        //        catch (Exception ex)
        //        {
        //            return 0;
        //        }
        //    }
        //}

        internal static long removeInspectionFileDrawing(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.InspectionFileDrawings.Where(x => x.InspectionFileDrawingId == id).FirstOrDefault();
                if (itm != null)
                {
                    //itm.IsDeleted = 1;
                    //db.Entry(itm).State = EntityState.Modified;
                    //db.SaveChanges();
                    db.InspectionFileDrawings.Remove(itm);
                    db.SaveChanges();
                    return itm.InspectionId;
                }
                return 0;
            }
        }

        internal static string saveNotification(Notification model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    Notification notification = new Notification();
                    notification.NotificationID = model.NotificationID;
                    notification.NotificationText = model.NotificationText;
                    notification.SenderPlatform = model.SenderPlatform;
                    notification.ReceiverPlatform = model.ReceiverPlatform;
                    notification.Userid_SenderID = model.Userid_SenderID;
                    notification.Userid_ReceiverID = model.Userid_ReceiverID;
                    notification.ReadStatus = false;
                    notification.CreatedDate = DateTime.Now;
                    notification.CreatedBy = model.CreatedBy;
                    notification.ModifiedDate = DateTime.Now;
                    notification.ModifiedBy = model.ModifiedBy;
                    db.Notifications.Add(notification);
                    db.SaveChanges();

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string GenerateGUID()
        {
            Guid notificationID = Guid.NewGuid();
            return notificationID.ToString();
        }

        internal static List<Notification> getAllNotificationByUserIdWeb()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                var list = db.Notifications.Where(x => x.Userid_ReceiverID == userId).ToList();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
        }

        internal static List<Notification> getAllNotificationByUserId(long id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.Notifications.Where(x => x.Userid_ReceiverID == id).OrderByDescending(p => p.CreatedDate).Take(10).ToList();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
        }

        internal static string UpdateNotificationStatus(List<Notification> model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                //foreach (var item in model)
                //{
                //    var objNotification = db.Notifications.Where(x => x.NotificationID == item.NotificationID && x.ReadStatus == false).FirstOrDefault();
                //    if (objNotification != null)
                //    {
                //        objNotification.ReadStatus = true;
                //        objNotification.ModifiedDate = DateTime.Now;
                //        db.Entry(objNotification).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //}
                return "true";
            }
        }

        //internal static async Task<bool> SendNotificationToAndroid(Notification model, string strdeviceToken, string strMessage)
        //{
        //    bool isSend = false;
        //    FirebaseHelper firebaseService = new FirebaseHelper();
        //    try
        //    {
        //        FirebaseHelper.InitializeFirebase();
        //        await firebaseService.SendNotificationAsync(strdeviceToken, "CAM Industrial", strMessage);
        //        isSend = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        isSend = false;
        //    }
        //    return isSend;
        //}

        //internal static async Task<bool> SendNotificationToApple(Notification model, string strdeviceToken, string strMessage)
        //{
        //    bool isSend = false;
        //    FirebaseHelper firebaseService = new FirebaseHelper();
        //    try
        //    {
        //        FirebaseHelper.InitializeFirebase();
        //        await firebaseService.SendNotificationAsync(strdeviceToken, "CAM Industrial", strMessage);
        //        isSend = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        isSend = false;
        //    }
        //    return isSend;
        //}


        //internal static async Task<bool> SendNotificationToApp(Notification model, string strdeviceToken, string strMessage)
        //{
        //    //var config = new FirebaseConfig
        //    //{
        //    //    AuthSecret = "AIzaSyCTr2zbj-2eGQyqGE87IB-bX7UtsKsV96U",
        //    //    BasePath = "rn-x-698a9.firebaseapp.com"
        //    //};

        //    //var client = new FirebaseClient(config);

        //    //var notification = new
        //    //{
        //    //    to = "drejKO21hU7muKEXTi_L92:APA91bFKtwmqlOS4cR71pYryuKSoy1yArGq0-Ur0GzI_lFIz2-PprxRfJcaggtHRtusCYWmA2_SD3ouGojr_nlRuj1DATab7Ky4HUcmo6gKMsrfXV2ONxFTJ1pqUuI06peysTBumidC3",
        //    //    data = new
        //    //    {
        //    //        key1 = "value1",
        //    //        key2 = "value2"
        //    //    },
        //    //    notification = new
        //    //    {
        //    //        title = "Notification title",
        //    //        body = "Notification body"
        //    //    }
        //    //};

        //    //var response = await client.PushAsync("notifications", notification);
        //    ////var response = await client.PostAsync("notifications", notification);

        //    var serverKey = System.Configuration.ConfigurationManager.AppSettings["FirebaseServerKey"].ToString();
        //    var senderId = System.Configuration.ConfigurationManager.AppSettings["FirebaseSenderId"].ToString();

        //    var deviceToken = strdeviceToken.Trim(); // "drejKO21hU7muKEXTi_L92:APA91bFKtwmqlOS4cR71pYryuKSoy1yArGq0-Ur0GzI_lFIz2-PprxRfJcaggtHRtusCYWmA2_SD3ouGojr_nlRuj1DATab7Ky4HUcmo6gKMsrfXV2ONxFTJ1pqUuI06peysTBumidC3";
        //    var FirebaseRequestURI = System.Configuration.ConfigurationManager.AppSettings["FirebaseRequestURI"].ToString();

        //    var message = new
        //    {
        //        notification = new
        //        {
        //            title = "CAM Industrial",
        //            body = strMessage
        //        },
        //        to = deviceToken
        //    };

        //    var jsonMessage = JsonConvert.SerializeObject(message);

        //    var request = new HttpRequestMessage(HttpMethod.Post, FirebaseRequestURI);
        //    request.Headers.TryAddWithoutValidation("Authorization", $"key={serverKey}");
        //    request.Headers.TryAddWithoutValidation("Sender", $"id={senderId}");
        //    request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

        //    var client = new HttpClient();
        //    var response = await client.SendAsync(request);
        //    if (response != null)
        //    {
        //        return true;
        //    }

        //    return true;
        //}



        internal static List<ActionRequired> getAllActionRequired()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.ActionRequireds.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0) { return list; }
                return null;
            }
        }

        internal static string GenerateInspectionDocumentNo(Int32 CustomerId, Int32 CustomerLocationId, string InspectionType)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                //GET_InsepctionCode_Result InspectionDocumentNo = new GET_InsepctionCode_Result();
                string InspectionDocumentNo;
                InspectionDocumentNo = db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType).FirstOrDefault();
                return Convert.ToString(InspectionDocumentNo);
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<ComponentPropertyType> getComponentPropertyType()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var list = db.ComponentPropertyTypes.Where(x => x.IsActive == true).ToList();
                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
        }

        internal static List<ComponentPropertyType> GetPropertyByComponent(Int32 ComponentId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var listPropertyType = db.ComponentPropertyTypes.Join(db.ComponentsProperties.Where(x => x.ComponentId == ComponentId & x.IsActive == true), d => d.ComponentPropertyTypeId, f => f.ComponentPropertyTypeId, (d, f) => d).ToList();
                //var listManufactures = db.Manufacturers.Join(db.ComponentsManufacturers.Where(x => x.ComponentId == ComponentId)).ToList();

                //var list = db.Manufacturers.Where(x => x.IsActive == true).OrderBy(x => x.ManufacturerName).ToList();
                if (listPropertyType.Count != 0) { return listPropertyType; }
                return null;
            }
            //using (DatabaseEntities db = new DatabaseEntities())
            //{
            //    var list = db.ComponentPropertyTypes.ToList();

            //    if (list.Count != 0)
            //    {
            //        return list;
            //    }
            //    return null;
            //}
        }



        internal static List<ComponentPropertyValue> getComponentPropertyValues(int componentPropertyTypeId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                //var list = db.ComponentPropertyValues.Where(x => x.ComponentPropertyTypeId == ComponentPropertyTypeId && x.IsActive == true).OrderBy(x => x.ComponentPropertyValue1).ToList();                
                //           var list = db.ComponentPropertyValues.Where(c => c.ComponentPropertyTypeId == ComponentPropertyTypeId).AsEnumerable().OrderBy(c =>
                //{
                //    var cleanedValue = c.ComponentPropertyValue1.Replace("\"", "")
                //                                               .Replace("'", "")
                //                                               .Replace("/", "")
                //                                               .Replace("\\", "");


                //    float parsedValue;
                //    return float.TryParse(cleanedValue, out parsedValue) ? parsedValue : float.MaxValue;
                //}).ToList();
                var list = db.ComponentPropertyValues
               .Where(c => c.ComponentPropertyTypeId == componentPropertyTypeId)  // Filter by ComponentPropertyTypeId = 90
               .AsEnumerable()
               .OrderBy(c =>
               {
                   var cleanedValue = c.ComponentPropertyValue1.Replace("\"", "")
                                                                        .Replace("'", "")
                                                                        .Replace("/", "")
                                                                        .Replace("\\", "")
                                                                        .Replace("-", "");

                   float parsedValue;
                   if (TryParseFraction(cleanedValue, out parsedValue))
                   {
                       return parsedValue;
                   }
                   else
                   {
                       return float.MaxValue;
                   }
               })
               .ThenBy(c => c.ComponentPropertyValue1)
               .ToList();

                if (list.Count != 0)
                {
                    return list;
                }
                return null;
            }
        }
        public static bool TryParseFraction(string value, out float result)
        {
            result = 0;

            // Match fraction-like patterns such as "2-1/4"
            var regex = new Regex(@"^(\d+)-(\d+)/(\d+)$");
            var match = regex.Match(value);

            if (match.Success)
            {
                // Extract the whole number and fraction
                int wholePart = int.Parse(match.Groups[1].Value);
                int numerator = int.Parse(match.Groups[2].Value);
                int denominator = int.Parse(match.Groups[3].Value);

                // Calculate the decimal equivalent
                result = wholePart + (float)numerator / denominator;
                return true;
            }

            // Try to parse as a simple numeric value (like "2.5")
            return float.TryParse(value, out result);
        }


        internal static Task<Dashboard> GetCustomerDashboardCountByYear(int year)
        {
            Dashboard objDashboard = new Dashboard();
            objDashboard.InspectionDueCount = 0;
            objDashboard.SentforApprovalCount = 0;
            objDashboard.ApprovedCompletedCount = 0;
            objDashboard.InProgressCount = 0;
            objDashboard.QuotationRequestedCount = 0;
            objDashboard.AwaitingApprovalCount = 0;
            objDashboard.QuotationApprovedCount = 0;
            objDashboard.RepairCompletedCount = 0;
            objDashboard.InspectionFinishedCount = 0;

            try
            {
                using (DatabaseEntities db = new DatabaseEntities())
                {
                    var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                    var customer = db.Customers.Where(x => x.UserID == userId && x.IsActive == true).FirstOrDefault();
                    if (customer != null)
                    {


                        var objDashboardResult = db.SP_Get_CustomerDashboardCount(year, customer.CustomerId);
                        var list = objDashboardResult.ToList();
                        foreach (var item in list)
                        {
                            switch (item.InspectionStatusId)
                            {
                                case 1:
                                    objDashboard.InspectionDueCount = (long)item.cnt;
                                    break;
                                case 2:
                                    objDashboard.InProgressCount = (long)item.cnt;
                                    break;
                                case 3:
                                    objDashboard.SentforApprovalCount = (long)item.cnt;
                                    break;
                                case 4:
                                    objDashboard.ApprovedCompletedCount = (long)item.cnt;
                                    break;
                                case 5:
                                    objDashboard.QuotationRequestedCount = (long)item.cnt;
                                    break;
                                case 6:
                                    objDashboard.AwaitingApprovalCount = (long)item.cnt;
                                    break;
                                case 7:
                                    objDashboard.QuotationApprovedCount = (long)item.cnt;
                                    break;
                                case 8:
                                    objDashboard.RepairCompletedCount = (long)item.cnt;
                                    break;
                                case 9:
                                    objDashboard.InspectionFinishedCount = (long)item.cnt;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                objDashboard.InspectionDueCount = 0;
                objDashboard.InProgressCount = 0;
                objDashboard.SentforApprovalCount = 0;
                objDashboard.ApprovedCompletedCount = 0;
                objDashboard.QuotationRequestedCount = 0;
                objDashboard.AwaitingApprovalCount = 0;
                objDashboard.QuotationApprovedCount = 0;
                objDashboard.RepairCompletedCount = 0;
                objDashboard.InspectionFinishedCount = 0;
                // throw;
            }
            return Task.FromResult(objDashboard);
        }

        internal static string getComponentFromDeficiency(int DeficiencyId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var DeficiencyData = db.Deficiencies.Where(x => x.DeficiencyID == DeficiencyId).FirstOrDefault();
                if (DeficiencyData != null)
                {
                    if (DeficiencyData.ComponentId != null)
                    {
                        return DeficiencyData.ComponentId.ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                return "0";
            }
        }
        internal static List<IdentifyRackingProfile> getAllIdentifyRackingProfile()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<IdentifyRackingProfile> ListIdentifyRackingProfile = new List<IdentifyRackingProfile>();
                string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Uri url = new Uri(tmpURL);
                string host = url.GetLeftPart(UriPartial.Authority);
                var lstIdentifyRackingProfile = db.IdentifyRackingProfiles.Where(x => x.IsActive == true).ToList();
                if (lstIdentifyRackingProfile.Count != 0)
                {
                    foreach (var d in lstIdentifyRackingProfile)
                    {
                        IdentifyRackingProfile objListIdentifyRackingProfile = new IdentifyRackingProfile();
                        objListIdentifyRackingProfile.IdentifyRackingProfileID = d.IdentifyRackingProfileID;
                        if (d.IdentifyRackingProfileImage == "" || d.IdentifyRackingProfileImage == null)
                        {
                            objListIdentifyRackingProfile.IdentifyRackingProfileImage = "";
                        }
                        else
                        {
                            objListIdentifyRackingProfile.IdentifyRackingProfileImage = host + "/img/IdentifyRackingProfile/" + d.IdentifyRackingProfileImage;
                        }
                        objListIdentifyRackingProfile.CreatedDate = d.CreatedDate;
                        objListIdentifyRackingProfile.CreatedBy = d.CreatedBy;
                        objListIdentifyRackingProfile.ModifiedDate = d.CreatedDate;
                        objListIdentifyRackingProfile.ModifiedBy = d.CreatedBy;
                        ListIdentifyRackingProfile.Add(objListIdentifyRackingProfile);
                    }
                    //return listObj;
                    return ListIdentifyRackingProfile;
                }
                return null;
            }
        }

        internal static string saveIdentifyRackingProfile(IdentifyRackingProfile model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    IdentifyRackingProfile iRackProfileObj = new IdentifyRackingProfile();
                    iRackProfileObj.IdentifyRackingProfileImage = model.IdentifyRackingProfileImage;
                    iRackProfileObj.IsActive = true;
                    iRackProfileObj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    iRackProfileObj.CreatedDate = DateTime.Now;
                    iRackProfileObj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    iRackProfileObj.ModifiedDate = DateTime.Now;
                    db.IdentifyRackingProfiles.Add(iRackProfileObj);
                    db.SaveChanges();
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string editIdentifyRackingProfile(IdentifyRackingProfile model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var def = db.IdentifyRackingProfiles.Where(x => x.IdentifyRackingProfileID == model.IdentifyRackingProfileID).FirstOrDefault();
                    if (def != null)
                    {
                        def.IdentifyRackingProfileImage = model.IdentifyRackingProfileImage;
                        def.IsActive = true;
                        def.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        def.ModifiedDate = DateTime.Now;
                        db.Entry(def).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        internal static string removeIdentifyRackingProfile(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.IdentifyRackingProfiles.Where(x => x.IdentifyRackingProfileID == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    itm.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    itm.ModifiedDate = DateTime.Now;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }
        internal static IdentifyRackingProfile getIdentifyRackingProfileById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.IdentifyRackingProfiles.Where(x => x.IsActive == true && x.IdentifyRackingProfileID == id).FirstOrDefault();
                if (itm != null) { return itm; }
                return null;
            }
        }

        internal static List<DocumentTitle> getAllDocumentTitle()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DocumentTitles.Where(x => x.IsActive == true).ToList();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static DocumentTitle getDocumentTitleById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DocumentTitles.Where(x => x.IsActive == true && x.DocumentId == id).FirstOrDefault();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static ImpSetting getImportantSettingById(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.ImpSettings.Where(x => x.SettingID == id).FirstOrDefault();
                if (itm != null)
                {
                    return itm;
                }
                return null;
            }
        }

        internal static int saveDocumentTitle(DocumentTitle model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    DocumentTitle obj = new DocumentTitle();
                    obj.DocumentTitle1 = model.DocumentTitle1;
                    obj.DocumentDescription = model.DocumentDescription;
                    obj.IsActive = true;
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                    db.DocumentTitles.Add(obj);
                    db.SaveChanges();

                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        internal static string editDocumentTitle(DocumentTitle model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var item = db.DocumentTitles.Where(x => x.DocumentId == model.DocumentId).FirstOrDefault();
                    if (item != null)
                    {
                        item.DocumentTitle1 = model.DocumentTitle1;
                        item.DocumentDescription = model.DocumentDescription;
                        item.IsActive = true;
                        item.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        item.ModifiedDate = DateTime.Now;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string EditSetting(ImpSetting model)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var item = db.ImpSettings.Where(x => x.SettingID == model.SettingID).FirstOrDefault();
                    if (item != null)
                    {
                        item.SettingValue = model.SettingValue;
                        item.ModifiedBy = HttpContext.Current.Session["LoggedInUserId"].ToString();
                        item.ModifiedDate = DateTime.Now;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return "Ok";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        internal static string removeDocumentTitle(int id)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var itm = db.DocumentTitles.Where(x => x.DocumentId == id).FirstOrDefault();
                if (itm != null)
                {
                    itm.IsActive = false;
                    db.Entry(itm).State = EntityState.Modified;
                    db.SaveChanges();
                    return "Ok";
                }
                return null;
            }
        }


        internal static List<InsepctionCount_Graph> getInspectionCountGraph(Int32 UserID)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                List<InsepctionCount_Graph> InspectionCount = new List<InsepctionCount_Graph>();
                var tmpInspectionCount = db.Get_InspectionCountGraph(UserID).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    InsepctionCount_Graph objGET_InsepctionCount_Graph = new InsepctionCount_Graph();
                    objGET_InsepctionCount_Graph.InspectionStatusName = item.InspectionStatusName;
                    objGET_InsepctionCount_Graph.InspectionStatusColor = item.InspectionStatusColor.Trim();
                    objGET_InsepctionCount_Graph.cnt = (Int32)item.cnt;
                    InspectionCount.Add(objGET_InsepctionCount_Graph);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<InsepctionCount_Graph> getInspectionCountGraphByYear(Int32 id, int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {

                Int32 userId = 0;

                if (id == 0)
                {
                    userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                }
                else
                {
                    userId = id;
                }
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                List<InsepctionCount_Graph> InspectionCount = new List<InsepctionCount_Graph>();
                var tmpInspectionCount = db.Get_InspectionCountGraph_new(userId, year).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    InsepctionCount_Graph objGET_InsepctionCount_Graph = new InsepctionCount_Graph();
                    objGET_InsepctionCount_Graph.InspectionStatusName = item.InspectionStatusName;
                    objGET_InsepctionCount_Graph.InspectionStatusColor = item.InspectionStatusColor.Trim();
                    objGET_InsepctionCount_Graph.cnt = (Int32)item.cnt;
                    InspectionCount.Add(objGET_InsepctionCount_Graph);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<Get_DeficienciesBySeverityCustomerNew_Result> GetDeficienciesbySeverityCustomer(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                List<Get_DeficienciesBySeverityCustomerNew_Result> InspectionCount = new List<Get_DeficienciesBySeverityCustomerNew_Result>();
                var tmpInspectionCount = db.Get_DeficienciesBySeverityCustomerNew(userId, year).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    Get_DeficienciesBySeverityCustomerNew_Result objGET_InsepctionCount_Graph = new Get_DeficienciesBySeverityCustomerNew_Result();
                    objGET_InsepctionCount_Graph.ClassificationsColor = item.ClassificationsColor.Trim();
                    objGET_InsepctionCount_Graph.Classifications = item.Classifications.Trim();
                    objGET_InsepctionCount_Graph.InspectionDeficiencyCnt = (Int32)item.InspectionDeficiencyCnt;
                    InspectionCount.Add(objGET_InsepctionCount_Graph);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<Get_DeficienciesBySeverityCustomerInspection_Result> GetDeficienciesbySeverityCustomerInspection(long InspectionId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                List<Get_DeficienciesBySeverityCustomerInspection_Result> InspectionCount = new List<Get_DeficienciesBySeverityCustomerInspection_Result>();
                var tmpInspectionCount = db.Get_DeficienciesBySeverityCustomerInspection(InspectionId).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    Get_DeficienciesBySeverityCustomerInspection_Result objGET_InsepctionCount_Graph = new Get_DeficienciesBySeverityCustomerInspection_Result();
                    objGET_InsepctionCount_Graph.ClassificationsColor = item.ClassificationsColor.Trim();
                    objGET_InsepctionCount_Graph.Classifications = item.Classifications.Trim();
                    objGET_InsepctionCount_Graph.InspectionDeficiencyCnt = (Int32)item.InspectionDeficiencyCnt;
                    InspectionCount.Add(objGET_InsepctionCount_Graph);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<Get_DeficienciesTrendFromPreviousYears_Result> GetDeficienciesTrendFromPreviousYears()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                List<Get_DeficienciesTrendFromPreviousYears_Result> InspectionCount = new List<Get_DeficienciesTrendFromPreviousYears_Result>();
                var tmpInspectionCount = db.Get_DeficienciesTrendFromPreviousYears(userId).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    Get_DeficienciesTrendFromPreviousYears_Result objGET_InsepctionCount_Graph = new Get_DeficienciesTrendFromPreviousYears_Result();
                    objGET_InsepctionCount_Graph.Years = item.Years;
                    objGET_InsepctionCount_Graph.InspectionDeficiencyCnt = (Int32)item.InspectionDeficiencyCnt;
                    objGET_InsepctionCount_Graph.Classifications = item.Classifications;
                    objGET_InsepctionCount_Graph.ClassificationsColor = item.ClassificationsColor;
                    objGET_InsepctionCount_Graph.DataOrder = item.DataOrder;
                    InspectionCount.Add(objGET_InsepctionCount_Graph);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }
        internal static List<Get_DeficienciesTrendFromPreviousYearsCustomerLocation_Result> GetDeficienciesTrendFromPreviousYearsForCustomerLocation(int customerLocationId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                List<Get_DeficienciesTrendFromPreviousYearsCustomerLocation_Result> InspectionCount = new List<Get_DeficienciesTrendFromPreviousYearsCustomerLocation_Result>();
                var tmpInspectionCount = db.Get_DeficienciesTrendFromPreviousYearsCustomerLocation(userId, customerLocationId).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    Get_DeficienciesTrendFromPreviousYearsCustomerLocation_Result objGET_InsepctionCount_Graph = new Get_DeficienciesTrendFromPreviousYearsCustomerLocation_Result();
                    objGET_InsepctionCount_Graph.Years = item.Years;
                    objGET_InsepctionCount_Graph.InspectionDeficiencyCnt = (Int32)item.InspectionDeficiencyCnt;
                    objGET_InsepctionCount_Graph.Classifications = item.Classifications;
                    objGET_InsepctionCount_Graph.ClassificationsColor = item.ClassificationsColor;
                    objGET_InsepctionCount_Graph.DataOrder = item.DataOrder;
                    InspectionCount.Add(objGET_InsepctionCount_Graph);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<Get_DeficienciesBreakdownCategories_Result> GetDeficienciesBreakdownCategories(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                List<Get_DeficienciesBreakdownCategories_Result> InspectionCount = new List<Get_DeficienciesBreakdownCategories_Result>();
                var tmpInspectionCount = db.Get_DeficienciesBreakdownCategories(userId, year).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    Get_DeficienciesBreakdownCategories_Result objDeficienciesBreakdownCategories = new Get_DeficienciesBreakdownCategories_Result();
                    objDeficienciesBreakdownCategories.DeficiencyCategoryId = item.DeficiencyCategoryId;
                    objDeficienciesBreakdownCategories.DeficiencyCategoryName = item.DeficiencyCategoryName;
                    objDeficienciesBreakdownCategories.DeficiencyCategoryCount = (Int32)item.DeficiencyCategoryCount;
                    objDeficienciesBreakdownCategories.MinorCnt = (Int32)item.MinorCnt;
                    objDeficienciesBreakdownCategories.IntermediateCnt = (Int32)item.IntermediateCnt;
                    objDeficienciesBreakdownCategories.MajorCnt = (Int32)item.MajorCnt;
                    InspectionCount.Add(objDeficienciesBreakdownCategories);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<Get_DeficienciesBreakdownCategoriesInspection_Result> GetDeficienciesBreakdownCategoriesInspection(long InspectionId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var userId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                //var InspectionDocumentNo = "00032CNAL0022RI00001";//db.GET_InsepctionCode(CustomerId, CustomerLocationId, InspectionType);
                List<Get_DeficienciesBreakdownCategoriesInspection_Result> InspectionCount = new List<Get_DeficienciesBreakdownCategoriesInspection_Result>();
                var tmpInspectionCount = db.Get_DeficienciesBreakdownCategoriesInspection(InspectionId).ToList();
                foreach (var item in tmpInspectionCount)
                {
                    Get_DeficienciesBreakdownCategoriesInspection_Result objDeficienciesBreakdownCategories = new Get_DeficienciesBreakdownCategoriesInspection_Result();
                    objDeficienciesBreakdownCategories.DeficiencyCategoryId = item.DeficiencyCategoryId;
                    objDeficienciesBreakdownCategories.DeficiencyCategoryName = item.DeficiencyCategoryName;
                    objDeficienciesBreakdownCategories.DeficiencyCategoryCount = (Int32)item.DeficiencyCategoryCount;
                    objDeficienciesBreakdownCategories.MinorCnt = (Int32)item.MinorCnt;
                    objDeficienciesBreakdownCategories.IntermediateCnt = (Int32)item.IntermediateCnt;
                    objDeficienciesBreakdownCategories.MajorCnt = (Int32)item.MajorCnt;
                    InspectionCount.Add(objDeficienciesBreakdownCategories);
                }
                return InspectionCount;
                //return Convert.ToString(InspectionDocumentNo);
            }
        }

        internal static List<sp_getEmpInspection_Count_Result> getDoneEmpInspectionGraph()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var data = db.sp_getEmpInspection_Count().ToList();
                return data;
            }
        }

        internal static List<sp_getApprovedInspection_Count_Result> getApprovedInspectionCount_Graph()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var data = db.sp_getApprovedInspection_Count().ToList();
                return data;
            }
        }

        internal static List<sp_getEmpInspection_Count_New_Result> getDoneEmpInspectionCountByYear(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var data = db.sp_getEmpInspection_Count_New(year).ToList();
                return data;
            }
        }

        internal static List<sp_getApprovedInspection_Count_New_Result> getApprovedInspectionCountByYear(int year)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var data = db.sp_getApprovedInspection_Count_New(year).ToList();
                return data;
            }
        }

        internal static AdminDashboardGraphViewModel GetDashboardCountByYear(int year)
        {
            //using (DatabaseEntities db = new DatabaseEntities())
            //{
            //    var data = db.sp_getApprovedInspection_Count_New(year).ToList();
            //    return data;
            //}
            AdminDashboardGraphViewModel graph = new AdminDashboardGraphViewModel();
            //string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            //Response.Write(host.ToString());
            //var userId = Convert.ToInt32(Session["LoggedInUserId"]);
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
            graph.DashboardActiveInventoryCount = 0;

            //graph.PieYear = DatabaseHelper.getInspectionCountGraphByYear(userId, 2023);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var objDashboardResult = db.SP_Get_AdminDashboardCount(year);

                foreach (var item in objDashboardResult)
                {
                    switch (item.InspectionStatusId)
                    {
                        case 1:
                            graph.InspectionDueCount = (long)item.cnt;
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
            return graph;
        }

        internal static AdminDashboardGraphViewModel GetDashboardCountForEmployeeByYear(Int32 id, int year)
        {
            //using (DatabaseEntities db = new DatabaseEntities())
            //{
            //    var data = db.sp_getApprovedInspection_Count_New(year).ToList();
            //    return data;
            //}
            AdminDashboardGraphViewModel graph = new AdminDashboardGraphViewModel();
            //string host = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            //Response.Write(host.ToString());
            //var userId = Convert.ToInt32(Session["LoggedInUserId"]);
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
            graph.DashboardActiveInventoryCount = 0;

            //graph.PieYear = DatabaseHelper.getInspectionCountGraphByYear(userId, 2023);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                if (id == 0)
                {
                    var objDashboardResult = db.SP_Get_AdminDashboardCount(year);

                    foreach (var item in objDashboardResult)
                    {
                        switch (item.InspectionStatusId)
                        {
                            case 1:
                                graph.InspectionDueCount = (long)item.cnt;
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
                else
                {
                    var objDashboardResult = db.SP_Get_AllEmployeeDashboardCount(id, year);

                    foreach (var item in objDashboardResult)
                    {
                        switch (item.InspectionStatusId)
                        {
                            case 1:
                                graph.InspectionDueCount = (long)item.cnt;
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

            }
            graph.LineDone = DatabaseHelper.getDoneEmpInspectionCountByYear(year);
            graph.LineApproved = DatabaseHelper.getApprovedInspectionCountByYear(year);
            return graph;
        }

        internal static CAMEmailSetting GetEmailInformation()
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                try
                {
                    var mailInfo = db.CAMEmailSettings.FirstOrDefault();
                    return mailInfo;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public static string RemoveDuplicates(string input)
        {
            // Split the string into an array of elements
            string[] elements = input.Split(',');

            // Convert the array to a HashSet to remove duplicates
            HashSet<string> uniqueElements = new HashSet<string>(elements);

            // Join the unique elements back into a string
            string result = string.Join(",", uniqueElements);

            return result;
        }

        public static double ConvertMixedFractionToDecimal(string mixedFraction)
        {
            double wholeNumber = 0;
            double fraction = 0;

            try
            {
                // Remove the quote symbol (if present)
                mixedFraction = mixedFraction.Replace("\"", "");

                // Check if the input contains a whole number and a fraction
                var parts = mixedFraction.Split('-');

                // Handle case where the input is a mixed fraction (e.g., 3-1/4)
                if (parts.Length == 2)
                {
                    wholeNumber = double.Parse(parts[0]); // Whole number part
                    var fractionParts = parts[1].Split('/'); // Fraction part

                    if (fractionParts.Length == 2)
                    {
                        double numerator = double.Parse(fractionParts[0]); // Numerator
                        double denominator = double.Parse(fractionParts[1]); // Denominator
                        fraction = numerator / denominator; // Calculate fraction value
                    }
                }
                // Handle case where the input is just a fraction (e.g., 1/4)
                else if (parts.Length == 1)
                {
                    var fractionParts = parts[0].Split('/');

                    if (fractionParts.Length == 2)
                    {
                        double numerator = double.Parse(fractionParts[0]); // Numerator
                        double denominator = double.Parse(fractionParts[1]); // Denominator
                        fraction = numerator / denominator; // Calculate fraction value
                    }
                    else
                    {
                        // If it's just a whole number (no fraction part)
                        wholeNumber = double.Parse(parts[0]);
                    }
                }
                return wholeNumber + fraction; // Return the sum of the whole number and fraction
            }
            catch (Exception ex)
            {
                // In case of any error, return 0
                return 0;
            }
        }
        internal static List<IncidentViewModel> GetAllIncidentByCustomerId(FilterCustomerModel filters)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<IncidentViewModel> _listM = new List<IncidentViewModel>();
                var userId = Convert.ToInt64(HttpContext.Current.Session["LoggedInUserId"]);
                if (userId != 0)
                {
                    var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
                    if (customer != null)
                    {
                        string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
                        Uri url = new Uri(tmpURL);
                        string host = url.GetLeftPart(UriPartial.Authority);
                        List<sp_GetIncidentReportsByCustomer_Result> list;
                        if (filters == null)
                        {
                            //5,NULL,0,NULL,0,0
                            list = db.sp_GetIncidentReportsByCustomer(customer.CustomerId, 0, 0, 0, null).ToList();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(filters.Location))
                            {
                                filters.Location = "0";
                            }
                            list = db.sp_GetIncidentReportsByCustomer(customer.CustomerId, Convert.ToInt64(filters.Location), filters.Province, filters.City, filters.Region).ToList();
                        }

                        if (list.Count != 0)
                        {
                            foreach (var d in list)
                            {
                                IncidentViewModel _list = new IncidentViewModel();
                                _list.IncidentReportId = d.IncidentReportId;
                                _list.IncidentNumber = d.IncidentNumber;
                                _list.IncidentReportedBy = d.IncidentReportedBy;
                                _list.IncidentType = d.IncidentType;
                                _list.IncidentDate = d.IncidentDate;
                                _list.CustomerId = d.CustomerId ?? 0;
                                _list.CustomerLocationId = d.CustomerLocationId;
                                _list.LocationName = d.LocationName;
                                _list.Region = d.Region;
                                _list.IncidentSummary = d.IncidentSummary;
                                _list.CreatedDate = d.CreatedDate;
                                _listM.Add(_list);
                            }
                        }


                        _listM = _listM.OrderByDescending(x => x.IncidentDate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                        if (_listM.Count != 0)
                        {
                            return _listM;
                        }
                    }
                    else
                    {

                    }
                }
                return null;
            }
        }

        internal static List<IncidentViewModel> GetAllIncidentAdminByCustomerId(long CustomerId)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            {
                List<IncidentViewModel> _listM = new List<IncidentViewModel>();
                List<sp_GetIncidentReportsByCustomer_Result> list;
                list = db.sp_GetIncidentReportsByCustomer(CustomerId, 0, 0, 0, null).ToList();
                if (list.Count != 0)
                {
                    foreach (var d in list)
                    {
                        IncidentViewModel _list = new IncidentViewModel();
                        _list.IncidentReportId = d.IncidentReportId;
                        _list.IncidentNumber = d.IncidentNumber;
                        _list.IncidentReportedBy = d.IncidentReportedBy;
                        _list.IncidentType = d.IncidentType;
                        _list.IncidentDate = d.IncidentDate;
                        _list.CustomerId = d.CustomerId ?? 0;
                        _list.CustomerLocationId = d.CustomerLocationId;
                        _list.LocationName = d.LocationName;
                        _list.Region = d.Region;
                        _list.IncidentSummary = d.IncidentSummary;
                        _list.CreatedDate = d.CreatedDate;
                        _listM.Add(_list);
                    }
                }
                _listM = _listM.OrderByDescending(x => x.IncidentDate).ToList(); //&& x.InspectionStatus >= 4 x.InspectionStatus > InspectionStatusId                        
                if (_listM.Count != 0)
                {
                    return _listM;
                }
                return null;
            }
        }

        internal static string SaveIncidentByCustomerId(IncidentViewModel model, List<HttpPostedFileBase> files)
        {
            using (DatabaseEntities db = new DatabaseEntities())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var userIdObj = HttpContext.Current.Session["LoggedInUserId"];

                    string CustomerFullAddress = "";
                    List<string> FullAddress = new List<string>();

                    long userId = (userIdObj == null) ? 0 : Convert.ToInt64(userIdObj);
                    if (userId == 0)
                        throw new Exception("User is not logged in.");

                    var cust = db.Customers.FirstOrDefault(x => x.UserID == userId);
                    if (cust == null)
                        throw new Exception("Customer not found for this user.");

                    string strLocationName = "";
                    List<string> attachedPhotos = new List<string>();
                    List<FileContentResult> attachmentFiles = new List<FileContentResult>();
                    if (model.CustomerLocationId != 0)
                    {
                        var loc = getCustomerLocationById(Convert.ToInt16(model.CustomerLocationId));
                        if (loc != null)
                        {
                            strLocationName = loc.LocationName;
                            FullAddress.Add(loc.LocationName);
                            if (loc.CustomerAddress != null)
                            {
                                FullAddress.Add(loc.CustomerAddress);
                                //FullAddress.Add(loc.Pincode);
                                //_list.custModel.CustomerAddress = loc.CustomerAddress;
                            }
                        }

                    }
                    else
                    {
                        model.CustomerLocationId = 0;
                        strLocationName = "";
                    }

                    if (cust != null)
                    {
                        if (cust.CustomerAddress != null)
                        {
                            FullAddress.Add(cust.CustomerAddress);
                        }
                    }

                    CustomerFullAddress = string.Join(",", FullAddress);

                    // --- IncidentReport insert ---
                    var incident = new IncidentReport
                    {
                        IncidentType = model.IncidentType,
                        CustomerId = cust.CustomerId,                     // from session/customer map
                        CustomerLocationId = model.CustomerLocationId,
                        IncidentDate = model.IncidentDate == DateTime.MinValue
                                       ? DateTime.Now
                                       : model.IncidentDate,
                        IncidentNumber = model.IncidentNumber,
                        IncidentReportedBy = model.IncidentReportedBy,
                        IncidentArea = model.IncidentArea,
                        IncidentRow = model.IncidentRow,
                        IncidentAisle = model.IncidentAisle,
                        IncidentBay = model.IncidentBay,
                        IncidentLevel = model.IncidentLevel,                  // CSV like "1,2,All"
                        IncidentBeamLocation = model.IncidentBeamLocation,    // CSV
                        IncidentFrameSide = model.IncidentFrameSide,          // CSV
                        IncidentSummary = model.IncidentSummary,
                        CreatedBy = userId.ToString(),
                        CreatedDate = DateTime.Now,
                        ModifiedBy = userId.ToString(),
                        ModifiedDate = DateTime.Now
                    };

                    db.IncidentReports.Add(incident);
                    db.SaveChanges(); // generates IncidentReportId

                    if (files != null && files.Count > 0)
                    {
                        string root = HttpContext.Current.Server.MapPath("~/IncidentPhoto/");
                        if (!Directory.Exists(root))
                            Directory.CreateDirectory(root);

                        int i = 1;
                        foreach (var file in files)
                        {
                            if (file == null || file.ContentLength <= 0) continue;

                            string ext = Path.GetExtension(file.FileName);
                            string tempFileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string unique = tempFileName + "_" + incident.IncidentReportId + "_" + i.ToString() + ext;
                            string physicalPath = Path.Combine(root, unique);
                            file.SaveAs(physicalPath);

                            var photo = new IncidentReportPhoto
                            {
                                IncidentReportId = incident.IncidentReportId,
                                IncidentPhotoName = unique,
                                IncidentPhotoPath = "/IncidentPhoto/" + unique,
                                CreatedBy = userId.ToString(),
                                CreatedDate = DateTime.Now,
                                ModifiedBy = userId.ToString(),
                                ModifiedDate = DateTime.Now
                            };
                            db.IncidentReportPhotoes.Add(photo);

                            // Read file and add as attachment
                            byte[] fileBytes = File.ReadAllBytes(physicalPath);
                            string contentType = MimeMapping.GetMimeMapping(physicalPath); // get mime type

                            var fileContentResult = new FileContentResult(fileBytes, contentType)
                            {
                                FileDownloadName = unique
                            };

                            attachmentFiles.Add(fileContentResult);
                            attachedPhotos.Add(unique);
                            i++;
                        }

                        db.SaveChanges();
                    }

                    transaction.Commit();
                    //cust.CustomerEmail = "nirav.m@siliconinfo.com";

                    string customerName = cust.CustomerContactName ?? cust.CustomerName;
                    string businessName = cust.CustomerName;
                    string incidentAddress = strLocationName;
                    string rackType = model.IncidentType;
                    string locationDescription = CustomerFullAddress;
                    string reportedBy = model.IncidentReportedBy;
                    DateTime incidentDate = model.IncidentDate;
                    string incidentNumber = model.IncidentNumber;
                    string incidentSummary = model.IncidentSummary;

                    // Send email to CAM and client :
                    // To Customer ----
                    if (cust.CustomerEmail != null)
                    {
                        var toEmail = cust.CustomerEmail.Trim();

                        string strMSG = "";
                        strMSG += "<html>";
                        strMSG += "<head>";
                        strMSG += "<style>";
                        strMSG += "p { margin: 0px; font-family: Verdana, sans-serif; font-size: 12px; }";
                        strMSG += "ul { padding-left: 20px; }";
                        strMSG += "</style>";
                        strMSG += "</head>";
                        strMSG += "<body>";
                        strMSG += "<div style='width: 800px; padding: 10px;'>";

                        // Greeting
                        strMSG += $"<p>Dear {customerName},</p><br/>";

                        // Intro
                        strMSG += "<p>Thank you for submitting the incident report regarding racking at your facility. We have successfully received the details, and our team at <b><span style='color:#005aab;'>Cam Industrial</span></b> will follow up with you shortly to review the incident and determine the necessary steps.</p><br/>";

                        // Summary Header
                        strMSG += "<p><b>Here is a summary of your submission for your reference:</b></p>";
                        strMSG += "<hr style='border: 1px solid #ccc;'/>";

                        // Incident Details
                        strMSG += "<p><b>Incident Report Details</b></p>";
                        strMSG += "<ul>";
                        strMSG += $"<li><b>Business Name:</b> {businessName}</li>";
                        strMSG += $"<li><b>Incident Address:</b> {incidentAddress}</li>";
                        strMSG += $"<li><b>Type of Racking Involved:</b> {rackType}</li>";
                        strMSG += $"<li><b>Location of Incident (within facility):</b> {locationDescription}</li>";
                        strMSG += $"<li><b>Reported By:</b> {reportedBy}</li>";
                        strMSG += $"<li><b>Date of Incident:</b> {incidentDate.ToString("MMMM dd, yyyy")}</li>";
                        strMSG += $"<li><b>Incident Number:</b> {incidentNumber}</li>";
                        strMSG += "</ul>";

                        // Incident Summary
                        strMSG += "<p><b>Incident Summary:</b></p>";
                        strMSG += $"<p>{incidentSummary.Replace(Environment.NewLine, "<br/>")}</p><br/>";

                        // Photos

                        if (attachedPhotos != null)
                        {
                            strMSG += "<p><b>Attached Photos:</b></p>";
                            strMSG += $"<p>{(attachedPhotos.Count > 0 ? string.Join("<br/>", attachedPhotos) : "Photos attached to this email.")}</p>";
                        }

                        strMSG += "<hr style='border: 1px solid #ccc;'/><br/>";
                        // Contact info
                        strMSG += "<p>If you have additional information or questions regarding this report, please feel free to contact us at ";
                        strMSG += "<a href='mailto:b.trivedi@camindustrial.net'>b.trivedi@camindustrial.net</a> or (403) 690-2976.</p><br/>";

                        strMSG += "<p>We appreciate your diligence in maintaining a safe workplace.</p><br/>";
                        strMSG += "<br/><br/>";
                        strMSG += "<p>Sincerely,</p>";
                        strMSG += "<div><div>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                        strMSG += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<br/>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                        strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                        strMSG += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                        strMSG += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                        strMSG += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                        strMSG += "<p><span><img style='width:2.618in;height:.6458in' ";
                        strMSG += "src='https://rack-manager.com/img/sigimg.png' data-image-whitelisted=''";
                        strMSG += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                        strMSG += "</div>";
                        strMSG += "</div>";
                        strMSG += "</div>";
                        strMSG += "</body>";
                        strMSG += "</html>";


                        var tEmail = new Thread(() => EmailHelper.SendEmail(toEmail, "Incident Report Successfully Submitted to Cam Industrial", attachmentFiles, strMSG, null));
                        tEmail.Start();
                    }

                    string strMSGToCam = "";
                    strMSGToCam += "<html>";
                    strMSGToCam += "<head>";
                    strMSGToCam += "<style>";
                    strMSGToCam += "p{margin:0px; font-family: Verdana, sans-serif; font-size: 10pt;}";
                    strMSGToCam += "ul {margin: 0 0 0 20px; padding: 0;}";
                    strMSGToCam += "</style>";
                    strMSGToCam += "</head>";
                    strMSGToCam += "<body>";
                    strMSGToCam += "<div style='width: 800px; padding: 10px; font-family: Verdana, sans-serif; font-size: 10pt; color: #333;'>";

                    strMSGToCam += "<p>Hi Team,</p>";
                    strMSGToCam += "<br/>";
                    strMSGToCam += "<p>A new incident report has been submitted through the Customer Portal. Please review the details below and initiate follow-up with the customer <b>as soon as possible.</b></p>";
                    strMSGToCam += "<hr style='border: 1px solid #ccc;' />";
                    strMSGToCam += "<p><b>Incident Details:</b></p>";
                    strMSGToCam += "<ul>";
                    strMSGToCam += "<li><b>Submitted By:</b> " + customerName + "</li>";
                    strMSGToCam += "<li><b>Business Name:</b> " + businessName + "</li>";
                    strMSGToCam += "<li><b>Email:</b> " + cust.CustomerEmail + "</li>";
                    strMSGToCam += "<li><b>Phone Number:</b> " + cust.CustomerPhone + "</li>";
                    strMSGToCam += "<li><b>Incident Address:</b> " + incidentAddress + "</li>";
                    strMSGToCam += "<li><b>Date of Incident:</b> " + incidentDate.ToString("MMMM dd, yyyy") + "</li>";
                    strMSGToCam += "<li><b>Incident Number:</b> " + incidentNumber + "</li>";
                    strMSGToCam += "</ul>";

                    strMSGToCam += "<p><b>Racking Information:</b></p>";
                    strMSGToCam += "<ul>";
                    strMSGToCam += "<li><b>Type of Rack:</b> " + rackType + "</li>";
                    strMSGToCam += "<li><b>Location of Incident (within facility):</b> " + locationDescription + "</li>";
                    strMSGToCam += "</ul>";

                    strMSGToCam += "<p><b>Incident Summary:</b></p>";
                    strMSGToCam += "<p style='white-space: pre-wrap;'>" + incidentSummary + "</p>";

                    if (attachedPhotos != null)
                    {
                        strMSGToCam += "<p><b>Photos Submitted:</b></p>";
                        strMSGToCam += $"<p>{(attachedPhotos.Count > 0 ? string.Join("<br/>", attachedPhotos) : "Photos attached to this email.")}</p>";
                    }

                    strMSGToCam += "<br/>";
                    strMSGToCam += "<p>Please take appropriate action at your earliest convenience.</p>";
                    strMSGToCam += "<br/>";
                    strMSGToCam += "<div><div>";
                    strMSGToCam += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7b7b7b' lang='EN-US'>Thanks,</span></p>";
                    strMSGToCam += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>Bhavik Trivedi </span></b>";
                    strMSGToCam += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>P.Eng, M.Tech, PMP</span></p>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Engineering Manager</span></b></p>";
                    strMSGToCam += "<br/>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>cam|</span></b><b>";
                    strMSGToCam += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>industrial</span></b></p>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>20 7095 64 Street SE |";
                    strMSGToCam += "</span></b><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>Calgary, AB, T2C 5C3</span></b></p>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSGToCam += "<br/>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>E ~ &nbsp;</span></b><b>";
                    strMSGToCam += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#454545' lang='EN-US'>";
                    strMSGToCam += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank'><span lang='EN-US'>b.trivedi@camindustrial.net</span></a></span></b></p>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>C ~</span></b><b>";
                    strMSGToCam += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 690-2976</span></b></p>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>D ~</span></b><b>";
                    strMSGToCam += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'> (587) 355-1346</span></b></p>";
                    strMSGToCam += "<p><b><span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>F ~</span></b><b>";
                    strMSGToCam += "<span style='font-size:8.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#7f7d7e' lang='EN-US'>(403) 720-7074</span></b></p>";
                    strMSGToCam += "<p><b><span style='font-size:9.0pt;font-family:&quot;Verdana&quot;,sans-serif;color:#005aab' lang='EN-US'>&nbsp;</span></b></p>";
                    strMSGToCam += "<p><span><img style='width:2.618in;height:.6458in' ";
                    strMSGToCam += " src='https://rack-manager.com/img/sigimg.png'  data-image-whitelisted=''";
                    strMSGToCam += "class='CToWUd' data-bit='iit' width='251' height='62' border='0'></span></p>";
                    strMSGToCam += "</div>";
                    strMSGToCam += "</div>";
                    strMSGToCam += "</div>";
                    strMSGToCam += "</body>";
                    strMSGToCam += "</html>";

                    var tEmail1 = new Thread(() => EmailHelper.SendEmail("b.trivedi@camindustrial.net", "New Incident Report Submitted by " + businessName + "– Immediate Follow-Up Required", attachmentFiles, strMSGToCam, null));
                    tEmail1.Start();


                    return incident.IncidentReportId.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "Error: " + ex.Message;
                }
            }
        }
        internal static IncidentReportViewModel GetIncidentReportById(long id)
        {
            string tmpURL = HttpContext.Current.Request.Url.AbsoluteUri;
            Uri url = new Uri(tmpURL);
            string host = url.GetLeftPart(UriPartial.Authority);
            using (DatabaseEntities db = new DatabaseEntities())
            {
                var report = (from ir in db.IncidentReports
                              join cl in db.CustomerLocations on ir.CustomerLocationId equals cl.CustomerLocationID
                              join c in db.Customers on ir.CustomerId equals c.CustomerId
                              join city in db.Cities on cl.CityID equals city.CityID into cityJoin
                              from city in cityJoin.DefaultIfEmpty()
                              join prov in db.Provinces on cl.ProvinceID equals prov.ProvinceID into provJoin
                              from prov in provJoin.DefaultIfEmpty()
                              where ir.IncidentReportId == id
                              select new IncidentReportViewModel
                              {
                                  IncidentReportId = ir.IncidentReportId,
                                  BusinessName = c.CustomerName,
                                  LogoUrl = !string.IsNullOrEmpty(c.CustomerLogo)
                      ? host + "/img/logos/" + c.CustomerLogo.Trim()
                      : host + "/img/logos/defaultcompany.png",
                                  LocationName = cl.LocationName,
                                  Address = cl.CustomerAddress,
                                  CityName = city != null ? city.CityName : string.Empty,
                                  ProvinceName = prov != null ? prov.ProvinceName : string.Empty,
                                  Region = cl.Region,
                                  RackType = ir.IncidentType, // Assuming you have RackType in CustomerLocation
                                  Area = ir.IncidentArea,
                                  Row = ir.IncidentRow,
                                  Aisle = ir.IncidentAisle,
                                  Bay = ir.IncidentBay,
                                  Level = ir.IncidentLevel,
                                  BeamLocation = ir.IncidentBeamLocation,
                                  FrameSide = ir.IncidentFrameSide,
                                  ReportedBy = ir.IncidentReportedBy,
                                  Summary = ir.IncidentSummary,
                                  IncidentDate = ir.IncidentDate,
                                  IncidentNumber = ir.IncidentNumber,
                                  Photos = db.IncidentReportPhotoes
                                             .Where(p => p.IncidentReportId == ir.IncidentReportId)
                                             .Select(p => new IncidentReportPhotoViewModel
                                             {
                                                 IncidentPhotoPath = p.IncidentPhotoPath
                                             }).ToList()
                              }).FirstOrDefault();

                return report;
            }
        }


    }
}



//if (list.FacilitiesAreasIds.Length != 0)
//{
//    var fAreaName = "";
//    string[] items = list.FacilitiesAreasIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
//    foreach (var f in items)
//    {
//        if (f != null)
//        {
//            int fId = Convert.ToInt16(f);
//            var fac = db.FacilitiesAreas.Where(x => x.FacilitiesAreaId == fId && x.IsActive == true).FirstOrDefault();
//            if (fac != null)
//            {
//                fAreaName = String.Join(",", fac.FacilitiesAreaName);

//            }
//        }
//    }
//    _list.FacilitiesAreas = fAreaName;
//}
//if (list.ProcessOverviewIds.Length != 0)
//{
//    var pOverName = "";
//    string[] overview = list.ProcessOverviewIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
//    foreach (var h in overview)
//    {
//        if (h != null)
//        {
//            int pId = Convert.ToInt16(h);
//            var pro = db.ProcessOverviews.Where(x => x.ProcessOverviewId == pId && x.IsActive == true).FirstOrDefault();
//            if (pro != null)
//            {
//                pOverName = String.Join(",", pro.ProcessOverviewDesc);
//            }
//        }
//    }
//    _list.ProcessOverviews = pOverName;
//}


//    - Fix issue of city list updated acording province while update in profile.
//- Fix issue of change province list if user change country from dropdown.
//- If user change province from dropdown then dropdown list of city will be updated according selected new province task done.
//- Show error messege if city filled and it's province empty done.
//- Display every information in dashbord.