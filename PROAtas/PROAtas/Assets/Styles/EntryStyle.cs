using PROAtas.Assets.Theme;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class EntryStyle
    {
        public static TEntry Standard<TEntry>(this TEntry editor) where TEntry : Entry
        {
            editor.FontSize = 14;
            editor.TextColor = Colors.PrimaryText;

            return editor;
        }
    }
}
