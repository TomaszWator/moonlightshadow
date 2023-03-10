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
        [Required(ErrorMessage = "Wprowadź system operacyjny")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Platforma: ")]
        [Required(ErrorMessage = "Wprowadź platformę sprzętową")]
        public string Platform { get; set; }

        [Display(Name = "Języki: ")]
        [Required(ErrorMessage = "Wprowadź obsługiwane języki")]
        public string Languages { get; set; }
        
        [Display(Name = "Wymagana karta graficzna: ")]
        [Required(ErrorMessage = "Wprowadź wymaganą kartę graficzną")]
        public string GraphicsCardRequirements { get; set; }

        [Display(Name = "Wymagana ilość ramu (GB): ")]
        [Required(ErrorMessage = "Wprowadź wymaganą ilość ramu")]
        public string RamRequirements { get; set; }
    }
}