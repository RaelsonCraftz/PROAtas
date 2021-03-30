﻿using Craftz.ViewModel;
using PROAtas.Core;

namespace PROAtas.ViewModels.Elements
{
    public class InformationElement : BaseElement<Information>
    {
        public InformationElement() { }

        public InformationElement(Information model) : base(model) { }

        #region Bindable Properties

        public int Order
        {
            get => Model.Order;
            set { Model.Order = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get => Model.Text;
            set { Model.Text = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
