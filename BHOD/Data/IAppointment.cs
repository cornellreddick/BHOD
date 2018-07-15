using BHOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Data
{
    interface IAppointment
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int AppointmentId);
        void Add(Appointment newAppointment);
        void AppointmentOut(int personalId, int paymentMethodId);
        void AppointmentIn(int personalId, int paymentMethodId);
        IEnumerable<AppointmentHistory> GetAppointmentHistory(int id);

        void Reserved(int personalId, int paymentMethodId);
        string GetCurrentPreBookedCustomerName(int id);
        DateTime GetCurrentPreBookedSchedule(int id);
        IEnumerable<PreBookedAppointments> GetCurrentPreBooked(int id);
    }
}
