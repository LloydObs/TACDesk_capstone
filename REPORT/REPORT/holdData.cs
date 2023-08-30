using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace REPORT
{
    public class holdData : ContentPage
    {

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
        public holdData(userInfo userDetails)
        {
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
    }
}