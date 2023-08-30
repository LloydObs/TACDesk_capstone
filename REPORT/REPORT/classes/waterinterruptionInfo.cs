using System;
using System.Collections.Generic;
using System.Text;

namespace REPORT
{
    public class waterinterruptionInfo
    {

        public string PrimeNumber { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string selectedOccurence { get; set; }
        public string timeOfOccurence { get; set; }
        public string reportID { get; set; }

        public string userID { get; set; }
        public string ReportStatus { get; set; }
        public string timeAndDateReportPosted { get; set; }
        public string IsApproved { get; set; }
        public string reportType { get; set; }

    }
}
