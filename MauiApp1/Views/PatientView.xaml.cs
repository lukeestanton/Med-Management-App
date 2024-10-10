using System.ComponentModel;
using MedManagementLibrary;

namespace MauiApp1.Views;

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
	}

	private void CancelClicked(object sender, EventArgs e) {
		Shell.Current.GoToAsync("//Patients");
	}

	private void AddClicked(object sender, EventArgs e) {
		var patientToAdd = BindingContext as Patient;
		if(patientToAdd != null) {
			PatientManager.Current.AddPatient(patientToAdd);
		}

		Shell.Current.GoToAsync("//Patients");		
	}

	private void PatientView_NavigatedTo(object sender, EventArgs e) {
		BindingContext = new Patient();
	}
}