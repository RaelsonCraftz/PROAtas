using Android.Widget;
using PROAtas.Droid.Services;
using PROAtas.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class ToastServiceDroid : IToastService
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}