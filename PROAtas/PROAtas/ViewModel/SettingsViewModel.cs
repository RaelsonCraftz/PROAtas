using PROAtas.Core;
using PROAtas.Model;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ObservableCollection<MinuteImageElement> ImageCollection { get; } = new ObservableCollection<MinuteImageElement>();

        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set { _selectedImage = value; NotifyPropertyChanged(); }
        }
        private ImageSource _selectedImage;

        public bool IsImageDialogOpen
        {
            get => _isImageDialogOpen;
            set { _isImageDialogOpen = value; NotifyPropertyChanged(); }
        }
        private bool _isImageDialogOpen;

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

        public ICommand SaveUser => new Command<string>(p => SaveUserExecute(p));
        private void SaveUserExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.UserName] = param;
                    App.Current.SavePropertiesAsync();
                    User = param;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand SaveOrganization => new Command<string>(p => SaveOrganizationExecute(p));
        private void SaveOrganizationExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.OrganizationName] = param;
                    App.Current.SavePropertiesAsync();
                    Organization = param;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeFontSize => new Command<int>(p => ChangeFontSizeExecute(p));
        private void ChangeFontSizeExecute(int param)
        {
            if (param != 0)
            {
                var log = logService.LogAction(() =>
                {
                    if (12 <= (FontSize + param) && (FontSize + param) <= 20)
                    {
                        App.Current.Properties[Constants.FontSize] = FontSize + param;
                        App.Current.SavePropertiesAsync();
                        FontSize += param;
                    }
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeFontFamily => new Command<string>(p => ChangeFontFamilyExecute(p));
        private void ChangeFontFamilyExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    App.Current.Properties[Constants.FontFamily] = param;
                    App.Current.SavePropertiesAsync();
                    FontFamily = param;
                });
                if (log != null) toastService.ShortAlert(log);
            }
        }

        public ICommand ChangeMarginLeft => new Command<string>(p => ChangeMarginLeftExecute(p));
        private void ChangeMarginLeftExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginLeft].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
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

        public ICommand ChangeMarginTop => new Command<string>(p => ChangeMarginTopExecute(p));
        private void ChangeMarginTopExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginTop].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
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

        public ICommand ChangeMarginRight => new Command<string>(p => ChangeMarginRightExecute(p));
        private void ChangeMarginRightExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginRight].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
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

        public ICommand ChangeMarginBottom => new Command<string>(p => ChangeMarginBottomExecute(p));
        private void ChangeMarginBottomExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    double.TryParse(param, out double value);
                    double.TryParse(App.Current.Properties[Constants.MarginBottom].ToString(), out double margin);

                    if (param == "+1" || param == "-1")
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

        public ICommand ChooseCollection => new Command(() => ChooseCollectionExecute());
        private async void ChooseCollectionExecute()
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

                        var imageBytes = imageService.GetBytesFromPath(minuteImage.Name);
                        minuteImage.ImageBytes = imageBytes;
                        dataService.MinuteImageRepository.Add(minuteImage);

                        var minuteElement = new MinuteImageElement(minuteImage);

                        var memoryStream = new MemoryStream(imageBytes);
                        memoryStream.Position = 0;
                        minuteElement.Source = ImageSource.FromStream(() => memoryStream);

                        Debug.WriteLine($"Base64: {Convert.ToBase64String(imageBytes)}");
                        InvokeMainThread(() =>
                        {
                            SelectedImage = minuteElement.Source;
                            //ImageCollection.Add(minuteElement);
                        });
                    }));

                    if (log != null)
                        toastService.ShortAlert("Algo deu errado. Você precisa selecionar uma imagem!");
                }
            }
            else
                await DisplayAlert("Permissão", "Você precisa habilitar permissão de gravação para utilizar esta funcionalidade!", "OK");
        }

        public ICommand ChooseUrl => new Command(() => ChooseUrlExecute());
        private async void ChooseUrlExecute()
        {

        }

        public ICommand ChooseStorage => new Command(() => ChooseStorageExecute());
        private async void ChooseStorageExecute()
        {

        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize()
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

            User = user;
            Organization = organization;
            FontSize = fontSize;
            FontFamily = fontFamily;
            MarginLeft = marginLeft;
            MarginTop = marginTop;
            MarginRight = marginRight;
            MarginBottom = marginBottom;

            imageService.CreateDirectory();

            var imageCollection = dataService.MinuteImageRepository.GetAll();
            var logoStream = new MemoryStream(imageService.GetBytesFromLogo());
            logoStream.Position = 0;

            var logoSource = ImageSource.FromStream(() => logoStream);
            ImageCollection.Add(new MinuteImageElement(new MinuteImage { Id = 0 })
            {
                Source = logoSource,
            });

            foreach (var minuteImage in imageCollection)
            {
                var imageBytes = imageService.GetBytesFromPath(minuteImage.Name);
                var imageStream = new MemoryStream(imageBytes);
                imageStream.Position = 0;

                var imageSource = ImageSource.FromStream(() => imageStream);
                ImageCollection.Add(new MinuteImageElement(minuteImage) { Source = imageSource });
            }

            SelectedImage = ImageCollection.FirstOrDefault(l => l.Model.Id == selectedImage)?.Source;
        }

        public override void Leave()
        {

        }

        #endregion
    }
}
