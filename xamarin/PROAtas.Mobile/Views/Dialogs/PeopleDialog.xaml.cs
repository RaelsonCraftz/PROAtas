using PROAtas.Helpers.Model;
using System;
using Xamarin.Forms.Xaml;
using Craftz.Dialogs;
using PROAtas.Mobile.ViewModel;

namespace PROAtas.Views.Dialogs
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