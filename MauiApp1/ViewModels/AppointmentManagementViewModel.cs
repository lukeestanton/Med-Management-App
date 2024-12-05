using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MedManagementLibrary;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class AppointmentManagementViewModel : INotifyPropertyChanged
    {
        private AppointmentManager _appMgr = AppointmentManager.Current;

        private AppointmentViewModel? _selectedAppointment;
        public AppointmentViewModel? SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (_selectedAppointment != value)
                {
                    _selectedAppointment = value;
                    NotifyPropertyChanged();
                    ((Command)EditCommand).ChangeCanExecute();
                    ((Command)DeleteCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsAppointmentSelected => SelectedAppointment != null;

        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public string Query { get; set; } = string.Empty;

        public AppointmentManagementViewModel()
        {
            Appointments = new ObservableCollection<AppointmentViewModel>();
            SetupCommands();
            Refresh();
        }

        private void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
            EditCommand = new Command(DoEdit, () => IsAppointmentSelected);
            DeleteCommand = new Command(DoDelete, () => IsAppointmentSelected);
            CancelCommand = new Command(DoCancel);
            SearchCommand = new Command(Refresh);
        }

        private async void DoAdd()
        {
            await Shell.Current.GoToAsync("//AppointmentDetails?appointmentId=0");
        }

        private async void DoEdit()
        {
            if (SelectedAppointment != null)
            {
                var selectedAppointmentId = SelectedAppointment.ID;
                await Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId}");
            }
        }

        private async void DoDelete()
        {
            if (SelectedAppointment != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Are you sure you want to delete the appointment '{SelectedAppointment.Name}'?", "Yes", "No");
                if (confirm)
                {
                    _appMgr.DeleteAppointment(SelectedAppointment.ID);
                    Refresh();
                }
            }
        }

        private async void DoCancel()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        public void Refresh()
        {
            Appointments.Clear();
            var filteredAppointments = _appMgr.GetAllAppointments()
                .Where(a => a.Name.ToUpper().Contains(Query.ToUpper()))
                .Select(a => new AppointmentViewModel(a));

            foreach (var appointment in filteredAppointments)
            {
                Appointments.Add(appointment);
            }

            NotifyPropertyChanged(nameof(Appointments));
            ((Command)EditCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }

        public void LoadAppointments()
        {
            Refresh();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}