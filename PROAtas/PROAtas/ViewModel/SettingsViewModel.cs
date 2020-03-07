using PROAtas.Core;
using PROAtas.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Service Container

        private readonly IToastService toastService = App.Current.toastService;
        private readonly IImageService imageService = App.Current.imageService;
        private readonly ILogService logService = App.Current.logService;

        #endregion

        public SettingsViewModel()
        {

        }

        #region Bindable Properties

        public List<string> FontList = new List<string>
        {
            "Arial",
            "Calibri",
            "Century Gothic",
            "Courier New",
            "Times New Roman",
            "Rockwell",
            "Segoe Script",
        };

        public ImageSource MinuteImage
        {
            get => _minuteImage;
            set { _minuteImage = value; NotifyPropertyChanged(); }
        }
        private ImageSource _minuteImage;

        public string User
        {
            get => _user;
            set { _user = value; NotifyPropertyChanged(); }
        }
        private string _user;

        public string Organization
        {
            get => _organization;
            set { _organization = value; NotifyPropertyChanged(); }
        }
        private string _organization;

        public string FontFamily
        {
            get => _fontFamily;
            set { _fontFamily = value; NotifyPropertyChanged(); }
        }
        private string _fontFamily;

        public int FontSize
        {
            get => _fontSize;
            set { _fontSize = value; NotifyPropertyChanged(); }
        }
        private int _fontSize;

        public double MarginLeft
        {
            get => _marginLeft;
            set { _marginLeft = value; NotifyPropertyChanged(); }
        }
        private double _marginLeft;

        public double MarginTop
        {
            get => _marginTop;
            set { _marginTop = value; NotifyPropertyChanged(); }
        }
        private double _marginTop;

        public double MarginRight
        {
            get => _marginRight;
            set { _marginRight = value; NotifyPropertyChanged(); }
        }
        private double _marginRight;

        public double MarginBottom
        {
            get => _marginBottom;
            set { _marginBottom = value; NotifyPropertyChanged(); }
        }
        private double _marginBottom;

        #endregion

        #region Commading

        public ICommand SaveUser => new Command<string>(p => SaveUserExecute(p));
        private void SaveUserExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.UserName] = param;
                    App.Current.SavePropertiesAsync();
                    User = param;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand SaveOrganization => new Command<string>(p => SaveOrganizationExecute(p));
        private void SaveOrganizationExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.OrganizationName] = param;
                    App.Current.SavePropertiesAsync();
                    Organization = param;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeFontSize => new Command<int>(p => ChangeFontSizeExecute(p));
        private void ChangeFontSizeExecute(int param)
        {
            if (param != 0)
            {
                var log = logService.LogAction(() =>
                {
                    if (12 <= (FontSize + param) && (FontSize + param) <= 20)
                    {
                        App.Current.Properties[Constants.FontSize] = FontSize + param;
                        App.Current.SavePropertiesAsync();
                        FontSize += param;
                    }
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeFontFamily => new Command<string>(p => ChangeFontFamilyExecute(p));
        private void ChangeFontFamilyExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.FontFamily] = param;
                    App.Current.SavePropertiesAsync();
                    FontFamily = param;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeMarginLeft => new Command<string>(p => ChangeMarginLeftExecute(p));
        private void ChangeMarginLeftExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginLeft].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginLeft] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginLeft = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeMarginTop => new Command<string>(p => ChangeMarginTopExecute(p));
        private void ChangeMarginTopExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginTop].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginTop] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginTop = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeMarginRight => new Command<string>(p => ChangeMarginRightExecute(p));
        private void ChangeMarginRightExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginRight].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginRight] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginRight = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeMarginBottom => new Command<string>(p => ChangeMarginBottomExecute(p));
        private void ChangeMarginBottomExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginBottom].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginBottom] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginBottom = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeMinuteImage => new Command(() => ChangeMinuteImageExecute());
        private async void ChangeMinuteImageExecute()
        {
            var stream = await imageService.GetImageStreamAsync();
            if (stream != null)
            {
                var image = ImageSource.FromStream(() => stream);

                MinuteImage = image;
            }
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize()
        {
            var user = App.Current.Properties[Constants.UserName].ToString();
            var organization = App.Current.Properties[Constants.OrganizationName].ToString();
            var fontSize = int.Parse(App.Current.Properties[Constants.FontSize].ToString());
            var fontFamily = App.Current.Properties[Constants.FontFamily].ToString();
            var marginLeft = double.Parse(App.Current.Properties[Constants.MarginLeft].ToString());
            var marginTop = double.Parse(App.Current.Properties[Constants.MarginTop].ToString());
            var marginRight = double.Parse(App.Current.Properties[Constants.MarginRight].ToString());
            var marginBottom = double.Parse(App.Current.Properties[Constants.MarginBottom].ToString());

            User = user;
            Organization = organization;
            FontSize = fontSize;
            FontFamily = fontFamily;
            MarginLeft = marginLeft;
            MarginTop = marginTop;
            MarginRight = marginRight;
            MarginBottom = marginBottom;
        }

        public override void Leave()
        {

        }

        #endregion
    }
}
