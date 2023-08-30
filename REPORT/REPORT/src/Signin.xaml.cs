using Firebase.Auth;
using Newtonsoft.Json;
using REPORT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database.Query;
namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signin : ContentPage
    {
        public string loggedInPhone { get; set; }
        public string loggInPassword { get; set; }
        firebaseConnection connection = new firebaseConnection();
        public static string getID = "";
        public static string firstName = "";
        public static string lastName = "";
        public static string passwordDb = "";
        public static string block = "";
        public static string street = "";
        public static string lot = "";
        public static string PhoneNumber = "";
        public static string userToken = "";
        public static string saveOTPNumber = "";
        public static string accountID = "";
        public Signin()
        {
            
            InitializeComponent();

            
            
            


        }
        
       
        private async void Button_Login(object sender, EventArgs e)
        {

            try
            {
                //instantiate entry from sign in form
                string phoneNumber = phoneNumberBox.Text;
                string password = passwordBox.Text;

                
                //checks if entries are empty
                if (passwordBox.Text is null)
                {
                    
                    //checkCredentials.Text = "Username and Password is Missing.";
                    //checkCredentials.IsVisible = true;
                    await App.Current.MainPage.DisplayAlert("Credential Error", "Phone Number is Missing!!", "Ok");
                    return;
                }
                else if (phoneNumberBox.Text is null)
                {
                    // checkCredentials.Text = "Phone Number is Missing.";
                    await App.Current.MainPage.DisplayAlert("Credential Error", "Password is Missing!!", "Ok");
                    return;



                }
                else if (String.IsNullOrEmpty(phoneNumberBox.Text) && String.IsNullOrEmpty(passwordBox.Text))
                {
                    //checkCredentials.Text = "Password is Missing";
                    await App.Current.MainPage.DisplayAlert("Credential Error", "Phone Number and Password is Missing!!", "Ok");
                    return;
                }
                else
                {

                    var userLogin = await connection.GetUser(phoneNumber);
                    if (userLogin != null)
                    
                    {
                        if (phoneNumber == userLogin.PhoneNumber && password == userLogin.password)
                        {
                            firstName = userLogin.Firstname;
                            lastName = userLogin.Lastname;
                            getID = userLogin.userID;
                            block = userLogin.block;
                            passwordDb = userLogin.password;
                            street = userLogin.street;
                            lot = userLogin.lot;
                            PhoneNumber = userLogin.PhoneNumber;
                            userToken = userLogin.userToken;
                            accountID = userLogin.accountID;

                            Application.Current.Properties["x_accountID"] = accountID.ToString();
                            await Application.Current.SavePropertiesAsync(); 
                            //autoSignedIn();

                            await Navigation.PushAsync(new ChooseUser());
                        }
                        else
                            await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                        
                       
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
            }

            

        }


        private async void Button_Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signup());
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }









    }
}