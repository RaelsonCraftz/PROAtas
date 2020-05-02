using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Behaviors;
using PROAtas.Services;
using PROAtas.ViewModel;
using PROAtas.Views.Dialogs;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Craftz.Views.BaseDialog;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Pages
{
    public class SettingsPage : BaseTabbedPage<SettingsViewModel>
    {
        #region Service Container

        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        public SettingsPage() => Build();

        private UrlDownloadDialog urlDialog;
        private void Build()
        {
            var app = App.Current;
            var vm = ViewModel = App.Current.settingsViewModel;

            Title = "Configurações";

            Behaviors.Add(new ActiveTabbedPageBehavior());

            // Main settings
            Children.Add(new BasePage
            {
                Title = "Geral",
                IconImageSource = Images.Organization,

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

            Children.Add(new BasePage
            {
                Title = "Margem",
                IconImageSource = Images.Margin,

                Content = new Grid
                {
                    RowSpacing = 0,
                    ColumnSpacing = 0,

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

            Children.Add(new BasePage
            {
                Title = "Imagem",
                IconImageSource = Images.Image,

                Content = new AbsoluteLayout
                {
                    Children =
                    {
                        new StackLayout
                        {
                            Spacing = 5,

                            Children =
                            {
                                new Image { } .CenterExpand() .Size(128)
                                    .Bind($"{nameof(vm.SelectedImage)}"),

                                new ScrollView
                                {
                                    Content = new StackLayout
                                    {
                                        Spacing = 5, Padding = 20,

                                        Children =
                                        {
                                            new Button { ImageSource = Images.Collection, Text = "Galeria" } .Standard() .FillH()
                                                .Bind(nameof(vm.OpenGallery)),

                                            new Button { ImageSource = Images.Url, Text = "Url" } .Standard() .FillH()
                                                .Bind(nameof(vm.OpenUrl)),

                                            new Button { ImageSource = Images.Storage, Text = "Armazenados" } .Standard() .FillH()
                                                .Bind(nameof(vm.OpenStorage)),
                                        }
                                    } .Padding(new Thickness(50, 0, 50, 0)),
                                },
                            },
                        } .Padding(new Thickness(0, 50, 0, 50)) .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 1, 1));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);
                        }),
                        
                        new ImageCollectionDialog(vm, EDockTo.Start) { }
                            .Bind(ImageCollectionDialog.IsOpenProperty, nameof(vm.IsImageDialogOpen))
                            .Invoke(c =>
                            {
                                AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 1, 1));
                                AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);

                                c.Opening += () =>
                                {
                                    c.imageCollection.SelectedItem = null;
                                };

                                c.Close += () =>
                                {
                                    vm.IsImageDialogOpen = false;
                                };
                            }),

                        new UrlDownloadDialog(EDockTo.Start) { }
                            .Assign(out urlDialog)
                            .Bind(ImageCollectionDialog.IsOpenProperty, nameof(vm.IsUrlDialogOpen))
                            .Invoke(c =>
                            {
                                AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 1, 1));
                                AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);

                                c.Opening += async () =>
                                {
                                    vm.DownloadedImage = null;
                                    c.downloadEntry.Text = string.Empty;
                                    c.downloadEntry.IsSavingEnabled = true;

                                    if (Clipboard.HasText)
                                        if (Uri.TryCreate(await Clipboard.GetTextAsync(), UriKind.Absolute, out Uri url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps))
                                        {
                                            c.downloadEntry.Text = url.AbsoluteUri;

                                            toastService.ShortAlert("Texto copiado para a url de Download!");
                                        }
                                };

                                c.Close += () =>
                                {
                                    c.downloadEntry.IsSavingEnabled = false;
                                    vm.IsUrlDialogOpen = false;
                                };
                            }),
                    }
                }
            });
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (ViewModel.IsUrlDialogOpen && Clipboard.HasText)
                if (Uri.TryCreate(await Clipboard.GetTextAsync(), UriKind.Absolute, out Uri url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps))
                {
                    urlDialog.downloadEntry.Text = url.AbsoluteUri;

                    toastService.ShortAlert("Texto copiado para a url de Download!");
                }
        }
    }
}
