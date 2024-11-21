using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MedManagementLibrary;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class PhysicianManagementViewModel : INotifyPropertyChanged
    {
        private PhysicianManager _appMgr = PhysicianManager.Current;

        private PhysicianViewModel? _selectedPhysician;
        public PhysicianViewModel? SelectedPhysician
        {
            get => _selectedPhysician;
            set
            {
                if (_selectedPhysician != value)
                {
                    _selectedPhysician = value;
                    NotifyPropertyChanged();
                    ((Command)EditCommand).ChangeCanExecute();
                    ((Command)DeleteCommand).ChangeCanExecute();
                }
            }
        }

        public bool IsPhysicianSelected => SelectedPhysician != null;

        public ObservableCollection<PhysicianViewModel> Physicians { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public string Query { get; set; } = string.Empty;

        public PhysicianManagementViewModel()
        {
            Physicians = new ObservableCollection<PhysicianViewModel>();
            SetupCommands();
            Refresh();
        }

        private void SetupCommands()
        {
            AddCommand = new Command(DoAdd);
            EditCommand = new Command(DoEdit, () => IsPhysicianSelected);
            DeleteCommand = new Command(DoDelete, () => IsPhysicianSelected);
            CancelCommand = new Command(DoCancel);
            SearchCommand = new Command(Refresh);
        }

        private async void DoAdd()
        {
            await Shell.Current.GoToAsync("//PhysicianDetails?physicianId=0");
        }

        private async void DoEdit()
        {
            if (SelectedPhysician != null)
            {
                var selectedPhysicianId = SelectedPhysician.ID;
                await Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId}");
            }
        }

        private async void DoDelete()
        {
            if (SelectedPhysician != null)
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Are you sure you want to delete the physician '{SelectedPhysician.Name}'?", "Yes", "No");
                if (confirm)
                {
                    _appMgr.DeletePhysician(SelectedPhysician.ID);
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
            Physicians.Clear();
            var filteredPhysicians = _appMgr.GetAllPhysicians()
                .Where(p => p.Name.ToUpper().Contains(Query.ToUpper()))
                .Select(p => new PhysicianViewModel(p));

            foreach (var physician in filteredPhysicians)
            {
                Physicians.Add(physician);
            }

            NotifyPropertyChanged(nameof(Physicians));
            ((Command)EditCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }

        public void LoadPhysicians()
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

