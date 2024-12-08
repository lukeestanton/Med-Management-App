using System;
using MedManagementLibrary.DTO;

namespace MedManagementLibrary
{
    public class Physician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        public Physician() { }

        public Physician(PhysicianDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Birthday = dto.Birthday;
        }
    }
}
