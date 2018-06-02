using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        public decimal Charges { get; set; }

        public DateTime Created { get; set; }

        public virtual IEnumerable<Appointment> Appointments { get; set; }
    }
}
