using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        public Command TouchCommand { get; }
        public ReportPage()
        {
            InitializeComponent();

            TouchCommand = new Command(() => emergencyCall());

            BindingContext = this;
        }

        private async void Button_CWC(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConstructionWorkersIssues());
        }

        private async void Button_IV(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IllegalVendors());
        }

        private async void Button_LAF(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LostAndFound());
        }

        private async void Button_PO(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PowerOutage());
        }

        private async void Button_PP(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PermanentParking());
        }

        private async void Button_SPE(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubdivisionHazards());
        }

        private async void Button_NC(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NeighborhoodConcerns());
        }

        private async void Button_US(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UnauthorizedSolicitation());
        }

        private async void Button_WI(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WaterInterruption());
        }

        private void Button_Home(object sender, EventArgs e)
        {
             Navigation.ShowPopup(new BeginnerPopup());
        }
        private void emergencyCall()
        {
            try
            {
                PhoneDialer.Open("Phone Number");
            }
            catch (FeatureNotSupportedException ex)
            {
                App.Current.MainPage.DisplayAlert("Feature Error", "Feature is not currently available on your phone.", "Ok");
            }
            catch (ArgumentNullException ex)
            {
                App.Current.MainPage.DisplayAlert("Call Error", "Number your calling is not in the system", "Ok");
            }
            catch (Exception ex)
            {
                    App.Current.MainPage.DisplayAlert("Error calling", ex.Message, "oK");
            }
            

        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }

    }
