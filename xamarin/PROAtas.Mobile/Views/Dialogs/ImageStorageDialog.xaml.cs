using Craftz.Views;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.ViewModel;
using PROAtas.Mobile.ViewModel.Elements;
using System;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageStorageDialog : BaseDialog<ImageStorageViewModel, MinuteImageElement>
    {
        public ImageStorageDialog() : base()
        {
            InitializeComponent();
        }

        public ImageStorageDialog(MinuteImageElement model, Action<MinuteImageElement> action) : base(model, action)
        {
            InitializeComponent();
            ViewModel.OnResult = action;
        }
    }
}