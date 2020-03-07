using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Behaviors
{
    public class FadingBehavior : Behavior<VisualElement>
    {
        private VisualElement Control;

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
                if (IsActive)
                {
                    Control.IsVisible = true;
                    await Control.FadeTo(1, 250, Easing.Linear);
                }
                else
                {
                    await Control.FadeTo(0, 250, Easing.Linear);
                    Control.IsVisible = false;
                }
            }
        }

        #endregion
    }
}
