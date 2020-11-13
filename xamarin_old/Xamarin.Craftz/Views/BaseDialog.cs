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

        public EDockTo DockTo { get; set; }

        public BaseDialog(EDockTo? dockTo = null, double translateToX = 100, double translateToY = 100)
        {
            Opacity = 0;
            InputTransparent = true;

            this.translateToY = translateToX;
            this.translateToX = translateToY;

            DockTo = dockTo ?? EDockTo.None;

            switch (DockTo)
            {
                case EDockTo.Start:
                    this.TranslationX = -this.translateToY;
                    break;
                case EDockTo.Top:
                    this.TranslationY = -this.translateToX;
                    break;
                case EDockTo.End:
                    this.TranslationX = this.translateToY;
                    break;
                case EDockTo.Bottom:
                    this.TranslationX = this.translateToX;
                    break;
                default:
                    break;
            }
        }

        private readonly double translateToX;
        private readonly double translateToY;

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
            ViewExtensions.CancelAnimations(this);

            if (IsOpen)
            {
                Opening?.Invoke();
                
                InputTransparent = false;

                _ = this?.TranslateTo(0, 0, 500, Easing.CubicOut);
                await this.FadeTo(1, 500, Easing.Linear);
            }
            else
            {
                Closing?.Invoke();

                InputTransparent = true;

                switch (DockTo)
                {
                    case EDockTo.Start:
                        _ = this?.TranslateTo(-translateToY, 0, 500, Easing.CubicOut);
                        break;
                    case EDockTo.Top:
                        _ = this?.TranslateTo(0, -translateToX, 500, Easing.CubicOut);
                        break;
                    case EDockTo.End:
                        _ = this?.TranslateTo(translateToY, 0, 500, Easing.CubicOut);
                        break;
                    case EDockTo.Bottom:
                        _ = this?.TranslateTo(0, translateToX, 500, Easing.CubicOut);
                        break;
                    default:
                        break;
                }

                await this.FadeTo(0, 500, Easing.Linear);
            }
        }

        public void CancelDialog()
        {
            Close?.Invoke();
        }
    }
}
