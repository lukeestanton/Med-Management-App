using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MedManagementLibrary;

namespace MauiApp1.ViewModels {
    public class PatientManagementViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Patient> Patients {
            get {
                return new ObservableCollection<Patient>(PatientManager.Current.GetAllPatients());
            }            
        } 

        public void Refresh() {
            NotifyPropertyChanged("Patients");
        }
    }
}

