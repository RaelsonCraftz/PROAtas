using PROAtas.Core;
using PROAtas.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PROAtas.ViewModel.Elements
{
    public class MinuteElement : BaseElement<Minute>
    {
        public MinuteElement() { }

        public MinuteElement(Minute model) : base(model)
        {
            this.Name = model.Name;
            this.Date = DateTime.ParseExact(model.Date, Formats.DateFormat, CultureInfo.InvariantCulture);
            this.Start = TimeSpan.ParseExact(model.Start, Formats.TimeFormat, CultureInfo.InvariantCulture);
            this.End = TimeSpan.ParseExact(model.End, Formats.TimeFormat, CultureInfo.InvariantCulture);
            this.PeopleQuantity = model.PeopleQuantity;
        }

        #region Bindable Properties

        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(); }
        }
        private string _name;

        public DateTime Date
        {
            get => _date;
            set { _date = value; NotifyPropertyChanged(); }
        }
        private DateTime _date;

        public TimeSpan Start
        {
            get => _start;
            set { _start = value; NotifyPropertyChanged(); }
        }
        private TimeSpan _start;

        public TimeSpan End
        {
            get => _end;
            set { _end = value; NotifyPropertyChanged(); }
        }
        private TimeSpan _end;

        public int PeopleQuantity
        {
            get => _peopleQuantity;
            set { _peopleQuantity = value; NotifyPropertyChanged(); }
        }
        private int _peopleQuantity;

        #endregion

        #region Helpers



        #endregion
    }
}
