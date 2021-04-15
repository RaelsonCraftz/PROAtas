using Craftz.Views;
using PROAtas.Mobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : BasePage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Background thread
            Task.Run(async () =>
            {
                // Initialization buffer
                await Task.Delay(500);

                // Initial animation
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    background.FadeTo(1, 1000, Easing.CubicOut);
                    background.ScaleTo(1, 4000, Easing.CubicInOut);
                    stack.FadeTo(1, 2500, Easing.CubicOut);
                    stack.TranslateTo(0, 96, 2500, Easing.CubicOut);
                });

                // Await animations
                await Task.Delay(5000);

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Shell.Current.GoToAsync($"//{Routes.Main}");
                });
            });
        }
    }
}