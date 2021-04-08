using Craftz.Views;
using PROAtas.Mobile.ViewModel;
using System;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FontSizeDialog : BaseDialog<FontSizeViewModel>
    {
        public FontSizeDialog() : base()
        {
            InitializeComponent();
        }

        public FontSizeDialog(Action<double> action) : base()
        {
            InitializeComponent();

            ViewModel.OnResult = action;
        }
    }
}