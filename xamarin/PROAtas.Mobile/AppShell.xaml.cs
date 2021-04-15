using PROAtas.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public string Version => VersionTracking.CurrentVersion;
        public string Build => VersionTracking.CurrentBuild;

        protected override bool OnBackButtonPressed()
        {
            var page = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

            if (page.SendBackButtonPressed())
                return true;
            else
                return base.OnBackButtonPressed();
        }
    }
}
