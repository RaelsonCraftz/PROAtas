using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class StackStyle
    {
        public static TStack Transparent<TStack>(this TStack stackLayout) where TStack : StackLayout
        {
            stackLayout.CascadeInputTransparent = false;
            stackLayout.InputTransparent = true;

            return stackLayout;
        }
    }
}
