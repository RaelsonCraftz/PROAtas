using Craftz.ViewModel;
using PROAtas.Core.Model.Entities;
using PROAtas.Mobile.Services.Shared;
using PROAtas.ViewModel.Elements;
using System;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class MomentViewModel : BaseViewModel<Minute>
    {
        private IDataService dataService;

        public MomentViewModel()
        {

        }

        public Action<Minute> OnResult;

        #region Bindable Properties

        public MinuteElement Minute
        {
            get => _minute;
            set { _minute = value; OnPropertyChanged(); }
        }
        private MinuteElement _minute;

        #endregion

        #region Commands

        public Command<DateTime> SaveDate
        {
            get { if (_saveDate == null) _saveDate = new Command<DateTime>(SaveDateExecute); return _saveDate; }
        }
        private Command<DateTime> _saveDate;
        private void SaveDateExecute(DateTime date)
        {
            if (date != null)
                logService.LogAction(() =>
                {
                    dataService.MinuteRepository.Update(Minute.Model);
                });
        }

        public Command<TimeSpan> SaveStart
        {
            get { if (_saveStart == null) _saveStart = new Command<TimeSpan>(SaveStartExecute); return _saveStart; }
        }
        private Command<TimeSpan> _saveStart;
        private void SaveStartExecute(TimeSpan time)
        {
            if (time != null)
                logService.LogAction(() =>
                {
                    dataService.MinuteRepository.Update(Minute.Model);
                });
        }

        public Command<TimeSpan> SaveEnd
        {
            get { if (_saveEnd == null) _saveEnd = new Command<TimeSpan>(SaveEndExecute); return _saveEnd; }
        }
        private Command<TimeSpan> _saveEnd;
        private void SaveEndExecute(TimeSpan time)
        {
            if (time != null)
                logService.LogAction(() =>
                {
                    dataService.MinuteRepository.Update(Minute.Model);
                });
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize(Minute model)
        {
            base.Initialize();

            dataService = DependencyService.Get<IDataService>();

            Minute = new MinuteElement(model);
        }

        public override void Leave()
        {
            OnResult?.Invoke(Minute.Model);
        }

        #endregion
    }
}