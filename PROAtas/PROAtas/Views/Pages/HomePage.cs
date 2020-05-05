using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Behaviors;
using PROAtas.Controls;
using PROAtas.Converters;
using PROAtas.Core;
using PROAtas.ViewModel;
using PROAtas.Views.DataTemplates;
using PROAtas.Views.Dialogs;
using Xamarin.Forms;
using static Craftz.Views.BaseDialog;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Pages
{
    public class HomePage : BasePage<HomeViewModel>
    {
        enum Row { Content, Banner }

        public HomePage() => Build();

        public OptionDialog optionDialog;

        public CollectionView minuteCollection;
        private void Build()
        {
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);

            Title = "PRO Atas";

            var app = App.Current;
            var vm = ViewModel = App.Current.homeViewModel;

            var searchHandler = new CustomSearchHandler
            {
                Placeholder = "Pesquisa...",
                BackgroundColor = Colors.TextIcons,
            };
            searchHandler.SetBinding(CustomSearchHandler.SearchCommandProperty, nameof(vm.SearchMinute));
            Shell.SetSearchHandler(this, searchHandler);

            Content = new AbsoluteLayout
            {
                Children =
                {
                    // Page Content
                    new Grid
                    {
                        RowDefinitions = Rows.Define(
                                    (Row.Content, GridLength.Star),
                                    (Row.Banner, new GridLength(50))),

                        Children =
                        {
                            new CollectionView { ItemTemplate = MinuteTemplate.New() } .VerticalListStyle() .SingleSelection()
                                .Assign(out minuteCollection)
                                .Row(0)
                                .Bind(CollectionView.ItemsSourceProperty, nameof(vm.Minutes))
                                .Bind(CollectionView.SelectionChangedCommandProperty, nameof(HomeViewModel.SelectMinute), source: vm)
                                .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: minuteCollection),

                            new Button { ImageSource = Images.Add, Margin = 10 } .Standard() .Bottom() .Right() .Round()
                                .Row(0)
                                .Bind(nameof(vm.CreateMinute)),

                            new AdMobView { AdUnitId = Constants.AdHome }
                                .Row(1),
                        }
                    } .Standard(),

                    // Black mask
                    new BoxView
                    {
                        BackgroundColor = Color.Black, Opacity = 0, InputTransparent = true,

                        Behaviors =
                        {
                            new FadingBehavior(0.5)
                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.SelectedMinute), converter: new NullToBool())
                        },

                        GestureRecognizers = { new TapGestureRecognizer() .Invoke(l => l.Tapped += (s, e) => CloseDialogs()) }
                    } .Invoke(c =>
                    {
                        AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0, 0, 1, 1));
                        AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.SizeProportional);
                    }),
                    
                    // Dialogs
                    new OptionDialog("Opções", "Editar", "Gerar Word", "Gerar PDF", "Deletar", Images.Edit, Images.Word, Images.PDF, Images.Delete, thirdTextColor: Colors.Success, dockSide: EDockTo.Start)
                            { LastWarning = "Deseja mesmo remover este arquivo?" }
                        .Assign(out optionDialog) . Center()
                        .Bind(OptionDialog.IsOpenProperty, nameof(vm.SelectedMinute), converter: new NullToBool())
                        .Bind(OptionDialog.FirstCommandProperty, nameof(vm.EditMinute))
                        .Bind(OptionDialog.SecondCommandProperty, nameof(vm.PrintWord))
                        .Bind(OptionDialog.ThirdCommandProperty, nameof(vm.PrintPDF))
                        .Bind(OptionDialog.LastCommandProperty, nameof(vm.DeleteMinute))
                        .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 1, 1));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);

                            c.Close += () =>
                            {
                                minuteCollection.SelectedItem = null;
                            };
                        }),
                },
            };
        }

        private void CloseDialogs()
        {
            optionDialog.CancelDialog();
        }
    }
}