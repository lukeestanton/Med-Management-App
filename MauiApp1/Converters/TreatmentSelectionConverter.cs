using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using MedManagementLibrary;
using MauiApp1.ViewModels;
using System.Collections.ObjectModel;

namespace MauiApp1.Converters
{
    public class TreatmentSelectionConverter : IValueConverter
    {
        // Convert SelectedTreatments and Treatment to bool (IsSelected)
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var selectedTreatments = value as ObservableCollection<Treatment>;
            var treatment = parameter as Treatment;

            if (selectedTreatments != null && treatment != null)
            {
                return selectedTreatments.Contains(treatment);
            }

            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var isChecked = (bool?)value ?? false;
            var treatment = parameter as Treatment;

            if (treatment == null)
                return null;

            // Access the ViewModel via the BindingContext of the ContentPage
            var viewModel = ((Element?)parameter)?.BindingContext as AppointmentViewModel;
            if (viewModel == null)
                return null;

            if (isChecked)
            {
                if (!viewModel.SelectedTreatments.Contains(treatment))
                    viewModel.SelectedTreatments.Add(treatment);
            }
            else
            {
                if (viewModel.SelectedTreatments.Contains(treatment))
                    viewModel.SelectedTreatments.Remove(treatment);
            }

            return null;
        }
    }
}
