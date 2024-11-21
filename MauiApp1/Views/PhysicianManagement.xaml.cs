using System.ComponentModel;
using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    public partial class PhysicianManagement : ContentPage, INotifyPropertyChanged
    {
        public PhysicianManagementViewModel ViewModel => BindingContext as PhysicianManagementViewModel;
        public PhysicianManagement()
        {
            InitializeComponent();
            BindingContext = new PhysicianManagementViewModel();
        }

        private void PhysicianManagement_NavigatedTo(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }
    }
}
