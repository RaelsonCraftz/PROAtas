using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomDatePicker : DatePicker
    {
        public CustomDatePicker()
        {
            DateSelected += CustomDatePicker_DateSelected;
        }

        private void CustomDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (IsFocused)
                Command?.Execute(e.NewDate);
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomDatePicker), default(ICommand));
    }
}
