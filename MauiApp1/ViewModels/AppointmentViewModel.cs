using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedManagementLibrary;
using MedManagementLibrary.DTO;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        public Appointment? Model { get; set; }

        public int ID => Model?.ID ?? 0;

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set
            {
                if (Model != null && Model.Name != value)
                {
                    Model.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PatientName
        {
            get
            {
                if (Model != null && Model.PatientID > 0)
                {
                    if (Model.Patient == null)
                    {
                        Model.Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == Model.PatientID);
                    }
                }

                return Model?.Patient?.Name ?? string.Empty;
            }
        }

        public Patient? SelectedPatient
        {
            get
            {
                return Model?.Patient;
            }
            set
            {
                var selectedPatient = value;
                if (Model != null)
                {
                    Model.Patient = selectedPatient;
                    Model.PatientID = selectedPatient?.ID ?? 0;
                    NotifyPropertyChanged(nameof(SelectedPatient));
                    NotifyPropertyChanged(nameof(PatientInsurance));
                    NotifyPropertyChanged(nameof(InsurancePrice));
                }
            }
        }

        public ObservableCollection<Patient> Patients
        {
            get
            {
                return new ObservableCollection<Patient>(PatientManager.Current.GetAllPatients());
            }
        }



        public string PhysicianName
        {
            get
            {
                if(Model != null && Model.PhysicianId > 0)
                {
                    if(Model.Physician == null)
                    {
                        Model.Physician = PhysicianServiceProxy
                            .Current
                            .Physicians
                            .FirstOrDefault(p => p.Id == Model.PhysicianId);
                    }
                }

                return Model?.Physician?.Name ?? string.Empty;
            }
        }

        public PhysicianDTO? SelectedPhysician { 
            get
            {
                return Model?.Physician;
            }

            set
            {
                var selectedPhysician = value;
                if(Model != null)
                {
                    Model.Physician = selectedPhysician;
                    Model.PhysicianId = selectedPhysician?.Id ?? 0;
                }

            }
         }
        public ObservableCollection<PhysicianDTO> Physicians { 
            get
            {
                return new ObservableCollection<PhysicianDTO>(PhysicianServiceProxy.Current.Physicians);
            }
        }


  
        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public void RefreshTime()
        {
            if (Model != null)
            {
                if (Model.StartTime != DateTime.MinValue)
                {
                    Model.StartTime = StartDate.Add(StartTime);
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                return Model?.StartTime.Date ?? DateTime.Today;
            }
            set
            {
                if (Model != null)
                {
                    Model.StartTime = value.Add(StartTime);
                    RefreshTime();
                    NotifyPropertyChanged(nameof(StartDate));
                    NotifyPropertyChanged(nameof(StartTime));
                }
            }
        }

        public TimeSpan StartTime
        {
            get
            {
                return Model?.StartTime.TimeOfDay ?? TimeSpan.Zero;
            }
            set
            {
                if (Model != null)
                {
                    Model.StartTime = Model.StartTime.Date.Add(value);
                    NotifyPropertyChanged(nameof(StartTime));
                    NotifyPropertyChanged(nameof(StartTimeDisplay));
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return Model?.EndTime.Date ?? DateTime.Today;
            }
            set
            {
                if (Model != null)
                {
                    Model.EndTime = value.Add(EndTime);
                    NotifyPropertyChanged(nameof(EndDate));
                    NotifyPropertyChanged(nameof(EndTime));
                }
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return Model?.EndTime.TimeOfDay ?? TimeSpan.Zero;
            }
            set
            {
                if (Model != null)
                {
                    Model.EndTime = Model.EndTime.Date.Add(value);
                    NotifyPropertyChanged(nameof(EndTime));
                    NotifyPropertyChanged(nameof(EndTimeDisplay));
                }
            }
        }

        public string StartTimeDisplay => StartTime.ToString(@"hh\:mm tt");
        public string EndTimeDisplay => EndTime.ToString(@"hh\:mm tt");

        public ObservableCollection<Treatment> AvailableTreatments { get; set; }
        public ObservableCollection<Treatment> SelectedTreatments { get; set; } = new ObservableCollection<Treatment>();

        public string Treatments => string.Join(", ", SelectedTreatments.Select(t => t.Name));

        public decimal Price => SelectedTreatments.Sum(t => t.Price);
        public decimal InsurancePrice
        {
            get
            {
                decimal coverage = PatientInsurance?.CoveragePercentage ?? 0;
                return Price * (1 - coverage);
            }
        }

        public Insurance? PatientInsurance => SelectedPatient?.InsurancePlan;

        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AppointmentViewModel()
        {
            Model = new Appointment();
            AvailableTreatments = new ObservableCollection<Treatment>(TreatmentManager.Current.GetAllTreatments());
            SelectedTreatments = new ObservableCollection<Treatment>();
            foreach(var treatment in AvailableTreatments)
            {
                treatment.PropertyChanged += Treatment_PropertyChanged;
            }
            AddOrUpdateCommand = new Command(DoAddOrUpdate);
            CancelCommand = new Command(DoCancel);
        }

        private void Treatment_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Treatment.IsSelected))
            {
                var treatment = sender as Treatment;
                if(treatment == null)
                    return;

                if(treatment.IsSelected)
                {
                    if(!SelectedTreatments.Contains(treatment))
                        SelectedTreatments.Add(treatment);
                }
                else
                {
                    if(SelectedTreatments.Contains(treatment))
                        SelectedTreatments.Remove(treatment);
                }

                NotifyPropertyChanged(nameof(Price));
                NotifyPropertyChanged(nameof(InsurancePrice));
            }
        }

        public AppointmentViewModel(Appointment appointment)
        {
            Model = appointment;
            InitializeData();
            LoadSelectedTreatments();
            SetupCommands();
        }

        private void InitializeData()
        {
            // Initialize Available Treatments
            AvailableTreatments = new ObservableCollection<Treatment>(TreatmentManager.Current.GetAllTreatments());

            // Subscribe to changes in SelectedTreatments
            SelectedTreatments.CollectionChanged += SelectedTreatments_CollectionChanged;
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
                if (SelectedPatient == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Validation Error", "Please select a patient.", "OK");
                    return;
                }

                if (!SelectedTreatments.Any())
                {
                    await Application.Current.MainPage.DisplayAlert("Validation Error", "Please select at least one treatment.", "OK");
                    return;
                }

                // Assign selected treatments
                Model.TreatmentIDs = SelectedTreatments.Select(t => t.ID).ToList();

                // Optionally, set appointment name based on patient and treatments
                Model.Name = $"{SelectedPatient.Name} - {string.Join(", ", SelectedTreatments.Select(t => t.Name))}";

                // Add or update appointment
                AppointmentManager.Current.AddAppointment(Model);

                await Application.Current.MainPage.DisplayAlert("Success", "Appointment saved successfully.", "OK");

                // Navigate back to Appointment Management
                await Shell.Current.GoToAsync("//AppointmentManagement");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

        private async void DoCancel()
        {
            // Navigate back to Appointment Management without saving
            await Shell.Current.GoToAsync("//AppointmentManagement");
        }

        private void SelectedTreatments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update the model's TreatmentIDs
            if (Model != null)
            {
                Model.TreatmentIDs = SelectedTreatments.Select(t => t.ID).ToList();
                NotifyPropertyChanged(nameof(Price));
                NotifyPropertyChanged(nameof(InsurancePrice));
            }
        }

        public void LoadAppointment(int appointmentId)
        {
            if (appointmentId > 0)
            {
                var selectedAppointment = AppointmentManager.Current.GetAppointmentById(appointmentId);
                if (selectedAppointment != null)
                {
                    Model = selectedAppointment;
                    SelectedPatient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == Model.PatientID);
                    LoadSelectedTreatments();
                }
            }
            else
            {
                Model = new Appointment();
                SelectedPatient = null;
                SelectedTreatments.Clear();
            }

            NotifyAllPropertiesChanged();
        }

        private void LoadSelectedTreatments()
        {
            SelectedTreatments.CollectionChanged -= SelectedTreatments_CollectionChanged;
            SelectedTreatments.Clear();
            foreach (var treatment in AvailableTreatments.Where(t => Model?.TreatmentIDs.Contains(t.ID) ?? false))
            {
                SelectedTreatments.Add(treatment);
            }
            SelectedTreatments.CollectionChanged += SelectedTreatments_CollectionChanged;
        }

        private void NotifyAllPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(ID));
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(PatientName));
            NotifyPropertyChanged(nameof(SelectedPatient));
            NotifyPropertyChanged(nameof(StartDate));
            NotifyPropertyChanged(nameof(StartTime));
            NotifyPropertyChanged(nameof(StartTimeDisplay));
            NotifyPropertyChanged(nameof(EndDate));
            NotifyPropertyChanged(nameof(EndTime));
            NotifyPropertyChanged(nameof(EndTimeDisplay));
            NotifyPropertyChanged(nameof(Price));
            NotifyPropertyChanged(nameof(InsurancePrice));
            NotifyPropertyChanged(nameof(PatientInsurance));
        }

        public void AddOrUpdate()
        {
            if (Model != null)
            {
                AppointmentManager.Current.AddAppointment(Model);
            }
        }

        public Appointment GetAppointmentModel()
        {
            return Model ?? new Appointment();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}