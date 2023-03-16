using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonlightShadow.Models;
using MoonlightShadow.Services;

namespace MoonlightShadow.Controllers
{
    public class SearchController : Controller
    {
        private readonly CameraService _cameraService;
        private readonly LaptopService _laptopService;
        private readonly PhoneService _phoneService;
        private readonly GameService _gameService;

        public SearchController(CameraService cameraService,
            LaptopService laptopService,
            PhoneService phoneService,
            GameService gameService)
        {
            _cameraService = cameraService;
            _laptopService = laptopService;
            _phoneService = phoneService;
            _gameService = gameService;
        }

        public List<Product> GetAllProducts()
        {
            var cameraList = _cameraService.Get();
            var laptopList = _laptopService.Get();
            var phoneList = _phoneService.Get();
            var gameList = _gameService.Get();

            var productList = new List<Product>();

            foreach (var camera in cameraList)
            {
                productList.Add(camera as Product);
            }

            foreach (var laptop in laptopList)
            {
                productList.Add(laptop as Product);
            }
            
            foreach (var phone in phoneList)
            {
                productList.Add(phone as Product);
            }

            foreach (var game in gameList)
            {
                productList.Add(game as Product);
            }

            return productList;
        }
        
        public string[] CleanAndDivideQueryFromSpaceAndComma(string query)
        {
            query = Regex.Replace(query, @"\s+", " ");
            query = Regex.Replace(query, ",{2,}", ",");

            var queryList = query.ToLower()
                                .Replace(", ", ",")
                                .Replace(" ,", ",")
                                .Split(new char[]{ ' ', ',' });

            int i = 0;
            foreach (var queryItem in queryList)
            {
                if(queryItem == " " || queryItem == ",")
                {
                    queryItem.Remove(i);
                }
                i++;
            }

            return queryList;
        }

        public List<Product> SearchingBasic(List<Product> productList, string[] queryList)
        {
            var foundProducts = new List<Product>();

            foreach (var product in productList)
            {
                foreach(var queryItem in queryList)
                {
                    if(product.Name.ToLower().Contains(queryItem))
                    {
                        foundProducts.Add(product);
                        break;
                    }
                }
            }
            return foundProducts;
        }

        public List<Product> SearchingExtended(List<Product> productList, string[] queryList)
        {
            var foundProducts = new List<Tuple<Product, int>>();

            int numberOfRepetitions = 0;

            foreach (var product in productList)
            {
                numberOfRepetitions = 0;

                foreach(var queryItem in queryList)
                {
                    if(product.Name.ToLower().Contains(queryItem))
                    {
                        numberOfRepetitions++;
                    }
                }

                if(numberOfRepetitions != 0)
                {
                    var tuple = new Tuple<Product, int>(product, numberOfRepetitions);

                    foundProducts.Add(tuple);
                }
            }
            var result = foundProducts.OrderByDescending(product => product.Item2).Select(product => product.Item1);
            
            return result.ToList<Product>();
        }

        [HttpGet]
        public IActionResult Index(string query)
        {
            if(query == null)
            {
                ViewBag.foundProductList = new List<Product>();
                return View();
            }

            var productList = GetAllProducts();

            var queryList = CleanAndDivideQueryFromSpaceAndComma(query);

            ViewBag.foundProductList = SearchingExtended(productList, queryList);

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
