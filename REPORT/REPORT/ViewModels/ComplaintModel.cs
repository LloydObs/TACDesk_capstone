using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ReportIT.Model;

namespace ReportIT.ViewModels
{
    class ComplaintModel
    {
        public IList<Complaint> ComplaintList { get; set; }

        public ComplaintModel()
        {
            try
            {
                ComplaintList = new ObservableCollection<Complaint>();
                ComplaintList.Add(new Complaint { ComplaintId = 1, ComplaintName = "Garbage Control" });
                ComplaintList.Add(new Complaint { ComplaintId = 2, ComplaintName = "Noise Complaint" });
                ComplaintList.Add(new Complaint { ComplaintId = 3, ComplaintName = "Pet Wastes Concern" });
                ComplaintList.Add(new Complaint { ComplaintId = 4, ComplaintName = "Others" });

            }
            catch (Exception) { }
        }
    }
}
