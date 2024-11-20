using System.ComponentModel;
using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    public partial class AppointmentManagement : ContentPage, INotifyPropertyChanged
    {
        public AppointmentManagementViewModel ViewModel => BindingContext as AppointmentManagementViewModel;

        public AppointmentManagement()
        {
            InitializeComponent();
            BindingContext = new AppointmentManagementViewModel();
        }

        private void AppointmentManagement_NavigatedTo(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
