
using Plugin.Media;
using Plugin.Media.Abstractions;
using ReportIT.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = Xamarin.Forms.Image;
using Stream = System.IO.Stream;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConstructionWorkersIssues : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();
        string constructionIssueID = "CWI";
        public static string getID = "";
        string others = "";
        string isDiscussed = "";
        MediaFile file;
        string imageStorageName = "CWI_Reports_Images";
        private string additionalInfo = "None Added";
        private string reportType = "Construction Worker Issues";

        public ConstructionWorkersIssues()
        {

            InitializeComponent();

            var typelist = new List<string>();
            typelist.Add("Dirt and dust control");
            typelist.Add("Noise outside reasonable working hours");
            typelist.Add("Not wearing ID");
            typelist.Add("Not wearing proper uniform");
            typelist.Add("Others");
            Issue.ItemsSource = typelist;
           


           

        }

        private async void Button_Back(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        
        private void Issue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSelected = Issue.Items[Issue.SelectedIndex];
            //if others selected, others text box set to enabled and visible
            if (itemSelected == "Others")
            {
                Others.IsEnabled = true;
                Others.IsVisible = true;
            }
            //else, disabled, set text to null, invisible
            else
            {
                Others.Text = "";
                Others.IsEnabled = false;
                Others.IsVisible = false;

            }
        }
        
        
        private async void Submit_Clicked(Object sender, EventArgs e)
        {

            try
            {
                Mypopup getPictures = new Mypopup();
                string issueName = Issue.Items[Issue.SelectedIndex];
                string occurence = Occurence.Text;
                string location = Location.Text;
                string isApproved = "Pending";
                string reportStatus = "Pending";
                getID = await connection.generateReportID(constructionIssueID);
                string currentTimeDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                var getUserID = ChooseUser.userID;
                var TakePicture = Mypopup.file;
                int issueValue = Issue.SelectedIndex;

                //sets others var to a value
                if (issueName == "Others")
                {
                    
                    others = Others.Text;
                }
                else
                {
                    others = "Not Applicable";
                }
                
                constructionworkerissuesInfo issueInfo = new constructionworkerissuesInfo();
                issueInfo.issueName = issueName;
                issueInfo.Others = others;
                issueInfo.Occurence = occurence;
                issueInfo.isDiscussed = isDiscussed;
                issueInfo.Location = location;
                issueInfo.AdditionalInfo = additionalInfo;
                issueInfo.IsApproved = isApproved;
                issueInfo.reportID = getID;
                issueInfo.userID = getUserID;
                issueInfo.ReportStatus = reportStatus;
                issueInfo.timeAndDateReportPosted = currentTimeDate;
                issueInfo.reportType = reportType;



                if (String.IsNullOrWhiteSpace(occurence))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Occurence cannot be blanked", "Ok");
                    return;
                }
                else if (String.IsNullOrWhiteSpace(location))
                {
                    await App.Current.MainPage.DisplayAlert("Input Error", "Location cannot be blanked", "Ok");
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


                var isSaved = await connection.SaveConstructionWorkerIssueInfo(issueInfo);
                if (isSaved)
                {
                    ReportSubmitted.getRefID = getID;
                    await Navigation.PushAsync(new ReportSubmitted());

                }
                else
                {
                    await DisplayAlert("Report Information Warning", "Information cannot be submitted now. Try Again.", "OK");

                }
            }catch(ArgumentOutOfRangeException ex)
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

                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when submitting the report. Try restarting the App.", "OK");


            }
            catch (FileNotFoundException ex)
            {
                await App.Current.MainPage.DisplayAlert("Uploading Image Error!!!", "Problems encountered when uploading a file. Try restarting the App.", "OK");

            }
            finally
            {
               //empties the image variable
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
            } catch (NullReferenceException ex)
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
                //shows the show image button after uploading an image
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
        
        private void Yes_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            isDiscussed = Yes.Value.ToString();
        }

        private void No_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            isDiscussed = No.Value.ToString();
        }
        private void removeImage_Clicked(Object sender, EventArgs e)
        {
            try
            {
                //removes the image and sets the button visibility to false
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

        
  





    }
}