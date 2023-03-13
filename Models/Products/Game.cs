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
        [Display(Name = "Platforma: ")]
        [Required(ErrorMessage = "Wprowadź platformę sprzętową")]
        public string Platform { get; set; }

        [Display(Name = "System operacyjny: ")]
        [Required(ErrorMessage = "Wprowadź system operacyjny")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Processor: ")]
        [Required(ErrorMessage = "Wprowadź system operacyjny")]
        public string Processor { get; set; }

        [Display(Name = "Wymagana karta graficzna: ")]
        [Required(ErrorMessage = "Wprowadź wymaganą kartę graficzną")]
        public string GraphicsCardRequirements { get; set; }

        [Display(Name = "Wymagana ilość ramu ")]
        [Required(ErrorMessage = "Wprowadź wymaganą ilość ramu")]
        public string RamRequirements { get; set; }

        [Display(Name = "Dysk twardy: ")]
        [Required(ErrorMessage = "Wprowadź ilość wolnego miejsca na dysku")]
        public string HardDriveRequirements { get; set; }

        [Display(Name = "Języki: ")]
        [Required(ErrorMessage = "Wprowadź obsługiwane języki")]
        public string Languages { get; set; }
    }
}