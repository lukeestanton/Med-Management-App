using System;
using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class AppointmentManager
    {
        private static AppointmentManager? _current;
        public static AppointmentManager Current => _current ??= new AppointmentManager();

        private List<Appointment> _appointments;

        private AppointmentManager()
        {
            _appointments = new List<Appointment>
            {
                new Appointment { ID = 1, Name = "Check-up with Dr. Smith", AppointmentDate = DateTime.Now.AddDays(1) },
                new Appointment { ID = 2, Name = "Consultation with Dr. Johnson", AppointmentDate = DateTime.Now.AddDays(2) }
            };
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments;
        }

        public void AddAppointment(Appointment appointment)
        {
            bool isAdd = false;

            if (appointment.ID <= 0)
            {
                appointment.ID = _appointments.Any() ? _appointments.Max(a => a.ID) + 1 : 1;
                isAdd = true; 
            }
            if (isAdd) 
            {
                _appointments.Add(appointment);
                Console.WriteLine($"New Appointment added: {appointment.Name}, ID: {appointment.ID}");
            }
            else
            {
                var existingAppointment = _appointments.FirstOrDefault(a => a.ID == appointment.ID);
                if (existingAppointment != null)
                {
                    existingAppointment.Name = appointment.Name;
                    existingAppointment.AppointmentDate = appointment.AppointmentDate;
                    Console.WriteLine($"Appointment updated: {existingAppointment.Name}, ID: {existingAppointment.ID}");
                }
            }
        }

        public void DeleteAppointment(int id)
        {
            var appointmentToRemove = _appointments.FirstOrDefault(a => a.ID == id);
            if (appointmentToRemove != null)
            {
                _appointments.Remove(appointmentToRemove);
            }
        }
    }
}
