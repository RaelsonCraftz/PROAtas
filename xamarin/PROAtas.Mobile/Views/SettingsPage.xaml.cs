using Craftz.Views;
using PROAtas.Mobile.ViewModel;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : BasePage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}