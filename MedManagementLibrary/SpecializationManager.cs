using System;

namespace MedManagementLibrary;

public class SpecializationManager
{
    private static readonly object _lock = new object();
        private static SpecializationManager? _instance;

        public static SpecializationManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SpecializationManager();
                    }
                    return _instance;
                }
            }
        }

        private List<Specialization> _specializations;

        private SpecializationManager()
        {
            _specializations = new List<Specialization>
            {
                new Specialization { ID = 1, Name = "Meditation" },
                new Specialization { ID = 2, Name = "Therapy" },
                new Specialization { ID = 3, Name = "Repair" },
                new Specialization { ID = 4, Name = "Surgery" },
            };
        }

        public List<Specialization> GetAllSpecializations()
        {
            return _specializations;
        }
}
