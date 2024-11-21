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
            var insuranceManager = InsuranceManager.Current;

            _patients = new List<Patient>
            {
                new Patient
                {
                    ID = 1,
                    Name = "The Joker",
                    Birthday = new DateTime(1950, 4, 1),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Arkham Insurance")
                },
                new Patient
                {
                    ID = 2,
                    Name = "Harley Quinn",
                    Birthday = new DateTime(1985, 6, 15),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Female",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Gotham Care")
                },
                new Patient
                {
                    ID = 3,
                    Name = "Two-Face",
                    Birthday = new DateTime(1975, 8, 20),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "BatShield Insurance")
                },
                new Patient
                {
                    ID = 4,
                    Name = "The Riddler",
                    Birthday = new DateTime(1968, 11, 5),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Wayne Health")
                },
                new Patient
                {
                    ID = 5,
                    Name = "Poison Ivy",
                    Birthday = new DateTime(1980, 3, 12),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Female",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Dark Knight Coverage")
                },
                new Patient
                {
                    ID = 6,
                    Name = "Scarecrow",
                    Birthday = new DateTime(1970, 10, 30),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Arkham Insurance")
                },
                new Patient
                {
                    ID = 7,
                    Name = "Mr. Freeze",
                    Birthday = new DateTime(1965, 2, 18),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Wayne Health")
                },
                new Patient
                {
                    ID = 8,
                    Name = "Bane",
                    Birthday = new DateTime(1972, 7, 14),
                    Address = "Arkham Asylum",
                    Race = "White",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "BatShield Insurance")
                },
                new Patient
                {
                    ID = 9,
                    Name = "Killer Croc",
                    Birthday = new DateTime(1982, 9, 9),
                    Address = "Arkham Asylum",
                    Race = "Crocodile",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Gotham Care")
                },
                new Patient
                {
                    ID = 10,
                    Name = "Man-Bat",
                    Birthday = new DateTime(1978, 12, 25),
                    Address = "Arkham Asylum",
                    Race = "Bat",
                    Gender = "Male",
                    InsurancePlan = insuranceManager.GetAllInsurancePlans()
                                     .FirstOrDefault(i => i.Name == "Dark Knight Coverage")
                }
            };
        }

        public List<Patient> GetAllPatients()
        {
            return _patients;
        }

        public Patient? GetPatientById(int id)
        {
            return _patients.FirstOrDefault(p => p.ID == id);
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
                    existingPatient.InsurancePlan = patient.InsurancePlan;
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



