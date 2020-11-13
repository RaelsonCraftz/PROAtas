using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Controls;
using PROAtas.ViewModel;
using Xamarin.Forms;

namespace PROAtas.Views.Dialogs
{
    public class InformationDialog : BaseDialog
    {
        public InformationDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

        public CustomEditor customEditor;
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

                        new Frame { } .FramedCustomEditor(out customEditor)
                            .FillExpand() .Invoke(c =>
                            {
                                customEditor.Placeholder = "Digite a informação...";
                                customEditor.Bind(CustomEditor.SaveCommandProperty, nameof(MinuteViewModel.SaveInformation));
                                customEditor.Bind(CustomEditor.TextProperty, $"{nameof(MinuteViewModel.SelectedInformation)}.{nameof(MinuteViewModel.SelectedInformation.Text)}");
                                customEditor.Bind(CustomEditor.IsSavingProperty, nameof(MinuteViewModel.IsSavingInformation), BindingMode.OneWayToSource);
                            }),
                    }
                }
            } .Standard();
        }
    }
}
