using Android.Content.Res;
using PROAtas.Droid.Services;
using PROAtas.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

[assembly: Dependency(typeof(AccessibilityServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class AccessibilityServiceDroid : IAccessibilityService
    {
        public float PlatformFontSize()
        {
            var fontScale = Resources.System.Configuration.FontScale;
            Debug.WriteLine($"Platform FontScale: {fontScale}");

            return Resources.System.Configuration.FontScale;
        }
    }
}