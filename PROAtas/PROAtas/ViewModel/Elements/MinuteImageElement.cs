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

        public ImageSource ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; NotifyPropertyChanged(); }
        }
        private ImageSource _imageSource;

        #endregion
    }
}
