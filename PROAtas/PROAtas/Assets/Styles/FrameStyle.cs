using CSharpForMarkup;
using PROAtas.Assets.Theme;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class FrameStyle
    {
        public static TFrame Standard<TFrame>(this TFrame frame) where TFrame : Frame
        {
            frame.BackgroundColor = Colors.Primary;
            frame.Padding = new Thickness(5);
            frame.CornerRadius = 6;

            return frame;
        }
    }
}
