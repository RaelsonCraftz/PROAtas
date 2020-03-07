using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.DataTemplates
{
    public class MinuteTemplate
    {
        enum Row { Header, Body }

        public static DataTemplate New() => new DataTemplate(() =>
        {
            // Outer Grid
            var grid = new Grid
            {
                BackgroundColor = Colors.TextIcons,

                Padding = 5,
                Children =
                {
                    new Frame
                    {
                        // Vertical StackLayout
                        Content = new StackLayout
                        {
                            Spacing = 5,

                            // Number of people on the meeting
                            Children =
                            {
                                // Horizontal StackLayout
                                new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,

                                    Children =
                                    {
                                        new Label { LineBreakMode = LineBreakMode.NoWrap } .BodyText() .CenterV() .Bold()
                                            .Bind(nameof(MinuteElement.PeopleQuantity), mode: BindingMode.OneTime),

                                        new Image { Source = Images.People },
                                    }
                                } .Right(),

                                // Name of the meeting
                                new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,

                                    Children =
                                    {
                                        new Image { Source = Images.Minute },

                                        new Label { LineBreakMode = LineBreakMode.NoWrap } .HeaderText() .CenterV()
                                            .Bind(nameof(MinuteElement.Name), mode: BindingMode.OneTime),
                                    }
                                } .Left(),

                                // Date of the meeting
                                new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,

                                    Children =
                                    {
                                        new Image { Source = Images.Date },

                                        new Label { LineBreakMode = LineBreakMode.NoWrap } .BodyText() .CenterV()
                                            .Bind(nameof(MinuteElement.Date), mode: BindingMode.OneTime, stringFormat: "{0:dd/MM/yyyy}"),
                                    }
                                } .Left(),
                            }
                        }
                    } .Standard(),
                }
            };

            return grid;
        });
    }
}
