using System;
using System.Collections.Generic;
using System.Text;

namespace REPORT
{
    public class lostandfoundInfo
    {
        public string userType { get; set; }    
        public string itemName { get; set; }
        public string issueName { get; set; }
        public string venueLastSeen { get; set; }
        public string dateLastSeen { get; set; }
        public string timeLastSeen { get; set; }
        public string brand { get; set; } = string.Empty;
        public string itemMaterial { get; set; } = string.Empty;
        public string itemColor{ get; set; } = string.Empty;
        public string itemQuantity { get; set; } = string.Empty;
        public string reportID { get; set; }
        public string userID { get; set; }
        public string Image { get; set; } = string.Empty;
        public string ReportStatus { get; set; }
        public string timeAndDateReportPosted { get; set; }
        public string IsApproved { get; set; }
        public string reportType { get; set; }

    }
}
