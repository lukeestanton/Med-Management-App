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
            return $"[{Id}] {Name}";
        }

        public string Display
        {
            get
            {
                return $"[{Id}] {Name}";
            }
        }

        public int Id { get; set; } 
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }

        public PhysicianDTO() { }
        public PhysicianDTO(Physician p)
        {
            Id = p.Id;
            Name = p.Name;
            Birthday = p.Birthday;
        }
    }
}


