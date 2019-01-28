using RestaurantWebsite.Data;
using RestaurantWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebsite.Services
{
    public class OrderService
    {
        RestaurantDbContext context;

        public OrderService(RestaurantDbContext c)
        {
            context = c;
        }

        public void AddOrder(Order o)
        {
            context.Orders.Add(o);
            context.SaveChanges();
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.ToList();
        }
    }
}
