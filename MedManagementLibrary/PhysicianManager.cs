using System;
using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class PhysicianManager
    {
        private static readonly object _lock = new object();
        private static PhysicianManager? _instance;

        public static PhysicianManager Current
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PhysicianManager();
                    }
                    return _instance;
                }
            }
        }

        private List<Physician> _physicians;

        private PhysicianManager()
        {

            _physicians = new List<Physician>
            {
                new Physician
                {
                    ID = 1,
                    Name = "Batman",
                    GraduationDate = new DateTime(2010, 8, 9),
                    SpecializationIDs = new List<int> { 1, 3, 4 }
                },
                new Physician
                {
                    ID = 2,
                    Name = "Gordon",
                    GraduationDate = new DateTime(1993, 5, 1),
                    SpecializationIDs = new List<int> { 1, 2 }
                },
                new Physician
                {
                    ID = 3,
                    Name = "Robin",
                    GraduationDate = new DateTime(2023, 1, 10),
                    SpecializationIDs = new List<int> { 1 }
                },
                new Physician
                {
                    ID = 4,
                    Name = "Alfred",
                    GraduationDate = new DateTime(1923, 3, 11),
                    SpecializationIDs = new List<int> { 2, 3, 4 }
                }
            };
        }

        public List<Physician> GetAllPhysicians()
        {
            return _physicians;
        }

        public Physician? GetPhysicianById(int id)
        {
            return _physicians.FirstOrDefault(p => p.ID == id);
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
            }
            else
            {
                var existingPhysician = _physicians.FirstOrDefault(p => p.ID == physician.ID);
                if (existingPhysician != null)
                {
                    existingPhysician.Name = physician.Name;
                    existingPhysician.GraduationDate = physician.GraduationDate;
                    existingPhysician.SpecializationIDs = physician.SpecializationIDs;
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
