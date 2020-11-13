using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Behaviors
{
    public class MovingBehavior : BaseBehavior<VisualElement>
    {
        public MovingBehavior()
        {

        }

        public MovingBehavior(double translateToX = 0, double translateToY = 0)
        {
            AssociatedObject.TranslationX = translateToX;
            AssociatedObject.TranslationY = translateToY;

            this.translateToX = translateToX;
            this.translateToY = translateToY;
        }

        private double translateToX;
        private double translateToY;

        #region Bindable Properties

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(MovingBehavior), default(bool), propertyChanged: MoveAnimation);

        #endregion

        #region Property Changed Events

        private static async void MoveAnimation(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MovingBehavior)bindable;
            await control.MoveAnimationExecution();
        }
        protected async Task MoveAnimationExecution()
        {
            if (AssociatedObject != null)
            {
                if (IsActive)
                    await AssociatedObject.TranslateTo(0, 0, 500, Easing.CubicOut);
                else
                    await AssociatedObject.TranslateTo(translateToX, translateToY, 500, Easing.CubicOut);
            }
        }

        #endregion
    }
}
