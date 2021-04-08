using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Craftz.Behavior
{
    public class FadingBehavior : BaseBehavior<VisualElement>
    {
        public FadingBehavior()
        {
            this.fadeTo = 1;
        }

        public FadingBehavior(double fadeTo)
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
                    await AssociatedObject.FadeTo(fadeTo, 250, Easing.Linear);
                else
                    await AssociatedObject.FadeTo(0, 250, Easing.Linear);
            }
        }

        #endregion
    }
}
