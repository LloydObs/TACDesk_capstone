using REPORT;
using ReportIT.effects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LostAndFound : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string lostenFoundID = "LAF";
        public static string getID = "";
        string userType = "";
        string imageStorageName = "LAF_Reports_Images";
        string itemColor = "None Added";
        string itemBrand = "None Added";
        string itemQuantity = "None Added";
        string itemMaterial = "None Added";
        private string reportType = "Lost and Found";
        public LostAndFound()
        {

            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Cash");
            typelist.Add("Clothing");
            typelist.Add("Electronic Device");
            typelist.Add("Personal Belonging");
            typelist.Add("Pet");

            Category.ItemsSource = typelist;
        }


        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                string itemName = ItemName.Text;
                string issueName = Category.Items[Category.SelectedIndex];
                string venueLastSeen = Venue.Text;
                string dateLastSeen = selectedDate.Date.ToString("MMM d, yyyy");
                string timeLastSeen = TimeLastSeen.Time.ToString(@"hh\:mm");
                getID = await connection.generateReportID(lostenFoundID);
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                string isApproved = "Pending";
                string reportStatus = "Pending";
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                //to optimize all later


                lostandfoundInfo userInfo = new lostandfoundInfo();
                userInfo.itemName = itemName;
                userInfo.issueName = issueName;
                userInfo.userType = userType;
                userInfo.venueLastSeen = venueLastSeen;
                userInfo.dateLastSeen = dateLastSeen;
                userInfo.timeLastSeen = timeLastSeen;
                userInfo.itemMaterial = itemMaterial;
                userInfo.brand = itemBrand;
                userInfo.itemQuantity = itemQuantity;
                userInfo.itemColor = itemColor;
                userInfo.reportID = getID;
                userInfo.userID = getUserID;
                userInfo.IsApproved = isApproved;
                userInfo.timeAndDateReportPosted = currentTimeDate;
                userInfo.ReportStatus = reportStatus;
                userInfo.reportType = reportType;

                if (String.IsNullOrWhiteSpace(itemName))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Item Name cannot be blanked", "Ok");
                    return;

                }
                else if (String.IsNullOrWhiteSpace(venueLastSeen))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Venue cannot be blanked", "Ok");
                    return;
                }
                else if (TakePicture != null)
                {
                    string image = await connection.uploadPictureToStorage(TakePicture.GetStream(), getID + "_" + Path.GetFileName(TakePicture.Path), imageStorageName);
                    userInfo.Image = image;

                }
                else
                {
                    var answer = await this.DisplayAlert("Submission Warning!!!", "Are you sure you don't want to add a photo to your report?", "Yes, Continue", "No");
                    if (answer == false)
                    {
                        return;
                    }
                    userInfo.Image = string.Empty;


                }
                var isSaved = await connection.SavelostInfo(userInfo);
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
                await App.Current.MainPage.DisplayAlert("Input Error", "No Item Category is Selected", "Ok");
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
        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void LostItem_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            userType = lostItem.Value.ToString();
        }

        private void FoundItem_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            userType = foundItem.Value.ToString();
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
        private void viewImage_Clicked(Object sender, EventArgs e)
        {
            Navigation.ShowPopup(new showImagePopup());
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

            private void Brand_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
            if (!String.IsNullOrWhiteSpace(Brand.Text))
            {
                itemBrand = Brand.Text;

            }
            
        }

        private void Material_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
            if (!String.IsNullOrWhiteSpace(Material.Text))
            {
                itemMaterial = Material.Text;

            }
        }

        private void Quantity_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
            if (!String.IsNullOrWhiteSpace(Quantity.Text))
            {
                itemQuantity = Quantity.Text;

            }
        }

        private void Color_Unfocused(object sender, EventArgs e)
        {
            //if additional entry is focused then set additional variable to entry box value of additional, else additional info set to "None added"
            if (!String.IsNullOrWhiteSpace(Color.Text))
            {
                itemColor = Color.Text;

            }
        }
    }
}