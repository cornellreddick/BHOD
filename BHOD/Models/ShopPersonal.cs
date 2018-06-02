using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public abstract class ShopPersonal
    {
        public int Id { get; set; }
        [Required]
        public string ShopName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Status Status { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public virtual Shop Location { get; set; }
    }
}
