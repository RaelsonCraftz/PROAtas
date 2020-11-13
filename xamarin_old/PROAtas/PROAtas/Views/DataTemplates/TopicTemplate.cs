using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Controls;
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
        public static DataTemplate New(object viewModel) => new DataTemplate(() =>
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
                                    Content = new Label { FontSize = 12 } .Center()
                                        .Bind(Label.TextColorProperty, nameof(TopicElement.IsSelected), converter: new BoolToColor(Colors.Accent, Colors.TextIcons))
                                        .Bind(Label.TextProperty, nameof(TopicElement.Order)),
                                } .Height(30)
                                    .Bind(Frame.BackgroundColorProperty, nameof(TopicElement.IsSelected), converter: new BoolToColor(Colors.LightPrimary, Colors.DarkPrimary)),

                                new Label { LineBreakMode = LineBreakMode.TailTruncation } .BodyText() .Center()
                                    .Bind(nameof(TopicElement.Text)),
                            }
                        }
                    },
                },
            } .SetWidth(100);

            return grid;
        });
    }
}
