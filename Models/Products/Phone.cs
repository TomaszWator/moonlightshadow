using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MoonlightShadow.Models.ClassHelper;
using MoonlightShadow.Models;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models.Products
{
    public class Phone : Product
    {
        [Display(Name = "System operacyjny")]
        public string OperatingSystem { get; set; }

        [Display(Name = "Ekran")]
        public string Screen { get; set; }

        [Display(Name = "Processor")]
        public string Processor { get; set; }

        [Display(Name = "Ram")]
        public string Ram { get; set; }

        [Display(Name = "Dysk twardy")]
        public string HardDrive { get; set; }
        
        [Display(Name = "Karta graficzna")]
        public string GraphicsCard { get; set; }

        [Display(Name = "Bateria")]
        public string Battery { get; set; }

        [Display(Name = "Rozdzielczość przedniej kamery")]
        public string FrontCameraResolution { get; set; }

        [Display(Name = "Rozdzielczość tylniej kamery")]
        public string BackCameraResolution { get; set; }

        [Display(Name = "SimCard")]
        public string SimCard { get; set; }

        [Display(Name = "Simlocke")]
        public string SimLocke { get; set; }

        [Display(Name = "Złącza")]
        public string Connectors { get; set; }

        // public List<string> Communication { get; set; }

        // public List<string> Multimedia { get; set; }

        // public List<string> Features { get; set; }
    }
}