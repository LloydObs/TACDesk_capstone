using ReportIT.Model;
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
    public partial class SubdivisionHazards : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string subdivisionHazardsID = "SH";
        public static string getID = "";
        string others = "";
        string imageStorageName = "SH_Reports_Images";
        string additionalInfo = "None Added";
        private string reportType = "Subdivision Hazards";
        public SubdivisionHazards()
        {
            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Live Wire");
            typelist.Add("Clogged Sewer");
            typelist.Add("Cracked Pavement");
            typelist.Add("Others");

            Issue.ItemsSource = typelist;
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                string issueName = Issue.Items[Issue.SelectedIndex];
                string location = Location.Text;
                getID = await connection.generateReportID(subdivisionHazardsID);
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                string isApproved = "Pending";
                string reportStatus = "Pending";
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");

                if (issueName == "Others")
                {
                    others = Others.Text;
                }
                else
                {
                    others = "Not Applicable";
                }

                subdivisionHazardsInfo hazardInfo = new subdivisionHazardsInfo();
                hazardInfo.issueName = issueName;
                hazardInfo.AdditionalInfo = additionalInfo;
                hazardInfo.location = location;
                hazardInfo.others = others;
                hazardInfo.IsApproved = isApproved;
                hazardInfo.reportID = getID;
                hazardInfo.userID = getUserID;
                hazardInfo.timeAndDateReportPosted = currentTimeDate;
                hazardInfo.ReportStatus = reportStatus;
                hazardInfo.reportType = reportType;

               


                if (String.IsNullOrWhiteSpace(location))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Location cannot be blanked", "Ok");
                    return;
                }
                else if (TakePicture != null)
                {
                    string image = await connection.uploadPictureToStorage(TakePicture.GetStream(), getID + "_" + Path.GetFileName(TakePicture.Path), imageStorageName);
                    hazardInfo.Image = image;
                }
                else
                {
                    var answer = await this.DisplayAlert("Submission Warning!!!", "Are you sure you don't want to add a photo to your report?", "Yes, Continue", "No");
                    if (answer == false)
                    {
                        return;
                    }
                    hazardInfo.Image = string.Empty;

                }

                var isSaved = await connection.SaveSubdivisionHazards(hazardInfo);
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
                await App.Current.MainPage.DisplayAlert("Input Error", "Issue cannot be blanked", "OK");
                return;
            }
            catch (ObjectDisposedException ex)
            {
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when uploading a picture. Try restarting the App.", "OK");

            }
            catch (NullReferenceException ex)
            {
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when submitting the report. Try restarting the App.", "OK");

            }
            catch (FileNotFoundException ex)
            {
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when uploading a picture. Try to restart the App.", "OK");

            }
            finally
            {
                Mypopup.file = null;
            }



        }
        private void Issue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSelected = Issue.Items[Issue.SelectedIndex];

            if (itemSelected == "Others")
            {
                Others.IsEnabled = true;
                Others.IsVisible = true;
            }
            else
            {
                Others.Text = "";
                Others.IsEnabled = false;
                Others.IsVisible = false;
            }

        }
        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void Button_Popup(object sender, EventArgs e)
        {
            try
            {
                var popup = new Mypopup();
                popup.Dismissed += Popup_Dismissed;
                await Navigation.ShowPopupAsync(popup);
            }
            catch (NullReferenceException ex)
            {
                return;
            }
            catch (ObjectDisposedException ex)
            {
                return;
            }

        }
        public void Popup_Dismissed(object sender, EventArgs e)
        {
            try
            {

                var TakePicture = Mypopup.file;
                if (TakePicture != null)
                {
                    pickedImage.IsVisible = true;
                    viewImage.Text = Path.GetFileName(TakePicture.Path);
                }

            }
            catch (NullReferenceException ex)
            {
                return;

            }
            catch (ObjectDisposedException ex)
            {
                return;
            }

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
        private void removeImage_Clicked(Object sender, EventArgs e)
        {
            try
            {
                pickedImage.IsVisible = false;
                Mypopup.file = null;
            }
            catch (NullReferenceException ex)
            {
                return;
            }
           


        }
        private void viewImage_Clicked(Object sender, EventArgs e)
        {
            Navigation.ShowPopup(new showImagePopup());
        }
        

    }
}