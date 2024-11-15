namespace MedManagementLibrary
{
    public class Appointment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PatientID { get; set; }

        public Appointment()
        {
            Name = string.Empty;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            PatientID = 0;
        }
    }
}





