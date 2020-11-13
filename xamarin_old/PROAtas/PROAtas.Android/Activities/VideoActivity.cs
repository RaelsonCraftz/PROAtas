using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Android.Support.V7.App;
using System;

namespace PROAtas.Droid.Activities
{
    public class VideoActivity : AppCompatActivity, IRewardedVideoAdListener
    {
        private readonly Action onReward;
        private readonly Action onClose;
        private readonly Action onFailure;
        private readonly IRewardedVideoAd rewardedVideoAd;

        public VideoActivity(Action onReward, Action onClose, Action onFailure, string adUnit)
        {
            this.onReward = onReward;
            this.onClose = onClose;
            this.onFailure = onFailure;

            rewardedVideoAd = MobileAds.GetRewardedVideoAdInstance(Android.App.Application.Context);
            var adRequest = new AdRequest.Builder().Build();
            rewardedVideoAd.UserId = "pub-1711953563979738";
            rewardedVideoAd.RewardedVideoAdListener = this;
            rewardedVideoAd.LoadAd(adUnit, adRequest);
        }

        public void OnRewarded(IRewardItem reward)
        {
            onReward?.Invoke();
        }

        public void OnRewardedVideoAdClosed()
        {
            onClose?.Invoke();
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            onFailure?.Invoke();
        }

        public void OnRewardedVideoAdLeftApplication()
        {

        }

        public void OnRewardedVideoAdLoaded()
        {
            rewardedVideoAd.Show();
        }

        public void OnRewardedVideoAdOpened()
        {

        }

        public void OnRewardedVideoCompleted()
        {

        }

        public void OnRewardedVideoStarted()
        {

        }
    }
}