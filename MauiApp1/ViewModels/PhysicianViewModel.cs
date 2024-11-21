using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedManagementLibrary;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels;

public class PhysicianViewModel : INotifyPropertyChanged
{
    public Physician? model { get; set; }

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

    

    public DateTime GraduationDate
    {
        get
        {
            return model?.GraduationDate.Date ?? DateTime.Today;
        }
        set
        {
            if (model != null)
            {
                NotifyPropertyChanged(nameof(GraduationDate));
            }
        }
    }

    public PhysicianViewModel(Physician physician)
    {
        model = physician; 
        SetupCommands();
    }

    public ICommand AddOrUpdateCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public PhysicianViewModel()
    {
        model = new Physician();
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
            // Validation
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter a physician name.", "OK");
                return;
            }

            // Add or update physician
            PhysicianManager.Current.AddPhysician(model);

            await Application.Current.MainPage.DisplayAlert("Success", "Physician saved successfully.", "OK");

            // Navigate back to Physician Management
            await Shell.Current.GoToAsync("//PhysicianManagement");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }

    private async void DoCancel()
    {
        // Navigate back to Physician Management without saving
        await Shell.Current.GoToAsync("//PhysicianManagement");
    }


    public void LoadPhysician(int physicianId)
    {
        if (physicianId > 0)
        {
            var selectedPhysician = PhysicianManager.Current.GetAllPhysicians().FirstOrDefault(p => p.ID == physicianId);
            if (selectedPhysician != null)
            {
                model = selectedPhysician;
            }
        }
        else
        {
            model = new Physician(); 
        }
        NotifyAllPropertiesChanged(); 
    }

    private void NotifyAllPropertiesChanged()
    {
        NotifyPropertyChanged(nameof(ID));
        NotifyPropertyChanged(nameof(Name));
        NotifyPropertyChanged(nameof(GraduationDate));
    }

    public void AddOrUpdate()
        {
            if (model != null)
            {
                PhysicianManager.Current.AddPhysician(model);
            }
        }

    public Physician GetPhysicianModel()
    {
        return model ?? new Physician();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
