using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainPage : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        public MainPage()
        {
            InitializeComponent();
            /*
            if (Application.Current.Properties.ContainsKey("x_Agreed") == true)
            {
                Navigation.PushAsync(new Signin());
            }
            else
            {
                Navigation.PushAsync(new MainPage());
            }
            */
            //Connectivity.ConnectivityChanged += ConnectivityChangedHandler;

        }
        /*
        public async void ConnectivityChangedHandler(object sender, ConnectivityChangedEventArgs e)
        {
           await Navigation.PushAsync(new OtpPage());
        }
        */
        /*
        private void autoSignedIn()
        {
            
            Signin credentials = new Signin();
            Signup enteredCredentials = new Signup();
            Application.Current.Properties["x_phoneNumber"] = credentials.loggedInPhone;
            Application.Current.Properties["x_pass"] = credentials.loggInPassword;
            Application.Current.Properties["x_enteredPhoneNunmber"] = ;
            Application.Current.SavePropertiesAsync();
            
        }
        */
        private async void Button_Login(object sender, EventArgs e)
        {



            
            
            bool ischecked = Agree.IsChecked;

            if (ischecked == false)
            {
                var answer = await this.DisplayAlert("Agreement Notice", "Cannot proceed if you do not accept the Terms of Service and Privacy Policy ", "Yes", "No");
                if (answer == true)
                {
                    Agree.IsChecked = true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Agreement Notice", "Cannot Proceed since you do not accept the Terms of Service and Privacy Policy", "OK");
                }
            }
            else
            {
                Application.Current.Properties["x_Agreed"] = ischecked.ToString();
                await Application.Current.SavePropertiesAsync();;
                await Navigation.PushAsync(new Signin());
                
            }
            
        }
        
        private async void Button_Register(object sender, EventArgs e)
        {
            bool ischecked = Agree.IsChecked;

            if (ischecked == false)
            {
                var answer = await this.DisplayAlert("Agreement Notice", "Cannot proceed if you do not accept the Terms of Service and Privacy Policy ", "Yes", "No");
                if (answer == true)
                {
                    Agree.IsChecked = true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Agreement Notice", "Cannot Proceed since you do not accept the Terms of Service and Privacy Policy", "OK");
                }
            }
            else
            {
                Application.Current.Properties["x_Agreed"] = ischecked.ToString();
                await Application.Current.SavePropertiesAsync();

                await Navigation.PushAsync(new Signup());
            }
        }

        private void Button_Popup(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new PrivacyPolicy());
        }

        private void Button_Popup1(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new TermsOfUse());
        }
        /*
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("x_Agreed") == true)
            {
                Navigation.RemovePage(this);
            }
            else
            {
                Navigation.PushAsync(new MainPage());
            }
        }
        */


        /*
        private async void jumpToMainPage()
        {
            autoSignedIn();
            await Navigation.PushAsync(new HomePage1());
        }
        */
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}



