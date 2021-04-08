using Craftz.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : BasePage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void PlayStore_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmação", "Você será levado para uma página na Play Store, deseja continuar?", "Sim", "Não"))
                _ = Launcher.OpenAsync($"https://play.google.com/store/apps/developer?id=Raelson+Craftz");
        }

        private void AppStore_Clicked(object sender, EventArgs e)
        {

        }
    }
}