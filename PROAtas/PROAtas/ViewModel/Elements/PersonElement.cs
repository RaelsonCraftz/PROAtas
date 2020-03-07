using PROAtas.Model;

namespace PROAtas.ViewModel.Elements
{
    public class PersonElement : BaseElement<Person>
    {
        public PersonElement() { }

        public PersonElement(Person model) : base(model)
        {
            this.Name = model.Name;
            this.Order = model.Order;
        }

        #region Bindable Properties

        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(); }
        }
        private string _name;

        public int Order
        {
            get => _order;
            set { _order = value; NotifyPropertyChanged(); }
        }
        private int _order;

        public bool IsSaving
        {
            get => _isSaving;
            set { _isSaving = value; NotifyPropertyChanged(); }
        }
        private bool _isSaving;

        #endregion

        #region Helpers



        #endregion
    }
}
