using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Behaviors;
using PROAtas.Controls;
using PROAtas.Converters;
using PROAtas.Core;
using PROAtas.Model;
using PROAtas.Services;
using PROAtas.ViewModel;
using PROAtas.Views.DataTemplates;
using PROAtas.Views.Dialogs;
using Xamarin.Forms;
using static Craftz.Views.BaseDialog;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;
using static PROAtas.Behaviors.MovingBehavior;

namespace PROAtas.Views.Pages
{
    public class MinutePage : BasePage<MinuteViewModel, Minute>
    {
        #region Service Container

        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        public MinutePage() => Build();

        enum Row { Minute, Banner }

        enum Col { TopicList, Information }

        public CustomEntry topicEntry;
        private void Build()
        {
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

            Shell.SetBackButtonBehavior(this, new BackButtonBehavior
            {
                Command = new Command(async () =>
                {
                    if (ViewModel.IsSaving)
                        toastService.ShortAlert("Aguarde a operação de salvar!");
                    else
                        await Shell.Current.Navigation.PopAsync(true);
                }),
            });
            
            Title = "Ata";

            var app = App.Current;
            var vm = ViewModel = app.minuteViewModel;

            Content = new AbsoluteLayout
            {
                Children =
                {
                    // Page Content
                    new Grid
                    {
                        RowDefinitions = Rows.Define(
                            (Row.Minute, Star),
                            (Row.Banner, 50)),

                        ColumnDefinitions = Columns.Define(
                            (Col.TopicList, 70),
                            (Col.Information, Star)),

                        Children =
                        {
                            // Topic Collection
                            new CollectionView
                            {
                                BackgroundColor = Colors.Primary,

                                GestureRecognizers =
                                {
                                    new TapGestureRecognizer { } .Invoke(c => c.Tapped += (s, e) =>
                                    {
                                        topicEntry.Unfocus();
                                    }),
                                },

                                // BODY - List of topics
                                ItemTemplate = TopicTemplate.New(vm),

                                // FOOTER - Action
                                Footer = new ContentView
                                {
                                    Padding = 5,

                                    Content = new Button { ImageSource = Images.Add } .Standard() .Round(40) .Center()
                                        .Bind(nameof(vm.CreateTopic)),
                                },
                            } .VerticalListStyle() .SingleSelection()
                                .Row(Row.Minute) .Col(Col.TopicList)
                                .Bind(CollectionView.ItemsSourceProperty, nameof(vm.Topics))
                                .Bind(CollectionView.SelectionChangedCommandProperty, nameof(vm.SelectTopic), source: vm)
                                .Invoke(c => 
                                {
                                    c.Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: c); 
                                }),

                            // Information Collection
                            new CollectionView
                            {
                                // BODY - List of information
                                ItemTemplate = InformationTemplate.New(vm),
                                Behaviors =
                                {
                                    new FadingBehavior { }
                                        .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool()),
                                },

                                // HEADER - Topic title with a button to delete the topic
                                Header = new ContentView
                                {
                                    Padding = 5,

                                    Content = new Frame
                                    {
                                        Padding = 5, CornerRadius = 6, BackgroundColor = Colors.Accent,

                                        Behaviors =
                                        {
                                            new MovingBehavior { MoveTo = EMoveTo.Top }
                                                .BindBehavior(MovingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool())
                                        },

                                        Content = new StackLayout
                                        {
                                            Spacing = 10, Orientation = StackOrientation.Horizontal,

                                            Children =
                                            {
                                                new Frame { } .FramedCustomEntry(out topicEntry, Images.TextBlack) .FillExpandH()
                                                    .Invoke(c =>
                                                    {
                                                        topicEntry.Placeholder = "Nome do tópico";
                                                        topicEntry.Bind(CustomEntry.TextProperty, $"{nameof(vm.SelectedTopic)}.{nameof(vm.SelectedTopic.Text)}");
                                                        topicEntry.Bind(CustomEntry.SaveCommandProperty, nameof(vm.SaveTopicTitle));
                                                        topicEntry.Bind(CustomEntry.IsSavingProperty, nameof(vm.IsSavingTopic), BindingMode.OneWayToSource);
                                                        topicEntry.Bind(CustomEntry.IsSavingEnabledProperty, nameof(vm.IsSavingEnabled));
                                                    }),

                                                new Button { ImageSource = Images.Delete } .Standard() .Danger() .Round(40) .Center()
                                                    .Bind(nameof(vm.DeleteTopic)),
                                            },
                                        }
                                    } .Padding(5) .SetTranslationY(-100),
                                },

                                // FOOTER - Actions
                                Footer = new ContentView
                                {
                                    Padding = 5,

                                    Content = new Button
                                    {
                                        ImageSource = Images.Add,

                                        Behaviors =
                                        {
                                            new FadingBehavior { }
                                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool()),
                                            new MovingBehavior { MoveTo = EMoveTo.End }
                                                .BindBehavior(MovingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool()),
                                        },
                                    } .Standard() .Round(40) .SetTranslationX(50) .Right()
                                        .Bind(nameof(vm.CreateInformation)),
                                }

                            } .VerticalListStyle() .SingleSelection() .FillExpandV()
                                .Row(Row.Minute) .Col(Col.Information)
                                .Bind(CollectionView.ItemsSourceProperty, nameof(vm.Information)),

                            new AdMobView { AdUnitId = Constants.AdMinute }
                                .Row(Row.Banner) .ColSpan(2),
                        }
                    } .Standard(),

