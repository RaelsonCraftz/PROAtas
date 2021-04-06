using Acr.UserDialogs;
using Craftz.ViewModel;
using PROAtas.Core.Model.Entities;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class InformationViewModel : BaseViewModel<Information>
    {
        private IDataService dataService;

        public InformationViewModel()
        {

        }

        #region Bindable Properties

        public InformationElement Information
        {
            get => _information;
            set { _information = value; OnPropertyChanged(); }
        }
        private InformationElement _information;

        #endregion

        #region Commands

        public Command<Action<Information>> SaveInformation
        {
            get { if (_saveInformation == null) _saveInformation = new Command<Action<Information>>(SaveInformationExecute); return _saveInformation; }
        }
        private Command<Action<Information>> _saveInformation;
        private async void SaveInformationExecute(Action<Information> onResult)
        {
            await logService.LogActionAsync(async () =>
            {
                dataService.InformationRepository.Update(Information.Model);
                onResult(Information.Model);

                await PopupNavigation.Instance.PopAsync();
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Alert(log);

                return Task.CompletedTask;
            });
        }

        public Command DiscardChanges
        {
            get { if (_discardChanges == null) _discardChanges = new Command(DiscardChangesExecute); return _discardChanges; }
        }
        private Command _discardChanges;
        private async void DiscardChangesExecute()
        {
            if (await UserDialogs.Instance.ConfirmAsync("Esta operação irá desfazer todas as alterações deste texto. Deseja prosseguir?", "Confirmação", "Sim", "Não"))
            {
                logService.LogAction(() =>
                {
                    Information.Text = Information.Original.Text;
                },
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);
                });
            }
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize(Information model = null)
        {
            base.Initialize(model);

            dataService = DependencyService.Get<IDataService>();

            Information = new InformationElement(new Information(model));
        }

        #endregion
    }
}
