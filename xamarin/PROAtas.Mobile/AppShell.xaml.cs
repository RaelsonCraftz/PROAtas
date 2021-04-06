using PROAtas.ViewModel;
using PROAtas.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PROAtas
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

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
