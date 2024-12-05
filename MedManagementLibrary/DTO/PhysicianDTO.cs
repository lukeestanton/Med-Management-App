using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedManagementLibrary;

namespace MedManagementLibrary.DTO 
{
    public class PhysicianDTO
    {
        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }

        public string Display
        {
            get
            {
                return $"[{ID}] {Name}";
            }
        }

        public int ID { get; set; } 
        public string? Name { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<int>? SpecializationIDs { get; set; }

        public PhysicianDTO() { }
        public PhysicianDTO(Physician p)
        {
            ID = p.ID;
            Name = p.Name;
            GraduationDate = p.GraduationDate;
            SpecializationIDs = p.SpecializationIDs;
        }
    }
}


