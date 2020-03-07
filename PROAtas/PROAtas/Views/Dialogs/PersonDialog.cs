using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using PROAtas.Views.DataTemplates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Dialogs
{
    public class PersonDialog : BaseDialog
    {
        enum Row { Header, Content, Action, Warning }

        public PersonDialog(object viewModel) => Build(viewModel);

        private void Build(object viewModel)
        {
            Content = new Grid
            {
                Children =
                {
                    // Black mask
                    new BoxView
                    {
                        GestureRecognizers = { new TapGestureRecognizer() .Invoke(l => l.Tapped += CancelDialog) }
                    } .Mask(),

                    new Grid
                    {
                        RowDefinitions = Rows.Define(
                            (0, GridLength.Auto),
                            (1, GridLength.Star)),

                        Margin = new Thickness(50, 30, 50, 30),
                        RowSpacing = 5, ColumnSpacing = 0,

                        Children =
                        {
                            // Close button
                            new ImageButton { Source = Images.Close, BackgroundColor = Color.Transparent } .Center()
                                .Row(0)
                                .Invoke(l => l.Clicked += CancelDialog),

                            // Border
                            new Frame
                            {
                                Content = new Grid
                                {
                                    RowDefinitions = Rows.Define(
                                        (Row.Header, GridLength.Auto),
                                        (Row.Content, GridLength.Star),
                                        (Row.Action, GridLength.Auto),
                                        (Row.Warning, GridLength.Auto)),

                                    Padding = 5,
                                    RowSpacing = 5, ColumnSpacing = 0,

                                    // Dialog Content
                                    Children =
                                    {
                                        new Image { Source = Images.People } .Center()
                                            .Row(Row.Header),

                                        new CollectionView { ItemTemplate = PersonTemplate.New(viewModel), SelectionMode = SelectionMode.None } .VerticalListStyle()
                                            .Assign(out CollectionView peopleCollection)
                                            .Row(Row.Content)
                                            .Bind(CollectionView.ItemsSourceProperty, nameof(MinuteViewModel.People)),

                                        new Button { ImageSource = Images.Add } .Standard() .Round() .Center()
                                            .Row(Row.Action)
                                            .Bind(nameof(MinuteViewModel.CreatePerson)),
                                    }
                                }
                            } .Standard() .Row(1),
                        }
                    } .Transparent(),
                }
            };
        }
    }
}
