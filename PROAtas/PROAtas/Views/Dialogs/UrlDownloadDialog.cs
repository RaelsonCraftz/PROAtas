using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Behaviors;
using PROAtas.Controls;
using PROAtas.Converters;
using PROAtas.ViewModel;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;
using static PROAtas.Behaviors.MovingBehavior;

namespace PROAtas.Views.Dialogs
{
    public class UrlDownloadDialog : BaseDialog
    {
        public UrlDownloadDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

        public DownloadEntry downloadEntry;
        private void Build()
        {
            Content = new AbsoluteLayout
            {
                Children =
                {
                    // Black mask
                    new BoxView
                    {
                        GestureRecognizers = { new TapGestureRecognizer() .Invoke(l => l.Tapped += CancelDialog) }
                    } .Mask() .Invoke(c =>
                    {
                        AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 1, 1));
                        AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);
                    }),

                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Spacing = 10,

                            Children =
                            {
                                new Image { Source = Images.Url },

                                new Frame
                                {
                                    Padding = 5, BackgroundColor = Colors.TextIcons,

                                    Content = new Grid
                                    {
                                        ColumnDefinitions = Columns.Define(
                                            (0, 32),
                                            (1, Star),
                                            (2, 32)),

                                        Children =
                                        {
                                            // Downloading Entry
                                            new DownloadEntry { Margin = 0, Placeholder = "Url", IsEnabled = false, Visual = VisualMarker.Default } .Standard()
                                                .Assign(out downloadEntry)
                                                .Col(1)
                                                .Bind(DownloadEntry.SaveCommandProperty, nameof(SettingsViewModel.DownloadUrl)),
                                            
                                            // Busy indicator
                                            new ActivityIndicator { Color = Colors.Primary } .Size(32)
                                                .Col(0)
                                                .Bind(nameof(DownloadEntry.IsSaving), source: downloadEntry),
                                            
                                            // Header image
                                            new Image
                                            {
                                                Source = Images.UrlBlack,
                                                Behaviors =
                                                {
                                                    new MovingBehavior { MoveTo = EMoveTo.End }
                                                        .BindBehavior(MovingBehavior.IsActiveProperty, nameof(DownloadEntry.IsSaving), source: downloadEntry, converter: new BoolToInvert()),
                                                    new FadingBehavior { }
                                                        .BindBehavior(FadingBehavior.IsActiveProperty, nameof(DownloadEntry.IsSaving), source: downloadEntry, converter: new BoolToInvert())
                                                }
                                            } .Col(0) .Size(32),
                                        }
                                    } .Standard(),
                                },

                                new Frame
                                {
                                    BackgroundColor = Colors.TextIcons,

                                    Content = new Image { } .Size(96)
                                        .Bind(nameof(SettingsViewModel.DownloadedImage))
                                } .Padding(5) .Size(128) .CenterExpand(),

                                new Button { ImageSource = Images.Check } .Success() .Round(48) .Right()
                                    .Bind(nameof(SettingsViewModel.SelectUrlImage))
                            }
                        },
                    } .Standard() .Invoke(c =>
                    {
                        InnerContent = c;

                        AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 0.8, 0.7));
                        AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);
                    }),
                }
            };
        }
    }
}
