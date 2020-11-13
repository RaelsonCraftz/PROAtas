using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Services
{
    public interface IAdService
    {
        void ShowVideo(Action onReward, Action onClose, Action onFailure, string adUnit);
    }
}
