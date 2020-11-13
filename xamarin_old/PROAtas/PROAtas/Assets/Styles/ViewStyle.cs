using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class ViewStyle
    {
        public static TView SetHeight<TView>(this TView view, double height = 60, int scale = 1) where TView : View
        {
            height *= scale;
            view.HeightRequest = height;

            return view;
        }

        public static TView SetWidth<TView>(this TView view, double width = 60, int scale = 1) where TView : View
        {
            width *= scale;
            view.WidthRequest = width;

            return view;
        }

        public static TView SetTranslationY<TView>(this TView view, double translationY, int scale = 1) where TView : View
        {
            translationY *= scale;
            view.TranslationY = translationY;

            return view;
        }

        public static TView SetTranslationX<TView>(this TView view, double translationX, int scale = 1) where TView : View
        {
            translationX *= scale;
            view.TranslationX = translationX;

            return view;
        }
    }
}
