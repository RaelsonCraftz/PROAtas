using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Mobile.Controls
{
    public class LoadPicker : Picker
    {
        public LoadPicker()
        {
            SelectedIndexChanged += LoadPicker_SelectedIndexChanged;
        }

        private void LoadPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Command?.Execute(SelectedItem);
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LoadPicker), default(Command));
    }
}
