using System;
using System.Collections.Generic;
using System.Text;

namespace REPORT
{
    public class unauthorizedSolicitationInfo
    {
        public string Reason { get; set; }
        public string Description { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string DateOfIncident { get; set; }
        public string timeOfIncident { get; set; }
        public string IsApproved { get; set; }
        public string reportID { get; set; }

        public string userID { get; set; }
        public string Image { get; set;  }
        public string ReportStatus { get; set; }
        public string timeAndDateReportPosted { get; set; }
        public string reportType { get; set; }

    }
}
