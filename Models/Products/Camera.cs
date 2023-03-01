using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using MoonlightShadow.Models.ClassHelper;

namespace MoonlightShadow.Models.Products
{
    public class Camera : Product
    {
        public double Weight { get; set; }

        public string WeightUnit { get; set; }

        public Dimension Dimensions { get; set; }

        public string DimensionUnit { get; set; }

        public int Resolution { get; set; }

        public string ResolutionUnit { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public double Megapiksels { get; set; }
    }
}