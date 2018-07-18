using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Domain.Appointments
{
    public class AppointmentModel
    {
        public string PaymentMethodId { get; set; }
        public string StylistName { get; set; }
        public int PersonalId { get; set; }
        public string ImageUrl { get; set; }
        public int ReservedCount { get; set; }
        public bool IsAppointmentPlaced { get; set; }
    }
}
