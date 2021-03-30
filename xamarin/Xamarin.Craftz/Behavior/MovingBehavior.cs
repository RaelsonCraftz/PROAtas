using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Craftz.Behavior
{
    public class MovingBehavior : BaseBehavior<VisualElement>
    {
        public enum EMoveTo { Start, Top, End, Bottom, }

        public MovingBehavior()
        {

        }

        public MovingBehavior(double translateToX = 0, double translateToY = 0)
        {
            this.translateToX = translateToX;
            this.translateToY = translateToY;
        }

        private double translateToX;
        private double translateToY;

        public EMoveTo MoveTo { get; set; }

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
            if (AssociatedControl != null)
            {
                if (IsActive)
                    await AssociatedControl.TranslateTo(0, 0, 500, Easing.CubicOut);
                else
                    switch (MoveTo)
                    {
                        case EMoveTo.Start:
                            await AssociatedControl.TranslateTo(translateToX == 0 ? -AssociatedControl.Width : -translateToX, 0, 500, Easing.CubicOut);
                            break;
                        case EMoveTo.Top:
                            await AssociatedControl.TranslateTo(0, translateToY == 0 ? -AssociatedControl.Height : -translateToY, 500, Easing.CubicOut);
                            break;
                        case EMoveTo.End:
                            await AssociatedControl.TranslateTo(translateToX == 0 ? AssociatedControl.Width : translateToX, 0, 500, Easing.CubicOut);
                            break;
                        case EMoveTo.Bottom:
                            await AssociatedControl.TranslateTo(0, translateToY == 0 ? AssociatedControl.Height : translateToY, 500, Easing.CubicOut);
                            break;
                    }
            }
        }

        #endregion
    }
}
