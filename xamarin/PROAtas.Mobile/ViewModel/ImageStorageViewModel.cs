using Acr.UserDialogs;
using Craftz.ViewModel;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.Services.Platform;
using PROAtas.Mobile.Services.Shared;
using PROAtas.Mobile.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class ImageStorageViewModel : BaseViewModel<MinuteImageElement>
    {
        private readonly IDataService dataService;
        private readonly IImageService imageService;

        public ImageStorageViewModel()
        {
            dataService = DependencyService.Get<IDataService>();
            imageService = DependencyService.Get<IImageService>();
        }

        public Action<MinuteImageElement> OnResult;

        #region Bindable Properties

        public MinuteImageElement SelectedImage
        {
            get => _selectedImage;
            set { _selectedImage = value; OnPropertyChanged(); }
        }
        private MinuteImageElement _selectedImage;

        public ObservableRangeCollection<MinuteImageElement> ImageCollection { get; } = new ObservableRangeCollection<MinuteImageElement>();

        #endregion

        #region Commands

        public Command<MinuteImageElement> SelectImage
        {
            get { if (_selectImage == null) _selectImage = new Command<MinuteImageElement>(SelectImageExecute); return _selectImage; }
        }
        private Command<MinuteImageElement> _selectImage;
        private void SelectImageExecute(MinuteImageElement minuteImage)
        {
            if (minuteImage != null)
                logService.LogAction(() =>
                {
                    OnResult?.Invoke(minuteImage);
                },
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Toast(log);
                });
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize(MinuteImageElement element)
        {
            base.Initialize();

            var logoSource = ImageSource.FromFile("icLogo.png");
            var logoImage = new MinuteImageElement(new MinuteImage { Id = 0 })
            {
                Source = logoSource
            };

            var images = dataService.MinuteImageRepository.GetAll();
            var imageCollection = new List<MinuteImageElement> { logoImage };

            imageCollection
                .AddRange(images.Select(l => new MinuteImageElement(l)
                {
                    Source = imageService.GetImageFromFile(l.Name)
                }));

            ImageCollection.AddRange(imageCollection);
            SelectedImage = ImageCollection.FirstOrDefault(l => l.Model.Id == element.Model.Id) ?? logoImage;
        }

        #endregion
    }
}
