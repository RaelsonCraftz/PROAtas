using PROAtas.Assets.Theme;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class AdMobView : View
    {
        public AdMobView()
        {
            BackgroundColor = Colors.Primary;
        }

        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
                   nameof(AdUnitId),
                   typeof(string),
                   typeof(AdMobView),
                   string.Empty);

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
    }
}
