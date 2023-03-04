using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Extension.ValidModel;
using MoonlightShadow.ViewModels;
using MoonlightShadow.ViewModels.Products;

namespace MoonlightShadow.Controllers
{
    public class SaleController : Controller
    {
        private readonly UserService _userService;
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;

        public SaleController(UserService userService, CameraService cameraService,
            LaptopService laptopService,
            PhoneService phoneService,
            GameService gameService)
        {
            _userService = userService;
            _cameraService = cameraService;
            _laptopService = laptopService;
            _phoneService = phoneService;
            _gameService = gameService;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var user = _userService.GetByEmail(userEmail);

            if (user.IsNull())
            {
                RedirectToAction("Index", "SignOut");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ProductsViewModel productsViewModel)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            var user = _userService.GetByEmail(userEmail);

            if (user.IsNull())
            {
                RedirectToAction("Index", "SignOut");
            }

            if(productsViewModel.TypeOfModel == "Camera")
            {
                if (TryValidateModel(productsViewModel.Camera) == false)
                {
                    return View(productsViewModel);
                }

                _cameraService.Create(productsViewModel.Camera);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSaleCamera";
            }

            if(productsViewModel.TypeOfModel == "Game")
            {
                if (TryValidateModel(productsViewModel.Game) == false)
                {
                    return View(productsViewModel);
                }

                _gameService.Create(productsViewModel.Game);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSaleGame";
            }

            if(productsViewModel.TypeOfModel == "Laptop")
            {
                if (TryValidateModel(productsViewModel.Laptop) == false)
                {
                    return View(productsViewModel);
                }

                _laptopService.Create(productsViewModel.Laptop);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSaleLaptop";
            }

            if(productsViewModel.TypeOfModel == "Phone")
            {
                if (TryValidateModel(productsViewModel.Phone) == false)
                {
                    return View(productsViewModel);
                }

                _phoneService.Create(productsViewModel.Phone);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSalePhone";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
