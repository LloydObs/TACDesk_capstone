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
    public partial class PowerOutage : ContentPage
    {

        firebaseConnection connection = new firebaseConnection();
        string powerOutageID = "PO";
        public static string getID = "";
        private string additionalInfo = "None Added";
        private string reportType = "Power Outage";
        public PowerOutage()
        {
            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Daily");
            typelist.Add("Weekly");
            typelist.Add("Monthly");
            
        }


        public async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                string meralcoNumber = MeralcoNumber.Text;
                string selectedType = Occurence.Text;
                string getTime = timeOccured.Time.ToString(@"hh\:mm");
                string isApproved = "Pending";
                string reportStatus = "Pending";
                getID = await connection.generateReportID(powerOutageID);
                var getUserID = ChooseUser.userID;
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");


                poweroutageInfo info = new poweroutageInfo();
                info.MeralcoNumber = meralcoNumber;
                info.AdditionalInfo = additionalInfo;
                info.selectedOccurence = selectedType;
                info.timeOfOccurence = getTime;
                info.IsApproved = isApproved;
                info.reportID = getID;
                info.userID = getUserID;
                info.timeAndDateReportPosted = currentTimeDate;
                info.ReportStatus = reportStatus;
                info.reportType = reportType;

                if (String.IsNullOrWhiteSpace(meralcoNumber))
                {

                    await App.Current.MainPage.DisplayAlert("Input Error", "Meralco Number cannot be blanked", "Ok");
                    return;
                }
                else if (String.IsNullOrWhiteSpace(selectedType))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Occurence cannot be blanked", "Ok");
                    return;
                }else
                {
                    bool isSaved = await connection.SavePowerOutageInfo(info);
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

            }
            catch (ArgumentOutOfRangeException)
            {
                await App.Current.MainPage.DisplayAlert("Input Error", "Data cannot be submitted at the moment. Try again later", "OK");
                return;
            }
            

        }
        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void Additional_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
            if (!String.IsNullOrWhiteSpace(Additional.Text))
            {
                additionalInfo = Additional.Text;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Clipboard.SetTextAsync(getID);

        }



        /* 
         public static string requestID
         {

             get { return returnID; }
             set { returnID = getID;}
         }
        */
    }
}