using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }

        
        public virtual IEnumerable<Customer> Hairstylist { get; set; }
        public virtual IEnumerable<ShopPersonal> ShopPersonal { get; set; }

        public string ImageUrl { get; set; }
    }
}
