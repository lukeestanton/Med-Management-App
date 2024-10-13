namespace MedManagementLibrary
{
    public class Appointment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Appointment()
        {
            Name = string.Empty;
            AppointmentDate = DateTime.Now;
        }
    }
}





