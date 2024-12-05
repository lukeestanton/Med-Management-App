using MedManagementLibrary;

namespace Api.Clinic.Database
{
    static public class FakeDatabase
    {
        public static int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        private static List<Physician> physicians = new List<Physician>
                {
                    new Physician{Id = 1, Name = "John Doe"}
                    , new Physician{Id = 2, Name = "Jane Doe"}
                };
        public static List<Physician> Physicians { 
            get
            {
                return physicians;
            } 
        }
        public static Physician? AddOrUpdatePhysician(Physician? physician)
        {
            if (physician == null)
            {
                return null;
            }
            bool isAdd = false;
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                Physicians.Add(physician);
            }
            else
            {
                var existing = Physicians.FirstOrDefault(p => p.Id == physician.Id);
                if (existing != null)
                {
                    existing.Name = physician.Name;
                    existing.Birthday = physician.Birthday;
                }
            }

            return physician;
        }
    }
}

