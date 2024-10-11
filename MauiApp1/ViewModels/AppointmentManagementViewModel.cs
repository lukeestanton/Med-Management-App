using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class AppointmentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Appointment? SelectedAppointment { get; set; }

        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return new ObservableCollection<Appointment>(AppointmentManager.Current.GetAllAppointments());
            }
        }

        public void Delete()
        {
            if (SelectedAppointment == null)
            {
                return;
            }

            AppointmentManager.Current.DeleteAppointment(SelectedAppointment.ID);
            Refresh();  // Refresh after deletion
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Appointments));  // Refresh appointments list
        }
    }
}


