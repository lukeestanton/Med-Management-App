namespace MedManagementLibrary
{
    public class Physician
    {
        public int ID { get; set; }  // New ID property
        public string Name { get; set; }
        public string LicenseNumber { get; set; }  // Keep as regular property
        public DateTime GraduationDate { get; set; }
        public List<string> Specializations { get; set; }

        public Physician()
        {
            Name = string.Empty;
            LicenseNumber = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specializations = new List<string>();
        }

        // Override ToString to return ID and Name
        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }
    }
}



