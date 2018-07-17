using BHOD.Data;
using BHOD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Services
{
    public class AppointmentService : IAppointment
    {
        private BHODContext _context;

        public AppointmentService(BHODContext context)
        {
            _context = context;
        }

        public void Add(Appointment newAppointment)
        {
            _context.Add(newAppointment);
            _context.SaveChanges();
        }
        
        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointmentses;
        }

        public IEnumerable<AppointmentHistory> GetAppointmentHistory(int id)
        {
            return _context.AppointmentHistories
                .Include(a => a.Shop)//Added to show a value here.
                .Include(a => a.Payment)
                .Where(a => a.Shop.Id == id);
        }

        public Appointment GetById(int appointmentId)
        {
            return GetAll().FirstOrDefault(appoinment => appoinment.Id == appointmentId);
        }

        public IEnumerable<PreBookedAppointments> GetCurrentPreBooked(int id)
        {
            return _context.PreBookedAppointmentses
                 .Include(a => a.ShopPersonal)
                 .Where(a => a.ShopPersonal.Id == id);
        }

        public Appointment GetAppointment(int personalId)
        {
            return _context.Appointmentses
                .Where(a => a.ShopPersonal.Id == personalId)
                .OrderByDescending(a => a.Since)
                .FirstOrDefault();
        }

        private void CloseExistingAppointmentHistory(int personalId, DateTime now)
        {
            var history = _context.AppointmentHistories
                .FirstOrDefault(a => a.Shop.Id == personalId && a.CheckedIn == null);

            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }

            _context.SaveChanges();
        }

        private void RemoveExistingCheckouts(int personalId)
        {
            var appointment = _context.Appointmentses
                .FirstOrDefault(app => app.ShopPersonal.Id == personalId);

            if (appointment != null)
            {
                _context.Remove(appointment);
            }
            _context.SaveChanges();
        }

        private void UpdatePersonalStatus(int personalId, string s)
        {
            var item = _context.ShopPersonals
                .FirstOrDefault(p => p.Id == personalId);

                _context.Update(item);

            item.Status = _context.Statuses
            .FirstOrDefault(status => status.Name == "Available");

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
            var now = DateTime.Now;

            var personal = _context.ShopPersonals
                .FirstOrDefault(p => p.Id == personalId);

            var payment = _context.PaymentMethods
                .FirstOrDefault(m => m.Id == paymentMethodId);
        }

        public void AppointmentIn(int personalId, int paymentMethodId)
        {
            var now = DateTime.Now;

            var item = _context.ShopPersonals
                .FirstOrDefault(p => p.Id == personalId);

           
            RemoveExistingCheckouts(personalId);

            CloseExistingAppointmentHistory(personalId, now);

            var currentPreBookedAppointments = _context.PreBookedAppointmentses
                .Include(p => p.ShopPersonal)
                .Include(p => p.PaymentMethod)
                .Where(p => p.ShopPersonal.Id == personalId);

            if (currentPreBookedAppointments.Any())
            {
                PreBookedAppointmentsToFirstPlaced(personalId, currentPreBookedAppointments);
            }

            UpdatePersonalStatus(personalId, "Available");
            _context.SaveChanges();

        }

        private void PreBookedAppointmentsToFirstPlaced(int personalId, IQueryable<PreBookedAppointments> currentPreBookedAppointments)
        {
            var earliestPlaced = currentPreBookedAppointments
                .OrderBy(prebooked => prebooked.PreBookedPlaced)
                .FirstOrDefault();

            var payment = earliestPlaced.PaymentMethod;

            _context.Remove(earliestPlaced);
            _context.SaveChanges();
            AppointmentOut(personalId, payment.Id);

        }

        public void AppointmentOut(int personalId, int paymentMethodId)
        {
            if (IsAppointmentPlaced(personalId))
            {
                return; 
            }

            var item = _context.ShopPersonals
                .FirstOrDefault(p => p.Id == personalId);

            UpdatePersonalStatus(personalId, "Reserved");

            var PaymentMethod = _context.PaymentMethods
                .Include(payment => payment.Appointments)
                .FirstOrDefault(payment => payment.Id == paymentMethodId);

            var now = DateTime.Now;

            var appointment = new Appointment
            {
                ShopPersonal = item,
                PaymentMethod = PaymentMethod,
                Since = now,
                Until = GetDefaultAppointmenTime(now)

            };

            _context.Add(appointment);

            var appointmentHistory = new AppointmentHistory
            {
                CheckedOut = now,
                Shop = item,
                Payment = PaymentMethod

            };

            _context.Add(appointmentHistory);
            _context.SaveChanges();
        }

        private DateTime GetDefaultAppointmenTime(DateTime now)
        {
            return now.AddDays(7);
        }

        private bool IsAppointmentPlaced(int personalId)
        {
            var isAppointmentPlaced = _context.Appointmentses
                .Where(ap => ap.ShopPersonal.Id == personalId)
                .Any();

            return isAppointmentPlaced;
        }
    }
}
