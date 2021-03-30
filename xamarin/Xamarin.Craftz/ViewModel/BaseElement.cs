using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Craftz.ViewModel
{
    public class BaseElement<T> : INotifyPropertyChanged where T : new()
    {
        public T Original { get; }
        public T Model { get; }

        public BaseElement()
        {

        }

        public BaseElement(T model)
        {
            this.Original = (T)Activator.CreateInstance(typeof(T), model);
            this.Model = model;
        }


        #region INotifyPropertyChanged

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
