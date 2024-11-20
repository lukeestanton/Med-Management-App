using System;
using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class PatientManager
    {
        private static readonly object _lock = new object();
        private static PatientManager? _instance;

        public static PatientManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PatientManager();
                    }
                    return _instance;
                }
            }
        }

        private List<Patient> _patients;

        private PatientManager()
        {
            _patients = new List<Patient>
            {
                new Patient
                {
                    ID = 1,
                    Name = "John Doe",
                    Birthday = new DateTime(2000, 1, 21),
                    Address = "123 Main St",
                    Race = "Caucasian",
                    Gender = "Male",
                    InsurancePlan = new Insurance { Name = "Basic Plan", CoveragePercentage = 0.7m }
                },
                new Patient
                {
                    ID = 2,
                    Name = "Jane Smith",
                    Birthday = new DateTime(2003, 5, 23),
                    Address = "456 Oak Ave",
                    Race = "African American",
                    Gender = "Female",
                    InsurancePlan = new Insurance { Name = "Premium Plan", CoveragePercentage = 0.9m }
                },
                new Patient
                {
                    ID = 3,
                    Name = "Luke Staton",
                    Birthday = new DateTime(2003, 12, 1),
                    Address = "456 Tally Way",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = new Insurance { Name = "TrumpCare", CoveragePercentage = 1.0m }
                }
            };
        }

        public List<Patient> GetAllPatients()
        {
            return _patients;
        }

        public void AddPatient(Patient patient)
        {
            bool isAdd = false;

            if (patient.ID <= 0)
            {
                patient.ID = _patients.Any() ? _patients.Max(p => p.ID) + 1 : 1;
                isAdd = true;
            }
            if (isAdd)
            {
                _patients.Add(patient);
            }
            else
            {
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
                }
            }
        }
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



