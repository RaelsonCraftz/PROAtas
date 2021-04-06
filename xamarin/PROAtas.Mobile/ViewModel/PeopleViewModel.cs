using Acr.UserDialogs;
using Craftz.ViewModel;
using PROAtas.Core.Model;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.Services.Shared;
using PROAtas.ViewModel.Elements;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class PeopleViewModel : BaseViewModel<People>
    {
        private IDataService dataService;

        public PeopleViewModel()
        {

        }

        #region Bindable Properties

        public string IdMinute;

        public bool IsPeopleBusy => People.Any(l => l.IsNameSaving);

        public ObservableRangeCollection<PersonElement> People { get; } = new ObservableRangeCollection<PersonElement>();

        #endregion

        #region Commands

        public Command<PersonElement> SavePersonName
        {
            get { if (_savePersonName == null) _savePersonName = new Command<PersonElement>(SavePersonNameExecute); return _savePersonName; }
        }
        private Command<PersonElement> _savePersonName;
        private void SavePersonNameExecute(PersonElement personElement)
        {
            if (personElement != null)
                logService.LogAction(() =>
                {
                    var person = new Person(personElement.Model);

                    dataService.PersonRepository.Update(person);
                },
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);
                });
        }

        public Command CreatePerson
        {
            get { if (_createPerson == null) _createPerson = new Command(CreatePersonExecute); return _createPerson; }
        }
        private Command _createPerson;
        private void CreatePersonExecute()
        {
            logService.LogAction(() =>
            // Action
            {
                var order = (People.LastOrDefault()?.Order ?? 0) + 1;
                var person = new Person
                {
                    IdMinute = this.IdMinute,
                    Name = "Nova pessoa",
                    Order = order,
                };

                dataService.PersonRepository.Add(person);
                People.Add(new PersonElement(person));
            },
            // Error callback
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Alert(log);
            });
        }

        public Command<PersonElement> DeletePerson
        {
            get { if (_deletePerson == null) _deletePerson = new Command<PersonElement>(DeletePersonExecute); return _deletePerson; }
        }
        private Command<PersonElement> _deletePerson;
        private void DeletePersonExecute(PersonElement person)
        {
            logService.LogActionAsync(async () =>
            {
                if (await UserDialogs.Instance.ConfirmAsync("Esta operação removerá a informação definitivamente. Deseja prosseguir?", "Confirmação", "Sim", "Não"))
                {
                    dataService.PersonRepository.Remove(person.Model);
                    People.Remove(person);
                }
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Alert(log);

                return Task.CompletedTask;
            });
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize(People model = null)
        {
            base.Initialize(model);

            dataService = DependencyService.Get<IDataService>();

            IdMinute = model.IdMinute;

            People.ReplaceRange(model.PeopleList.Select(l => new PersonElement(new Person(l))));
        }

        public override bool CanLeave()
        {
            if (IsPeopleBusy)
                return false;
            else
                return base.CanLeave();
        }

        #endregion
    }
}
