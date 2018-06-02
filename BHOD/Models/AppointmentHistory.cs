using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class AppointmentHistory
    {
        public int Id { get; set; }

        [Required]
        public ShopPersonal Shop { get; set; }
        [Required]
        public PaymentMethod Payment { get; set; }
        [Required]
        public DateTime CheckedOut { get; set; }
        [Required]
        public DateTime? CheckedIn { get; set; }
    }
}
