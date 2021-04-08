using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Mobile.Controls
{
    public class StepSlider : Slider
    {

        public StepSlider()
        {
            ValueChanged += StepSlider_ValueChanged;
        }

        ~StepSlider()
        {
            ValueChanged -= StepSlider_ValueChanged;
        }

        private readonly object _valueLock = new object();

        public ICommand NewValueCommand
        {
            get { return (ICommand)GetValue(NewValueCommandProperty); }
            set { SetValue(NewValueCommandProperty, value); }
        }
        public static readonly BindableProperty NewValueCommandProperty = BindableProperty.Create(nameof(NewValueCommand), typeof(ICommand), typeof(StepSlider), default(ICommand));

        #region Events

        private void StepSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var oldValue = Math.Round(e.OldValue, 0);
            var newValue = Math.Round(e.NewValue, 0);

            if (oldValue == newValue)
            {
                Debug.WriteLine($"[{AppInfo.Name} valor do StepSlider manteve em: {oldValue}]");
                lock (_valueLock)
                {
                    Value = oldValue;
                    return;
                }
            }

            Debug.WriteLine($"[{AppInfo.Name} valor do StepSlider mudou: {newValue}]");
            lock (_valueLock)
            {
                Value = newValue;
                NewValueCommand?.Execute(newValue);
            }
        }

        #endregion

    }
}
