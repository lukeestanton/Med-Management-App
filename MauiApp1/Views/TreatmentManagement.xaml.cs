using System;
using Microsoft.Maui.Controls;
using MauiApp1.ViewModels;

namespace MauiApp1.Views
{
    public partial class TreatmentManagement : ContentPage
    {
        public TreatmentManagement()
        {
            InitializeComponent();
            BindingContext = new TreatmentManagementViewModel();
        }

        private void TreatmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as TreatmentManagementViewModel)?.Refresh();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//TreatmentDetails?treatmentId=0");
        }

        private void EditClicked(object sender, EventArgs e)
        {
            var selectedTreatmentId = (BindingContext as TreatmentManagementViewModel)?.SelectedTreatment?.ID ?? 0;
            Shell.Current.GoToAsync($"//TreatmentDetails?treatmentId={selectedTreatmentId}");
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private void RefreshClicked(object sender, EventArgs e) {
            (BindingContext as TreatmentManagementViewModel)?.Refresh();
        } 
    }
}
