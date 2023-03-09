using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MoonlightShadow.Models.ClassHelper;
using MoonlightShadow.Models;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models.Products
{
    public class Game : Product
    {
        // Parameters
        [Display(Name = "System operacyjny: ")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Platforma: ")]
        public string Platform { get; set; }

        [Display(Name = "Języki: ")]
        public string Languages { get; set; }
        
        [Display(Name = "Wymagana karta graficzna: ")]
        public string GraphicsCardRequirements { get; set; }

        [Display(Name = "Wymagana ilość ramu: ")]
        public string RamRequirements { get; set; }
    }
}