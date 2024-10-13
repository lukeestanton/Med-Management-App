using System;
using System.ComponentModel;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class PhysicianViewModel : INotifyPropertyChanged
    {
        private Physician model;

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

        public PhysicianViewModel()
        {
            model = new Physician();
        }

        public PhysicianViewModel(Physician _model)
        {
            model = _model ?? new Physician();
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
        }

        public Physician GetPhysicianModel()
        {
            return model;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
