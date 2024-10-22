using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MedManagementLibrary;

namespace MauiApp1.ViewModels {
    public class PatientManagementViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PatientViewModel? SelectedPatient { get; set; }

        public string? Query { get; set; }

        public ObservableCollection<PatientViewModel> Patients {
            get {
                var retVal = new ObservableCollection<PatientViewModel>(
                    PatientManager
                    .Current
                    .GetAllPatients()
                    .Where(p=>p != null)
                    .Where(p => p.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Take(100)
                    .Select(p => new PatientViewModel(p))
                    );

                return retVal;
            }            
        }

        public void Delete() 
        {
            if(SelectedPatient == null) {
                return;
            }
            PatientManager.Current.DeletePatient(SelectedPatient.ID);

            Refresh();
        } 

        public void Refresh() {
            NotifyPropertyChanged(nameof(Patients));
        }
    }
}

