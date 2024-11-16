using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using MauiApp1.ViewModels;
using MedManagementLibrary;

namespace MauiApp1.Views
{
    [QueryProperty(nameof(TreatmentId), "treatmentId")]
    public partial class TreatmentView : ContentPage
    {
        public int TreatmentId { get; set; }

        private TreatmentViewModel viewModel;

        public TreatmentView()
        {
            InitializeComponent();
            viewModel = new TreatmentViewModel();
            BindingContext = viewModel;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Treatments");
        }

        private void AddClicked(object sender, EventArgs e)
        {
            var treatmentToAdd = viewModel.GetTreatmentModel();
            if (treatmentToAdd != null)
            {
                TreatmentManager.Current.AddTreatment(treatmentToAdd);
            }

            Shell.Current.GoToAsync("//Treatments");
        }

        private void TreatmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            viewModel.LoadTreatment(TreatmentId);
        }
        
    }
}