using Craftz.Views;
using PROAtas.Core;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MinutePage : BasePage<MinuteViewModel, Minute>
    {
        public MinutePage()
        {
            InitializeComponent();
        }

        private void CreateTopic_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.CreateTopic?.Execute(null);

            carousel.ScrollTo(ViewModel.Minute.Topics.Count - 1);
            topicCollection.ScrollTo(ViewModel.Minute.Topics.Count - 1);
        }
    }
}