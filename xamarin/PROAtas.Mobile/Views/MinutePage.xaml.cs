using Craftz.Pages;
using PROAtas.Core;
using PROAtas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MinutePage : BasePage<MinuteViewModel, Minute>
    {
        public MinutePage()
        {
            InitializeComponent();

            viewModel = BindingContext as MinuteViewModel;
        }

        private MinuteViewModel viewModel;

        private void CreateTopic_Clicked(object sender, System.EventArgs e)
        {
            viewModel.CreateTopic.Execute(null);

            carousel.ScrollTo(viewModel.Minute.Topics.Count - 1);
            topicCollection.ScrollTo(viewModel.Minute.Topics.Count - 1);
        }
    }
}