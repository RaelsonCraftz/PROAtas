using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Behaviors
{
    public class FadingBehavior : BaseBehavior<VisualElement>
    {
        public FadingBehavior()
        {

        }

        public FadingBehavior(double fadeTo = 1)
        {
            this.fadeTo = fadeTo;
        }

        private double fadeTo;

        #region Bindable Properties

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(FadingBehavior), default(bool), propertyChanged: FadeAnimation);

        #endregion

        #region Property Changed Events

        private static async void FadeAnimation(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FadingBehavior)bindable;
            await control.FadeAnimationExecution();
        }
        protected async Task FadeAnimationExecution()
        {
            if (AssociatedObject != null)
            {
                if (IsActive)
                {
                    AssociatedObject.InputTransparent = false;
                    await AssociatedObject.FadeTo(fadeTo, 250, Easing.Linear);
                }
                else
                {
                    AssociatedObject.InputTransparent = true;
                    await AssociatedObject.FadeTo(0, 250, Easing.Linear);
                }
            }
        }

        #endregion
    }
}
