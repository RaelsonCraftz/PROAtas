using PROAtas.Assets.Theme;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class LabelStyle
    {
        public static TLabel HeaderText<TLabel>(this TLabel label) where TLabel : Label
        {
            label.FontAttributes = FontAttributes.Bold;
            label.FontSize = 20;
            label.TextColor = Colors.TextIcons;

            return label;
        }

        public static TLabel BodyText<TLabel>(this TLabel label) where TLabel : Label
        {
            label.FontSize = 14;
            label.TextColor = Colors.TextIcons;

            return label;
        }

        public static TLabel SecondaryText<TLabel>(this TLabel label) where TLabel : Label
        {
            label.FontSize = 12;
            label.TextColor = Colors.SecondaryText;

            return label;
        }
    }
}
