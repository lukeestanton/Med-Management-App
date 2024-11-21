namespace MedManagementLibrary
{
    public class Physician
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<int> SpecializationIDs { get; set; }

        public Physician()
        {
            Name = string.Empty;
            GraduationDate = DateTime.MinValue;
            SpecializationIDs = new List<int>();
        }
    }
}



