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
    public partial class ChooseUser : ContentPage
    {
        public ChooseUser()
        {
            InitializeComponent();
        }
        firebaseConnection connection = new firebaseConnection();
        public static string firstName;
        public static string lastName;
        public static string phoneNumber;
        public static string accountID;
        public static string block;
        public static string lot;
        public static string password;
        public static string street;
        public static string userID;
        public static string userToken;
        public static string countAllUser;
        protected override async void OnAppearing()
        {
            //store account ID if logging in 
            if(Application.Current.Properties.ContainsKey("x_accountID") == true)
            {
                string myVal = Application.Current.Properties["x_accountID"].ToString();
                var userlist = await connection.GetAllUserFromSpecificAccount(myVal);
                UserList.ItemsSource = userlist;
            }
            else
            {
                var userList = await connection.GetAllUserFromSpecificAccount(Signin.accountID);
                UserList.ItemsSource = userList;
            }
            


           
        }

         private void ReportView_ItemTapped(object sender, ItemTappedEventArgs e)
         {
            
            if (e.Item == null)
            {
                return;
            }

            ((ListView)sender).SelectedItem = null;
            var userDetails = e.Item as userInfo;

           

            Navigation.PushAsync(new HomePage1());
            firstName = userDetails.Firstname;
            lastName = userDetails.Lastname;
            phoneNumber = userDetails.PhoneNumber;
            accountID = userDetails.accountID;
            block = userDetails.block;
            lot = userDetails.lot;
            password = userDetails.password;
            street = userDetails.street;
            userID = userDetails.userID;
            userToken = userDetails.userToken;
           
           
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            //counter for user count
            int countAllUsers;
            if (Application.Current.Properties.ContainsKey("x_accountID") == true)
            {
                string myVal = Application.Current.Properties["x_accountID"].ToString();
                var userList = await connection.GetAllUserFromSpecificAccount(myVal);
                UserList.ItemsSource = userList;

                countAllUsers = userList.Count();
                if (countAllUsers == 2)
                {
                    await App.Current.MainPage.DisplayAlert("Maximum account reached", "Only 2 Profiles can be created per account!!", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new AddUser());
                }
            }
            else
            {
               var userList = await connection.GetAllUserFromSpecificAccount(Signin.accountID);
                UserList.ItemsSource = userList;
                countAllUsers = userList.Count();
                if (countAllUsers == 2)
                {
                    await App.Current.MainPage.DisplayAlert("Maximum account reached", "Only 2 Profiles can be created per account!!", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new AddUser());
                }
            }

         

          
           
           
            //countAllUser = connection.CountAllDataFromAccount(userID);

        }
        protected override bool OnBackButtonPressed()
        {
            // return true to not navigate back to home page
            return true;
            
        }


    }
}