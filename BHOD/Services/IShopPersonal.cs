﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHOD.Models;

namespace BHOD.Services
{
    public interface IShopPersonal
    {
        IEnumerable<ShopPersonal> GetAll();
        ShopPersonal GetById(int id);

        void Add(ShopPersonal newPersonal);
        string GetBarberOrHairstylist(int id);
        string GetType(int id);
        string ShopName(int id);
        string GetCity(int id);
        string GetState(int id);

        Shop GetCurrentLocation(int id);
        //commit
    }
}
