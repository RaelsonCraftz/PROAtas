using PROAtas.Models;
using PROAtas.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public virtual void Initialize()
        {

        }

        public virtual void Leave()
        {

        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool isBusy = false;
        
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string title = string.Empty;

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

        #region INotifyPropertyChanged Implementation

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class BaseViewModel<TModel> : INotifyPropertyChanged where TModel : class
    {
        public virtual void Initialize(TModel model = null)
        {

        }

        public virtual void Leave()
        {

        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool isBusy = false;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string title = string.Empty;

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

        #region INotifyPropertyChanged Implementation

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
