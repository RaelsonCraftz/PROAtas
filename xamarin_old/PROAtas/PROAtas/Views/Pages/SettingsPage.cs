using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Behaviors;
using PROAtas.Controls;
using PROAtas.Core;
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
        private CustomEntry userEntry;
        private CustomEntry organizationEntry;

        private CustomEntry leftEntry;
        private CustomEntry topEntry;
        private CustomEntry rightEntry;
        private CustomEntry bottomEntry;
        private void Build()
        {
            var app = App.Current;
            var vm = ViewModel = App.Current.settingsViewModel;

            Title = "Configurações";

            // Main settings
            Children.Add(new BasePage
            {
                Title = "Geral",
                IconImageSource = Images.Organization,

                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(0, 5, 0, 0),
                        Spacing = 10,

                        Children =
                        {
                            new Frame
                            {
                                Content = new Frame { } .FramedCustomEntry(out userEntry, Images.PersonBlack)
                                    .Invoke(c =>
                                    {
                                        userEntry.Placeholder = "Usuário";
                                        userEntry.Bind(CustomEntry.SaveCommandProperty, nameof(vm.SaveUser));
                                        userEntry.Bind(CustomEntry.TextProperty, nameof(vm.User));
                                        userEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                                    }),
                            } .Standard(),

                            new Frame
                            {
                                Content = new Frame { } .FramedCustomEntry(out organizationEntry, Images.OrganizationBlack)
                                    .Invoke(c =>
                                    {
                                        organizationEntry.Placeholder = "Organização";
                                        organizationEntry.Bind(CustomEntry.SaveCommandProperty, nameof(vm.SaveOrganization));
                                        organizationEntry.Bind(CustomEntry.TextProperty, nameof(vm.Organization));
                                        organizationEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                                    }),
                            } .Standard(),

                            new Frame { } .FramedHorizontalLabelInput(Images.FontSize, "Tamanho", nameof(vm.FontSize), nameof(vm.ChangeFontSize)),

                            new Frame { } .FramedPicker(Images.FontSize, "Fonte", nameof(vm.FontFamily), nameof(vm.ChangeFontFamily), vm.FontList),

                            new AdMobView { AdUnitId = Constants.AdOrganizacao, HeightRequest = 50 } .BottomExpand(),
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
                        (0, Star),
                        (1, Star),
                        (2, Star),
                        (3, 50)),

                    ColumnDefinitions = Columns.Define(
                        (0, Star),
                        (1, Star),
                        (2, Star)),

                    Children =
                    {
                        new Frame { } .FramedVerticalEntryInput(out topEntry, Images.MarginTop, nameof(vm.ChangeMarginTop)) .Center()
                            .Row(0) .Col(1)
                            .Invoke(c =>
                            {
                                topEntry.Placeholder = "0,00";
                                topEntry.Bind(CustomEntry.TextProperty, nameof(vm.MarginTop));
                                topEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                            }),

                        new Frame { } .FramedVerticalEntryInput(out leftEntry, Images.MarginLeft, nameof(vm.ChangeMarginLeft)) .Center()
                            .Row(1) .Col(0)
                            .Invoke(c =>
                            {
                                leftEntry.Placeholder = "0,00";
                                leftEntry.Bind(CustomEntry.TextProperty, nameof(vm.MarginLeft));
                                leftEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                            }),

                        new Frame { } .FramedVerticalEntryInput(out rightEntry, Images.MarginRight, nameof(vm.ChangeMarginRight)) .Center()
                            .Row(1) .Col(2)
                            .Invoke(c =>
                            {
                                rightEntry.Placeholder = "0,00";
                                rightEntry.Bind(CustomEntry.TextProperty, nameof(vm.MarginRight));
                                rightEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                            }),

                        new Frame { } .FramedVerticalEntryInput(out bottomEntry, Images.MarginBottom, nameof(vm.ChangeMarginBottom)) .Center()
                            .Row(2) .Col(1)
                            .Invoke(c =>
                            {
                                bottomEntry.Placeholder = "0,00";
                                bottomEntry.Bind(CustomEntry.TextProperty, nameof(vm.MarginBottom));
                                bottomEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                            }),

                        new AdMobView { AdUnitId = Constants.AdMargem }
                            .Row(3) .ColSpan(3),
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

                                new AdMobView { AdUnitId = Constants.AdImagem, HeightRequest = 50 } .BottomExpand(),
                            },
                        } .Padding(new Thickness(0, 50, 0, 0)) .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 1, 1));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);
                        }),
                        
                        // Black mask
                        new BoxView
                        {
                            BackgroundColor = Color.Black, Opacity = 0, InputTransparent = true,

                            Behaviors =
                            {
                                new FadingBehavior(0.5)
                                    .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.IsLocked))
                            },

                            GestureRecognizers = { new TapGestureRecognizer() .Invoke(l => l.Tapped += (s, e) => CloseDialogs()) }
                        } .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0, 0, 1, 1));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.SizeProportional);
                        }),

                        // Dialogs
                        new ImageCollectionDialog(vm, EDockTo.Start) { }
                            .Bind(ImageCollectionDialog.IsOpenProperty, nameof(vm.IsImageDialogOpen))
                            .Invoke(c =>
                            {
                                AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 0.8, 0.7));
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
                                AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 0.8, 0.7));
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

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            CloseDialogs();
        }

        private void CloseDialogs()
        {
            ViewModel.IsImageDialogOpen = false;
            ViewModel.IsUrlDialogOpen = false;
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.IsImageDialogOpen || ViewModel.IsUrlDialogOpen)
            {
                CloseDialogs();
                return true;
            }

            return false;
        }
    }
}
