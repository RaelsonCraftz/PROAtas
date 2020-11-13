using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Converters
{
    public class BoolToColor : IValueConverter
    {
        public Color TrueColor;
        public Color FalseColor;

        public BoolToColor(Color trueColor, Color falseColor)
        {
            TrueColor = trueColor;
            FalseColor = falseColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = (bool)value;

            if (boolean) return TrueColor;
            else return FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
