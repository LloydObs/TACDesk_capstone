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
    public partial class AddUser : ContentPage
    {
        public AddUser()
        {
            InitializeComponent();
        }
        firebaseConnection connection = new firebaseConnection();
        public static string accountID;
        public static string userID;

        private async void createAccount_Clicked(object sender, EventArgs e)
        {
            //get all data 
            string firstName = FirstName.Text;
            string lastName = LastName.Text;
            string block = Signin.block;
            string street = Signin.street;
            string phoneNumber = Signin.PhoneNumber;
            userID = connection.generateUserID();
            string password = Signin.passwordDb;
            string userLoginToken = Signin.userToken;
            string lot = Signin.lot;
            //accountID = connection.generateAccountID(firstName, lastName, block, lot, phoneNumber);
            accountID = Signin.accountID;

            userInfo user = new userInfo();
            user.Firstname = firstName;
            user.Lastname = lastName;
            user.PhoneNumber = phoneNumber;
            user.password = password;
            user.street = street;
            user.block = block;
            user.lot = lot;
            user.userID = userID;
            user.userToken = userLoginToken;
            user.accountID = accountID;

            var isSaved = await connection.AddUserInfo(user);
            if (isSaved)
            {
                await App.Current.MainPage.DisplayAlert("New Account Created", "Account is now Created", "ok");
                await Navigation.PushAsync(new ChooseUser());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Account Creation Error", "Account cannot be created. Try restarting the app.", "Ok");
            }
        }
    }
}