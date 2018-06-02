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
            return _context.ShopPersonals
                .Include(personal => personal.Status)
                .Include(personal => personal.Location)
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

        public string ShopName(int id)
        {
            if (_context.ShopPersonals.Any(a => a.Id == id))
            {
                return _context.ShopPersonals
                    .FirstOrDefault(a => a.Id == id).ShopName;
            }
            else
            {
                return "";
            }
        }

        public string GetCity(int id)
        {
            if (_context.Barbers.Any(a => a.Id == id))
            {
                return _context.Barbers
                    .FirstOrDefault(a => a.Id == id).City;
            }
            else
            {
                return "";
            }
        }

        public string GetState(int id)
        {
            if (_context.ShopPersonals.Any(a => a.Id == id))
            {
                return _context.ShopPersonals
                    .FirstOrDefault(a => a.Id == id).State;
            }
            else
            {
                return "";
            }
        }



        public string GetBarberOrHairstylist(int id)
        {
            var isBarber = _context.ShopPersonals.OfType<Barber>()
                .Where(personal => personal.Id == id);

            var isHairstylist = _context.ShopPersonals.OfType<Hairstylist>()
                .Where(personal => personal.Id == id);

            return isBarber != null?
                
                  _context.Barbers.FirstOrDefault(barber => barber.Id == id).BarberFirstName
                : _context.Hairstylists.FirstOrDefault(hairstylist => hairstylist.Id == id).HairstylistsFirstName ??
                  "Unknown";
            
        }

        public Shop GetCurrentLocation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
