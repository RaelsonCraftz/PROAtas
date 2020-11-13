using PROAtas.Droid.Services;
using PROAtas.Services;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(UriServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class UriServiceDroid : IUriService
    {
        public void OpenStoreLink(string link)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse(link);
            Android.Content.Intent intent = new Android.Content.Intent(Android.Content.Intent.ActionView, uri);

            intent.SetPackage("com.playstore.android");
            if (intent == null) Android.App.Application.Context.StartActivity(intent);
            else Device.OpenUri(new Uri(link));
        }
    }
}