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
using System.Linq;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Pages
{
    public class MinutePage : BasePage<MinuteViewModel, Minute>
    {
        #region Service Container

        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        public MinutePage() => Build();

        enum Row { TopicList, TopicTitle, Information, Banner }

        private void Build()
        {
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);

            Title = "Ata";

            var app = App.Current;
            var vm = ViewModel = app.minuteViewModel;

            Content = new Grid
            {
                RowDefinitions = Rows.Define(
                (Row.TopicList, 90),
                (Row.TopicTitle, GridLength.Auto),
                (Row.Information, GridLength.Star),
                (Row.Banner, new GridLength(50))),

                Children =
                {
                    // Topic title with a button to delete the topic
                    new Grid
                    {
                        Behaviors =
                        {
                            new MovingBehavior { DockSide = EPanelOrientation.Top }
                                .BindBehavior(MovingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool())
                        },

                        Children =
                        {
                            new Frame
                            {
                                Padding = 5, CornerRadius = 6, BackgroundColor = Colors.Accent,

                                Content = new Grid
                                {
                                    ColumnDefinitions = Columns.Define(
                                        (0, GridLength.Star),
                                        (1, GridLength.Auto)),

                                    RowSpacing = 0, ColumnSpacing = 10,

                                    Children =
                                    {
                                        new Frame { } .FramedCustomEntry(Images.TextBlack, "Nome do tópico", nameof(vm.SaveTopicTitle), $"{nameof(vm.SelectedTopic)}.{nameof(vm.SelectedTopic.Text)}", isSavingPath: nameof(vm.IsSavingTopic))
                                            .Col(0),

                                        new Button { ImageSource = Images.Delete } .Standard() .Danger() .Round(40) .Center()
                                            .Col(1)
                                            .Bind(nameof(vm.DeleteTopic)),
                                    },
                                }
                            },
                        }
                    } .Padding(5) .SetTranslationY(-100)
                        .Row(Row.TopicTitle),
                    
                    // Topic list with a button to add new topics on the left
                    new Grid
                    {
                        ColumnDefinitions = Columns.Define(
                            (0, GridLength.Auto),
                            (1, GridLength.Star)),

                        RowSpacing = 0, ColumnSpacing = 10,
                        Padding = 5, BackgroundColor = Colors.Primary,

                        Children =
                        {
                            new Button { ImageSource = Images.Add } .Standard() .Round(40) .Center()
                                .Col(0)
                                .Bind(nameof(vm.CreateTopic)),

                            new CollectionView { ItemTemplate = TopicTemplate.New(), ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems } .HorizontalListStyle() .Single()
                                .Assign(out CollectionView topicCollection)
                                .Col(1)
                                .Bind(CollectionView.ItemsSourceProperty, nameof(vm.Topics))
                                .Invoke(l => l.SelectionChanged += (sender, e) =>
                                {
                                    if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                                        vm.SelectTopic?.Execute(e.CurrentSelection?.FirstOrDefault());
                                    else if (e.CurrentSelection?.FirstOrDefault() != vm.SelectedTopic)
                                    {
                                        topicCollection.SelectedItem = vm.SelectedTopic;
                                        toastService.ShortAlert("Aguarde a operação de salvar!");
                                    }
                                }),
                        }
                    } .Row(Row.TopicList),

                    // Information list beloging to the selected topic
                    new Grid
                    {
                        IsVisible = false, Opacity = 0,

                        Behaviors =
                        {
                            new FadingBehavior {  }
                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool())
                        },

                        Children =
                        {
                            new CollectionView { ItemTemplate = InformationTemplate.New(vm), ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems } .VerticalListStyle() .Single()
                                .Bind(CollectionView.ItemsSourceProperty, nameof(vm.Information)),

                            new Button { ImageSource = Images.Add, Margin = 10 } .Standard() .Round() .Bottom() .Right()
                                .Bind(nameof(vm.CreateInformation)),
                        }
                    } .Standard()
                        .Row(Row.Information),

                    new InformationDialog()
                        .RowSpan(3)
                        .Bind(InformationDialog.IsOpenProperty, nameof(vm.SelectedInformation), converter: new NullToBool())
                        .Invoke(l => l.Close += () =>
                        {
                            if (!vm.IsSavingInformation)
                                vm.SelectedInformation = null;
                            else
                                toastService.ShortAlert("Aguarde a operação de salvar!");
                        }),

                    new PersonDialog(vm)
                        .Assign(out PersonDialog personDialog)
                        .RowSpan(3)
                        .Invoke(l => l.Close += () =>
                        {
                            if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                                personDialog.IsOpen = false;
                            else
                                toastService.ShortAlert("Aguarde a operação de salvar!");
                        }),

                    new TimeDialog { }
                        .Assign(out TimeDialog timeDialog)
                        .RowSpan(3)
                        .Invoke(l => l.Close += () =>
                        {
                            timeDialog.IsOpen = false;
                        }),

                    new MinuteNameDialog { }
                        .Assign(out MinuteNameDialog minuteNameDialog)
                        .RowSpan(3)
                        .Invoke(l => l.Close += () =>
                        {
                            if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                                minuteNameDialog.IsOpen = false;
                            else
                                toastService.ShortAlert("Aguarde a operação de salvar!");
                        }),

                    new AdMobView { AdUnitId = Constants.AdMinute }
                        .Row(3),
                }
            }.Standard();

            ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = Images.Minute,
            }.Invoke(l => l.Clicked += (s, e) =>
            {
                if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                {
                    vm.SelectedTopic = null;
                    vm.SelectedInformation = null;
                    topicCollection.SelectedItem = null;
                    vm.ClearTopicSelection();
                    timeDialog.IsOpen = false;
                    personDialog.IsOpen = false;

                    minuteNameDialog.IsOpen = true;
                }
                else
                    toastService.ShortAlert("Aguarde a operação de salvar!");
            }));

            ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = Images.People,
            }.Invoke(l => l.Clicked += (s, e) =>
            {
                if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                {
                    vm.SelectedTopic = null;
                    vm.SelectedInformation = null;
                    topicCollection.SelectedItem = null;
                    vm.ClearTopicSelection();
                    timeDialog.IsOpen = false;
                    minuteNameDialog.IsOpen = false;

                    personDialog.IsOpen = true;
                }
                else
                    toastService.ShortAlert("Aguarde a operação de salvar!");
            }));

            ToolbarItems.Add(new ToolbarItem
            {
                IconImageSource = Images.Time,
            }.Invoke(l => l.Clicked += (s, e) =>
            {
                if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                {
                    vm.SelectedTopic = null;
                    vm.SelectedInformation = null;
                    topicCollection.SelectedItem = null;
                    vm.ClearTopicSelection();
                    personDialog.IsOpen = false;
                    minuteNameDialog.IsOpen = false;

                    timeDialog.IsOpen = true;
                }
                else
                    toastService.ShortAlert("Aguarde a operação de salvar!");
            }));
        }
    }
}