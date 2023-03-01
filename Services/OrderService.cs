using System.Net;
using System.Collections.Generic;
using System.Linq;
using MoonlightShadow.Models;
using Microsoft.AspNetCore.Http;
using Extension.Web;
using MoonlightShadow.Services;
using MoonlightShadow.Models.Transaction;
using MoonlightShadow.Models.Products;

namespace MoonlightShadow.Services
{
    public class OrderService
    {
        private readonly UserService _userService;
        private readonly SessionService _sessionService;
        private readonly ProductService _productService;

        public OrderService(UserService userService, SessionService sessionService, ProductService productService)
        {
            _userService = userService;
            _sessionService = sessionService;
            _productService = productService;
        }

        public void CreateCartOrderItemByDb(string userEmail, OrderItem orderItemIn)
        {
            var user = _userService.GetByEmail(userEmail);

            user.CartOrder.Items.Add(orderItemIn);

            var price = _productService.GetBy(orderItemIn).Price;

            user.CartOrder.FullPrice += price;

            _userService.Update(user);
        }

        public void CreateCartOrderItemBySession(OrderItem orderItemIn)
        {
            Order cartOrder = _sessionService.GetComplexData<Order>("Cart");

            if (cartOrder == null)
            {
                cartOrder = new Order();
            }

            cartOrder.Items.Add(orderItemIn);

            var price = _productService.GetBy(orderItemIn).Price;

            cartOrder.FullPrice += price;

            _sessionService.SetComplexData("Cart", cartOrder);
        }

        public void RemoveCartOrderItemByDb(string userEmail, OrderItem orderItemOut)
        {
            var user = _userService.GetByEmail(userEmail);

            var orderItemToRemove = user.CartOrder.Items.FirstOrDefault(orderItem => orderItem.Id == orderItemOut.Id && orderItem.Category == orderItemOut.Category);

            if (orderItemToRemove != null)
            {
                user.CartOrder.Items.Remove(orderItemToRemove);

                var price = _productService.GetBy(orderItemToRemove).Price;

                user.CartOrder.FullPrice -= price;

                _userService.Update(user);
            }
        }

        public void RemoveCartOrderItemBySession(OrderItem orderItemOut)
        {
            Order cartOrder = _sessionService.GetComplexData<Order>("Cart");

            if (cartOrder == null)
            {
                cartOrder = new Order();
            }

            var orderItemToRemove = cartOrder.Items.FirstOrDefault(orderItem => orderItem.Id == orderItemOut.Id && orderItem.Category == orderItemOut.Category);

            if (orderItemToRemove != null)
            {
                cartOrder.Items.Remove(orderItemToRemove);

                var price = _productService.GetBy(orderItemToRemove).Price;

                cartOrder.FullPrice -= price;

                _sessionService.SetComplexData("Cart", cartOrder);
            }
        }

        public void CreateFollowedOrderItemByDb(string userEmail, OrderItem orderItemIn)
        {
            var user = _userService.GetByEmail(userEmail);

            user.FollowedOrder.Items.Add(orderItemIn);

            _userService.Update(user);

            var product = _productService.GetBy(orderItemIn);

            product.FollowedQuantity++;

            _productService.Update(product);
        }

        public void RemoveFollowedOrderItemByDb(string userEmail, OrderItem orderItemOut)
        {
            var user = _userService.GetByEmail(userEmail);

            var orderItemToRemove = user.FollowedOrder.Items.FirstOrDefault(orderItem => orderItem.Id == orderItemOut.Id && orderItem.Category == orderItemOut.Category);

            if (orderItemToRemove != null)
            {
                user.FollowedOrder.Items.Remove(orderItemToRemove);

                _userService.Update(user);

                var product = _productService.GetBy(orderItemOut);

                product.FollowedQuantity--;

                _productService.Update(product);
            }
        }

        public void CreateRecomendedOrderItemByDb(string userEmail, OrderItem orderItemIn)
        {
            var user = _userService.GetByEmail(userEmail);

            user.RecomendedOrder.Items.Add(orderItemIn);

            _userService.Update(user);

            var product = _productService.GetBy(orderItemIn);

            product.RecomendedQuantity++;

            _productService.Update(product);
        }

        public void RemoveRecomendedOrderItemByDb(string userEmail, OrderItem orderItemOut)
        {
            var user = _userService.GetByEmail(userEmail);

            var orderItemToRemove = user.RecomendedOrder.Items.FirstOrDefault(orderItem => orderItem.Id == orderItemOut.Id && orderItem.Category == orderItemOut.Category);

            if (orderItemToRemove != null)
            {
                user.RecomendedOrder.Items.Remove(orderItemToRemove);

                _userService.Update(user);

                var product = _productService.GetBy(orderItemOut);

                product.RecomendedQuantity--;

                _productService.Update(product);
            }
        }

        public BoughtOrder GetBoughtOrder(Order order)
        {
            var boughtOrder = new BoughtOrder();

            boughtOrder.FullPrice = order.FullPrice;
            boughtOrder.TitleTransaction = order.TitleTransaction;
            boughtOrder.ProductItems = new HashSet<Models.Product>();
            
            foreach(var orderItem in order.Items)
            {
                var product = _productService.GetBy(orderItem);

                boughtOrder.ProductItems.Add(product);
            }

            return boughtOrder;
        }

        public List<BoughtOrder> GetBoughtOrders(List<Order> orders)
        {
            var boughtOrders = new List<BoughtOrder>();

            foreach (var order in orders)
            {
                boughtOrders.Add(GetBoughtOrder(order));
            }

            return boughtOrders;
        }
    }
}