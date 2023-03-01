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
using System.Web;
using Extension.Web;
using Microsoft.AspNetCore.Http;
using Extension.ValidModel;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.Controllers
{
    public class CartController : Controller
    {
        private readonly UserService _userService;

        private readonly ProductService _productService;

        private readonly OrderService _orderService;

        public CartController(
            UserService userService,
            ProductService productService,
            OrderService cartService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = cartService;
        }

        public IActionResult Index()
        {
            List<Product> cart = new List<Product>();
            decimal cartPrice = 0;

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())
            {
                var user = _userService.GetByEmail(userEmail);

                cart = _productService.GetBy(user.CartOrder);

                cartPrice = user.CartOrder.FullPrice;

                ViewBag.Followed = _productService.GetBy(user.FollowedOrder);
            }
            else
            {
                Order cartOrder = HttpContext.Session.GetComplexData<Order>("Cart");

                if(cartOrder == null) cartOrder = new Order();

                cartPrice = cartOrder.FullPrice;

                cart = _productService.GetBy(cartOrder);
            }

            ViewBag.Cart = cart;
            ViewBag.CartPrice = cartPrice;

            return View();
        }

        [HttpGet]
        public IActionResult BuyNow(string category, string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull()) 
            {
                _orderService.CreateCartOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }
            else
            {
                _orderService.CreateCartOrderItemBySession(new OrderItem() { Id = id, Category = category });
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult DeleteOrder(string category, string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.RemoveCartOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }
            else
            {
                _orderService.RemoveCartOrderItemBySession(new OrderItem() { Id = id, Category = category });
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult AddOrder(string category, string id, string returnUrl)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.CreateCartOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }
            else
            {
                _orderService.CreateCartOrderItemBySession(new OrderItem() { Id = id, Category = category });
            }

            TempData.Remove("ShowModal");
            TempData["ShowModal"] = "ConfirmAddToCart";

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult DeleteFollowed(string category, string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                 _orderService.RemoveFollowedOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult AddFollowedToCart(string category, string id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.RemoveFollowedOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });

                _orderService.CreateCartOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult AddToFollowed(string category, string id, string returnUrl)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.CreateFollowedOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }

            TempData.Remove("ShowModal");
            TempData["ShowModal"] = "ConfirmAddToFollowed";

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult RemoveFromFollowed(string category, string id, string returnUrl)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.RemoveFollowedOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult AddToRecomended(string category, string id, string returnUrl)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.CreateRecomendedOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }

            TempData.Remove("ShowModal");
            TempData["ShowModal"] = "ConfirmAddToRecomended";

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult RemoveFromRecomended(string category, string id, string returnUrl)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail.IsNotNull())  
            {
                _orderService.RemoveRecomendedOrderItemByDb(userEmail, new OrderItem() { Id = id, Category = category });
            }

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}