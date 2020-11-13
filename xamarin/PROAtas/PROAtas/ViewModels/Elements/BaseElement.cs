using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PROAtas.ViewModels.Elements
{
    public class BaseElement<T> : INotifyPropertyChanged
    {
        public T Original { get; }
        public T Model { get; }

        public BaseElement()
        {

        }

        public BaseElement(T model)
        {
            this.Original = model;
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
