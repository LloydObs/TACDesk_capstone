using Firebase.Auth;
using REPORT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        List<Entry> entry = new List<Entry>();
        firebaseConnection connection = new firebaseConnection();
        userAuth authenticateAcc = new userAuth();
        public static string userID = "";
        public static string userLoginToken = "";
        public static string firstName = "";
        public static string lastName = "";
        public static string phonenumber = "";
        public static string password = "";
        public static string street = "";
        public static string block = "";
        public static string lot = "";
        public static string saveOTPNumber = "";
        private string errorMessagePassword;
        public static string accountID;
        public Signup()
        {
            InitializeComponent();
            
            var streetList = new List<string>();
            streetList.Add("Succoth");
            streetList.Add("Gilead");
            streetList.Add("Nazareth");
            streetList.Add("Galatia");
            streetList.Add("Jericho");
            streetList.Add("Shiloh");
            streetList.Add("Sinai");
            streetList.Add("Sidon");
            streetList.Add("Berea");
            streetList.Add("Bethel");
            streetList.Add("Gilgal");
            streetList.Add("Bethlehem");
            streetList.Add("Lebanon");
            streetList.Add("Mt. of Olvies");
            streetList.Add("Bethsaida");
            streetList.Add("Bethany");
            streetList.Add("Beersheba");
            streetList.Add("Eden");
            streetList.Add("Judea");
            streetList.Add("Libnah");
            streetList.Add("Damascus");
            streetList.Add("Hebron");
            streetList.Add("Rome");
            streetList.Add("Corinth");
            streetList.Add("Malta");
            streetList.Add("Cyprus");
            streetList.Add("Gethsemane");
            streetList.Add("Mt. Carmel");
            streetList.Add("Isreal");
            streetList.Add("Ezekiel");
            streetList.Add("Joppa");
            streetList.Add("Lystra");
            streetList.Add("Socorro");
            streetList.Add("Canaan");
            streetList.Add("Midian");
            streetList.Add("Moab");
            streetList.Add("Jerusalem");
            streetList.Add("Mt. Tabor");
            streetList.Add("Thessalonica");
            streetList.Add("Tarsus");
            streetList.Add("Emmaus");
            streetList.Add("Tyre");
            streetList.Add("Ephesus");
            streetList.Add("Gadara");
            streetList.Add("Jordan");
            tacStreets.ItemsSource = streetList;

            
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Signin());
        }
        private async void Button_Create(object sender, EventArgs e)
        {
            
            
            try
            {
                //instantiates all entries from form
                firstName = FirstName.Text.Trim();
                lastName = LastName.Text.Trim();
                phonenumber = phoneNumber.Text.Trim();
                password = Password.Text.Trim();
                street = tacStreets.Items[tacStreets.SelectedIndex];
                //street = Street.Text;
                block = Block.Text.Trim();
                lot = Lot.Text.Trim();
                userID = connection.generateUserID();
                userLoginToken = connection.userSignInToken();
                bool checkPassword = ValidatePassword(password, out errorMessagePassword);
                accountID = connection.generateAccountID(firstName, lastName, block, lot, phonenumber);



                //input validation

                if (String.IsNullOrWhiteSpace(firstName))
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "wrong FName", "Ok");


                }
                else if (String.IsNullOrWhiteSpace(lastName))
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "wrong lname", "Ok");
                }
                else if (String.IsNullOrWhiteSpace(block))
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "wrongblock", "Ok");
                }
                else if (String.IsNullOrWhiteSpace(lot))
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Lot is Missing", "Ok");
                }
                else if (String.IsNullOrWhiteSpace(street))
                {
                    await App.Current.MainPage.DisplayAlert("Warning", " Street is Missing", "Ok");
                }
                else if (String.IsNullOrWhiteSpace(phonenumber))
                {
                    await App.Current.MainPage.DisplayAlert("Warning", " Phone Number is Missing", "Ok");
                }
                //checks if some entries are missing
                else if (checkPassword is false)
                {
                    await App.Current.MainPage.DisplayAlert("Warning", errorMessagePassword, "OK");

                }
                else
                {
                    //checks if there are similar numbers in the list of users 
                    var userLogin = await connection.checkBlock(block, lot);
                    if (userLogin != null)
                    {
                        if (block == userLogin.block && lot == userLogin.lot)
                        {
                            await App.Current.MainPage.DisplayAlert("Account Restriction Error", "One Account per household", "Ok");
                        }
                    }
                    else
                    {
                        saveOTPNumber = connection.generateOTPNumber().ToString();
                        //await App.Current.MainPage.DisplayAlert("asdasd", saveOTPNumber, "test");
                        connection.OTPtextInfo(saveOTPNumber, phonenumber);
                        await Navigation.PushAsync(new OtpPage());
                    }
                }

            }

            //add exception errors
            catch (NullReferenceException error)
            {
                //await App.Current.MainPage.DisplayAlert("Error", "Information cannot be added. Try again later.", "OK");
                await App.Current.MainPage.DisplayAlert("Error", error.Message, "OK");
                //throw new NullReferenceException("test message");


            }
            catch(ArgumentNullException error)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Information cannot be submitted. Try again later", "Ok");
            }
            catch (ArgumentOutOfRangeException)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Some fields are missing!!", "Ok");
            }



            
        }
        protected override bool OnBackButtonPressed()
        {
            return false;
        }
        


        
        
        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var passwordCredential = password;
            ErrorMessage = string.Empty;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpper = new Regex(@"[A-Z]+");
            var MinMaxChars = new Regex(@".{8,15}");
            var hasLower = new Regex(@"[a-z]+");
            //var hasSymbols = new Regex(@"[!@#$%^&*()_+=]");

            if (!hasLower.IsMatch(passwordCredential))
            {
                ErrorMessage = "Password needs atleast 1 lowercase";
                return false;
            }
            else if (!hasUpper.IsMatch(passwordCredential))
            {
                ErrorMessage = "Password needs atleast 1 uppercase letter.";
                return false;
            }else  if (!MinMaxChars.IsMatch(passwordCredential))
            {
                ErrorMessage = "Password length should be between 8 - 15 characters";
                return false;
            }else if (!hasNumber.IsMatch(passwordCredential))
            {
                ErrorMessage = "Password needs atleast 1 number";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void lastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z ]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        private void firstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z ]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
    }
}