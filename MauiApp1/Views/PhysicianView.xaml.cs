using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
    public int PhysicianId { get; set; }

    private PhysicianViewModel viewModel;

    public PhysicianView()
    {
        InitializeComponent();
        viewModel = new PhysicianViewModel();
        BindingContext = viewModel;
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhysicianManagement");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        var physicianToAdd = viewModel.GetPhysicianModel();
        if (physicianToAdd != null)
        {
            PhysicianManager.Current.AddPhysician(physicianToAdd);
        }
		
        Shell.Current.GoToAsync("//PhysicianManagement");
    }

    private void PhysicianView_NavigatedTo(object sender, EventArgs e)
    {
        viewModel.LoadPhysician(PhysicianId);
    }
}
