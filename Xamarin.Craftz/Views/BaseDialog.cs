using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Craftz.Views
{
    public class BaseDialog : ContentView
    {
        public enum EDockTo { Start, Top, End, Bottom, None, }

        public delegate void OnOpening();
        public event OnOpening Opening;

        public delegate void OnClosing();
        public event OnClosing Closing;

        public delegate void OnClose();
        public event OnClose Close;

        public View InnerContent
        {
            get { return _innerContent; }
            set
            {
                _innerContent = value;

                switch (DockTo)
                {
                    case EDockTo.Start:
                        _innerContent.TranslationX = -DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
                        break;
                    case EDockTo.Top:
                        _innerContent.TranslationY = -DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
                        break;
                    case EDockTo.End:
                        _innerContent.TranslationX = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
                        break;
                    case EDockTo.Bottom:
                        _innerContent.TranslationX = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
                        break;
                    default:
                        break;
                }
            }
        }
        private View _innerContent;

        public EDockTo DockTo { get; set; }

        public BaseDialog()
        {
            Opacity = 0;
            InputTransparent = true;
        }

        public BaseDialog(EDockTo? dockTo)
        {
            //IsVisible = false;
            Opacity = 0;
            InputTransparent = true;
            DockTo = dockTo ?? EDockTo.None;
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

                InputTransparent = false;

                _ = InnerContent?.TranslateTo(0, 0, 500, Easing.CubicOut);
                await this.FadeTo(1, 500, Easing.Linear);
            }
            else
            {
                Closing?.Invoke();

                switch (DockTo)
                {
                    case EDockTo.Start:
                        _ = InnerContent?.TranslateTo(-Width, 0, 500, Easing.CubicOut);
                        break;
                    case EDockTo.Top:
                        _ = InnerContent?.TranslateTo(0, -Height, 500, Easing.CubicOut);
                        break;
                    case EDockTo.End:
                        _ = InnerContent?.TranslateTo(Width, 0, 500, Easing.CubicOut);
                        break;
                    case EDockTo.Bottom:
                        _ = InnerContent?.TranslateTo(0, Height, 500, Easing.CubicOut);
                        break;
                    default:
                        break;
                }

                await this.FadeTo(0, 500, Easing.Linear);

                InputTransparent = true;
            }
        }

        protected void CancelDialog(object sender, EventArgs e)
        {
            Close?.Invoke();
        }
    }
}
