using PROAtas.Droid.Services;
using PROAtas.Droid.Views;
using PROAtas.Mobile.Services.Platform;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class AdServiceDroid : IAdService
    {
        public void ShowVideo(string adUnit, Action onReward, Action onClose, Action onFailure)
        {
            new VideoActivity(onReward, onClose, onFailure, adUnit);
        }
    }
}