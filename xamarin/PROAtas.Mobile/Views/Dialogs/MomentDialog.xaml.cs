using Craftz.Views;
using PROAtas.Core.Model;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PROAtas.Mobile.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MomentDialog : BaseDialog<MomentViewModel, Minute>
    {
        public MomentDialog()
        {
            InitializeComponent();
        }

        public MomentDialog(Minute model) : base(model)
        {
            InitializeComponent();
        }

        public MomentDialog(Minute model, Action<Minute> action) : base(model, action)
        {
            InitializeComponent();
            ViewModel.OnResult = action;
        }
    }
}