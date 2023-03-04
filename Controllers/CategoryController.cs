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
            ViewBag.Action = "Camera";
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

        public IActionResult Laptop()
        {
            ViewBag.Action = "Laptop";
            ViewBag.Category = "Laptopy";
            ViewBag.Cover = "https://images.unsplash.com/photo-1520375300877-3500f50d069e?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";

            ViewBag.Products = _laptopService.Get();

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

        public IActionResult Game()
        {
            ViewBag.Action = "Game";
            ViewBag.Category = "Gry";
            ViewBag.Cover = "https://images.unsplash.com/photo-1617195347883-e72aeeef0ae7?ixid=MXwxMjA3fDB8MHxzZWFyY2h8MTR8fGdhbWVzfGVufDB8fDJ8&ixlib=rb-1.2.1&auto=format&fit=crop&w=500&q=60";

            ViewBag.Products = _gameService.Get();

            return View("~/Views/Category/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}