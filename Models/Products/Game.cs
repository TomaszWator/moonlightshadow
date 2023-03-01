using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MoonlightShadow.Models.ClassHelper;
using MoonlightShadow.Models;

namespace MoonlightShadow.Models.Products
{
    public class Game : Product
    {
        // Parameters
        public string OperatingSystem { get; set; }

        public string Platform { get; set; }

        public string GameMode { get; set; }

        public List<string> Languages { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public string GraphicsCardRequirements { get; set; }

        public Unit RamRequirements { get; set; }
    }
}