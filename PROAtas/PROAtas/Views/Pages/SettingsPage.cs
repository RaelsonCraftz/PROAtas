using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using PROAtas.Views.Dialogs;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;
using static PROAtas.Views.Dialogs.BaseDialog;

namespace PROAtas.Views.Pages
{
    public class SettingsPage : TabbedPage
    {
        public SettingsPage() => Build();

        private void Build()
        {
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

            var app = App.Current;
            var vm = App.Current.settingsViewModel;

            Title = "Configurações";

            // Main settings
            Children.Add(new BasePage<SettingsViewModel>
            {
                Title = "Geral",
                IconImageSource = Images.Organization,
                ViewModel = vm,

                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = 5,
                        Spacing = 10,

                        Children =
                        {
                            new Frame
                            {
                                Content = new Frame { } .FramedCustomEntry(Images.PersonBlack, "Usuário", nameof(vm.SaveUser), nameof(vm.User)),
                            } .Standard(),

                            new Frame
                            {
                                Content = new Frame { } .FramedCustomEntry(Images.OrganizationBlack, "Organização", nameof(vm.SaveOrganization), nameof(vm.Organization)),
                            } .Standard(),

                            new Frame { } .FramedHorizontalLabelInput(Images.FontSize, "Tamanho", nameof(vm.FontSize), nameof(vm.ChangeFontSize)),

                            new Frame { } .FramedPicker(Images.FontSize, "Fonte", nameof(vm.FontFamily), nameof(vm.ChangeFontFamily), vm.FontList)
                        }
                    }
                },
            });

            Children.Add(new BasePage<SettingsViewModel>
            {
                Title = "Margem",
                IconImageSource = Images.Margin,
                ViewModel = vm,

                Content = new Grid
                {
                    RowDefinitions = Rows.Define(
                        (0, GridLength.Star),
                        (1, GridLength.Star),
                        (2, GridLength.Star)),

                    ColumnDefinitions = Columns.Define(
                        (0, GridLength.Star),
                        (1, GridLength.Star),
                        (2, GridLength.Star)),

                    Children =
                    {
                        new Frame { } .FramedVerticalEntryInput(Images.MarginTop, "Topo", nameof(vm.MarginTop), nameof(vm.ChangeMarginTop)) .Center()
                            .Row(0) .Col(1),

                        new Frame { } .FramedVerticalEntryInput(Images.MarginLeft, "Esq.", nameof(vm.MarginLeft), nameof(vm.ChangeMarginLeft)) .Center()
                            .Row(1) .Col(0),

                        new Frame { } .FramedVerticalEntryInput(Images.MarginRight, "Dir.", nameof(vm.MarginRight), nameof(vm.ChangeMarginRight)) .Center()
                            .Row(1) .Col(2),

                        new Frame { } .FramedVerticalEntryInput(Images.MarginBottom, "Fundo", nameof(vm.MarginBottom), nameof(vm.ChangeMarginBottom)) .Center()
                            .Row(2) .Col(1),
                    },
                }
            });

            Children.Add(new BasePage<SettingsViewModel>
            {
                Title = "Imagem",
                IconImageSource = Images.Image,
                ViewModel = vm,

                Content = new Grid
                {
                    RowDefinitions = Rows.Define(
                        (0, new GridLength(2, GridUnitType.Star)),
                        (1, GridLength.Auto)),

                    RowSpacing = 5,
                    ColumnSpacing = 0,

                    Children =
                    {
                        new Image { } .Top() .Size(128)
                            .Row(0)
                            .Bind($"{nameof(vm.SelectedImage)}"),

                        new ScrollView
                        {
                            Content = new StackLayout
                            {
                                Spacing = 5,

                                Children =
                                {
                                    new Button { ImageSource = Images.Collection, Text = "Imagens" } .Standard() .FillExpandH()
                                        .Bind(nameof(vm.ChooseCollection)),

                                    new Button { ImageSource = Images.Url, Text = "Url" } .Standard() .FillExpandH()
                                        .Bind(nameof(vm.ChooseUrl)),

                                    new Button { ImageSource = Images.Storage, Text = "Armazenados" } .Standard() .FillExpandH()
                                        .Bind(nameof(vm.ChooseStorage)),
                                }
                            } .Padding(new Thickness(50, 0, 50, 0)),
                        } .Row(1) .Bottom(),

                        //new ImageStorageDialog(EDockTo.End) { }
                        //    .Assign(out ImageStorageDialog imageStorageDialog)
                        //    .RowSpan(2)
                        //    .Invoke(l => l.Close += () =>
                        //    {
                        //        vm.IsImageDialogOpen = false;
                        //    }),
                    },
                }.Center(),
            });
        }
    }
}
