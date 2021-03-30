using Craftz.Pages;
using PROAtas.ViewModels;
using PROAtas.ViewModels.Elements;
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