using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ProvinceID { get; set; }
        public string Country { get; set; }
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
    }

    public partial class CustomerAreaViewModel
    {
        public long AreaID { get; set; }
        public string Customer { get; set; }
        public long CustomerID { get; set; }
        public string CustomerLocation { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string AreaName { get; set; }
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
        public List<long> LinkedCustomerLocationIDs { get; set; }
        public List<long> LinkedCustomerUserLocationIds { get; set; }
        public string LinkedLocationNames { get; set; }
    }

    public class InspectionViewModel
    {
        public long InspectionId { get; set; }
        public string InspectionDocumentNo { get; set; }
        public string InspectionDocumentNoRef { get; set; }
        public string InspectionType { get; set; }
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
        public string CustomerFullAddress { get; set; }
        public string CustomerLogo { get; set; }
        public string Employee { get; set; }
        public long EmployeeId { get; set; }
        public string FacilitiesAreasIds { get; set; }
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
    }


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
        public int InspectionId { get; set; }
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
        public List<string> SelectedStatuses { get; set; }
    }

    public class FilterFilesModel
    {
        public bool InspectionDocs { get; set; }
        public bool HistoricalDocs { get; set; }
        public int Province { get; set; }
        public string Region { get; set; }
        public int City { get; set; }
        public string Location { get; set; }
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
}
