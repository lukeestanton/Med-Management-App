using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class TreatmentViewModel : INotifyPropertyChanged
    {
        private Treatment model;

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

        public decimal Price
        {
            get => model.Price;
            set
            {
                if (model.Price != value)
                {
                    model.Price = value;
                    NotifyPropertyChanged(nameof(Price));
                }
            }
        }

        public TreatmentViewModel()
        {
            model = new Treatment();
            SetupCommands();
        }

        public TreatmentViewModel(Treatment? _model)
        {
            model = _model ?? new Treatment();
            SetupCommands();
        }

        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public void SetupCommands() {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((t) => DoEdit(t as TreatmentViewModel));
        }

        private void DoDelete() {
        if(ID > 0) {
            TreatmentManager.Current.DeleteTreatment(ID);
            Shell.Current.GoToAsync("//Treatments");
        }   
        }

        private void DoEdit(TreatmentViewModel? tvm) 
        {
            if (tvm == null)
                {
                    return;
                }
            var selectedTreatmentId = tvm?.ID ?? 0;
            Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
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

        public Treatment GetTreatmentModel()
        {
            return model;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
