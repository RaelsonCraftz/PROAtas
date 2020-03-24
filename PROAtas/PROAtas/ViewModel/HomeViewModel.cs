using Newtonsoft.Json;
using PROAtas.Core;
using PROAtas.Model;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using PROAtas.Views.Pages;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        #region Service Container

        private readonly IPermissionService permissionService = App.Current.permissionService;
        private readonly IToastService toastService = App.Current.toastService;
        private readonly IPrintService printService = App.Current.printService;
        private readonly IImageService imageService = App.Current.imageService;
        private readonly IDataService dataService = App.Current.dataService;
        private readonly ILogService logService = App.Current.logService;
        private readonly IAdService adService = App.Current.adService;

        #endregion

        public HomeViewModel()
        {

        }

        private bool IsRewarded;

        #region Bindable Properties

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; NotifyPropertyChanged(); }
        }
        private bool _isLoading;

        public List<Minute> BaseMinutes { get; } = new List<Minute>();
        public ObservableCollection<MinuteElement> Minutes { get; } = new ObservableCollection<MinuteElement>();

        public MinuteElement SelectedMinute
        {
            get => _selectedMinute;
            set { _selectedMinute = value; NotifyPropertyChanged(); }
        }
        private MinuteElement _selectedMinute;

        #endregion

        #region Commanding

        public ICommand SearchMinute => new Command<string>(p => SearchMinuteExecute(p));
        private void SearchMinuteExecute(string param)
        {
            if (param != null)
            {
                Minutes.Clear();

                foreach (var minute in BaseMinutes)
                    if (minute.Name.ToLower().Contains(param.ToLower()))
                        Minutes.Insert(0, new MinuteElement(minute));
            }
        }

        public ICommand CreateMinute => new Command(() => CreateMinuteExecute());
        private void CreateMinuteExecute()
        {
            IsLoading = true;
            Task.Run(() =>
            {
                var log = logService.LogAction(() =>
                {
                    var organizationName = App.Current.Properties[Constants.OrganizationName]?.ToString() ?? "Nova Organização";

                    var minute = new Minute();
                    minute.Name = $"{organizationName} {DateTime.Today.ToString(Formats.DateFormat)}";
                    minute.Date = DateTime.Today.Date.ToString(Formats.DateFormat);
                    minute.Start = DateTime.Now.TimeOfDay.ToString(Formats.TimeFormat);
                    minute.End = DateTime.Now.TimeOfDay.ToString(Formats.TimeFormat);
                    minute.Active = true;

                    dataService.MinuteRepository.Add(minute);

                    var jsonStr = JsonConvert.SerializeObject(minute);
                    InvokeMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync($"{nameof(MinutePage)}/?Model={jsonStr}", true);
                    });
                });
                if (log != null) 
                    InvokeMainThread(() => toastService.ShortAlert(log));

                InvokeMainThread(() => IsLoading = false);
            });
        }

        public ICommand EditMinute => new Command(() => EditMinuteExecute());
        private void EditMinuteExecute()
        {
            IsLoading = true;
            Task.Run(() =>
            {
                var log = logService.LogAction(() =>
                {
                    var minute = new Minute();
                    minute.Id = SelectedMinute.Model.Id;
                    minute.Name = SelectedMinute.Name;
                    minute.Date = SelectedMinute.Date.ToString(Formats.DateFormat);
                    minute.Start = SelectedMinute.Start.ToString(Formats.TimeFormat);
                    minute.End = SelectedMinute.End.ToString(Formats.TimeFormat);
                    minute.Active = true;

                    SelectedMinute = null;

                    var jsonStr = JsonConvert.SerializeObject(minute);
                    InvokeMainThread(async () =>
                    {
                        await Shell.Current.GoToAsync($"{nameof(MinutePage)}/?Model={jsonStr}", true);
                    });
                });
                if (log != null) 
                    InvokeMainThread(() => toastService.ShortAlert(log));

                InvokeMainThread(() => IsLoading = false);
            });
        }

        public ICommand PrintWord => new Command(() => PrintWordExecute());
        private async void PrintWordExecute()
        {
            if (await permissionService.RequestStoragePermission())
            {
                IsLoading = true;
                _ = Task.Run(() =>
                {
                    var log = logService.LogAction(() =>
                    {
                        byte[] localbyte;
                        if (Application.Current.Properties[Constants.SelectedMinuteImage]?.ToString() != "0")
                        {
                            var selectedImage = int.Parse(Application.Current.Properties[Constants.SelectedMinuteImage]?.ToString());
                            var minuteImage = dataService.MinuteImageRepository.Get(selectedImage);
                            minuteImage.ImageBytes = imageService.GetBytesFromPath(minuteImage.Name);

                            localbyte = minuteImage.ImageBytes;
                        }
                        else
                            localbyte = imageService.GetBytesFromLogo();

                        WordDocument localDocument = CreateDocument(SelectedMinute.Model, localbyte);

                        MemoryStream stream = new MemoryStream();
                        stream.Position = 0;

                        localDocument.Save(stream, Syncfusion.DocIO.FormatType.Docx);

                        localDocument.Close();

                        var arquivonome = SelectedMinute.Model.Name
                            .Replace("&", " ")
                            .Replace(@"""", "-")
                            .Replace("?", "")
                            .Replace("<", "-")
                            .Replace(">", "-")
                            .Replace("#", "")
                            .Replace("{", "(")
                            .Replace("}", ")")
                            .Replace("%", " ")
                            .Replace("~", "-")
                            .Replace("/", "-")
                            .Replace(@"\", "-");
                        printService.Print(arquivonome + ".docx", "application/msword", stream);

                    });
                    InvokeMainThread(() =>
                    {
                        if (log != null)
                            toastService.ShortAlert(log);

                        SelectedMinute = null;
                        IsLoading = false;
                    });
                });
            }
            else
                await DisplayAlert("Permissão", "Você precisa habilitar permissão de gravação para utilizar esta funcionalidade!", "OK");
        }

        public ICommand PrintPDF => new Command(() => PrintPDFExecute());
        private async void PrintPDFExecute()
        {
            if (await permissionService.RequestStoragePermission())
            {
                if (await DisplayAlert("Aviso", "Para gerar um PDF você precisará ver um vídeo antes. Isto ajuda a financiar este aplicativo. Você concorda?\r\n\r\nREQUER INTERNET", "Sim", "Não"))
                {
                    IsLoading = true;
                    IsRewarded = false;
                    adService.ShowVideo(
                        // Callback for success
                        () =>
                        {
                            IsRewarded = true;
                        },
                        // Callback for ad close
                        () =>
                        {
                            if (IsRewarded)
                            {
                                byte[] localbyte;
                                if (Application.Current.Properties[Constants.SelectedMinuteImage]?.ToString() != "0")
                                {
                                    var selectedImage = int.Parse(Application.Current.Properties[Constants.SelectedMinuteImage]?.ToString());
                                    var minuteImage = dataService.MinuteImageRepository.Get(selectedImage);
                                    minuteImage.ImageBytes = imageService.GetBytesFromPath(minuteImage.Name);

                                    localbyte = minuteImage.ImageBytes;
                                }
                                else
                                    localbyte = imageService.GetBytesFromLogo();

                                WordDocument localWord = CreateDocument(SelectedMinute.Model, localbyte, true);

                                DocIORenderer render = new DocIORenderer();

                                PdfDocument localPDF = render.ConvertToPDF(localWord);

                                render.Dispose();

                                localWord.Dispose();

                                MemoryStream arquivostream = new MemoryStream();
                                arquivostream.Position = 0;

                                localPDF.Save(arquivostream);

                                localPDF.Close();

                                var fileName = SelectedMinute.Model.Name
                                    .Replace("&", " ")
                                    .Replace(@"""", "-")
                                    .Replace("?", "")
                                    .Replace("<", "-")
                                    .Replace(">", "-")
                                    .Replace("#", "")
                                    .Replace("{", "(")
                                    .Replace("}", ")")
                                    .Replace("%", " ")
                                    .Replace("~", "-")
                                    .Replace("/", "-")
                                    .Replace(@"\", "-");
                                printService.Print(fileName + ".pdf", "application/msword", arquivostream);
                            }
                            SelectedMinute = null;
                            IsRewarded = false;
                            IsLoading = false;
                        },
                        // Callback for failure
                        () =>
                        {
                            IsLoading = false;
                            toastService.ShortAlert("Conexão falhou. Verifique a internet!");
                        }, Constants.AdVideo);
                }
            }
            else
                await DisplayAlert("Permissão", "Você precisa habilitar permissão de gravação para utilizar esta funcionalidade!", "OK");
        }

        public ICommand DeleteMinute => new Command(() => DeleteMinuteExecute());
        private async void DeleteMinuteExecute()
        {
            if (await DisplayAlert("Aviso", "Esta operação desativará esta ata. Deseja prosseguir?", "Sim", "Não"))
            {
                var log = logService.LogAction(() =>
                {
                    var minute = SelectedMinute.Model;

                    // Deactivating the minute
                    minute.Active = false;
                    dataService.MinuteRepository.Update(minute);
                    BaseMinutes.Remove(BaseMinutes.FirstOrDefault(l => l.Id == minute.Id));
                    Minutes.Remove(SelectedMinute);

                    SelectedMinute = null;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        #endregion

        #region Helpers

        WordDocument CreateDocument(Minute minute, byte[] localbyte, bool isPDF = false)
        {
            var userName = App.Current.Properties[Constants.UserName]?.ToString();
            var organizationName = App.Current.Properties[Constants.OrganizationName]?.ToString();
            var fontFamily = App.Current.Properties[Constants.FontFamily]?.ToString();
            var fontSize = int.Parse(App.Current.Properties[Constants.FontSize]?.ToString());

            var marginLeft = int.Parse(App.Current.Properties[Constants.MarginLeft]?.ToString());
            var marginTop = int.Parse(App.Current.Properties[Constants.MarginTop]?.ToString());
            var marginRight = int.Parse(App.Current.Properties[Constants.MarginRight]?.ToString());
            var marginBottom = int.Parse(App.Current.Properties[Constants.MarginBottom]?.ToString());

            WordDocument localDocument = new WordDocument();
            
            IWSection localSection = localDocument.AddSection();
            localSection.PageSetup.Margins = new MarginsF(marginLeft * 28.35f, marginTop * 28.35f, marginRight * 28.35f, marginBottom * 28.35f);

            IWParagraph localParagraph1 = localSection.AddParagraph();
            localParagraph1.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;

            IWPicture localpicture = localParagraph1.AppendPicture(localbyte);
            localpicture.HeightScale = 20;
            localpicture.WidthScale = 20;

            localSection.AddParagraph();

            IWParagraph localParagraph2 = localSection.AddParagraph();
            localParagraph2.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            IWTextRange titleText = localParagraph2.AppendText("ATA DE REUNIÃO" + " - " + organizationName);

            titleText.CharacterFormat.TextColor = Syncfusion.Drawing.Color.ForestGreen;
            titleText.CharacterFormat.Font = new Syncfusion.Drawing.Font(fontFamily, fontSize + 4);

            localSection.AddParagraph();

            // People List
            var people = string.Empty;
            var personList = dataService.PersonRepository.Find(l => l.IdMinute == minute.Id)?.ToList() ?? new List<Person>();
            for (int i = 0; i < personList.Count; i++)
            {
                if (i == 0) people = personList[i].Name;
                else people += ($", {personList[i].Name}");
            }

            // Justifying the paragraph
            IWParagraph localParagraph3 = localSection.AddParagraph();
            localParagraph3.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Justify;

            IWTextRange textoDescricao = localParagraph3.AppendText($"Reunião da Organização {organizationName}, realizada no dia {minute.Date} às {minute.Start} com a presença de {people}");
            textoDescricao.CharacterFormat.Font = new Syncfusion.Drawing.Font(fontFamily, fontSize);

            localSection.AddParagraph();

            // Justifying the paragraph
            IWParagraph localParagraph4 = localSection.AddParagraph();
            localParagraph4.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Justify;

            // Paragraph header
            IWTextRange subDescriptionText = localParagraph4.AppendText("A seguinte pauta foi discutida:");
            subDescriptionText.CharacterFormat.Font = new Syncfusion.Drawing.Font(fontFamily, fontSize);

            // Topic and Information List
            var topics = dataService.TopicRepository.Find(l => l.IdMinute == minute.Id)?.ToList() ?? new List<Topic>();
            for (int i = 0; i < topics.Count; i++)
            {
                localSection.AddParagraph();

                // Paragraph Format
                IWParagraph localParagraphTopic = localSection.AddParagraph();
                localParagraphTopic.ListFormat.ApplyDefNumberedStyle();
                localParagraphTopic.ListFormat.ContinueListNumbering();
                localParagraphTopic.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Justify;

                // Topic Text
                IWTextRange topicText = localParagraphTopic.AppendText($"{topics[i].Text}:");
                topicText.CharacterFormat.Font = new Syncfusion.Drawing.Font(fontFamily, fontSize);
                topicText.CharacterFormat.Bold = true;

                // Information List
                var information = dataService.InformationRepository.Find(l => l.IdTopic == topics[i].Id)?.ToList() ?? new List<Information>();
                foreach (var info in information)
                {
                    // Checking if this information is not empty
                    if (!string.IsNullOrEmpty(info.Text?.Replace("\n\n", string.Empty).Trim()))
                    {
                        IWParagraph localParagraphInformation = localSection.AddParagraph();
                        localParagraphInformation.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Justify;
                        localParagraphInformation.ListFormat.ApplyDefBulletStyle();

                        // Information Text
                        IWTextRange informationText;
                        // If the text has any line breaks, remove them
                        info.Text = info.Text?.Replace("\n\n", " ");

                        // Checking if the output is PDF
                        if (isPDF)
                            informationText = localParagraphInformation.AppendText($"- {info.Text};");
                        else
                            informationText = localParagraphInformation.AppendText($"{info.Text};");
                        
                        // Text Font
                        informationText.CharacterFormat.Font = new Syncfusion.Drawing.Font(fontFamily, fontSize);
                    }
                }
            }

            localSection.AddParagraph();

            IWParagraph localParagraph5 = localSection.AddParagraph();
            localParagraph5.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Justify;

            IWTextRange localText = localParagraph5.AppendText($"Finalizada às {minute.End}. Gerada por {userName}.");
            localText.CharacterFormat.FontSize = fontSize;
            localText.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Black;
            localText.CharacterFormat.Font = new Syncfusion.Drawing.Font(fontFamily, fontSize);

            return localDocument;
        }

        #endregion

        #region Initializers

        public override void Initialize()
        {
            var minutes = dataService.MinuteRepository.GetAll()?.Where(l => l.Active) ?? new List<Minute>();
            var people = dataService.PersonRepository.GetAll();

            BaseMinutes.Clear();
            Minutes.Clear();
            foreach (var minute in minutes)
            {
                BaseMinutes.Add(minute);

                minute.PeopleQuantity = people.Where(l => l.IdMinute == minute.Id)?.Count() ?? 0;

                Minutes.Insert(0, new MinuteElement(minute));
            }

            
        }

        public override void Leave()
        {

        }

        #endregion
    }
}