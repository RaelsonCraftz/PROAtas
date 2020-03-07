using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomPicker : Picker
    {
        public CustomPicker()
        {
            SelectedIndexChanged += CustomPicker_SelectedIndexChanged;
        }

        private void CustomPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Command?.Execute(SelectedItem);
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomPicker), default(ICommand));
    }
}
