using System;
using System.Collections.Generic;
using System.Linq;

namespace MedManagementLibrary
{
    public class AppointmentManager
    {
        private static readonly object _lock = new object();
        private static AppointmentManager? _instance;
        public static AppointmentManager Current
        {
            get
            {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppointmentManager();
                    }
                    return _instance;
                }
            }
        }

        private List<Appointment> _appointments;

        private AppointmentManager()
        {
            _appointments = new List<Appointment>
            {
                new Appointment 
                { 
                    ID = 1, 
                    Name = "Joker - Meditation",
                    StartTime = DateTime.Now.AddDays(1).AddHours(9), 
                    EndTime = DateTime.Now.AddDays(1).AddHours(10), 
                    PatientID = 1, 
                    PhysicianId = 1,
                    TreatmentIDs = new List<int> { 1 },
                    Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == 1)
                },
                new Appointment 
                { 
                    ID = 2, 
                    Name = "Harley Quinn - Physical Therapy and Bone Repair",
                    StartTime = DateTime.Now.AddDays(2).AddHours(11), 
                    EndTime = DateTime.Now.AddDays(2).AddHours(12), 
                    PatientID = 2,
                    PhysicianId = 2,
                    TreatmentIDs = new List<int> { 2, 3 },
                    Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == 2)
                }
            };
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments;
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointments.FirstOrDefault(a => a.ID == id);
        }

        public void AddAppointment(Appointment appointment)
        {
            lock(_lock)
            {
                if (appointment.ID <= 0)
                {
                    appointment.ID = _appointments.Any() ? _appointments.Max(a => a.ID) + 1 : 1;
                    _appointments.Add(appointment);
                }
                else
                {
                    var existingAppointment = _appointments.FirstOrDefault(a => a.ID == appointment.ID);
                    if (existingAppointment != null)
                    {
                        // Update existing appointment
                        existingAppointment.Name = appointment.Name;
                        existingAppointment.StartTime = appointment.StartTime;
                        existingAppointment.EndTime = appointment.EndTime;
                        existingAppointment.PatientID = appointment.PatientID;
                        existingAppointment.TreatmentIDs = appointment.TreatmentIDs;
                        existingAppointment.Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == appointment.PatientID);
                    }
                    else
                    {
                        // If ID does not exist, add as new
                        appointment.Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == appointment.PatientID);
                        _appointments.Add(appointment);
                    }
                }
            }
        }

        public void DeleteAppointment(int id)
        {
            lock(_lock)
            {
                var appointmentToRemove = _appointments.FirstOrDefault(a => a.ID == id);
                if (appointmentToRemove != null)
                {
                    _appointments.Remove(appointmentToRemove);
                }
            }
        }
    }
}