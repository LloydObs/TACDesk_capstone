
using Firebase.Auth;
using ReportIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtpPage : ContentPage
    {
        Random otpNumber = new Random();
        firebaseConnection connection = new firebaseConnection();
        public static string newOTP = "";

        public OtpPage()
        {
            InitializeComponent();
            
        }
        private void Entry_Focused_1(object sender, FocusEventArgs e)
        {
            box1.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_2(object sender, FocusEventArgs e)
        {
            box2.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_3(object sender, FocusEventArgs e)
        {
            box3.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_4(object sender, FocusEventArgs e)
        {
            box4.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_5(object sender, FocusEventArgs e)
        {
            box5.BorderColor = Color.ForestGreen;
        }

        private void Entry_Focused_6(object sender, FocusEventArgs e)
        {
            box6.BorderColor = Color.ForestGreen;
        }

        private void box1_Unfocused(object sender, FocusEventArgs e)
        {
            box1.BorderColor = Color.Gray;
            box2.BorderColor = Color.Gray;
            box3.BorderColor = Color.Gray;
            box4.BorderColor = Color.Gray;
            box5.BorderColor = Color.Gray;
            box6.BorderColor = Color.Gray;
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            
            string getBox1 = value1.Text;
            string getBox2 = value2.Text;
            string getBox3 = value3.Text;
            string getBox4 = value4.Text;
            string getBox5 = value5.Text;
            string getBox6 = value6.Text;
            //concat numbers
            string getAllNumbers = getBox1 + getBox2 + getBox3 + getBox4 + getBox5 + getBox6;
            string getOTP = Signup.saveOTPNumber;
            string getOTPSignUp = newOTP;
     
            
            userInfo user = new userInfo();
            user.Firstname = Signup.firstName;
            user.Lastname = Signup.lastName;
            user.PhoneNumber = Signup.phonenumber;
            user.password = Signup.password;
            user.street = Signup.street;
            user.block = Signup.block;
            user.lot = Signup.lot;
            user.userID = Signup.userID;
            user.userToken = Signup.userLoginToken;
            user.accountID = Signup.accountID;


            if (getAllNumbers == getOTP || getAllNumbers == getOTPSignUp)
            {
                var isSaved = await connection.Save(user);
                if (isSaved)
                {
                    await App.Current.MainPage.DisplayAlert("New Account Created", "Account is now Created", "ok");
                    await Navigation.PushAsync(new Signin());

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Account Creation Error", "Account cannot be created. Try restarting the app.", "Ok");
                }

            
            }
            
           



        }
        private void resendOTP_Clicked(object sender, EventArgs e)
        {
            newOTP = connection.generateOTPNumber().ToString();
            connection.OTPtextInfo(newOTP, Signup.phonenumber);
            return;

            
        }
    }
}