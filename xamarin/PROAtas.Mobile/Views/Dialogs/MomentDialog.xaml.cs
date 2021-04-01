using Craftz.Dialogs;
using PROAtas.Core.Model;
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
    public partial class MomentDialog : BaseDialog<MomentViewModel, Moment>
    {
        public MomentDialog()
        {
            InitializeComponent();
        }

        public MomentDialog(Moment model) : base(model)
        {
            InitializeComponent();
        }

        public MomentDialog(Moment model, Action<Moment> action) : base(model, action)
        {
            InitializeComponent();
        }
    }
}