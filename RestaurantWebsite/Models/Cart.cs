using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebsite.Models
{
    public class Cart
    {
        public List<CartItem> cartItems;
        public double TotalPrice { get; set; }

        public Cart()
        {
            cartItems = new List<CartItem>();
        }
    }
}
