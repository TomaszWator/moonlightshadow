using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using MoonlightShadow.Models.ClassHelper;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models.Products
{
    public class Camera : Product
    {
        [Display(Name = "Waga: ")]
        public double Weight { get; set; }

        [Display(Name = "Jednostka wagi: ")]
        public string WeightUnit { get; set; }

        public Dimension Dimensions { get; set; }

        [Display(Name = "Jednostka rozmiaru: ")]
        public string DimensionUnit { get; set; }

        [Display(Name = "Rozdzielczość: ")]
        public int Resolution { get; set; }

        [Display(Name = "Jednostka rozdzielczości: ")]
        public string ResolutionUnit { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        [Display(Name = "Ilość megapikseli: ")]
        public double Megapiksels { get; set; }
    }
}