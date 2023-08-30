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
    public partial class UnauthorizedSolicitation : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string unauthorizedSolicitationID = "US";
        public static string getID = "";
        string imageStorageName = "US_Reports_Images";
        string additional = "None Added";
        private string reportType = "Unauthorized Solicitation";
        public UnauthorizedSolicitation()
        {
            InitializeComponent();
        }

        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        
        private async void Submit_Clicked(object sender, EventArgs e)
        {

            try
            {
                string reason = Reason.Text;
                string description = Description.Text;
                string timeofIncident = timeOfIncident.Time.ToString(@"hh\:mm");
                string dateofIncident = dateOfIncident.Date.ToString("MMM d, yyyy");
                string isApproved = "Pending";
                getID = await connection.generateReportID(unauthorizedSolicitationID);
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                string reportStatus = "Pending";



                unauthorizedSolicitationInfo issueInfo = new unauthorizedSolicitationInfo();
                issueInfo.Reason = reason;
                issueInfo.Description = description;
                issueInfo.AdditionalInfo = additional;
                issueInfo.timeOfIncident = timeofIncident;
                issueInfo.DateOfIncident = dateofIncident;
                issueInfo.IsApproved = isApproved;
                issueInfo.reportID = getID;
                issueInfo.userID = getUserID;
                issueInfo.timeAndDateReportPosted = currentTimeDate;
                issueInfo.ReportStatus = reportStatus;
                issueInfo.reportType = reportType;


                if (String.IsNullOrWhiteSpace(reason))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error","Reason for Solicitation cannot be blanked","Ok");
                    return;
                }else if (String.IsNullOrWhiteSpace(description))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Description cannot be blanked", "Ok");
                    return;
                }
                else if (TakePicture != null)
                {
                    string image = await connection.uploadPictureToStorage(TakePicture.GetStream(), getID + "_" + Path.GetFileName(TakePicture.Path), imageStorageName);
                    issueInfo.Image = image;

                }
                else
                {
                    var answer = await this.DisplayAlert("Submission Warning!!!", "Are you sure you don't want to add a photo to your report?", "Yes, Continue", "No");
                    if (answer == false)
                    {
                        return;
                    }
                    issueInfo.Image = string.Empty;
                }

                var isSaved = await connection.SaveUnauthorizedSolicitation(issueInfo);
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
                await App.Current.MainPage.DisplayAlert("Input Error", "Information cannot be submitted at the moment. Try Again", "Ok");
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

            if (!String.IsNullOrWhiteSpace(Additional.Text))
            {
                additional = Additional.Text;

            }
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
        
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