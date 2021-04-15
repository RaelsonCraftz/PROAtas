using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;

namespace PROAtas.Droid.Views
{
    [Activity(
        Theme = "@style/Theme.Splash",
        Icon = "@drawable/ic_logo", 
        MainLauncher = true, 
        NoHistory = true, 
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InvokeActivity();
        }

        private void InvokeActivity()
        {
            var mainActivity = new Intent(ApplicationContext, typeof(MainActivity));
            mainActivity.AddFlags(ActivityFlags.NoAnimation);

            StartActivity(mainActivity);
        }

        // Prevents the user to kill content while splash screen is running
        public override void OnBackPressed() { }
    }
}