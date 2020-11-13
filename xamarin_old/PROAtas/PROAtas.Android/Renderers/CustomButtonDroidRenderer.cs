using Android.Content;
using Android.Views;
using PROAtas.Controls;
using PROAtas.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonDroidRenderer))]
namespace PROAtas.Droid.Renderers
{
    public class CustomButtonDroidRenderer : ButtonRenderer
    {
        public CustomButtonDroidRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.Gravity = GravityFlags.Start;
        }
    }
}