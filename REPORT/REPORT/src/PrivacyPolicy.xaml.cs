using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyPolicy : Popup
    {
        public PrivacyPolicy()
        {
            InitializeComponent();
        }
    }
}