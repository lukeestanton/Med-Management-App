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
                new Insurance { ID = 1, Name = "United Health Care", CoveragePercentage = 0.8m },
                new Insurance { ID = 2, Name = "Cigna", CoveragePercentage = 0.5m },
                new Insurance { ID = 3, Name = "TrumpCare", CoveragePercentage = 1.0m },
            };
        }

        public List<Insurance> GetAllInsurancePlans()
        {
            return _insurancePlans;
        }

    }
}
