using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace BHOD.Models
{
    public class Customer
    {
        public  int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
        public  virtual  Shop Shop { get; set; }
    }
}
