using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantWebsite.Helpers;

namespace RestaurantWebsite.Components
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Cart c = HttpContext.Session.Get<Cart>("Cart");
            double total = 0;

            foreach (CartItem item in c.cartItems)
            {
                total += (item.Item.Price * item.Quantity);
            }

            c.TotalPrice = total;

            return View(c);
        }
    }
}
