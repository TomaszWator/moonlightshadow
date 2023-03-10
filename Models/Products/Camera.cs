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
        [Display(Name = "Przekątna ekranu ")]
        public string ScreenDiagonal { get; set; }

        [Display(Name = "Rozdzielczość: ")]
        public string Resolution { get; set; }

        [Display(Name = "Jednostka rozdzielczości: ")]
        public string ResolutionUnit { get; set; }

        [Display(Name = "Typ matrycy: ")]
        public string MatrixType { get; set; }  
    }
}