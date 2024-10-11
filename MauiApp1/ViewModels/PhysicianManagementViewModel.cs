using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MedManagementLibrary;

namespace MauiApp1.ViewModels
{
    public class PhysicianManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Physician? SelectedPhysician { get; set; }

        public ObservableCollection<Physician> Physicians
        {
            get
            {
                return new ObservableCollection<Physician>(PhysicianManager.Current.GetAllPhysicians());
            }
        }

        public void Delete()
        {
            if (SelectedPhysician == null)
            {
                return;
            }
            PhysicianManager.Current.DeletePhysician(SelectedPhysician.ID);

            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Physicians));
        }
    }
}
