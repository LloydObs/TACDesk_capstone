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
    public partial class Mypopup : Popup
    {

        public static MediaFile file;
        public static MediaFile defaultFile;


        public Mypopup()
        {
            InitializeComponent();
        }

        private async void Button_Upload(System.Object sender, System.EventArgs e)
        {
            try
            {
                await PickPhoto();
                Dismiss(null);

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
        public async void Button_TakePicture(System.Object sender, System.EventArgs e)
        {
            try
            {
                
                await takePicture();
                Dismiss(null);
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


        public async Task<Stream> PickPhoto()
        {

            try
            {
                await CrossMedia.Current.Initialize();
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {

                }
                pickedImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });

            }
            catch (Exception ex)
            {

            }
            
            return file.GetStream();


        }


        public async Task<Stream> takePicture()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    SaveToAlbum = false,
                    DefaultCamera = CameraDevice.Rear,
                    CompressionQuality = 92
                }); 
                pickedImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });

            }
            catch (Exception ex)
            {

            }
            
            return file.GetStream();
        }
       
     
    }
}