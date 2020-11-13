using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Craftz.ViewModel
{
    public class BaseElement<TModel> : INotifyPropertyChanged
    {
        public TModel Model { get; }

        public BaseElement() { }

        public BaseElement(TModel model)
        {
            this.Model = model;
        }

        #region INotifyPropertyChanged Implementation

        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
