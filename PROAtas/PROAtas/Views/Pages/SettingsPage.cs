using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Pages
{
    public class SettingsPage : TabbedPage
    {
        public SettingsPage() => Build();

        private void Build()
        {
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
                    Children =
                    {
                        new Button { Text = "Imagem" } .Center()
                            .Bind(nameof(vm.ChangeMinuteImage)),
                    }
                }
            });
        }
    }
}
