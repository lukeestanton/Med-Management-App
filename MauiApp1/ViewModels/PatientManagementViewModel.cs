using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MedManagementLibrary;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class PatientManagementViewModel : INotifyPropertyChanged
    {
        private PatientManager _appMgr = PatientManager.Current;

        private PatientViewModel? _selectedPatient;
        public PatientViewModel? SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    NotifyPropertyChanged();
                    ((Command)EditCommand).ChangeCanExecute();
                    ((Command)DeleteCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsPatientSelected => SelectedPatient != null;

        public ObservableCollection<PatientViewModel> Patients { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public string Query { get; set; } = string.Empty;

        public PatientManagementViewModel()
        {
            Patients = new ObservableCollection<PatientViewModel>();
            SetupCommands();
            Refresh();
        }

        private void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
            EditCommand = new Command(DoEdit, () => IsPatientSelected);
            DeleteCommand = new Command(DoDelete, () => IsPatientSelected);
            CancelCommand = new Command(DoCancel);
            SearchCommand = new Command(Refresh);
        }

        private async void DoAdd()
        {
            await Shell.Current.GoToAsync("//PatientDetails?patientId=0");
        }

        private async void DoEdit()
        {
            if (SelectedPatient != null)
            {
                var selectedPatientId = SelectedPatient.ID;
                await Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
            }
        }

        private async void DoDelete()
        {
            if (SelectedPatient != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Are you sure you want to delete the patient '{SelectedPatient.Name}'?", "Yes", "No");
                if (confirm)
                {
                    _appMgr.DeletePatient(SelectedPatient.ID);
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
            Patients.Clear();
            var filteredPatients = _appMgr.GetAllPatients()
                .Where(p => p.Name.ToUpper().Contains(Query.ToUpper()))
                .Select(p => new PatientViewModel(p));

            foreach (var patient in filteredPatients)
            {
                Patients.Add(patient);
            }

            NotifyPropertyChanged(nameof(Patients));
            ((Command)EditCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }

        public void LoadPatients()
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
