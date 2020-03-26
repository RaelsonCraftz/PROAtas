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
using static PROAtas.Behaviors.MovingBehavior;
using static PROAtas.Views.Dialogs.BaseDialog;

namespace PROAtas.Views.Pages
{
    public class MinutePage : BasePage<MinuteViewModel, Minute>
    {
        #region Service Container

        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        public MinutePage() => Build();

        enum Row { TopicList, Information, Banner }

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
                (Row.Information, GridLength.Star),
                (Row.Banner, new GridLength(50))),

                Children =
                {
                    // Topic list
                    new CollectionView
                    {
                        BackgroundColor = Colors.Primary,
                        ItemTemplate = TopicTemplate.New(), ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,

                        Footer = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,

                            Children =
                            {
                                new Button { ImageSource = Images.Add } .Standard() .Round(40) .Center()
                                    .Bind(nameof(vm.CreateTopic)),
                            }
                        },

                    } .HorizontalListStyle() .Single()
                        .Assign(out CollectionView topicCollection)
                        .Row(Row.TopicList)
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

                    // Information list beloging to the selected topic
                    new CollectionView
                    {
                        // Topic title with a button to delete the topic
                        Header = new Grid
                        {
                            Behaviors =
                            {
                                new MovingBehavior { MoveTo = EMoveTo.Top }
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
                            },
                        } .Padding(5) .SetTranslationY(-100),

                        ItemTemplate = InformationTemplate.New(vm), ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                        Behaviors =
                        {
                            new FadingBehavior { }
                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool()),
                        },

                        // Actions
                        Footer = new StackLayout 
                        { 
                            Orientation = StackOrientation.Horizontal,

                            Children =
                            {
                                new Button 
                                { 
                                    ImageSource = Images.Add, Margin = 10,

                                    Behaviors =
                                    { 
                                        new MovingBehavior { MoveTo = EMoveTo.Start }
                                            .BindBehavior(MovingBehavior.IsActiveProperty, nameof(vm.SelectedTopic), converter: new NullToBool()),
                                    },
                                } .Standard() .Round(48) .SetTranslationX(-58)
                                    .Bind(nameof(vm.CreateInformation)),
                            },
                        } .Center(),
                    } .VerticalListStyle() .Single()
                        .Row(Row.Information)
                        .Bind(CollectionView.ItemsSourceProperty, nameof(vm.Information)),

                    new InformationDialog(EDockTo.End)
                        .RowSpan(2)
                        .Bind(InformationDialog.IsOpenProperty, nameof(vm.SelectedInformation), converter: new NullToBool())
                        .Invoke(l => l.Close += () =>
                        {
                            if (!vm.IsSavingInformation)
                                vm.SelectedInformation = null;
                            else
                                toastService.ShortAlert("Aguarde a operação de salvar!");
                        }),

                    new PersonDialog(vm, EDockTo.End)
                        .Assign(out PersonDialog personDialog)
                        .RowSpan(2)
                        .Invoke(l => l.Close += () =>
                        {
                            if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                                personDialog.IsOpen = false;
                            else
                                toastService.ShortAlert("Aguarde a operação de salvar!");
                        }),

                    new TimeDialog(EDockTo.End) { }
                        .Assign(out TimeDialog timeDialog)
                        .RowSpan(2)
                        .Invoke(l => l.Close += () =>
                        {
                            timeDialog.IsOpen = false;
                        }),

                    new MinuteNameDialog(EDockTo.End) { }
                        .Assign(out MinuteNameDialog minuteNameDialog)
                        .RowSpan(2)
                        .Invoke(l => l.Close += () =>
                        {
                            if (!vm.IsSavingTopic && !vm.IsSavingInformation && !vm.IsSavingMinuteName && !vm.People.Any(p => p.IsSaving))
                                minuteNameDialog.IsOpen = false;
                            else
                                toastService.ShortAlert("Aguarde a operação de salvar!");
                        }),

                    new AdMobView { AdUnitId = Constants.AdMinute }
                        .Row(2),
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