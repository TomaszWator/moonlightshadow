using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoonlightShadow.Models;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.ViewModels.Category
{
    public class CategoryViewModel
    {
        public CameraViewModel CameraViewModel { get; set; }
        public GameViewModel GameViewModel { get; set; }
        public LaptopViewModel LaptopViewModel { get; set; }
        public PhoneViewModel PhoneViewModel { get; set; }

        public string ProductCondition { get; set; }
        public string FilterPrice { get; set; }
        public decimal MinimumPrice { get; set; }
        public decimal MaximumPrice { get; set; }
        public string OrderByPrice { get; set; }
    }
}