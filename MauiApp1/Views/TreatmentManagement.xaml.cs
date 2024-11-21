using System.ComponentModel;
using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    public partial class TreatmentManagement : ContentPage, INotifyPropertyChanged
    {
        public TreatmentManagementViewModel ViewModel => BindingContext as TreatmentManagementViewModel;
        public TreatmentManagement()
        {
            InitializeComponent();
            BindingContext = new TreatmentManagementViewModel();
        }

        private void TreatmentManagement_NavigatedTo(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }

    }
}
