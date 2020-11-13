using PROAtas.ViewModels;
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