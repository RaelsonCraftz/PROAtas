using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PROAtas.Controls;
using PROAtas.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TextButton), typeof(TextButtonDroidRenderer))]
namespace PROAtas.Droid.Renderers
{
    public class TextButtonDroidRenderer : ButtonRenderer
    {
        public TextButtonDroidRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Gravity = GravityFlags.Start;
                Control.SetAllCaps(false);
            }
        }
    }
}