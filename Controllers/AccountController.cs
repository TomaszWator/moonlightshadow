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
                RedirectToAction("Index", "SignOut");
            }
            
            var accountViewModel = new AccountViewModel();

            accountViewModel.UserDataViewModel = user.GetUserDataViewModel();
            accountViewModel.ShippingDataViewModel = user.GetShippingDataViewModel();
            accountViewModel.BoughtOrders = user.BoughtOrders;

            if(user.Privileges == true)
                accountViewModel.UsersTransactions = _transactionService.Get();
            
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
                RedirectToAction("Index", "SignOut");
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
                    ModelState.AddModelError("ChangePasswordViewModel.Email", "Podano z??y login");

                    return View(accountViewModel);
                }

                if (accountViewModel.ChangePasswordViewModel.NewPassword != accountViewModel.ChangePasswordViewModel.RepetedPassword)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.RepetedPassword", "Powt??rzone has??o jest r????ne od nowego");

                    return View(accountViewModel);
                }

                if (user.Password.Hash != Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.Password, user.Password.Salt).hash)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.Password", "Has??o niepoprawne");

                    return View(accountViewModel);
                }

                if (user.Password.Hash == Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.NewPassword, user.Password.Salt).hash)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.NewPassword", "Nowe has??o jest takie same jak by??o");

                    return View(accountViewModel);
                }

                user.Password = new Password(Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.NewPassword).ToTuple());

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

                if (user.Password.Hash != Hasher.Encrypt(accountViewModel.ShippingDataViewModel.Password, user.Password.Salt).hash)
                {
                    ModelState.AddModelError("Password", "Has??o niepoprawne");

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

        [HttpGet]
        public IActionResult SetPaymentVerified(string id)
        {
            var transaction = _transactionService.GetBy(id);

            transaction.BoughtOrder.isPaymentVerified = true;

            _transactionService.Update(transaction);

            var user = _userService.GetByLogin(transaction.UserId);

            if(user != null)
            {
                var boughtOrder = user.BoughtOrders.Where(boughtOrder => boughtOrder.TitleTransaction == transaction.BoughtOrder.TitleTransaction).FirstOrDefault();

                var index = user.BoughtOrders.IndexOf(boughtOrder);

                boughtOrder.isPaymentVerified = true;

                user.BoughtOrders[index] = boughtOrder;

                _userService.Update(user);
            }

            _mailSenderService.SendPaymentAcceptedMail(transaction.EmailShipping, transaction.BoughtOrder.TitleTransaction);

            // Po potwierdzeniu p??atno??ci aktualizuje ilo???? kupionych produkt??w
            foreach (var productItemInTransaction in transaction.BoughtOrder.ProductItems)
            {
                if(productItemInTransaction.Category == "Camera")
                {
                    var camera = _cameraService.Get().FirstOrDefault(camera => camera.Id == productItemInTransaction.Id);

                    if(camera != null) camera.BoughtQuantity++;

                    _cameraService.Update(camera.Id, camera);
                }
                else if(productItemInTransaction.Category == "Game")
                {
                    var game = _gameService.Get().FirstOrDefault(game => game.Id == productItemInTransaction.Id);

                    if(game != null) game.BoughtQuantity++;

                    _gameService.Update(game.Id, game);
                }
                else if(productItemInTransaction.Category == "Laptop")
                {
                    var laptop = _laptopService.Get().FirstOrDefault(laptop => laptop.Id == productItemInTransaction.Id);

                    if(laptop != null) laptop.BoughtQuantity++;

                    _laptopService.Update(laptop.Id, laptop);
                }
                else if(productItemInTransaction.Category == "Phone")
                {
                    var phone = _phoneService.Get().FirstOrDefault(phone => phone.Id == productItemInTransaction.Id);

                    if(phone != null) phone.BoughtQuantity++;

                    _phoneService.Update(phone.Id, phone);
                }
            }

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult SetShippment(string id)
        {
            var transaction = _transactionService.GetBy(id);

            transaction.BoughtOrder.isShippment = true;

            _transactionService.Update(transaction);

            var user = _userService.GetByLogin(transaction.UserId);

            if(user != null)
            {
                var boughtOrder = user.BoughtOrders.Where(boughtOrder => boughtOrder.TitleTransaction == transaction.BoughtOrder.TitleTransaction).FirstOrDefault();

                var index = user.BoughtOrders.IndexOf(boughtOrder);

                boughtOrder.isShippment = true;

                user.BoughtOrders[index] = boughtOrder;

                _userService.Update(user);
            }

            _mailSenderService.SendShippingOnTheWayMail(transaction.EmailShipping, transaction.BoughtOrder.TitleTransaction);

            return RedirectToAction("Index", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}