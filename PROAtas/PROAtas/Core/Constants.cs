using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core
{
    public class Constants
    {
        // AdMob codes
#if DEBUG
        public const string AdHome = "ca-app-pub-3940256099942544/6300978111";
        public const string AdMinute = "ca-app-pub-3940256099942544/6300978111";
        public const string AdVideo = "ca-app-pub-3940256099942544/5224354917";
#else
        public const string AdHome = "ca-app-pub-1711953563979738/2709482100";
        public const string AdMinute = "ca-app-pub-1711953563979738/3033886902";
        public const string AdVideo = "ca-app-pub-1711953563979738/2537994585";
#endif

        // Preference keys
        public const string UserName = nameof(UserName);
        public const string OrganizationName = nameof(OrganizationName);
        public const string FontSize = nameof(FontSize);
        public const string FontFamily = nameof(FontFamily);
        public const string MarginLeft = nameof(MarginLeft);
        public const string MarginTop = nameof(MarginTop);
        public const string MarginRight = nameof(MarginRight);
        public const string MarginBottom = nameof(MarginBottom);

        public const string Version = nameof(Version);
    }
}
