using REPORT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaterInterruption : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string waterInterruptionID = "WS";
        public static string getID = "";
        string additionalInfo = "None Added";
        private string reportType = "Water Interruption";
        public WaterInterruption()
        {
            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Daily");
            typelist.Add("Weekly");
            typelist.Add("Monthly");


        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {

            try
            {
                string primeNumber = PrimeNumber.Text;
                string selectedType = Occurence.Text;
                string getTime = timeOccurence.Time.ToString(@"hh\:mm");
                getID = await connection.generateReportID(waterInterruptionID);
                var getUserID = ChooseUser.userID;
                string isApproved = "Pending";
                string reportStatus = "Pending";
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");




                waterinterruptionInfo info = new waterinterruptionInfo();
                info.PrimeNumber = primeNumber;
                info.AdditionalInfo = additionalInfo;
                info.selectedOccurence = selectedType;
                info.timeOfOccurence = getTime;
                info.reportID = getID;
                info.userID = getUserID;
                info.IsApproved = isApproved;
                info.ReportStatus = reportStatus;
                info.timeAndDateReportPosted = currentTimeDate;
                info.reportType = reportType;


                if (String.IsNullOrWhiteSpace(selectedType))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Occurence cannot be blanked", "Ok");
                    return;
                }else if (String.IsNullOrWhiteSpace(primeNumber))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Prime Number cannot be blanked", "Ok");
                    return;
                }
               
                var isSaved = await connection.SavewaterInterruptionInfo(info);
                if (isSaved)
                {
                    ReportSubmitted.getRefID = getID;
                    await Navigation.PushAsync(new ReportSubmitted());

                }
                else
                {
                    await DisplayAlert("Report Information Warning", "Information cannot be submitted now. Try Again.", "OK");
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                await App.Current.MainPage.DisplayAlert("Input Error", "Information cannot be submitted at the moment. Try again.", "Ok");
                return;
            }
          

        }
        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Clipboard.SetTextAsync(getID);

        }

        private void Additional_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
            if (!String.IsNullOrWhiteSpace(Additional.Text))
            {
                additionalInfo = Additional.Text;
            }
        }
    }
}