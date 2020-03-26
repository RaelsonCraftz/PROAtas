using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Views.DataTemplates
{
    public class MinuteImageTemplate
    {
        public static DataTemplate New() => new DataTemplate(() =>
        {
            // Outer Grid
            var grid = new Grid
            {
                BackgroundColor = Colors.Primary,
                
                Children =
                {
                    // Outer Border
                    new Frame
                    {
                        Padding = 5, CornerRadius = 6, BackgroundColor = Colors.Accent,

                        // Frame content
                        Content = new Image { } .Fill() .Size(128)
                            .Bind(nameof(MinuteImageElement.Source)),
                    }
                }
            };

            return grid;
        });
    }
}
