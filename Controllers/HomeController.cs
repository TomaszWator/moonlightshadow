using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.Services;


// mongodb://MoonlightShadow:NUd1E0qRQpC9K76Z@moonlightshadowcluster-shard-00-00.6nlfm.mongodb.net:27017,moonlightshadowcluster-shard-00-01.6nlfm.mongodb.net:27017,moonlightshadowcluster-shard-00-02.6nlfm.mongodb.net:27017/MoonlightShadowDb?ssl=true&replicaSet=atlas-68mu11-shard-0&authSource=admin&retryWrites=true&w=majority
namespace MoonlightShadow.Controllers
{
    public class HomeController : Controller
    {
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;

        public HomeController(CameraService cameraService,
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
            var cameraList = _cameraService.Get();
            var laptopList = _laptopService.Get();
            var phoneList = _phoneService.Get();
            var gameList = _gameService.Get();

            var productList = new List<Product>();

            foreach (var camera in cameraList)
            {
                productList.Add(camera as Product);
            }

            foreach (var laptop in laptopList)
            {
                productList.Add(laptop as Product);
            }
            
            foreach (var phone in phoneList)
            {
                productList.Add(phone as Product);
            }

            foreach (var game in gameList)
            {
                productList.Add(game as Product);
            }

            ViewBag.ProductBestsellerList = productList.OrderByDescending(product => product.BoughtQuantity).Take(4);
            ViewBag.ProductLastAddedList = productList.OrderByDescending(product => product.TimeCreated).Take(4);
            ViewBag.ProductRecomendedList = productList.OrderByDescending(product => product.RecomendedQuantity).Take(4);
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
