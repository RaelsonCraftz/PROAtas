using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Core
{
    public static class AppConsts
    {
        // AdMob codes
#if DEBUG
        public const string AdHome = "ca-app-pub-3940256099942544/6300978111";
        public const string AdMinute = "ca-app-pub-3940256099942544/6300978111";
        public const string AdVideo = "ca-app-pub-3940256099942544/5224354917";
        public const string AdOrganizacao = "ca-app-pub-3940256099942544/6300978111";
        public const string AdMargem = "ca-app-pub-3940256099942544/6300978111";
        public const string AdImagem = "ca-app-pub-3940256099942544/6300978111";
        public const string AdInformacao = "ca-app-pub-3940256099942544/6300978111";
        public const string AdPessoas = "ca-app-pub-3940256099942544/6300978111";
        public const string AdMomento = "ca-app-pub-3940256099942544/6300978111";
#else
        public const string AdHome = "ca-app-pub-1711953563979738/2709482100";
        public const string AdMinute = "ca-app-pub-1711953563979738/3033886902";
        public const string AdVideo = "ca-app-pub-1711953563979738/2537994585";
        public const string AdOrganizacao = "ca-app-pub-1711953563979738/6412535655";
        public const string AdMargem = "ca-app-pub-1711953563979738/3594800623";
        public const string AdImagem = "ca-app-pub-1711953563979738/9421842373";
        public const string AdInformacao = "ca-app-pub-1711953563979738/2248939334";
        public const string AdPessoas = "ca-app-pub-1711953563979738/1865795951";
        public const string AdMomento = "ca-app-pub-1711953563979738/8385960516";
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
        public const string SelectedMinuteImage = nameof(SelectedMinuteImage);

        public const string Version = nameof(Version);
    }
}
