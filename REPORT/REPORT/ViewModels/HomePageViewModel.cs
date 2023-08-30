using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace REPORT.ViewModels
{
    public class HomePageViewModel
    {
        public ICommand AddReportCommand => new Command(AddReport);
        public ObservableCollection <string> Problems { get; set; }
       
        public string ProblemName { get; set; }
         public HomePageViewModel()
        {
            Problems = new ObservableCollection<string>();

            Problems.Add("Raven");
            Problems.Add("Hans");
            Problems.Add("Kyle");
            Problems.Add("Jerus");
            Problems.Add("Lloyd");

            //string[] arrProblems = new string[]
            //{
            //      "Raven", "Hans", "Kyle", "Jerus", "Gian"
            //};
            //Problems = arrProblems;
        }


        public void AddReport()
        {
            Problems.Add(ProblemName);

        }




    }
}
