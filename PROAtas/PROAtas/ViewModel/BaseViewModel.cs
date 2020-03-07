using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public abstract void Initialize();
        public abstract void Leave();

        #region INotifyPropertyChanged Implementation

        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

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

        #endregion
    }

    public abstract class BaseViewModel<TModel> : INotifyPropertyChanged where TModel : class
    {
        public abstract void Initialize(TModel model = null);
        public abstract void Leave();

        #region INotifyPropertyChanged Implementation

        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

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

        #endregion
    }
}
