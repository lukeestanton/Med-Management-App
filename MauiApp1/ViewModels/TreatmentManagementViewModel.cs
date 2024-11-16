using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedManagementLibrary;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TreatmentViewModel? SelectedTreatment { get; set; }

        public string? Query { get; set; }

        public ObservableCollection<TreatmentViewModel> Treatments {
            get {
                var retVal = new ObservableCollection<TreatmentViewModel>(
                    TreatmentManager
                    .Current
                    .GetAllTreatments()
                    .Where(t=>t != null)
                    .Where(t => t.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Take(100)
                    .Select(t => new TreatmentViewModel(t))
                    );

                return retVal;
            }            
        }

        public void Delete() 
        {
            if(SelectedTreatment == null) {
                return;
            }
            TreatmentManager.Current.DeleteTreatment(SelectedTreatment.ID);

            Refresh();
        } 

        public void Refresh() {
            NotifyPropertyChanged(nameof(Treatments));
        }
    }
}
