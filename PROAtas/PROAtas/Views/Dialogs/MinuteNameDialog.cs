using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Dialogs
{
    public class MinuteNameDialog : BaseDialog
    {
        enum Row { Header, Content }

        public MinuteNameDialog() => Build();

        private void Build()
        {
            Content = new Grid
            {
                Children =
                {
                    // Black mask
                    new BoxView
                    {
                        GestureRecognizers = { new TapGestureRecognizer() .Invoke(l => l.Tapped += CancelDialog) }
                    } .Mask(),

                    new Grid
                    {
                        RowDefinitions = Rows.Define(
                            (0, GridLength.Auto),
                            (1, GridLength.Auto)),

                        Margin = new Thickness(50, 80, 50, 80),
                        RowSpacing = 5, ColumnSpacing = 0,

                        Children =
                        {
                            // Close button
                            new ImageButton { Source = Images.Close, BackgroundColor = Color.Transparent } .Center()
                                .Row(0)
                                .Invoke(l => l.Clicked += CancelDialog),

                            // Border
                            new Frame
                            {
                                Content = new Grid
                                {
                                    RowDefinitions = Rows.Define(
                                        (Row.Header, GridLength.Auto),
                                        (Row.Content, GridLength.Auto)),

                                    Padding = 5,
                                    RowSpacing = 5, ColumnSpacing = 0,

                                    // Dialog Content
                                    Children =
                                    {
                                        new Image { Source = Images.Minute } .Center()
                                            .Row(Row.Header),

                                        new Frame { } .FramedCustomEntry(Images.MinuteBlack, "Nome da Ata", nameof(MinuteViewModel.RenameMinute), nameof(MinuteViewModel.MinuteName), isSavingPath: nameof(MinuteViewModel.IsSavingMinuteName))
                                            .Row(Row.Content),
                                    }
                                }
                            } .Standard() .Row(1) .CenterV()
                        }
                    } .Transparent(),
                }
            };
        }
    }
}
