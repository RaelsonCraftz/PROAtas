using Acr.UserDialogs;
using PROAtas.Helpers.Model;
using PROAtas.Core;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using PROAtas.Views.Dialogs;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Craftz.ViewModel;
using PROAtas.Mobile.Views.Dialogs;
using PROAtas.Core.Model;

namespace PROAtas.Mobile.ViewModel
{
    public class MinuteViewModel : BaseViewModel<Minute>
    {
        private IDataService dataService;

        public MinuteViewModel()
        {

        }

        #region Bindable Properties

        public bool HasAtLeastOneTopic => Minute?.Topics?.Count > 1;

        public TopicElement SelectedTopic
        {
            get => _selectedTopic;
            set { _selectedTopic = value; OnPropertyChanged(); }
        }
        private TopicElement _selectedTopic;

        public MinuteElement Minute
        {
            get => _minute;
            set { _minute = value; OnPropertyChanged(); }
        }
        private MinuteElement _minute;

        #endregion

        #region Commands

        public Command<string> SaveMinuteTitle
        {
            get { if (_saveMinuteTitle == null) _saveMinuteTitle = new Command<string>(SaveMinuteTitleExecute); return _saveMinuteTitle; }
        }
        private Command<string> _saveMinuteTitle;
        private void SaveMinuteTitleExecute(string minuteName)
        {
            _ = SetBusyAsync(() =>
            {
                logService.LogAction(() =>
                {
                    var minute = new Minute(Minute.Model);
                    minute.Name = minuteName;

                    dataService.MinuteRepository.Update(minute);
                },
                log => 
                { 
                    if (log != null)
                        UserDialogs.Instance.Alert(log);
                });

                return Task.CompletedTask;
            });
        }

        private bool isTopicSelectionEnabled = true;
        public Command<TopicElement> SelectTopic
        {
            get { if (_selectTopic == null) _selectTopic = new Command<TopicElement>(SelectTopicExecute); return _selectTopic; }
        }
        private Command<TopicElement> _selectTopic;
        private void SelectTopicExecute(TopicElement selectedTopic)
        {
            logService.LogAction(() =>
            {
                ChangeSelectionCommands(false);
            
                SelectedTopic = selectedTopic;

                // Unhighlight all topics
                foreach (var topic in Minute.Topics)
                    topic.IsSelected = false;

                // Highlight current topic
                selectedTopic.IsSelected = true;

                ChangeSelectionCommands(true);
            }, 
            log => 
            { 
                if (log != null)
                    UserDialogs.Instance.Alert(log);
            });
        }

        private bool isTopicIndexEnabled = true;
        public Command<TopicElement> ChangeTopic
        {
            get { if (_changeTopic == null) _changeTopic = new Command<TopicElement>(ChangeTopicExecute, c => isTopicIndexEnabled); return _changeTopic; }
        }
        private Command<TopicElement> _changeTopic;
        private void ChangeTopicExecute(TopicElement selectedTopic)
        {
            logService.LogAction(() =>
            {
                ChangeSelectionCommands(false);

                // Unhighlight all topics
                foreach (var topic in Minute.Topics)
                    topic.IsSelected = false;

                // Highlight topic of current index
                selectedTopic.IsSelected = true;
                
                ChangeSelectionCommands(true);
            },
            log => 
            { 
                if (log != null)
                    UserDialogs.Instance.Alert(log);
            });
        }

        public Command CreateTopic
        {
            get { if (_createTopic == null) _createTopic = new Command(CreateTopicExecute); return _createTopic; }
        }
        private Command _createTopic;
        private void CreateTopicExecute()
        {
            logService.LogAction(() =>
            {
                ReorderTopics();

                var order = Minute.Topics.Count + 1;
                var topic = new Topic
                {
                    IdMinute = Minute.Model.Id,
                    Order = order,
                    Text = $"Tópico {order}",
                };

                dataService.TopicRepository.Add(topic);
                Minute.Topics.Add(new TopicElement(topic));

                DeleteTopic.ChangeCanExecute();
            }, 
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Alert(log);
            });
        }

