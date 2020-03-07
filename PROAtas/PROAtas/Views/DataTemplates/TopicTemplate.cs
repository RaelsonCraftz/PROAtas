using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Converters;
using PROAtas.ViewModel;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.DataTemplates
{
    public class TopicTemplate
    {
        public static DataTemplate New() => new DataTemplate(() =>
        {
            // Outer Grid
            var grid = new Grid()
            {
                BackgroundColor = Colors.Primary,
                Padding = 5,

                Children =
                {
                    new Frame
                    {
                        Padding = 5, CornerRadius = 6, BackgroundColor = Colors.Accent,

                        // Inner Grid
                        Content = new StackLayout
                        {
                            Spacing = 5,

                            Children =
                            {
                                new Frame 
                                {
                                    Padding = 5, CornerRadius = 6, BackgroundColor = Colors.DarkPrimary,

                                    Content = new Label { } .HeaderText() .Center()
                                        .Bind(Label.TextColorProperty, nameof(TopicElement.IsSelected), converter: new BoolToColor(Colors.Accent, Colors.TextIcons))
                                        .Bind(nameof(TopicElement.Order)),
                                } .Bind(Frame.BackgroundColorProperty, nameof(TopicElement.IsSelected), converter: new BoolToColor(Colors.LightPrimary, Colors.DarkPrimary)),

                                new Label { LineBreakMode = LineBreakMode.NoWrap } .BodyText() .Center()
                                    .Bind(nameof(TopicElement.Text), converter: new StringToTrim(12)),
                            }
                        }
                    },
                },
            } .SetWidth(100);

            return grid;
        });
    }
}
