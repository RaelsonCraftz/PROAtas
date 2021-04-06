using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Mobile.Controls
{
    public class AdMobView : View
    {
        public AdMobView()
        {
            BackgroundColor = (Color)Application.Current.Resources["Primary"];
        }

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(nameof(AdUnitId), typeof(string), typeof(AdMobView), string.Empty);
    }
}
