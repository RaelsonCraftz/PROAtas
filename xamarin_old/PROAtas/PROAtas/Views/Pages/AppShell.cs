using CSharpForMarkup;
using PROAtas.Assets.Theme;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Views.Pages
{
    public class AppShell : Shell
    {
        public AppShell() => Build();

        private void Build()
        {
            // Shell Routes for Shell Navigation
            Routing.RegisterRoute(nameof(MinutePage), typeof(MinutePage));

            FlyoutHeader = new Grid()
            {
                Padding = 10,

                BackgroundColor = Colors.TextIcons,
                Children =
                {
                    new Image() { Source = Images.Logo } .Right(),

                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = "PRO Atas", FontSize = 20, TextColor = Colors.Accent, FontAttributes = FontAttributes.Bold },
                            new Label { Text = VersionTracking.CurrentVersion, FontSize = 14, TextColor = Colors.Accent, FontAttributes = FontAttributes.Italic },
                        }
                    } .Left(),
                },
            };

            Items.Add(new FlyoutItem()
            {
                Title = "Início",
                Icon = Images.HomeBlack,
                Items =
                {
                    new ShellContent { Content = new HomePage(), Route = "Home", }
                }
            });

            Items.Add(new FlyoutItem()
            {
                Title = "Configurações",
                Icon = Images.SettingsBlack,
                Items =
                {
                    new ShellContent { Content = new SettingsPage(), Route = "Settings", }
                }
            });

            Items.Add(new MenuItem()
            {
                Text = "Sobre Mim",
                IconImageSource = Images.AboutBlack,
                Command = new Command(async () =>
                {
                    await Current.Navigation.PushModalAsync(new AboutPage(), true);
                    FlyoutIsPresented = false;
                }),
            });
        }

        protected override bool OnBackButtonPressed()
        {
            var page = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

            if (page.SendBackButtonPressed())
                return true;
            else
                return base.OnBackButtonPressed();
        }
    }
}