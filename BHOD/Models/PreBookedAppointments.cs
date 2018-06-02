using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class PreBookedAppointments
    {
        public int Id { get; set; }
        public virtual ShopPersonal ShopPersonal { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public DateTime PreBookedPlaced { get; set; }
    }
}
