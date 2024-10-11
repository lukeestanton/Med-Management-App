using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
	}

	public int PatientId { get; set; }

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

	private void PatientView_NavigatedTo(object sender, EventArgs e)
	{
		if (PatientId > 0) 
		{
			var selectedPatient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == PatientId);
			if (selectedPatient != null)
			{
				BindingContext = selectedPatient;
			}
		} 
		else 
		{
			// Create a new patient when patientId is 0
			BindingContext = new Patient();
		}
	}
}