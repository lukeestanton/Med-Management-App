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
    }
}


