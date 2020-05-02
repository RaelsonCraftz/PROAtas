using PROAtas.Assets.Theme;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class ButtonStyle
    {
        public static TButton Standard<TButton>(this TButton button) where TButton : Button
        {
            button.BackgroundColor = Colors.Accent;
            button.TextColor = Colors.TextIcons;
            button.Padding = 5;
            button.CornerRadius = 6;

            return button;
        }

        public static TButton Success<TButton>(this TButton button) where TButton : Button
        {
            button.BackgroundColor = Colors.Success;
            button.TextColor = Colors.TextIcons;
            button.Padding = 5;
            button.CornerRadius = 6;

            return button;
        }

        public static TButton Danger<TButton>(this TButton button) where TButton : Button
        {
            button.BackgroundColor = Colors.Danger;
            button.TextColor = Colors.TextIcons;
            button.Padding = 5;
            button.CornerRadius = 6;

            return button;
        }

        public static TButton Round<TButton>(this TButton button, double size = 60, int scale = 1) where TButton : Button
        {
            size *= scale;
            button.WidthRequest = size;
            button.HeightRequest = size;

            button.CornerRadius = (int)size / 2;

            return button;
        }
    }
}
