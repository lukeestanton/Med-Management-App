using System;
using System.Collections.Generic;

namespace MedManagementLibrary
{
    public class PatientManager
    {
        private static PatientManager? _current;
        public static PatientManager Current => _current ??= new PatientManager();

        private List<Patient> _patients;

        // Constructor initializes the patient list with some sample patients
        private PatientManager()
        {
            _patients = new List<Patient>
            {
                new Patient { ID = 1, Name = "John Doe", Birthday = new DateTime(2000, 1, 21), Address = "123 Main St", Race = "Caucasian", Gender = "Male" },
                new Patient { ID = 2, Name = "Jane Smith", Birthday = new DateTime(2003, 5, 23), Address = "456 Oak Ave", Race = "African American", Gender = "Female" }
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
            bool isAdd = false;

            // Check if the patient has an ID; if not, assign a new one
            if (patient.ID <= 0)
            {
                patient.ID = _patients.Any() ? _patients.Max(p => p.ID) + 1 : 1; // Assign new ID
                isAdd = true;
            }

            if (isAdd)
            {
                _patients.Add(patient); // Add new patient
                Console.WriteLine($"New patient added: {patient.Name}, ID: {patient.ID}");
            }
            else
            {
                // Update existing patient
                var existingPatient = _patients.FirstOrDefault(p => p.ID == patient.ID);
                if (existingPatient != null)
                {
                    existingPatient.Name = patient.Name;
                    existingPatient.Birthday = patient.Birthday;
                    existingPatient.Address = patient.Address;
                    existingPatient.Race = patient.Race;
                    existingPatient.Gender = patient.Gender;
                    existingPatient.Diagnoses = patient.Diagnoses;
                    existingPatient.Prescriptions = patient.Prescriptions;
                    Console.WriteLine($"Patient updated: {existingPatient.Name}, ID: {existingPatient.ID}");
                }
            }
        }



        // Delete a patient by ID
        public void DeletePatient(int id)
        {
            var patientToRemove = _patients.FirstOrDefault(p => p.ID == id);
            if (patientToRemove != null)
            {
                _patients.Remove(patientToRemove);
            }
        }
    }
}



