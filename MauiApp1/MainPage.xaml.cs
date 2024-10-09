namespace MauiApp1;

using MedManagementLibrary;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
	}

    private void PatientsClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Patients");
    }

}