using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Views.Dialogs
{
    public class LoadingDialog : BaseDialog
    {
        public LoadingDialog() : base() => Build();

        private void Build()
        {
            Content = new Grid
            {
                Children = 
                {
                    // Black mask
                    new BoxView { } .Mask(),

                    // Loading animation
                    new Frame
                    {
                        BackgroundColor = Colors.Primary, CornerRadius = 6, Padding = new Thickness(50, 5, 50, 5),

                        Content = new StackLayout
                        {
                            Spacing = 5,

                            Children =
                            {
                                new ActivityIndicator { Color = Colors.TextIcons, } .Size(64) .Center()
                                    .Bind(ActivityIndicator.IsRunningProperty, nameof(LoadingDialog.IsOpen), source: this),

                                new Label { Text = "Carregando" } .BodyText() .Center(),
                            }
                        }
                    } .Center(),
                }
            };
        }
    }
}
