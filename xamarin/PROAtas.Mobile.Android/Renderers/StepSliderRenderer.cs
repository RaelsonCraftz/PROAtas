using Android.Content;
using Android.Views;
using PROAtas.Droid.Renderers;
using PROAtas.Mobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(StepSlider), typeof(StepSliderRenderer))]
namespace PROAtas.Droid.Renderers
{
    public class StepSliderRenderer : SliderRenderer
    {
        public StepSliderRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
        }

        //public override bool DispatchTouchEvent(MotionEvent e)
        //{
        //    switch (e.Action)
        //    {
        //        case MotionEventActions.Down:
        //            (_navigationPage ??= GetNavigationPage(Parent)).RequestDisallowInterceptTouchEvent(true);
        //            break;
        //        case MotionEventActions.Move:
        //            //This is the core of the problem!!!
        //            _navigationPage.RequestDisallowInterceptTouchEvent(true);
        //            break;
        //        case MotionEventActions.Up:
        //            break;
        //        default:
        //            break;
        //    }
        //    return base.DispatchTouchEvent(e);
        //}

        //private IViewParent _navigationPage;
        //private IViewParent GetNavigationPage(IViewParent parent)
        //{
        //    if (parent is NavigationPageRenderer)
        //        return parent;
        //    return GetNavigationPage(parent.Parent);
        //}
    }
}