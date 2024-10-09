public class AppointmentManager
{
    private List<Appointment> appointments = new List<Appointment>();

    public bool ScheduleAppointment(Patient patient, Physician physician, DateTime appointmentDate)
    {
        if (!IsWithinBusinessHours(appointmentDate) || IsDoubleBooked(physician, appointmentDate))
        {
            Console.WriteLine("You can't rescedule that!!");
            return false;
        }

        appointments.Add(new Appointment(patient, physician, appointmentDate));
        Console.WriteLine($"Appointment scheduled for {patient.Name} with Dr. {physician.Name} on {appointmentDate}");
        return true;
    }

    private bool IsWithinBusinessHours(DateTime appointmentDate)
    {
        if (appointmentDate.DayOfWeek == DayOfWeek.Saturday || appointmentDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }

        TimeSpan start = new TimeSpan(8, 0, 0); // 8am
        TimeSpan end = new TimeSpan(17, 0, 0); // 5pm
        TimeSpan appointmentTime = appointmentDate.TimeOfDay;

        return appointmentTime >= start && appointmentTime <= end;
    }

    private bool IsDoubleBooked(Physician physician, DateTime appointmentDate)
    {
        foreach (var appointment in appointments)
        {
            if (appointment.Physician == physician && appointment.AppointmentDate == appointmentDate)
            {
                return true;
            }
        }
        return false;
    }
}
