using PROAtas.iOS.Services;
using PROAtas.Services;
using System.Diagnostics;
using Xamarin.Forms;

[assembly: Dependency(typeof(AccessibilityServiceIOS))]
namespace PROAtas.iOS.Services
{
    public class AccessibilityServiceIOS : IAccessibilityService
    {
        // [CHECKME] Still being implemented!
        public float PlatformFontSize()
        {
            var sizeCategory = UIKit.UIApplication.SharedApplication.PreferredContentSizeCategory;

            Debug.WriteLine(sizeCategory);

            return 1;
        }
    }
}