using System;
using MedManagementLibrary;

namespace Api.Clinic.Database 
{
    static public class FakeDatabase
    {
        public static IEnumerable<Physician> Physicians 
        {
            get {
                return new List<Physician>
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
        }
    }
}


