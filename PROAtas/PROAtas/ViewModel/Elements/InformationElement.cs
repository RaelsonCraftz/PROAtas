using Craftz.ViewModel;
using PROAtas.Model;

namespace PROAtas.ViewModel.Elements
{
    public class InformationElement : BaseElement<Information>
    {
        public InformationElement() { }

        public InformationElement(Information model) : base(model)
        {
            this.Order = model.Order;
            this.Text = model.Text;
        }

        #region Bindable Properties

        public int Order
        {
            get => _order;
            set { _order = value; NotifyPropertyChanged(); }
        }
        private int _order;

        public string Text
        {
            get => _text;
            set { _text = value; NotifyPropertyChanged(); }
        }
        private string _text;

        #endregion

        #region Helpers



        #endregion
    }
}
