using System;
using MedManagementLibrary;

namespace MauiApp1.ViewModels;

public class PatientViewModel
{
    private Patient? model { get; set; }

    public int ID {
        get 
        {
            if(model == null) 
            {
                return -1;
            }
            return model.ID;
        }
        set 
        {
            if(model != null && model.ID != value) 
            {
                model.ID = value;
            }
        }
    }

    public string Name 
    {
        get => model?.Name ?? string.Empty;

        set 
        {
            if(model != null) 
            {
                model.Name = value;
            }
        }
    }

    public PatientViewModel() 
    {
        model = new Patient();
    }

    public PatientViewModel(Patient? _model) 
    {
        model = _model;
    }
}
