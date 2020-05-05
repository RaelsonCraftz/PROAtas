using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using Xamarin.Forms;

namespace PROAtas.Views.Dialogs
{
    public class InformationDialog : BaseDialog
    {
        public InformationDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

        private void Build()
        {
            Content = new Frame
            {
                Content = new StackLayout
                {
                    // Dialog Content
                    Children =
                    {
                        new Image { Source = Images.Text } .Center(),

                        new Frame { } .FramedCustomEditor(nameof(MinuteViewModel.SaveInformation), nameof(MinuteViewModel.IsSavingInformation), $"{nameof(MinuteViewModel.SelectedInformation)}.{nameof(MinuteViewModel.SelectedInformation.Text)}")
                            .FillExpand(),
                    }
                }
            } .Standard();
        }
    }
}