        public Command<TopicElement> DeleteTopic
        {
            get { if (_deleteTopic == null) _deleteTopic = new Command<TopicElement>(DeleteTopicExecute, p => HasAtLeastOneTopic); return _deleteTopic; }
        }
        private Command<TopicElement> _deleteTopic;
        private void DeleteTopicExecute(TopicElement topic)
        {
            logService.LogActionAsync(async () =>
            {
                if (!HasAtLeastOneTopic)
                {
                    DeleteTopic.ChangeCanExecute();
                    await UserDialogs.Instance.AlertAsync("Você não pode deletar o único tópico presente nesta ata", "Aviso", "Ok");
                    return;
                }

                if (await UserDialogs.Instance.ConfirmAsync("Esta operação removerá o tópico e todas as suas informações definitivamente. Deseja prosseguir?", "Confirmação", "Sim", "Não"))
                {
                    // Looks for a valid index of a topic to be selected afterwards
                    TopicElement nextTopic;
                    var index = Minute.Topics.IndexOf(topic);
                    if (index != 0)
                    {
                        nextTopic = Minute.Topics[index - 1];

                        // Selects next valid topic
                        SelectTopic.Execute(nextTopic);
                    }
                    else
                        nextTopic = Minute.Topics[index];
                    
                    // Delete topic
                    dataService.TopicRepository.Remove(topic.Model);

                    var informationList = topic.Information.Select(l => l.Model);
                    foreach (var info in informationList)
                        dataService.InformationRepository.Remove(info);

                    Minute.Topics.Remove(topic);
                    ReorderTopics();

                    // Selects next valid topic
                    if (index == 0)
                        SelectTopic.Execute(nextTopic);

                    DeleteTopic.ChangeCanExecute();
                }
            },
            log => 
            { 
                if (log != null)
                    UserDialogs.Instance.Alert(log);

                return Task.CompletedTask;
            });
        }

