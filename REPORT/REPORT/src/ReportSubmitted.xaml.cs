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
    public partial class ReportSubmitted : ContentPage
    {    
        public static string getRefID;

        public ReportSubmitted()
        {
            InitializeComponent();
            try
            {
                ReportID.Text = "Ref ID: " + getRefID;
            }
            catch (Exception error)
            {
                App.Current.MainPage.DisplayAlert("Report Error!!", error.Message, "OK");

            }
            

            

        }

        private async void backToHome_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new HomePage1());
            }
            catch (Exception error)
            {
                await App.Current.MainPage.DisplayAlert("test", error.Message, "OK");
                //App.Current.MainPage.DisplayAlert("Uploading Data Error!!!", "Problems encountered when submitting the report. Try to restarting the App.", "OK");

            }

        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }


    }
}