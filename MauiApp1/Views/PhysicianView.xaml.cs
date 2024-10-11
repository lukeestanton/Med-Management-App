using System.ComponentModel;
using MedManagementLibrary;

namespace MauiApp1.Views;

[QueryProperty(nameof(PhysicianId), "physicianId")]
public partial class PhysicianView : ContentPage
{
    public PhysicianView()
    {
        InitializeComponent();
    }

    public int PhysicianId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PhysicianManagement");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        var physicianToAdd = BindingContext as Physician;
        if (physicianToAdd != null)
        {
            PhysicianManager.Current.AddPhysician(physicianToAdd);
        }

        Shell.Current.GoToAsync("//PhysicianManagement");
    }

    private void PhysicianView_NavigatedTo(object sender, EventArgs e)
    {
        if (PhysicianId > 0)
        {
            var selectedPhysician = PhysicianManager.Current.GetAllPhysicians()
                .FirstOrDefault(p => p.ID == PhysicianId);
            if (selectedPhysician != null)
            {
                BindingContext = selectedPhysician;
            }
        }
        else
        {
            // Create a new physician when physicianId is 0
            BindingContext = new Physician();
        }
    }
}




