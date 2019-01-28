using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebsite.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public MenuItem Item { get; set; }
        public int Quantity { get; set; }
    }
}
