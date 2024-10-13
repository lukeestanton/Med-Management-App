using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
	public int AppointmentId { get; set; }

	private AppointmentViewModel viewModel;

    public AppointmentView()
    {
        InitializeComponent();
		viewModel = new AppointmentViewModel();
		BindingContext = viewModel;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentManagement");
    }

    private void AddClicked(object sender, EventArgs e) {
        var appointmentToAdd = viewModel.GetAppointmentModel();
        if (appointmentToAdd != null) 
		{
            AppointmentManager.Current.AddAppointment(appointmentToAdd);
        }
        Shell.Current.GoToAsync("//AppointmentManagement");
    }

    private void AppointmentView_NavigatedTo(object sender, EventArgs e)
    {
        viewModel.LoadAppointment(AppointmentId);
    }
}
