using Craftz.Pages;
using PROAtas.Mobile.ViewModel;
using PROAtas.ViewModel.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}