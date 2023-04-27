using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.ViewModels;
using MoonlightShadow.Services;
using MoonlightShadow.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Extension.Security;
using Extension.ValidModel;
using System.Security.Claims;
using WebApi.Services;

namespace MoonlightShadow.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;
        private readonly TransactionService _transactionService;
        private readonly MailSenderService _mailSenderService;
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;

        public AccountController(UserService userService, 
                                OrderService orderService, 
                                TransactionService transactionService,
                                MailSenderService mailSenderService,
                                CameraService cameraService,
                                LaptopService laptopService,
                                PhoneService phoneService,
                                GameService gameService)
        {
            _userService = userService;
            _orderService = orderService;
            _transactionService = transactionService;
            _mailSenderService = mailSenderService;
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
                RedirectToAction("Index", "Logout");
            }
            
            var accountViewModel = new AccountViewModel();

            accountViewModel.UserDataViewModel = user.GetUserDataViewModel();
            accountViewModel.ShippingDataViewModel = user.GetShippingDataViewModel();
            accountViewModel.BoughtOrders = user.BoughtOrders;
            
            return View(accountViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AccountViewModel accountViewModel)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            
            var user = _userService.GetByEmail(userEmail);

            if (user.IsNull())
            {
                RedirectToAction("Index", "Login");
            }

            accountViewModel.UserDataViewModel = user.GetUserDataViewModel();
            accountViewModel.BoughtOrders = user.BoughtOrders;

            if (accountViewModel.TypeOfModel == "ChangePasswordViewModel")
            {
                if (TryValidateModel(accountViewModel.ChangePasswordViewModel) == false)
                {
                    return View(accountViewModel);
                }

                if (user.Email != accountViewModel.ChangePasswordViewModel.Email)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.Email", "Podano zły login");

                    return View(accountViewModel);
                }

                if (accountViewModel.ChangePasswordViewModel.NewPassword != accountViewModel.ChangePasswordViewModel.RepetedPassword)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.RepetedPassword", "Powtórzone hasło jest różne od nowego");

                    return View(accountViewModel);
                }

                if (user.PasswordHash != Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.Password))
                {
                    ModelState.AddModelError("ChangePasswordViewModel.Password", "Hasło niepoprawne");

                    return View(accountViewModel);
                }

                if (user.PasswordHash == Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.NewPassword))
                {
                    ModelState.AddModelError("ChangePasswordViewModel.NewPassword", "Nowe hasło jest takie same jak było");

                    return View(accountViewModel);
                }

                user.PasswordHash = Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.NewPassword);

                _userService.Update(user);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmChangePassword";

                return View(accountViewModel);
            }

            else if (accountViewModel.TypeOfModel == "ShippingDataViewModel")
            {
                if (TryValidateModel(accountViewModel.ShippingDataViewModel) == false)
                {
                    return View(accountViewModel);
                }

                if (user.PasswordHash != Hasher.Encrypt(accountViewModel.ShippingDataViewModel.Password))
                {
                    ModelState.AddModelError("Password", "Hasło niepoprawne");

                    return View(accountViewModel);
                }

                user.Update(accountViewModel.ShippingDataViewModel);

                _userService.Update(user);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmUpdateShippingData";

                accountViewModel.UserDataViewModel = user.GetUserDataViewModel();

                return View(accountViewModel);
            }
            
            return View(accountViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}