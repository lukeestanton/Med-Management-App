using System;
using System.ComponentModel;
using MedManagementLibrary;

namespace MauiApp1.ViewModels;

public class PatientViewModel : INotifyPropertyChanged
{
    private Patient model;

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

    public PatientViewModel()
    {
        model = new Patient(); 
    }

    public PatientViewModel(Patient _model)
    {
        model = _model ?? new Patient();
    }

    public void LoadPatient(int patientId)
    {
        if (patientId > 0)
        {
            var selectedPatient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == patientId);
            if (selectedPatient != null)
            {
                model = selectedPatient;
            }
        }
        else
        {
            model = new Patient(); 
        }
        NotifyAllPropertiesChanged(); 
    }

    private void NotifyAllPropertiesChanged()
    {
        NotifyPropertyChanged(nameof(ID));
        NotifyPropertyChanged(nameof(Name));
    }

    public Patient GetPatientModel()
    {
        return model;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
