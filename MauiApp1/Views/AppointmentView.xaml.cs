using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(AppointmentId), "appointmentId")]
public partial class AppointmentView : ContentPage
{
    public int AppointmentId { get; set; }

    public AppointmentViewModel? ViewModel => BindingContext as AppointmentViewModel;

    public AppointmentView()
    {
        InitializeComponent();
        // BindingContext is set in XAML
    }

    private void AppointmentView_NavigatedTo(object sender, EventArgs e)
    {
        ViewModel.LoadAppointment(AppointmentId);
    }
}