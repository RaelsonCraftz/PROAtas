using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Views.Dialogs
{
    public class BaseDialog : ContentView
    {
        public delegate void OnOpening();
        public event OnOpening Opening;

        public delegate void OnClosing();
        public event OnClosing Closing;

        public delegate void OnClose();
        public event OnClose Close;

        public BaseDialog()
        {
            IsVisible = false;

            Animate();
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(BaseDialog), false, propertyChanged: AnimateControl);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(BaseDialog), default(string));

        public ICommand Confirm
        {
            get { return (ICommand)GetValue(ConfirmProperty); }
            set { SetValue(ConfirmProperty, value); }
        }
        public static readonly BindableProperty ConfirmProperty = BindableProperty.Create(nameof(Confirm), typeof(ICommand), typeof(BaseDialog), default(ICommand));

        public static void AnimateControl(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (BaseDialog)bindable;
            control.Animate();
        }

        private async void Animate()
        {
            if (IsOpen)
            {
                Opening?.Invoke();
                IsVisible = true;
                await this.FadeTo(1, 250, Easing.Linear);
            }
            else
            {
                Closing?.Invoke();
                await this.FadeTo(0, 250, Easing.Linear);
                IsVisible = false;
            }
        }

        protected void CancelDialog(object sender, EventArgs e)
        {
            Close?.Invoke();
        }
    }
}
