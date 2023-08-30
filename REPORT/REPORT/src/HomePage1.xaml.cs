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
    public partial class HomePage1 : TabbedPage
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
        public HomePage1()
        {
            InitializeComponent();
            Navigation.PopAsync();
            
            



        }


    }
}