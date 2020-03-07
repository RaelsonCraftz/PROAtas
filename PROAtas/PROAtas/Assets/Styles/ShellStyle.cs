using CSharpForMarkup;
using PROAtas.Assets.Theme;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class ShellStyle
    {
        public static TShell Standard<TShell>(this TShell shell) where TShell : Shell
        {
            var style = new Style<Element>(
                (Shell.BackgroundColorProperty, Colors.Primary));

            var style2 = new Style<TabBar>() .BasedOn(style);

            shell.Style = style2;
            shell.FlyoutBackgroundColor = Colors.Primary;

            return shell;
        }
    }
}
