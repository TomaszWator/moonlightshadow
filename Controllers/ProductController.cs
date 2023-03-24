using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using MoonlightShadow.Services;
using Extension.ValidModel;

namespace MoonlightShadow.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserService _userService;
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;

        public ProductController(UserService userService,
            CameraService cameraService, 
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

        public IActionResult Index(string category, string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Camera(string id)
        {
            ViewBag.Action = "Camera";

            MoonlightShadow.Models.Products.Camera camera = _cameraService.GetBy(id);

            if(camera == null) return Error();
            
            ViewBag.Product = camera;

            ViewBag.returnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            bool followed = false;
            bool recomended = false;

            if(userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                followed = user.FollowedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
                recomended = user.RecomendedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
            }

            ViewBag.isFollowed = followed;
            ViewBag.isRecomended = recomended;
            ViewBag.similarProducts = _cameraService.Get().Where(cameraInService => cameraInService.IdUserCreated == camera.IdUserCreated && camera.Id != cameraInService.Id).Take(3).ToList();

            return View("~/Views/Product/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Game(string id)
        {
            ViewBag.Action = "Game";

            MoonlightShadow.Models.Products.Game game = _gameService.GetBy(id);

            if(game == null) return Error();
            
            ViewBag.Product = game;

            ViewBag.returnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            bool followed = false;
            bool recomended = false;

            if(userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                followed = user.FollowedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
                recomended = user.RecomendedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
            }

            ViewBag.isFollowed = followed;
            ViewBag.isRecomended = recomended;
            ViewBag.similarProducts = _gameService.Get().Where(gameInService => gameInService.IdUserCreated == game.IdUserCreated && game.Id != gameInService.Id).Take(3).ToList();

            return View("~/Views/Product/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Laptop(string id)
        {
            ViewBag.Action = "Laptop";

            MoonlightShadow.Models.Products.Laptop laptop = _laptopService.GetBy(id);

            if(laptop == null) return Error();
            
            ViewBag.Product = laptop;

            ViewBag.returnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            bool followed = false;
            bool recomended = false;

            if(userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                followed = user.FollowedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
                recomended = user.RecomendedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
            }

            ViewBag.isFollowed = followed;
            ViewBag.isRecomended = recomended;
            ViewBag.similarProducts = _laptopService.Get().Where(laptopInService => laptopInService.IdUserCreated == laptop.IdUserCreated && laptop.Id != laptopInService.Id).Take(3).ToList();

            return View("~/Views/Product/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Phone(string id)
        {
            ViewBag.Action = "Phone";

            MoonlightShadow.Models.Products.Phone phone = _phoneService.GetBy(id);

            if(phone == null) return Error();
            
            ViewBag.Product = phone;

            ViewBag.returnUrl = HttpContext.Request.Path + HttpContext.Request.QueryString;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            bool followed = false;
            bool recomended = false;

            if(userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                followed = user.FollowedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
                recomended = user.RecomendedOrder.Items.Any(o => o.Category == ViewBag.Action && o.Id == id);
            }

            ViewBag.isFollowed = followed;
            ViewBag.isRecomended = recomended;
            ViewBag.similarProducts = _phoneService.Get().Where(phoneInService => phoneInService.IdUserCreated == phone.IdUserCreated && phone.Id != phoneInService.Id).Take(3).ToList();
            
            return View("~/Views/Product/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}