using Android.App;
using Android.Content;
using Android.Graphics.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using REPORT;
using REPORT.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(REPORT.CustomLabel), typeof(CustomLabelRender))]
namespace REPORT.Droid
{
    public class CustomLabelRender:LabelRenderer
    {
        public CustomLabelRender(Context context):base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.JustificationMode = (Android.Text.JustificationMode)JustificationMode.InterWord;
            }
        }

    }
}