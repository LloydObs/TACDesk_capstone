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
    public partial class History : ContentPage
    {
        public History()
        {
            InitializeComponent();
            //command when refreshing home page
            ReportView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
            loadActivityIndicator();

        }



        firebaseConnection connection = new firebaseConnection();

        protected override async void OnAppearing()
        {
            var Reports = await connection.getUserHistory(ChooseUser.userID);
            ReportView.ItemsSource = null;
            ReportView.ItemsSource = Reports;
            ReportView.IsRefreshing = false;

            //to fix later
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
        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}