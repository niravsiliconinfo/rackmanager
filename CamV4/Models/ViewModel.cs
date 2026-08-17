using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;

namespace CamV4.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class UserEmployeeViewModel
    {
        public long UserID { get; set; }
        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter user password.")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "Please enter user type.")]
        public Nullable<int> UserType { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> UserStatus { get; set; }
        public string UserToken { get; set; }
        public string DeviceType { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeEmail { get; set; }
        [Required(ErrorMessage = "Please enter employee name.")]
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string MobileNo { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> CountryID { get; set; }

        public string CityName { get; set; }
        public string ProvianceName { get; set; }
        public string CountryName { get; set; }

        public string PinCode { get; set; }
        public Nullable<int> Gender { get; set; }
        public string TitleDegrees { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> IsStampingEngineer { get; set; }
    }

    public class EmployeeSalesViewModel
    {
        public long EmployeeSalesID { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public long UserID { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeSalesName { get; set; }
        public string EmployeeAddress { get; set; }
        public string CityName { get; set; }
        public string ProvianceName { get; set; }
        public string CountryName { get; set; }
        public string PinCode { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Gender { get; set; }
        public string TitleDegrees { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string SalesCompanyListing { get; set; }
    }

    public class EmployeeViewModel
    {
        public long EmployeeID { get; set; }
        public long UserID { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string CityName { get; set; }
        public string ProvianceName { get; set; }
        public string CountryName { get; set; }
        public string PinCode { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Gender { get; set; }
        public string TitleDegrees { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        
    }
    public class UserViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public Nullable<int> UserType { get; set; }
        public bool UserStatus { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public partial class CustomerLocationViewModel
    {
        public long CustomerLocationID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
        public string CustomerAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Region { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public partial class CustomerViewModel
    {
        public long CustomerID { get; set; }
        public long UserID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLogo { get; set; }
        public string CustomerAddress { get; set; }
        public string CityID { get; set; }
        public string CityName { get; set; }
        public string ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string CustomerNAVNo { get; set; }
        public string CustomerContactName { get; set; }
        public string PinCode { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        User user { get; set; }
        public string CustomerFullPathLogo { get; set; }
        public string CustomerPharse { get; set; }
        public string CustomerFullAddress { get; set; }   

    }

    public partial class CustAndCustomerLocationViewModel
    {
        public long CustomerID { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerWebsite { get; set; }
        public string CustomerLogo { get; set; }
        public string CustomerNAVNo { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContactName { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> Country { get; set; }
        public string PinCode { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public List<CustomerLocationViewModel> CustomerLocation { get; set; }
        public string CustomerFullPathLogo { get; set; }
        public string CustomerPharse { get; set; }
        public Nullable<int> SalesRepresentativeId { get; set; }
    }

    public partial class CustomerAreaViewModel
    {
        public long AreaID { get; set; }
        public string Customer { get; set; }
        public long CustomerID { get; set; }
        public long CustomerLocationID { get; set; }  
        public long CustomerFacilityID { get; set; }
        public string CustomerLocation { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string AreaName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    public partial class CustomerFacilityViewModel
    {
        public long CustomerFacilityID { get; set; }
        public long CustomerLocationID { get; set; }
        public string Customer { get; set; }
        public long CustomerID { get; set; }
        public string CustomerLocation { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string FacilityName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    
    public partial class CustomerLocationContactViewModel
    {
        public long LocationContactId { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public long CustomerId { get; set; }
        public string Customer { get; set; }
        public long CustomerLocationID { get; set; }
        public string CustomerLocation { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool Selected { get; set; }
        public string LocationIds { get; set; }
        public string FacilityIds { get; set; } 
        public string AreaIds { get; set; }
        public List<long> LinkedCustomerLocationIDs { get; set; }
        public List<long> LinkedCustomerUserLocationIds { get; set; }
        public string LinkedLocationNames { get; set; }
        public string LinkedFacilityNames { get; set; }
        public string LinkedAreaNames { get; set; }

        public List<long> LinkedFacilityIDs { get; set; }
        public List<long> LinkedAreaIDs { get; set; }
    }


    public partial class CustomerLocationContactViewModelList
    {
        public long CustomerLocationID { get; set; }
        public string CustomerLocation { get; set; }
        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }        
    }

    public class InspectionViewModel
    {
        public long InspectionId { get; set; }
        public string InspectionDocumentNo { get; set; }
        public string InspectionDocumentNoRef { get; set; }
        public string InspectionType { get; set; }
        public string InspectionTypeCode { get; set; }
        public System.DateTime InspectionDate { get; set; }
        public string InspectionDateFormatted { get; set; }
        public Nullable<System.DateTime> Reportdate { get; set; }
        public int InspectionStatus { get; set; }
        public string InspectionStatusName { get; set; }
        public Nullable<System.DateTime> InspectionStartedOn { get; set; }
        public Nullable<System.DateTime> InspectionEndOn { get; set; }
        public long CustomerId { get; set; }
        public string Customer { get; set; }
        public string CustomerContactName { get; set; }
        public long CustomerLocationId { get; set; }
        public string CustomerLocation { get; set; }
        public string Region { get; set; }
        public Nullable<long> CustomerAreaID { get; set; }
        public string CustomerArea { get; set; }
        public Nullable<long> CustomerFacilityID { get; set; }
        public string CustomerFacility { get; set; }
        public string CustomerFullAddress { get; set; }
        public string CustomerLogo { get; set; }
        public string Employee { get; set; }
        public long EmployeeId { get; set; }
        public string FacilitiesAreasIds { get; set; }
        public Nullable<int> isShelvingCheckLists { get; set; }
        public string ShelvingChecklistComments { get; set; }
        public string ProcessOverviewsIds { get; set; }
        public string FacilitiesAreas { get; set; }
        public string ProcessOverviews { get; set; }
        public string ReferenceDocumentIds { get; set; }
        public string ReferenceDocuments { get; set; }
        public string CustomerContactIds { get; set; }
        public string CADDocuments { get; set; }
        public string ConclusionRecommendationss { get; set; }
        public string InspectionPDFPath { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string InspectionDeficiencyAdminStatus { get; set; }
        public Nullable<long> StampingEngineerId { get; set; }
        public List<InspectionDeficiencyViewModel> iDefModel { get; set; }
        public Deficiency defModel { get; set; }
        public Customer custModel = new Customer();
        public Employee empModel = new Employee();
        public Employee empStampingEngModel = new Employee();
        public List<FacilitiesArea> InspectionFacilitiesArea { get; set; }

        public List<CustomerLocationContactViewModel> ListCustomerLocationContacts { get; set; }
        public List<ProcessOverview> InspectionProcessOverview { get; set; }
        public List<DocumentTitle> InspectionDocumentTitle { get; set; }
        public List<InspectionDeficiencyMTOViewModel> iMTOModel { get; set; }
        public List<Deficiency> ListConclusionandRecommendationsViewModel { get; set; }
        public List<InspectionFileDrawingViewModel> ListInspectionFileDrawing { get; set; }

        public Nullable<int> CapacityTable { get; set; }
        public Nullable<int> PlanElevationDrawing { get; set; }
        public Quotation objQuotation { get; set; }
        public List<ShelvingCheckListViewModel> ShelvingCheckLists { get; set; }
    }

    #region "ShelvingCheckList"

    public class ShelvingCheckListViewModel
    {
        public long ShelvingCheckListId { get; set; }
        public long InspectionId { get; set; }
        public string ShelvingCheckListName { get; set; }
        public string ShelvingCheckListSection { get; set; }
        public string ShelvingCheckListTypeName { get; set; }
        public string ShelvingCheckListManufacturer { get; set; }
        public string ShelvingCheckListRowId { get; set; }
        public string ShelvingCheckListBays { get; set; }
        public string ShelvingCheckListNoOfShelvesBay { get; set; }
        public string ShelvingCheckListSize { get; set; }
        public string ShelvingCheckListPost { get; set; }
        public string ShelvingCheckListPostType { get; set; }
        public string ShelvingCheckListShelfBeam { get; set; }
        public string ShelvingCheckListFrameConnector { get; set; }
        public string ShelvingCheckListTieBar { get; set; }
        public string ShelvingCheckListCapacityForBay { get; set; }
        public string ShelvingCheckListCapacityforShelf { get; set; }

        public List<ShelvingCheckListDetailViewModel> ShelvingCheckListDetails { get; set; }
        public List<ShelvingCheckListPhotoViewModel> ShelvingCheckListPhotos { get; set; }

        public ShelvingCheckListViewModel()
        {
            ShelvingCheckListDetails = new List<ShelvingCheckListDetailViewModel>();
            ShelvingCheckListPhotos = new List<ShelvingCheckListPhotoViewModel>();
        }
    }

    public class ShelvingCheckListDetailViewModel
    {
        public long ShelvingCheckListDetailId { get; set; }
        public long ShelvingCheckListId { get; set; }
        public int ShelvingCheckListDeficiencyID { get; set; }
        public string ShelvingCheckListDeficiencyInfo { get; set; }
        public bool Tick_Yes { get; set; }
        public bool Tick_No { get; set; }
        public bool Tick_NA { get; set; }
        public string ShelvingCheckListDeficiencyComment { get; set; }
    }

    public class ShelvingCheckListPhotoViewModel
    {
        public long ShelvingCheckListPhotoId { get; set; }
        public long ShelvingCheckListId { get; set; }
        public string ShelvingCheckListPath { get; set; }
        public string ShelvingCheckListPathFull { get; set; }
        public string ShelvingCheckListPathFullThumb { get; set; }
    }

    public class ShelvingCheckListSaveModel
    {
        public string loggedInUserId { get; set; }
        public ShelvingCheckList ShelvingCheckList { get; set; }
        public List<ShelvingCheckListDetail> ShelvingCheckListDetails { get; set; }
        public List<ShelvingCheckListPhotoBase64> ShelvingCheckListPhotos { get; set; }
    }

    public class ShelvingCheckListPhotoBase64
    {
        public long ShelvingCheckListPhotoId { get; set; }  // 0 = new, >0 = existing
        public string PhotoBase64 { get; set; }             // Base64 string from mobile
        public bool ShouldDelete { get; set; }              // true = delete this photo
    }

    public class SaveShelvingCheckListResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public long ShelvingCheckListId { get; set; }
    }

    #endregion

    public class IncidentViewModel
    {
        public long IncidentReportId { get; set; }
        public string IncidentType { get; set; }
        public long CustomerId { get; set; }
        public long? CustomerLocationId { get; set; }
        public string LocationName { get; set; }
        public string CustomerAddress { get; set; }
        public int? CityID { get; set; }
        public int? ProvinceID { get; set; }
        public int? CountryID { get; set; }
        public string Region { get; set; }
        public DateTime IncidentDate { get; set; }
        public string IncidentNumber { get; set; }
        public string IncidentReportedBy { get; set; }
        public string IncidentArea { get; set; }
        public string IncidentRow { get; set; }
        public string IncidentAisle { get; set; }
        public string IncidentBay { get; set; }
        public string IncidentLevel { get; set; }
        public string IncidentBeamLocation { get; set; }
        public string IncidentFrameSide { get; set; }
        public string IncidentSummary { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class IncidentReportViewModel
    {
        public long IncidentReportId { get; set; }

        // Header info
        public string BusinessName { get; set; }
        public string LogoUrl { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public string Region { get; set; }
        public string RackType { get; set; } // Optional, based on CustomerLocation

        // Location details
        public string Area { get; set; }
        public string Row { get; set; }
        public string Aisle { get; set; }
        public string Bay { get; set; }
        public string Level { get; set; }
        public string BeamLocation { get; set; }
        public string FrameSide { get; set; }

        // Reporter & summary
        public string ReportedBy { get; set; }
        public string Summary { get; set; }

        // Footer info
        public DateTime? IncidentDate { get; set; }
        public string IncidentNumber { get; set; }

        // Photos
        public List<IncidentReportPhotoViewModel> Photos { get; set; }
    }

    public class IncidentReportPhotoViewModel
    {
        public string IncidentPhotoPath { get; set; }
    }


    public partial class InspectionDueViewModel
    {
        public long InspectionDueId { get; set; }
        public System.DateTime ScheduledDate { get; set; }
        public long AssignedEmployeeID { get; set; }
        public string Employee { get; set; }
        public string Customer { get; set; }
        public string CustomerLocation { get; set; }
        public string CustomerArea { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public partial class InspectionCloneViewModel
    {
        public long InspectionId { get; set; }
        public string InspectionDocumentNo { get; set; }
        public string InspectionDocumentNoRef { get; set; }
        public string InspectionType { get; set; }
        public System.DateTime InspectionDate { get; set; }
        public string InspectionDateFormatted { get; set; }
        public int InspectionStatus { get; set; }
        public string InspectionStatusName { get; set; }
        public string Customer { get; set; }
        public string CustomerLocation { get; set; }
        public string CustomerArea { get; set; }

    }
    public class InspectionFileDrawingViewModel
    {
        public long InspectionFileDrawingId { get; set; }
        public string InspectionId { get; set; }
        public string FileDrawingPath { get; set; }
        public string FileDrawingName { get; set; }
        public string FileCategory { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }

        public List<InspectionFileDrawingChildViewModel> inspectionFileDrawingChildViewModels { get; set; }
    }
    public class InspectionFileDrawingChildViewModel
    {
        public long InspectionFileDrawingId { get; set; }
        public string InspectionId { get; set; }
        public string FileDrawingPath { get; set; }
        public string FileDrawingName { get; set; }
        public string FileCategory { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
    //public partial class FacilitiesAreaViewModel
    //{
    //    public int FacilitiesAreaId { get; set; }
    //    public string FacilitiesAreaName { get; set; }
    //    public string FacilitiesAreaDesc { get; set; }
    //    public Nullable<bool> IsActive { get; set; }
    //    public Nullable<System.DateTime> CreatedDate { get; set; }
    //    public string CreatedBy { get; set; }
    //    public Nullable<System.DateTime> ModifiedDate { get; set; }
    //    public string ModifiedBy { get; set; }
    //}
    public partial class InspectionDeficiencyViewModel
    {
        public Int16 RowNo { get; set; }
        public long InspectionDeficiencyId { get; set; }
        public long InspectionId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CustomerNomenclatureNo { get; set; }
        public string CustomerNomenclatureBayNoID { get; set; }
        public string BayFrameSide { get; set; }
        public string BeamFrameLevel { get; set; }
        public Int32 ConclusionRecommendationsID { get; set; }
        public string ConclusionRecommendationsTitle { get; set; }
        public Int32 DeficiencyID { get; set; }
        public string DeficiencyType { get; set; }
        public string DeficiencyInfo { get; set; }
        public string DeficiencyDesc { get; set; }
        public Nullable<bool> Action_ReferReport { get; set; }
        public Nullable<bool> Action_Monitor { get; set; }
        public Nullable<bool> Action_Replace { get; set; }
        public Nullable<bool> Action_Repair { get; set; }
        public Nullable<int> Severity_IndexNo { get; set; }
        public string ActionTaken { get; set; }
        public Nullable<int> InspectionDeficiencyTechnicianStatus { get; set; }
        public string InspectionDeficiencyTechnicianRemark { get; set; }
        public Nullable<int> InspectionDeficiencyAdminStatus { get; set; }
        public string InspectionDeficiencyTechnicianStatusText { get; set; }
        public string InspectionDeficiencyAdminStatusText { get; set; }
        public Nullable<int> InspectionDeficiencyRequestQuotation { get; set; }
        public Nullable<int> selectedReqQuote { get; set; }
        public Nullable<int> InspectionDeficiencyApprovedQuotation { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<InspectionDeficiencyPhotoViewModel> InspectionDeficiencyPhotoViewModel { get; set; }

        public List<InspectionDeficiencyPhotoTechnicianViewModel> InspectionDeficiencyPhotoTechnicianViewModel { get; set; }

        public List<InspectionDeficiencyMTOViewModel> InspectionDeficiencyMTO { get; set; }

    }

    public partial class ComponentSavedViewModel
    {
        public long ComponentSavedId { get; set; }
        public long ComponentId { get; set; }
        public long ComponentManufacturerId { get; set; }
        public long CustomerId { get; set; }
        public long CustomerLocationID { get; set; }
        public string ComponentSavedFullName { get; set; }
        public string Size_Description { get; set; }
        public string Size_DescriptionOriginal { get; set; }
        public string Size_DescriptionShort { get; set; }
        public string Size_DescriptionShortOriginal { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public List<ComponentSavedDetailViewModel> ComponentSavedDetailViewModel { get; set; }
    }
    public partial class ComponentSavedDetailViewModel
    {
        public long ComponentSavedDetailId { get; set; }
        public long ComponentSavedId { get; set; }
        public int ComponentPropertyTypeId { get; set; }
        public string ComponentPropertyType { get; set; }
        public int ComponentPropertyValueId { get; set; }
        public string ComponentPropertyValue { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class InspectionDeficiencyPhotoViewModel
    {
        public string base64DeficiencyPhotoImage { get; set; }
        public string DeficiencyPhoto { get; set; }
        public string DeficiencyPhotoThumb { get; set; }
        public bool InspectionDeficiencyIsStatus { get; set; }
    }
    public class InspectionDeficiencyPhotoTechnicianViewModel
    {
        public string base64DeficiencyPhotoImage { get; set; }
        public string DeficiencyPhoto { get; set; }
        public string DeficiencyPhotoThumb { get; set; }
        public bool InspectionDeficiencyIsStatus { get; set; }
    }
    public partial class InspectionDeficiencyMTOViewModel
    {
        public Int16 DeficiencyRowNo { get; set; }
        public Nullable<int> Severity_IndexNo { get; set; }
        public long InspectionDeficiencyMTOId { get; set; }
        public long InspectionDeficiencyId { get; set; }
        public long ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentImage { get; set; }
        public Nullable<long> ManufacturerId { get; set; }
        public String ManufacturerName { get; set; }
        public string VendorID { get; set; }
        public string CAMID { get; set; }
        public string Type { get; set; }
        public string Size_Description { get; set; }
        public string Size_DescriptionShort { get; set; }
        public int QuantityReq { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<long> ComponentSavedId { get; set; }
        public List<InspectionDeficiencyMTODetailViewModel> iMTOModelDetails { get; set; }
    }


    public partial class InspectionDeficiencyMTODetailViewModel
    {
        public long InspectionDeficiencyMTODetailId { get; set; }
        public long InspectionDeficiencyMTOId { get; set; }
        public int ComponentPropertyTypeId { get; set; }
        public string ComponentPropertyTypeName { get; set; }
        public string ComponentPropertyTypeDesctiption { get; set; }
        public int ComponentPropertyValueId { get; set; }
        public string ComponentPropertyValue { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
    public partial class ComponentViewModel
    {
        public long ComponentPriceId { get; set; }
        public long ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string Manufacturer { get; set; }
        public string ComponentImage { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class AdminDashboardGraphViewModel
    {
        public List<InsepctionCount_Graph> Pie { get; set; }
        //public List<Get_DeficienciesBySeverityCustomerNew_Result> PieCusomter { get; set; }
        public List<InsepctionCount_Graph> PieYear { get; set; }
        public List<sp_getEmpInspection_Count_New_Result> LineDone { get; set; }
        public List<sp_getApprovedInspection_Count_New_Result> LineApproved { get; set; }
        public long InspectionDueCount { get; set; }
        public long InProgressCount { get; set; }
        public long SentforApprovalCount { get; set; }
        public long ApprovedCompletedCount { get; set; }
        public long QuotationRequestedCount { get; set; }
        public long AwaitingApprovalCount { get; set; }
        public long QuotationApprovedCount { get; set; }
        public long RepairCompletedCount { get; set; }
        public long InspectionFinishedCount { get; set; }
        public long DashboardActiveUserCount { get; set; }
        public long DashboardActiveUserAdminCount { get; set; }
        public long DashboardActiveUserEmployeeCount { get; set; }
        public long DashboardActiveCompanyCount { get; set; }
        public long DashboardActiveInventoryCount { get; set; }
    }
    public class Dashboard
    {
        public long CustomerCount { get; set; }
        public long EmployeeCount { get; set; }
        public long AdminCount { get; set; }
        public long InspectorCount { get; set; }
        public long InspectionDueCount { get; set; }
        public long InProgressCount { get; set; }
        public long SentforApprovalCount { get; set; }
        public long ApprovedCompletedCount { get; set; }
        public long QuotationRequestedCount { get; set; }
        public long AwaitingApprovalCount { get; set; }
        public long QuotationApprovedCount { get; set; }
        public long RepairCompletedCount { get; set; }
        public long InspectionFinishedCount { get; set; }
        public long DashboardActiveUserCount { get; set; }
        public long DashboardActiveUserAdminCount { get; set; }
        public long DashboardActiveUserEmployeeCount { get; set; }
        public long DashboardActiveCompanyCount { get; set; }
    }

    public class InspectionCustomerViewModel
    {
        public long InspectionId { get; set; }
        public string InspectionDocumentNo { get; set; }
        public string InspectionType { get; set; }
        public System.DateTime InspectionDate { get; set; }
        public Nullable<System.DateTime> Reportdate { get; set; }
        public int InspectionStatus { get; set; }
        public Nullable<System.DateTime> InspectionStartedOn { get; set; }
        public Nullable<System.DateTime> InspectionEndOn { get; set; }
        public string Customer { get; set; }
        public string CustomerLocation { get; set; }
        public string Employee { get; set; }
        public string CustomerContactIds { get; set; }
        public string CADDocuments { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }



    public partial class ComponentPropertyTypeViewModel
    {
        public long ComponentId { get; set; }
        public int ComponentPropertyTypeId { get; set; }
        public string ComponentPropertyTypeName { get; set; }
        public string ComponentPropertyTypeDesctiption { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public partial class ComponentPropertyValueViewModel
    {
        public int ComponentPropertyValueId { get; set; }
        public string ComponentPropertyType { get; set; }
        public string ComponentPropertyValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class InsepctionCount_Graph
    {
        public long cnt { get; set; }
        public string InspectionStatusName { get; set; }
        public string InspectionStatusColor { get; set; }
    }

    public class MailViewModel
    {
        public long InspectionId { get; set; }
        public string LocationContactId { get; set; }
        public int SentToClient { get; set; }    
    }

    public class CustomerDashboardGraphViewModel
    {
        public List<Get_DeficienciesBySeverityCustomerNew_Result> PieCusomter { get; set; }
        public long InspectionDueCount { get; set; }
        public long InProgressCount { get; set; }
        public long SentforApprovalCount { get; set; }
        public long ApprovedCompletedCount { get; set; }
        public long QuotationRequestedCount { get; set; }
        public long AwaitingApprovalCount { get; set; }
        public long QuotationApprovedCount { get; set; }
        public long RepairCompletedCount { get; set; }
        public long InspectionFinishedCount { get; set; }
    }

    public class SaveImportFile
    {
        public string ComponentId { get; set; }
        public HttpPostedFileBase file { get; set; }
    }

    public class ComponentPriceListViewModel
    {
        public long ComponentPriceId { get; set; }
        public long ComponentId { get; set; }
        public string ComponentName { get; set; }
        public long ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ItemPartNo { get; set; }
        public string ComponentPriceDescription { get; set; }
        public Nullable<decimal> ComponentPrice { get; set; }
        public Nullable<decimal> ComponentLabourTime { get; set; }
        public Nullable<decimal> ComponentWeight { get; set; }

        public Nullable<decimal> Surcharge { get; set; }
        public Nullable<decimal> Markup { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public int iMatched { get; set; }
        public List<ComponentPriceListViewModelDetails> ComponentPriceListViewModelDetails { get; set; }
    }
    public class ComponentPriceListViewModelDetails
    {
        public long ComponentPriceDetailId { get; set; }
        public Nullable<long> ComponentPriceId { get; set; }
        public Nullable<int> ComponentPropertyTypeId { get; set; }
        public string ComponentPricePropertyTypeDescription { get; set; }
        public string ComponentPricePropertyValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ImpSettingsViewModel
    {
        public int SettingID { get; set; }
        public string SettingType { get; set; }
        public string SettingValue { get; set; }
    }

    public partial class AdminQuotation
    {
        public long QuotationId { get; set; }
        public long InspectionId { get; set; }
        public int QuotationStatus { get; set; }
        public string QuotationNo { get; set; }
        public long CustomerId { get; set; }
        public long CustomerLocationId { get; set; }
        public Nullable<long> CustomerAreaID { get; set; }
        public Nullable<System.DateTime> QuotationDate { get; set; }
        public string YourReference { get; set; }
        public string ValidTo { get; set; }
        public string PaymentTerms { get; set; }
        public string ShipmentMethod { get; set; }
        public Nullable<long> SalesPersonId { get; set; }
        public string SalesPersonName { get; set; }
        public Nullable<decimal> LabourUnitPrice { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> GSTPer { get; set; }
        public Nullable<decimal> GSTValue { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> TotalLabour { get; set; }
        public Nullable<decimal> TotalUnitPrice { get; set; }
        public string QuotationNotes { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public decimal QuotationSurcharge { get; set; }
        public decimal QuotationMarkup { get; set; }
        public List<QuotationItem> objQuotationItems { get; set; }
        public Nullable<bool> SendEmailForApproval { get; set; }
        public string LocationContactId { get; set; }
        public bool IsUpdateAll { get; set; }
    }

    public class SaveQuotationRequest
    {
        public long QuotationId { get; set; }
        public QuotationItem QuotationComponentList { get; set; }
        //public List<QuotationItem> QuotationComponentList { get; set; }
    }
    public class QuotationItemListPrepare
    {
        public long InspectionDeficiencyMTOId { get; set; }
        public long ComponentId { get; set; }
        public string ComponentName { get; set; }
        public Nullable<long> ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string Type { get; set; }
        public string Size_Description { get; set; }
        public List<InspectionDeficiencyMTOItemDetail> ItemDetails { get; set; }
        public bool isFound { get; set; }
        public string ItemPartNo { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<decimal> ItemUnitPrice { get; set; }
        public Nullable<decimal> ItemSurcharge { get; set; }
        public Nullable<decimal> ItemMarkup { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> ItemQuantity { get; set; }
        public Nullable<decimal> ItemLabour { get; set; }
        public Nullable<decimal> ItemWeight { get; set; }
        public Nullable<decimal> ItemWeightTotal { get; set; }
        public Nullable<decimal> LineTotal { get; set; }
        public Nullable<decimal> ItemLabourTotal { get; set; }
        public Nullable<bool> IsTBD { get; set; }
    }

    public class InspectionDeficiencyMTOItemDetail
    {
        public long ComponentPropertyTypeId { get; set; }
        public string ComponentPropertyTypeName { get; set; }
        public long ComponentPropertyValueId { get; set; }
        public string ComponentPropertyValue { get; set; }
    }

    public class ComponentPropertiesMatch
    {
        public List<ComponentPropertiesMatchList> objComponentPropertiesMatchList { get; set; }
    }
    public class ComponentPropertiesMatchList
    {
        public string ComponentPropertyType { get; set; }
        public string ComponentPropertyValue { get; set; }
    }
    public class PropertyMatch
    {
        public int PropertyTypeId { get; set; }
        public long PropertyValueId { get; set; }
        public string PropertyValue { get; set; }
    }
    public class FilterCustomerModel
    {
        public string InspectionTypeId { get; set; }
        public int Province { get; set; }
        public string Region { get; set; }
        public int City { get; set; }
        public string Location { get; set; }
        public string Facility { get; set; }
        public string Area { get; set; }
        public List<string> SelectedStatuses { get; set; }
        public string facilityId { get; set; }
        public string areaId { get; set; }
    }

    public class FilterFilesModel
    {
        public bool InspectionDocs { get; set; }
        public bool HistoricalDocs { get; set; }
        public int Province { get; set; }
        public string Region { get; set; }
        public int City { get; set; }
        public string Location { get; set; }
        public string Facility { get; set; }
        public List<string> DocumentTypeList { get; set; }
    }

    public class CustomerLocationHistoryLegacyFileListing
    {
        public long CustomerLocationHistoryLegacyFileId { get; set; }
        public long CustomerId { get; set; }
        public string InspectionDocumentNo { get; set; }
        public string CustomerLocationName { get; set; }
        public string Region { get; set; }

        public string FileDrawingPath { get; set; }
        public string FileDrawingName { get; set; }
        public string FileCategory { get; set; }
        public string CustomerName { get; set; }
        public Nullable<long> CustomerLocationID { get; set; }
    }

    public class CustomerRegion
    {
        public string CustRegion { get; set; }
    }

    #region "For Inspection DataTable "
    public class DataTableAjaxPostModel
    {
        public int draw { get; set; }
        public int start { get; set; } // Offset
        public int length { get; set; } // Page size
        public DataTableSearch search { get; set; }
        public List<DataTableOrder> order { get; set; }
        public List<DataTableColumn> columns { get; set; }
    }

    public class DataTableSearch
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class DataTableOrder
    {
        public int column { get; set; }
        public string dir { get; set; } // asc or desc
    }

    public class DataTableColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DataTableSearch search { get; set; }
    }

    public class CityViewModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    }

    public class DeleteFileModel
    {
        public string FileName { get; set; }
        public long CustomerId { get; set; }
        public long CustomerLocationID { get; set; }
        public int customerID { get; set; }
        public string CustomerLocation { get; set; }
    }
    #endregion

    //  Dropdown item models 

    public class CustDash_LocationItem
    {
        public long CustomerLocationID { get; set; }
        public string LocationName { get; set; }
    }

    public class CustDash_FacilityItem
    {
        public long CustomerFacilityID { get; set; }
        public string FacilityName { get; set; }
        public long CustomerLocationID { get; set; }
    }

    //  Graph result models 

    public class CustDash_CategoryBreakdownItem
    {
        public string DeficiencyCategory { get; set; }
        public int MinorCnt { get; set; }
        public int IntermediateCnt { get; set; }
        public int MajorCnt { get; set; }
        public int TotalCnt { get; set; }
    }

    public class CustDash_PieItem
    {
        public string Classifications { get; set; }
        public string ClassificationsColor { get; set; }
        public int InspectionDeficiencyCnt { get; set; }
    }

    public class CustDash_TrendItem
    {
        public int Year { get; set; }
        public int Minor { get; set; }
        public int Intermediate { get; set; }
        public int Major { get; set; }
    }

    public class CustDash_StatusCountItem
    {
        public int InspectionsDue { get; set; }
        public int InProgress { get; set; }
        public int SentForApproval { get; set; }
        public int ReportComplete { get; set; }
        public int QuotationRequested { get; set; }
        public int AwaitingApproval { get; set; }
        public int QuotationApproved { get; set; }
        public int RepairCompleted { get; set; }
        public int Finished { get; set; }
    }

    //  Main ViewModel returned by API 

    public class CustDash_DashboardDataViewModel
    {
        public int SelectedYear { get; set; }
        public long? SelectedLocationId { get; set; }
        public long? SelectedFacilityId { get; set; }

        public List<CustDash_CategoryBreakdownItem> CategoryBreakdown { get; set; }
        public List<CustDash_PieItem> PieData { get; set; }
        public List<CustDash_TrendItem> TrendData { get; set; }
        public CustDash_StatusCountItem StatusCounts { get; set; }

        public CustDash_DashboardDataViewModel()
        {
            CategoryBreakdown = new List<CustDash_CategoryBreakdownItem>();
            PieData = new List<CustDash_PieItem>();
            TrendData = new List<CustDash_TrendItem>();
            StatusCounts = new CustDash_StatusCountItem();
        }
    }

    public class BulkSaveResult
    {
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public List<DeficiencySaveResult> Results { get; set; }
    }

    public class DeficiencySaveResult
    {
        public bool IsSuccess { get; set; }
        public long InspectionDeficiencyId { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class IdModel
    {
        public long id { get; set; }
    }
    

    // ============================================================
    // Internal Inspection Module - ViewModels final
    // ============================================================

    public partial class InternalInspectionViewModel
    {
        public long InternalInspectionID { get; set; }
        public string InternalInspectionNumber { get; set; }
        public Nullable<DateTime> InternalInspectionDate { get; set; }
        public string InternalInspectionDateFormatted { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string LogoUrl { get; set; }
        public long CustomerLocationID { get; set; }
        public string CustomerLocationName { get; set; }
        public string CustomerLocationAddress { get; set; }
        public string Region { get; set; }
        public Nullable<long> CustomerFacilityID { get; set; }
        public string CustomerFacilityName { get; set; }
        public Nullable<long> CustomerAreaID { get; set; }
        public string CustomerAreaName { get; set; }
        public string ReportedBy { get; set; }  // stores userId
        public string ReportedByName { get; set; }  // resolved display name
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }  // resolved display name
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        // Child collections
        public List<InternalInspectionDeficiencyViewModel> Deficiencies { get; set; }

        // Computed
        public int DeficiencyCount { get; set; }
        public int EngineerReviewCount { get; set; }
        public decimal EngineerReviewTotalCost { get; set; }
        public int MinorCount { get; set; }
        public int ModerateCount { get; set; }
        public int SevereCount { get; set; }
    }

    public partial class InternalInspectionDeficiencyViewModel
    {
        public long InternalInspectionDeficiencyID { get; set; }
        public long InternalInspectionID { get; set; }
        public string Area { get; set; }
        public string Row { get; set; }
        public string Aisle { get; set; }
        public string Bay { get; set; }
        public string BeamFrameLevel { get; set; }
        public string BeamLocation { get; set; }
        public string FrameSide { get; set; }
        public string InternalAssessment { get; set; }
        public string InternalAction { get; set; }
        public string RecommendedAction { get; set; }
        public bool IsEngineerReviewRequested { get; set; }
        public Nullable<decimal> EngineerReviewCost { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public List<InternalInspectionPhotoViewModel> Photos { get; set; }
    }

    public partial class InternalInspectionPhotoViewModel
    {
        public long InternalInspectionPhotoID { get; set; }
        public long InternalInspectionDeficiencyID { get; set; }
        public long InternalInspectionID { get; set; }
        public string PhotoPath { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoFullUrl { get; set; }
        public string PhotoThumbFullUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }

    public partial class InternalInspectionSearchViewModel
    {
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> CustomerLocationID { get; set; }
        public string Status { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string InspectionNumber { get; set; }
    }

    #region "Training Module"
    public partial class TrainingCourseViewModel
    {
        public long TrainingCourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }  // A or B
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceFormatted { get; set; }  // computed display
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }

    // ---- Registration header ----
    public partial class TrainingRegistrationViewModel
    {
        public long TrainingRegistrationID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerLogo { get; set; }
        public Nullable<DateTime> RegistrationDate { get; set; }
        public string RegistrationDateFormatted { get; set; }
        public decimal TotalPrice { get; set; }
        public string TotalPriceFormatted { get; set; }
        public string Status { get; set; }
        // Status: Pending / Confirmed / Enrolled / Completed
        public string Notes { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int PersonCount { get; set; }
        public List<TrainingRegistrationPersonViewModel> Persons { get; set; }
    }

    // ---- Registration person ----
    public partial class TrainingRegistrationPersonViewModel
    {
        public long TrainingRegistrationPersonID { get; set; }
        public long TrainingRegistrationID { get; set; }
        public long CustomerID { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public long TrainingCourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public decimal CoursePrice { get; set; }
        public string CoursePriceFormatted { get; set; }
        // Admin-updated fields
        public string CourseStatus { get; set; }  // Incomplete / Complete
        public string CertificatePath { get; set; }
        public string CertificateFullUrl { get; set; }  // resolved full URL
        public Nullable<DateTime> CertificateExpiryDate { get; set; }
        public string CertificateExpiryFormatted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }

    // ---- Save registration (POST from customer) ----
    public partial class SaveTrainingRegistrationViewModel
    {
        public long TrainingRegistrationID { get; set; }  // 0 = new
        public long CustomerID { get; set; }  // resolved server-side if 0
        public List<SaveTrainingPersonViewModel> Persons { get; set; }
    }

    public partial class SaveTrainingPersonViewModel
    {
        public long TrainingRegistrationPersonID { get; set; }  // 0 = new
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public long TrainingCourseID { get; set; }
    }

    // ---- Admin update person status ----
    public partial class UpdateTrainingPersonStatusViewModel
    {
        public long TrainingRegistrationPersonID { get; set; }
        public string CourseStatus { get; set; }
        public string CertificateExpiryDate { get; set; }
    }

    // ---- Webinar ----
    public partial class TrainingWebinarViewModel
    {
        public long TrainingWebinarID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExternalLink { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }

    // ---- Blog ----
    public partial class TrainingBlogViewModel
    {
        public long TrainingBlogID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExternalLink { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }

    // ---- Technical Talk / Community Q&A ----
    public partial class TrainingTechnicalTalkViewModel
    {
        public long TrainingTechnicalTalkID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsPublished { get; set; }
        public bool IsAdminCreated { get; set; }
        public int DisplayOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
    public class TC_AnswerQuestionRequest
    {
        public long TrainingTechnicalTalkID { get; set; }
        public string Answer { get; set; }
    }

    // ---- Request helper classes (TC-namespaced to avoid collision) ----
    public class TC_IdRequest { public long id { get; set; } }
    public class TC_SubmitQuestionRequest { public string question { get; set; } }
    public class TC_UpdateRegStatusRequest
    {
        public long id { get; set; }
        public string status { get; set; }
        public string notes { get; set; }
    }

    #endregion

    public class InspectionFilterModel
    {
        public string InspectionTypeId { get; set; }

        public List<int> SelectedStatusIds { get; set; }

        public string Region { get; set; }

        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public long? CustomerLocationId { get; set; }

        public long? CustomerFacilityId { get; set; }

        public long? CustomerAreaId { get; set; }
    }

    public class DocumentFilterModel
    {
        public string Region { get; set; }

        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public long? CustomerLocationId { get; set; }

        public long? CustomerFacilityId { get; set; }

        public long? CustomerAreaId { get; set; }

        public bool IncludeInspectionDocuments { get; set; }

        public bool IncludeHistoricalDocuments { get; set; }

        public List<string> InspectionCategories { get; set; }

        public List<string> HistoricalCategories { get; set; }
    }
    public class IncidentFilterModel
    {
        public string IncidentType { get; set; }

        public string Region { get; set; }

        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public long? CustomerLocationId { get; set; }

        public long? CustomerFacilityId { get; set; }

        public long? CustomerAreaId { get; set; }
    }

    public class InternalInspectionFilterModel
    {
        public string Status { get; set; }

        public string Region { get; set; }

        public int? ProvinceId { get; set; }

        public int? CityId { get; set; }

        public long? CustomerLocationId { get; set; }

        public long? CustomerFacilityId { get; set; }

        public long? CustomerAreaId { get; set; }
    }

    #region "Spare Material ViewModel"


    public class InventoryFileDto
    {
        public long FileID { get; set; }
        public string FileName { get; set; }
        public string LocationName { get; set; }
        public long LocationID { get; set; }
        public string Region { get; set; }
        public long? FacilityID { get; set; }
        public string FacilityName { get; set; }
        public long? AreaID { get; set; }
        public string AreaName { get; set; }
        public int RowCount { get; set; }
        public string FileSize { get; set; }
        public string UploadedBy { get; set; }
        public string UploadDate { get; set; }
        public string UpdatedAt { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

    public class InventoryHeaderDto
    {
        public long ColumnID { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public bool Locked { get; set; }
        public bool Visible { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class InventoryRowDto
    {
        public long ItemID { get; set; }
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }

    public class InventoryAuditDto
    {
        public string DateTime { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }
        public string Action { get; set; }
        public string LocationName { get; set; }
        public string FileName { get; set; }
        public string Details { get; set; }
        public string IPAddress { get; set; }
    }

    public class LocationDto { public long LocationID { get; set; } public string LocationName { get; set; } }
    public class FacilityDto { public long FacilityID { get; set; } public string FacilityName { get; set; } }
    public class AreaDto { public long AreaID { get; set; } public string AreaName { get; set; } }

   
    public class UpdateFileMetadataDto
    {
        public long FileID { get; set; }
        public long LocationID { get; set; }
        public long? FacilityID { get; set; }
        public long? AreaID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }

    public class CustomerBasicDto
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNAVNo { get; set; }
        public string CustomerAddress { get; set; }
    }

    public class InventoryFilterRequest
    {
        public string Region { get; set; }
        public int? ProvinceID { get; set; }
        public int? CityID { get; set; }
        public long? LocationID { get; set; }
        public long? FacilityID { get; set; }
        public long? AreaID { get; set; }
        public string Status { get; set; }
        public string Search { get; set; }
    }
    public class InventoryFilterDto
    {
        public string Region { get; set; }
        public int? ProvinceID { get; set; }
        public int? CityID { get; set; }
        public long? LocationID { get; set; }
        public long? FacilityID { get; set; }
        public long? AreaID { get; set; }
        public string Status { get; set; }
        public string Search { get; set; }
    }

    #endregion

    #region "Account Delete"  

    public class AccountDeletionRequests
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(1000)]
        public string Reason { get; set; }

        [Required]
        public DateTime RequestedDateUtc { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } // Pending, Verified, Completed, Rejected

        [MaxLength(45)]
        public string IpAddress { get; set; }

        public DateTime? ProcessedDateUtc { get; set; }

        [MaxLength(200)]
        public string ProcessedBy { get; set; }
    }
    public class AccountDeletionRequestModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
        public bool ConfirmDelete { get; set; }
    }
    #endregion

}
