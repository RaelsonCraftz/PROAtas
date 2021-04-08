using Craftz.ViewModel;
using PROAtas.Core;
using PROAtas.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Mobile.ViewModel.Elements
{
    public class PersonElement : BaseElement<Person>
    {
        public PersonElement() { }

        public PersonElement(Person model) : base(model) { }

        #region Bindable Properties

        public string Name
        {
            get => Model.Name;
            set { Model.Name = value; OnPropertyChanged(); }
        }

        public int Order
        {
            get => Model.Order;
            set { Model.Order = value; OnPropertyChanged(); }
        }

        public bool IsNameSaving
        {
            get => _isNameSaving;
            set { _isNameSaving = value; OnPropertyChanged(); }
        }
        private bool _isNameSaving;

        #endregion
    }
}
