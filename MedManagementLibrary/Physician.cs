using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedManagementLibrary
{
    public class Physician
    {
        public override string ToString()
        {
            return Display;
        }

        public string Display
        {
            get
            {
                return $"[{Id}] {Name}";
            }
        }
        public int Id { get; set; }
        private string? name;
        public string Name { 
            get {
                return name ?? string.Empty;
            }

            set {
                name = value;
            }
        }
        public DateTime Birthday {  get; set; }

        public Physician()
        {
            Name = string.Empty;
            Birthday = DateTime.MinValue;
        }
    }
}
