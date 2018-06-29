using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHOD.Domain.Selections;
using BHOD.Services;

namespace BHOD.Controllers
{
    public class SelectionController : Controller
    {
        private IShopPersonal _personal;

        public SelectionController(IShopPersonal personal)
        {
            _personal = personal;
        }

        public IActionResult Index()
        {
            var personalModels = _personal.GetAll();

            var listingResult = personalModels
                .Select(result => new PersonalIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    GetStylistName = result.ShopName,
                    BarberOrHairstylist = _personal.GetBarberOrHairstylist(result.Id),
                    Type = _personal.GetType(result.Id)
                });

            var model = new PersonalIndexModel()
            {
                Personal = listingResult
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var personal = _personal.GetById(id)
            return View();
        }
    }
}
