using Craftz.ViewModel;
using PROAtas.Model;
using Xamarin.Forms;

namespace PROAtas.ViewModel.Elements
{
    public class MinuteImageElement : BaseElement<MinuteImage>
    {
        public MinuteImageElement() { }

        public MinuteImageElement(MinuteImage model) : base(model)
        {
            
        }

        #region Bindable Properties

        public ImageSource Source
        {
            get => _source;
            set { _source = value; NotifyPropertyChanged(); }
        }
        private ImageSource _source;

        #endregion
    }
}
