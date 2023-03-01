using System.Collections.Generic;
using MoonlightShadow.Models.Products;
using MoonlightShadow.Models;

namespace MoonlightShadow.Models.Transaction
{
    public class BoughtOrder
    {
        public HashSet<Product> ProductItems { get; set; }
        public string TitleTransaction { get; set; }
        public decimal FullPrice { get; set; }
        public bool isPaymentVerified { get; set; }
        public bool isShippment { get; set; }

        public BoughtOrder()
        {
            ProductItems = new HashSet<Product>();
            TitleTransaction = "";
            FullPrice = 0;
            isPaymentVerified = false;
            isShippment = false;
        }
    }
}