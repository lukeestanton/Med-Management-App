using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    public partial class PatientManagement : ContentPage, INotifyPropertyChanged
    {
        public PatientManagement()
        {
            InitializeComponent();
            BindingContext = new PatientManagementViewModel();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//PatientDetails");
        }

        private void PatientManagement_NavigatedTo(object sender, EventArgs e)
        {
            (BindingContext as PatientManagementViewModel)?.Refresh();
        }
    }
}

