using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebsite.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
