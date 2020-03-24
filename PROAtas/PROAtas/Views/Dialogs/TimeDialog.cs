using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Dialogs
{
    public class TimeDialog : BaseDialog
    {
        enum Row { Header, Content }

        public TimeDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

        private void Build()
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

                        Margin = new Thickness(50, 80, 50, 80),
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
                                        (Row.Content, GridLength.Star)),

                                    // Dialog Content
                                    Children =
                                    {
                                        new Image { Source = Images.Time } .Center()
                                            .Row(Row.Header),

                                        new ScrollView
                                        {
                                            Content = new StackLayout
                                            {
                                                Spacing = 5,

                                                Children =
                                                {
                                                    new Frame { } .FramedDatePicker(Images.Date, "Data", nameof(MinuteViewModel.SaveDate), nameof(MinuteViewModel.Date)),

                                                    new Frame { } .FramedTimePicker(Images.Time, "Início", nameof(MinuteViewModel.SaveStart), nameof(MinuteViewModel.Start)),

                                                    new Frame { } .FramedTimePicker(Images.Time, "Fim", nameof(MinuteViewModel.SaveEnd), nameof(MinuteViewModel.End)),
                                                }
                                            }
                                        } .Row(Row.Content),
                                    },
                                }
                            } .Standard() .Row(1),
                        }
                    } .Transparent() .Assign(out Grid innerContent)
                }
            };

            InnerContent = innerContent;
        }
    }
}
