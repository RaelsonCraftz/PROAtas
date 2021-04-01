using Craftz.ViewModel;
using PROAtas.Core.Model;
using PROAtas.Mobile.ViewModel.Elements;

namespace PROAtas.Mobile.ViewModel
{
    public class MomentViewModel : BaseViewModel<Moment>
    {
        public MomentViewModel()
        {

        }

        #region Bindable Properties

        public MomentElement Moment
        {
            get => _moment;
            set { _moment = value; OnPropertyChanged(); }
        }
        private MomentElement _moment;

        #endregion

        #region Commands



        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize(Moment model)
        {
            base.Initialize();

            Moment = new MomentElement(model);
        }

        #endregion
    }
}