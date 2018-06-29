using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class Hairstylist : ShopPersonal
    {
        [Required]
        public string HairstylistName { get; set; }
        
    }
}
