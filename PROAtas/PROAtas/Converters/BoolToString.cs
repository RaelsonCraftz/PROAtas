using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Converters
{
    public class BoolToString : IValueConverter
    {
        public string TrueString;
        public string FalseString;

        public BoolToString(string trueString, string falseString)
        {
            TrueString = trueString;
            FalseString = falseString;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = (bool)value;

            if (boolean) return TrueString;
            else return FalseString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
