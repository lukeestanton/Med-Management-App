using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class TreatmentViewModel : INotifyPropertyChanged
    {
        public Treatment? model { get; set; }

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

        public decimal Price
        {
            get => model?.Price ?? 0;
            set
            {
                if (model != null && model.Price != value)
                {
                    model.Price = value;
                    NotifyPropertyChanged(nameof(Price));
                }
            }
        }

        public TreatmentViewModel(Treatment treatment)
        {
            model = treatment;
            SetupCommands();
        }

        public ICommand AddOrUpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public TreatmentViewModel()
        {
            model = new Treatment();
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
                    await Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter a treatment name.", "OK");
                    return;
                }

                TreatmentManager.Current.AddTreatment(model);

                await Application.Current.MainPage.DisplayAlert("Success", "Treatment saved successfully.", "OK");

                await Shell.Current.GoToAsync("//Treatments");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

        private async void DoCancel()
        {
            await Shell.Current.GoToAsync("//Treatments");
        }

        public void LoadTreatment(int treatmentId)
        {
            if (treatmentId > 0)
            {
                var selectedTreatment = TreatmentManager.Current.GetAllTreatments().FirstOrDefault(t => t.ID == treatmentId);
                if (selectedTreatment != null)
                {
                    model = selectedTreatment;
                }
            }
            else
            {
                model = new Treatment(); 
            }
            NotifyAllPropertiesChanged(); 
        }

        private void NotifyAllPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(ID));
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Price));
        }

        public void AddOrUpdate()
        {
            if (model != null)
            {
                TreatmentManager.Current.AddTreatment(model);
            }
        }

        public Treatment GetTreatmentModel()
        {
            return model ?? new Treatment();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
