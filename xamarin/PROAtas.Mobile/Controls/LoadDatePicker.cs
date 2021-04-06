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

        public Command LoadCommand
        {
            get { return (Command)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(LoadCommand), typeof(Command), typeof(LoadDatePicker), default(Command));

        private void LoadDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (IsFocused)
                LoadCommand?.Execute(Date);
        }
    }
}
