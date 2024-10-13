using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public PhysicianViewModel? SelectedPhysician { get; set; }

        public ObservableCollection<PhysicianViewModel> Physicians { 
            get {
                return new ObservableCollection<PhysicianViewModel>(PhysicianManager.Current.GetAllPhysicians().Where(p=>p != null).Select(p => new PhysicianViewModel(p)));
            }
        }

        public void Delete() 
        {
            if(SelectedPhysician == null) {
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