                    // Black mask
                    new BoxView
                    {
                        BackgroundColor = Color.Black, Opacity = 0, InputTransparent = true,

                        Behaviors =
                        {
                            new FadingBehavior(0.5)
                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.IsLocked))
                        },

                        GestureRecognizers = 
                        { 
                            new TapGestureRecognizer() .Invoke(l => 
                            l.Tapped += (s, e) =>
                            {
                                if (!vm.IsSaving)
                                    vm.ClearDialogs();
                                else
                                    toastService.ShortAlert("Aguarde a operação de salvar!");
                            })
                        },
                    } .Invoke(c =>
                    {
                        AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0, 0, 1, 1));
                        AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.SizeProportional);
                    }),

                    // Dialogs
                    new InformationDialog(EDockTo.Start)
                        .Bind(InformationDialog.IsOpenProperty, nameof(vm.SelectedInformation), converter: new NullToBool())
                        .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 0.8, 0.7));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);

                            c.Close += () =>
                            {
                                if (!vm.IsSaving)
                                    vm.SelectedInformation = null;
                                else
                                    toastService.ShortAlert("Aguarde a operação de salvar!");
                            };
                        }),

                    new PersonDialog(vm, EDockTo.Start)
                        .Bind(InformationDialog.IsOpenProperty, nameof(vm.IsPeopleOpen))
                        .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 0.8, 0.7));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);

                            c.Close += () =>
                            {
                                if (!vm.IsSaving)
                                    vm.IsPeopleOpen = false;
                                else
                                    toastService.ShortAlert("Aguarde a operação de salvar!");
                            };
                        }),

                    new TimeDialog(EDockTo.Start)
                        .Bind(InformationDialog.IsOpenProperty, nameof(vm.IsTimeOpen))
                        .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.5, 0.8, 0.7));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.All);

                            c.Close += () =>
                            {
                                vm.IsTimeOpen = false;
                            };
                        }),

                    new MinuteNameDialog(EDockTo.Start)
                        .Bind(InformationDialog.IsOpenProperty, nameof(vm.IsMinuteNameOpen))
                        .Invoke(c =>
                        {
                            AbsoluteLayout.SetLayoutBounds(c, new Rectangle(0.5, 0.2, 0.8, 100));
                            AbsoluteLayout.SetLayoutFlags(c, AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);

                            c.Close += () =>
                            {
                                if (!vm.IsSaving)
                                    vm.IsMinuteNameOpen = false;
                                else
                                    toastService.ShortAlert("Aguarde a operação de salvar!");
                            };
                        }),
                }
            };

            ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = Images.Minute,
            }.Invoke(c => c.Clicked += (s, e) =>
            {
                if (!vm.IsSaving)
                {
                    if (vm.IsMinuteNameOpen)
                        vm.IsMinuteNameOpen = false;
                    else
                    {
                        vm.ClearDialogs();
                        vm.IsMinuteNameOpen = true;
                    }
                }
                else
                    toastService.ShortAlert("Aguarde a operação de salvar!");
            }));

            ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = Images.People,
            }.Invoke(c => c.Clicked += (s, e) =>
            {
                if (!vm.IsSaving)
                {
                    if (vm.IsPeopleOpen)
                        vm.IsPeopleOpen = false;
                    else
                    {
                        vm.ClearDialogs();
                        vm.IsPeopleOpen = true;
                    }
                }
                else
                    toastService.ShortAlert("Aguarde a operação de salvar!");
            }));

            ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = Images.Time,
            }.Invoke(c => c.Clicked += (s, e) =>
            {
                if (!vm.IsSaving)
                {
                    if (vm.IsTimeOpen)
                        vm.IsTimeOpen = false;
                    else
                    {
                        vm.ClearDialogs();
                        vm.IsTimeOpen = true;
                    }
                }
                else
                    toastService.ShortAlert("Aguarde a operação de salvar!");
            }));
        }

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.IsSaving)
            {
                toastService.ShortAlert("Aguarde a operação de salvar!");
                return true;
            }

            if (ViewModel.IsMinuteNameOpen || ViewModel.IsPeopleOpen || ViewModel.IsTimeOpen || ViewModel.SelectedInformation != null)
            { 
                ViewModel.ClearDialogs();
                return true;
            }

            return false;
        }
    }
}