using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHOD.Data;
using BHOD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BHOD.Services
{
    public class ShopPersonalServices : IShopPersonal
    {
        private BHODContext _context;

        public ShopPersonalServices(BHODContext context)
        {
            _context = context;
        }

        public IEnumerable<ShopPersonal> GetAll()
        {
            return _context.ShopPersonals
                .Include(personal => personal.Status)
                .Include(personal => personal.Location);
        }

        public ShopPersonal GetById(int id)
        {
            return GetAll()
                .FirstOrDefault(personal => personal.Id == id);
        }

        public void Add(ShopPersonal newPersonal)
        {
            _context.Add(newPersonal);
            _context.SaveChanges();
        }


        public string GetType(int id)
        {
            var barber = _context.ShopPersonals.OfType<Barber>()
                .Where(b => b.Id == id);

            return barber.Any() ? "Barber" : "Hairstylist";
        }

    
        public string GetStylistName(int id)
        {
            return _context.ShopPersonals.FirstOrDefault(a => a.Id == id)
                .ShopName;
                
        }

        public string GetBarberOrHairstylist(int id)
        {
            var isBarber = _context.ShopPersonals.OfType<Barber>()
                .Where(personal => personal.Id == id).Any();

            var isHairstylist = _context.ShopPersonals.OfType<Hairstylist>()
                .Where(personal => personal.Id == id).Any();

            return isBarber ?
                
                  _context.Barbers.FirstOrDefault(barber => barber.Id == id).BarberName
                : _context.Hairstylists.FirstOrDefault(hairstylist => hairstylist.Id == id).HairstylistName ??
                  "Unknown";
            
        }

        public Shop GetCurrentLocation(int id)
        {
            return _context.ShopPersonals.FirstOrDefault(personal => personal.Id == id).Location;
        }
    }
}
