using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROAtas.Droid.Views
{
    [Activity(
        Theme = "@style/Theme.Presentation",
        Icon = "@drawable/ic_logo",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop)]
    public class PresentationActivity : Activity
    {
        Animation textAnimation;
        Animation scaleAnimation;
        TextView textView;
        ImageView imageView;

        TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.presentation_screen);

            textAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.appearfrombottom);
            textView = FindViewById<TextView>(Resource.Id.app_title);
            textView.StartAnimation(textAnimation);

            scaleAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.scaleandfadein);
            imageView = FindViewById<ImageView>(Resource.Id.background_pattern);
            imageView.StartAnimation(scaleAnimation);

            scaleAnimation.AnimationEnd += ContinueAfterAnimation;
            await tcs.Task;

            InvokeActivity();
        }

        private void ContinueAfterAnimation(object sender, Animation.AnimationEndEventArgs e)
        {
            tcs.SetResult(null);
        }

        private void InvokeActivity()
        {
            var mainActivity = new Intent(ApplicationContext, typeof(MainActivity));
            mainActivity.AddFlags(ActivityFlags.NoAnimation);

            StartActivity(mainActivity);

            SetPersistent(true);
        }

        // Prevents the user to kill content while splash screen is running
        public override void OnBackPressed() { }
    }
}