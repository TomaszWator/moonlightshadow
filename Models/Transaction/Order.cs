using System.Collections.Generic;
using Extension.Security;

namespace MoonlightShadow.Models.Transaction
{
    public class Order
    {
        public HashSet<OrderItem> Items { get; set; }
        public string TitleTransaction { get; set; }
        public decimal FullPrice { get; set; }

        public Order()
        {
            Items = new HashSet<OrderItem>();
            TitleTransaction = Hasher.GetRandomAlphaNumericString(24);
            FullPrice = 0;
        }
    }
}