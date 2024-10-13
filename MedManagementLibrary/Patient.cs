namespace MedManagementLibrary
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public List<string> Diagnoses { get; set; }
        public List<string> Prescriptions { get; set; }

        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            Race = string.Empty;
            Gender = string.Empty;
            Diagnoses = new List<string>();
            Prescriptions = new List<string>();
        }
    }
}
