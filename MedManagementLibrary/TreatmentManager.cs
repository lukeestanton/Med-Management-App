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
                    Name = "Meditation",
                    Price = 60.00m
                },
                new Treatment
                {
                    ID = 2,
                    Name = "Physical Therapy",
                    Price = 55.00m
                },
                new Treatment
                {
                    ID = 3,
                    Name = "Bone Repair",
                    Price = 170.00m
                },
                new Treatment
                {
                    ID = 4,
                    Name = "Muscle Repair",
                    Price = 165.00m
                },
                new Treatment
                {
                    ID = 5,
                    Name = "Cognitive Therapy",
                    Price = 80.00m
                },
                new Treatment
                {
                    ID = 6,
                    Name = "Music Therapy",
                    Price = 50.00m
                },
                new Treatment
                {
                    ID = 7,
                    Name = "Organ Repair",
                    Price = 290.00m
                },
                new Treatment
                {
                    ID = 8,
                    Name = "Aromatherapy",
                    Price = 40.00m
                },
                new Treatment
                {
                    ID = 9,
                    Name = "Stress Relief",
                    Price = 75.00m
                },
                new Treatment
                {
                    ID = 10,
                    Name = "Brain Surgery",
                    Price = 565.00m
                }
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
            var treatmentToRemove = _treatments.FirstOrDefault(t => t.ID == id);
            if (treatmentToRemove != null)
            {
                _treatments.Remove(treatmentToRemove);
            }
        }
    }
}
