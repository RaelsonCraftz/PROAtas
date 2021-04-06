using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PROAtas.Droid.Renderers;
using PROAtas.Mobile.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoadEntry), typeof(LoadEntryDroidRenderer))]
namespace PROAtas.Droid.Renderers
{
    public class LoadEntryDroidRenderer : EntryRenderer
    {
        public LoadEntryDroidRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Android.App.Application.Context.GetDrawable(Resource.Drawable.round_corner);
                Control.Gravity = GravityFlags.CenterVertical;
                Control.SetPadding(10, 0, 0, 0);
            }
        }
    }
}