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
    public class MinuteNameDialog : BaseDialog
    {
        public MinuteNameDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

        public CustomEntry minuteNameEntry;
        private void Build()
        {
            Content = new Frame
            {
                Content = new StackLayout
                {
                    // Dialog Content
                    Children =
                    {
                        new Image { Source = Images.Minute } .Center(),

                        new Frame { } .FramedCustomEntry(out minuteNameEntry, Images.MinuteBlack)
                            .Invoke(c =>
                            {
                                minuteNameEntry.Placeholder = "Nome da Ata";
                                minuteNameEntry.Bind(CustomEntry.SaveCommandProperty, nameof(MinuteViewModel.RenameMinute));
                                minuteNameEntry.Bind(CustomEntry.TextProperty, nameof(MinuteViewModel.MinuteName));
                                minuteNameEntry.Bind(CustomEntry.IsSavingProperty, nameof(MinuteViewModel.IsSavingMinuteName));
                            }),
                    }
                }
            } .Standard() .CenterExpandV();
        }
    }
}
