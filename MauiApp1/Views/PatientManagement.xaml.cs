using System.ComponentModel;
using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    public partial class PatientManagement : ContentPage, INotifyPropertyChanged
    {
        public PatientManagementViewModel ViewModel => BindingContext as PatientManagementViewModel;
        public PatientManagement()
        {
            InitializeComponent();
            BindingContext = new PatientManagementViewModel();
        }

        private void PatientManagement_NavigatedTo(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }

    }
}
