using CamV4.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CamV4.Helper
{
    public static class InspectionDetailsHelper
    {
        public static InspectionViewModel GetInspectionDetails(long id)
        {
            using (var db = new DatabaseEntities())
            {
                var inspectionDetails = db.GetInspectionDetailsForSheet(id).FirstOrDefault();
                if (inspectionDetails == null)
                    return null;

                var inspection = new InspectionViewModel
                {
                    InspectionId = inspectionDetails.InspectionId,
                    InspectionDocumentNo = inspectionDetails.InspectionDocumentNo,
                    InspectionDocumentNoRef = inspectionDetails.InspectionDocumentNoRef,
                    InspectionType = inspectionDetails.InspectionType,
                    InspectionDate = inspectionDetails.InspectionDate,
                    InspectionStatus = inspectionDetails.InspectionStatus,
                    InspectionStatusName = inspectionDetails.InspectionStatusName,
                    InspectionPDFPath = inspectionDetails.InspectionPDFPath,
                    Customer = inspectionDetails.Customer,
                    Employee = null, // or assign the correct string property if needed, e.g., inspectionDetails.Employee
                    CustomerLocation = inspectionDetails.CustomerLocation,
                    CustomerArea = inspectionDetails.CustomerArea,
                    CustomerFullAddress = inspectionDetails.CustomerFullAddress,
                    CADDocuments = inspectionDetails.CADDocuments,
                    Reportdate = inspectionDetails.Reportdate,
                    FacilitiesAreas = inspectionDetails.FacilitiesAreas,
                    ProcessOverviews = inspectionDetails.ProcessOverviews,
                    CustomerContactIds = inspectionDetails.CustomerContactIds
                    //,
                    //custModel = new CustomerViewModel
                    //{
                    //    CustomerFullPathLogo = inspectionDetails.CustomerLogo,
                    //    CustomerAddress = inspectionDetails.CustomerModelAddress
                    //},
                    //empModel = new EmployeeViewModel
                    //{
                    //    TitleDegrees = inspectionDetails.TitleDegrees,
                    //    EmployeeEmail = inspectionDetails.EmployeeEmail
                    //    //MobileNo = inspectionDetails.EmployeeMobileNo
                    //} 
                };

                // Get deficiency details using TVF
                var deficiencies = db.InspectionDeficiencies
                    .Where(d => d.InspectionId == id && (d.IsDelete ?? false) == false)
                    .Select(d => new InspectionDeficiencyViewModel
                    {
                        InspectionDeficiencyId = d.InspectionDeficiencyId,
                        InspectionId = d.InspectionId,
                        CustomerNomenclatureNo = d.CustomerNomenclatureNo == null ? null : d.CustomerNomenclatureNo.Trim(),
                        CustomerNomenclatureBayNoID = d.CustomerNomenclatureBayNoID == null ? null : d.CustomerNomenclatureBayNoID.Trim(),
                        BayFrameSide = d.BayFrameSide,
                        BeamFrameLevel = d.BeamFrameLevel,
                        DeficiencyType = d.DeficiencyType,
                        DeficiencyInfo = d.DeficiencyInfo,
                        Action_ReferReport = d.Action_ReferReport ?? false,
                        Action_Monitor = d.Action_Monitor ?? false,
                        Action_Replace = d.Action_Replace ?? false,
                        Action_Repair = d.Action_Repair ?? false,
                        ActionTaken = GetActionTakenText(d),
                        Severity_IndexNo = d.Severity_IndexNo ?? 0,
                        InspectionDeficiencyTechnicianStatus = d.InspectionDeficiencyTechnicianStatus ?? 0,
                        InspectionDeficiencyTechnicianStatusText = GetTechnicianStatusText(d.InspectionDeficiencyTechnicianStatus ?? 0),
                        InspectionDeficiencyAdminStatus = d.InspectionDeficiencyAdminStatus ?? 0,
                        InspectionDeficiencyTechnicianRemark = d.InspectionDeficiencyTechnicianRemarks,
                        InspectionDeficiencyRequestQuotation = d.InspectionDeficiencyRequestQuotation ?? 0,
                        InspectionDeficiencyApprovedQuotation = d.InspectionDeficiencyApprovedQuotation ?? 0,
                        InspectionDeficiencyPhotoViewModel = new List<InspectionDeficiencyPhotoViewModel>(),
                        InspectionDeficiencyPhotoTechnicianViewModel = new List<InspectionDeficiencyPhotoTechnicianViewModel>()
                    })
                    .ToList();

                // Get photos for all deficiencies in one query
                var photos = db.InspectionDeficiencyPhotoes
                    .Where(p => p.InspectionDeficiencyId == id)
                    .Select(p => new
                    {
                        p.InspectionDeficiencyId,
                        p.InspectionDeficiencyPhotoPath,
                        p.InspectionDeficiencyIsStatus
                    })
                    .ToList();

                //// Assign photos to their respective deficiencies
                //foreach (var deficiency in deficiencies)
                //{
                //    var defPhotos = photos.Where(p => p.InspectionDeficiencyId == deficiency.InspectionDeficiencyId);
                //    foreach (var photo in defPhotos)
                //    {
                //        var photoViewModel = new InspectionDeficiencyPhotoTechnicianViewModel
                //        {
                //            DeficiencyPhoto = photo.InspectionDeficiencyPhotoPath,
                //            DeficiencyPhotoThumb = photo.InspectionDeficiencyPhotoPath // You may want to add thumbnail logic here
                //        };

                //        if (photo.InspectionDeficiencyIsStatus ?? false)
                //            deficiency.InspectionDeficiencyPhotoTechnicianViewModel.Add(photoViewModel);
                //        else
                //            deficiency.InspectionDeficiencyPhotoViewModel.Add(photoViewModel);
                //    }
                //}

                inspection.iDefModel = deficiencies;

                // Get customer contacts
                inspection.ListCustomerLocationContacts = db.CustomerLocationContacts
                    .Where(c => c.CustomerLocationID == inspection.CustomerLocationId && (c.IsActive ?? false))
                    .Select(c => new CustomerLocationContactViewModel
                    {
                        LocationContactId = c.LocationContactId,
                        ContactName = c.ContactName,
                        ContactEmail = c.ContactEmail,
                        ContactPhone = c.ContactPhone
                    })
                    .ToList();

                return inspection;
            }
        }

        private static string GetActionTakenText(InspectionDeficiency deficiency)
        {
            var actions = new List<string>();

            if (deficiency.Action_ReferReport ?? false)
                actions.Add("Refer Report");
            if (deficiency.Action_Monitor ?? false)
                actions.Add("Monitor");
            if (deficiency.Action_Replace ?? false)
                actions.Add("Replace");
            if (deficiency.Action_Repair ?? false)
                actions.Add("Repair");

            return string.Join(", ", actions);
        }

        private static string GetTechnicianStatusText(int status)
        {
            switch (status)
            {
                case 1:
                    return "Completed";
                case 2:
                    return "In Progress";
                case 3:
                    return "Pending";
                default:
                    return "Not Started";
            }
        }
    }
}
