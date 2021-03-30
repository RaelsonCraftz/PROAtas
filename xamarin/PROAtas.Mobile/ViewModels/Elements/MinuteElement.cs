using Craftz.ViewModel;
using PROAtas.Core;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PROAtas.ViewModels.Elements
{
    public class MinuteElement : BaseElement<Minute>
    {
        public MinuteElement() { }

        public MinuteElement(Minute model) : base(model) { }

        #region Bindable Properties

        public string Name
        {
            get => Model.Name;
            set { Model.Name = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get => DateTime.ParseExact(!string.IsNullOrEmpty(Model.Date) ? Model.Date : DateTime.Today.ToString(Formats.DateFormat), Formats.DateFormat, CultureInfo.InvariantCulture);
            set { Model.Date = value.ToString(Formats.DateFormat); OnPropertyChanged(); }
        }

        public TimeSpan Start
        {
            get => TimeSpan.ParseExact(!string.IsNullOrEmpty(Model.Start) ? Model.Start : DateTime.Now.ToString(Formats.TimeFormat), Formats.TimeFormat, CultureInfo.InvariantCulture);
            set { Model.Start = value.ToString(Formats.TimeFormat); OnPropertyChanged(); }
        }

        public TimeSpan End
        {
            get => TimeSpan.ParseExact(!string.IsNullOrEmpty(Model.End) ? Model.End : DateTime.Now.ToString(Formats.TimeFormat), Formats.TimeFormat, CultureInfo.InvariantCulture);
            set { Model.End = value.ToString(Formats.TimeFormat); OnPropertyChanged(); }
        }

        public int PeopleQuantity
        {
            get => Model.PeopleQuantity;
            set { Model.PeopleQuantity = value; OnPropertyChanged(); }
        }

        public bool IsFavorite
        {
            get => Model.Favorite;
            set { Model.Favorite = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }
        bool _isSelected;

        public ObservableCollection<TopicElement> Topics { get; } = new ObservableCollection<TopicElement>();

        public ObservableCollection<PersonElement> People { get; } = new ObservableCollection<PersonElement>();

        #endregion
    }
}
