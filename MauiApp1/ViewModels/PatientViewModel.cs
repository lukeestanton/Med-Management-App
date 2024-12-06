using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedManagementLibrary;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels;

public class PatientViewModel : INotifyPropertyChanged
{
    public Patient? model { get; set; }

    public int ID => model?.ID ?? 0;

    public string Name
    {
        get => model?.Name ?? string.Empty;
        set
        {
            if (model != null && model.Name != value)
            {
                model.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }

    public string Race
    {
        get => model?.Race ?? string.Empty;
        set
        {
            if (model != null && model.Race != value)
            {
                model.Race = value;
                NotifyPropertyChanged(nameof(Race));
            }
        }
    }

    public string Gender
    {
        get => model?.Gender ?? string.Empty;
        set
        {
            if (model != null && model.Gender != value)
            {
                model.Gender = value;
                NotifyPropertyChanged(nameof(Gender));
            }
        }
    }

    public string InsurancePlan
    {
        get => model?.InsurancePlan?.Name ?? string.Empty;
        set
        {
            if (model?.InsurancePlan != null && model.InsurancePlan.Name != value)
            {
                model.InsurancePlan.Name = value;
                NotifyPropertyChanged(nameof(InsurancePlan));
            }
        }
    }

    public Insurance? SelectedInsurancePlan
        {
            get
            {
                return model?.InsurancePlan;
            }
            set
            {
                var selectedInsurancePlan = value;
                if (model != null)
                {
                    model.InsurancePlan = selectedInsurancePlan;
                    model.InsurancePlanID = selectedInsurancePlan?.ID ?? 0;
                    NotifyPropertyChanged(nameof(SelectedInsurancePlan));
                }
            }
        }

    public ObservableCollection<Insurance> InsurancePlans
    {
        get
        {
            return new ObservableCollection<Insurance>(InsuranceManager.Current.GetAllInsurancePlans());
        }
    }

    public PatientViewModel(Patient patient)
    {
        model = patient; 
        SetupCommands();
    }

    public ICommand AddOrUpdateCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public PatientViewModel()
    {
        model = new Patient();
        AddOrUpdateCommand = new Command(DoAddOrUpdate);
        CancelCommand = new Command(DoCancel);
    }

    private void SetupCommands()
    {
        AddOrUpdateCommand = new Command(DoAddOrUpdate);
        CancelCommand = new Command(DoCancel);
    }

    private async void DoAddOrUpdate()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter a patient name.", "OK");
                return;
            }

            if (SelectedInsurancePlan == null)
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please select an Insurance Plan.", "OK");
                return;
            }

            PatientManager.Current.AddPatient(model);

            await Application.Current.MainPage.DisplayAlert("Success", "Patient saved successfully.", "OK");

            await Shell.Current.GoToAsync("//Patients");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }

    private async void DoCancel()
    {
        await Shell.Current.GoToAsync("//Patients");
    }


    public void LoadPatient(int patientId)
    {
        if (patientId > 0)
        {
            var selectedPatient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == patientId);
            if (selectedPatient != null)
            {
                model = selectedPatient;
                SelectedInsurancePlan = InsuranceManager.Current.GetAllInsurancePlans().FirstOrDefault(i => i.ID == model.InsurancePlanID);
            }
        }
        else
        {
            model = new Patient(); 
            SelectedInsurancePlan = null;
        }
        NotifyAllPropertiesChanged(); 
    }

    private void NotifyAllPropertiesChanged()
    {
        NotifyPropertyChanged(nameof(ID));
        NotifyPropertyChanged(nameof(Name));
        NotifyPropertyChanged(nameof(Race));
        NotifyPropertyChanged(nameof(Gender));
        NotifyPropertyChanged(nameof(InsurancePlan));
    }

    public void AddOrUpdate()
    {
        if (model != null)
        {
            PatientManager.Current.AddPatient(model);
        }
    }

    public Patient GetPatientModel()
    {
        return model ?? new Patient();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
