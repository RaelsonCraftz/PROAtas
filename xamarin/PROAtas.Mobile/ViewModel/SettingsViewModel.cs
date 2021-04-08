using Acr.UserDialogs;
using Craftz.ViewModel;
using PROAtas.Core;
using PROAtas.Core.Model;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.Services.Platform;
using PROAtas.Mobile.Services.Shared;
using PROAtas.Mobile.ViewModel.Elements;
using PROAtas.Mobile.Views.Dialogs;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IDataService dataService;
        private readonly IImageService imageService;
        private readonly IPermissionService permissionService;

        public SettingsViewModel()
        {
            dataService = DependencyService.Get<IDataService>();
            imageService = DependencyService.Get<IImageService>();
            permissionService = DependencyService.Get<IPermissionService>();
        }

        #region Bindable Properties

        public List<string> FontList => new List<string>
        {
            "Arial",
            "Calibri",
            "Century Gothic",
            "Courier New",
            "Times New Roman",
            "Rockwell",
            "Segoe Script",
        };

        public bool IsUserSaving
        {
            get => _isUserSaving;
            set { _isUserSaving = value; OnPropertyChanged(); }
        }
        private bool _isUserSaving;

        public bool IsOrganizationSaving
        {
            get => _isOrganizationSaving;
            set { _isOrganizationSaving = value; OnPropertyChanged(); }
        }
        private bool _isOrganizationSaving;

        public SettingsElement Settings
        {
            get => _settings;
            set { _settings = value; OnPropertyChanged(); }
        }
        private SettingsElement _settings;

        public MinuteImageElement SelectedImage
        {
            get => _selectedImage;
            set { _selectedImage = value; OnPropertyChanged(); }
        }
        private MinuteImageElement _selectedImage;

        #endregion

        #region Commands

        public Command OpenFontSizeDialog
        {
            get { if (_openFontSizeDialog == null) _openFontSizeDialog = new Command(OpenFontSizeDialogExecute); return _openFontSizeDialog; }
        }
        private Command _openFontSizeDialog;
        private void OpenFontSizeDialogExecute()
        {
            logService.LogActionAsync(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new FontSizeDialog(UpdateFontSize));
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command OpenImageStorageDialog
        {
            get { if (_openImageStorageDialog == null) _openImageStorageDialog = new Command(OpenImageStorageDialogExecute); return _openImageStorageDialog; }
        }
        private Command _openImageStorageDialog;
        private void OpenImageStorageDialogExecute()
        {
            logService.LogActionAsync(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new ImageStorageDialog(SelectedImage, UpdateImage));
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command OpenGallery
        {
            get { if (_openGallery == null) _openGallery = new Command(OpenGalleryExecute); return _openGallery; }
        }
        private Command _openGallery;
        private void OpenGalleryExecute()
        {
            logService.LogActionAsync(async () =>
            {
                if (!await permissionService.RequestStoragePermission())
                    return;

                var stream = await imageService.GetImageFromGalleryAsync();
                if (stream == null)
                    return;

                var imageStream = new MemoryStream(imageService.GetBytesFromStream(stream))
                {
                    Position = 0,
                };

                // ImageSource.FromStream already disposes the memory stream
                var imageSource = ImageSource.FromStream(() => imageStream);
                var minuteImage = new MinuteImage()
                {
                    Name = Guid.NewGuid().ToString(),
                };

                // Persisting the image on the folder and also on the database
                await imageService.SaveImageToDirectory(imageSource, minuteImage.Name);
                dataService.MinuteImageRepository.Add(minuteImage);

                var minuteElement = new MinuteImageElement(minuteImage);
                minuteElement.Source = imageService.GetImageFromFile(minuteImage.Name);

                App.Current.Properties[AppConsts.SelectedMinuteImage] = minuteImage.Id;
                _ = App.Current.SavePropertiesAsync();

                SelectedImage = minuteElement;
            },
            log =>
            { 
                if (log != null)
                    UserDialogs.Instance.Alert(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveUser
        {
            get { if (_saveUser == null) _saveUser = new Command<string>(SaveUserExecute); return _saveUser; }
        }
        private Command<string> _saveUser;
        private void SaveUserExecute(string user)
        {
            logService.LogActionAsync(async () =>
            {
                App.Current.Properties[AppConsts.UserName] = user;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveOrganization
        {
            get { if (_saveOrganization == null) _saveOrganization = new Command<string>(SaveOrganizationExecute); return _saveOrganization; }
        }
        private Command<string> _saveOrganization;
        private void SaveOrganizationExecute(string organization)
        {
            logService.LogActionAsync(async () =>
            {
                App.Current.Properties[AppConsts.OrganizationName] = organization;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveFontFamily
        {
            get { if (_saveFontFamily == null) _saveFontFamily = new Command<string>(SaveFontFamilyExecute); return _saveFontFamily; }
        }
        private Command<string> _saveFontFamily;
        private void SaveFontFamilyExecute(string fontFamily)
        {
            logService.LogActionAsync(async () =>
            {
                App.Current.Properties[AppConsts.FontFamily] = fontFamily;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveMarginLeft
        {
            get { if (_saveMarginLeft == null) _saveMarginLeft = new Command<string>(SaveMarginLeftExecute); return _saveMarginLeft; }
        }
        private Command<string> _saveMarginLeft;
        private void SaveMarginLeftExecute(string inputMargin)
        {
            logService.LogActionAsync(async () =>
            {
                if (!double.TryParse(inputMargin, out var marginValue))
                    return;

                Settings.MarginLeft = marginValue;
                App.Current.Properties[AppConsts.MarginLeft] = marginValue;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveMarginTop
        {
            get { if (_saveMarginTop == null) _saveMarginTop = new Command<string>(SaveMarginTopExecute); return _saveMarginTop; }
        }
        private Command<string> _saveMarginTop;
        private void SaveMarginTopExecute(string inputMargin)
        {
            logService.LogActionAsync(async () =>
            {
                if (!double.TryParse(inputMargin, out var marginValue))
                    return;

                Settings.MarginTop = marginValue;
                App.Current.Properties[AppConsts.MarginTop] = marginValue;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveMarginRight
        {
            get { if (_saveMarginRight == null) _saveMarginRight = new Command<string>(SaveMarginRightExecute); return _saveMarginRight; }
        }
        private Command<string> _saveMarginRight;
        private void SaveMarginRightExecute(string inputMargin)
        {
            logService.LogActionAsync(async () =>
            {
                if (!double.TryParse(inputMargin, out var marginValue))
                    return;

                Settings.MarginRight = marginValue;
                App.Current.Properties[AppConsts.MarginRight] = marginValue;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        public Command<string> SaveMarginBottom
        {
            get { if (_saveMarginBottom == null) _saveMarginBottom = new Command<string>(SaveMarginBottomExecute); return _saveMarginBottom; }
        }
        private Command<string> _saveMarginBottom;
        private void SaveMarginBottomExecute(string inputMargin)
        {
            logService.LogActionAsync(async () =>
            {
                if (!double.TryParse(inputMargin, out var marginValue))
                    return;

                Settings.MarginBottom = marginValue;
                App.Current.Properties[AppConsts.MarginBottom] = marginValue;

                await App.Current.SavePropertiesAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        #endregion

        #region Helpers

        private void UpdateFontSize(double fontSize)
        {
            Settings.FontSize = fontSize;
        }

        private void UpdateImage(MinuteImageElement minuteImage)
        {
            SelectedImage = minuteImage;
        }

        #endregion

        #region Initializers

        public override void Initialize()
        {
            base.Initialize();

            // Regarding organization info
            var user = App.Current.Properties[AppConsts.UserName].ToString();
            var organization = App.Current.Properties[AppConsts.OrganizationName].ToString();
            var fontSize = int.Parse(App.Current.Properties[AppConsts.FontSize].ToString());
            var fontFamily = App.Current.Properties[AppConsts.FontFamily].ToString();

            Settings = new SettingsElement(new Settings
            {
                User = user,
                Organization = organization,
                FontSize = fontSize,
                FontFamily = fontFamily,
            });

            // Regarding margin info
            if (double.TryParse(App.Current.Properties[AppConsts.MarginLeft].ToString(), out var marginLeft))
                Settings.MarginLeft = marginLeft;

            if (double.TryParse(App.Current.Properties[AppConsts.MarginTop].ToString(), out var marginTop))
                Settings.MarginTop = marginTop;

            if (double.TryParse(App.Current.Properties[AppConsts.MarginRight].ToString(), out var marginRight))
                Settings.MarginRight = marginRight;

            if (double.TryParse(App.Current.Properties[AppConsts.MarginBottom].ToString(), out var marginBottom))
                Settings.MarginBottom = marginBottom;
            
            // Regarding minute image info
            imageService.CreateDirectory();

            var images = dataService.MinuteImageRepository.GetAll();
            var logoSource = ImageSource.FromFile("icLogo.png");

            var selectedImage = int.Parse(App.Current.Properties[AppConsts.SelectedMinuteImage].ToString());
            var logoImage = new MinuteImageElement(new MinuteImage { Id = 0 })
            {
                Source = logoSource
            };
            var imageCollection = new List<MinuteImageElement> { logoImage };

            imageCollection
                .AddRange(images.Select(l => new MinuteImageElement(l) 
                { 
                    Source = imageService.GetImageFromFile(l.Name) 
                }));

            SelectedImage = imageCollection.FirstOrDefault(l => l.Model.Id == selectedImage) ?? logoImage;
        }

        #endregion
    }
}
