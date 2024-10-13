using MedManagementLibrary;
using MauiApp1.ViewModels;

namespace MauiApp1.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
    public int PatientId { get; set; }

    private PatientViewModel viewModel;

    public PatientView()
    {
        InitializeComponent();
        viewModel = new PatientViewModel();
        BindingContext = viewModel;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Patients");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        var patientToAdd = viewModel.GetPatientModel();
        if (patientToAdd != null)
        {
            PatientManager.Current.AddPatient(patientToAdd);
        }

        Shell.Current.GoToAsync("//Patients");
    }

    private void PatientView_NavigatedTo(object sender, EventArgs e)
    {
        viewModel.LoadPatient(PatientId);
    }
}
