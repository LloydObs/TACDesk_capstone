using ReportIT;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReportPage : ContentPage
    {
        public AddReportPage()
        {
            InitializeComponent();

            
        }

        private async void LAF_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LostAndFound());
        }

        private async void DSP_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SubdivisionHazards());
        }

        private async void PO_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PowerOutage());
        }

        private async void WI_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WaterInterruption());
        }

        private async void OTHERS_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PowerOutage());
        }

        private async void NC_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NeighborhoodConcerns());
        }
    }
}