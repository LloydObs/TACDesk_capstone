using ReportIT;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Expander_Experimental" });
            //checks if properties of account id has value then proceeds to choosing multiple accounts
            if(Application.Current.Properties.ContainsKey("x_accountID") == true){
                MainPage = new NavigationPage(new ChooseUser());
            }
            //checks if user agreed to privacy policy
            else if (Application.Current.Properties.ContainsKey("x_Agreed") == true)
            {
                MainPage = new NavigationPage(new Signin());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
           
        }


    }
}
