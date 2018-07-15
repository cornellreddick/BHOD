using BHOD.Data;
using BHOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Services
{
    public class AppointmentService : IAppointment
    {
        public void Add(Appointment newAppointment)
        {
            throw new NotImplementedException();
        }

        public void AppointmentIn(int personalId, int paymentMethodId)
        {
            throw new NotImplementedException();
        }

        public void AppointmentOut(int personalId, int paymentMethodId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppointmentHistory> GetAppointmentHistory(int id)
        {
            throw new NotImplementedException();
        }

        public Appointment GetById(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PreBookedAppointments> GetCurrentPreBooked(int id)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentPreBookedCustomerName(int id)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCurrentPreBookedSchedule(int id)
        {
            throw new NotImplementedException();
        }

        public void Reserved(int personalId, int paymentMethodId)
        {
            throw new NotImplementedException();
        }
    }
}
