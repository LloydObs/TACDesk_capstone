using Plugin.Media.Abstractions;
using REPORT;
using ReportIT.Model;
using ReportIT.ViewModels;
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
    public partial class NeighborhoodConcerns : ContentPage

    {
        firebaseConnection connection = new firebaseConnection();
        ComplaintModel complaintList = new ComplaintModel();
        neighborhoodconcernsInfo concernInfo = new neighborhoodconcernsInfo();
        string neighbhorhoodConcernID = "NC";
        public static string getID = "";
        string isDisccused = "";
        string imageStorageName = "NC_Reports_Images";
        string additionalInfo = "None Added";
        private string reportType = "Neighbhorhood Concerns";
        string others = "";
        public NeighborhoodConcerns()
        {
            InitializeComponent();
        }
        public interface IImageService
        {
            Task<MediaFile> GetMediaFileAsync(string fileName);
        }
        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                string issueName = Complaint.Items[Complaint.SelectedIndex];
                string incidentOccured = Occurence.Text;
                string accusedAddress = Address.Text;
                string IsApproved = "Pending";
                string reportStatus = "Pending";
                getID = await connection.generateReportID(neighbhorhoodConcernID);
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                var setDefaultPhoto = ImageSource.FromFile("photoNA.png");
                
              

                if (issueName == "Others")
                {
                    others = Others.Text;
                }
                else
                {
                    others = "Not Applicable";
                }

                neighborhoodconcernsInfo concernInfo = new neighborhoodconcernsInfo();
                concernInfo.issueName = issueName;
                concernInfo.incidentOccurence = incidentOccured;
                concernInfo.isDiscussed = isDisccused;
                concernInfo.AdditionalInfo = additionalInfo;
                concernInfo.accusedAddress = accusedAddress;
                concernInfo.IsApproved = IsApproved;
                concernInfo.reportID = getID;
                concernInfo.userID = getUserID;
                concernInfo.ReportStatus = reportStatus;
                concernInfo.timeAndDateReportPosted = currentTimeDate;
                concernInfo.reportType = reportType;
                concernInfo.Others = others;


                if (String.IsNullOrWhiteSpace(incidentOccured))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Occurence cannot be blanked", "Ok");
                    return;

                }
                else if (String.IsNullOrWhiteSpace(accusedAddress))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Address cannot be blanked", "Ok");
                    return;
                }

                if (TakePicture != null)
                {
                    string image = await connection.uploadPictureToStorage(TakePicture.GetStream(), getID + "_" + Path.GetFileName(TakePicture.Path), imageStorageName);
                    concernInfo.Image = image;

                }
                else
                {
                    var answer = await this.DisplayAlert("Submission Warning!!!", "Are you sure you don't want to add a photo to your report?", "Yes, Continue", "No");
                    if (answer == false)
                    {
                        return;
                    }
                    concernInfo.Image = string.Empty;
                    
                }

                var isSaved = await connection.SaveNeighborhoodConcernsInfo(concernInfo);
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
                await App.Current.MainPage.DisplayAlert("Input Error", "No Complaint is Selected", "OK");
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
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when uploading a file. Try restarting the App.", "OK");

            }
            finally
            {
                Mypopup.file = null;
            }







        }
        private void Complaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSelected = Complaint.Items[Complaint.SelectedIndex];

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

        private void No_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            isDisccused = No.Value.ToString();
        }
        private void Yes_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            isDisccused = Yes.Value.ToString();
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

        private void Additional_Unfocused(object sender, EventArgs e)
        {
                 //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"

                if (!String.IsNullOrWhiteSpace(AdditionalInfo.Text))
                {
                    additionalInfo = AdditionalInfo.Text;

                }

               


           
          
        }
        private void removeImage_Clicked(Object sender, EventArgs e)
        {
            try
            {
                pickedImage.IsVisible = false;
                Mypopup.file = null;

            }catch(NullReferenceException ex)
            {
                return;
            }
           



        }
        private void viewImage_Clicked(Object sender, EventArgs e)
        {

            Navigation.ShowPopup(new showImagePopup());



        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Clipboard.SetTextAsync(getID);

        }



    }
}