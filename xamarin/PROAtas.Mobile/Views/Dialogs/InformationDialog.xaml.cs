using Craftz.Dialogs;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.ViewModel;
using System;
using Xamarin.Forms.Xaml;

namespace PROAtas.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformationDialog : BaseDialog<InformationViewModel, Information>
    {
        public InformationDialog()
        {
            InitializeComponent();
        }

        public InformationDialog(Information model) : base(model)
        {
            InitializeComponent();
        }

        public InformationDialog(Information model, Action<Information> action) : base(model, action)
        {
            InitializeComponent();
        }
    }
}