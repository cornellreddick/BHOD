using BHOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Data
{
    interface IAppointment
    {
        void Add(Appointment newAppointment);

        IEnumerable<Appointment> GetAll();
        IEnumerable<AppointmentHistory> GetAppointmentHistory(int id);
        IEnumerable<PreBookedAppointments> GetCurrentPreBooked(int id);

        Appointment GetById(int AppointmentId);
        Appointment GetAppointment(int personalId);
        string GetCurrentAppointmentCustomer(int personalId);
        string GetCurrentPreBookedCustomerName(int id);

        void AppointmentOut(int personalId, int paymentMethodId);
        void AppointmentIn(int personalId, int paymentMethodId);
        void Reserved(int personalId, int paymentMethodId);
        DateTime GetCurrentPreBookedSchedule(int id);
       
    }
}
