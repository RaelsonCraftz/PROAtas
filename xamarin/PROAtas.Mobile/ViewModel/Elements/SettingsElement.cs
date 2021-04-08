using Craftz.ViewModel;
using PROAtas.Core.Model;

namespace PROAtas.Mobile.ViewModel.Elements
{
    public class SettingsElement : BaseElement<Settings>
    {
        public SettingsElement() { }

        public SettingsElement(Settings model) : base(model) { }

        #region Bindable Properties

        public string User
        {
            get => Model.User;
            set { Model.User = value; OnPropertyChanged(); }
        }

        public string Organization
        {
            get => Model.Organization;
            set { Model.Organization = value; OnPropertyChanged(); }
        }

        public double FontSize
        {
            get => Model.FontSize;
            set { Model.FontSize = value; OnPropertyChanged(); }
        }

        public string FontFamily
        {
            get => Model.FontFamily;
            set { Model.FontFamily = value; OnPropertyChanged(); }
        }

        public double MarginLeft
        {
            get => Model.MarginLeft;
            set { Model.MarginLeft = value; OnPropertyChanged(); }
        }

        public double MarginTop
        {
            get => Model.MarginTop;
            set { Model.MarginTop = value; OnPropertyChanged(); }
        }

        public double MarginRight
        {
            get => Model.MarginRight;
            set { Model.MarginRight = value; OnPropertyChanged(); }
        }

        public double MarginBottom
        {
            get => Model.MarginBottom;
            set { Model.MarginBottom = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
