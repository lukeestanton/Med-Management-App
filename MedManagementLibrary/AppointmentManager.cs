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
                }
                return _instance;
            }
        }

        private List<Appointment> _appointments;

        private AppointmentManager()
        {
            _appointments = new List<Appointment>();
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointments;
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointments.FirstOrDefault(a => a.ID == id);
        }

        public bool IsPhysicianAvailable(int physicianId, DateTime desiredStart, DateTime desiredEnd, int? currentAppointmentId = null)
        {
            var overlappingAppointments = _appointments.Where(a =>
                a.PhysicianId == physicianId &&
                (currentAppointmentId == null || a.ID != currentAppointmentId.Value) &&
                a.StartTime < desiredEnd &&
                a.EndTime > desiredStart
            );

            return !overlappingAppointments.Any();
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
                        existingAppointment.Name = appointment.Name;
                        existingAppointment.StartTime = appointment.StartTime;
                        existingAppointment.EndTime = appointment.EndTime;
                        existingAppointment.PatientID = appointment.PatientID;
                        existingAppointment.PhysicianId = appointment.PhysicianId;
                        existingAppointment.TreatmentIDs = appointment.TreatmentIDs;
                        existingAppointment.Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == appointment.PatientID);
                        existingAppointment.Physician = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => p.Id == appointment.PhysicianId);
                    }
                    else
                    {
                        appointment.Patient = PatientManager.Current.GetAllPatients().FirstOrDefault(p => p.ID == appointment.PatientID);
                        appointment.Physician = PhysicianServiceProxy.Current.Physicians.FirstOrDefault(p => p.Id == appointment.PhysicianId);
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
