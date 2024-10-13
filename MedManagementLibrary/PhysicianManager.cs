using System;
using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class PhysicianManager
    {
        private static PhysicianManager? _current;
        public static PhysicianManager Current => _current ??= new PhysicianManager();

        private List<Physician> _physicians;

        private PhysicianManager()
        {
            _physicians = new List<Physician>
            {
                new Physician { ID = 1, Name = "Dr. John Smith", LicenseNumber = "12345", GraduationDate = new DateTime(2010, 5, 23), Specializations = new List<string> { "Cardiology" } },
                new Physician { ID = 2, Name = "Dr. Emily Johnson", LicenseNumber = "67890", GraduationDate = new DateTime(2015, 6, 12), Specializations = new List<string> { "Neurology" } }
            };
        }

        public List<Physician> GetAllPhysicians()
        {
            return _physicians;
        }

        public void AddPhysician(Physician physician)
        {
            bool isAdd = false;

            if (physician.ID <= 0)
            {
                physician.ID = _physicians.Any() ? _physicians.Max(p => p.ID) + 1 : 1;
                isAdd = true;
            }

            if (isAdd)
            {
                _physicians.Add(physician);
                Console.WriteLine($"New physician added: {physician.Name}, ID: {physician.ID}");
            }
            else
            {
                var existingPhysician = _physicians.FirstOrDefault(p => p.ID == physician.ID);
                if (existingPhysician != null)
                {
                    existingPhysician.Name = physician.Name;
                    existingPhysician.LicenseNumber = physician.LicenseNumber;
                    existingPhysician.GraduationDate = physician.GraduationDate;
                    existingPhysician.Specializations = physician.Specializations;
                    Console.WriteLine($"Patient updated: {existingPhysician.Name}, ID: {existingPhysician.ID}");
                }
            }
        }

        public void DeletePhysician(int id)
        {
            var physicianToRemove = _physicians.FirstOrDefault(p => p.ID == id);
            if (physicianToRemove != null)
            {
                _physicians.Remove(physicianToRemove);
            }
        }
    }
}

