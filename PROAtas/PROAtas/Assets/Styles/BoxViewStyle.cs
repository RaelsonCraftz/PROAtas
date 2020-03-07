using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class BoxViewStyle
    {
        public static TBoxView Mask<TBoxView>(this TBoxView boxView) where TBoxView : BoxView
        {
            boxView.BackgroundColor = Color.Black;
            boxView.Opacity = 0.5;

            return boxView;
        }
    }
}
