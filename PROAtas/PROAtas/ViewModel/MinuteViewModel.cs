using Craftz.ViewModel;
using PROAtas.Core;
using PROAtas.Model;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.ViewModel
{
    public class MinuteViewModel : BaseViewModel<Minute>
    {
        #region Service Container

        private readonly IDataService dataService = App.Current.dataService;
        private readonly ILogService logService = App.Current.logService;
        private readonly IToastService toastService = App.Current.toastService;

        #endregion

        #region Bindable Properties

        public Minute Minute { get; set; }

        public bool IsTimeOpen
        {
            get => _isTimeOpen;
            set { _isTimeOpen = value; NotifyPropertyChanged(); }
        }
        private bool _isTimeOpen;

        public bool IsSavingTopic
        {
            get => _isSavingTopic;
            set { _isSavingTopic = value; NotifyPropertyChanged(); }
        }
        private bool _isSavingTopic;

        public bool IsSavingInformation
        {
            get => _isSavingInformation;
            set { _isSavingInformation = value; NotifyPropertyChanged(); }
        }
        private bool _isSavingInformation;

        public bool IsSavingMinuteName
        {
            get => _isSavingMinuteName;
            set { _isSavingMinuteName = value; NotifyPropertyChanged(); }
        }
        private bool _isSavingMinuteName;

        public bool HasError
        {
            get => _hasError;
            set { _hasError = value; NotifyPropertyChanged(); }
        }
        private bool _hasError;

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; NotifyPropertyChanged(); }
        }
        private string _errorMessage;

        public string MinuteName
        {
            get => _minuteName;
            set { _minuteName = value; NotifyPropertyChanged(); }
        }
        private string _minuteName;

        public DateTime Date
        {
            get => _date;
            set { _date = value; NotifyPropertyChanged(); }
        }
        private DateTime _date;

        public TimeSpan Start
        {
            get => _start;
            set { _start = value; NotifyPropertyChanged(); }
        }
        private TimeSpan _start;

        public TimeSpan End
        {
            get => _end;
            set { _end = value; NotifyPropertyChanged(); }
        }
        private TimeSpan _end;

        public ObservableCollection<PersonElement> People
        {
            get => _people;
            set { _people = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<PersonElement> _people = new ObservableCollection<PersonElement>();

        public ObservableCollection<TopicElement> Topics
        {
            get => _topics;
            set { _topics = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<TopicElement> _topics = new ObservableCollection<TopicElement>();

        public TopicElement SelectedTopic
        {
            get => _selectedTopic;
            set { _selectedTopic = value; NotifyPropertyChanged(); }
        }
        private TopicElement _selectedTopic;

        public ObservableCollection<InformationElement> Information
        {
            get => _information;
            set { _information = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<InformationElement> _information = new ObservableCollection<InformationElement>();

        public InformationElement SelectedInformation
        {
            get => _selectedInformation;
            set { _selectedInformation = value; NotifyPropertyChanged(); }
        }
        private InformationElement _selectedInformation;

        #endregion

        #region Commanding

        public ICommand RenameMinute => new Command<string>(p => RenameMinuteExecute(p));
        private void RenameMinuteExecute(string param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    var minute = new Minute
                    {
                        Id = Minute.Id,
                        Name = param,
                        Date = Minute.Date,
                        Start = Minute.Start,
                        End = Minute.End,
                        Active = true,
                    };
                    dataService.MinuteRepository.Update(minute);

                    Minute.Name = param;
                    MinuteName = param;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand SaveDate => new Command<DateTime>(p => SaveDateExecute(p));
        private void SaveDateExecute(DateTime param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    var minute = new Minute
                    {
                        Id = Minute.Id,
                        Name = Minute.Name,
                        Date = param.ToString(Formats.DateFormat),
                        Start = Minute.Start,
                        End = Minute.End,
                        Active = true,
                    };
                    dataService.MinuteRepository.Update(minute);

                    Minute.Date = param.ToString(Formats.DateFormat);
                    Date = param;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand SaveStart => new Command<TimeSpan>(p => SaveStartExecute(p));
        private void SaveStartExecute(TimeSpan param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    var minute = new Minute
                    {
                        Id = Minute.Id,
                        Name = Minute.Name,
                        Date = Minute.Date,
                        Start = param.ToString(Formats.TimeFormat),
                        End = Minute.End,
                        Active = true,
                    };
                    dataService.MinuteRepository.Update(minute);

                    Minute.Start = param.ToString(Formats.TimeFormat);
                    Start = param;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand SaveEnd => new Command<TimeSpan>(p => SaveEndExecute(p));
        private void SaveEndExecute(TimeSpan param)
        {
            if (param != null)
            {
                var log = logService.LogAction(() =>
                {
                    var minute = new Minute
                    {
                        Id = Minute.Id,
                        Name = Minute.Name,
                        Date = Minute.Date,
                        Start = Minute.Start,
                        End = param.ToString(Formats.TimeFormat),
                        Active = true,
                    };
                    dataService.MinuteRepository.Update(minute);

                    Minute.End = param.ToString(Formats.TimeFormat);
                    End = param;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand CreatePerson => new Command(() => CreatePersonExecute());
        private void CreatePersonExecute()
        {
            var log = logService.LogAction(() =>
            {
                var person = new Person
                {
                    IdMinute = Minute.Id,
                    Name = "",
                    Order = People.Count + 1,
                };

                dataService.PersonRepository.Add(person);
                People.Add(new PersonElement(person));
            });
            if (log != null)
                toastService.ShortAlert(log);
        }

        public ICommand SavePerson => new Command<PersonElement>(p => SavePersonExecute(p));
        private void SavePersonExecute(PersonElement param)
        {
            if (param != null)
            {
                HasError = false;

                var log = logService.LogAction(() =>
                {
                    var person = param.Model;
                    person.Name = param.Name;

                    dataService.PersonRepository.Update(person);
                });
                if (log != null)
                {
                    toastService.ShortAlert(log);
                    ErrorMessage = "Houve um erro salvando a pessoa!";
                    HasError = true;
                }
            }
        }

        public ICommand DeletePerson => new Command<PersonElement>(p => DeletePersonExecute(p));
        private async void DeletePersonExecute(PersonElement param)
        {
            if (await DisplayAlert("Aviso", "Esta operação removerá esta pessoa desta ata. Deseja prosseguir?", "Sim", "Não"))
            {
                var log = logService.LogAction(() =>
                {
                    var person = param.Model;

                    // Removing the person
                    dataService.PersonRepository.Remove(person);
                    People.Remove(param);

                    for (int i = 0; i < People.Count; i++)
                    {
                        var personModel = People[i].Model;
                        personModel.Order = i + 1;
                        People[i].Order = i + 1;

                        dataService.PersonRepository.Update(personModel);
                    }
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand CreateTopic => new Command(() => CreateTopicExecute());
        private void CreateTopicExecute()
        {
            var log = logService.LogAction(() =>
            {
                var topic = new Topic
                {
                    IdMinute = Minute.Id,
                    Order = Topics.Count + 1,
                    Text = $"Novo {Topics.Count + 1}",
                };

                dataService.TopicRepository.Add(topic);
                Topics.Add(new TopicElement(topic));
            });
            if (log != null)
                toastService.ShortAlert(log);
        }

        public ICommand SelectTopic => new Command<TopicElement>(p => SelectTopicExecute(p));
        private async void SelectTopicExecute(TopicElement param)
        {
            if (param != null)
            {
                ClearTopicSelection(param);

                param.IsSelected = true;
                SelectedTopic = param;

                HasError = false;

                var log = await logService.LogActionAsync(Task.Run(() =>
                {

                    var information = dataService.InformationRepository.GetAll().Where(l => l.IdTopic == param.Model.Id).ToList();

                    var informationElements = new List<InformationElement>();
                    information.ForEach(l => informationElements.Add(new InformationElement(l)));

                    InvokeMainThread(() =>
                    {
                        Information = new ObservableCollection<InformationElement>(informationElements);
                    });
                }));
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand SaveTopicTitle => new Command<string>(p => SaveTopicTitleExecute(p));
        private void SaveTopicTitleExecute(string param)
        {
            if (SelectedTopic != null)
            {
                HasError = false;

                var log = logService.LogAction(() =>
                {
                    var topic = SelectedTopic.Model;
                    topic.Text = param;

                    dataService.TopicRepository.Update(topic);
                });
                if (log != null)
                {
                    toastService.ShortAlert(log);
                    ErrorMessage = "Houve um erro salvando o tópico!";
                    HasError = true;
                }
            }
        }

        public ICommand DeleteTopic => new Command(() => DeleteTopicExecute());
        private async void DeleteTopicExecute()
        {
            if (await DisplayAlert("Aviso", "Esta operação removerá este tópico e todas as suas informações. Deseja prosseguir?", "Sim", "Não"))
            {
                var log = logService.LogAction(() =>
                {
                    var topic = SelectedTopic.Model;

                    // Removing all information related to the topic
                    var information = dataService.InformationRepository.GetAll().Where(l => l.IdTopic == topic.Id);
                    foreach (var info in information)
                        dataService.InformationRepository.Remove(info);

                    // Removing the topic
                    dataService.TopicRepository.Remove(topic);
                    Topics.Remove(SelectedTopic);
                    SelectedTopic = null;

                    // Renaming the order of all topics
                    for (int i = 0; i < Topics.Count; i++)
                    {
                        var topicModel = Topics[i].Model;
                        topicModel.Order = i + 1;
                        Topics[i].Order = i + 1;

                        dataService.TopicRepository.Update(topicModel);
                    }
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand CreateInformation => new Command(() => CreateInformationExecute());
        private void CreateInformationExecute()
        {
            if (SelectedTopic != null)
            {
                var log = logService.LogAction(() =>
                {
                    var information = new Information()
                    {
                        IdTopic = SelectedTopic.Model.Id,
                        Order = Information.Count + 1,
                    };

                    dataService.InformationRepository.Add(information);
                    Information.Add(new InformationElement(information));
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand SelectInformation => new Command<InformationElement>(p => SelectInformationExecute(p));
        private void SelectInformationExecute(InformationElement param)
        {
            if (param != null)
            {
                HasError = false;

                var log = logService.LogAction(() =>
                {
                    SelectedInformation = param;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand SaveInformation => new Command<string>(p => SaveInformationExecute(p));
        private void SaveInformationExecute(string param)
        {
            if (SelectedInformation != null)
            {
                var log = logService.LogAction(() =>
                {
                    var information = SelectedInformation.Model;
                    information.Text = param;

                    dataService.InformationRepository.Update(information);
                    SelectedInformation.Text = param;
                });
                if (log != null)
                    toastService.ShortAlert(log);
            }
        }

        public ICommand DeleteInformation => new Command<InformationElement>(p => DeleteInformationExecute(p));
        private async void DeleteInformationExecute(InformationElement param)
        {
            if (await DisplayAlert("Aviso", "Esta operação removerá esta informação. Deseja prosseguir?", "Sim", "Não"))
            {
                var log = logService.LogAction(() =>
                {
                    dataService.InformationRepository.Remove(param.Model);
                    Information.Remove(param);
                });
            }
        }

        #endregion

        #region Helpers

        public void ClearTopicSelection(TopicElement param = null)
        {
            foreach (var topic in Topics)
                if (topic != param)
                    topic.IsSelected = false;
        }

        #endregion

        #region Initializers

        public override void Initialize(Minute model)
        {
            Task.Run(() =>
            {
                var topics = dataService.TopicRepository.GetAll().Where(l => l.IdMinute == model.Id).OrderBy(l => l.Order).ToList();
                var people = dataService.PersonRepository.GetAll().Where(l => l.IdMinute == model.Id).ToList();

                var topicElements = new List<TopicElement>();
                topics.ForEach(l => topicElements.Add(new TopicElement(l)));

                var peopleElements = new List<PersonElement>();
                people.ForEach(l => peopleElements.Add(new PersonElement(l)));

                InvokeMainThread(() =>
                {
                    Minute = model;

                    MinuteName = model.Name;
                    Date = DateTime.ParseExact(model.Date, Formats.DateFormat, CultureInfo.InvariantCulture);
                    Start = TimeSpan.ParseExact(model.Start, Formats.TimeFormat, CultureInfo.InvariantCulture);
                    End = TimeSpan.ParseExact(model.End, Formats.TimeFormat, CultureInfo.InvariantCulture);

                    Information.Clear();
                    Topics = new ObservableCollection<TopicElement>(topicElements);
                    People = new ObservableCollection<PersonElement>(peopleElements);
                });
            });
        }

        public override void Leave()
        {
            HasError = false;

            SelectedTopic = null;
            SelectedInformation = null;
        }

        #endregion
    }
}