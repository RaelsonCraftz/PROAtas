using Craftz.ViewModel;
using PROAtas.Core;
using PROAtas.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PROAtas.ViewModel.Elements
{
    public class TopicElement : BaseElement<Topic>
    {
        public TopicElement() { }

        public TopicElement(Topic model) : base(model) { }

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

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }
        private bool _isSelected;

        public ObservableCollection<InformationElement> Information { get; } = new ObservableCollection<InformationElement>();

        #endregion
    }
}
