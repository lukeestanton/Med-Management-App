namespace MedManagementLibrary
{
    public class Appointment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PatientID { get; set; }
        public int PhysicianID { get; set; }
        public List<int> TreatmentIDs { get; set; }

        public Patient? Patient { get; set; }
        public Physician? Physician { get; set; }

        public Appointment()
        {
            Name = string.Empty;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now.AddHours(1);
            PatientID = 0;
            PhysicianID = 0;
            TreatmentIDs = new List<int>();
            Patient = null;
            Physician = null;
        }
    }
}

