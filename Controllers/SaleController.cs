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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MoonlightShadow.Controllers
{
    public class SaleController : Controller
    {
        private readonly UserService _userService;
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;
        private IHostingEnvironment _environment;

        public SaleController(UserService userService, 
            CameraService cameraService,
            LaptopService laptopService,
            PhoneService phoneService,
            GameService gameService,
            IHostingEnvironment environment)
        {
            _userService = userService;
            _cameraService = cameraService;
            _laptopService = laptopService;
            _phoneService = phoneService;
            _gameService = gameService;
            _environment = environment;

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
        public async Task<IActionResult> Index(ProductsViewModel productsViewModel, List<IFormFile> postedFiles)
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

                productsViewModel.Camera.Category = "Camera";
                productsViewModel.Camera.IdUserCreated = user.Login;
                productsViewModel.Camera.TimeCreated = DateTime.Now;

                var camera = _cameraService.Create(productsViewModel.Camera);

                string wwwPath = _environment.WebRootPath;
                string contentPath = _environment.ContentRootPath;
        
                string path = Path.Combine(_environment.WebRootPath, @"uploads\camera\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                camera.ImagesPath = new List<string>();

                int fileNumber = 0;

                foreach (var file in postedFiles)
                {
                    var extension = Path.GetExtension(file.FileName);
                        
                    using (var stream = new FileStream(path + camera.Id + fileNumber + extension, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    camera.ImagesPath.Add("../uploads/camera/" + camera.Id + fileNumber + extension);

                    fileNumber++;
                }
                _cameraService.Update(camera.Id, camera);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSaleCamera";
            }

            if(productsViewModel.TypeOfModel == "Game")
            {
                if (TryValidateModel(productsViewModel.Game) == false)
                {
                    return View(productsViewModel);
                }

                productsViewModel.Game.Category = "Game";
                productsViewModel.Game.IdUserCreated = user.Login;
                productsViewModel.Game.TimeCreated = DateTime.Now;

                var game = _gameService.Create(productsViewModel.Game);

                string wwwPath = _environment.WebRootPath;
                string contentPath = _environment.ContentRootPath;
        
                string path = Path.Combine(_environment.WebRootPath, @"uploads/game/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                game.ImagesPath = new List<string>();

                int fileNumber = 0;

                foreach (var file in postedFiles)
                {
                    var extension = Path.GetExtension(file.FileName);
                        
                    using (var stream = new FileStream(path + game.Id + fileNumber + extension, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    game.ImagesPath.Add("../uploads/game/" + game.Id + fileNumber + extension);

                    fileNumber++;
                }
                _gameService.Update(game.Id, game);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSaleGame";
            }

            if(productsViewModel.TypeOfModel == "Laptop")
            {
                if (TryValidateModel(productsViewModel.Laptop) == false)
                {
                    return View(productsViewModel);
                }

                productsViewModel.Laptop.Category = "Laptop";
                productsViewModel.Laptop.IdUserCreated = user.Login;
                productsViewModel.Laptop.TimeCreated = DateTime.Now;

                var laptop = _laptopService.Create(productsViewModel.Laptop);

                string wwwPath = _environment.WebRootPath;
                string contentPath = _environment.ContentRootPath;
        
                string path = Path.Combine(_environment.WebRootPath, @"uploads\laptop\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                laptop.ImagesPath = new List<string>();

                int fileNumber = 0;

                foreach (var file in postedFiles)
                {
                    var extension = Path.GetExtension(file.FileName);
                        
                    using (var stream = new FileStream(path + laptop.Id + fileNumber + extension, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    laptop.ImagesPath.Add("../uploads/laptop/" + laptop.Id + fileNumber + extension);

                    fileNumber++;
                }
                _laptopService.Update(laptop.Id, laptop);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmSaleLaptop";
            }

            if(productsViewModel.TypeOfModel == "Phone")
            {
                if (TryValidateModel(productsViewModel.Phone) == false)
                {
                    return View(productsViewModel);
                }

                productsViewModel.Phone.Category = "Phone";
                productsViewModel.Phone.IdUserCreated = user.Login;
                productsViewModel.Phone.TimeCreated = DateTime.Now;

                var phone = _phoneService.Create(productsViewModel.Phone);

                string wwwPath = _environment.WebRootPath;
                string contentPath = _environment.ContentRootPath;
        
                string path = Path.Combine(_environment.WebRootPath, @"uploads\phone\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                phone.ImagesPath = new List<string>();

                int fileNumber = 0;

                foreach (var file in postedFiles)
                {
                    var extension = Path.GetExtension(file.FileName);
                        
                    using (var stream = new FileStream(path + phone.Id + fileNumber + extension, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    phone.ImagesPath.Add("../uploads/phone/" + phone.Id + fileNumber + extension);

                    fileNumber++;
                }
                _phoneService.Update(phone.Id, phone);

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
