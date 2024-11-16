using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class TreatmentManager
    {
        private static readonly object _lock = new object();
        private static TreatmentManager? _instance;

        public static TreatmentManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TreatmentManager();
                    }
                    return _instance;
                }
            }
        }

        private List<Treatment> _treatments;

        private TreatmentManager()
        {
            _treatments = new List<Treatment>
            {
                new Treatment
                {
                    ID = 1,
                    Name = "Flu Shot",
                    Price = 20
                },
                new Treatment
                {
                    ID = 2,
                    Name = "Covid Test",
                    Price = 40
                },
                new Treatment
                {
                    ID = 3,
                    Name = "Strep Test",
                    Price = 50
                },
                new Treatment
                {
                    ID = 4,
                    Name = "Heart Surgery",
                    Price = 900
                },
            };
        }

        public List<Treatment> GetAllTreatments()
        {
            return _treatments;
        }

        public void AddTreatment(Treatment treatment)
        {
            bool isAdd = false;

            if (treatment.ID <= 0)
            {
                treatment.ID = _treatments.Any() ? _treatments.Max(t => t.ID) + 1 : 1;
                isAdd = true;
            }

            if (isAdd)
            {
                _treatments.Add(treatment);
            }
            else
            {
                var existingTreatment = _treatments.FirstOrDefault(t => t.ID == treatment.ID);
                if (existingTreatment != null)
                {
                    existingTreatment.Name = treatment.Name;
                    existingTreatment.Price = treatment.Price;
                }
            }
        }

        public void DeleteTreatment(int id)
        {
            var treatmentToRemove = _treatments.Find(t => t.ID == id);
            if (treatmentToRemove != null)
            {
                _treatments.Remove(treatmentToRemove);
            }
        }
    }
}
