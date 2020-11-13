using Android.Gms.Ads.Reward;
using PROAtas.Droid.Activities;
using PROAtas.Droid.Services;
using PROAtas.Services;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class AdServiceDroid : IAdService
    {
        public IRewardedVideoAd rewardedVideoAd { get; set; }

        public void ShowVideo(Action onReward, Action onClose, Action onFailure, string adUnit)
        {
            new VideoActivity(onReward, onClose, onFailure, adUnit);
        }
    }
}