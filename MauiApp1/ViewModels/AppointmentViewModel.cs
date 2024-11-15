using System;
using System.ComponentModel;
using System.Linq;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private Appointment model;

        public int ID => model.ID;

        // Property to get and set the appointment's name (if needed)
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

        // New property to get the patient's name
        public string PatientName
        {
            get
            {
                var patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == model.PatientID);
                return patient?.Name ?? "Unknown Patient";
            }
        }

        // Property for StartTime
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

        // Property for EndTime
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
        }

        public AppointmentViewModel(Appointment _model)
        {
            model = _model ?? new Appointment();
        }

        public void LoadAppointment(int appointmentId)
        {
            if (appointmentId > 0)
            {
                var selectedAppointment = AppointmentManager.Current.GetAllAppointments().FirstOrDefault(a => a.ID == appointmentId);
                if (selectedAppointment != null)
                {
                    model = selectedAppointment;
                }
            }
            else
            {
                model = new Appointment();
            }
            NotifyAllPropertiesChanged();
        }

        private void NotifyAllPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(ID));
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(PatientName));
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
