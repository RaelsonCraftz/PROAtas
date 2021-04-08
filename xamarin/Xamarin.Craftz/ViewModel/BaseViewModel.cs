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

        public virtual void Leave()
        {

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

        public Command ClosePage
        {
            get { if (_closePage == null) _closePage = new Command(ClosePageExecute); return _closePage; }
        }
        private Command _closePage;
        private void ClosePageExecute()
        {
            if (CanLeave())
                Shell.Current.Navigation.PopAsync(true);
        }

        public Command CloseDialog
        {
            get { if (_closeDialog == null) _closeDialog = new Command(CloseDialogExecute); return _closeDialog; }
        }
        private Command _closeDialog;
        private void CloseDialogExecute()
        {
            if (CanLeave())
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
                ShowLoading(message);

            try
            {
                await task();
            }
            catch (Exception e)
            {
                HideLoading();
                throw e;
            }

            HideLoading();
        }

        protected void ShowLoading(string message = null)
        {
            UserDialogs.Instance.ShowLoading(message ?? "Carregando", MaskType.Black);
        }

        protected void HideLoading()
        {
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

        public virtual void Leave()
        {

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

        public Command ClosePage
        {
            get { if (_closePage == null) _closePage = new Command(ClosePageExecute); return _closePage; }
        }
        private Command _closePage;
        private void ClosePageExecute()
        {
            if (CanLeave())
                Shell.Current.Navigation.PopAsync(true);
        }

        public Command CloseDialog
        {
            get { if (_closeDialog == null) _closeDialog = new Command(CloseDialogExecute); return _closeDialog; }
        }
        private Command _closeDialog;
        private void CloseDialogExecute()
        {
            if (CanLeave())
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
                ShowLoading(message);

            try
            {
                await task();
            }
            catch (Exception e)
            {
                HideLoading();
                throw e;
            }

            HideLoading();
        }

        protected void ShowLoading(string message = null)
        {
            UserDialogs.Instance.ShowLoading(message ?? "Carregando", MaskType.Black);
        }

        protected void HideLoading()
        {
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
