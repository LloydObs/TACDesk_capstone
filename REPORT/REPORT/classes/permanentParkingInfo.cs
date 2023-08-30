using System;
using System.Collections.Generic;
using System.Text;

namespace REPORT
{
    public class permanentParkingInfo
    {

        public string Address { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string issueName { get; set; }
        public string hasSticker { get; set; }
        public string isOwnerKnown { get; set; }
        public string reportID { get; set; }
        public string userID { get; set; }
        public string Image { get; set; } = string.Empty;
        public string ReportStatus { get; set; }
        public string timeAndDateReportPosted { get; set; }
        public string IsApproved { get; set; }
        public string reportType { get; set; }
    }
}
