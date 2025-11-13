using CamV4.Controllers;
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

        public static void SendEmail(string to, string subject, List<FileContentResult> attachmentFiles, string body, List<string> strCCEmails)
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
       
        public static AlternateView Mail_Body(string username)
        {
            string str = "<html><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta name='viewport' content='width=device-width, initial-scale=1'><meta http-equiv='X-UA-Compatible' content='IE=edge' /> "
                         + " <style>/* -- BODY & CONTAINER -- */ .body{background-color:#f6f6f6;width:100%}.container{display:block;margin:0 auto!important;max-width:580px;padding:10px;width:580px}.content{box-sizing:border-box;display:block;margin:0 auto;max-width:580px;padding:10px} "
                         + " /* -- HEADER, FOOTER, MAIN -- */ .main{background:#fff;border-radius:3px;width:100%}.wrapper{box-sizing:border-box;padding:20px} "
                         + " /* -- TYPOGRAPHY -- */ p,ul{font-family:sans-serif;font-size:16px;font-weight:400;margin:0 0 15px}p li,ul li{list-style-position:inside;margin-left:-20px;color:#666}ul.dashed{list-style-type:'- '}"
                         + " /* -- OTHER STYLES  -- */ .closing{font-size:13px;color:#dcaf26}.message{font-size:15px;color:#666}.lastheader{margin-bottom:0;font-size:15px}"
                         + " /* -- RESPONSIVE AND MOBILE FRIENDLY STYLES  -- */ @media only screen and (max-width:620px){table[class=body] h1{font-size:28px!important;margin-bottom:10px!important}table[class=body] a,table[class=body] ol,table[class=body] p,table[class=body] span,table[class=body] td,table[class=body] ul{font-size:16px!important}table[class=body] .article,table[class=body] "
                         + " .wrapper{padding:10px!important}table[class=body] .content{padding:0!important}table[class=body] .container{padding:0!important;width:100%!important}table[class=body] .main{border-left-width:0!important;border-radius:0!important;border-right-width:0!important}table[class=body] "
                         + " .btn a,table[class=body] .btn table{width:100%!important}table[class=body] .img-responsive{height:auto!important;max-width:100%!important;width:auto!important}}</style></head>"

                         + " <body><table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'><tr><td>&nbsp;</td><td class='container'><div class='content'>"
                         + " <!-- START CENTERED WHITE CONTAINER --> <table role='presentation' class='main'>"
                         + " <!-- START MAIN CONTENT AREA --> <tr><td class='wrapper'><table role='presentation' border='0' cellpadding='0' cellspacing='0'><tr><td><p>Hello Admin,</p><p class='message'>Yahhooo...Have a good day!</p>"
                         + " <p class='lastheader'>Thanks & Regards,</p><p class='closing'><i>Kledli.no team.</i></p></td></tr></table></td></tr>"
                         + " <!-- END MAIN CONTENT AREA --></table> <!-- END CENTERED WHITE CONTAINER --></div></td><td>&nbsp;</td></tr></table></body></html>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

            return AV;
        }
    }
}