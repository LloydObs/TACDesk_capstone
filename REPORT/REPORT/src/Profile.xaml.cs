using Firebase.Auth;
using REPORT;
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
    public partial class Profile : ContentPage
    {
        firebaseConnection connection = new firebaseConnection();

        public Profile()
        {
            InitializeComponent();
        }
        private async void logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("x_accountID");
            await Navigation.PushAsync(new Signin());
        }
        protected override async void OnAppearing()
        {
               var userLogin = await connection.GetProfileSelecteduser(ChooseUser.userID);
               completeName.Text = userLogin.Firstname + " " + userLogin.Lastname + " ";
               userID.Text = "#" + userLogin.userID;
               firstName.Text= userLogin.Firstname;
               lastName.Text = userLogin.Lastname;
               lot.Text = userLogin.lot;
               block.Text = userLogin.block;
               street.Text = userLogin.street;
               phoneNumber.Text = userLogin.PhoneNumber;
               password.Text = userLogin.password;
            
                        

        }
        protected override bool OnBackButtonPressed()
        {
            return true;

        }

        private async void Button_Edit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfile());
        }

        private async void Button_History(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new History());
        }

    }
}