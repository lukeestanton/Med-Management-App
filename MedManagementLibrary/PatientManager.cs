using System;
using System.Collections.Generic;

namespace MedManagementLibrary
{
    public class PatientManager
    {
        private static PatientManager _current;
        public static PatientManager Current => _current ??= new PatientManager();

        private List<Patient> _patients;

        // Constructor initializes the patient list with some sample patients
        private PatientManager()
        {
            _patients = new List<Patient>
            {
                new Patient { Name = "John Doe", Birthday = new DateTime(2000, 1, 21), Address = "123 Main St", Race = "Caucasian", Gender = "Male" },
                new Patient { Name = "Jane Smith", Birthday = new DateTime(2003, 5, 23), Address = "456 Oak Ave", Race = "African American", Gender = "Female" }
            };
        }

        // Retrieve all patients
        public List<Patient> GetAllPatients()
        {
            return _patients;
        }

        // Add a new patient
        public void AddPatient(Patient patient)
        {
            _patients.Add(patient);
        }
    }
}



