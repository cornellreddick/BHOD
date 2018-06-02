using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BHOD.Models
{
    public class ShopHours
    {
        public int Id { get; set; }
        public Shop Location { get; set; }

        [Range(0, 6)]
        public int DayOfWeek { get; set; }

        [Range(0, 21)]
        public int OpenTime { get; set; }

        [Range(0, 21)]
        public int CloseTime { get; set; }
    }
}
