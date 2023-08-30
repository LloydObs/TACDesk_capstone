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
    public partial class PermanentParking : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string stickerPresent;
        string isOwnerKnown;
        string permanentparkingID = "PP";
        public static string getID = "";
        string imageStorageName = "PP_Reports_Images";
        string additionalInfo = "None Added";
        private string reportType = "Permanent Parking";
        string address;
        public PermanentParking()
        {
            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Commercial Sticker");
            typelist.Add("HOA sticker");
            typelist.Add("Outsider sticker");
            Sticker.ItemsSource = typelist;
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                address = Address.Text;
                string stickerType = Sticker.Items[Sticker.SelectedIndex];
                var getStickerValue = Sticker.SelectedIndex;
                getID = await connection.generateReportID(permanentparkingID);
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                string isApproved = "Pending";
                string reportStatus = "Pending";
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");


                permanentParkingInfo issueInfo = new permanentParkingInfo();
                issueInfo.Address = address;
                issueInfo.AdditionalInfo = additionalInfo;
                issueInfo.issueName = stickerType;
                issueInfo.reportID = getID;
                issueInfo.hasSticker = stickerPresent;
                issueInfo.isOwnerKnown = isOwnerKnown;
                issueInfo.userID = getUserID;
                issueInfo.IsApproved = isApproved;
                issueInfo.ReportStatus = reportStatus;
                issueInfo.timeAndDateReportPosted = currentTimeDate;
                issueInfo.reportType = reportType;


                if (knownOwner.IsChecked && String.IsNullOrEmpty(address))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Address cannot be blanked since you selected you know the owner.", "Ok");
                    return;
                }
                else if (hasSticker.IsChecked && Sticker.SelectedItem is false)
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Sticker Type cannot be blanked since you selected it has a sticker", "Ok");
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

                var isSaved = await connection.SavePermanentParkingInfo(issueInfo);
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

                await App.Current.MainPage.DisplayAlert("Input Error", "No Sticker is Selected", "Ok");
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
        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void knownOwner_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            isOwnerKnown = knownOwner.Value.ToString();
        }

        private void unknownOwner_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            isOwnerKnown = unknownOwner.Value.ToString();   

        }

        private void hasSticker_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            stickerPresent = hasSticker.Value.ToString();
        }

        private void noSticker_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            stickerPresent = noSticker.Value.ToString();
        }

        private void Additional_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
           
                if (!String.IsNullOrWhiteSpace(AdditionalInfo.Text))
                {
                    additionalInfo = AdditionalInfo.Text;

                }

              





        
         }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Clipboard.SetTextAsync(getID);

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