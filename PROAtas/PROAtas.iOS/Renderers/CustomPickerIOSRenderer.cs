using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PROAtas.Controls;
using PROAtas.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerIOSRenderer))]
namespace PROAtas.iOS.Renderers
{
    public class CustomPickerIOSRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}