using PROAtas.ViewModels.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {

        }

        #region Bindable Properties

        public ObservableCollection<MinuteElement> Minutes { get; } = new ObservableCollection<MinuteElement>();

        #endregion

        #region Commands

        public Command SearchMinute
        {
            get { if (_searchMinute == null) _searchMinute = new Command(SearchMinuteExecute); return _searchMinute; }
        }
        private Command _searchMinute;
        private void SearchMinuteExecute()
        {

        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers



        #endregion
    }
}
