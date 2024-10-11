namespace MauiApp1
{
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

        private void PhysicianManagerButtonClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//PhysicianManagement");
        }

        private void AppointmentsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AppointmentManagement");
        }
    }
}
