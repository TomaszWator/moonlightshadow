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

        public AccountController(UserService userService, 
                                OrderService orderService, 
                                TransactionService transactionService,
                                MailSenderService mailSenderService)
        {
            _userService = userService;
            _orderService = orderService;
            _transactionService = transactionService;
            _mailSenderService = mailSenderService;
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
                    ModelState.AddModelError("ChangePasswordViewModel.Email", "Podano zły login");

                    return View(accountViewModel);
                }

                if (accountViewModel.ChangePasswordViewModel.NewPassword != accountViewModel.ChangePasswordViewModel.RepetedPassword)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.RepetedPassword", "Powtórzone hasło jest różne od nowego");

                    return View(accountViewModel);
                }

                if (user.Password.Hash != Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.Password, user.Password.Salt).hash)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.Password", "Hasło niepoprawne");

                    return View(accountViewModel);
                }

                if (user.Password.Hash == Hasher.Encrypt(accountViewModel.ChangePasswordViewModel.NewPassword, user.Password.Salt).hash)
                {
                    ModelState.AddModelError("ChangePasswordViewModel.NewPassword", "Nowe hasło jest takie same jak było");

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

        [HttpGet]
        public IActionResult SetPaymentVerified(string id)
        {
            var transaction = _transactionService.GetBy(id);

            transaction.BoughtOrder.isPaymentVerified = true;

            _transactionService.Update(transaction);

            var user = _userService.GetByLogin(transaction.UserId);

            var boughtOrder = user.BoughtOrders.Where(boughtOrder => boughtOrder.TitleTransaction == transaction.BoughtOrder.TitleTransaction).FirstOrDefault();

            var index = user.BoughtOrders.IndexOf(boughtOrder);

            boughtOrder.isPaymentVerified = true;

            user.BoughtOrders[index] = boughtOrder;

            _userService.Update(user);

            _mailSenderService.SendPaymentAcceptedMail(transaction.EmailShipping, transaction.BoughtOrder.TitleTransaction);

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult SetShippment(string id)
        {
            var transaction = _transactionService.GetBy(id);

            transaction.BoughtOrder.isShippment = true;

            _transactionService.Update(transaction);

            var user = _userService.GetByLogin(transaction.UserId);

            var boughtOrder = user.BoughtOrders.Where(boughtOrder => boughtOrder.TitleTransaction == transaction.BoughtOrder.TitleTransaction).FirstOrDefault();

            var index = user.BoughtOrders.IndexOf(boughtOrder);

            boughtOrder.isShippment = true;

            user.BoughtOrders[index] = boughtOrder;

            _userService.Update(user);

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