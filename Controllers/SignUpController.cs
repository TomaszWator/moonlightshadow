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
using Extension.ValidModel;
using WebApi.Services;
using Extension.Security;
using Extension.Web;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MoonlightShadow.Controllers
{
    public class SignUpController : Controller
    {
        private readonly UserService _userService;
        private readonly MailSenderService _mailSenderService;
        private readonly SessionService _sessionService;

        public SignUpController(UserService userService,
            MailSenderService mailSenderService,
            SessionService sessionService)
        {
            _userService = userService;
            _mailSenderService = mailSenderService;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SignUpViewModel SignUpForm)
        {
            if (ModelState.IsValid == false)
            {
                return View(SignUpForm);
            }

            if(SignUpForm.Password != SignUpForm.RepetedPassword)
            {
                ModelState.AddModelError("RepetedPassword", "Hasła nie są identyczne");

                SignUpForm.Password = "";
                SignUpForm.RepetedPassword = "";

                return View(SignUpForm);
            }

            var user = _userService.GetByLogin(SignUpForm.Login);

            if (user.IsNotNull())
            {
                ModelState.AddModelError("Login", "Użytkownik już istnieje w bazie");

                return View(SignUpForm);
            }

            user = _userService.GetByEmail(SignUpForm.Email);

            if (user.IsNotNull())
            {
                ModelState.AddModelError("Email", "Istnieje konto na podanego maila");

                return View(SignUpForm);
            }

            _userService.Create(new Models.User(SignUpForm));

            var registrationToken = Hasher.GetToken();

            _sessionService.SetString("registrationToken", registrationToken);

            _sessionService.SetString("userEmail", SignUpForm.Email);

            _mailSenderService.SendRegistrationMail(SignUpForm.Email, SignUpForm.Login, registrationToken);

            TempData.Remove("ShowModal");
            TempData["ShowModal"] = "ConfirmSignUpMailSend";

            return RedirectToAction("Index", "SignUp");
        }

        [HttpGet]
        public IActionResult VerifyEmail(string email, string token)
        {
            var sessionToken = _sessionService.GetString("registrationToken");

            Console.WriteLine("Token: " + token);

            if (token == sessionToken)
            {
                var user = _userService.GetByEmail(email);

                if(user.IsNull())
                {
                    TempData.Remove("ShowModal");
                    TempData["ShowModal"] = "AbortVerificationMail";

                    return RedirectToAction("Index", "Login");
                }

                user.IsMailRegisterVerified = true;

                _userService.Update(user);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "ConfirmVerificationMail";

                return RedirectToAction("Index", "Login");
            }
            else
            {
                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "AbortVerificationMail";

                return RedirectToAction("Index", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}