        public Command<TopicElement> CreateInformation
        {
            get { if (_createInformation == null) _createInformation = new Command<TopicElement>(CreateInformationExecute); return _createInformation; }
        }
        private Command<TopicElement> _createInformation;
        private void CreateInformationExecute(TopicElement selectedTopic)
        {
            logService.LogAction(
                // Action
                () =>
                {
                    var order = (selectedTopic.Information.LastOrDefault()?.Order ?? 0) + 1;
                    var information = new Information
                    {
                        IdTopic = selectedTopic.Model.Id,
                        Order = order,
                    };

                    dataService.InformationRepository.Add(information);
                    selectedTopic.Information.Add(new InformationElement(information));
                }, 
                // Error callback
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);
                });
        }

        public Command<InformationElement> SelectInformation
        {
            get { if (_selectInformation == null) _selectInformation = new Command<InformationElement>(SelectInformationExecute); return _selectInformation; }
        }
        private Command<InformationElement> _selectInformation;
        private void SelectInformationExecute(InformationElement information)
        {
            logService.LogActionAsync(
                // Action
                async () => 
                {
                    await PopupNavigation.Instance.PushAsync(new InformationDialog(information.Model, UpdateInformation));
                }, 
                // Error callback
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);

                    return Task.CompletedTask;
                });
        }

        public Command<InformationElement> DeleteInformation
        {
            get { if (_deleteInformation == null) _deleteInformation = new Command<InformationElement>(DeleteInformationExecute); return _deleteInformation; }
        }
        private Command<InformationElement> _deleteInformation;
        private void DeleteInformationExecute(InformationElement information)
        {
            logService.LogActionAsync(async () =>
            {
                if (await UserDialogs.Instance.ConfirmAsync("Esta operação removerá a informação definitivamente. Deseja prosseguir?", "Confirmação", "Sim", "Não"))
                {
                    dataService.InformationRepository.Remove(information.Model);

                    var topic = Minute.Topics.FirstOrDefault(l => l.Model.Id == information.Model.IdTopic);
                    topic.Information.Remove(information);
                }
            }, 
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Alert(log);

                return Task.CompletedTask;
            });
        }

        public Command OpenPeople
        {
            get { if (_openPeople == null) _openPeople = new Command(OpenPeopleExecute); return _openPeople; }
        }
        private Command _openPeople;
        private void OpenPeopleExecute()
        {
            logService.LogActionAsync(
                // Action
                async () =>
                {
                    var peopleList = dataService.PersonRepository.Find(l => l.IdMinute == Minute.Model.Id);
                    var people = new People
                    {
                        IdMinute = Minute.Model.Id,
                        PeopleList = peopleList,
                    };

                    await PopupNavigation.Instance.PushAsync(new PeopleDialog(people));
                }, 
                // Error callback
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);

                    return Task.CompletedTask;
                });
        }

        public Command OpenMoment
        {
            get { if (_openMoment == null) _openMoment = new Command(OpenMomentExecute); return _openMoment; }
        }
        private Command _openMoment;
        private void OpenMomentExecute()
        {
            logService.LogActionAsync(
                // Action
                async () =>
                {
                    var moment = new Moment
                    {
                        Date = Minute.Date,
                        Start = Minute.Start,
                        End = Minute.End,
                    };

                    await PopupNavigation.Instance.PushAsync(new MomentDialog(moment));
                },
                // Error callback
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);

                    return Task.CompletedTask;
                });
        }

        #endregion

        #region Helpers

        public void ChangeSelectionCommands(bool isEnabled)
        {
            isTopicIndexEnabled = isEnabled;
            isTopicSelectionEnabled = isEnabled;
            ChangeTopic.ChangeCanExecute();
        }

        private void UpdateInformation(Information information)
        {
            logService.LogAction(
                // Action
                () =>
                {
                    var topic = Minute.Topics.FirstOrDefault(l => l.Model.Id == information.IdTopic);
                    var info = topic.Information.FirstOrDefault(l => l.Model.Id == information.Id);

                    info.Text = information.Text;
                },
                // Error callback
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);
                });
        }

        private void ReorderTopics()
        {
            for (int i = 0; i < Minute.Topics.Count; i++)
                Minute.Topics[i].Order = i + 1;
        }

        #endregion

        #region Initializers

        public override void Initialize(Minute model)
        {
            base.Initialize(model);

            dataService = DependencyService.Get<IDataService>();

            // Download topics
            var topics = dataService.TopicRepository.GetAll().Where(l => l.IdMinute == model.Id).OrderBy(l => l.Order).ToList();
            // Download people
            var people = dataService.PersonRepository.GetAll().Where(l => l.IdMinute == model.Id).ToList();

            // Instantiate topics
            var topicElements = new List<TopicElement>();
            topics.ForEach(l => topicElements.Add(new TopicElement(l) { Order = topicElements.Count + 1 }));
            
            // Download information for each topic
            foreach (var topicElement in topicElements)
            {
                var information = dataService.InformationRepository.GetAll().Where(l => l.IdTopic == topicElement.Model.Id);
                foreach (var info in information)
                    topicElement.Information.Add(new InformationElement(info));
            }

            // Instantiate people
            var peopleElements = new List<PersonElement>();
            people.ForEach(l => peopleElements.Add(new PersonElement(l)));

            // Push all instances to the bindable properties
            Minute = new MinuteElement(model);
            foreach (var topic in topicElements)
                Minute.Topics.Add(topic);

            if (Minute.Topics.Count != 0)
                Minute.Topics.First().IsSelected = true;

            foreach (var person in peopleElements)
                Minute.People.Add(person);

            SelectTopic.ChangeCanExecute();
        }

        public override bool CanLeave()
        {
            if (IsBusy)
                UserDialogs.Instance.Toast("Por favor, espere a operação encerrar");

            return !IsBusy;
        }

        #endregion
    }
}