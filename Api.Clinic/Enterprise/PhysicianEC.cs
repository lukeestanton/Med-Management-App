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
            return FakeDatabase.Physicians.FirstOrDefault(p => p.Id == id);
        }

        public Physician? Delete(int id)
        {
            var physicianToDelete = FakeDatabase.Physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToDelete != null)
            {
                FakeDatabase.Physicians.Remove(physicianToDelete);
            }

            return physicianToDelete;
        }

        public Physician? AddOrUpdate(Physician? physician)
        {
            return FakeDatabase.AddOrUpdatePhysician(physician);
        }
    }
}