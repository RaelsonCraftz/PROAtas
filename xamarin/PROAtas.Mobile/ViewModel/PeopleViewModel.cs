using Acr.UserDialogs;
using PROAtas.Helpers.Model;
using PROAtas.Core;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Craftz.ViewModel;
using System.Threading.Tasks;

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

        public ObservableCollection<PersonElement> People { get; } = new ObservableCollection<PersonElement>();

        #endregion

        #region Commands

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

        public Command ClosePeople
        {
            get { if (_closePeople == null) _closePeople = new Command(ClosePeopleExecute); return _closePeople; }
        }
        private Command _closePeople;
        private void ClosePeopleExecute()
        {
            PopupNavigation.Instance.PopAsync();
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
            foreach (var person in model.PeopleList)
                People.Add(new PersonElement(new Person(person)));
        }

        public override bool CanLeave()
        {
            return base.CanLeave();
        }

        #endregion
    }
}
