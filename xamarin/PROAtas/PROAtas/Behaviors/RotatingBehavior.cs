using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Behaviors
{
    public class RotatingBehavior : BaseBehavior<VisualElement>
    {
        private readonly double rotateFrom;
        private readonly double rotateTo;

        public RotatingBehavior()
        {

        }

        public RotatingBehavior(double rotateFrom = 0, double rotateTo = 0)
        {
            this.rotateFrom = rotateFrom;
            this.rotateTo = rotateTo;
        }

        #region Bindable Properties

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(RotatingBehavior), default(bool), propertyChanged: MoveAnimation);

        #endregion

        #region Property Changed Event

        private static async void MoveAnimation(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RotatingBehavior)bindable;
            await control.MoveAnimationExecution();
        }
        protected async Task MoveAnimationExecution()
        {
            if (AssociatedObject != null)
            {
                if (IsActive)
                    await AssociatedObject.RotateTo(rotateFrom, 500, Easing.CubicOut);
                else
                    await AssociatedObject.RotateTo(rotateTo, 500, Easing.CubicOut);
            }
        }

        #endregion
    }
}
