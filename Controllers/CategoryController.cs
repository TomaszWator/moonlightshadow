using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.Services;
using MoonlightShadow.Models.Products;
using MoonlightShadow.Models.ClassHelper;
using MoonlightShadow.ViewModels.Category;

namespace MoonlightShadow.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;

        public CategoryController(CameraService cameraService,
            LaptopService laptopService,
            PhoneService phoneService,
            GameService gameService)
        {
            _cameraService = cameraService;
            _laptopService = laptopService;
            _phoneService = phoneService;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            ViewBag.Action = "Wszystkie produkty";
            ViewBag.Category = "Wszystkie produkty";
            return View();
        }

        public IActionResult Camera()
        {
            ViewBag.Action = "Camera";
            ViewBag.Category = "Aparaty";
            ViewBag.Cover = "https://images.unsplash.com/photo-1617629245105-a1632dd7c14a?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";

            ViewBag.Products = _cameraService.Get();

            return View("~/Views/Category/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Camera(CategoryViewModel categoryViewModel)
        {
            ViewBag.Action = "Camera";
            ViewBag.Category = "Aparaty";
            ViewBag.Cover = "https://images.unsplash.com/photo-1617629245105-a1632dd7c14a?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";

            var cameras = _cameraService.Get();

            if(categoryViewModel.FilterPrice == "max50")
            {
                cameras = cameras.Where(camera => camera.Price <= 50).ToList<Camera>();
            }

            else if(categoryViewModel.FilterPrice == "min50max100")
            {
                cameras = cameras.Where(camera => camera.Price >= 50 && camera.Price <= 100).ToList<Camera>();
            }

            else if(categoryViewModel.FilterPrice == "min100max400")
            {
                cameras = cameras.Where(camera => camera.Price >= 100 && camera.Price <= 400).ToList<Camera>();
            }

            else if(categoryViewModel.FilterPrice == "min400max1000")
            {
                cameras = cameras.Where(camera => camera.Price >= 400 && camera.Price <= 1000).ToList<Camera>();
            }

            else if(categoryViewModel.FilterPrice == "other")
            {
                cameras = cameras.Where(camera => camera.Price >= categoryViewModel.MinimumPrice && 
                                            camera.Price <= categoryViewModel.MaximumPrice).ToList<Camera>();
            }

            if(categoryViewModel.OrderByPrice == "ascending")
            {
                cameras = cameras.OrderBy(camera => camera.Price).ToList<Camera>();
            }
            else if(categoryViewModel.OrderByPrice == "descending")
            {
                cameras = cameras.OrderByDescending(camera => camera.Price).ToList<Camera>();
            }

            ViewBag.Products = cameras;

            return View("~/Views/Category/Index.cshtml");
        }

        public IActionResult Laptop()
        {
            ViewBag.Action = "Laptop";
            ViewBag.Category = "Laptopy";
            ViewBag.Cover = "https://images.unsplash.com/photo-1520375300877-3500f50d069e?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";

            ViewBag.Products = _laptopService.Get();

            return View("~/Views/Category/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Laptop(CategoryViewModel categoryViewModel)
        {
            ViewBag.Action = "Laptop";
            ViewBag.Category = "Laptopy";
            ViewBag.Cover = "https://images.unsplash.com/photo-1520375300877-3500f50d069e?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";

            var laptops = _laptopService.Get();

            if(categoryViewModel.FilterPrice == "max50")
            {
                laptops = laptops.Where(laptop => laptop.Price <= 50).ToList<Laptop>();
            }

            else if(categoryViewModel.FilterPrice == "min50max100")
            {
                laptops = laptops.Where(laptop => laptop.Price >= 50 && laptop.Price <= 100).ToList<Laptop>();
            }

            else if(categoryViewModel.FilterPrice == "min100max400")
            {
                laptops = laptops.Where(laptop => laptop.Price >= 100 && laptop.Price <= 400).ToList<Laptop>();
            }

            else if(categoryViewModel.FilterPrice == "min400max1000")
            {
                laptops = laptops.Where(laptop => laptop.Price >= 400 && laptop.Price <= 1000).ToList<Laptop>();
            }
    
            else if(categoryViewModel.FilterPrice == "other")
            {
                laptops = laptops.Where(laptop => laptop.Price >= categoryViewModel.MinimumPrice && 
                                            laptop.Price <= categoryViewModel.MaximumPrice).ToList<Laptop>();
            }

            if(categoryViewModel.OrderByPrice == "ascending")
            {
                laptops = laptops.OrderBy(laptop => laptop.Price).ToList<Laptop>();
            }
            else if(categoryViewModel.OrderByPrice == "descending")
            {
                laptops = laptops.OrderByDescending(laptop => laptop.Price).ToList<Laptop>();
            }

            ViewBag.Products = laptops;

            return View("~/Views/Category/Index.cshtml");
        }

        public IActionResult Phone()
        {
            ViewBag.Action = "Phone";
            ViewBag.Category = "Telefony";
            ViewBag.Cover = "https://images.unsplash.com/photo-1615391208061-2205dfea6b93?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=634&q=80";

            ViewBag.Products = _phoneService.Get();

            return View("~/Views/Category/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Phone(CategoryViewModel categoryViewModel)
        {
            ViewBag.Action = "Phone";
            ViewBag.Category = "Telefony";
            ViewBag.Cover = "https://images.unsplash.com/photo-1615391208061-2205dfea6b93?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=634&q=80";

            var phones = _phoneService.Get();

            if(categoryViewModel.FilterPrice == "max50")
            {
                phones = phones.Where(phone => phone.Price <= 50).ToList<Phone>();
            }

            else if(categoryViewModel.FilterPrice == "min50max100")
            {
                phones = phones.Where(phone => phone.Price >= 50 && phone.Price <= 100).ToList<Phone>();
            }

            else if(categoryViewModel.FilterPrice == "min100max400")
            {
                phones = phones.Where(phone => phone.Price >= 100 && phone.Price <= 400).ToList<Phone>();
            }

            else if(categoryViewModel.FilterPrice == "min400max1000")
            {
                phones = phones.Where(phone => phone.Price >= 400 && phone.Price <= 1000).ToList<Phone>();
            }

            else if(categoryViewModel.FilterPrice == "other")
            {
                phones = phones.Where(phone => phone.Price >= categoryViewModel.MinimumPrice && 
                                            phone.Price <= categoryViewModel.MaximumPrice).ToList<Phone>();
            }

            if(categoryViewModel.OrderByPrice == "ascending")
            {
                phones = phones.OrderBy(phone => phone.Price).ToList<Phone>();
            }
            else if(categoryViewModel.OrderByPrice == "descending")
            {
                phones = phones.OrderByDescending(phone => phone.Price).ToList<Phone>();
            }

            ViewBag.Products = phones;

            return View("~/Views/Category/Index.cshtml");
        }

        public IActionResult Game()
        {
            ViewBag.Action = "Game";
            ViewBag.Category = "Gry";
            ViewBag.Cover = "https://images.unsplash.com/photo-1617195347883-e72aeeef0ae7?ixid=MXwxMjA3fDB8MHxzZWFyY2h8MTR8fGdhbWVzfGVufDB8fDJ8&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60";

            ViewBag.Products = _gameService.Get();

            return View("~/Views/Category/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Game(CategoryViewModel categoryViewModel)
        {
            ViewBag.Action = "Game";
            ViewBag.Category = "Gry";
            ViewBag.Cover = "https://images.unsplash.com/photo-1617195347883-e72aeeef0ae7?ixid=MXwxMjA3fDB8MHxzZWFyY2h8MTR8fGdhbWVzfGVufDB8fDJ8&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60";

            var games = _gameService.Get();

            if(categoryViewModel.FilterPrice == "max50")
            {
                games = games.Where(game => game.Price <= 50).ToList<Game>();
            }

            else if(categoryViewModel.FilterPrice == "min50max100")
            {
                games = games.Where(game => game.Price >= 50 && game.Price <= 100).ToList<Game>();
            }

            else if(categoryViewModel.FilterPrice == "min100max400")
            {
                games = games.Where(game => game.Price >= 100 && game.Price <= 400).ToList<Game>();
            }

            else if(categoryViewModel.FilterPrice == "min400max1000")
            {
                games = games.Where(game => game.Price >= 400 && game.Price <= 1000).ToList<Game>();
            }

            else if(categoryViewModel.FilterPrice == "other")
            {
                games = games.Where(game => game.Price >= categoryViewModel.MinimumPrice && 
                                            game.Price <= categoryViewModel.MaximumPrice).ToList<Game>();
            }

            if(categoryViewModel.OrderByPrice == "ascending")
            {
                games = games.OrderBy(game => game.Price).ToList<Game>();
            }
            else if(categoryViewModel.OrderByPrice == "descending")
            {
                games = games.OrderByDescending(game => game.Price).ToList<Game>();
            }

            ViewBag.Products = games;

            return View("~/Views/Category/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}