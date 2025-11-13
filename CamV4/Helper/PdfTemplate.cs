using CamV4.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CamV4.Helper
{
    public class PdfTemplate
    {
        //public static string GetHTMLString(string host , int id)
        //{
        //    DatabaseEntities db = new DatabaseEntities();
        //    var iDetails = DatabaseHelper.getInspectionDetailsForSheet(id);
        //    if (iDetails != null)
        //    {
        //        host = host.Replace("https", "http");
        //        string strVar = "";
        //        int pageNo = 1;
        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0mm auto;padding:-5mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody>";
        //        strVar += "<tr>";
        //        strVar += "<td align='center' valign='top' class='' style='border:5px solid #0070c0;padding: 2px;width: 19cm;height: auto;}'>";
        //        strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div><div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width:300px;' /></div>";
        //        strVar += "<div style='font-size: 35px;text-decoration: underline;text-transform: uppercase;'>RACKING INSPECTION REPORT</div></div>";
        //        strVar += "<div class='' style='height:150px;padding-top: 50px;font-size: 24px;line-height:35px;'>";
        //        strVar += "<div>" + iDetails.Customer + "</div> <div style='font-size:14px;'>" + iDetails.CustomerArea + " " + iDetails.CustomerLocation + "</div><div style='font-size:14px;'>" + iDetails.custModel.CustomerAddress + "</div><div class='customer-logo'><img src='" + iDetails.custModel.CustomerLogo + "' style='width:250px;height:auto;' /></div></div>";
        //        strVar += "<div><img src='" + host + "Content/V2/images/mid-logo.jpg' style='width: 250px;margin: 100px 0px;' /></div> <div class=''>";
        //        strVar += "<div style='float:left;text-align:left;color:#005aab;font-weight:bold;line-height:30px;'><p style='margin: 0px;'>Inspection & Report By</p> <p style='margin: 0px;'>" + iDetails.Employee + ", " + iDetails.empModel.TitleDegrees + "</p>";
        //        strVar += "<p style='margin: 0px;'>" + iDetails.empModel.EmployeeEmail + "</p> <p style='color: #999;margin: 0px;'>" + iDetails.empModel.MobileNo + "</p> </div>";
        //        strVar += "<div style='float: right;text-align: left;color: #005aab;font-weight: bold;line-height: 30px;'> <p style='margin: 0px;'>Inspection Date:" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</p> <p style='margin: 0px;'>Report Date:" + Convert.ToDateTime(iDetails.Reportdate).ToString("dd MMM yyyy") + "</p> </div> <div style='clear: both'></div> </div>";
        //        strVar += "<div style='margin: 10px 0px 10px 0px;'> <div style='margin: 0px auto;float: none;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width:250px;'/></div><div style='clear: both'></div></div> </div> </td> </tr> </tbody> </table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0mm auto;padding:-25mm 0mm;;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  align='center' valign='top' class='' style='border:5px solid #0070c0;padding: 2px;width: 21cm;height: auto;}'>";
        //        strVar += "<div class='' style='border: 1px solid #0070c0;padding:10px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='font-size: 30px;text-transform: uppercase;border-bottom: 3px solid #212121;display: inline-block;margin: 20px 0px;font-weight: bold;'>Table of Contents</div>";
        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 12px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'>";
        //        strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 10px;font-weight: bold;position: relative;bottom: -2px;'>1A. INTRODUCTION</span>";
        //        strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 10px;font-weight: bold;position: relative;bottom: -2px;'>2</span>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom:5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;' style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'>";
        //        strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1B. SCOPE OF WORK</span>";
        //        strVar += "<span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2</span>";
        //        strVar += "</div>";
        //        strVar += "</div>";

        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'>";
        //        strVar += " <span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1C. DAMAGE CLASSIFICATION</span>";
        //        strVar += "	 <span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>4</span>";
        //        strVar += " </div>";
        //        strVar += " <div>";
        //        strVar += " <div style='padding-left: 50px;'>";
        //        strVar += " <div>";
        //        strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'>";
        //        strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;' class='ng-binding'>Frame Post Damage</span>";
        //        strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " <div>";
        //        strVar += " <div style='padding-left: 50px;'>";
        //        strVar += " <div>";
        //        strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'>";
        //        strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;' class='ng-binding'>Frame Brace Damage</span>";
        //        strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " <div>";
        //        strVar += " <div style='padding-left: 50px;'>";
        //        strVar += " <div>";
        //        strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'>";
        //        strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;' class='ng-binding'>Beam Damage</span>";
        //        strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " <div>";
        //        strVar += " <div style='padding-left: 50px;'>";
        //        strVar += " <div>";
        //        strVar += " <div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'>";
        //        strVar += "			 <span style='float: left;padding-right: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;' class='ng-binding'>Safety Recommendations</span>";
        //        strVar += "			 <span style='float: right;padding-left: 5px;background: #fff;height: 13px;font-weight: bold;position: relative;bottom: -2px;'></span></div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += " </div>";
        //        strVar += "</div>";

        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>1D. ENGINEERING REVIEW</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>6</span></div>";
        //        strVar += "</div>";
        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2A. DEFICIENCY PICTURE REFERENCES</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>7</span></div>";
        //        strVar += "</div>";
        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2B. REPAIR OR REPLACEMENT BASED ON DEFICIENCIES</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>7</span></div>";
        //        strVar += "</div>";
        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'><span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>3A. CONCLUSION AND RECOMMENDATIONS</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>8</span></div>";
        //        strVar += "</div>";
        //        strVar += "<div style='color: #000;font-size: 1rem;font-family: Arial, Helvetica, sans-serif;line-height: normal;'>";
        //        strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 15px;display: inline-block;width: 100%;font-size: 15px;text-transform: uppercase;'>";
        //        strVar += "<span style='float: left;padding-right: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>2C. FACILITIES AREA</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'>10</span>";
        //        strVar += "</div>";

        //        string fAreaName = "";
        //        string[] items = iDetails.FacilitiesAreasIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //        strVar += "	<div>";
        //        foreach (var f in items)
        //        {
        //            if (f != null)
        //            {
        //                int fId = Convert.ToInt16(f);
        //                var fac = db.FacilitiesAreas.Where(x => x.FacilitiesAreaId == fId && x.IsActive == true).FirstOrDefault();
        //                if (fac != null)
        //                {
        //                    fAreaName = fac.FacilitiesAreaName.Trim();
        //                    strVar += "<div style='padding-left: 50px;'>";
        //                    strVar += "<div>";
        //                    strVar += "<div style='border-bottom: 2px dotted #212121;margin-bottom: 5px;display: inline-block;width: 100%;font-size: 13px;text-transform: uppercase;'><span style='float:left;padding-right:5px;background:#fff;height:17px;font-weight:bold;position:relative;bottom:-2px;'>" + fAreaName + "</span><span style='float: right;padding-left: 5px;background: #fff;height: 17px;font-weight: bold;position: relative;bottom: -2px;'></span></div>";
        //                    strVar += "</div>";
        //                    strVar += "</div>";
        //                }
        //            }
        //        }
        //        strVar += "	</div>";
        //        strVar += "</div>";
        //        strVar += "	</div>";
        //        strVar += "</div>";
        //        strVar += "	</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' ><tbody ><tr ><td   valign='top' class='' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div class='' style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "</div>";
        //        strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1A. Introduction</h2>";
        //        strVar += "<p style='font-size: 15px;font-family: Arial, Helvetica, sans-serif;line-height: 24px;margin: 10px 0px;'>" + iDetails.Customer + " has requested Cam Industrial to perform a detailed inspection to identify, report, and address all the damages and deficiencies within their " + iDetails.CustomerArea + " " + iDetails.CustomerLocation + " facility. The following report fulfills this request and additionally provides suggestions that could be used to prevent future damages.</p>";
        //        strVar += "<p style='font-size: 15px;font-weight: bold;font-family: Arial, Helvetica, sans-serif;line-height: 24px;margin: 10px 0px;'>Pallet Racking Damage Inspection Report for the purpose of providing detailed information of the condition of the existing pallet racking systems. According to A344-24 Section 5.5.6 "Users should retain and maintain documents that establish the capacity of racking structures. Various regulations (i.e., OHS acts, codes, and regulations) require the capacity of equipment be known. Given that the structural adequacy of the rack affects the safety of the user and the workplace, the pallet rack capacity should be established by an engineer who is familiar with this Guide.</p>";
        //        strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1B. Scope of Work</h2>";
        //        strVar += "<h4 style='font-size: 15px;font-weight: bold;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>Process Overview</h4>";
        //        strVar += "<ul style='margin: 0;padding: 0;'>";
        //        foreach (var Process in iDetails.InspectionProcessOverview)
        //        {
        //            strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 20px;'>";
        //            strVar += "" + Process.ProcessOverviewDesc + "  </li>";
        //        }
        //        strVar += "</ul>";
        //        strVar += " <div style='margin:150px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-15mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "	<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "		<tbody >";
        //        strVar += "			<tr >";
        //        strVar += "				<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "					<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "						<div>";
        //        strVar += "							<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "						</div>";
        //        strVar += "						<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;padding-bottom:500px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "							The observation was performed with specific focus on the conditions visible from the access aisle";
        //        strVar += "							on the conditions visible from the access aisle viewpoint. In certain cases, it is possible that";
        //        strVar += "							existing damages were not visible due to obstacles that may have obstructed the view of the";
        //        strVar += "							inspector.<strong> Cam Industrial is not responsible for the omission of noteworthy items that are";
        //        strVar += "							a result of the accuracy limitations or any incidents that may occur as a result of";
        //        strVar += "							omissions.</strong> The inspection and report will provide the customer with up to date information";
        //        strVar += "							regarding the condition of their racking system. Damage is measured by tolerable levels of";
        //        strVar += "							damage outlined by engineering review. This program can be used as a systematic pallet racking";
        //        strVar += "							inspection program done quarterly, semi-annually, or annually.";
        //        strVar += "						</p>";
        //        strVar += "						<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += " <div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += " <div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += " <div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += " <div style='clear: both'></div>";
        //        strVar += "						</div>";
        //        strVar += "					</div>";
        //        strVar += "				</td>";
        //        strVar += "			</tr>";
        //        strVar += "		</tbody>";
        //        strVar += "	</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-15mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "	<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "		<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "			<div>";
        //        strVar += "				<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "			</div>";
        //        strVar += "			<h2 style='text-align: left;font-family: Arial, Helvetica, sans-serif;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;'>1C. Damage Classification</h2>";
        //        strVar += "			<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "				Damage classification is based on a scale of 1 to 10. (1-3) is considered Minor, which should be";
        //        strVar += "				monitored in subsequent inspections. (4-7) is considered Intermediate, which should be repaired or replaced as soon as possible.";
        //        strVar += "				Finally, (8-10) is considered Major, these items require immediate action such as offloading the area and quarantining the rack until such time that it can be safely dismantled and repaired. ";
        //        strVar += "				Each component has different thresholds for each classification, but these thresholds are not the only determining factors in damage classification. ";
        //        strVar += "				Other examples of factors that must be included in damage classification are rust/discoloration, shearing of metal, or multiple damage locations. ";
        //        strVar += "				Upright or frame damage can be classified into two categories; damage to frame posts and damage to frame bracing. ";
        //        strVar += "				Below the Racking Damage Classification Table and Figure 1 depict the classifications for frame damage.";
        //        strVar += "			</p>";
        //        strVar += "			<p style='font-size: 14px;line-height: 15px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "				*Adopted from Section 10.7, Rules for the Measurement and Classification of Damage to Uprights and Bracing";
        //        strVar += "				Members, published by the Fédération Européenne de la Manutention, Section X, FEM 10.2.04, Guidelines for the Safe Use of Static Steel Racking and Shelving, User Code, November 2001";
        //        strVar += "			</p>";
        //        strVar += "			<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 0;color: #000;font-size: 12px;font-family: Arial, Helvetica, sans-serif;line-height: normal;background: #e0e0e0;border-width:1px;'>";
        //        strVar += "				<tr>";
        //        strVar += "					<td height='30' colspan='3' align='center' bgcolor='#c6d9f1' style='border-width: 1px;font-family: Arial, Helvetica, sans-serif;'><strong>Racking Inspection – Frame Damage Classification</strong></td>";
        //        strVar += "				</tr>";
        //        strVar += "				<tr>";
        //        strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Classification</strong></td>";
        //        strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Damage Threshold</strong></td>";
        //        strVar += "					<td height='30' align='center' bgcolor='#ffffcc' style='padding:2px;border-width:1px;font-family: Arial, Helvetica, sans-serif;'><strong>Action</strong></td>";
        //        strVar += "				</tr>";
        //        strVar += "				<tr>";
        //        strVar += "					<td align='center' bgcolor='#00cc00' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Minor (1-3)</td>";
        //        strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "						<p>1. < or = 5mm</p>";
        //        strVar += "						<p>2. < or = 3mm</p>";
        //        strVar += "						<p>3. < or = 10mm</p>";
        //        strVar += "					</td>";
        //        strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for observation in subsequent inspections to ensure damage does not worsen or affect other areas</td>";
        //        strVar += "				</tr>";
        //        strVar += "				<tr>";
        //        strVar += "					<td align='center' bgcolor='#ffff00' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Intermediate (4-7)</td>";
        //        strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "						<p>1. 6mm to 10mm</p>";
        //        strVar += "						<p>2. 4mm to 6mm</p>";
        //        strVar += "						<p>3. 11mm to 20mm</p>";
        //        strVar += "					</td>";
        //        strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for replacement of component. Replace or repair component as soon as possible.</td>";
        //        strVar += "				</tr>";
        //        strVar += "				<tr>";
        //        strVar += "					<td align='center' bgcolor='#ff0000' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Major (8-10)</td>";
        //        strVar += "					<td align='center' bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "						<p>1. > 10mm</p>";
        //        strVar += "						<p>2. > 6mm</p>";
        //        strVar += "						<p>3. > 20mm</p>";
        //        strVar += "					</td>";
        //        strVar += "					<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px;font-family: Arial, Helvetica, sans-serif;'>Mark for replacement of component. Evaluate for immediate action such as offloading affected area or quarantine connecting areas</td>";
        //        strVar += "				</tr>";
        //        strVar += "			</table>";
        //        strVar += "			<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += "				<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "							<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += " <div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "							<div style='clear: both'></div>";
        //        strVar += "			</div>";
        //        strVar += "		</div>";
        //        strVar += "	</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-20mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "	<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "</div>";
        //        strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 16px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>Frame Post Damage</h2>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "	Damage to frame posts is not acceptable (refer to Figure 1). Posts of storage rack frames are";
        //        strVar += "	performance structural members and altering their shape with damage can have a significant";
        //        strVar += "	effect of their ability to carry compressive loads. As a general rule, frame posts should be";
        //        strVar += "	maintained in a “like new” condition. Therefore, any damage to frame posts should warrant";
        //        strVar += "	replacement of the frame post if it is a bolted type or an entire frame if it is a welded type.";
        //        strVar += "</p>";
        //        strVar += "<p style='text-align: center;'>";
        //        strVar += "	<img src='" + host + "Content/V2/images/Farme-img.png' width='520' />";
        //        strVar += "</p>";
        //        strVar += "<div style='font-size: 15px;line-height: 24px;margin: 10px 0px;text-align: center;font-family: Arial, Helvetica, sans-serif;'>Figure 1: Measurement method of damages to the frame components</div>";
        //        strVar += "<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-50mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "</div>";
        //        strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1C. Damage Classification (Continued)</h2>";
        //        strVar += "<br/>";
        //        strVar += "<h6 style='text-align:left;margin:10px 0px 0px 0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Frame Brace Damage</h6>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "The second type of frame damage classification is damage to frame bracing (See item 3 in";
        //        strVar += "<span style='font-weight: bold;'>Figure-1</span>). Damage to diagonal or horizontal braces is not acceptable because they are critical";
        //        strVar += "structural members. When a brace is damaged, it becomes crippled and can no longer resist the";
        //        strVar += "compressive forces for which they were designed. Damaged braces can be repaired by unbolting";
        //        strVar += "and replacing the brace if the frame is kitted or welding in a new brace if it is a welded frame.";
        //        strVar += "Damage to bracing does not necessarily warrant entire frame replacement.";
        //        strVar += "</p>";
        //        strVar += "<h6 style='text-align:left;margin:0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Beam Damage</h6>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;padding-top:0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "Similarly to frame posts and bracing, damage to horizontal beams is not acceptable and can also";
        //        strVar += "affect the structural integrity of the component. Damage to beams can be observed as connector";
        //        strVar += "damage, dents in the face of the beam and yielded sections of beam where the box or channel is";
        //        strVar += "separated. Any type of damage to beams requires replacement of the component.";
        //        strVar += "</p>";
        //        strVar += "<h6 style='text-align:left;margin:0px;padding-bottom:0px;font-size:16px;text-transform:none;display:inline-block;font-family: Arial, Helvetica, sans-serif;'>Safety Recommendations</h6>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "Every racking system, regardless of the manufacturer, comes equipped with essential components that are set as minimum requirements. These components include frame posts, frame bracing, frame footpads, beams - [step, box or channels], beam connectors, safety pins or bolts, and anchors, and it is essential that they are strictly adhered to.";
        //        strVar += "requirements. These components include; frame posts, frame bracing, frame footpads, beam box";
        //        strVar += "channel, beam connector, safety pins or bolts, and anchors";
        //        strVar += "</p>";
        //        strVar += "<h2 style='text-align: left;margin:0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>1D. Engineering Review</h2>";
        //        strVar += "<p style='font-size: 15px;font-family: Arial, Helvetica, sans-serif;line-height: 24px;'>";
        //        strVar += "Approval by an engineer [stamped/sealed] indicates that the report has been reviewed and confirms the following: It";
        //        strVar += "indicates that an Engineer has reviewed the work and confirms the following:";
        //        strVar += "</p>";
        //        strVar += "<ul style='margin: 0;padding: 0;'>";
        //        strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>The work was conducted by a trained inspector.</li>";
        //        strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>The inspection was conducted using a documented process.</li>";
        //        strVar += "<li style='list-style: decimal;font-size: 15px;font-family: Arial, Helvetica, sans-serif;padding-left: 0px;margin-left: 50px;line-height: 24px;'>";
        //        strVar += "The decisions, recommendations, or comments that were documented are reasonable,";
        //        strVar += "based on the information being reviewed. The engineer will rely on the Inspector’s on-site";
        //        strVar += "evaluation and accept it as being accurate, where photographs do not fully capture the";
        //        strVar += "severity of the damage.";
        //        strVar += "</li>";
        //        strVar += "</ul>";
        //        strVar += "<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "</div>";
        //        strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size: 15px;line-height: 24px;'>";
        //        strVar += "The actual loads being placed within the system have not been obtained or considered in this";
        //        strVar += "report, except where excessive beam deflection has been noted in the deficiency report. The";
        //        strVar += "report assumes that the system components have been manufactured according to adequate";
        //        strVar += "engineering and manufacturing standards and as such the integrity of the construction of the";
        //        strVar += "components has not been tested or verified.";
        //        strVar += "</p>";
        //        strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size: 15px;line-height: 24px;;margin: 10px 0px 0px 0px;'>";
        //        strVar += "Where possible the manufacturer’s published capacities have been used to establish the system";
        //        strVar += "capacity, modified in this report as required to reflect the current condition of the components.";
        //        strVar += "</p>";
        //        strVar += "<h2 style='text-align: left;margin: 15px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2A. Deficiency Picture References</h2>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "Pictures are taken to provide a visual confirmation of the deficiencies/action items. In some cases,";
        //        strVar += "it is possible that pictures of existing damages were not attainable due to height restrictions or";
        //        strVar += "even in lower levels where pallets or product obstructed the view of the inspector. Some pictures";
        //        strVar += "may be omitted from the report if the engineer discovers additional deficiencies or findings after";
        //        strVar += "the initial review completed by the racking inspector.";
        //        strVar += "</p>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "A separate document of the pictures will show the condition of the racking based on the";
        //        strVar += "deficiencies/action items that were documented during the racking inspection time of review.";
        //        strVar += "</p>";
        //        strVar += "<h2 style='text-align: left;margin: 15px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2B. Repair or Replacement Based on Deficiencies (Material Take-Off)</h2>";
        //        strVar += "<p style='font-size: 15px;line-height: 24px;margin: 10px 0px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "Based on the racking layout at the time of inspection, the following deficiencies were documented";
        //        strVar += "that are recommended for repair or replacement. For a detailed list of all deficiencies, comments";
        //        strVar += "and recommendations, please view the Racking Inspection Deficiency List.";
        //        strVar += "</p>";
        //        strVar += "<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        List<List<Deficiency>> sets = new List<List<Deficiency>>();
        //        List<Deficiency> selectedDeficiency = iDetails.ListConclusionandRecommendationsViewModel;
        //        int iCount = 0;
        //        int iSrNo = 1;
        //        for (int i = 0; i < selectedDeficiency.Count; i += 3)
        //        {
        //            List<Deficiency> set = new List<Deficiency>();
        //            for (int j = i; j < i + 3 && j < selectedDeficiency.Count; j++)
        //            {
        //                set.Add(selectedDeficiency[j]);
        //            }
        //            sets.Add(set);
        //        }

        //        foreach (var mainSet in sets)
        //        {
        //            strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //            strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //            strVar += "<tbody >";
        //            strVar += "<tr >";
        //            strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //            if (iCount == 0)
        //            {
        //                strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //                strVar += "<div>";
        //                strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //                strVar += "</div>";
        //                strVar += "<h2 style='text-align: left;margin: 10px 0px 0px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>3A. CONCLUSION AND RECOMMENDATIONS</h2>";
        //            }
        //            iCount += 1;

        //            strVar += "<ul style='margin: 10px 0px 0px 0px;padding: 0;'>";
        //            foreach (var deficiency in mainSet)
        //            {
        //                strVar += "<li style='list-style: none;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 24px;font-family: Arial, Helvetica, sans-serif;'>";
        //                strVar += iSrNo.ToString() + ". " + deficiency.DeficiencyInfo + ". <p>" + deficiency.DeficiencyDescription + "</p>";
        //                strVar += "</li>";
        //                iSrNo += 1;
        //            }
        //            strVar += "</ul>";
        //            strVar += "<div style='margin: 50px 0px 20px 0px;'>";
        //            strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //            strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //            strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //            strVar += "<div style='clear: both'></div>";
        //            strVar += "</div>";
        //            strVar += "</div>";
        //            strVar += "</td>";
        //            strVar += "</tr>";
        //            strVar += "</tbody>";
        //            strVar += "</table>";
        //            strVar += "</section>";
        //        }

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;'></div>";
        //        strVar += "</div>";
        //        strVar += "<h2 style='text-align: left;margin: 10px 0px 10px 0px;font-size: 18px;text-transform: none;display: inline-block;border-bottom: 2px solid #212121;font-family: Arial, Helvetica, sans-serif;'>2C. FACILITIES AREA</h2>";
        //        strVar += "<ul style='margin: 0;padding: 0;'>";
        //        foreach (var facility in iDetails.InspectionFacilitiesArea)
        //        {
        //            strVar += "<li style='list-style: decimal;font-size: 15px;padding-left: 0px;margin-left: 50px;line-height: 20px;font-family: Arial, Helvetica, sans-serif;'>";
        //            strVar += "" + facility.FacilitiesAreaName + " <p>" + facility.FacilitiesAreaDesc + "</p>";
        //            strVar += "</li>";
        //        }
        //        strVar += "</ul>";
        //        strVar += "<div style='margin: 200px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;' /></div>";
        //        strVar += "</div>";

        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'>";
        //        strVar += "<h4>" + iDetails.Customer + " - " + iDetails.CustomerLocation + "</h4>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "<div>";
        //        strVar += "</div>";

        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'>";
        //        strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px'>Engineering Notes: " + iDetails.InspectionDocumentNo + "</div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "<p style='font-family: Arial, Helvetica, sans-serif;font-size:15px'>";
        //        strVar += "These reports were prepared in accordance with the recommendations outlined in the standards below. A copy of the reports is attached for your reference.";
        //        strVar += "</p>";
        //        strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 0; color: #000; font-size: 12px; font-family: Arial, Helvetica, sans-serif; line-height: normal; background: #e0e0e0; border-width:1px;'>";
        //        foreach (var t in iDetails.InspectionDocumentTitle)
        //        {
        //            strVar += "<tr>";
        //            strVar += "<td align='center' style='padding:10px 10px 10px 10px; border-width: 1px; background-color: #ADD8E6;'>" + t.DocumentTitle1 + "</td>";
        //            strVar += "<td bgcolor='#ffffff' style='padding:2px 2px 2px 5px; border-width: 1px; '>" + t.DocumentDescription + "</td>";
        //            strVar += "</tr>";
        //        }
        //        strVar += "</table>";
        //        strVar += "<p>Our inspection revealed structural and nonstructural deficiencies mentioned in the section 3A Conclusion and Recommendations and corrective actions are at the owner’s discretion.</p>";
        //        strVar += "<p>In the report to follow, you will find:</p>";
        //        strVar += "<p>1) A detailed list of the deficiencies in the racking system</p>";
        //        strVar += "<p>";
        //        strVar += "    2) Photos of the deficiencies";
        //        strVar += "</p>";
        //        strVar += "<p>";
        //        strVar += "    3) Material needed to remedy the deficiencies";
        //        strVar += "</p>";
        //        strVar += "<p>";
        //        strVar += "    <br />";
        //        strVar += "    <br />";
        //        strVar += "    <br />";
        //        strVar += "    <br />";
        //        strVar += "</p>";
        //        strVar += "<p>";
        //        strVar += "    Yours truly,";
        //        strVar += "</p>";

        //        strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:15px'>";
        //        strVar += iDetails.empModel.EmployeeName + "," + iDetails.empModel.TitleDegrees;
        //        strVar += "</div>";
        //        strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:15px'>";
        //        strVar += "Cam Industrial";
        //        strVar += "</div>";

        //        strVar += "<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:-25mm 0mm 0mm 0mm;box-sizing:border-box;'>";
        //        strVar += "<table width='100%' border='0' cellpadding='0' cellspacing='0' >";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  valign='top' style='border: 5px solid #0070c0;padding: 2px;'>";
        //        strVar += "<div style='border: 1px solid #0070c0;padding: 20px;'>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'><img src='" + host + "Content/V2/images/logos/CamLogo.png' style='width: 30%;' /></div>";
        //        strVar += "</div>";
        //        strVar += "<div>";
        //        strVar += "<div style='text-align: center;'>";
        //        strVar += "<h2 style='text-align: center;margin: 20px 0px 10px 0px;font-size: 28px;text-transform: none;display: inline-block;'font-family: Arial, Helvetica, sans-serif;'>" + iDetails.Customer + "</h2>";
        //        strVar += "<div style='font-family: Arial, Helvetica, sans-serif;font-size:24px;text-align: center;'>Document Appendix</div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='margin: 20px 0px 0px 0px; color: #000; font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: normal; background: #e0e0e0; border-width:1px;'>";
        //        strVar += "<tr>";
        //        strVar += "<td align='center' style='padding: 20px; border-width: 1px; background-color: #ADD8E6;'>";
        //        strVar += "Document Title";
        //        strVar += "</td>";
        //        strVar += "<td align='center' style='padding: 20px; border-width: 1px; background-color: #ADD8E6;'>";
        //        strVar += "Document Number";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        foreach (var k in iDetails.InspectionFacilitiesArea)
        //        {
        //            strVar += "<tr>";
        //            strVar += "<td bgcolor='#ffffff' style='padding: 20px; border-width: 1px; '>" + k.FacilitiesAreaName + "</td>";
        //            strVar += "<td bgcolor='#ffffff' style='padding: 2px; border-width: 1px; '>";
        //            if ((k.FacilitiesAreaId == 4 || k.FacilitiesAreaId == 5) || (k.FacilitiesAreaId == 4 && k.FacilitiesAreaId == 5))
        //            {
        //                strVar += "T.B.D";
        //            }
        //            else
        //            {
        //                strVar += "" + iDetails.InspectionDocumentNo + "";
        //            }

        //            strVar += "</td>";
        //            strVar += "</tr>";
        //        }
        //        strVar += "</table>";
        //        strVar += "<div style='margin: 50px 0px 20px 0px;'>";
        //        strVar += "<div style='width: 28%;float: left;font-size: 10px;font-weight: bold;'>RACKING INSPECTION REPORT<span> - " + iDetails.Customer + "</span></div>";
        //        strVar += "<div style='text-align: center;float: left;width: 65%;'><img src='" + host + "Content/V2/images/footer-logo.jpg' style='width: 70%;'></div>";
        //        strVar += "<div style='float: right;width: 7%;font-size: 16px;font-weight: bold;text-align: right;position: relative;'>" + pageNo++ + "</div>";
        //        strVar += "<div style='clear: both'></div>";
        //        strVar += "</div>";
        //        strVar += "</div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm 0mm 0mm -10mm;box-sizing:border-box;'>";
        //        strVar += "<table width='95%' border='1' align='left' cellpadding='0' cellspacing='0'  style='border-collapse: collapse;color:#000000;font-size:11px'>";
        //        strVar += "<tbody >";
        //        strVar += "<tr >";
        //        strVar += "<td  align='center' valign='top' class=''>";
        //        strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='border-collapse: collapse;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "<tr>";
        //        strVar += "<td colspan='6' style='font-size:14px;font-weight:600;text-align:center;padding:2px;background:#79addd;font-family: Arial, Helvetica, sans-serif;'>RACKING INSPECTION DEFICIENCY LIST</td>";
        //        strVar += "<td rowspan='4' style='width:150px'><img src='" + host + "Content/V2/images/table-logo.png' style='width:90%;padding:2px'></td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Client:</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.Customer + "</td>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Document Number:</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.InspectionDocumentNo + "</td>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Type of Racking:</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.InspectionType + "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Location/ Address</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.CustomerArea + " " + iDetails.CustomerLocation + "</td>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Date of Inspection</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</td>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Total Inspection Deficiencies</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.iDefModel.Count() + "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Contact</td>";
        //        if (iDetails.ListCustomerLocationContacts != null)
        //        {
        //            strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.ListCustomerLocationContacts[0].ContactName + "</td>";
        //        }
        //        else
        //        {
        //            strVar += "<td style='padding:2px;font-size:9px;'></td>";
        //        }

        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Inspected By</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>" + iDetails.Employee + "</td>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px'>Action Required</td>";
        //        strVar += "<td style='padding:2px;font-size:9px;'>Yes</td>";
        //        strVar += "</tr>";
        //        strVar += "</table>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr >";
        //        strVar += "<td  align='center' valign='top' class=''>";
        //        strVar += "<table width='100%' border='1' cellspacing='0' cellpadding='0' style='font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "<tr>";
        //        strVar += "<td colspan='5' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Racking Classification</td>";
        //        strVar += "<td colspan='2' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Deficiency Type</td>";
        //        strVar += "<td colspan='6' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Damage Assessment</td>";
        //        strVar += "<td colspan='2' bgcolor='#79ADDD' style='padding:2px;text-align: center;font-size:9px;'>Repair Status</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td colspan='5' bgcolor='#0dffff' style='text-align:center;padding:2px;;font-size:9px;'>Location</td>";
        //        strVar += "<td bgcolor='#ffc820' style='text-align:center;padding:2px;;font-size:9px;'>Category</td>";
        //        strVar += "<td bgcolor='#ffc820' style='text-align:center;padding:2px;;font-size:9px;'>Description</td>";
        //        strVar += "<td colspan='6' bgcolor='#ff6160' style='text-align:center;padding:2px;;font-size:9px;'>Action Required</td>";
        //        strVar += "<td colspan='2' bgcolor='#0dffff' style='text-align:center;padding:2px;;font-size:9px;'>Action Required</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr bgcolor='#ffffcc'>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Item ID</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Row Number</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Bay ID/ Number</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Bay/ Frame Side</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Beam/ Frame Level</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'>Title</td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'>Title</td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Refer report for more detail</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Monitor</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Replace Component</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Repair Component</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Severity Index Number</div></td>";
        //        strVar += "<td valign='middle' style='text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Reference Images</div></td>";
        //        strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Status</div></td>";
        //        strVar += "<td valign='middle' style='text-align:center;padding:2px'><div style='position: relative;font-size:10px'>Reference Images</div></td>";
        //        strVar += "</tr>";
        //        strVar += "<tbody>";
        //        foreach (var d in iDetails.iDefModel)
        //        {
        //            strVar += "<tr style='border-width:1px;'><i class='fa-solid fa-xmark'></i>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.RowNo + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.CustomerNomenclatureNo + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.CustomerNomenclatureBayNoID + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.BayFrameSide + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.BeamFrameLevel + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.DeficiencyType + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.DeficiencyInfo + "</td>";
        //            if (d.Action_ReferReport == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'/></td>"; }
        //            else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'/></td>"; }
        //            if (d.Action_Monitor == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'/></td>"; }
        //            else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td>"; }
        //            if (d.Action_Replace == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'></td>"; }
        //            else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td>"; }
        //            if (d.Action_Repair == true) { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/check-mark.png' style='width:12px;'></td>"; }
        //            else { strVar += "<td valign='middle' style='text-align:center;padding:2px;font-size:9px;'><img src='" + host + "Content/V2/images/close-mark.png' style='width:12px;'></td>"; }
        //            if (d.Severity_IndexNo >= 1 && d.Severity_IndexNo <= 3)
        //            {
        //                strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#00CC00;font-size:9px;'>" + d.Severity_IndexNo + "</td>";
        //            }
        //            if (d.Severity_IndexNo >= 4 && d.Severity_IndexNo <= 7)
        //            {
        //                strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#FFFF00;font-size:9px;'>" + d.Severity_IndexNo + "</td>";
        //            }
        //            if (d.Severity_IndexNo >= 8 && d.Severity_IndexNo <= 10)
        //            {
        //                strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;background:#FF0000;font-size:9px;'>" + d.Severity_IndexNo + "</td>";
        //            }
        //            strVar += "<td valign='middle' style='width:65px;text-align:left;padding:2px;font-size:9px;'>";

        //            if (d.InspectionDeficiencyPhotoViewModel != null)
        //            {
        //                strVar += "<table>";
        //                foreach (var photo in d.InspectionDeficiencyPhotoViewModel)
        //                {
        //                    strVar += "<tr><td>";
        //                    strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhoto + "' style='width:64px!important;height:64px!important;' alt=''/></a>";
        //                    strVar += "</td></tr>";
        //                }
        //                strVar += "</table>";
        //            }
        //            strVar += "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px;font-size:9px;'>" + d.InspectionDeficiencyTechnicianStatusText + "</td>";
        //            strVar += "<td valign='middle' style='width:2rem;text-align:center;padding:2px'>";
        //            if (d.InspectionDeficiencyPhotoTechnicianViewModel != null)
        //            {
        //                strVar += "<table>";
        //                foreach (var photo in d.InspectionDeficiencyPhotoTechnicianViewModel)
        //                {
        //                    strVar += "<tr><td>";
        //                    strVar += "<a target='_blank' href='" + photo.DeficiencyPhoto + "'><img src='" + photo.DeficiencyPhoto + "' style='width:64px!important;height:64px!important;' alt=''></a>";
        //                    strVar += "</td></tr>";
        //                }
        //                strVar += "</table>";
        //            }
        //            strVar += "</td>";
        //            strVar += "</tr>";
        //        }
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        //Material Take-off List
        //        strVar += "<section style='width:19cm;height:29.7cm;background:white;box-shadow:0 .2mm 2mm rgba(0,0,0,.3);margin:0px;padding:0mm 0mm 0mm -7mm;box-sizing:border-box;'>";
        //        strVar += "<table width='95%' border='1' align='left' cellpadding='0' cellspacing='0'  style='border-collapse: collapse;color:#000000;font-size:11px;font-family: Arial, Helvetica, sans-serif;'>";
        //        strVar += "<tr>";
        //        strVar += "<td height='30' colspan='9' align='center' bgcolor='#80b1de' style='font-family: Arial, Helvetica, sans-serif;'>RACKING INSPECTION - MATERIAL TAKE OFF LIST</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td width='100' bgcolor='#ffffcc' style='padding:2px 10px'>Client:</td>";
        //        strVar += "<td colspan='4' style='padding:2px'>" + iDetails.Customer + "</td>";
        //        strVar += "<td width='100' align='center' bgcolor='#ffffcc' style='padding:2px'>Type of Racking:</td>";
        //        strVar += "<td colspan='3' style='padding:2px 10px'>" + iDetails.InspectionType + "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px'>Location & Address:</td>";
        //        strVar += "<td colspan='4' style='padding:2px'>" + iDetails.CustomerArea + " " + iDetails.CustomerLocation + "</td>";
        //        strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px'>Date of Inspection:</td>";
        //        strVar += "<td colspan='3' style='padding:2px 10px'>" + Convert.ToDateTime(iDetails.InspectionDate).ToString("dd MMM yyyy") + "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px'>Contact:</td>";
        //        if (iDetails.ListCustomerLocationContacts != null)
        //        {
        //            strVar += "<td colspan='4' style='padding:2px'>" + iDetails.ListCustomerLocationContacts[0].ContactName + "</td>";
        //        }
        //        else
        //        {
        //            strVar += "<td colspan='4' style='padding:2px'></td>";
        //        }
        //        strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px'>Inspection By:</td>";
        //        strVar += "<td colspan='3' style='padding:2px 10px'>" + iDetails.Employee + "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td bgcolor='#ffffcc' style='padding:2px 10px'>Project Number:</td>";
        //        strVar += "<td colspan='4' style='padding:2px'>" + iDetails.InspectionDocumentNo + "</td>";
        //        strVar += "<td align='center' bgcolor='#ffffcc' style='padding:2px'>Report/ BOM By:</td>";
        //        strVar += "<td colspan='3' style='padding:2px 10px'>" + iDetails.Employee + "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td colspan='9' align='center' valign='middle'>";
        //        strVar += "<div style='float:left;padding-left:30px;'><img src='" + host + "/Content/V2/images/table-logo.png' style='width:150px'></div>";
        //        strVar += "<div style='float:right;padding-right:30px;'><img src='" + host + "/Content/V2/images/footer-logo.jpg' style='width:250px;margin:10px 0px;'></div>";
        //        strVar += "</td>";
        //        strVar += "</tr>";
        //        strVar += "<tr>";
        //        strVar += "<td align='center' bgcolor='#dbe5f1'>Severity<br />Index</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Action Item Reference</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Component</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Manufacturer</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Vendor ID</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Type</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>CAM ID</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Size/ Description</td>";
        //        strVar += "<td height='30' align='center' bgcolor='#dbe5f1'>Quantity<br />Required</td>";
        //        strVar += "</tr>";
        //        strVar += "<tbody>";
        //        foreach (var d in iDetails.iMTOModel)
        //        {

        //            InspectionDeficiencyMTODetailViewModel objMTODetails = new InspectionDeficiencyMTODetailViewModel();
        //            var iMTOdetails = db.InspectionDeficiencyMTODetails.Where(h => h.InspectionDeficiencyMTOId == d.InspectionDeficiencyMTOId).ToList();
        //            var fType = "";
        //            if (iMTOdetails.Count != 0)
        //            {
        //                foreach (var mtoDetail in iMTOdetails)
        //                {
        //                    if (mtoDetail.ComponentPropertyTypeId != 0)
        //                    {
        //                        var type = DatabaseHelper.getComponentPropertyTypeById(mtoDetail.ComponentPropertyTypeId);
        //                        if (type != null)
        //                        {
        //                            if (type.ComponentPropertyTypeName.Contains("Type"))
        //                            {
        //                                var value = db.ComponentPropertyValues.Where(x => x.ComponentPropertyValueId == mtoDetail.ComponentPropertyValueId && x.ComponentPropertyTypeId == mtoDetail.ComponentPropertyTypeId).ToList();
        //                                if (value.Count != 0)
        //                                {
        //                                    foreach (var v in value)
        //                                    {
        //                                        fType += v.ComponentPropertyValue1 + ",";
        //                                        fType = fType.Remove(fType.Length - 1);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //            }

        //            strVar += "<tr style='border-width:1px;'>";
        //            if (d.Severity_IndexNo >= 1 && d.Severity_IndexNo <= 3)
        //            {
        //                strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#00CC00;color:#000000;'>" + d.Severity_IndexNo + "</td>";
        //            }
        //            else if (d.Severity_IndexNo >= 4 && d.Severity_IndexNo <= 7)
        //            {
        //                strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#FFFF00;color:#000000;'>" + d.Severity_IndexNo + "</td>";
        //            }
        //            else if (d.Severity_IndexNo >= 8 && d.Severity_IndexNo <= 10)
        //            {
        //                strVar += "<td valign='middle' style='width:1rem;text-align:center;padding:2px;background:#FF0000;color:#000000;'>" + d.Severity_IndexNo + "</td>";
        //            }
        //            else
        //            {
        //                strVar += "<td valign='middle' style='width:1rem;text-align:center;'></td>";
        //            }
        //            strVar += "<td align='center' bgcolor='#d9e1f2' style='padding:2px'>" + d.DeficiencyRowNo + "</td>";
        //            strVar += "<td style='padding:2px 10px'>" + d.ComponentName + "</td>";
        //            strVar += "<td style='padding:2px 10px'>" + d.ManufacturerName + "</td>";
        //            strVar += "<td style='padding:2px 10px'></td>";
        //            strVar += "<td style='padding:2px 10px'>" + fType.ToString() + "</td>";
        //            strVar += "<td style='padding:2px 10px'></td>";
        //            strVar += "<td style='padding:2px 10px'>" + d.Size_Description + "</td>";
        //            strVar += "<td style='padding:2px 10px'>" + d.QuantityReq + "</td>";
        //            strVar += "</tr>";
        //        }

        //        strVar += "</tbody>";
        //        strVar += "</table>";
        //        strVar += "</section>";

        //        return strVar;
        //    }
        //    return null;
        //}
    }
}