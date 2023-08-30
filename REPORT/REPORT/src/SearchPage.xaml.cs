using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReportIT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void IncidentEntry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
            
        }
        

    }
}