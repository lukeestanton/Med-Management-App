namespace MedManagementLibrary
{
    public class Appointment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }

        private static int _idCounter = 1;

        public Appointment()
        {
            Name = string.Empty;
            AppointmentDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }
    }
}





