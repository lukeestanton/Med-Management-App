using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MedManagementLibrary;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        private TreatmentManager _appMgr = TreatmentManager.Current;

        private TreatmentViewModel? _selectedTreatment;
        public TreatmentViewModel? SelectedTreatment
        {
            get => _selectedTreatment;
            set
            {
                if (_selectedTreatment != value)
                {
                    _selectedTreatment = value;
                    NotifyPropertyChanged();
                    ((Command)EditCommand).ChangeCanExecute();
                    ((Command)DeleteCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsTreatmentSelected => SelectedTreatment != null;

        public ObservableCollection<TreatmentViewModel> Treatments { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public string Query { get; set; } = string.Empty;

        public TreatmentManagementViewModel()
        {
            Treatments = new ObservableCollection<TreatmentViewModel>();
            SetupCommands();
            Refresh();
        }

        private void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
            EditCommand = new Command(DoEdit, () => IsTreatmentSelected);
            DeleteCommand = new Command(DoDelete, () => IsTreatmentSelected);
            CancelCommand = new Command(DoCancel);
            SearchCommand = new Command(Refresh);
        }

        private async void DoAdd()
        {
            await Shell.Current.GoToAsync("//TreatmentDetails?treatmentId=0");
        }

        private async void DoEdit()
        {
            if (SelectedTreatment != null)
            {
                var selectedTreatmentId = SelectedTreatment.ID;
                await Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
            }
        }

        private async void DoDelete()
        {
            if (SelectedTreatment != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Are you sure you want to delete the treatment '{SelectedTreatment.Name}'?", "Yes", "No");
                if (confirm)
                {
                    _appMgr.DeleteTreatment(SelectedTreatment.ID);
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
            Treatments.Clear();
            var filteredTreatments = _appMgr.GetAllTreatments()
                .Where(t => t.Name.ToUpper().Contains(Query.ToUpper()))
                .Select(t => new TreatmentViewModel(t));

            foreach (var treatment in filteredTreatments)
            {
                Treatments.Add(treatment);
            }

            NotifyPropertyChanged(nameof(Treatments));
            ((Command)EditCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }

        public void LoadTreatments()
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
