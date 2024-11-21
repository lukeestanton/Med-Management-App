using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class InsuranceManager
    {
        private static readonly object _lock = new object();
        private static InsuranceManager? _instance;

        public static InsuranceManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new InsuranceManager();
                    }
                    return _instance;
                }
            }
        }

        private List<Insurance> _insurancePlans;

        private InsuranceManager()
        {
            _insurancePlans = new List<Insurance>
            {
                new Insurance { ID = 1, Name = "Wayne Health", CoveragePercentage = 0.9m },
                new Insurance { ID = 2, Name = "Gotham Care", CoveragePercentage = 0.85m },
                new Insurance { ID = 3, Name = "Arkham Insurance", CoveragePercentage = 0.75m },
                new Insurance { ID = 4, Name = "BatShield Insurance", CoveragePercentage = 0.8m },
                new Insurance { ID = 5, Name = "Dark Knight Coverage", CoveragePercentage = 1.0m },
            };
        }

        public List<Insurance> GetAllInsurancePlans()
        {
            return _insurancePlans;
        }

    }
}
