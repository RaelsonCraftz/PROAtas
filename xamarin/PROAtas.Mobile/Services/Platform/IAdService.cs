using System;

namespace PROAtas.Mobile.Services.Platform
{
    public interface IAdService
    {
        void ShowVideo(string adUnit, Action onReward, Action onClose, Action onFailure);
    }
}
