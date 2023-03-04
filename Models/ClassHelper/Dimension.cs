using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models.ClassHelper
{
    public class Dimension
    {
        [Display(Name = "Rozmiar X: ")]
        public double X { get; set; }

        [Display(Name = "Rozmiar Y: ")]
        public double Y { get; set; }

        [Display(Name = "Rozmiar Z: ")]
        public double Z { get; set; }
    }
}