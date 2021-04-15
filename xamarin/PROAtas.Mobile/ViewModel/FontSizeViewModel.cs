using Acr.UserDialogs;
using Craftz.ViewModel;
using PROAtas.Core;
using PROAtas.Core.Model;
using PROAtas.Mobile.Core;
using PROAtas.Mobile.Services.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class FontSizeViewModel : BaseViewModel
    {
        private readonly IDataService dataService;

        public FontSizeViewModel()
        {
            dataService = DependencyService.Get<IDataService>();
        }

        public Action<double> OnResult;

        #region Bindable Properties

        public double TitleFontSize => FontSize + 4;

        public double FontSize
        {
            get => _fontSize;
            set { _fontSize = value; OnPropertyChanged(); OnPropertyChanged(nameof(TitleFontSize)); }
        }
        private double _fontSize;

        #endregion

        #region Commands

        public Command<double> SaveFontSize
        {
            get { if (_saveFontSize == null) _saveFontSize = new Command<double>(SaveFontSizeExecute); return _saveFontSize; }
        }
        private Command<double> _saveFontSize;
        private void SaveFontSizeExecute(double fontSize)
        {
            logService.LogActionAsync(async () =>
            {
                App.Current.Properties[AppConsts.FontSize] = fontSize;
                OnResult(fontSize);

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize()
        {
            base.Initialize();

            double.TryParse(App.Current.Properties[AppConsts.FontSize].ToString(), out var result);
            FontSize = result;
        }

        #endregion
    }
}
