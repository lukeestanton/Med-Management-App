namespace MedManagementLibrary
{
    public class Physician
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<string> Specializations { get; set; }

        public Physician()
        {
            Name = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specializations = new List<string>();
        }
    }
}



