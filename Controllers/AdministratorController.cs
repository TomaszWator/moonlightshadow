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
using Microsoft.AspNetCore.Authorization;
using MoonlightShadow.ViewModels.Administrator;
using WebApi.Services;

namespace MoonlightShadow.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public AdministratorController(UserService userService, TransactionService transactionService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if(userRole != "admin") return RedirectToAction("Index", "Home");

            var administratorViewModel = new AdministratorViewModel();
            
            administratorViewModel.PaymentTransactions = _transactionService.Get().Where(o => o.BoughtOrder.isPaymentVerified == false);

            administratorViewModel.ShippmentTransactions = _transactionService.Get().Where(o => o.BoughtOrder.isPaymentVerified == false);

            return View(administratorViewModel);
        }
    }
}