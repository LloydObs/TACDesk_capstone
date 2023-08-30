using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using System.ComponentModel;
using REPORT.Droid;
using Android.Service.Controls;

[assembly: ResolutionGroupName("PlainEntryGroup")]
[assembly: ExportEffect(typeof(AndroidPlainEntryEffect), "PlainEntryEffect")]

namespace REPORT.Droid
{
    public class AndroidPlainEntryEffect:PlatformEffect
    {
        public AndroidPlainEntryEffect()
        {


        }

        protected override void OnAttached()
        {
            try
            {
                if (Control != null)
                {
                    Android.Graphics.Color entryLineColor = Android.Graphics.Color.White;
                    Control.BackgroundTintList = ColorStateList.ValueOf(entryLineColor);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error... Unable to set property on attached control", ex.Message);
            }
        }


        protected override void OnDetached()
        {
            //throw new NotImplementedException();

        }
       

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }
        
    }
}