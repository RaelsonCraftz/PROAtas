using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Pages
{
    public class AboutPage : BasePage
    {
        #region Service Container

        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        enum Row { Image, Description, Contact, Stores }

        enum Col { PlayStore, AppStore }

        public AboutPage() => Build();

        private void Build()
        {
            var app = App.Current;

            Content = new AbsoluteLayout
            {
                Children =
                {
                    new Grid
                    {
                        Padding = new Thickness(10, 50, 10, 10),

                        RowDefinitions = Rows.Define(
                            (Row.Image, 128),
                            (Row.Description, Star),
                            (Row.Contact, 40),
                            (Row.Stores, 48)),

                        ColumnDefinitions = Columns.Define(
                            (Col.PlayStore, Star),
                            (Col.AppStore, Star)),

                        Children =
                        {
                            new Frame
                            {
                                Padding = 0, CornerRadius = 6, BackgroundColor = Colors.Divider,

                                Content = new Image { Source = "imgAuthor.jpg" },
                            } .Center()
                                .Row(Row.Image) .ColSpan(2),

                            new ScrollView
                            {
                                Content = new Label 
                                { 
                                    Text = "Olá!\r\n\r\nEu me chamo Raelson. Sou Engenheiro Civil de formação e desenvolvedor de aplicativos (mobile ou desktop). Entrei no mercado do desenvolvimento 6 anos atrás, começando com projetos voltados para a engenharia. Hoje, atuo em qualquer mercado. Já criei CADs para desktop, trabalhei com sistemas administrativos, rastreadores GPS e outros ramos que a engenharia e o mercado comumente pedem. Caso tenha algum projeto em mente, só entrar em contato através do meu e-mail com orçamento, ideias e prazos. Tenho horários bastante flexíveis e pego todo tipo de projeto mobile.\r\n\r\nHoje sou Engenheiro sem Fronteiras e desenvolvedor freelancer.", 
                                    FontSize = 12, TextColor = Colors.PrimaryText, Margin = new Thickness(20, 0, 20, 0), LineBreakMode = LineBreakMode.WordWrap 
                                }
                                    .LeftExpand(),
                            } .Row(Row.Description) .ColSpan(2),

                            new Frame
                            {
                                Padding = 5, CornerRadius = 6, BackgroundColor = Colors.TextIcons,

                                Content = new StackLayout
                                {
                                    Spacing = 5, Orientation = StackOrientation.Horizontal,

                                    Children =
                                    {
                                        new Image { Source = Images.Email } .Center(),
                                        new Label { Text = "raelsoncraftz@gmail.com" } .LeftExpand() .CenterV(),
                                        new Button { BackgroundColor = Colors.TextIcons, ImageSource = Images.Copy, } .Size(40)
                                            .Invoke(c => c.Clicked += (s, e) =>
                                            {
                                                Clipboard.SetTextAsync("raelsoncraftz@gmail.com");
                                                toastService.ShortAlert("E-mail copiado! Cole onde precisar");
                                            }),
                                    },
                                },
                            } .Row(Row.Contact) .ColSpan(2),

                            new Button { Text = "Play Store", ImageSource = Images.PlayStore }
                                .Row(Row.Stores) .Col(Col.PlayStore)
                                .Invoke(c => c.Clicked += async (s, e) =>
                                {
                                    await Launcher.OpenAsync($"https://play.google.com/store/apps/developer?id=Raelson+Craftz");
                                }),

                            new Button { Text = "App Store", ImageSource = Images.AppStore, IsEnabled = false, }
                                .Row(Row.Stores) .Col(Col.AppStore),
                        },
                    } .Invoke(c =>
                    {
                        AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0, 0, 1, 1));
                        AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);
                    }),

                    new Button { BackgroundColor = Color.Transparent, ImageSource = Images.CloseBlack, } .Round(40)
                        .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(10, 10, 40, 40));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.None);

                            c.Clicked += async (s, e) =>
                            {
                                await Shell.Current.Navigation.PopModalAsync(true);
                            };
                        }),
                },
            };
        }
    }
}
