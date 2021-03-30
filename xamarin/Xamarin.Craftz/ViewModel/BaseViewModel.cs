using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Craftz.Services;
using Xamarin.Forms;

namespace Craftz.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected ILogService logService;

        public virtual void Initialize()
        {
            logService = DependencyService.Get<ILogService>();
        }

        public virtual bool CanLeave()
        {
            return true;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }
        bool _isBusy = false;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        string _title = string.Empty;

        #region Commands

        public Command Close
        {
            get { if (_close == null) _close = new Command(CloseExecute); return _close; }
        }
        private Command _close;
        private void CloseExecute()
        {
            PopupNavigation.Instance.PopAsync();
        }

        #endregion

        #region Helpers

        protected void InvokeMainThread(Action action)
            => Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(action);

        protected async Task DisplayAlert(string title, string message, string cancel)
            => await Application.Current.MainPage.DisplayAlert(title, message, cancel);

        protected async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
            => await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);

        protected async Task SetBusyAsync(Func<Task> task, string message = null, bool showLoading = true)
        {
            if (showLoading)
                UserDialogs.Instance.ShowLoading(message ?? "Carregando", MaskType.Black);

            await task();

            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class BaseViewModel<TModel> : INotifyPropertyChanged where TModel : class
    {
        protected ILogService logService;

        public virtual void Initialize(TModel model = null)
        {
            logService = DependencyService.Get<ILogService>();
        }

        public virtual bool CanLeave()
        {
            return true;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }
        bool _isBusy = false;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        string _title = string.Empty;

        #region Commands

        public Command Close
        {
            get { if (_close == null) _close = new Command(CloseExecute); return _close; }
        }
        private Command _close;
        private void CloseExecute()
        {
            PopupNavigation.Instance.PopAsync();
        }

        #endregion

        #region Helpers

        protected void InvokeMainThread(Action action)
            => Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(action);

        protected async Task DisplayAlert(string title, string message, string cancel)
            => await Application.Current.MainPage.DisplayAlert(title, message, cancel);

        protected async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        protected async Task SetBusyAsync(Func<Task> task, string message = null, bool showLoading = true)
        {
            if (showLoading)
                UserDialogs.Instance.ShowLoading(message ?? "Carregando", MaskType.Black);

            await task();

            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
