using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MoonlightShadow.Models.ClassHelper;
using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models.Products
{
    public class Laptop : Product
    {
        // Parameters
        [Display(Name = "System operacyjny")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Przekątna ekranu")]
        public string ScreenDiagonal { get; set; }

        [Display(Name = "Rozdzielczość")]
        public string ScreenResolution { get; set; }

        [Display(Name = "Rodzaj matrycy")]
        public string ScreenMatrixType { get; set; }

        [Display(Name = "Częstotliwość odświeżania ekranu")]
        public string ScreenRefreshing { get; set; }
        
        [Display(Name = "Ekran dotykowy (Tak/Nie)")]
        public string TouchScreen { get; set; }

        [Display(Name = "Processor")]
        public string Processor { get; set; }

        [Display(Name = "Ilość ramu")]
        public string RamSize { get; set; }

        [Display(Name = "Ilość wolnych slotów")]
        public string RamFreeSlots { get; set; }

        [Display(Name = "Częstotliwość ramu")]
        public string RamFrequency { get; set; }

        [Display(Name = "Dysk twardy")]
        public string HardDrive { get; set; }
        
        [Display(Name = "Karta graficzna")]
        public string GraphicsCard { get; set; }

        [Display(Name = "Pojemność baterii")]
        public string BatteryCapacity { get; set; }

        [Display(Name = "Złącza")]
        public string Connectors { get; set; }

        // public List<string> Communication { get; set; }

        // public List<string> Multimedia { get; set; }

        // public List<string> Features { get; set; }

        // public List<string> Control { get; set; }
    }
}