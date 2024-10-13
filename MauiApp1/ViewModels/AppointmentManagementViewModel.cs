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

        public AppointmentViewModel? SelectedAppointment { get; set; }

        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get
            {
                return new ObservableCollection<AppointmentViewModel>(AppointmentManager.Current.GetAllAppointments().Where(a => a != null).Select(a => new AppointmentViewModel(a)));
            }
        }

        public void Delete()
        {
            if (SelectedAppointment == null)
            {
                return;
            }

            AppointmentManager.Current.DeleteAppointment(SelectedAppointment.ID);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Appointments));
        }
    }
}

