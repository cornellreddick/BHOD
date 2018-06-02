using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class Appointment
    {
        public  int Id { get; set; }
        [Required]
        public ShopPersonal ShopPersonal { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}
