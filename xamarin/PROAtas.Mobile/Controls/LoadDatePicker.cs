using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Mobile.Controls
{
    public class LoadDatePicker : DatePicker
    {
        public LoadDatePicker()
        {
            DateSelected += LoadDatePicker_DateSelected;
        }

        ~LoadDatePicker()
        {
            DateSelected -= LoadDatePicker_DateSelected;
        }

        public ICommand LoadCommand
        {
            get { return (Command)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(LoadCommand), typeof(ICommand), typeof(LoadDatePicker), default(ICommand));

        private void LoadDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (IsFocused)
                LoadCommand?.Execute(Date);
        }
    }
}
