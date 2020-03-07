using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Converters
{
    public class StringToTrim : IValueConverter
    {
        public int StringSize;

        public StringToTrim(int stringSize)
        {
            this.StringSize = stringSize;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;

            if (str.Length > StringSize) return str.Remove(StringSize - 3).Trim() + "...";
            else return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
