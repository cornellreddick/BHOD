using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHOD.Domain.Selections;
using BHOD.Services;
using BHOD.Data;
using static BHOD.Domain.Selections.PersonalDetailModel;
using BHOD.Domain.Appointments;

namespace BHOD.Controllers
{
    public class SelectionController : Controller
    {
        private IShopPersonal _personal;
        private IAppointment _appointments; 

        public SelectionController(IShopPersonal personal, IAppointment appointments)
        {
            _personal = personal;
            _appointments = appointments;
        }

      
        public IActionResult Index()
        {
            var personalModels = _personal.GetAll();

            var listingResult = personalModels
                .Select(result => new PersonalIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    GetStylistName = result.ShopName,
                    BarberOrHairstylist = _personal.GetBarberOrHairstylist(result.Id),
                    Type = _personal.GetType(result.Id)
                });

            var model = new PersonalIndexModel()
            {
                Personal = listingResult
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var personal = _personal.GetById(id);

            var prebookedAppointments = _appointments.GetCurrentPreBooked(id)
                .Select(a => new PersonalPrebookedModel
                {
                   AppointmentPlaced = _appointments.GetCurrentPreBookedSchedule(a.Id).ToString("d"), 
                    CustomerName = _appointments.GetCurrentPreBookedCustomerName(a.Id)
                });

            var model = new PersonalDetailModel
            {
                PersonalId = id,
                GetStylistName = personal.ShopName,
                Status = personal.Status.Name,
                ImageUrl = personal.ImageUrl,
                BarberOrHairstylist = _personal.GetBarberOrHairstylist(id),
                CurrentLocation = _personal.GetCurrentLocation(id).Name,
                Type = _personal.GetType(id),
                AppointmentHistory = _appointments.GetAppointmentHistory(id),
                LatestAppointment = _appointments.GetAppointment(id),
                CustomerName = _appointments.GetCurrentAppointmentCustomer(id),
                PrebookedAppointment = prebookedAppointments 

            };

            return View(model);  
        }

        public IActionResult Appointment(int id)
        {
            var personal = _personal.GetById(id);
            var model = new AppointmentModel
            {
                PersonalId = id,
                ImageUrl = personal.ShopName,
                PaymentMethodId = "",
                IsAppointmentPlaced = _appointments.IsAppointmentPlaced(id)
            };
            return View(model);
           
        }

        public IActionResult PlaceAppointment(int personalId, int paymentMethodId)
        {
            _appointments.AppointmentIn(personalId, paymentMethodId);
            return RedirectToAction("Detail", new { id = personalId });
        }
    }
}
