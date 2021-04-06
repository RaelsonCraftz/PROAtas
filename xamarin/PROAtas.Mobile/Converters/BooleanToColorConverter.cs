using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Converters
{
    public class BooleanToColorConverter : IValueConverter, IMarkupExtension
    {
        public Color? TrueColor { get; set; }
        public Color? FalseColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return TrueColor ?? (Color)Application.Current.Resources["Accent"];
            else
                return FalseColor ?? (Color)Application.Current.Resources["DarkAccent"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
