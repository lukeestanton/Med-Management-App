using System;
using Api.Clinic.Database;
using MedManagementLibrary;

namespace Api.Clinic.Enterprise 
{
    public class PhysicianEC
    {
        public PhysicianEC() { }

        public IEnumerable<Physician> Physicians 
        { 
            get 
            {
                return FakeDatabase.Physicians;
            }            
        }

        public Physician? GetById(int id) 
        {
            return FakeDatabase.Physicians.FirstOrDefault(p => p.ID == id);
        }

        public Physician? Delete(int id)
        {
            var physicianToDelete = FakeDatabase.Physicians.FirstOrDefault(p => p.ID == id);
            if (physicianToDelete != null)
            {
                FakeDatabase.Physicians.Remove(physicianToDelete);
            }

            return physicianToDelete;
        }

        public Physician Add(Physician physician)
        {
            physician.ID = FakeDatabase.Physicians.Max(p => p.ID) + 1;
            FakeDatabase.Physicians.Add(physician);
            return physician;
        }

        public Physician? Update(Physician physician)
        {
            var existingPhysician = GetById(physician.ID);
            if (existingPhysician != null)
            {
                existingPhysician.Name = physician.Name;
                existingPhysician.GraduationDate = physician.GraduationDate;
                existingPhysician.SpecializationIDs = physician.SpecializationIDs;
                return existingPhysician;
            }
            return null;
        }
    }
}


