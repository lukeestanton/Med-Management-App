using System;
using System.ComponentModel;
using MedManagementLibrary;

namespace MauiApp1.ViewModels;

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
