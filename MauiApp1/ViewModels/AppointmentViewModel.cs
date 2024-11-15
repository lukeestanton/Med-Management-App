using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private Appointment model;

        public int ID => model.ID;

        public string Name
        {
            get => model.Name;
            set
            {
                if (model.Name != value)
                {
                    model.Name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

    private ObservableCollection<Patient> _patients;
    public ObservableCollection<Patient> Patients
    {
        get => _patients;
        set
        {
            if (_patients != value)
            {
                _patients = value;
                NotifyPropertyChanged(nameof(Patients));
            }
        }
    }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    model.PatientID = _selectedPatient?.ID ?? 0;
                    NotifyPropertyChanged(nameof(SelectedPatient));
                }
            }
        }


        public string PatientName
        {
            get
            {
                var patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == model.PatientID);
                return patient?.Name ?? "Unknown Patient";
            }
        }

        public DateTime StartTime
        {
            get => model.StartTime;
            set
            {
                if (model.StartTime != value)
                {
                    model.StartTime = value;
                    NotifyPropertyChanged(nameof(StartTime));
                }
            }
        }

        public DateTime EndTime
        {
            get => model.EndTime;
            set
            {
                if (model.EndTime != value)
                {
                    model.EndTime = value;
                    NotifyPropertyChanged(nameof(EndTime));
                }
            }
        }

        public AppointmentViewModel()
        {
            model = new Appointment();
            Patients = new ObservableCollection<Patient>(PatientManager.Current.GetAllPatients());
        }

        public AppointmentViewModel(Appointment _model)
        {
            model = _model ?? new Appointment();
            Patients = new ObservableCollection<Patient>(PatientManager.Current.GetAllPatients());
            _selectedPatient = Patients.FirstOrDefault(p => p.ID == model.PatientID);
        }

        public void LoadAppointment(int appointmentId)
        {
            Patients = new ObservableCollection<Patient>(PatientManager.Current.GetAllPatients());
            if (appointmentId > 0)
            {
                var selectedAppointment = AppointmentManager.Current.GetAllAppointments().FirstOrDefault(a => a.ID == appointmentId);
                if (selectedAppointment != null)
                {
                    model = selectedAppointment;
                    // Update SelectedPatient based on the loaded appointment
                    _selectedPatient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == model.PatientID);
                }
            }
            else
            {
                model = new Appointment();
                _selectedPatient = null;
            }
            NotifyAllPropertiesChanged();
        }

        private void NotifyAllPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(ID));
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(PatientName));
            NotifyPropertyChanged(nameof(SelectedPatient));
            NotifyPropertyChanged(nameof(StartTime));
            NotifyPropertyChanged(nameof(EndTime));
        }

        public Appointment GetAppointmentModel()
        {
            return model;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
