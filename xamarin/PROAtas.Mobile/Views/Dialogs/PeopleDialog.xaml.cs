using PROAtas.Helpers.Model;
using PROAtas.Core;
using PROAtas.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using Craftz.Dialogs;

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