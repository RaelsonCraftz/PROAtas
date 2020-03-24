using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using PROAtas.Views.DataTemplates;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Dialogs
{
    public class ImageStorageDialog : BaseDialog
    {
        enum Row { Header, Content, Action }

        public ImageStorageDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

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
                            (1, GridLength.Star)),

                        Margin = new Thickness(50, 30, 50, 30),
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
                                        (Row.Content, GridLength.Star),
                                        (Row.Action, GridLength.Auto)),

                                    Padding = 5,
                                    RowSpacing = 5, ColumnSpacing = 0,

                                    // Dialog Content
                                    Children =
                                    {
                                        new Image { Source = Images.Storage } .Center()
                                            .Row(Row.Header),

                                        new CollectionView { ItemTemplate = MinuteImageTemplate.New() } .GridStyle(ItemsLayoutOrientation.Vertical, 3, 5)
                                            .Assign(out CollectionView imageCollection)
                                            .Row(Row.Content)
                                            .Bind(CollectionView.ItemsSourceProperty, nameof(SettingsViewModel.ImageCollection)),
                                    }
                                }
                            } .Standard() .Row(1),
                        }
                    } .Transparent() .Assign(out Grid innerContent)
                }
            };

            InnerContent = innerContent;
        }
    }
}
