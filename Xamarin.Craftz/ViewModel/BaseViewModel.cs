using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Craftz.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public virtual void Initialize()
        {

        }

        public virtual void Leave()
        {

        }

        #region INotifyPropertyChanged Implementation

        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Helpers

        protected void InvokeMainThread(Action action)
            => Device.BeginInvokeOnMainThread(action);

        protected async Task DisplayAlert(string title, string message, string cancel)
            => await Application.Current.MainPage.DisplayAlert(title, message, cancel);

        protected async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

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
