using Craftz.ViewModel;
using PROAtas.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel.Elements
{
    public class MinuteImageElement : BaseElement<MinuteImage>
    {
        public MinuteImageElement() { }

        public MinuteImageElement(MinuteImage model) : base(model) { }

        #region Bindable Properties

        public ImageSource Source
        {
            get => _source;
            set { _source = value; OnPropertyChanged(); }
        }
        private ImageSource _source;

        #endregion
    }
}
