using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class Barber : ShopPersonal
    {
        [Required]
        public string BarberName { get; set; }
      

    }
}
