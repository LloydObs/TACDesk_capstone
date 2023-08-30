using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportDetail : ContentPage
    {
        public ReportDetail(reports reportDetails)
        {
            InitializeComponent();
            string getReportID = reportDetails.reportID;
            string extractID = getReportID.Substring(0, getReportID.IndexOf("-")).Trim();
            string getReportType = ReturnReportType(extractID);
            string issueName = reportDetails.issueName;
            string othersDescription = reportDetails.Others;
            string getLabel = returnSpecificReportLabel(issueName);
            string returnOthersString = returnOthersValue(issueName, othersDescription);


            //int resourceId = (int)typeof(Resource.Drawable).GetField("photoNA.png").GetValue(null);


            if (String.IsNullOrEmpty(reportDetails.Image))
            {

                reportImage.Source = ImageSource.FromFile("photoNA.png");
            }
            else
            {
                reportImage.Source = reportDetails.Image;
            }
            

            reportType.Text = getReportType;
            specificReport.Text = issueName;
            othersLabel.Text = returnOthersString;
            reportID.Text = "Report ID: " + getReportID;
            userID.Text = "Submitted by: " + reportDetails.userID;
            
            
            dateSubmitted.Text = reportDetails.timeAndDateReportPosted;
            reportStatus.Text = reportDetails.ReportStatus;
        }

        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private string ReturnReportType(string reportCode)
        {
            string reportCodeConv;
            switch (reportCode)
            {
                case "CWI":
                    reportCodeConv = "Construction Worker Issues";
                    break;
                case "IV":
                    reportCodeConv = "Illegal Vendors";
                    break;
                case "LAF":
                    reportCodeConv = "Lost and Found";
                    break;
                case "NC":
                    reportCodeConv = "Neighborhood Concerns";
                    break;
                case "PO":
                    reportCodeConv = "Power Outage";
                    break;
                case "PP":
                    reportCodeConv = "Permanent Parking";
                    break;
                case "SH":
                    reportCodeConv = "Subdivision Hazards";
                    break;
                case "US":
                    reportCodeConv = "Unauthorized Solicitation";
                    break;
                case "WS":
                    reportCodeConv = "Water Interruption";
                    break;
                default:
                    reportCodeConv = "Unknown Report";
                    break;
            }
            return reportCodeConv;
        }

        private string returnSpecificReportLabel(string SpecificReport)
        {
            string message;
            if (SpecificReport != "Others")
            {
                message = SpecificReport;
            }
            else
            {
                message = " : " + SpecificReport;
            }
            return message;
        }

        private string returnOthersValue(string SpecificReport, string getOthersDescription)
        {
            string message;
            if (SpecificReport != "Others" || getOthersDescription == "Not Applicable")
            {
                message = String.Empty;
            }
            else
            {
                message = " : " + getOthersDescription;
            }
            return message;
        }

    }
}