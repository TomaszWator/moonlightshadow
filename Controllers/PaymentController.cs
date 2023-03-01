using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Extension.ValidModel;
using MoonlightShadow.ViewModels;
using MoonlightShadow.Services;
using Extension.Web;
using WebApi.Services;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;
        private readonly ProductService _productService;
        private readonly SessionService _sessionService;
        private readonly MailSenderService _mailSenderService;
        private readonly OrderService _orderService;


        public PaymentController(UserService userService,
            TransactionService transactionService,
            ProductService productService,
            SessionService sessionService,
            MailSenderService mailSenderService,
            OrderService orderService)
        {
            _userService = userService;
            _transactionService = transactionService;
            _productService = productService;
            _sessionService = sessionService;
            _mailSenderService = mailSenderService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var paymentViewModel = new PaymentViewModel();

            if (userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                paymentViewModel = user.GetPaymentViewModel();

                if (user.CartOrder == null || user.CartOrder.Items.Count == 0)
                {
                    TempData.Remove("ShowModal");
                    TempData["ShowModal"] = "EmptyCart";

                    return RedirectToAction("Index", "Cart");
                }

                paymentViewModel.TitleTransaction = user.CartOrder.TitleTransaction;

                paymentViewModel.FullPrice = user.CartOrder.FullPrice;
            }
            else
            {
                Order cartOrder = HttpContext.Session.GetComplexData<Order>("Cart");

                if (cartOrder == null || cartOrder.Items.Count == 0)
                {
                    TempData.Remove("ShowModal");
                    TempData["ShowModal"] = "EmptyCart";

                    return RedirectToAction("Index", "Cart");
                }

                paymentViewModel.TitleTransaction = cartOrder.TitleTransaction;

                paymentViewModel.FullPrice = cartOrder.FullPrice;
            }

            return View(paymentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PaymentViewModel paymentViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(paymentViewModel);
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                var boughtOrder = _orderService.GetBoughtOrder(user.CartOrder);

                _transactionService.Create(new Transaction(boughtOrder, paymentViewModel, user.Login));

                user.BoughtOrders.Add(_orderService.GetBoughtOrder(user.CartOrder));

                user.CartOrder = new Order();

                _userService.Update(user);

                _mailSenderService.SendWaitingForAcceptPaymentMail(paymentViewModel.EmailContact);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "WaitingForPayment";

                return RedirectToAction("Index", "Cart");
            }
            else
            {
                Order cartOrder = HttpContext.Session.GetComplexData<Order>("Cart");

                var boughtOrder = _orderService.GetBoughtOrder(cartOrder);

                _transactionService.Create(new Transaction(boughtOrder, paymentViewModel));

                _sessionService.Remove("Cart");

                _mailSenderService.SendWaitingForAcceptPaymentMail(paymentViewModel.EmailContact);

                TempData.Remove("ShowModal");
                TempData["ShowModal"] = "WaitingForPayment";

                return RedirectToAction("Index", "Cart");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AcceptTransaction(string id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "admin") return RedirectToAction("Index", "Account");

            var transaction = _transactionService.GetBy(id);

            transaction.BoughtOrder.isPaymentVerified = true;

            if (transaction.UserId != null)
            {
                var user = _userService.GetByLogin(transaction.UserId);

                var boughtOrder = user.BoughtOrders.First(o => o.TitleTransaction == transaction.BoughtOrder.TitleTransaction);

                boughtOrder.isPaymentVerified = true;

                _userService.Update(user);
            }

            _transactionService.Update(transaction);

            _mailSenderService.SendPaymentAcceptedMail(transaction.EmailContact, transaction.BoughtOrder.TitleTransaction);

            var boughtProducts = _productService.GetBy(transaction.BoughtOrder);

            foreach (var boughtProduct in boughtProducts)
            {
                boughtProduct.BoughtQuantity++;
            }

            _productService.Update(boughtProducts);

            return RedirectToAction("Index", "Administrator");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}