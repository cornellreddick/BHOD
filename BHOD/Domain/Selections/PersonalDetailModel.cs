using BHOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Domain.Selections
{
    public class PersonalDetailModel
    {
        public int PersonalId { get; set; }
        public string GetStylistName { get; set; }
        public string BarberOrHairstylist { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public string CurrentLocation { get; set; }
        public string ImageUrl { get; set; }
        public string CustomerName { get; set; }
        public Appointment LatestAppointment { get; set; }
        public IEnumerable<AppointmentHistory> AppointmentHistory { get; set; }
        public IEnumerable<PersonalPrebookedModel> PrebookedAppointment { get; set; }

        public class PersonalPrebookedModel
        {
            public string CustomerName { get; set; }
            public string AppointmentPlaced { get; set; }
        }

    }
}
