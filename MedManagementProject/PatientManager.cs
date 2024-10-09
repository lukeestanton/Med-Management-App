using System;

namespace MedManagementProject;

public class PatientManager
{
    private List<Patient> _patients;

    public PatientManager()
    {
        _patients = new List<Patient>();
    }

    public List<Patient> GetAllPatients()
    {
        return _patients;
    }

    public void AddPatient(Patient patient)
    {
        _patients.Add(patient);
    }
}
