using System;

namespace MedManagementLibrary.DTO
{
    public class PhysicianDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        public PhysicianDTO() { }

        public PhysicianDTO(Physician physician)
        {
            Id = physician.Id;
            Name = physician.Name;
            Birthday = physician.Birthday;
        }
    }
}


