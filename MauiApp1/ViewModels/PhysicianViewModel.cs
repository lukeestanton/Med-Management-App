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
    public Physician? Model { get; set; }

    public int ID => Model?.ID ?? 0;

    public string Name
    {
        get => Model?.Name ?? string.Empty;
        set
        {
            if (Model != null && Model.Name != value)
            {
                Model.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }

    public DateTime GraduationDate
    {
        get
        {
            return Model?.GraduationDate.Date ?? DateTime.Today;
        }
        set
        {
            if (Model != null)
            {
                NotifyPropertyChanged(nameof(GraduationDate));
            }
        }
    }

    public ObservableCollection<Specialization> AvailableSpecializations { get; set; }
    public ObservableCollection<Specialization> SelectedSpecializations { get; set; } = new ObservableCollection<Specialization>();

    public string Specializations => string.Join(", ", SelectedSpecializations.Select(s => s.Name));


    public ICommand AddOrUpdateCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public PhysicianViewModel()
    {
        Model = new Physician();
        AvailableSpecializations = new ObservableCollection<Specialization>(SpecializationManager.Current.GetAllSpecializations());
        SelectedSpecializations = new ObservableCollection<Specialization>();
        foreach(var specialization in AvailableSpecializations)
        {
            specialization.PropertyChanged += Specialization_PropertyChanged;
        }
        AddOrUpdateCommand = new Command(DoAddOrUpdate);
        CancelCommand = new Command(DoCancel);
    }

    private void Specialization_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(Specialization.IsSelected))
        {
            var specialization = sender as Specialization;
            if(specialization == null)
                return;

            if(specialization.IsSelected)
            {
                if(!SelectedSpecializations.Contains(specialization))
                    SelectedSpecializations.Add(specialization);
            }
            else
            {
                if(SelectedSpecializations.Contains(specialization))
                    SelectedSpecializations.Remove(specialization);
            }

            NotifyPropertyChanged(nameof(Name));
        }
    }

    public PhysicianViewModel(Physician physician)
    {
        Model = physician;
        InitializeData();
        LoadSelectedSpecializations();
        SetupCommands();
    }

    private void InitializeData()
    {
        // Initialize Available Specializations
        AvailableSpecializations = new ObservableCollection<Specialization>(SpecializationManager.Current.GetAllSpecializations());

        // Subscribe to changes in SelectedSpecializations
        SelectedSpecializations.CollectionChanged += SelectedSpecializations_CollectionChanged;
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

            if (!SelectedSpecializations.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Please select at least one specialization.", "OK");
                return;
            }

            Model.SpecializationIDs = SelectedSpecializations.Select(s => s.ID).ToList();

            // Add or update physician
            PhysicianManager.Current.AddPhysician(Model);

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

    private void SelectedSpecializations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Update the model's SpecializationIDs
        if (Model != null)
        {
            Model.SpecializationIDs = SelectedSpecializations.Select(s => s.ID).ToList();
        }
        NotifyPropertyChanged(nameof(Specializations));
    }


    public void LoadPhysician(int physicianId)
    {
        if (physicianId > 0)
        {
            var selectedPhysician = PhysicianManager.Current.GetAllPhysicians().FirstOrDefault(p => p.ID == physicianId);
            if (selectedPhysician != null)
            {
                Model = selectedPhysician;
                LoadSelectedSpecializations();
            }
        }
        else
        {
            Model = new Physician(); 
            SelectedSpecializations.Clear();
        }

        NotifyAllPropertiesChanged(); 
    }

    private void LoadSelectedSpecializations()
    {
        SelectedSpecializations.CollectionChanged -= SelectedSpecializations_CollectionChanged;
        SelectedSpecializations.Clear();
        foreach (var specialization in AvailableSpecializations.Where(s => Model?.SpecializationIDs.Contains(s.ID) ?? false))
        {
            SelectedSpecializations.Add(specialization);
        }
        SelectedSpecializations.CollectionChanged += SelectedSpecializations_CollectionChanged;

        NotifyPropertyChanged(nameof(Specializations));
    }

    private void NotifyAllPropertiesChanged()
    {
        NotifyPropertyChanged(nameof(ID));
        NotifyPropertyChanged(nameof(Name));
        NotifyPropertyChanged(nameof(GraduationDate));
    }

    public void AddOrUpdate()
        {
            if (Model != null)
            {
                PhysicianManager.Current.AddPhysician(Model);
            }
        }

    public Physician GetPhysicianModel()
    {
        return Model ?? new Physician();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
