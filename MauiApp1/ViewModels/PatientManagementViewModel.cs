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

        public Patient? SelectedPatient { get; set; }

        public ObservableCollection<Patient> Patients {
            get {
                return new ObservableCollection<Patient>(PatientManager.Current.GetAllPatients());
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

