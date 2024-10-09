namespace MedManagementLibrary {
    public class Appointment
{
    public Patient Patient { get; set; }
    public Physician Physician { get; set; }
    public DateTime AppointmentDate { get; set; }

    public Appointment(Patient patient, Physician physician, DateTime appointmentDate)
    {
        Patient = patient;
        Physician = physician;
        AppointmentDate = appointmentDate;
    }
}
}


