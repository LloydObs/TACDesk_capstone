using REPORT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace REPORT.ViewModels
{
     class IncidentViewModel
    {
        public IList<Incidents> IncidentList { get; set; }

        public IncidentViewModel()
        {
            try
            {
                IncidentList = new ObservableCollection<Incidents>();
                IncidentList.Add(new Incidents { IncidentId = 1, IncidentName = "Accessories" });
                IncidentList.Add(new Incidents { IncidentId = 2, IncidentName = "Cash" });
                IncidentList.Add(new Incidents { IncidentId = 3, IncidentName = "Clothing" });
                IncidentList.Add(new Incidents { IncidentId = 4, IncidentName = "Electronic Devices" });
                IncidentList.Add(new Incidents { IncidentId = 5, IncidentName = "Personal Belongings" });
            }
            catch (Exception) { }
        }
    }
}
