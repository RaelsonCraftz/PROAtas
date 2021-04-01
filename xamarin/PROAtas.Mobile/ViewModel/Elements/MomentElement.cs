using Craftz.ViewModel;
using PROAtas.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PROAtas.Mobile.ViewModel.Elements
{
    public class MomentElement : BaseElement<Moment>
    {
        public MomentElement() { }

        public MomentElement(Moment model) : base(model) { }

        #region Bindable Properties

        public DateTime Date
        {
            get => Model.Date;
            set { Model.Date = value; OnPropertyChanged(); }
        }

        public TimeSpan Start
        {
            get => Model.Start;
            set { Model.Start = value; OnPropertyChanged(); }
        }

        public TimeSpan End
        {
            get => Model.End;
            set { Model.End = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
