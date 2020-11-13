using Craftz.ViewModel;
using PROAtas.Model;

namespace PROAtas.ViewModel.Elements
{
    public class TopicElement : BaseElement<Topic>
    {
        public TopicElement() { }

        public TopicElement(Topic model) : base(model)
        {
            this.Order = model.Order;
            this.Text = model.Text;
            this.IsSelected = false;
        }

        #region Bindable Properties

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; NotifyPropertyChanged(); }
        }
        private bool _isSelected;

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
