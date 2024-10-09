using System;
using System.Collections.Generic;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    public partial class PatientManagement : ContentPage
    {
        // Expose the Patients list to the UI
        public List<Patient> Patients => PatientManager.Current.GetAllPatients();

        public PatientManagement()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}

