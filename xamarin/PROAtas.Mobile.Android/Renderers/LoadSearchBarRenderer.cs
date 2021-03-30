using Android.Content;
using Android.Widget;
using PROAtas.Controls;
using PROAtas.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoadSearchBar), typeof(LoadSearchBarRenderer))]
namespace PROAtas.Droid.Renderers
{
    public class LoadSearchBarRenderer : SearchBarRenderer
    {
        public LoadSearchBarRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Android.App.Application.Context.GetDrawable(Resource.Drawable.round_corner);
                Control.SetPadding(10, 0, 0, 0);

                var searchView = Control as SearchView;

                int searchPlateId = searchView.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                Android.Views.View searchPlateView = searchView.FindViewById(searchPlateId);
                searchPlateView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}