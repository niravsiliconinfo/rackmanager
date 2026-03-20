using CamV4.Helper;
using CamV4.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace CamV4.Controllers
{
    [RoutePrefix("api/pageview")]
    public class PageViewController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        //Mobile Apis
        #region "Login/User/Customers/Employee"
        [Route("mobileLogin")]
        [HttpPost]
        public async Task<UserEmployeeViewModel> MobileLogin(LoginViewModel model)
        {
            var user = DatabaseHelper.mobilelogin(model);
            return user;
        }

        //var details = DatabaseHelper.getAllCustomers();
        //return details;

        //[System.Web.Http.Authorize]
        //[Route("getAllCustomers")]
        //[HttpGet]
        //public async Task<IHttpActionResult> GetAllCustomer()
        //{
        //    //var identity = (ClaimsIdentity)User.Identity;

        //    //var userId = identity
        //    //    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    //if (userId == null)
        //    //    return Unauthorized();

        //    //var details = await Task.Run(() =>
        //    //    DatabaseHelper.getAllCustomers());

        //    var details = DatabaseHelper.getAllCustomers();
        //    return details;

        //    //return Ok(details);
        //}

        [Route("mobileSignup")]
        [HttpPost]
        public async Task<string> MobileSignup(UserEmployeeViewModel model)
        {
            var user = DatabaseHelper.saveUserEmployee(model);
            return user;
        }

        [Route("getAllEmployee")]
        [HttpGet]
        public async Task<List<EmployeeViewModel>> GetAllEmployee()
        {
            List<EmployeeViewModel> details = DatabaseHelper.getAllEmployee();
            return details;
        }

        [Route("getAllEngineerEmployee")]
        [HttpGet]
        public async Task<List<EmployeeViewModel>> GetAllEngineerEmployee()
        {
            List<EmployeeViewModel> lstEmployeeEngineer = DatabaseHelper.GetAllEngineerEmployee();
            return lstEmployeeEngineer;
        }

        [Route("getAllStampingEmployee")]
        [HttpGet]
        public async Task<List<EmployeeViewModel>> GetAllStampingEmployee()
        {
            List<EmployeeViewModel> details = DatabaseHelper.GetAllStampingEmployee();
            return details;
        }

        [Route("getAllSalesRep")]
        [HttpGet]
        public async Task<List<EmployeeSalesViewModel>> getAllSalesRep()
        {
            List<EmployeeSalesViewModel> lstEmployeeEngineer = DatabaseHelper.GetAllSalesRep();
            return lstEmployeeEngineer;
        }

        [Route("getAllSalesPerson")]
        [HttpGet]
        public async Task<List<EmployeeSalesViewModel>> GetAllSalesPerson()
        {
            List<EmployeeSalesViewModel> details = DatabaseHelper.GetAllSalesPerson();
            return details;
        }

        [Route("getComponentItemDetails")]
        [HttpGet]
        public async Task<ComponentPriceList> GetComponentItemDetails(string ItemPartNo)
        {
            ComponentPriceList objComponentPriceList = new ComponentPriceList();
            ComponentPriceList details = DatabaseHelper.GetComponentItemDetails(ItemPartNo);
            return details;

        }

        [Route("getComponentItemDetailsByDescription")]
        [HttpGet]
        public async Task<ComponentPriceList> GetComponentItemDetailsByDescription(string ComponentPriceDescription)
        {
            ComponentPriceList objComponentPriceList = new ComponentPriceList();
            ComponentPriceList details = DatabaseHelper.GetComponentItemDetailsByDescription(ComponentPriceDescription);
            return details;

        }

        [Route("getAllEmployeeUser")]
        [HttpGet]
        public async Task<List<UserEmployeeViewModel>> GetAllEmployeeUser()
        {
            List<UserEmployeeViewModel> details = DatabaseHelper.getAllEmployeeUser();
            return details;
        }

        //[Route("getAllAdminCountInUser")]
        //[HttpGet]
        //public async Task<int> GetAllAdminCountInUser()
        //{
        //    var details = DatabaseHelper.getAllAdminCountInUser();
        //    return details;
        //}

        //[Route("getInventoryCount")]
        //[HttpGet]
        //public async Task<int> getInventoryCount()
        //{
        //    var details = DatabaseHelper.getInventoryCount();
        //    return details;
        //}



        //[Route("getAllEmployeeCountInUser")]
        //[HttpGet]
        //public async Task<int> GetAllEmployeeCountInUser()
        //{
        //    var details = DatabaseHelper.getAllEmployeeCountInUser();
        //    return details;
        //}

        [Route("getUserEmployeeById")]
        [HttpGet]
        public async Task<UserEmployeeViewModel> GetUserEmployeeById(int id)
        {
            UserEmployeeViewModel details = DatabaseHelper.getUserEmployeeById(id);
            return details;
        }

        [Route("getUserEmployeeDetailsById")]
        [HttpGet]
        public async Task<UserEmployeeViewModel> GetUserEmployeeDetailsById(int id)
        {
            UserEmployeeViewModel details = DatabaseHelper.getUserEmployeeDetailsById(id);
            return details;
        }

        [Route("getAllUserbyUserId")]
        [HttpGet]
        public async Task<List<User>> GetAllUserbyUserId(int id)
        {
            var details = DatabaseHelper.getAllUserbyUserId(id);
            return details;
        }

        [Route("editUserDeviceToken")]
        [HttpPost]
        public async Task<string> EditUserDeviceToken(User model)
        {
            var details = DatabaseHelper.editUserDeviceToken(model);
            return "Ok";
        }

        [Route("saveUserEmployee")]
        [HttpPost]
        public async Task<string> SaveUserEmployee(UserEmployeeViewModel model)
        {
            var details = DatabaseHelper.saveUserEmployee(model);
            return "Ok";
        }

        //MyProfile
        [Route("editUserEmployee")]
        [HttpPost]
        public async Task<string> EditUserEmployee(UserEmployeeViewModel model)
        {
            var details = DatabaseHelper.editUserEmployee(model);
            return "Ok";
        }

        [Route("empEditMyProfile")]
        [HttpPost]
        public async Task<string> EmpEditMyProfile(UserEmployeeViewModel model)
        {
            var details = DatabaseHelper.empEditMyProfile(model);
            return "Ok";
        }

        [Route("removeUserEmployee")]
        [HttpPost]
        public async Task<string> RemoveUserEmployee(int id)
        {
            var details = DatabaseHelper.removeUserEmployee(id);
            return details;
        }

        [Route("removeUserEmployeemobile")]
        [HttpPost]
        public async Task<string> RemoveUserEmployeemobile(User model)
        {
            var details = DatabaseHelper.removeUserEmployee(model.UserId);
            return details;
        }

        [Route("editPassword")]
        [HttpPost]
        public async Task<string> EditPassword(User model)
        {
            var details = DatabaseHelper.editPassword(model);
            return "Ok";
        }

        [Route("editEmployeePasswordByAdmin")]
        [HttpPost]
        public async Task<string> EditEmployeePasswordByAdmin(UserEmployeeViewModel model)
        {
            var details = DatabaseHelper.editEmployeePasswordByAdmin(model);
            return "Ok";
        }

        [Route("editCustomerPasswordByAdmin")]
        [HttpPost]
        public async Task<string> EditCustomerPasswordByAdmin(User model)
        {
            var details = DatabaseHelper.editCustomerPasswordByAdmin(model);
            return "Ok";
        }

        #endregion


        #region "Common Masters"

        [Route("getAllInspectionStatus")]
        [HttpGet]
        public async Task<List<InspectionStatu>> GetAllInspectionStatus()
        {
            var details = DatabaseHelper.GetAllInspectionStatus();
            return details;
        }

        [Route("getAllCities")]
        [HttpGet]
        public async Task<List<CityViewModel>> GetAllCities()
        {
            var details = DatabaseHelper.getAllCities();
            return details;
        }

        [Route("getCitybyId")]
        [HttpGet]
        public async Task<City> GetCitybyId(int? id)
        {
            var details = DatabaseHelper.getCitybyId(id);
            return details;
        }

        [Route("getCitybyProvinceId")]
        [HttpGet]
        public async Task<List<City>> GetCitybyProvinceId(int? id)
        {
            var details = DatabaseHelper.getCitybyProvinceId(id);
            return details;
        }

        [Route("getCitybyProvinceIdByCustomer")]
        [HttpGet]
        public async Task<List<City>> GetCitybyProvinceIdByCustomer(int? id)
        {
            var details = DatabaseHelper.GetCitybyProvinceIdByCustomer(id);
            return details;
        }

        [Route("getCitybyByCustomer")]
        [HttpGet]
        public async Task<List<City>> GetCitybyByCustomer()
        {
            var details = DatabaseHelper.GetCitybyByCustomer();
            return details;
        }


        [Route("getLocationbyCityIdByCustomer")]
        [HttpGet]
        public async Task<List<CustomerLocation>> GetLocationbyCityIdByCustomer(long? id)
        {
            var details = DatabaseHelper.GetLocationbyCityIdByCustomer(id);
            return details;
        }

        [Route("getLocationByCustomer")]
        [HttpGet]
        public async Task<List<CustomerLocation>> getLocationByCustomer()
        {
            var details = DatabaseHelper.GetLocationByCustomer();
            return details;
        }

        [Route("getFacilityByCustomer")]
        [HttpGet]
        public async Task<List<CustomerFacility>> GetFacilityByCustomer()
        {
            var details = DatabaseHelper.GetFacilityByCustomer();
            return details;
        }

        [Route("getAreaByCustomer")]
        [HttpGet]
        public async Task<List<CustomerArea>> GetAreaByCustomer()
        {
            var details = DatabaseHelper.GetAreaByCustomer();
            return details;
        }

        [Route("getLocationbyRegionByCustomer")]
        [HttpGet]
        public async Task<List<CustomerLocation>> GetLocationbyRegionByCustomer(string region)
        {
            var details = DatabaseHelper.GetLocationbyRegionByCustomer(region);
            return details;
        }

        [Route("getAllProvince")]
        [HttpGet]
        public async Task<List<Province>> GetAllProvince()
        {
            var details = DatabaseHelper.getAllProvinces();
            return details;
        }

        [Route("getProvincebyId")]
        [HttpGet]
        public async Task<Province> GetProvincebyId(int? id)
        {
            var details = DatabaseHelper.getProvincebyId(id);
            return details;
        }

        [Route("getProvincebyCountryId")]
        [HttpGet]
        public async Task<List<Province>> GetProvincebyCountryId(int? id)
        {
            var details = DatabaseHelper.getProvincebyCountryId(id);
            return details;
        }

        [Route("getProvincebyCountryIdByCustomer")]
        [HttpGet]
        public async Task<List<Province>> GetProvincebyCountryIdByCustomer()
        {
            var details = DatabaseHelper.GetProvincebyCountryIdByCustomer();
            return details;
        }

        [Route("getRegionbyCustomer")]
        [HttpGet]
        public async Task<List<CustomerRegion>> GetRegionbyCustomer()
        {
            var details = DatabaseHelper.GetRegionbyCustomer();
            return details;
        }

        [Route("getAllCountries")]
        [HttpGet]
        public async Task<List<Country>> GetAllCountries()
        {
            var details = DatabaseHelper.getAllCountries();
            return details;
        }

        [Route("getCountrybyId")]
        [HttpGet]
        public async Task<Country> GetCountrybyId(int? id)
        {
            var details = DatabaseHelper.getCountrybyId(id);
            return details;
        }

        [Route("getAllCustomersByUserType")]
        [HttpGet]
        public async Task<List<User>> GetAllCustomersByUserType()
        {
            var details = DatabaseHelper.getAllCustomersByUserType();
            return details;
        }

        [Route("getAllCustomers")]
        [HttpGet]
        public async Task<List<CustomerViewModel>> GetAllCustomer()
        {
            var details = DatabaseHelper.getAllCustomers();
            return details;
        }

        [Route("getAllCustomersByEmployeeId")]
        [HttpGet]
        public async Task<List<CustomerViewModel>> GetAllCustomersByEmployeeId()
        {
            var details = DatabaseHelper.getAllCustomersByEmployeeId();
            return details;
        }

        [Route("getCustomerById")]
        [HttpGet]
        public async Task<Customer> GetCustomerById(int id)
        {
            var details = DatabaseHelper.getCustomerById(id);
            return details;
        }

        [Route("getCustomerDetailsById")]
        [HttpGet]
        public async Task<CustomerViewModel> GetCustomerDetailsById(int id)
        {
            var details = DatabaseHelper.getCustomerDetailsById(id);
            return details;
        }

        [Route("getCustAndCustLocationDetailsById")]
        [HttpGet]
        public async Task<CustAndCustomerLocationViewModel> GetCustAndCustLocationDetailsById(int id)
        {
            var details = DatabaseHelper.getCustAndCustLocationDetailsById(id);
            return details;
        }

        [Route("saveCustomer")]
        [HttpPost]
        public async Task<string> SaveCustomer(Customer model)
        {
            var details = DatabaseHelper.saveCustomer(model);
            return details;
        }

        [Route("editCustomer")]
        [HttpPost]
        public async Task<string> EditCustomer(Customer model)
        {
            var details = DatabaseHelper.editCustomer(model);
            return details;
        }

        [Route("sendPassword")]
        [HttpPost]
        public async Task<string> SendPassword(int id)
        {
            var details = DatabaseHelper.sendPassword(id);
            return details;
        }

        [Route("removeCustomer")]
        [HttpPost]
        public async Task<string> RemoveCustomer(int id)
        {
            var details = DatabaseHelper.removeCustomer(id);
            return details;
        }

        [Route("getAllCustomerLocations")]
        [HttpGet]
        public async Task<List<CustomerLocationViewModel>> GetAllCustomerLocations()
        {
            var details = DatabaseHelper.getAllCustomerLocations();
            return details;
        }

        [Route("getCustomerLocationById")]
        [HttpGet]
        public async Task<CustomerLocation> GetCustomerLocationById(int id)
        {
            var details = DatabaseHelper.getCustomerLocationById(id);
            return details;
        }

        [Route("getCustomerLocationDetailsById")]
        [HttpGet]
        public async Task<CustomerLocationViewModel> GetCustomerLocationDetailsById(int id)
        {
            var details = DatabaseHelper.getCustomerLocationDetailsById(id);
            return details;
        }


        #region "For Customer user with location facility and area"

        // ---- 1. Load contact grid ----
        // JS: /api/pageview/getLocationContactDetailsByCustomerId?CustomerId=123
        [Route("getLocationContactDetailsByCustomerId")]
        [HttpGet]
        public List<CustomerLocationContactViewModel> GetLocationContactDetailsByCustomerId(long CustomerId)
        {
            return DatabaseHelper.GetLocationContactDetailsByCustomerId(CustomerId);
        }

        // ---- 2. Load single contact for edit ----
        // JS: /api/pageview/getLocationContactUserDetailsById?id=456
        [Route("getLocationContactUserDetailsById")]
        [HttpGet]
        public CustomerLocationContactViewModel GetLocationContactUserDetailsById(long id)
        {
            return DatabaseHelper.getLocationContactUserDetailsById(id);
        }

        // ---- 3. Save new contact (Add form) ----
        // JS: POST /api/pageview/saveLocationContactMultiple
        [Route("saveLocationContactMultiple")]
        [HttpPost]
        public async Task<string> SaveLocationContactMultiple(CustomerLocationContactViewModel model)
        {
            return DatabaseHelper.saveLocationContactMultiple(model);
        }

        // ---- 4. Update existing contact (Edit form) ----
        // JS: POST /api/pageview/editLocationContactMultiple
        [Route("editLocationContactMultiple")]
        [HttpPost]
        public async Task<string> EditLocationContactMultiple(CustomerLocationContactViewModel model)
        {
            return DatabaseHelper.editLocationContactMultiple(model);
        }

        // ---- 5. Soft delete contact ----
        // JS: POST /api/pageview/removeLocationContact?id=456
        [Route("removeLocationContact")]
        [HttpPost]
        public string RemoveLocationContact(long id)
        {
            return DatabaseHelper.removeLocationContact(id);
        }

        // ---- 6. Load location checklist for Add/Edit forms ----
        // JS: /api/pageview/getCustomerLocationByCustomerId?id=123
        // Already exists — confirm it returns LocationName, CustomerLocationID, City fields
        [Route("getCustomerLocationByCustomerId")]
        [HttpGet]
        public List<CustomerLocationViewModel> GetCustomerLocationByCustomerId(long id)
        {
            return DatabaseHelper.getCustomerLocationByCustomerId(id);
        }

        // ---- 7. Load facility checklist for Add/Edit forms ----
        // JS: /api/pageview/getCustomerFacilityByCustomerId?id=123
        [Route("getCustomerFacilityByCustomerId")]
        [HttpGet]
        public List<CustomerFacilityViewModel> GetCustomerFacilityByCustomerId(long id)
        {
            return DatabaseHelper.getCustomerFacilityByCustomerId(id);
        }

        // ---- 8. Load area checklist for Add/Edit forms ----
        // JS: /api/pageview/getCustomerAreaByCustomerId?id=123
        [Route("getCustomerAreaByCustomerId")]
        [HttpGet]
        public List<CustomerAreaViewModel> GetCustomerAreaByCustomerId(long id)
        {
            return DatabaseHelper.getCustomerAreaByCustomerId(id);
        }

        #endregion

        //[Route("getCustomerLocationByCustomerId")]
        //[HttpGet]
        //public async Task<List<CustomerLocationViewModel>> GetCustomerLocationByCustomerId(int id)
        //{
        //    var details = DatabaseHelper.getCustomerLocationByCustomerId(id);
        //    return details;
        //}

        //[Route("getCustomerFacilityByCustomerId")]
        //[HttpGet]
        //public async Task<List<CustomerFacilityViewModel>> GetCustomerFacilityByCustomerId(int id)
        //{
        //    var details = DatabaseHelper.getCustomerFacilityByCustomerId(id);
        //    return details;
        //}

        //[Route("getCustomerAreaByCustomerId")]
        //[HttpGet]
        //public async Task<List<CustomerAreaViewModel>> GetCustomerAreaByCustomerId(int id)
        //{
        //    var details = DatabaseHelper.getCustomerAreaByCustomerId(id);
        //    return details;
        //}


        [Route("getCustomerLocationByCustomerIdCustomer")]
        [HttpGet]
        public async Task<List<CustomerLocationViewModel>> GetCustomerLocationByCustomerId()
        {
            var details = DatabaseHelper.getCustomerLocationByCustomerId();
            return details;
        }

        [Route("getCustomerLocationByUserId")]
        [HttpGet]
        public async Task<List<CustomerLocationViewModel>> GetCustomerLocationByUserId()
        {
            var details = DatabaseHelper.getCustomerLocationByUserId();
            return details;
        }

        //[Route("saveCustomerLocation")]
        //[HttpPost]
        //public async Task<string> SaveCustomerLocation(CustomerLocation model)
        //{
        //    var details = DatabaseHelper.saveCustomerLocation(model);
        //    return details;
        //}

        [Route("editCustomerLocation")]
        [HttpPost]
        public async Task<string> EditCustomerLocation(CustomerLocation model)
        {
            var details = DatabaseHelper.editCustomerLocation(model);
            return details;
        }

        [Route("removeCustomerLocation")]
        [HttpPost]
        public async Task<string> RemoveCustomerLocation(int id)
        {
            var details = DatabaseHelper.removeCustomerLocation(id);
            return details;
        }

        [Route("getAllCustomerArea")]
        [HttpGet]
        public async Task<List<CustomerArea>> GetAllCustomerArea()
        {
            var details = DatabaseHelper.getAllCustomerArea();
            return details;
        }

        [Route("getAreaDetailsById")]
        [HttpGet]
        public async Task<CustomerArea> GetAreaDetailsById(int id)
        {
            var details = DatabaseHelper.getAreaDetailsById(id);
            return details;
        }

        [Route("getAreaDetailsByLocationId")]
        [HttpGet]
        public async Task<List<CustomerAreaViewModel>> GetAreaDetailsByLocationId(int id)
        {
            var details = DatabaseHelper.getAreaDetailsByLocationId(id);
            return details;
        }

        [Route("getFacilityByLocationId")]
        [HttpGet]
        public async Task<List<CustomerFacilityViewModel>> GetFacilityByLocationId(int id)
        {
            var details = DatabaseHelper.getFacilityDetailsByLocationId(id);
            return details;
        }


        //[Route("saveCustomerArea")]
        //[HttpPost]
        //public async Task<long> SaveCustomerArea(CustomerArea model)
        //{
        //    var details = DatabaseHelper.saveCustomerArea(model);
        //    return details;
        //}

        [Route("editCustomerArea")]
        [HttpPost]
        public async Task<int> editCustomerArea(CustomerArea model)
        {
            var details = DatabaseHelper.editCustomerArea(model);
            return details;
        }

        [Route("removeCustomerArea")]
        [HttpPost]
        public async Task<long> RemoveCustomerArea(int id)
        {
            var details = DatabaseHelper.removeCustomerArea(id);
            return details;
        }

        [Route("getLocationContactDetailsById")]
        [HttpGet]
        public async Task<CustomerLocationContact> GetLocationContactDetailsById(int id)
        {
            var details = DatabaseHelper.getLocationContactDetailsById(id);
            return details;
        }


        //[Route("getLocationContactUserDetailsById")]
        //[HttpGet]
        //public async Task<CustomerLocationContactViewModel> GetLocationContactUserDetailsById(long id)
        //{
        //    var details = DatabaseHelper.getLocationContactUserDetailsById(id);
        //    return details;
        //}


        [Route("getLocationContactDetailsByUserId")]
        [HttpGet]
        public async Task<CustomerLocationContact> GetLocationContactDetailsByUserId()
        {
            var details = DatabaseHelper.getLocationContactDetailsByUserId();
            return details;
        }

        [Route("getLocationContactDetailsByLocationId")]
        [HttpGet]
        public List<CustomerLocationContactViewModel> GetLocationContactDetailsByLocationId(string CustomerLocationId)
        {
            var cLId = Convert.ToInt64(CustomerLocationId);
            List<CustomerLocationContactViewModel> customerlocationcontactlist = DatabaseHelper.getLocationContactDetailsByLocationId(cLId);
            return customerlocationcontactlist;
        }

        //[Route("getLocationContactDetailsByCustomerId")]
        //[HttpGet]
        //public List<CustomerLocationContactViewModel> GetLocationContactDetailsByCustomerId(string CustomerId)
        //{
        //    var cLId = Convert.ToInt64(CustomerId);
        //    List<CustomerLocationContactViewModel> customerlocationcontactlist = DatabaseHelper.GetLocationContactDetailsByCustomerId(cLId);
        //    return customerlocationcontactlist;
        //}

        [Route("getLocationContactDetailsByLocationIdCustomer")]
        [HttpGet]
        public List<CustomerLocationContactViewModel> GetLocationContactDetailsByLocationId()
        {
            List<CustomerLocationContactViewModel> customerlocationcontactlist = DatabaseHelper.getLocationContactDetailsByLocationId();
            return customerlocationcontactlist;
        }

        [Route("getLocationContactByCustomer")]
        [HttpGet]
        public List<CustomerLocationContactViewModel> GetLocationContactByCustomer()
        {
            List<CustomerLocationContactViewModel> customerlocationcontactlist = DatabaseHelper.GetLocationContactByCustomer();
            return customerlocationcontactlist;
        }

        [Route("saveLocationContact")]
        [HttpPost]
        public async Task<string> SaveLocationContact(CustomerLocationContactViewModel model)
        {
            var details = DatabaseHelper.saveLocationContact(model);
            return details;
        }

        [Route("saveNewLocationContactMobile")]
        [HttpPost]
        public List<CustomerLocationContactViewModel> saveNewLocationContactMobile(CustomerLocationContactViewModel model)
        {
            var details = DatabaseHelper.saveNewLocationContactMobile(model);
            return details;
        }

        //[Route("saveLocationContactMultiple")]
        //[HttpPost]
        //public async Task<string> SaveLocationContactMultiple(CustomerLocationContactViewModel model)
        //{
        //    var details = DatabaseHelper.saveLocationContactMultiple(model);
        //    return details;
        //}

        //[Route("editLocationContactMultiple")]
        //[HttpPost]
        //public async Task<string> EditLocationContactMultiple(CustomerLocationContactViewModel model)
        //{
        //    var details = DatabaseHelper.editLocationContactMultiple(model);
        //    return details;
        //}

        [Route("editLocationContact")]
        [HttpPost]
        public async Task<int> EditLocationContact(CustomerLocationContact model)
        {
            var details = DatabaseHelper.editLocationContact(model);
            return details;
        }

        //[Route("removeLocationContact")]
        //[HttpPost]
        //public async Task<string> RemoveLocationContact(int id)
        //{
        //    var details = DatabaseHelper.removeLocationContact(id);
        //    return details;
        //}

        [Route("getFacilityDetailsByLocationId")]
        [HttpGet]
        public async Task<List<CustomerFacilityViewModel>> GetFacilityDetailsByLocationId(int id)
        {
            var details = DatabaseHelper.getFacilityDetailsByLocationId(id);
            return details;
        }

        //[Route("saveCustomerFacility")]
        //[HttpPost]
        //public async Task<long> SaveCustomerFacility(CustomerFacility model)
        //{
        //    var details = DatabaseHelper.saveCustomerFacility(model);
        //    return details;
        //}

        [Route("editCustomerFacility")]
        [HttpPost]
        public async Task<int> editCustomerFacility(CustomerFacility model)
        {
            var details = DatabaseHelper.editCustomerFacility(model);
            return details;
        }

        [Route("removeCustomerFacility")]
        [HttpPost]
        public async Task<long> RemoveCustomerFacility(int id)
        {
            var details = DatabaseHelper.removeCustomerFacility(id);
            return details;
        }

        [Route("getFacilityDetailsById")]
        [HttpGet]
        public async Task<CustomerFacility> getFacilityDetailsById(int id)
        {
            var details = DatabaseHelper.getFacilityDetailsById(id);
            return details;
        }

        #endregion

        [Route("saveImages")]
        [HttpPost]
        public async Task<string> SaveImages(Images_Upload model)
        {
            var details = DatabaseHelper.saveImages(model);
            return "Ok";
        }


        #region "Customer - Location - Facility - Area Fucntions"

        [HttpGet]
        [Route("GetCustomerHierarchy")]
        public IHttpActionResult GetCustomerHierarchy(long customerId)
        {
            try
            {
                var result = new
                {
                    Locations = DatabaseHelper.GetCustomerLocations(customerId),
                    Facilities = DatabaseHelper.GetCustomerFacilities(customerId),
                    Areas = DatabaseHelper.GetCustomerAreas(customerId)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/pageview/SaveCustomerLocation
        [HttpPost]
        [Route("SaveCustomerLocation")]
        public IHttpActionResult SaveCustomerLocation(CustomerLocation model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.LocationName))
                    return BadRequest("LocationName is required.");

                string user = HttpContext.Current.Session["LoggedInUserId"]?.ToString() ?? "System";
                int result = DatabaseHelper.SaveCustomerLocation(model, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/pageview/DeleteCustomerLocation
        [HttpPost]
        [Route("DeleteCustomerLocation")]
        public IHttpActionResult DeleteCustomerLocation([FromBody] IdModel model)
        {
            try
            {
                string user = HttpContext.Current.Session["LoggedInUserId"]?.ToString() ?? "System";
                int result = DatabaseHelper.DeleteCustomerLocation(model.id, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/pageview/SaveCustomerFacility
        [HttpPost]
        [Route("SaveCustomerFacility")]
        public IHttpActionResult SaveCustomerFacility(CustomerFacility model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.FacilityName))
                    return BadRequest("FacilityName is required.");
                if (model.CustomerLocationID <= 0)
                    return BadRequest("CustomerLocationID is required.");

                string user = HttpContext.Current.Session["LoggedInUserId"]?.ToString() ?? "System";
                int result = DatabaseHelper.SaveCustomerFacility(model, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/pageview/DeleteCustomerFacility
        [HttpPost]
        [Route("DeleteCustomerFacility")]
        public IHttpActionResult DeleteCustomerFacility([FromBody] IdModel model)
        {
            try
            {
                string user = HttpContext.Current.Session["LoggedInUserId"]?.ToString() ?? "System";
                int result = DatabaseHelper.DeleteCustomerFacility(model.id, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/pageview/SaveCustomerArea
        [HttpPost]
        [Route("SaveCustomerArea")]
        public IHttpActionResult SaveCustomerArea(CustomerArea model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.AreaName))
                    return BadRequest("AreaName is required.");
                if (model.CustomerLocationID <= 0)
                    return BadRequest("CustomerLocationID is required.");

                string user = HttpContext.Current.Session["LoggedInUserId"]?.ToString() ?? "System";
                int result = DatabaseHelper.SaveCustomerArea(model, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/pageview/DeleteCustomerArea
        [HttpPost]
        [Route("DeleteCustomerArea")]
        public IHttpActionResult DeleteCustomerArea([FromBody] IdModel model)
        {
            try
            {
                string user = HttpContext.Current.Session["LoggedInUserId"]?.ToString() ?? "System";
                int result = DatabaseHelper.DeleteCustomerArea(model.id, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region "Inspection"

        [Route("GenerateInspectionDocumentNo")]
        [HttpGet]
        public async Task<string> GenerateInspectionDocumentNo(Int32 CustomerId, Int32 CustomerLocationId, string InspectionType = "")
        {
            if (InspectionType == "")
            {
                InspectionType = "RI";
            }
            var InspectionDocumentNo = DatabaseHelper.GenerateInspectionDocumentNo(CustomerId, CustomerLocationId, InspectionType);
            return InspectionDocumentNo;
        }




        [Route("getAllConclusionRecommendations")]
        [HttpGet]
        public async Task<List<ConclusionRecommendation>> GetAllConclusionRecommendations()
        {
            var details = DatabaseHelper.getAllConclusionRecommendations();
            return details;
        }

        [Route("getConclusionRecommendationsById")]
        [HttpPost]
        public async Task<ConclusionRecommendation> GetConclusionRecommendationsById(int id)
        {
            var details = DatabaseHelper.getConclusionRecommendationsById(id);
            return details;
        }

        [Route("saveConclusionRecommendations")]
        [HttpPost]
        public async Task<string> SaveConclusionRecommendations(ConclusionRecommendation model)
        {
            var details = DatabaseHelper.saveConclusionRecommendations(model);
            return details;
        }

        [Route("editConclusionRecommendations")]
        [HttpPost]
        public async Task<string> EditConclusionRecommendations(ConclusionRecommendation model)
        {
            var details = DatabaseHelper.editConclusionRecommendations(model);
            return details;
        }

        [Route("removeConclusionRecommendations")]
        [HttpPost]
        public async Task<string> RemoveConclusionRecommendations(int id)
        {
            var details = DatabaseHelper.removeConclusionRecommendations(id);
            return details;
        }

        [Route("getAllFacilitiesArea")]
        [HttpGet]
        public async Task<List<FacilitiesArea>> GetAllFacilitiesArea()
        {
            var details = DatabaseHelper.getAllFacilitiesArea();
            return details;
        }

        [Route("getFacilitiesAreaById")]
        [HttpGet]
        public async Task<FacilitiesArea> GetFacilitiesAreaById(int id)
        {
            var details = DatabaseHelper.getFacilitiesAreaById(id);
            return details;
        }

        [Route("saveFacilitiesArea")]
        [HttpPost]
        public async Task<string> SaveFacilitiesArea(FacilitiesArea model)
        {
            var details = DatabaseHelper.saveFacilitiesArea(model);
            return details;
        }

        [Route("editFacilitiesArea")]
        [HttpPost]
        public async Task<string> EditFacilitiesArea(FacilitiesArea model)
        {
            var details = DatabaseHelper.editFacilitiesArea(model);
            return details;
        }

        [Route("removeFacilitiesArea")]
        [HttpPost]
        public async Task<string> RemoveFacilitiesArea(int id)
        {
            var details = DatabaseHelper.removeFacilitiesArea(id);
            return details;
        }

        [Route("getAllProcessOverview")]
        [HttpGet]
        public async Task<List<ProcessOverview>> GetAllProcessOverview()
        {
            var details = DatabaseHelper.getAllProcessOverview();
            return details;
        }

        [Route("getProcessOverviewById")]
        [HttpGet]
        public async Task<ProcessOverview> GetProcessOverviewById(int id)
        {
            var details = DatabaseHelper.getProcessOverviewById(id);
            return details;
        }

        [Route("saveProcessOverview")]
        [HttpPost]
        public async Task<string> SaveProcessOverview(ProcessOverview model)
        {
            if (model == null || string.IsNullOrEmpty(model.ProcessOverviewDesc))
            {
                return "Invalid data";
            }

            model.IsShelvingChecklist = model.IsShelvingChecklist ?? false; // Ensure default value
            db.ProcessOverviews.Add(model);
            await db.SaveChangesAsync();
            return "Ok";
        }

        [Route("editProcessOverview")]
        [HttpPost]
        public async Task<string> EditProcessOverview(ProcessOverview model)
        {
            if (model == null || model.ProcessOverviewId <= 0)
            {
                return "Invalid data";
            }

            var existing = db.ProcessOverviews.Find(model.ProcessOverviewId);
            if (existing == null)
            {
                return "Not found";
            }

            existing.ProcessOverviewDesc = model.ProcessOverviewDesc;
            existing.IsShelvingChecklist = model.IsShelvingChecklist;
            await db.SaveChangesAsync();
            return "Ok";
        }

        [Route("removeProcessOverview")]
        [HttpPost]
        public async Task<string> RemoveProcessOverview(int id)
        {
            var details = DatabaseHelper.removeProcessOverview(id);
            return details;
        }

        [Route("getAllDeficiencySummary")]
        [HttpGet]
        public async Task<List<DeficiencySummary>> GetAllDeficiencySummary()
        {
            var details = DatabaseHelper.getAllDeficiencySummary();
            return details;
        }

        [Route("getDeficiencySummaryById")]
        [HttpGet]
        public async Task<DeficiencySummary> GetDeficiencySummaryById(int id)
        {
            var details = DatabaseHelper.getDeficiencySummaryById(id);
            return details;
        }

        [Route("saveDeficiencySummary")]
        [HttpPost]
        public async Task<string> SaveDeficiencySummary(DeficiencySummary model)
        {
            var details = DatabaseHelper.saveDeficiencySummary(model);
            return details;
        }

        [Route("editDeficiencySummary")]
        [HttpPost]
        public async Task<string> EditDeficiencySummary(DeficiencySummary model)
        {
            var details = DatabaseHelper.editDeficiencySummary(model);
            return details;
        }

        [Route("removeDeficiencySummary")]
        [HttpPost]
        public async Task<string> RemoveDeficiencySummary(int id)
        {
            var details = DatabaseHelper.removeDeficiencySummary(id);
            return details;
        }

        [Route("getAllDeficiencyCategory")]
        [HttpGet]
        public async Task<List<DeficiencyCategory>> GetAllDeficiencyCategory()
        {
            var details = DatabaseHelper.getAllDeficiencyCategory();
            return details;
        }

        [Route("getDeficiencyCategoryById")]
        [HttpGet]
        public async Task<DeficiencyCategory> GetDeficiencyCategoryById(int id)
        {
            var details = DatabaseHelper.getDeficiencyCategoryById(id);
            return details;
        }

        [Route("saveDeficiencyCategory")]
        [HttpPost]
        public async Task<string> SaveDeficiencyCategory(DeficiencyCategory model)
        {
            var details = DatabaseHelper.saveDeficiencyCategory(model);
            return details;
        }

        [Route("editDeficiencyCategory")]
        [HttpPost]
        public async Task<string> EditDeficiencyCategory(DeficiencyCategory model)
        {
            var details = DatabaseHelper.editDeficiencyCategory(model);
            return details;
        }

        [Route("removeDeficiencyCategory")]
        [HttpPost]
        public async Task<string> RemoveDeficiencyCategory(int id)
        {
            var details = DatabaseHelper.removeDeficiencyCategory(id);
            return details;
        }

        [Route("getAllDeficiency")]
        [HttpGet]
        public async Task<List<Deficiency>> GetAllDeficiency()
        {
            var details = DatabaseHelper.getAllDeficiency();
            return details;
        }

        [Route("getDeficiencyById")]
        [HttpGet]
        public async Task<Deficiency> GetDeficiencyById(int id)
        {
            var details = DatabaseHelper.getDeficiencyById(id);
            return details;
        }

        [Route("getComponentDescriptionById")]
        [HttpGet]
        public async Task<Deficiency> getComponentDescriptionById(int id)
        {
            var details = DatabaseHelper.getComponentDescriptionById(id);
            return details;
        }

        [Route("getDeficiencyByDefType")]
        [HttpGet]
        public async Task<List<Deficiency>> GetDeficiencyByDefType(string type)
        {
            var details = DatabaseHelper.getDeficiencyByDefType(type);
            return details;
        }

        [Route("saveDeficiency")]
        [HttpPost]
        public async Task<string> SaveDeficiency(Deficiency model)
        {
            var details = DatabaseHelper.saveDeficiency(model);
            return details;
        }

        [Route("editDeficiency")]
        [HttpPost]
        public async Task<string> EditDeficiency(Deficiency model)
        {
            var details = DatabaseHelper.editDeficiency(model);
            return details;
        }

        [Route("removeDeficiency")]
        [HttpPost]
        public async Task<string> RemoveDeficiency(int id)
        {
            var details = DatabaseHelper.removeDeficiency(id);
            return details;
        }

        [Route("getAllManufacturer")]
        [HttpGet]
        public async Task<List<Manufacturer>> GetAllManufacturer(int ManuType = 0)
        {
            var details = DatabaseHelper.GetAllManufacturer(ManuType);
            return details;
        }

        [Route("getAllManufacturerByComponent")]
        [HttpGet]
        public async Task<List<Manufacturer>> GetAllManufacturerByComponent(Int32 ComponentId)
        {
            var lstManufactures = DatabaseHelper.getAllManufacturerByComponent(ComponentId);
            return lstManufactures;

        }
        [Route("getManufacturerById")]
        [HttpGet]
        public async Task<Manufacturer> GetManufacturerById(int id)
        {
            var details = DatabaseHelper.getManufacturerById(id);
            return details;
        }

        [Route("saveManufacturer")]
        [HttpPost]
        public async Task<string> SaveManufacturer(Manufacturer model)
        {
            var details = DatabaseHelper.saveManufacturer(model);
            return details;
        }

        [Route("editManufacturer")]
        [HttpPost]
        public async Task<string> EditManufacturer(Manufacturer model)
        {
            var details = DatabaseHelper.editManufacturer(model);
            return details;
        }

        [Route("removeManufacturer")]
        [HttpPost]
        public async Task<string> RemoveManufacturer(int id)
        {
            var details = DatabaseHelper.removeManufacturer(id);
            return details;
        }

        [Route("getComponentsManufacturerById")]
        [HttpGet]
        public async Task<Component> GetComponentsManufacturerById(int id)
        {
            var details = DatabaseHelper.getComponentsManufacturerById(id);
            return details;
        }

        [Route("getAllComponent")]
        [HttpGet]
        public async Task<List<ComponentViewModel>> GetAllComponent()
        {
            var details = DatabaseHelper.getAllComponent();
            return details;
        }

        [Route("getAllComponentPrice")]
        [HttpGet]
        public async Task<List<ComponentPriceListViewModel>> getAllComponentPrice(long ComponentId)
        {
            var details = DatabaseHelper.getAllComponentPrice(ComponentId);
            return details;
        }

        [Route("getAllImportantsettings")]
        [HttpGet]
        public async Task<List<ImpSettingsViewModel>> getAllImportantSettings()
        {
            var details = DatabaseHelper.getAllImportantSettings();
            return details;
        }
        [Route("editComponentPrice")]
        [HttpPost]
        public async Task<string> EditComponentPrice(ComponentPriceList model)
        {
            var details = DatabaseHelper.EditComponentPrice(model);
            return details;
        }

        [Route("deleteComponentPrice")]
        [HttpPost]
        public async Task<string> DeleteComponentPrice(long id)
        {
            var details = DatabaseHelper.DeleteComponentPrice(id);
            return details;
        }

        [Route("getComponentById")]
        [HttpGet]
        public async Task<Component> GetComponentById(int id)
        {
            var details = DatabaseHelper.getComponentById(id);
            return details;
        }

        [Route("getComponentDetailsById")]
        [HttpGet]
        public async Task<ComponentViewModel> GetComponentDetailsById(int id)
        {
            var details = DatabaseHelper.getComponentDetailsById(id);
            return details;
        }

        [Route("saveComponent")]
        [HttpPost]
        public async Task<string> SaveComponent(Component model)
        {
            var details = DatabaseHelper.saveComponent(model);
            return details;
        }

        [Route("editComponent")]
        [HttpPost]
        public async Task<string> EditComponent(Component model)
        {
            var details = DatabaseHelper.editComponent(model);
            return details;
        }

        [Route("removeComponent")]
        [HttpPost]
        public async Task<string> RemoveComponent(int id)
        {
            var details = DatabaseHelper.removeComponent(id);
            return details;
        }



        [Route("getComponentPropertyTypeById")]
        [HttpGet]
        public async Task<ComponentPropertyTypeViewModel> GetComponentPropertyTypeById(int id)
        {
            var details = DatabaseHelper.getComponentPropertyTypeById(id);
            return details;
        }

        [Route("saveComponentPropertyType")]
        [HttpPost]
        public async Task<int> SaveComponentPropertyType(ComponentPropertyTypeViewModel model)
        {
            var details = DatabaseHelper.saveComponentPropertyType(model);
            return details;
        }

        [Route("editComponentPropertyType")]
        [HttpPost]
        public async Task<string> EditComponentPropertyType(ComponentPropertyTypeViewModel model)
        {
            var details = DatabaseHelper.editComponentPropertyType(model);
            return details;
        }

        [Route("removeComponentPropertyType")]
        [HttpPost]
        public async Task<string> RemoveComponentPropertyType(int id)
        {
            var details = DatabaseHelper.removeComponentPropertyType(id);
            return details;
        }

        [Route("getAllComponentPropertyValues")]
        [HttpGet]
        public async Task<List<ComponentPropertyValue>> GetAllComponentPropertyValues()
        {
            var details = DatabaseHelper.getAllComponentPropertyValues();
            return details;
        }

        [Route("getComponentPropertyValuesById")]
        [HttpGet]
        public async Task<ComponentPropertyValue> GetComponentPropertyValuesById(int id)
        {
            var details = DatabaseHelper.getComponentPropertyValuesById(id);
            return details;
        }

        [Route("saveComponentPropertyValues")]
        [HttpPost]
        public async Task<string> SaveComponentPropertyValues(ComponentPropertyValue model)
        {
            var details = DatabaseHelper.saveComponentPropertyValues(model);
            return details;
        }

        [Route("editComponentPropertyValues")]
        [HttpPost]
        public async Task<string> EditComponentPropertyValues(ComponentPropertyValue model)
        {
            var details = DatabaseHelper.editComponentPropertyValues(model);
            return details;
        }

        [Route("removeComponentPropertyValues")]
        [HttpPost]
        public async Task<string> RemoveComponentPropertyValues(int id)
        {
            var details = DatabaseHelper.removeComponentPropertyValues(id);
            return details;
        }

        [Route("getAllRackingType")]
        [HttpGet]
        public async Task<List<RackingType>> GetAllRackingType()
        {
            var details = DatabaseHelper.getAllRackingType();
            return details;
        }

        [Route("getRackingTypeById")]
        [HttpGet]
        public async Task<RackingType> GetRackingTypeById(int id)
        {
            var details = DatabaseHelper.getRackingTypeById(id);
            return details;
        }

        [Route("saveRackingType")]
        [HttpPost]
        public async Task<string> SaveRackingType(RackingType model)
        {
            var details = DatabaseHelper.saveRackingType(model);
            return details;
        }

        [Route("editRackingType")]
        [HttpPost]
        public async Task<string> EditRackingType(RackingType model)
        {
            var details = DatabaseHelper.editRackingType(model);
            return details;
        }

        [Route("removeRackingType")]
        [HttpPost]
        public async Task<string> RemoveRackingType(int id)
        {
            var details = DatabaseHelper.removeRackingType(id);
            return details;
        }

        [Route("getAllInspection")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInspection()
        {
            var details = DatabaseHelper.getAllInspection();
            return details;
        }

        [Route("getAllInspectionByEmployeeId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInspectionByEmployeeId()
        {
            var details = DatabaseHelper.getAllInspectionByEmployeeId();
            return details;
        }

        [Route("getAllInspectionByCustomerId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInspectionByCustomerId()
        {
            var details = DatabaseHelper.getAllInspectionByCustomerId(3);
            return details;
        }

        [Route("getAllInspectionDueByCustomerId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInspectionDueByCustomerId()
        {
            FilterCustomerModel filters = new FilterCustomerModel();
            var details = DatabaseHelper.GetDueInspectionsCustomerFilters(filters);
            //var details = DatabaseHelper.getAllInspectionDueByCustomerId(3);
            return details;
        }

        [Route("getAllIncidentByCustomerId")]
        [HttpGet]
        public async Task<List<IncidentViewModel>> GetAllIncidentByCustomerId()
        {
            FilterCustomerModel filters = new FilterCustomerModel();
            var details = DatabaseHelper.GetAllIncidentByCustomerId(filters);
            //var details = DatabaseHelper.getAllInspectionDueByCustomerId(3);
            return details;
        }

        [Route("getAllIncidentAdminByCustomerId")]
        [HttpGet]
        public async Task<List<IncidentViewModel>> GetAllIncidentAdminByCustomerId(long id)
        {
            var details = DatabaseHelper.GetAllIncidentAdminByCustomerId(id);
            //var details = DatabaseHelper.getAllInspectionDueByCustomerId(3);
            return details;
        }


        [Route("saveIncidentByCustomerId")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveIncidentByCustomerId()
        {
            try
            {
                var req = HttpContext.Current.Request;

                // --- model from form ---
                var model = new IncidentViewModel
                {
                    IncidentType = req.Form["IncidentType"],

                    CustomerId = int.TryParse(req.Form["CustomerId"], out var custId) ? custId : 0,
                    CustomerLocationId = int.TryParse(req.Form["CustomerLocationId"], out var locId) ? locId : 0,

                    IncidentDate = DateTime.TryParse(req.Form["IncidentDate"], out var dt) ? dt : DateTime.MinValue,
                    IncidentNumber = req.Form["IncidentNumber"],
                    IncidentReportedBy = req.Form["IncidentReportedBy"],
                    IncidentArea = req.Form["IncidentArea"],
                    IncidentRow = req.Form["IncidentRow"],
                    IncidentAisle = req.Form["IncidentAisle"],
                    IncidentBay = req.Form["IncidentBay"],
                    IncidentLevel = req.Form["IncidentLevel"],
                    IncidentBeamLocation = req.Form["IncidentBeamLocation"],
                    IncidentFrameSide = req.Form["IncidentFrameSide"],
                    IncidentSummary = req.Form["IncidentSummary"]
                };

                // --- gather files (whatever keys; we care about content) ---
                var fileList = new List<HttpPostedFileBase>();
                if (req.Files.Count > 0)
                {
                    for (int i = 0; i < req.Files.Count; i++)
                    {
                        var f = req.Files[i];
                        if (f != null && f.ContentLength > 0)
                            fileList.Add(new HttpPostedFileWrapper(f));
                    }
                }

                // --- save via EF helper (saves incident + uploads + photo rows) ---
                var idStr = DatabaseHelper.SaveIncidentByCustomerId(model, fileList);

                // helper returns either an ID or "Error: ...", normalize:
                if (idStr != null && idStr.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
                    return Ok(new { success = false, message = idStr });

                return Ok(new { success = true, id = idStr });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [Route("getIncidentReportById")]
        [HttpGet]
        public IHttpActionResult GetIncidentReportById(int id)
        {
            try
            {
                var incident = DatabaseHelper.GetIncidentReportById(id);
                if (incident == null)
                    return NotFound();

                return Ok(incident);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getDueInspectionsCustomerFilters")]
        public async Task<List<InspectionViewModel>> GetDueInspectionsCustomerFilters(FilterCustomerModel filters)
        {
            var details = DatabaseHelper.GetDueInspectionsCustomerFilters(filters);
            return details;
        }

        [HttpPost]
        [Route("getAllInspectionsCustomerFilters")]
        public async Task<List<InspectionViewModel>> GetAllInspectionsCustomerFilters(FilterCustomerModel filters)
        {
            var details = DatabaseHelper.GetAllInspectionsCustomerFilters(filters);
            return details;
        }

        [HttpGet]
        [Route("getInspectionDataClone")]
        public async Task<List<InspectionCloneViewModel>> GetInspectionDataClone(int id)
        {
            var details = DatabaseHelper.GetInspectionDataClone(id);
            return details;
        }

        [HttpPost]
        [Route("inspectionClone")]
        public async Task<int> InspectionClone(int targetid, int sourceid)
        {
            var details = DatabaseHelper.InspectionClone(targetid, sourceid);
            return details;
        }

        [HttpPost]
        [Route("getAllCustomerDocumentsWithFilters")]
        public async Task<List<CustomerLocationHistoryLegacyFileListing>> GetAllCustomerDocumentsWithFilters(FilterFilesModel filters)
        {
            var details = DatabaseHelper.GetAllCustomerDocumentsWithFilters(filters);
            return details;
        }

        [Route("getAllInspectionByCustomerIdInspectionStatus")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInspectionByCustomerIdInspectionStatus(int InspectionStatusId)
        {
            var details = DatabaseHelper.GetAllInspectionByCustomerIdInspectionStatus(InspectionStatusId);
            return details;
        }

        [Route("getInspectionById")]
        [HttpGet]
        public async Task<Inspection> GetInspectionById(int InspectionId)
        {
            var details = DatabaseHelper.getInspectionById(InspectionId);
            return details;
        }

        [Route("getInspectionByContactId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetInspectionByContactId()
        {
            var details = DatabaseHelper.getInspectionByContactId();
            return details;
        }

        [Route("getInspectionDetailsById")]
        [HttpGet]
        public async Task<InspectionViewModel> GetInspectionDetailsById(int InspectionId)
        {
            var details = DatabaseHelper.getInspectionDetailsById(InspectionId);
            return details;
        }

        [Route("getRecentInspectionAdminDashboard")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetRecentInspectionAdminDashboard()
        {
            var details = DatabaseHelper.getRecentInspectionAdminDashboard();
            return details;
        }

        [Route("getRecentCompletedInspectionbyCustomerId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetRecentCompletedInspectionbyCustomerId()
        {
            var details = DatabaseHelper.getRecentCompletedInspectionbyCustomerId();
            return details;
        }

        [Route("getRecentInspectionbyCustomerId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> getRecentInspectionbyCustomerId()
        {
            var details = DatabaseHelper.getRecentInspectionbyCustomerId();
            return details;
        }

        [Route("getRecentInspectionCustomerByEmployeeId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> getRecentInspectionCustomerByEmployeeId()
        {
            var details = DatabaseHelper.getRecentInspectionCustomerByEmployeeId();
            return details;
        }

        [Route("getRecentInspectionCustomerByEmployeeIdForMobile")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetRecentInspectionCustomerByEmployeeIdForMobile(int id)
        {
            var details = DatabaseHelper.getRecentInspectionCustomerByEmployeeIdForMobile(id);
            return details;
        }

        [Route("getAllInspectionDueEmployeeIdForMobile")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInspectionDueEmployeeIdForMobile(int id)
        {
            var details = DatabaseHelper.GetAllInspectionDueEmployeeIdForMobile(id);
            return details;
        }

        [Route("getAllApproveAndCompleteInspection")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllApproveAndCompleteInspection()
        {
            var details = DatabaseHelper.getAllApproveAndCompleteInspection();
            return details;
        }

        [Route("getAllSentForApprovalInspection")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllSentForApprovalInspection()
        {
            var details = DatabaseHelper.getAllSentForApprovalInspection();
            return details;
        }

        [Route("getAllInProgressInspection")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllInProgressInspection()
        {
            var details = DatabaseHelper.getAllInProgressInspection();
            return details;
        }

        [Route("getAllCompleteInspectionCount")]
        [HttpGet]
        public async Task<Int32> GetAllCompleteInspectionCount()
        {
            var InspectionCount = DatabaseHelper.getAllSentForApprovalInspection().Count;
            return InspectionCount;
        }

        [Route("getAllInProgressInspectionCount")]
        [HttpGet]
        public async Task<Int32> GetAllInProgressInspectionCount()
        {
            var InspectionCount = DatabaseHelper.getAllInProgressInspection().Count;
            return InspectionCount;
        }

        [Route("getAllDueInspection")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllDueInspection()
        {
            var details = DatabaseHelper.getAllDueInspection();
            return details;
        }

        [Route("getAllDueInspectionAdminDashboard")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetAllDueInspectionAdminDashboard()
        {
            var details = DatabaseHelper.getAllDueInspectionAdminDashboard();
            return details;
        }

        [Route("getDueInspectionByCustomerId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetDueInspectionByCustomerId()
        {
            var details = DatabaseHelper.getDueInspectionByCustomerId();
            return details;
        }

        [Route("getInspectionDetailsByLocationId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetInspectionDetailsByLocationId(int CustomerId, int CustomerLocationId, bool bForTech = false)
        {
            var details = DatabaseHelper.getInspectionDetailsByLocationId(CustomerId, CustomerLocationId, bForTech);
            return details;
        }

        [Route("getInspectionDetailsByLocationIdAreaId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetInspectionDetailsByLocationIdAreaId(int CustomerId, int CustomerLocationId, int AreaId, bool bForTech = false)
        {
            var details = DatabaseHelper.getInspectionDetailsByLocationIdAreaId(CustomerId, CustomerLocationId, AreaId, bForTech);
            return details;
        }
        [Route("getInspectionDetailsForSheet")]
        [HttpGet]
        public async Task<InspectionViewModel> GetInspectionDetailsForSheet(int id)
        {
            var details = DatabaseHelper.getInspectionDetailsForSheet(id);
            return details;
        }

        //[Route("getInspectionDetailsForSheetMobile")]
        //[HttpGet]
        //public async Task<InspectionViewModel> GetInspectionDetailsForSheetMobile(int id)
        //{
        //    var details = DatabaseHelper.getInspectionDetailsForSheetMobile(id);
        //    return details;
        //}

        //[Route("getInspectionDetailsmobile")]
        //[HttpGet]
        //public async Task<InspectionViewModel> getInspectionDetailsMobile(int id)
        //{
        //    var details = InspectionDetailsHelper.GetInspectionDetails(id);
        //    return details;
        //}


        [Route("getInspectionDetailsForSheetMobile")]
        [HttpGet]
        public async Task<InspectionViewModel> getInspectionDetailsForSheetMobile(int id)
        {
            var details = DatabaseHelper.getInspectionDetailsForSheetMobile(id);
            return details;
        }
        [Route("saveInspection")]
        [HttpPost]
        public async Task<string> SaveInspection(Inspection model)
        {
            var details = DatabaseHelper.saveInspection(model);
            return details;
        }


        [Route("editInspection")]
        [HttpPost]
        public async Task<string> EditInspection(Inspection model)
        {
            var details = DatabaseHelper.editInspection(model);
            return details;
        }

        [Route("editInspectionStatus")]
        [HttpPost]
        public async Task<string> EditInspectionStatus(long inspectionId, int inspectionStatus)
        {
            var details = DatabaseHelper.editInspectionStatus(inspectionId, inspectionStatus);
            return details;
        }

        [Route("removeInspection")]
        [HttpPost]
        public async Task<string> RemoveInspection(int id)
        {
            var details = DatabaseHelper.removeInspection(id);
            return details;
        }

        [Route("getAllInspectionType")]
        [HttpGet]
        public async Task<List<InspectionType>> GetAllInspectionType()
        {
            var details = DatabaseHelper.getAllInspectionType();
            return details;
        }

        [Route("getAllInspectionDeficiency")]
        [HttpGet]
        public async Task<List<InspectionDeficiency>> GetAllInspectionDeficiency()
        {
            var details = DatabaseHelper.getAllInspectionDeficiency();
            return details;
        }

        [Route("getAllInspectionDeficiencyById")]
        [HttpGet]
        public async Task<InspectionDeficiency> GetAllInspectionDeficiencyById(int id)
        {
            var details = DatabaseHelper.getAllInspectionDeficiencyById(id);
            return details;
        }

        [Route("saveInspectionDeficiency")]
        [HttpPost]
        public async Task<string> SaveInspectionDeficiency(InspectionDeficiencyViewModel model)
        {
            var details = DatabaseHelper.saveInspectionDeficiency(model);
            return details;
        }

        [Route("saveInspectionDeficiencyMobile")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveInspectionDeficiencyMobile(List<InspectionDeficiencyViewModel> models)
        {
            try
            {
                if (models == null || !models.Any())
                {
                    return BadRequest("No deficiencies provided");
                }

                //var result = await Task.Run(() => DatabaseHelper.SaveInspectionDeficiencyMobile(models));
                var result = DatabaseHelper.SaveInspectionDeficiencyMobile(models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Logger.Error($"Error in SaveInspectionDeficiencyMobile: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        [Route("updateInspectionStatusTechnician")]
        [HttpPost]
        public async Task<string> UpdateInspectionStatusTechnician(long inspectionId, int iStatus)
        {
            var details = DatabaseHelper.UpdateInspectionStatusTechnician(inspectionId, iStatus);
            return details;
        }

        [Route("saveInspectionDeficiencyTechnician")]
        [HttpPost]
        public async Task<string> saveInspectionDeficiencyTechnician(InspectionDeficiencyViewModel model)
        {
            var details = DatabaseHelper.saveInspectionDeficiencyTechnician(model);
            return details;
        }

        [Route("saveInspectionDeficiencyTechnicianMobile")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveInspectionDeficiencyTechnicianMobile(List<InspectionDeficiencyViewModel> models)
        {
            try
            {
                if (models == null || !models.Any())
                {
                    return BadRequest("No technician deficiencies provided");
                }

                //var result = await Task.Run(() => DatabaseHelper.saveInspectionDeficiencyTechnicianMobile(models));
                var result = DatabaseHelper.saveInspectionDeficiencyTechnicianMobile(models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("saveInspectionDeficiencyAdminStatus")]
        [HttpPost]
        public async Task<string> SaveInspectionDeficiencyAdminStatus(long inspectionId, string iAdminStatus)
        {
            var details = DatabaseHelper.saveInspectionDeficiencyAdminStatus(inspectionId, iAdminStatus);
            return details;
        }

        [Route("SaveUpdateApproveInspectionAdmin")]
        [HttpPost]
        public async Task<string> SaveUpdateApproveInspectionAdmin(long inspectionId, int iInspectionStatus, string iAdminIspectionDeficiencyIdStatus, long iStampingEngineerId, string sCheckedDocument, string ShelvingChecklistComments ="")
        {
            var details = DatabaseHelper.SaveUpdateApproveInspectionAdmin(inspectionId, iInspectionStatus, iAdminIspectionDeficiencyIdStatus, iStampingEngineerId, sCheckedDocument, ShelvingChecklistComments);
            return details;
        }

        //[Route("GenerateQuotationFromCustomer")]
        //[HttpPost]
        //public async Task<string> GenerateQuotationFromCustomer(long inspectionId, string sCustomerSelectedDeficiencyIds)
        //{
        //    var details = DatabaseHelper.GenerateQuotationFromCustomerAsync(inspectionId, sCustomerSelectedDeficiencyIds);
        //    return details;
        //}

        [Route("GenerateQuotationFromCustomer")]
        [HttpPost]
        public string GenerateQuotationFromCustomer(long inspectionId, string sCustomerSelectedDeficiencyIds)
        {
            return DatabaseHelper.GenerateQuotationFromCustomerAsync(inspectionId, sCustomerSelectedDeficiencyIds);
        }


        [Route("saveQuotationItemsByAdmin")]
        [HttpPost]
        // QuotationItem QuotationComponentList  SaveQuotationRequest objSaveQuotationRequest SaveQuotationRequest objSaveQuotationRequestlong QuotationId, List<QuotationComponent> QuotationComponentList)
        public async Task<Quotation> SaveQuotationItemsByAdmin(long InspectionId, long QuotationId, long QuotationInspectionItemId, string ItemPartNo,
            string ItemDescription, string ItemUnitPrice, string ItemSurcharge, string ItemMarkup, string ItemPrice,
            string ItemQuantity, string ItemWeight, string LineTotal, string ItemWeightTotal, bool isTBD, string ItemLabour, string ItemLabourTotal, string inline)
        {
            if (ItemLabour.Trim().Length == 0)
            {
                ItemLabour = "0";
            }
            if (ItemLabourTotal.Trim().Length == 0)
            {
                ItemLabourTotal = "0";
            }
            bool bInline = Convert.ToBoolean(inline);
            Quotation objQuotation = new Quotation();
            QuotationItem objQuotationItem = new QuotationItem();
            objQuotationItem.QuotationInspectionItemId = QuotationInspectionItemId;
            objQuotationItem.InspectionId = InspectionId;
            objQuotationItem.QuotationId = QuotationId;
            objQuotationItem.ItemPartNo = ItemPartNo;
            objQuotationItem.ItemDescription = ItemDescription;
            objQuotationItem.ItemUnitPrice = Convert.ToDecimal(ItemUnitPrice);
            objQuotationItem.ItemSurcharge = Convert.ToDecimal(ItemSurcharge);
            objQuotationItem.ItemMarkup = Convert.ToDecimal(ItemMarkup);
            objQuotationItem.ItemPrice = Convert.ToDecimal(ItemPrice);
            objQuotationItem.ItemQuantity = Convert.ToInt32(ItemQuantity);
            objQuotationItem.ItemWeight = Convert.ToDecimal(ItemWeight);
            objQuotationItem.LineTotal = Convert.ToDecimal(LineTotal);
            objQuotationItem.ItemWeightTotal = Convert.ToDecimal(ItemWeightTotal);
            objQuotationItem.ItemLabour = Convert.ToDecimal(ItemLabour);
            objQuotationItem.ItemLabourTotal = Convert.ToDecimal(ItemLabourTotal);
            objQuotationItem.IsTBD = isTBD;
            //if (objSaveQuotationRequest != null)
            //{ }            
            objQuotation = DatabaseHelper.SaveQuotationDetails(QuotationId, objQuotationItem, bInline);

            return objQuotation;
        }

        [Route("updateLabourByAdmin")]
        [HttpPost]
        // QuotationItem QuotationComponentList  SaveQuotationRequest objSaveQuotationRequest SaveQuotationRequest objSaveQuotationRequestlong QuotationId, List<QuotationComponent> QuotationComponentList)
        public async Task<Quotation> UpdateLabourByAdmin(Quotation objQuotation)
        {
            objQuotation = DatabaseHelper.UpdateLabourByAdmin(objQuotation);
            return objQuotation;
        }
        //[Route("saveQuotationItemsByAdmin")]
        //[HttpPost]
        //// QuotationItem QuotationComponentList  SaveQuotationRequest objSaveQuotationRequest SaveQuotationRequest objSaveQuotationRequestlong QuotationId, List<QuotationComponent> QuotationComponentList)
        //public async Task<Quotation> SaveQuotationItemsByAdmin(QuotationItem objQuotationItem, string inline)
        //{
        //    //if (objQuotationItem.ItemLabour.Trim().Length == 0)
        //    //{
        //    //    ItemLabour = "0";
        //    //}
        //    //if (ItemLabourTotal.Trim().Length == 0)
        //    //{
        //    //    ItemLabourTotal = "0";
        //    //}
        //    bool bInline = Convert.ToBoolean(inline);
        //    Quotation objQuotation = new Quotation();
        //    //QuotationItem objQuotationItem = new QuotationItem();
        //    //objQuotationItem.QuotationInspectionItemId = QuotationInspectionItemId;
        //    //objQuotationItem.InspectionId = InspectionId;
        //    //objQuotationItem.QuotationId = QuotationId;
        //    //objQuotationItem.ItemPartNo = ItemPartNo;
        //    //objQuotationItem.ItemDescription = ItemDescription;
        //    //objQuotationItem.ItemUnitPrice = Convert.ToDecimal(ItemUnitPrice);
        //    //objQuotationItem.ItemSurcharge = Convert.ToDecimal(ItemSurcharge);
        //    //objQuotationItem.ItemMarkup = Convert.ToDecimal(ItemMarkup);
        //    //objQuotationItem.ItemPrice = Convert.ToDecimal(ItemPrice);
        //    //objQuotationItem.ItemQuantity = Convert.ToInt32(ItemQuantity);
        //    //objQuotationItem.ItemWeight = Convert.ToDecimal(ItemWeight);
        //    //objQuotationItem.LineTotal = Convert.ToDecimal(LineTotal);
        //    //objQuotationItem.ItemWeightTotal = Convert.ToDecimal(ItemWeightTotal);
        //    //objQuotationItem.ItemLabour = Convert.ToDecimal(ItemLabour);
        //    //objQuotationItem.ItemLabourTotal = Convert.ToDecimal(ItemLabourTotal);
        //    //objQuotationItem.IsTBD = isTBD;
        //    ////if (objSaveQuotationRequest != null)
        //    ////{ }            
        //    objQuotation = DatabaseHelper.SaveQuotationDetails(objQuotationItem.QuotationId, objQuotationItem, bInline);

        //    return objQuotation;
        //}
        [Route("saveQuotationAdmin")]
        [HttpPost]
        public async Task<Quotation> SaveQuotationAdmin(AdminQuotation objQuotation)
        {
            var strResult = DatabaseHelper.saveQuotationAdmin(objQuotation);
            return strResult;
        }

        [Route("sendQuotationtoCustomerForApproval")]
        [HttpPost]
        public async Task<Quotation> sendQuotationtoCustomerForApproval(AdminQuotation objQuotation)
        {
            var strResult = DatabaseHelper.sendQuotationtoCustomerForApproval(objQuotation);
            return strResult;
        }
        [Route("updateitempriceadmin")]
        [HttpPost]
        public async Task<Quotation> Updateitempriceadmin(AdminQuotation objQuotation)
        {
            var strResult = DatabaseHelper.Updateitempriceadmin(objQuotation);
            return strResult;
        }
        [Route("updateQuotationGSTAdmin")]
        [HttpPost]
        public async Task<string> updateQuotationGSTAdmin(Quotation objQuotation)
        {
            var strResult = DatabaseHelper.UpdateQuotationGSTAdmin(objQuotation);
            return strResult;
        }

        [Route("approveQuotationCustomer")]
        [HttpPost]
        public async Task<string> ApproveQuotationCustomer(long inspectionId, long quotationId)
        {
            var strResult = DatabaseHelper.ApproveQuotationCustomer(inspectionId, quotationId);
            return strResult;
        }



        [Route("removeQuotationItemsByAdmin")]
        [HttpPost]
        public async Task<Quotation> RemoveQuotationItemsByAdmin(long id, long quotationId)
        {
            Quotation objQuotation = new Quotation();
            objQuotation = DatabaseHelper.RemoveQuotationItemsByAdmin(id, quotationId);
            return objQuotation;
        }

        [Route("saveComponentSaved")]
        [HttpPost]
        public async Task<string> SaveComponentSaved(List<ComponentSavedViewModel> model)
        {
            var details = DatabaseHelper.saveComponentSaved(model);
            return details;
        }

        [Route("getComponentSaved")]
        [HttpGet]
        public async Task<List<ComponentSavedViewModel>> GetComponentSaved(long ComponentId, long ComponentManufacturerId, long CustomerId, long CustomerLocationID)
        {
            var LstComponentSaved = DatabaseHelper.getComponentSaved(ComponentId, ComponentManufacturerId, CustomerId, CustomerLocationID);
            return LstComponentSaved;
        }

        [Route("getComponentSavedMaster")]
        [HttpGet]
        public async Task<List<ComponentSavedViewModel>> getComponentSavedMaster(long ComponentId, long ComponentManufacturerId, string ComponentDesc = "")
        {
            var LstComponentSaved = DatabaseHelper.getComponentSavedMaster(ComponentId, ComponentManufacturerId, ComponentDesc);
            return LstComponentSaved;
        }


        [Route("editInspectionDeficiency")]
        [HttpPost]
        public async Task<string> EditInspectionDeficiency(InspectionDeficiency model)
        {
            var details = DatabaseHelper.editInspectionDeficiency(model);
            return details;
        }

        [Route("getAllInspectionDeficiencyPhoto")]
        [HttpGet]
        public async Task<List<InspectionDeficiencyPhoto>> GetAllInspectionDeficiencyPhoto()
        {
            var details = DatabaseHelper.getAllInspectionDeficiencyPhoto();
            return details;
        }

        [Route("saveInspectionDeficiencyPhoto")]
        [HttpPost]
        public async Task<string> SaveInspectionDeficiencyPhoto(InspectionDeficiencyPhoto model)
        {
            var details = DatabaseHelper.saveInspectionDeficiencyPhoto(model);
            return details;
        }

        [Route("editInspectionDeficiencyPhoto")]
        [HttpPost]
        public async Task<string> EditInspectionDeficiencyPhoto(InspectionDeficiencyPhoto model)
        {
            var details = DatabaseHelper.editInspectionDeficiencyPhoto(model);
            return details;
        }

        [Route("getAllInspectionDeficiencyMTO")]
        [HttpGet]
        public async Task<List<InspectionDeficiencyMTO>> GetAllInspectionDeficiencyMTO()
        {
            var details = DatabaseHelper.getAllInspectionDeficiencyMTO();
            return details;
        }

        [Route("saveInspectionDeficiencyMTO")]
        [HttpPost]
        public async Task<string> SaveInspectionDeficiencyMTO(InspectionDeficiencyMTOViewModel model)
        {
            var details = DatabaseHelper.saveInspectionDeficiencyMTO(model);
            return details;
        }

        [Route("editInspectionDeficiencyMTO")]
        [HttpPost]
        public async Task<string> EditInspectionDeficiencyMTO(InspectionDeficiencyMTO model)
        {
            var details = DatabaseHelper.editInspectionDeficiencyMTO(model);
            return details;
        }

        [Route("getAllInspectionDeficiencyMTODetail")]
        [HttpGet]
        public async Task<List<InspectionDeficiencyMTODetail>> GetAllInspectionDeficiencyMTODetail()
        {
            var details = DatabaseHelper.getAllInspectionDeficiencyMTODetail();
            return details;
        }

        [Route("saveInspectionDeficiencyMTODetail")]
        [HttpPost]
        public async Task<string> SaveInspectionDeficiencyMTODetail(InspectionDeficiencyMTODetailViewModel model)
        {
            var details = DatabaseHelper.saveInspectionDeficiencyMTODetail(model);
            return details;
        }

        [Route("getAllInspectionDue")]
        [HttpGet]
        public async Task<List<InspectionDueViewModel>> GetAllInspectionDue()
        {
            var details = DatabaseHelper.getAllInspectionDue();
            return details;
        }

        [Route("getInspectionDueByEmployeeId")]
        [HttpGet]
        public async Task<List<InspectionViewModel>> GetInspectionDueByEmployeeId()
        {
            var details = DatabaseHelper.getInspectionDueByEmployeeId();
            return details;
        }

        [Route("saveInspectionDue")]
        [HttpPost]
        public async Task<string> SaveInspectionDue(Inspection model)
        {
            var details = await DatabaseHelper.saveInspectionDue(model);
            return details;
        }

        [Route("editInspectionDue")]
        [HttpPost]
        public async Task<string> EditInspectionDue(Inspection model)
        {
            var details = DatabaseHelper.editInspectionDue(model);
            return details;
        }

        [Route("removeInspectionDue")]
        [HttpPost]
        public async Task<string> RemoveInspectionDue(int id)
        {
            var details = DatabaseHelper.removeInspectionDue(id);
            return details;
        }

        [Route("getInspectionFileDrawingById")]
        [HttpGet]
        public async Task<InspectionFileDrawing> GetInspectionFileDrawingById(long id)
        {
            var details = DatabaseHelper.getInspectionFileDrawingById(id);
            return details;
        }

        [Route("getInspectionFileDrawingByInspectionId")]
        [HttpGet]
        public async Task<List<InspectionFileDrawing>> GetInspectionFileDrawingByInspectionId(long id)
        {
            var details = DatabaseHelper.getInspectionFileDrawingByInspectionId(id);
            return details;
        }

        [Route("saveInspectionFileDrawing")]
        [HttpPost]
        public async Task<long> SaveInspectionFileDrawing(InspectionFileDrawing model)
        {
            var details = DatabaseHelper.saveInspectionFileDrawing(model);
            return details;
        }

        [Route("uploadHistoryLegacyFile")]
        [HttpPost]
        public async Task<long> UploadHistoryLegacyFile(CustomerLocationHistoryLegacyFile model)
        {
            var details = DatabaseHelper.UploadHistoryLegacyFile(model);
            return details;
        }


        [Route("deleteHistoryLegacyFile")]
        [HttpPost]
        public async Task<string> DeleteHistoryLegacyFile(long CustomerLocationHistoryLegacyFileId)
        {
            try
            {
                int iResult = DatabaseHelper.removeHistoryLegacyFile(CustomerLocationHistoryLegacyFileId);
                string details = "";
                if (iResult == 0)
                {
                    details = "File not found or already deleted.";
                }
                else
                {
                    details = "File deleted successfully.";
                }
                return details;
            }
            catch (Exception ex)
            {
                return "Error deleting file: " + ex.Message;
            }
        }
        //
        [Route("getCustomerLocationHistoryLegacyFiles")]
        [HttpGet]
        public async Task<List<CustomerLocationHistoryLegacyFileListing>> GetCustomerLocationHistoryLegacyFiles(int id)
        {
            var details = DatabaseHelper.GetCustomerLocationHistoryLegacyFiles(id);
            return details;
        }

        [Route("removeInspectionFileDrawing")]
        [HttpPost]
        public async Task<long> RemoveInspectionFileDrawing(int id)
        {
            var details = DatabaseHelper.removeInspectionFileDrawing(id);
            return details;
        }

        [Route("getAllActionRequired")]
        [HttpGet]
        public async Task<List<ActionRequired>> GetAllActionRequired()
        {
            var details = DatabaseHelper.getAllActionRequired();
            return details;
        }

        [Route("getComponentPropertyType")]
        [HttpGet]
        public async Task<List<ComponentPropertyType>> GetComponentPropertyType()
        {
            var lstComponentPropertyType = DatabaseHelper.getComponentPropertyType();
            return lstComponentPropertyType;
        }

        [Route("getPropertyByComponent")]
        [HttpGet]
        public async Task<List<ComponentPropertyType>> GetPropertyByComponent(int ComponentId)
        {
            var lstComponentPropertyType = DatabaseHelper.GetPropertyByComponent(ComponentId);
            return lstComponentPropertyType;
        }

        [Route("GetComponentPropertyValues")]
        [HttpGet]
        public async Task<List<ComponentPropertyValue>> GetComponentPropertyValues(int ComponentPropertyTypeId)
        {
            var listComponentPropertyType = DatabaseHelper.getComponentPropertyValues(ComponentPropertyTypeId);
            return listComponentPropertyType;
        }

        [Route("getComponentFromDeficiency")]
        [HttpGet]
        public async Task<string> GetComponentFromDeficiency(int DeficiencyId)
        {
            var ComponentId = DatabaseHelper.getComponentFromDeficiency(DeficiencyId);
            return ComponentId;
        }

        [Route("getAllIdentifyRackingProfile")]
        [HttpGet]
        public async Task<List<IdentifyRackingProfile>> GetAllIdentifyRackingProfile()
        {
            var AllIdentifyRackingProfile = DatabaseHelper.getAllIdentifyRackingProfile();
            return AllIdentifyRackingProfile;
        }

        [Route("removeIdentifyRackingProfile")]
        [HttpPost]
        public async Task<string> RemoveIdentifyRackingProfile(int id)
        {
            var details = DatabaseHelper.removeIdentifyRackingProfile(id);
            return details;
        }

        [Route("getAllDocumentTitle")]
        [HttpGet]
        public async Task<List<DocumentTitle>> GetAllDocumentTitle()
        {
            var details = DatabaseHelper.getAllDocumentTitle();
            return details;
        }

        [Route("getDocumentTitleById")]
        [HttpGet]
        public async Task<DocumentTitle> GetDocumentTitleById(int id)
        {
            var details = DatabaseHelper.getDocumentTitleById(id);
            return details;
        }

        [Route("saveDocumentTitle")]
        [HttpPost]
        public async Task<int> SaveDocumentTitle(DocumentTitle model)
        {
            var details = DatabaseHelper.saveDocumentTitle(model);
            return details;
        }

        [Route("editDocumentTitle")]
        [HttpPost]
        public async Task<string> EditDocumentTitle(DocumentTitle model)
        {
            var details = DatabaseHelper.editDocumentTitle(model);
            return details;
        }

        [Route("editSetting")]
        [HttpPost]
        public async Task<string> EditSetting(ImpSetting model)
        {
            var details = DatabaseHelper.EditSetting(model);
            return details;
        }

        [Route("removeDocumentTitle")]
        [HttpPost]
        public async Task<string> RemoveDocumentTitle(int id)
        {
            var details = DatabaseHelper.removeDocumentTitle(id);
            return details;
        }

        #endregion

        #region "Dashboard"
        [Route("getCustomerCount")]
        [HttpGet]
        public async Task<Int32> GetCustomerCount()
        {
            int custCnt = DatabaseHelper.getAllCustomers().Count;
            return custCnt;
        }

        [Route("getEmployeeCount")]
        [HttpGet]
        public async Task<Int32> GetEmployeeCount()
        {
            int empCnt = DatabaseHelper.getAllEmployee().Count;
            return empCnt;
        }

        [Route("ShowRegistration")]
        [HttpGet]
        public async Task<bool> ShowRegistration()
        {
            bool isRegistration = AdminController.ShowRegistration();
            return isRegistration;
        }

        [Route("getDashboard")]
        [HttpGet]
        public async Task<AdminDashboardGraphViewModel> GetDashboard(Int32 id, int year)
        {
            AdminDashboardGraphViewModel graph = new AdminDashboardGraphViewModel();
            //graph.InspectionDueCount = 0;
            //graph.InProgressCount = 0;
            //graph.SentforApprovalCount = 0;
            //graph.ApprovedCompletedCount = 0;
            //graph.QuotationRequestedCount = 0;
            //graph.AwaitingApprovalCount = 0;
            //graph.QuotationApprovedCount = 0;
            //graph.RepairCompletedCount = 0;
            //graph.InspectionFinishedCount = 0;
            //graph.DashboardActiveUserCount = 0;
            //graph.DashboardActiveUserAdminCount = 0;
            //graph.DashboardActiveUserEmployeeCount = 0;
            //graph.DashboardActiveCompanyCount = 0;
            try
            {
                graph = DatabaseHelper.GetDashboardCountForEmployeeByYear(id, year);
            }
            catch (Exception)
            {
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
                // throw;
            }
            return graph;
        }

        [Route("getCustomerDashboardCountByYear")]
        [HttpGet]
        public async Task<Dashboard> GetCustomerDashboardCountByYear(int year)
        {
            Dashboard objDashboard = new Dashboard();
            objDashboard = await DatabaseHelper.GetCustomerDashboardCountByYear(year);
            return objDashboard;
        }

        [Route("getInspectionCountGraph")]
        [HttpGet]
        public async Task<List<InsepctionCount_Graph>> GetInspectionCountGraph(Int32 id)
        {
            var lstInspectionCount = DatabaseHelper.getInspectionCountGraph(id);
            return lstInspectionCount;
        }

        [Route("getDoneEmpInspectionGraph")]
        [HttpGet]
        public async Task<List<sp_getEmpInspection_Count_Result>> GetDoneEmpInspectionGraph()
        {
            var details = DatabaseHelper.getDoneEmpInspectionGraph();
            return details;
        }

        [Route("getApprovedInspectionCount_Graph")]
        [HttpGet]
        public async Task<List<sp_getApprovedInspection_Count_Result>> GetApprovedInspectionCount_Graph()
        {
            var details = DatabaseHelper.getApprovedInspectionCount_Graph();
            return details;
        }

        [Route("getInspectionCountGraphByYear")]
        [HttpGet]
        public async Task<List<InsepctionCount_Graph>> GetInspectionCountGraphByYear(int year)
        {
            var lstInspectionCount = DatabaseHelper.getInspectionCountGraphByYear(0, year);
            return lstInspectionCount;
        }

        [Route("getInspectionCountGraphByYearMobile")]
        [HttpGet]
        public async Task<List<InsepctionCount_Graph>> GetInspectionCountGraphByYear(Int32 id, int year)
        {
            var lstInspectionCount = DatabaseHelper.getInspectionCountGraphByYear(id, year);
            return lstInspectionCount;
        }

        [Route("getDeficienciesBreakdownCategories")]
        [HttpGet]
        public async Task<List<Get_DeficienciesBreakdownCategories_Result>> GetDeficienciesBreakdownCategories(int year)
        {
            var lstInspectionCount = DatabaseHelper.GetDeficienciesBreakdownCategories(year);
            return lstInspectionCount;
        }

        [Route("getDeficienciesBreakdownCategoriesInspection")]
        [HttpGet]
        public async Task<List<Get_DeficienciesBreakdownCategoriesInspection_Result>> GetDeficienciesBreakdownCategoriesInspection(long InspectionId)
        {
            var lstInspectionCount = DatabaseHelper.GetDeficienciesBreakdownCategoriesInspection(InspectionId);
            return lstInspectionCount;
        }

        [Route("getDeficienciesbySeverityCustomer")]
        [HttpGet]
        public async Task<List<Get_DeficienciesBySeverityCustomerNew_Result>> GetDeficienciesbySeverityCustomer(int year)
        {
            var lstInspectionCount = DatabaseHelper.GetDeficienciesbySeverityCustomer(year);
            return lstInspectionCount;
        }

        [Route("getDeficienciesbySeverityCustomerInspection")]
        [HttpGet]
        public async Task<List<Get_DeficienciesBySeverityCustomerInspection_Result>> GetDeficienciesbySeverityCustomerInspection(long inspectionid)
        {
            var lstInspectionCount = DatabaseHelper.GetDeficienciesbySeverityCustomerInspection(inspectionid);
            return lstInspectionCount;
        }

        [Route("getDeficienciesTrendFromPreviousYears")]
        [HttpGet]
        public async Task<List<Get_DeficienciesTrendFromPreviousYears_Result>> GetDeficienciesTrendFromPreviousYears()
        {
            var lstInspectionCount = DatabaseHelper.GetDeficienciesTrendFromPreviousYears();
            return lstInspectionCount;
        }

        [Route("getDeficienciesTrendFromPreviousYearsForCustomerLocation")]
        [HttpGet]
        public async Task<List<Get_DeficienciesTrendFromPreviousYearsCustomerLocation_Result>> GetDeficienciesTrendFromPreviousYearsForCustomerLocation(int customerLocationid)
        {
            var lstInspectionCount = DatabaseHelper.GetDeficienciesTrendFromPreviousYearsForCustomerLocation(customerLocationid);
            return lstInspectionCount;
        }

        [Route("getDoneEmpInspectionCountByYear")]
        [HttpGet]
        public async Task<List<sp_getEmpInspection_Count_New_Result>> GetDoneEmpInspectionCountByYear(int year)
        {
            var details = DatabaseHelper.getDoneEmpInspectionCountByYear(year);
            return details;
        }

        [Route("getApprovedInspectionCountByYear")]
        [HttpGet]
        public async Task<List<sp_getApprovedInspection_Count_New_Result>> GetApprovedInspectionCountByYear(int year)
        {
            var details = DatabaseHelper.getApprovedInspectionCountByYear(year);
            return details;
        }

        [Route("getDashboardCountByYear")]
        [HttpGet]
        public async Task<AdminDashboardGraphViewModel> GetDashboardCountByYear(int year)
        {
            AdminDashboardGraphViewModel graph = new AdminDashboardGraphViewModel();
            graph = DatabaseHelper.GetDashboardCountByYear(year);
            return graph;
        }

        [Route("getAllDueInspectionbyYear")]
        [HttpGet]
        public async Task<long> GetAllDueInspectionbyYear(int year)
        {
            var details = DatabaseHelper.getAllDueInspectionbyYear(year);
            return details;
        }

        [Route("getAllInProgressInspectionbyYear")]
        [HttpGet]
        public async Task<long> GetAllInProgressInspectionbyYear(int year)
        {
            var details = DatabaseHelper.getAllInProgressInspectionbyYear(year);
            return details;
        }

        [Route("getAllSentToApprovalInspectionbyYear")]
        [HttpGet]
        public async Task<long> GetAllSentToApprovalInspectionbyYear(int year)
        {
            var details = DatabaseHelper.getAllSentToApprovalInspectionbyYear(year);
            return details;
        }

        [Route("getAllApprovedAndCompleteInspectionbyYear")]
        [HttpGet]
        public async Task<long> GetAllApprovedAndCompleteInspectionbyYear(int year)
        {
            var details = DatabaseHelper.getAllApprovedAndCompleteInspectionbyYear(year);
            return details;
        }

        [Route("getAllNotificationByUserIdWeb")]
        [HttpGet]
        public async Task<List<Notification>> GetAllNotificationByUserIdWeb()
        {
            var details = DatabaseHelper.getAllNotificationByUserIdWeb();
            return details;
        }

        [Route("getAllNotificationByUserId")]
        [HttpGet]
        public async Task<List<Notification>> getAllNotificationByUserId(long id)
        {
            var details = DatabaseHelper.getAllNotificationByUserId(id);
            return details;
        }

        [Route("updateNotificationStatus")]
        [HttpPost]
        public async Task<string> updateNotificationStatus(List<Notification> model)
        {
            var details = DatabaseHelper.UpdateNotificationStatus(model);
            return details.ToString();
        }

        [Route("SaveNotification")]
        [HttpPost]
        public async Task<string> SaveNotification(Notification model)
        {
            var details = DatabaseHelper.saveNotification(model);
            return "Ok";
        }

        [Route("GenerateGUID")]
        [HttpPost]
        public async Task<string> GenerateGUID()
        {
            var GUID = DatabaseHelper.GenerateGUID();
            return GUID.ToString();
        }

        #endregion

        #region ShelvingCheckListType CRUD

        [HttpPost]
        [Route("ShelvingCheckList_CreateShelvingCheckListType")]
        public IHttpActionResult CreateShelvingCheckListType(ShelvingCheckListType model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data");
                }

                // For mobile app - get loggedInUserId from request body or form
                var loggedInUserId = model.CreatedBy ?? "mobile_user";

                var result = DatabaseHelper.CreateShelvingCheckListType(model, loggedInUserId);
                if (result > 0)
                {
                    return Ok(new { success = true, message = "Shelving checklist type created successfully", id = result });
                }
                return BadRequest("Failed to create shelving checklist type");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_UpdateShelvingCheckListType")]
        public IHttpActionResult UpdateShelvingCheckListType(ShelvingCheckListType model)
        {
            try
            {
                if (model == null || model.ShelvingCheckListTypeId <= 0)
                {
                    return BadRequest("Invalid data");
                }

                var loggedInUserId = model.ModifiedBy ?? "mobile_user";

                var result = DatabaseHelper.UpdateShelvingCheckListType(model, loggedInUserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Shelving checklist type updated successfully" });
                }
                return BadRequest("Failed to update shelving checklist type");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_DeleteShelvingCheckListType")]
        public IHttpActionResult DeleteShelvingCheckListType([FromBody] DeleteRequest request)
        {
            try
            {
                if (request == null || request.id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var loggedInUserId = request.loggedInUserId ?? "mobile_user";

                var result = DatabaseHelper.DeleteShelvingCheckListType(request.id, loggedInUserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Shelving checklist type deleted successfully" });
                }
                return BadRequest("Failed to delete shelving checklist type");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListType")]
        public IHttpActionResult GetShelvingCheckListType(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var result = DatabaseHelper.GetShelvingCheckListTypeById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetAllShelvingCheckListTypes")]
        public IHttpActionResult GetAllShelvingCheckListTypes()
        {
            try
            {
                var result = DatabaseHelper.GetAllShelvingCheckListTypes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region ShelvingCheckListDeficiency CRUD

        [HttpPost]
        [Route("ShelvingCheckList_CreateShelvingCheckListDeficiency")]
        public IHttpActionResult CreateShelvingCheckListDeficiency(ShelvingCheckListDeficiency model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data");
                }

                var loggedInUserId = model.CreatedBy ?? "mobile_user";

                var result = DatabaseHelper.CreateShelvingCheckListDeficiency(model, loggedInUserId);
                if (result > 0)
                {
                    return Ok(new { success = true, message = "Shelving checklist deficiency created successfully", id = result });
                }
                return BadRequest("Failed to create shelving checklist deficiency");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_UpdateShelvingCheckListDeficiency")]
        public IHttpActionResult UpdateShelvingCheckListDeficiency(ShelvingCheckListDeficiency model)
        {
            try
            {
                if (model == null || model.ShelvingCheckListDeficiencyID <= 0)
                {
                    return BadRequest("Invalid data");
                }

                var loggedInUserId = model.ModifiedBy ?? "mobile_user";

                var result = DatabaseHelper.UpdateShelvingCheckListDeficiency(model, loggedInUserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Shelving checklist deficiency updated successfully" });
                }
                return BadRequest("Failed to update shelving checklist deficiency");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_DeleteShelvingCheckListDeficiency")]
        public IHttpActionResult DeleteShelvingCheckListDeficiency([FromBody] DeleteRequest request)
        {
            try
            {
                if (request == null || request.id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var loggedInUserId = request.loggedInUserId ?? "mobile_user";

                var result = DatabaseHelper.DeleteShelvingCheckListDeficiency(request.id, loggedInUserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Shelving checklist deficiency deleted successfully" });
                }
                return BadRequest("Failed to delete shelving checklist deficiency");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListDeficiency")]
        public IHttpActionResult GetShelvingCheckListDeficiency(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var result = DatabaseHelper.GetShelvingCheckListDeficiencyById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetAllActiveShelvingCheckListDeficiencies")]
        public IHttpActionResult GetAllActiveShelvingCheckListDeficiencies()
        {
            try
            {
                var result = DatabaseHelper.GetAllActiveShelvingCheckListDeficiencies();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region ShelvingCheckList CRUD

        [HttpPost]
        [Route("ShelvingCheckList_SaveShelvingCheckList")]
        public IHttpActionResult SaveShelvingCheckList(ShelvingCheckListSaveModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid data");
                }

                var loggedInUserId = model.loggedInUserId ?? "mobile_user";

                // Get the physical path for photo storage
                string photoBasePath = HttpContext.Current.Server.MapPath("~/img/ShelvingCheckList/");
                string thumbBasePath = HttpContext.Current.Server.MapPath("~/img/ShelvingCheckListThumb/");

                var result = DatabaseHelper.SaveShelvingCheckListComplete(
                    model.ShelvingCheckList,
                    model.ShelvingCheckListDetails,
                    model.ShelvingCheckListPhotos,
                    loggedInUserId,
                    photoBasePath,
                    thumbBasePath
                );

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        shelvingCheckListId = result.ShelvingCheckListId
                    });
                }

                return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_DeleteShelvingCheckList")]
        public IHttpActionResult DeleteShelvingCheckList([FromBody] DeleteRequest request)
        {
            try
            {
                if (request == null || request.id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var loggedInUserId = request.loggedInUserId ?? "mobile_user";

                var result = DatabaseHelper.DeleteShelvingCheckList(request.id, loggedInUserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Shelving checklist deleted successfully" });
                }
                return BadRequest("Failed to delete shelving checklist");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListsByInspectionId")]
        public IHttpActionResult GetShelvingCheckListsByInspectionId(long inspectionId)
        {
            try
            {
                if (inspectionId <= 0)
                {
                    return BadRequest("Invalid inspection id");
                }

                var result = DatabaseHelper.GetShelvingCheckListsByInspectionId(inspectionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckList")]
        public IHttpActionResult GetShelvingCheckList(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var result = DatabaseHelper.GetShelvingCheckListById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region ShelvingCheckListDetail CRUD

        [HttpPost]
        [Route("ShelvingCheckList_SaveShelvingCheckListDetails")]
        public IHttpActionResult SaveShelvingCheckListDetails(SaveDetailsRequest request)
        {
            try
            {
                if (request == null || request.details == null || !request.details.Any())
                {
                    return BadRequest("Invalid data");
                }

                var loggedInUserId = request.loggedInUserId ?? "mobile_user";

                // Validate each detail record
                foreach (var detail in request.details)
                {
                    var checkCount = 0;
                    if (detail.Tick_Yes == true) checkCount++;
                    if (detail.Tick_No == true) checkCount++;
                    if (detail.Tick_NA == true) checkCount++;

                    if (checkCount != 1)
                    {
                        return BadRequest("Exactly one of Yes, No, or NA must be selected for each checklist detail");
                    }
                }

                // Determine if create or update based on DetailId
                var detailsToCreate = request.details.Where(d => d.ShelvingCheckListDetailId == 0).ToList();
                var detailsToUpdate = request.details.Where(d => d.ShelvingCheckListDetailId > 0).ToList();

                int createdCount = 0;
                int updatedCount = 0;

                if (detailsToCreate.Any())
                {
                    createdCount = DatabaseHelper.CreateShelvingCheckListDetails(detailsToCreate, loggedInUserId);
                }

                if (detailsToUpdate.Any())
                {
                    updatedCount = DatabaseHelper.UpdateShelvingCheckListDetails(detailsToUpdate, loggedInUserId);
                }

                return Ok(new
                {
                    success = true,
                    message = "Shelving checklist details saved successfully",
                    created = createdCount,
                    updated = updatedCount,
                    total = createdCount + updatedCount
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_DeleteShelvingCheckListDetail")]
        public IHttpActionResult DeleteShelvingCheckListDetail([FromBody] DeleteDetailRequest request)
        {
            try
            {
                if (request == null || request.id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                var loggedInUserId = request.loggedInUserId ?? "mobile_user";

                var result = DatabaseHelper.DeleteShelvingCheckListDetail(request.id, loggedInUserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Shelving checklist detail deleted successfully" });
                }
                return BadRequest("Failed to delete shelving checklist detail");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListDetailsByCheckListId")]
        public IHttpActionResult GetShelvingCheckListDetailsByCheckListId(int checkListId)
        {
            try
            {
                if (checkListId <= 0)
                {
                    return BadRequest("Invalid checklist id");
                }

                var result = DatabaseHelper.GetShelvingCheckListDetailsByCheckListId(checkListId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region ShelvingCheckListPhoto CRUD

        [HttpPost]
        [Route("ShelvingCheckList_SaveShelvingCheckListPhotos")]
        public IHttpActionResult SaveShelvingCheckListPhotos()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;

                var checkListIdStr = httpRequest.Form["ShelvingCheckListId"];
                var loggedInUserId = httpRequest.Form["loggedInUserId"] ?? "mobile_user";

                if (string.IsNullOrEmpty(checkListIdStr))
                {
                    return BadRequest("ShelvingCheckListId is required");
                }

                long shelvingCheckListId = long.Parse(checkListIdStr);

                if (httpRequest.Files.Count == 0)
                {
                    return BadRequest("No files uploaded");
                }

                // Create physical folder path: img/ShelvingCheckList/
                var uploadPath = HttpContext.Current.Server.MapPath("~/img/ShelvingCheckList");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var uploadedPhotos = new List<ShelvingCheckListPhoto>();

                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    var file = httpRequest.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        // Generate unique filename with timestamp
                        var fileExtension = Path.GetExtension(file.FileName);
                        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                        var physicalPath = Path.Combine(uploadPath, uniqueFileName);

                        // Save physical file
                        file.SaveAs(physicalPath);

                        // Save to database - store only filename, not full path
                        var photo = new ShelvingCheckListPhoto
                        {
                            ShelvingCheckListId = shelvingCheckListId,
                            ShelvingCheckListName = Path.GetFileNameWithoutExtension(file.FileName),
                            ShelvingCheckListPath = uniqueFileName, // Just filename
                            CreatedBy = loggedInUserId,
                            CreatedDate = DateTime.Now
                        };

                        var photoId = DatabaseHelper.CreateShelvingCheckListPhoto(photo, loggedInUserId);
                        if (photoId > 0)
                        {
                            photo.ShelvingCheckListPhotoId = photoId;
                            uploadedPhotos.Add(photo);
                        }
                    }
                }

                if (uploadedPhotos.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Photos uploaded successfully",
                        photos = uploadedPhotos,
                        count = uploadedPhotos.Count
                    });
                }
                return BadRequest("Failed to upload photos");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ShelvingCheckList_DeleteShelvingCheckListPhoto")]
        public IHttpActionResult DeleteShelvingCheckListPhoto([FromBody] DeletePhotoRequest request)
        {
            try
            {
                if (request == null || request.id <= 0)
                {
                    return BadRequest("Invalid id");
                }

                // Get photo info before deleting
                var photo = DatabaseHelper.GetShelvingCheckListPhotoById(request.id);
                if (photo != null && !string.IsNullOrEmpty(photo.ShelvingCheckListPath))
                {
                    // Delete physical file
                    var filePath = HttpContext.Current.Server.MapPath("~/img/ShelvingCheckList/" + photo.ShelvingCheckListPath);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    var filePathThump = HttpContext.Current.Server.MapPath("~/img/Shelvingchecklistthumb/" + photo.ShelvingCheckListPath);
                    if (File.Exists(filePathThump))
                    {
                        File.Delete(filePathThump);
                    }
                }

                // Delete from database
                var result = DatabaseHelper.DeleteShelvingCheckListPhoto(request.id);
                if (result)
                {
                    return Ok(new { success = true, message = "Photo deleted successfully" });
                }
                return BadRequest("Failed to delete photo");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListPhotosByCheckListId")]
        public IHttpActionResult GetShelvingCheckListPhotosByCheckListId(int checkListId)
        {
            try
            {
                if (checkListId <= 0)
                {
                    return BadRequest("Invalid checklist id");
                }

                var result = DatabaseHelper.GetShelvingCheckListPhotosByCheckListId(checkListId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Combined Data Retrieval

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListCompleteDataByInspectionId")]
        public IHttpActionResult GetShelvingCheckListCompleteDataByInspectionId(long inspectionId)
        {
            try
            {
                if (inspectionId <= 0)
                {
                    return BadRequest("Invalid inspection id");
                }

                var checklists = DatabaseHelper.GetShelvingCheckListsByInspectionId(inspectionId);
                var result = new List<object>();

                foreach (var checklist in checklists)
                {
                    var details = DatabaseHelper.GetShelvingCheckListDetailsByCheckListId(checklist.ShelvingCheckListId);
                    var photos = DatabaseHelper.GetShelvingCheckListPhotosByCheckListId(checklist.ShelvingCheckListId);

                    result.Add(new
                    {
                        checklist = checklist,
                        details = details,
                        photos = photos
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("ShelvingCheckList_GetShelvingCheckListCompleteData")]
        public IHttpActionResult GetShelvingCheckListCompleteData(int checkListId)
        {
            try
            {
                if (checkListId <= 0)
                {
                    return BadRequest("Invalid checklist id");
                }

                var checklist = DatabaseHelper.GetShelvingCheckListById(checkListId);
                if (checklist == null)
                {
                    return NotFound();
                }

                var details = DatabaseHelper.GetShelvingCheckListDetailsByCheckListId(checkListId);
                var photos = DatabaseHelper.GetShelvingCheckListPhotosByCheckListId(checkListId);

                var result = new
                {
                    checklist = checklist,
                    details = details,
                    photos = photos
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Helper Classes for Requests

        public class DeleteRequest
        {
            public int id { get; set; }
            public string loggedInUserId { get; set; }
        }

        public class DeleteDetailRequest
        {
            public long id { get; set; }
            public string loggedInUserId { get; set; }
        }

        public class DeletePhotoRequest
        {
            public long id { get; set; }
            public string loggedInUserId { get; set; }
        }

        public class SaveDetailsRequest
        {
            public List<ShelvingCheckListDetail> details { get; set; }
            public string loggedInUserId { get; set; }
        }

        #endregion


        [HttpGet]
        [Route("demonotification")]
        [AllowAnonymous]
        public async Task<bool> DemoNotification(string strToken)
        {
            try
            {
                FirebaseHelper firebaseService = new FirebaseHelper();
                FirebaseHelper.InitializeFirebase();
                //if (objUser.DeviceType == "AP")
                //{
                //    await firebaseService.SendIOSNotificationAsync(objUser.UserToken.Trim(), "CAM Industrial ", strMessage);
                //}
                //else
                //{
                //await firebaseService.SendAndroidNotificationAsync("erCqmomnzEn8uocCRNRUSs:APA91bG_n44NZizEcVwN-tW5cJGIv4UT0ia2UsAFEfXaOD8SBiJJ8QSfgj8UO36y5UeKMRwHDJBbAJ38MYC0Yd_PtrXi2YOiNIt2moWOFgDqGHDiBxe1C-s", "CAM Industrial ", "Demo Notification");
                await firebaseService.SendAndroidNotificationAsync(strToken, "CAM Industrial ", "Demo Notification");
                //}
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }

        private static ConcurrentBag<StreamWriter> clients;
        static PageViewController()
        {
            clients = new ConcurrentBag<StreamWriter>();
        }

        public async Task PostAsync(Notification m)
        {
            m.CreatedDate = DateTime.Now;
            await ChatCallbackMsg(m);
        }
        private async Task ChatCallbackMsg(Notification m)
        {
            foreach (var client in clients)
            {
                try
                {
                    var data = string.Format("data:{0}|{1}|{2}\n\n", m.Userid_SenderID, m.NotificationText, m.CreatedDate);
                    await client.WriteAsync(data);
                    await client.FlushAsync();
                    client.Dispose();
                }
                catch (Exception)
                {
                    StreamWriter ignore;
                    clients.TryTake(out ignore);
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage Subscribe(HttpRequestMessage request)
        {
            var response = request.CreateResponse();
            response.Content = new PushStreamContent((a, b, c) =>
            { OnStreamAvailable(a, b, c); }, "text/event-stream");
            return response;
        }

        private void OnStreamAvailable(Stream stream, HttpContent content,
            TransportContext context)
        {
            var client = new StreamWriter(stream);
            clients.Add(client);
        }

        // ============================================================
        // PageViewController - Internal Inspection Module
        // Endpoints only — no business logic
        // ============================================================

        // ============================================================
        // PageViewController - Internal Inspection Module
        // Endpoints only — no business logic
        // ============================================================

        #region "Internal Inspection"

        // ---- 1. Get all inspections for a customer (Customer listing page) ----
        // JS: GET /api/pageview/getInternalInspectionsByCustomerId?customerId=123
        [Route("getInternalInspectionsByCustomerId")]
        [HttpGet]
        public IHttpActionResult GetInternalInspectionsByCustomerId(long customerId)
        {
            try
            {
                var result = DatabaseHelper.GetInternalInspectionsByCustomerId(customerId);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 1b. Get inspections for currently logged-in customer (session-resolved) ----
        // JS: GET /api/pageview/getMyInternalInspections
        // Optional filter params: CustomerLocationID, CustomerFacilityID, AreaID, Region
        [Route("getMyInternalInspections")]
        [HttpGet]
        public IHttpActionResult GetMyInternalInspections(
            long? CustomerLocationID = null,
            long? CustomerFacilityID = null,
            long? AreaID = null,
            string Region = null)
        {
            try
            {
                var result = DatabaseHelper.GetInternalInspectionsByCurrentCustomer(
                    CustomerLocationID, CustomerFacilityID, AreaID, Region);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 2. Get all inspections for Admin/Employee (with filters) ----
        // JS: POST /api/pageview/getInternalInspectionsForAdmin
        [Route("getInternalInspectionsForAdmin")]
        [HttpPost]
        public IHttpActionResult GetInternalInspectionsForAdmin(InternalInspectionSearchViewModel filter)
        {
            try
            {
                var result = DatabaseHelper.GetInternalInspectionsForAdmin(filter);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 3. Get single inspection detail (View page) ----
        // JS: GET /api/pageview/getInternalInspectionById?id=456
        [Route("getInternalInspectionById")]
        [HttpGet]
        public IHttpActionResult GetInternalInspectionById(long id)
        {
            try
            {
                var result = DatabaseHelper.GetInternalInspectionById(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 4. Save new or update existing inspection ----
        // JS: POST /api/pageview/saveInternalInspection
        [Route("saveInternalInspection")]
        [HttpPost]
        public IHttpActionResult SaveInternalInspection(SaveInternalInspectionViewModel model)
        {
            try
            {
                var result = DatabaseHelper.SaveInternalInspection(model);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 5. Upload photos for an inspection ----
        // JS: POST /api/pageview/saveInternalInspectionPhotos
        // Accepts multipart/form-data — file handling logic lives in DatabaseHelper
        [Route("saveInternalInspectionPhotos")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveInternalInspectionPhotos()
        {
            try
            {
                var result = await DatabaseHelper.SaveInternalInspectionPhotos(Request);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 6. Soft delete an inspection ----
        // JS: POST /api/pageview/deleteInternalInspection
        [Route("deleteInternalInspection")]
        [HttpPost]
        public IHttpActionResult DeleteInternalInspection([FromBody] DeleteInspectionRequest request)
        {
            try
            {
                var result = DatabaseHelper.DeleteInternalInspection(request.id);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- 7. Update inspection status (Admin/Employee only) ----
        // JS: POST /api/pageview/updateInternalInspectionStatus
        [Route("updateInternalInspectionStatus")]
        [HttpPost]
        public IHttpActionResult UpdateInternalInspectionStatus([FromBody] UpdateInspectionStatusRequest request)
        {
            try
            {
                var result = DatabaseHelper.UpdateInternalInspectionStatus(request.id, request.status);
                return Ok(result);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        // ---- Request helper classes ----
        public class DeleteInspectionRequest
        {
            public long id { get; set; }
        }

        public class UpdateInspectionStatusRequest
        {
            public long id { get; set; }
            public string status { get; set; }
        }

        #endregion



    }
}

