using PROAtas.Assets.Theme;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class EditorStyle
    {
        public static TEditor Standard<TEditor>(this TEditor editor) where TEditor : Editor
        {
            editor.FontSize = 14;
            editor.TextColor = Colors.PrimaryText;

            return editor;
        }
    }
}
