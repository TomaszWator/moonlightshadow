using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using MoonlightShadow.Services;
using Extension.Security;
using Extension.ValidModel;
using WebApi.Services;

namespace MoonlightShadow.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserService _userService;
        private readonly MailSenderService _mailSenderService;
        private SessionService _sessionService;

        public LoginController(UserService userService,
            MailSenderService mailSenderService,
            SessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
            _mailSenderService = mailSenderService;
        }

        public IActionResult Index(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Account");
            }

            ViewBag.ReturnUrl = returnUrl;

            ViewData["MailRegisterVerified"] = true;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Index", "Account");
            }

            ViewBag.ReturnUrl = returnUrl;

            ViewData["MailRegisterVerified"] = true;

            if (loginViewModel.TypeOfModel == "LoginFormViewModel")
            {
                if (TryValidateModel(loginViewModel.LoginFormViewModel) == false)
                {
                    return View(loginViewModel);
                }

                var user = _userService.GetByEmail(loginViewModel.LoginFormViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError("LoginFormViewModel.Email", "U??ytkownik nie istnieje w bazie");

                    return View(loginViewModel);
                }

                if (user.IsMailRegisterVerified == false)
                {
                    ViewData["MailRegisterVerified"] = false;

                    ModelState.AddModelError("LoginFormViewModel.Email", "Mail nie zosta?? zweryfikowany");

                    return View(loginViewModel);
                }

                if (user.Password.Hash != Hasher.Encrypt(loginViewModel.LoginFormViewModel.Password, user.Password.Salt).hash)
                {
                    ModelState.AddModelError("LoginFormViewModel.Password", "Has??o niepoprawne");

                    return View(loginViewModel);
                }

                if(user.Privileges == true)
                {
                    HttpContext.SignInAsync(Claimer.CreateClaimPrincipal(ClaimTypes.Email, user.Email, "admin"));
                }
                else
                {
                    HttpContext.SignInAsync(Claimer.CreateClaimPrincipal(ClaimTypes.Email, user.Email));
                }

                if (returnUrl.IsNotNullOrEmptyOrWhiteSpace())
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (loginViewModel.TypeOfModel == "RemindMePasswordViewModel")
            {
                if (TryValidateModel(loginViewModel.RemindMePasswordViewModel) == false)
                {
                    TempData.Remove("ShowModal");
                    TempData["ShowModal"] = "RemindMePassword";

                    return View(loginViewModel);
                }

                var user = _userService.GetByEmail(loginViewModel.RemindMePasswordViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError("RemindMePasswordViewModel.Email", "U??ytkownik nie istnieje w bazie");

                    TempData.Remove("ShowModal");
                    TempData["ShowModal"] = "RemindMePassword";

                    return View(loginViewModel);
                }

                var newPassword = Hasher.GetRandomAlphaNumericString(8);

                var acceptNewPasswordToken = Hasher.GetRandomAlphaNumericString(30).ToLower();

                _mailSenderService.SendNewPassword(loginViewModel.RemindMePasswordViewModel.Email, user.Login, newPassword, acceptNewPasswordToken);

                _sessionService.SetString("acceptNewPasswordToken", acceptNewPasswordToken);

                user.NewPassword = new Password(Hasher.Encrypt(newPassword).ToTuple());

                _userService.Update(user);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmRemindMePasswordMailSend";

                return View();
            }

            return View(loginViewModel);
        }

        public IActionResult AcceptRemindMePassword(string email, string token)
        {
            var acceptNewPasswordToken = _sessionService.GetString("acceptNewPasswordToken");

            Console.WriteLine("Token: " + token);

            if (token == acceptNewPasswordToken)
            {
                _sessionService.Remove("acceptNewPasswordToken");
                
                var user = _userService.GetByEmail(email);

                if (user != null)
                {
                    user.Password = user.NewPassword;

                    _userService.Update(user);

                    TempData.Remove("ShowModal");
                    TempData["ShowModal"] = "ConfirmRemindMePassword";

                    return RedirectToAction("Index", "Login");
                }
            }

            TempData.Remove("ShowModal");
            TempData["ShowModal"] = "AbortRemindMePassword";

            return RedirectToAction("Index", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}