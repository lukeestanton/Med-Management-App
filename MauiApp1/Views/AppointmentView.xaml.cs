using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
    public AppointmentView()
    {
        InitializeComponent();
    }

    public int AppointmentId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AppointmentManagement");
    }

    private void AddClicked(object sender, EventArgs e) {
        var appointmentToAdd = BindingContext as Appointment;
        if (appointmentToAdd != null) {
            AppointmentManager.Current.AddAppointment(appointmentToAdd);
        }
        Shell.Current.GoToAsync("//AppointmentManagement");
    }

    private void AppointmentView_NavigatedTo(object sender, EventArgs e)
    {
        if (AppointmentId > 0)
        {
            var selectedAppointment = AppointmentManager.Current.GetAllAppointments().FirstOrDefault(a => a.ID == AppointmentId);
            if (selectedAppointment != null)
            {
                BindingContext = selectedAppointment;
            }
        }
        else
        {
            // Create a new appointment if appointmentId is 0
            BindingContext = new Appointment();
        }
    }
}
