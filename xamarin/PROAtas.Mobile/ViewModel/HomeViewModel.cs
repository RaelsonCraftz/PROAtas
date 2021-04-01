using Acr.UserDialogs;
using Craftz.ViewModel;
using Newtonsoft.Json;
using PROAtas.Core;
using PROAtas.Services;
using PROAtas.ViewModel.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Mobile.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IDataService dataService;

        public HomeViewModel()
        {
            dataService = DependencyService.Get<IDataService>();
        }

        #region Bindable Properties

        public ObservableCollection<MinuteElement> Minutes { get; } = new ObservableCollection<MinuteElement>();

        public bool IsSelectionEmpty => !HasMinuteSelected;
        public bool HasMinuteSelected => SelectedMinute != null;
        public MinuteElement SelectedMinute
        {
            get => _selectedMinute;
            set { _selectedMinute = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasMinuteSelected)); OnPropertyChanged(nameof(IsSelectionEmpty)); }
        }
        MinuteElement _selectedMinute;

        #endregion

        #region Commands

        public Command SearchMinute
        {
            get { if (_searchMinute == null) _searchMinute = new Command(SearchMinuteExecute); return _searchMinute; }
        }
        private Command _searchMinute;
        private void SearchMinuteExecute()
        {

        }

        public Command<MinuteElement> SelectMinute
        {
            get { if (_selectMinute == null) _selectMinute = new Command<MinuteElement>(SelectMinuteExecute); return _selectMinute; }
        }
        private Command<MinuteElement> _selectMinute;
        private void SelectMinuteExecute(MinuteElement selectedMinute)
        {
            if (selectedMinute == SelectedMinute)
            {
                ClearSelection?.Execute(null);
            }
            else
            {
                // Unselect current minute
                if (SelectedMinute != null)
                    SelectedMinute.IsSelected = false;

                // Replace minute
                selectedMinute.IsSelected = true;
                SelectedMinute = selectedMinute;
            }
        }

        public Command CreateMinute
        {
            get { if (_createMinute == null) _createMinute = new Command(CreateMinuteExecute); return _createMinute; }
        }
        private Command _createMinute;
        private void CreateMinuteExecute()
        {
            _ = SetBusyAsync(async () =>
            {
                await logService.LogActionAsync(async () =>
                {
                    var organizationName = App.Current.Properties[AppConsts.OrganizationName]?.ToString() ?? "Nova Organização";

                    var minute = new Minute();
                    minute.Id = Guid.NewGuid().ToString();
                    minute.Name = $"{organizationName} {DateTime.Today.ToString(Formats.DateFormat).Replace("/", "-")}";
                    minute.Date = DateTime.Today.Date.ToString(Formats.DateFormat);
                    minute.Start = DateTime.Now.TimeOfDay.ToString(Formats.TimeFormat);
                    minute.End = DateTime.Now.TimeOfDay.ToString(Formats.TimeFormat);
                    minute.Active = true;

                    dataService.MinuteRepository.Add(minute);

                    var topic = new Topic();
                    topic.IdMinute = minute.Id;
                    topic.Order = 1;
                    topic.Text = "Tópico 1";

                    dataService.TopicRepository.Add(topic);

                    await Task.Delay(300);

                    var jsonStr = JsonConvert.SerializeObject(minute);
                    await Shell.Current.GoToAsync($"minute/?model={jsonStr}", true);
                },
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);

                    return Task.CompletedTask;
                });
            });
        }

        public Command EditMinute
        {
            get { if (_editMinute == null) _editMinute = new Command(EditMinuteExecute); return _editMinute; }
        }
        private Command _editMinute;
        private void EditMinuteExecute()
        {
            _ = SetBusyAsync(async () =>
            {
                await logService.LogActionAsync(async () =>
                {
                    await Task.Delay(300);

                    var jsonStr = JsonConvert.SerializeObject(SelectedMinute.Model);
                    await Shell.Current.GoToAsync($"minute/?model={jsonStr}", true);
                }, 
                log =>
                {
                    if (log != null)
                        UserDialogs.Instance.Alert(log);

                    return Task.CompletedTask;
                });
            });
        }

        public Command ClearSelection
        {
            get { if (_clearSelection == null) _clearSelection = new Command(ClearSelectionExecute); return _clearSelection; }
        }
        private Command _clearSelection;
        private void ClearSelectionExecute()
        {
            // Unselect all minutes
            foreach (var minute in Minutes)
                minute.IsSelected = false;

            // Clear selected minute
            SelectedMinute = null;
        }

        #endregion

        #region Helpers

        private void LoadMinutes()
        {
            var minutes = dataService.MinuteRepository.GetAll();
            var minuteCollection = new List<MinuteElement>();

            foreach (var minute in minutes)
                minuteCollection.Add(new MinuteElement(minute));

            InvokeMainThread(() =>
            {
                SelectedMinute = null;
                Minutes.Clear();
                foreach (var minute in minuteCollection.OrderByDescending(l => l.Date))
                    Minutes.Add(minute);
            });
        }

        #endregion

        #region Initializers

        public override void Initialize()
        {
            base.Initialize();

            LoadMinutes();
        }

        #endregion
    }
}
