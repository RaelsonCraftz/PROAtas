using System;
using Xamarin.Forms.Xaml;
using Craftz.Views;
using PROAtas.Mobile.ViewModel;
using PROAtas.Core.Model;

namespace PROAtas.Mobile.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeopleDialog : BaseDialog<PeopleViewModel, People>
    {
        public PeopleDialog()
        {
            InitializeComponent();
        }

        public PeopleDialog(People model) : base(model)
        {
            InitializeComponent();
        }

        public PeopleDialog(People model, Action<People> action) : base(model, action)
        {
            InitializeComponent();
        }
    }
}