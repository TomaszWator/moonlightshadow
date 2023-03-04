using MoonlightShadow.Models.Products;

namespace MoonlightShadow.ViewModels.Products
{
    public class ProductsViewModel
    {
        public Camera Camera { get; set; }
        public Game Game { get; set; }
        public Laptop Laptop { get; set; }
        public Phone Phone { get; set; }
        public string TypeOfModel { get; set; }
    }
}