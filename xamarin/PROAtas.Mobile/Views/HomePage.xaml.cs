using Craftz.Views;
using PROAtas.Mobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views
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