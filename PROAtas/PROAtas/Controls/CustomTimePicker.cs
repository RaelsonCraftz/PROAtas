using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomTimePicker : TimePicker
    {
        public CustomTimePicker()
        {
            PropertyChanged += TimeChanged;
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomTimePicker), default(ICommand));

        private void TimeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName && IsFocused)
            {
                Command?.Execute(((CustomTimePicker)sender).Time);
            }
        }
    }
}
