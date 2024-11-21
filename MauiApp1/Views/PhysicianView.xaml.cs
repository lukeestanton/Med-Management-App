using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
    public int PhysicianId { get; set; }

    public PhysicianViewModel? ViewModel => BindingContext as PhysicianViewModel;

    public PhysicianView()
    {
        InitializeComponent();
    }

    private void PhysicianView_NavigatedTo(object sender, EventArgs e)
    {
        ViewModel.LoadPhysician(PhysicianId);
    }
}
