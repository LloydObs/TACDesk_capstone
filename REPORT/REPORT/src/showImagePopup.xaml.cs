using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
namespace REPORT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class showImagePopup : Popup
    {
        public showImagePopup()
        {
            InitializeComponent();
            try
            {
                //converts image to stream para maview yung image
                ImageSource getSource = ImageSource.FromStream(() => Mypopup.file.GetStream());

                viewImage.Source = getSource;
                

            }
            catch (NullReferenceException ex)
            {
                return;

            }
            catch (ObjectDisposedException ex)
            {
                return;

            }


        }

        private void goBack_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}