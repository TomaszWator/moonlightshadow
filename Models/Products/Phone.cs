using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MoonlightShadow.Models.ClassHelper;
using MoonlightShadow.Models;

namespace MoonlightShadow.Models.Products
{
    public class Phone : Product
    {
        public string OperatingSystem { get; set; }

        public Screen Screen { get; set; }

        public Processor Processor { get; set; }

        public Ram Ram { get; set; }

        public HardDrive HardDrive { get; set; }
        
        public GraphicsCard GraphicsCard { get; set; }

        public Battery Battery { get; set; }

        public Unit FrontCameraResolution { get; set; }

        public Unit BackCameraResolution { get; set; }

        public SimCard SimCard { get; set; }

        public List<string> Connectors { get; set; }

        public List<string> Communication { get; set; }

        public List<string> Multimedia { get; set; }

        public List<string> Features { get; set; }
    }
}