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
            IEnumerable<Order> orders = context.Orders.ToList();
            foreach (var item in orders)
            {
                item.OrderItems = context.OrderItems.Where(o => o.OrderId == item.Id).ToList();
            }

            return context.Orders.ToList();


        }
    }
}
