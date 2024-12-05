using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedManagementLibrary.DTO;

namespace MedManagementLibrary
{
    public class Physician
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
        public string? Name  { get; set; }
        public DateTime Birthday { get; set; }

        public Physician()
        {
            Name = string.Empty;
            Birthday = DateTime.MinValue;
        }

        public Physician(PhysicianDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Birthday = p.Birthday;
        }
    }
}
