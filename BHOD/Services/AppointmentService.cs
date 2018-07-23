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

        private void UpdatePersonalStatus(int personalId, string newStatus)
        {
            var item = _context.ShopPersonals
                .FirstOrDefault(p => p.Id == personalId);

                _context.Update(item);

            item.Status = _context.Statuses
            .FirstOrDefault(status => status.Name == newStatus);

        }
        public string GetCurrentPreBookedCustomerName(int Prebookedid)
        {
            var reserved = _context.PreBookedAppointmentses
                .Include(pb => pb.ShopPersonal)
                .Include(pb => pb.PaymentMethod)
                .FirstOrDefault(pb => pb.Id == Prebookedid);

            var paymentId = reserved?.ShopPersonal.Id;

            var customer = _context.Customers.Include(c => c.PaymentMethod)
                .FirstOrDefault(c => c.PaymentMethod.Id == paymentId);

            return customer?.FirstName + " " + customer.LastName;
        }

        public DateTime GetCurrentPreBookedSchedule(int Prebookedid)
        {
            return _context.PreBookedAppointmentses
                  .Include(pb => pb.ShopPersonal)
                  .Include(pb => pb.PaymentMethod)
                  .FirstOrDefault(pb => pb.Id == Prebookedid)
                  .PreBookedPlaced;


        }

        public void Reserved(int personalId, int paymentMethodId)
        {
            var now = DateTime.Now;

            var personal = _context.ShopPersonals
                .Include(p => p.Status)
                .FirstOrDefault(p => p.Id == personalId);

            var payment = _context.PaymentMethods
                .FirstOrDefault(m => m.Id == paymentMethodId);

            if (personal.Status.Name == "Available")
            {
                UpdatePersonalStatus(personalId, "Reserved");
            }

            var prebooked = new PreBookedAppointments
            {
                PreBookedPlaced = now,
                ShopPersonal = personal,
                PaymentMethod = payment
            };

            _context.Add(prebooked);
            _context.SaveChanges();
        }

        public void AppointmentIn(int personalId)
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
                return;
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
                CheckedIn = now,
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

        public bool IsAppointmentPlaced(int personalId)
        {
            var isAppointmentPlaced = _context.Appointmentses
                .Where(ap => ap.ShopPersonal.Id == personalId)
                .Any();

            return isAppointmentPlaced;
        }

        public string GetCurrentAppointmentCustomer(int personalId)
        {

            var appointment = GetAppointmentByPersonalId(personalId);
            if (appointment == null)
            {
                return "c n";
            };

            var paymentId = appointment.PaymentMethod.Id;

            var customer = _context.Customers
                .Include(c => c.PaymentMethod)
                .FirstOrDefault(c => c.PaymentMethod.Id == paymentId);

                return customer.FirstName + " " + customer.LastName;
            
        }

        private Appointment GetAppointmentByPersonalId(int personalId)
        {
            return _context.Appointmentses
                 .Include(a => a.ShopPersonal)
                 .Include(a => a.PaymentMethod)
                 .FirstOrDefault(a => a.ShopPersonal.Id == personalId);

            
        }

      
    }
}
