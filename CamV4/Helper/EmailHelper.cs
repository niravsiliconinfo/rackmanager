using CamV4.Controllers;
using CamV4.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CamV4.Helper
{
    public class EmailHelper
    {
        private static string _smtp = ConfigurationManager.AppSettings["smtp"];
        private static string _adminEmail = ConfigurationManager.AppSettings["adminEmail"];

        public static void SendPdfEmail(List<string> to, string subject, FileContentResult attachmentFile, string body, long id, List<string> strCCEmails)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            var _se = DatabaseHelper.GetEmailInformation();
            message.From = new MailAddress(_se.SE_FROM_EMAIL);
            foreach (var item in to)
            {
                if (item != "")
                {
                    message.To.Add(new MailAddress(item.ToString()));
                }
            }
            foreach (var item in strCCEmails)
            {
                if (item != "")
                {
                    message.CC.Add(new MailAddress(item.ToString()));
                }
            }
            //message.Bcc.Add(new MailAddress("nirav.m@siliconinfo.com"));
            message.Subject = subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            //System.Net.Mail.Attachment attachment;
            //var pdf = AdminController.ToPdfV2(id);
            //attachment = new System.Net.Mail.Attachment("E:/Work Report/Krupali_Work_Report_June_2022.xlsx");
            //attachment = new System.Net.Mail.Attachment(file);
            //message.Attachments.Add(attachment);

            //var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            //var attachmentStream = new MemoryStream((attachmentFile as FileContentResult).FileContents);
            //var attachmentTitle = (attachmentFile as FileContentResult).FileDownloadName;
            //message.Attachments.Add(new Attachment(attachmentStream, attachmentTitle, contentType.ToString()));

            smtp.Port = Convert.ToInt32(_se.SE_SMTP_PORT);  //25 587
            smtp.Host = _se.SE_HOST; //for gmail host  
            smtp.EnableSsl = bool.Parse(_se.SE_SSL); ;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_se.SE_EMAIL, _se.SE_EMAIL_PASS);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(message);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static async Task SendEmailAsync(string to, string subject, FileContentResult attachmentFile, string body, List<string> strCCEmails)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            var _se = DatabaseHelper.GetEmailInformation();

            message.From = new MailAddress(_se.SE_FROM_EMAIL);
            message.To.Add(new MailAddress(to));

            // Add CC Emails
            if (strCCEmails != null)
            {
                foreach (var item in strCCEmails)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        message.CC.Add(new MailAddress(item.ToString()));
                    }
                }
            }

            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;

            // Add attachment if provided
            if (attachmentFile != null)
            {
                var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                var attachmentStream = new MemoryStream(attachmentFile.FileContents);
                var attachmentTitle = attachmentFile.FileDownloadName;
                message.Attachments.Add(new Attachment(attachmentStream, attachmentTitle, contentType.ToString()));
            }

            smtp.Port = Convert.ToInt32(_se.SE_SMTP_PORT);  // 25, 587, etc.
            smtp.Host = _se.SE_HOST;  // SMTP Host (e.g., Gmail, Outlook, etc.)
            smtp.EnableSsl = bool.Parse(_se.SE_SSL);  // Enable SSL
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_se.SE_EMAIL, _se.SE_EMAIL_PASS);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                // Asynchronous email sending
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine(e.Message);
            }
        }

        public static void SendEmail(string to, string subject, List<FileContentResult> attachmentFiles, string body, List<string> strCCEmails, List<string> strBCCEmails)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            var _se = DatabaseHelper.GetEmailInformation();
            message.From = new MailAddress(_se.SE_FROM_EMAIL);
            message.To.Add(new MailAddress(to));
            if (strCCEmails != null)
            {
                foreach (var item in strCCEmails)
                {
                    if (item != "")
                    {
                        message.CC.Add(new MailAddress(item.ToString()));
                    }
                }
            }
            if (strBCCEmails != null)
            {
                foreach (var item in strBCCEmails)
                {
                    if (item != "")
                    {
                        message.Bcc.Add(new MailAddress(item.ToString()));
                    }
                }
            }
            //message.Bcc.Add(new MailAddress("nirav.m@siliconinfo.com"));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Headers.Add("X-Priority", "3");
            message.Headers.Add("X-Mailer", "Microsoft Outlook 16.0");

            if (attachmentFiles != null && attachmentFiles.Count > 0)
            {
                foreach (var attachmentFile in attachmentFiles)
                {
                    var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    var attachmentStream = new MemoryStream(attachmentFile.FileContents);
                    var attachmentTitle = attachmentFile.FileDownloadName;

                    // Reset stream position before attaching
                    attachmentStream.Position = 0;

                    var attachment = new Attachment(attachmentStream, attachmentTitle, contentType.ToString());
                    message.Attachments.Add(attachment);
                }
            }

            smtp.Port = Convert.ToInt32(_se.SE_SMTP_PORT);  //25 587
            smtp.Host = _se.SE_HOST; //for gmail host  
            smtp.EnableSsl = bool.Parse(_se.SE_SSL);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_se.SE_EMAIL, _se.SE_EMAIL_PASS);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        #region "All Email Templates"

        //Old Email Singnature
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

        public static string SendContactEmailWithPassword(CustomerLocationContactViewModel model,    List<CustomerLocationContactViewModelList> lstLocatioList)
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
                    List<string> strBCCEmailslist = new List<string>();
                    string toCustContact;
                    strBCCEmailslist.Add("b.trivedi@camindustrial.net");

                    //var fac = model.CustomerFacilityID.HasValue ? db.CustomerFacilities.FirstOrDefault(x => x.CustomerFacilityID == model.CustomerFacilityID.Value) : null;
                    //var ar = model.CustomerAreaID.HasValue ? db.CustomerAreas.FirstOrDefault(x => x.AreaID == model.CustomerAreaID.Value) : null;                   

                    //string locationDetails = objCustomerLocation.LocationName;

                    //if (fac != null)
                    //{
                    //    locationDetails = fac.FacilityName + " / " + locationDetails;
                    //}

                    //if (ar != null)
                    //{
                    //    locationDetails = ar.AreaName + " / " + locationDetails;
                    //}

                    //List<EmployeeViewModel> objPMList = new List<EmployeeViewModel>();
                    //objPMList = DatabaseHelper.GetAllProjectManager();
                    //if (objPMList != null && objPMList.Count != 0)
                    //{
                    //    foreach (var pm in objPMList)
                    //    {
                    //        if (!string.IsNullOrWhiteSpace(pm.EmployeeEmail))
                    //        {
                    //            strCCEmailslist.Add(pm.EmployeeEmail);
                    //        }
                    //    }
                    //}

                    //List<EmployeeSalesViewModel> objSalesList = new List<EmployeeSalesViewModel>();
                    //objSalesList = DatabaseHelper.GetAllSalesRep();
                    //foreach (var sales in objSalesList)
                    //{
                    //    var customerArray = sales.SalesCompanyListing?.Split(',');

                    //    if (customerArray != null && customerArray.Contains(model.CustomerId.ToString()))
                    //    {
                    //        if (!string.IsNullOrWhiteSpace(sales.EmployeeEmail))
                    //        {
                    //            strCCEmailslist.Add(sales.EmployeeEmail);
                    //        }
                    //    }
                    //}

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

                    //strMSG += "<ul>";
                    //foreach (var loc in lstLocatioList)
                    //{
                    //    strMSG += $"<li><p>{loc.CustomerLocation}</p></li>";
                    //}
                    //strMSG += "</ul>";
                    strMSG += "<ul>";

                    var locationGroups = lstLocatioList
                        .Where(x => x.CustomerLocationID != 0)
                        .GroupBy(x => new
                        {
                            x.CustomerLocationID,
                            x.CustomerLocation
                        })
                        .ToList();

                    foreach (var locationGroup in locationGroups)
                    {
                        // =====================================================
                        // LOCATION
                        // =====================================================

                        strMSG += "<li>";
                        strMSG += "<p>" +
                                  HttpUtility.HtmlEncode(locationGroup.Key.CustomerLocation) +
                                  "</p>";


                        // =====================================================
                        // FACILITIES / AREAS UNDER LOCATION
                        // =====================================================

                        bool hasFacility =
                            locationGroup.Any(x =>
                                !string.IsNullOrWhiteSpace(x.FacilityId));

                        bool hasAreaWithoutFacility =
                            locationGroup.Any(x =>
                                string.IsNullOrWhiteSpace(x.FacilityId) &&
                                !string.IsNullOrWhiteSpace(x.AreaId));


                        if (hasFacility || hasAreaWithoutFacility)
                        {
                            strMSG += "<ul>";


                            // =================================================
                            // FACILITIES
                            // =================================================

                            var facilityGroups = locationGroup
                                .Where(x =>
                                    !string.IsNullOrWhiteSpace(x.FacilityId))
                                .GroupBy(x => new
                                {
                                    x.FacilityId,
                                    x.FacilityName
                                })
                                .ToList();


                            foreach (var facilityGroup in facilityGroups)
                            {
                                // ---------------------------------------------
                                // FACILITY
                                // ---------------------------------------------

                                string facilityName =
                                    !string.IsNullOrWhiteSpace(
                                        facilityGroup.Key.FacilityName)
                                        ? facilityGroup.Key.FacilityName
                                        : "Facility " + facilityGroup.Key.FacilityId;


                                strMSG += "<li>";
                                strMSG += "<p>" +
                                          HttpUtility.HtmlEncode(facilityName) +
                                          "</p>";


                                // ---------------------------------------------
                                // AREAS UNDER THIS FACILITY
                                // ---------------------------------------------

                                var facilityAreas = facilityGroup
                                    .Where(x =>
                                        !string.IsNullOrWhiteSpace(x.AreaId))
                                    .GroupBy(x => new
                                    {
                                        x.AreaId,
                                        x.AreaName
                                    })
                                    .ToList();


                                if (facilityAreas.Any())
                                {
                                    strMSG += "<ul>";

                                    foreach (var area in facilityAreas)
                                    {
                                        string areaName =
                                            !string.IsNullOrWhiteSpace(area.Key.AreaName)
                                                ? area.Key.AreaName
                                                : "Area " + area.Key.AreaId;


                                        strMSG += "<li>";
                                        strMSG += "<p>" +
                                                  HttpUtility.HtmlEncode(areaName) +
                                                  "</p>";
                                        strMSG += "</li>";
                                    }

                                    strMSG += "</ul>";
                                }


                                strMSG += "</li>";
                            }


                            // =================================================
                            // AREAS THAT DO NOT HAVE A FACILITY
                            // =================================================

                            var locationAreasWithoutFacility =
                                locationGroup
                                    .Where(x =>
                                        string.IsNullOrWhiteSpace(x.FacilityId) &&
                                        !string.IsNullOrWhiteSpace(x.AreaId))
                                    .GroupBy(x => new
                                    {
                                        x.AreaId,
                                        x.AreaName
                                    })
                                    .ToList();


                            foreach (var area in locationAreasWithoutFacility)
                            {
                                string areaName =
                                    !string.IsNullOrWhiteSpace(area.Key.AreaName)
                                        ? area.Key.AreaName
                                        : "Area " + area.Key.AreaId;


                                strMSG += "<li>";
                                strMSG += "<p>" +
                                          HttpUtility.HtmlEncode(areaName) +
                                          "</p>";
                                strMSG += "</li>";
                            }


                            strMSG += "</ul>";
                        }


                        strMSG += "</li>";
                    }

                    strMSG += "</ul>";

                    strMSG += "<br/>";
                    strMSG += "<p>You can access  <a href='https://rack-manager.com/'>(rack-manager.com)</a> by using your email as user ID and temporary password as " + model.UserPassword + " provided by admin.</p>";
                    strMSG += "<p>You will find the outcome of the inspection, and the detailed findings are now documented in the report. We understand the importance of this report in providing valuable insights into the condition and any necessary actions regarding the pallet racking.</p>";
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

                    strMSG += "<table cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse; font-family:Verdana, sans-serif;'>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:10px 0 10px 0;'>";
                    strMSG += "<span style='font-size:9pt; font-family:Verdana,sans-serif; color:#7b7b7b; font-weight:bold;'>Best regards,</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 1px 0;'>";
                    strMSG += "<span style='font-size:9pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>Bhavik Trivedi </span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab;'>P.Eng, ing., M.Tech, PMP</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 18px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>Engineering Manager</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>cam</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'> | industrial</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 12px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>20 7095 64 Street SE | </span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>Calgary, AB, T2C 5C3</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>E&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank' style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold; text-decoration:underline;'>b.trivedi@camindustrial.net</a>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>C&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>(403) 690-2976</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>D&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>(587) 355-1346</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 10px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>F&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>(403) 720-7074</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0;'>";
                    strMSG += "<img src='https://rack-manager.com/img/sigimg.png' alt='cam industrial' width='251' height='62' border='0' style='display:block; width:251px; height:62px;'>";
                    strMSG += "</td>";
                    strMSG += "</tr>";
                    strMSG += "</table>";
                    
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</body>";
                    strMSG += "</html>";

                    var tEmail = new Thread(() => EmailHelper.SendEmail(toCustContact, subject, null, strMSG, strCCEmailslist, strBCCEmailslist)); //attachmentFile
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

        public static string sendPassword(int CustomerID)
        {
            string strReturn = "Ok";
            var cust = DatabaseHelper.getCustomerById(CustomerID);
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
                    strMSG += "<p>You can access and review the status of the racking inspection, as well as the final report (once it is completed), on the Rack Manager platform <a href='https://rack-manager.com/' target='_blank'>(rack-manager.com)</a></p>";
                    strMSG += "<p>Following are the credentials to access the Rack Manager platform.</p>";
                    strMSG += "<p>Username : " + strUsername + "</p>";
                    strMSG += "<p>Password : " + cust.CustomerPharse + "</p>";
                    strMSG += "<br/>";
                    strMSG += "<p>Once you login using your credentials, you can change your password by going to \"Manage Password\" </p>";
                    strMSG += "<p><span><img src = 'https://rack-manager.com/img/ManagePassword.png' /></span></p>";
                    strMSG += "<br/>";
                    //strMSG += "<p>Please feel free to call +1 800 772 3213 or email the assigned engineer in case rescheduling is required.</p>";
                    //strMSG += "<p>We look forward to working with you soon.</p>";
                    strMSG += "</div></div><br/><br/><div><div>";
                    strMSG += "<table cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse; font-family:Verdana, sans-serif;'>";
                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 10px 0;'>";
                    strMSG += "<span style='font-size:9pt; font-family:Verdana,sans-serif; color:#7b7b7b; font-weight:bold;'>Best regards,</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 1px 0;'>";
                    strMSG += "<span style='font-size:9pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>Bhavik Trivedi </span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab;'>P.Eng, ing., M.Tech, PMP</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 18px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>Engineering Manager</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>cam</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'> | industrial</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 12px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>20 7095 64 Street SE | </span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>Calgary, AB, T2C 5C3</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>E&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<a href='mailto:b.trivedi@camindustrial.net' target='_blank' style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold; text-decoration:underline;'>b.trivedi@camindustrial.net</a>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>C&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>(403) 690-2976</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 2px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>D&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>(587) 355-1346</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0 0 10px 0;'>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#005aab; font-weight:bold;'>F&nbsp;&nbsp;&nbsp;~&nbsp;&nbsp;</span>";
                    strMSG += "<span style='font-size:8pt; font-family:Verdana,sans-serif; color:#7f7d7e; font-weight:bold;'>(403) 720-7074</span>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "<tr>";
                    strMSG += "<td style='padding:0;'>";
                    strMSG += "<img src='https://rack-manager.com/img/sigimg.png' alt='cam industrial' width='251' height='62' border='0' style='display:block; width:251px; height:62px;'>";
                    strMSG += "</td>";
                    strMSG += "</tr>";

                    strMSG += "</table>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</div>";
                    strMSG += "</body>";
                    strMSG += "</html>";
                    var tEmailCust = new Thread(() => EmailHelper.SendEmail(strCustomerEmail, "Your credentials to access the Rack Manager platform.", null, strMSG, null, null));
                    tEmailCust.Start();
                }
                else
                {
                    strReturn = "Information is missing from customer. Please update email, username, password in customer.";
                }
            }
            return strReturn;
        }
        #endregion

        //public static AlternateView Mail_Body(string username)
        //{
        //    string str = "<html><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta name='viewport' content='width=device-width, initial-scale=1'><meta http-equiv='X-UA-Compatible' content='IE=edge' /> "
        //                 + " <style>/* -- BODY & CONTAINER -- */ .body{background-color:#f6f6f6;width:100%}.container{display:block;margin:0 auto!important;max-width:580px;padding:10px;width:580px}.content{box-sizing:border-box;display:block;margin:0 auto;max-width:580px;padding:10px} "
        //                 + " /* -- HEADER, FOOTER, MAIN -- */ .main{background:#fff;border-radius:3px;width:100%}.wrapper{box-sizing:border-box;padding:20px} "
        //                 + " /* -- TYPOGRAPHY -- */ p,ul{font-family:sans-serif;font-size:16px;font-weight:400;margin:0 0 15px}p li,ul li{list-style-position:inside;margin-left:-20px;color:#666}ul.dashed{list-style-type:'- '}"
        //                 + " /* -- OTHER STYLES  -- */ .closing{font-size:13px;color:#dcaf26}.message{font-size:15px;color:#666}.lastheader{margin-bottom:0;font-size:15px}"
        //                 + " /* -- RESPONSIVE AND MOBILE FRIENDLY STYLES  -- */ @media only screen and (max-width:620px){table[class=body] h1{font-size:28px!important;margin-bottom:10px!important}table[class=body] a,table[class=body] ol,table[class=body] p,table[class=body] span,table[class=body] td,table[class=body] ul{font-size:16px!important}table[class=body] .article,table[class=body] "
        //                 + " .wrapper{padding:10px!important}table[class=body] .content{padding:0!important}table[class=body] .container{padding:0!important;width:100%!important}table[class=body] .main{border-left-width:0!important;border-radius:0!important;border-right-width:0!important}table[class=body] "
        //                 + " .btn a,table[class=body] .btn table{width:100%!important}table[class=body] .img-responsive{height:auto!important;max-width:100%!important;width:auto!important}}</style></head>"

        //                 + " <body><table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'><tr><td>&nbsp;</td><td class='container'><div class='content'>"
        //                 + " <!-- START CENTERED WHITE CONTAINER --> <table role='presentation' class='main'>"
        //                 + " <!-- START MAIN CONTENT AREA --> <tr><td class='wrapper'><table role='presentation' border='0' cellpadding='0' cellspacing='0'><tr><td><p>Hello Admin,</p><p class='message'>Have a good day!</p>"
        //                 + " <p class='lastheader'>Thanks & Regards,</p><p class='closing'><i>Kledli.no team.</i></p></td></tr></table></td></tr>"
        //                 + " <!-- END MAIN CONTENT AREA --></table> <!-- END CENTERED WHITE CONTAINER --></div></td><td>&nbsp;</td></tr></table></body></html>";
        //    AlternateView AV =
        //    AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

        //    return AV;
        //}
    }
}