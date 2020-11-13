using Acr.UserDialogs;
using Craftz.ViewModel;
using PROAtas.Core;
using PROAtas.Model;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Service Container

        private readonly IPermissionService permissionService = App.Current.permissionService;
        private readonly IToastService toastService = App.Current.toastService;
        private readonly IImageService imageService = App.Current.imageService;
        private readonly IDataService dataService = App.Current.dataService;
        private readonly ILogService logService = App.Current.logService;

        #endregion

        public SettingsViewModel()
        {

        }

        #region Bindable Properties

        public List<string> FontList = new List<string>
        {
            "Arial",
            "Calibri",
            "Century Gothic",
            "Courier New",
            "Times New Roman",
            "Rockwell",
            "Segoe Script",
        };

        public ObservableCollection<MinuteImageElement> ImageCollection
        {
            get => _imageCollection;
            set { _imageCollection = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<MinuteImageElement> _imageCollection;

        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set { _selectedImage = value; NotifyPropertyChanged(); }
        }
        private ImageSource _selectedImage;

        private byte[] byteImage;
        public ImageSource DownloadedImage
        {
            get => _downloadedImage;
            set { _downloadedImage = value; NotifyPropertyChanged(); SelectUrlImage.ChangeCanExecute(); }
        }
        private ImageSource _downloadedImage;

        public bool IsLocked
        {
            get => IsImageDialogOpen || IsUrlDialogOpen;
        }

        public bool IsImageDialogOpen
        {
            get => _isImageDialogOpen;
            set { _isImageDialogOpen = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(IsLocked)); }
        }
        private bool _isImageDialogOpen;

        public bool IsUrlDialogOpen
        {
            get => _isUrlDialogOpen;
            set { _isUrlDialogOpen = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(IsLocked)); }
        }
        private bool _isUrlDialogOpen;

        public bool IsSavingEnabled
        {
            get => _isSavingEnabled;
            set { _isSavingEnabled = value; NotifyPropertyChanged(); }
        }
        private bool _isSavingEnabled;

        public string User
        {
            get => _user;
            set { _user = value; NotifyPropertyChanged(); }
        }
        private string _user;

        public string Organization
        {
            get => _organization;
            set { _organization = value; NotifyPropertyChanged(); }
        }
        private string _organization;

        public string FontFamily
        {
            get => _fontFamily;
            set { _fontFamily = value; NotifyPropertyChanged(); }
        }
        private string _fontFamily;

        public int FontSize
        {
            get => _fontSize;
            set { _fontSize = value; NotifyPropertyChanged(); }
        }
        private int _fontSize;

        public double MarginLeft
        {
            get => _marginLeft;
            set { _marginLeft = value; NotifyPropertyChanged(); }
        }
        private double _marginLeft;

        public double MarginTop
        {
            get => _marginTop;
            set { _marginTop = value; NotifyPropertyChanged(); }
        }
        private double _marginTop;

        public double MarginRight
        {
            get => _marginRight;
            set { _marginRight = value; NotifyPropertyChanged(); }
        }
        private double _marginRight;

        public double MarginBottom
        {
            get => _marginBottom;
            set { _marginBottom = value; NotifyPropertyChanged(); }
        }
        private double _marginBottom;

        #endregion

        #region Commading

        public Command SaveUser
        {
            get { if (_saveUser == null) _saveUser = new Command<string>(SaveUserExecute); return _saveUser; }
        }
        private Command _saveUser;
        private void SaveUserExecute(string user)
        {
            if (user != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.UserName] = user;
                    App.Current.SavePropertiesAsync();
                    User = user;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command SaveOrganization
        {
            get { if (_saveOrganization == null) _saveOrganization = new Command<string>(SaveOrganizationExecute); return _saveOrganization; }
        }
        private Command _saveOrganization;
        private void SaveOrganizationExecute(string organization)
        {
            if (organization != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.OrganizationName] = organization;
                    App.Current.SavePropertiesAsync();
                    Organization = organization;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command ChangeFontSize
        {
            get { if (_changeFontSize == null) _changeFontSize = new Command<int>(ChangeFontSizeExecute); return _changeFontSize; }
        }
        private Command _changeFontSize;
        private void ChangeFontSizeExecute(int fontSize)
        {
            if (fontSize != 0)
            {
                var log = logService.LogAction(() =>
                {
                    if (12 <= (FontSize + fontSize) && (FontSize + fontSize) <= 20)
                    {
                        App.Current.Properties[Constants.FontSize] = FontSize + fontSize;
                        App.Current.SavePropertiesAsync();
                        FontSize += fontSize;
                    }
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command ChangeFontFamily
        {
            get { if (_changeFontFamily == null) _changeFontFamily = new Command<string>(ChangeFontFamilyExecute); return _changeFontFamily; }
        }
        private Command _changeFontFamily;
        private void ChangeFontFamilyExecute(string fontFamily)
        {
            if (fontFamily != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.FontFamily] = fontFamily;
                    App.Current.SavePropertiesAsync();
                    FontFamily = fontFamily;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command ChangeMarginLeft
        {
            get { if (_changeMarginLeft == null) _changeMarginLeft = new Command<string>(ChangeMarginLeftExecute); return _changeMarginLeft; }
        }
        private Command _changeMarginLeft;
        private void ChangeMarginLeftExecute(string left)
        {
            if (left != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(left, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginLeft].ToString(), out double margin);

                    if (left == "+1" || left == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginLeft] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginLeft = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command ChangeMarginTop
        {
            get { if (_changeMarginTop == null) _changeMarginTop = new Command<string>(ChangeMarginTopExecute); return _changeMarginTop; }
        }
        private Command _changeMarginTop;
        private void ChangeMarginTopExecute(string top)
        {
            if (top != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(top, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginTop].ToString(), out double margin);

                    if (top == "+1" || top == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginTop] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginTop = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command ChangeMarginRight
        {
            get { if (_changeMarginRight == null) _changeMarginRight = new Command<string>(ChangeMarginRightExecute); return _changeMarginRight; }
        }
        private Command _changeMarginRight;
        private void ChangeMarginRightExecute(string right)
        {
            if (right != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(right, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginRight].ToString(), out double margin);

                    if (right == "+1" || right == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginRight] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginRight = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command ChangeMarginBottom
        {
            get { if (_changeMarginBottom == null) _changeMarginBottom = new Command<string>(ChangeMarginBottomExecute); return _changeMarginBottom; }
        }
        private Command _changeMarginBottom;
        private void ChangeMarginBottomExecute(string bottom)
        {
            if (bottom != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(bottom, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginBottom].ToString(), out double margin);

                    if (bottom == "+1" || bottom == "-1")
                        margin += value;
                    else
                        margin = value;

                    App.Current.Properties[Constants.MarginBottom] = margin.ToString();
                    App.Current.SavePropertiesAsync();
                    MarginBottom = margin;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public Command OpenGallery
        {
            get { if (_openGallery == null) _openGallery = new Command(OpenGalleryExecute); return _openGallery; }
        }
        private Command _openGallery;
        private async void OpenGalleryExecute()
        {
            if (await permissionService.RequestStoragePermission())
            {
                var stream = await imageService.GetImageFromGalleryAsync();
                if (stream != null)
                {
                    var log = await logService.LogActionAsync(Task.Run(async () =>
                    {
                        var imageStream = new MemoryStream(imageService.GetBytesFromStream(stream));
                        imageStream.Position = 0;
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

                        App.Current.Properties[Constants.SelectedMinuteImage] = minuteImage.Id;
                        _ = App.Current.SavePropertiesAsync();

                        InvokeMainThread(() =>
                        {
                            SelectedImage = minuteElement.Source;
                            ImageCollection.Add(minuteElement);
                        });
                    }));

                    if (log != null)
                        toastService.ShortAlert("Algo deu errado. Você precisa selecionar uma imagem!");
                }
            }
            else
                await DisplayAlert("Permissão", "Você precisa habilitar permissão de gravação para utilizar esta funcionalidade!", "OK");
        }

        public Command OpenUrl
        {
            get { if (_openUrl == null) _openUrl = new Command(OpenUrlExecute); return _openUrl; }
        }
        private Command _openUrl;
        private void OpenUrlExecute() => IsUrlDialogOpen = true;

        public Command DownloadUrl
        {
            get { if (_downloadUrl == null) _downloadUrl = new Command<byte[]>(DownloadUrlExecute); return _downloadUrl; }
        }
        private Command _downloadUrl;
        private void DownloadUrlExecute(byte[] bytes)
        {
            if (bytes != null)
            {
                byteImage = bytes;
                DownloadedImage = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }

        public Command SelectUrlImage
        {
            get { if (_selectUrlImage == null) _selectUrlImage = new Command(SelectUrlImageExecute, () => DownloadedImage != null); return _selectUrlImage; }
        }
        private Command _selectUrlImage;
        private async void SelectUrlImageExecute()
        {
            if (await permissionService.RequestStoragePermission())
            {
                using (var dialog = UserDialogs.Instance.Loading("Carregando...", maskType: MaskType.Black))
                {
                    var log = await logService.LogActionAsync(Task.Run(async () =>
                    {
                        var minuteImage = new MinuteImage()
                        {
                            Name = Guid.NewGuid().ToString(),
                        };

                        // Persisting the image on the folder and also on the database
                        await imageService.SaveImageToDirectory(ImageSource.FromStream(() => new MemoryStream(byteImage)), minuteImage.Name);
                        dataService.MinuteImageRepository.Add(minuteImage);

                        App.Current.Properties[Constants.SelectedMinuteImage] = minuteImage.Id;
                        _ = App.Current.SavePropertiesAsync();

                        var imageElement = new MinuteImageElement(minuteImage) { Source = imageService.GetImageFromFile(minuteImage.Name) };

                        InvokeMainThread(() =>
                        {
                            ImageCollection.Add(imageElement);
                            SelectedImage = imageElement.Source;
                            DownloadedImage = null;

                            IsUrlDialogOpen = false;
                        });
                    }));
                    if (log != null)
                        toastService.ShortAlert(log);
                }
            }
            else
                await DisplayAlert("Permissão", "Você precisa habilitar permissão de gravação para utilizar esta funcionalidade!", "OK");
        }

        public Command OpenStorage
        {
            get { if (_openStorage == null) _openStorage = new Command(OpenStorageExecute); return _openStorage; }
        }
        private Command _openStorage;
        private void OpenStorageExecute() => IsImageDialogOpen = true;

        public Command SelectCollection
        {
            get { if (_selectCollection == null) _selectCollection = new Command<MinuteImageElement>(SelectCollectionExecute); return _selectCollection; }
        }
        private Command _selectCollection;
        private void SelectCollectionExecute(MinuteImageElement image)
        {
            if (image != null)
            {
                IsImageDialogOpen = false;

                SelectedImage = image.Source;
                App.Current.Properties[Constants.SelectedMinuteImage] = image.Model.Id;
                _ = App.Current.SavePropertiesAsync();
            }
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize()
        {
            IsSavingEnabled = false;

            Task.Run(() =>
            {
                var user = App.Current.Properties[Constants.UserName].ToString();
                var organization = App.Current.Properties[Constants.OrganizationName].ToString();
                var fontSize = int.Parse(App.Current.Properties[Constants.FontSize].ToString());
                var fontFamily = App.Current.Properties[Constants.FontFamily].ToString();
                var marginLeft = double.Parse(App.Current.Properties[Constants.MarginLeft].ToString());
                var marginTop = double.Parse(App.Current.Properties[Constants.MarginTop].ToString());
                var marginRight = double.Parse(App.Current.Properties[Constants.MarginRight].ToString());
                var marginBottom = double.Parse(App.Current.Properties[Constants.MarginBottom].ToString());
                var selectedImage = int.Parse(App.Current.Properties[Constants.SelectedMinuteImage].ToString());

                imageService.CreateDirectory();

                var images = dataService.MinuteImageRepository.GetAll();
                var logoSource = ImageSource.FromFile("icLogo.png");

                var imageCollection = new List<MinuteImageElement>();
                imageCollection.Add(new MinuteImageElement(new MinuteImage { Id = 0 })
                {
                    Source = logoSource,
                });

                foreach (var minuteImage in images)
                {
                    var imageSource = imageService.GetImageFromFile(minuteImage.Name);
                    imageCollection.Add(new MinuteImageElement(minuteImage) { Source = imageSource });
                }

                InvokeMainThread(() =>
                {
                    User = user;
                    Organization = organization;
                    FontSize = fontSize;
                    FontFamily = fontFamily;
                    MarginLeft = marginLeft;
                    MarginTop = marginTop;
                    MarginRight = marginRight;
                    MarginBottom = marginBottom;

                    ImageCollection = new ObservableCollection<MinuteImageElement>(imageCollection);
                    SelectedImage = imageCollection.FirstOrDefault(l => l.Model.Id == selectedImage)?.Source;

                    IsSavingEnabled = true;
                });
            });
        }

        public override void Leave()
        {
            
        }

        #endregion
    }
}