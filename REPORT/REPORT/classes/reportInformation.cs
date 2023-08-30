using System;
using System.Collections.Generic;
using System.Text;

namespace REPORT
{
    public class reportInformation
    {
        public string issueName { get; set; }
        public string incidentOccurence { get; set; }
        public string isDiscussed { get; set; }
        public string accusedAddress { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string IsApproved { get; set; }
        public string reportID { get; set; }
        public string userID { get; set; }
        public string Image { get; set; }
        public string ReportStatus { get; set; }
        public string timeAndDateReportPosted { get; set; }
        public string reportType { get; set; }
        public string Others { get; set; }

        public string Occurence { get; set; }
        public string Location { get; set; }
    }
}
