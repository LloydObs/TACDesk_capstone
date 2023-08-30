using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class IllegalVendors : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string illegalVendorID = "IV";
        public static string getID = "";
        string others = "";
        string imageStorageName = "IV_Reports_Images";
        string additionalInfo = "None Added";
        private string reportType = "Illegal Vendors";
        public IllegalVendors()
        {
            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Clothing/Accessories/Jewelries");
            typelist.Add("Food");
            typelist.Add("Gadgets");
            typelist.Add("Health related products");
            typelist.Add("Others");
            Product.ItemsSource = typelist;
        }

        private void Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSelected = Product.Items[Product.SelectedIndex];

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

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                string issueName = Product.Items[Product.SelectedIndex];
                string occurence = Occurence.Text;
                string description = Description.Text;
                string dateIncident = dateOfIncident.Date.ToString("MMM d, yyyy");
                string timeIncident = timeOfIncident.Time.ToString(@"hh\:mm");
                string IsApproved = "Pending";
                getID = await connection.generateReportID(illegalVendorID);
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                string reportStatus = "Pending";

                if (issueName == "Others")
                {

                    others = Others.Text;
                }
                else
                {
                    others = "Not Applicable";
                }


                illegalVendorsInfo issueInfo = new illegalVendorsInfo();
                issueInfo.issueName = issueName;
                issueInfo.Others = others;
                issueInfo.Occurence = occurence;
                issueInfo.Description = description;
                issueInfo.dateOfIncident = dateIncident;
                issueInfo.timeOfIncident = timeIncident;
                issueInfo.AdditionalInfo = additionalInfo;
                issueInfo.IsApproved = IsApproved;
                issueInfo.reportID = getID;
                issueInfo.userID = getUserID;
                issueInfo.ReportStatus = reportStatus;
                issueInfo.timeAndDateReportPosted = currentTimeDate;
                issueInfo.reportType = reportType;

                if (String.IsNullOrWhiteSpace(occurence))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Occurence line cannot be blanked", "Ok");
                    return;
                }
                else if (String.IsNullOrWhiteSpace(description))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Description line cannot be blanked", "Ok");
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

                var isSaved = await connection.SaveIllegalVendorInfo(issueInfo);
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
            catch (ArgumentOutOfRangeException ex)
            {
                await App.Current.MainPage.DisplayAlert("Input Error", "No Issue is Selected", "Ok");
                return;
            }
            catch (ObjectDisposedException ex)
            {
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when uploading a picture. Try restarting the App.", "OK");

            }
            catch (NullReferenceException ex)
            {
                ReportSubmitted.getRefID = getID;
                await Navigation.PushAsync(new ReportSubmitted());
            }
            catch (FileNotFoundException ex)
            {
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when uploading a picture. Try to restart the App.", "OK");

            }
            finally
            {
               //empties image file after submission
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

        private void Additional_Unfocused(object sender, EventArgs e)
        {
                     
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"

                if (!String.IsNullOrWhiteSpace(Additional.Text))
                {
                    additionalInfo = Additional.Text;

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