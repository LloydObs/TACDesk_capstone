using Firebase.Auth;
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
    public partial class EditProfile : ContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
        }
        firebaseConnection connection = new firebaseConnection();
        private async void save_Clicked(object sender, EventArgs e)
        {
           
            string newFirstName = firstName.Text;
            string newLastName = lastName.Text;
            string newBlock = block.Text;
            string newStreet = street.Text;
            string newPassword = password.Text;
            string newPhoneNumber = phoneNumber.Text;
            string newLot = lot.Text;
            string userToken = Signin.userToken;
            string userID = ChooseUser.userID;
            string accountID = ChooseUser.accountID;

            userInfo newInfo = new userInfo();
            newInfo.Firstname = newFirstName;
            newInfo.Lastname = newLastName;
            newInfo.block = newBlock;
            newInfo.street = newStreet;
            newInfo.password = newPassword;
            newInfo.PhoneNumber = newPhoneNumber;
            newInfo.lot = newLot;
            newInfo.userToken = userToken;
            newInfo.userID = userID;
            newInfo.accountID = accountID;



            var isSaved = await connection.Update(newInfo);
           
            if (isSaved)
            {


                await App.Current.MainPage.DisplayAlert("Account Credentials Update", "User data is updated", "Ok");
                await Navigation.PushAsync(new HomePage1());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Account Credential Update", "Current data cannot be updated at the moment.Try again later.", "Ok");
            }



        }
        protected override async void OnAppearing()
        {
            //shows user data 
            var userLogin = await connection.GetProfileSelecteduser(ChooseUser.userID);
            firstName.Text = userLogin.Firstname;
            lastName.Text = userLogin.Lastname;
            lot.Text = userLogin.lot;
            block.Text = userLogin.block;
            street.Text = userLogin.street;
            phoneNumber.Text = userLogin.PhoneNumber;
            password.Text = userLogin.password;



        }
    }
}