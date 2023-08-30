
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        firebaseConnection connection = new firebaseConnection();

        public  HomePage()
        {

            InitializeComponent();
            //command when refreshing home page
            ReportView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
            loadActivityIndicator();

        }
        protected override async void OnAppearing()
        {
            try
            {
                var Reports = await connection.concatReports();
                ReportView.ItemsSource = null;
                ReportView.ItemsSource = Reports;
                ReportView.IsRefreshing = false;
                ShowNotif();
            }
            catch (NullReferenceException)
            {
                await App.Current.MainPage.DisplayAlert("Loading Data Encountered Errors", "Loading report data encountered. Try restarting the app.", "OK");
            }
           




        }
        private async void loadActivityIndicator()
        {
            loadingPopup.IsVisible = true;
            await Task.Delay(4000);
            loadingPopup.IsVisible = false;

        }
       

        private void ReportView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var reports = e.Item as reports;
            Navigation.PushAsync(new ReportDetail(reports));
            ((ListView)sender).SelectedItem = null;
  
        }



        private async void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string textVal =  searchBar.Text;
            if (!String.IsNullOrWhiteSpace(textVal))
            {
                var Reports = await connection.getSearchedReportType(textVal);
                ReportView.ItemsSource = null;
                ReportView.ItemsSource = Reports;
            }else
            {
                OnAppearing();
            }

        }

        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textVal = searchBar.Text;
            if (!String.IsNullOrWhiteSpace(textVal))
            {
                var Reports = await connection.getSearchedReportType(textVal);
                ReportView.ItemsSource = null;
                ReportView.ItemsSource = Reports;
            }else
            {
                OnAppearing();
            }
        }
        private async void ShowNotif()
        {

            var getCurrentCounter = await connection.concatReports();
            int currentCount = getCurrentCounter.Count();

            var previousCount = Application.Current.Properties.ContainsKey("PreviousCount") ? (int)Application.Current.Properties["PreviousCount"] : 0;
            if (currentCount > previousCount)
            {
                Application.Current.Properties["PreviousCount"] = currentCount;
                await Application.Current.SavePropertiesAsync();
                var notification = new NotificationRequest
                {
                    BadgeNumber = 1,
                    Description = $"There are {currentCount - previousCount} new approved reports in the Home Page.",
                    Title = "TACDesk",
                    NotificationId = 1337
                };
                await LocalNotificationCenter.Current.Show(notification);

            }

        }
        protected override bool OnBackButtonPressed()
        {
            //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

            return true;
        }



        /*
protected override bool OnBackButtonPressed()
{
//return base.OnBackButtonPressed();
return true;
}
*/
    }
}