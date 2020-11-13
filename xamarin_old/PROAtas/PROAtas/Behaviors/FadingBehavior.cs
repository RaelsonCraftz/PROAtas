using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Behaviors
{
    public class FadingBehavior : Behavior<VisualElement>
    {
        private VisualElement Control;
        private double fadeTo;

        public FadingBehavior(double fadeTo = 1)
        {
            this.fadeTo = fadeTo;
        }

        #region Behavior Implementation

        private void BindingContextAttached(object sender, EventArgs args)
        {
            this.BindingContext = ((BindableObject)sender).BindingContext;
        }

        protected override void OnAttachedTo(VisualElement control)
        {
            Control = control;
            control.BindingContextChanged += BindingContextAttached;

            base.OnAttachedTo(control);
        }

        protected override void OnDetachingFrom(VisualElement control)
        {
            Control = null;
            control.BindingContextChanged -= BindingContextAttached;

            base.OnDetachingFrom(control);
        }

        #endregion


        #region Bindable Properties

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(FadingBehavior), default(bool), propertyChanged: OpenAnimation);

        #endregion

        #region Property Changed Events

        private static async void OpenAnimation(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FadingBehavior)bindable;
            await control.OpenAnimationExecution();
        }
        protected async Task OpenAnimationExecution()
        {
            if (Control != null)
            {
                ViewExtensions.CancelAnimations(Control);
                
                if (IsActive)
                {
                    Control.InputTransparent = false;
                    await Control.FadeTo(fadeTo, 250, Easing.Linear);
                }
                else
                {
                    Control.InputTransparent = true;
                    await Control.FadeTo(0, 250, Easing.Linear);
                }
            }
        }

        #endregion
    }
}
