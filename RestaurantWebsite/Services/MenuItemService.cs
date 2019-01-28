using RestaurantWebsite.Data;
using RestaurantWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebsite.Services
{
    public class MenuItemService
    {
        RestaurantDbContext context;

        public MenuItemService(RestaurantDbContext c)
        {
            context = c;
        }

        public void Delete(int id)
        {
            MenuItem itemToDelete = context.MenuItems.FirstOrDefault(d => d.Id == id);
            context.MenuItems.Remove(itemToDelete);
            context.SaveChanges();
        }

        public IEnumerable<MenuItem> Get()
        {
            List<MenuItem> items = context.MenuItems.ToList();
            return context.MenuItems.ToList();
        }

        public void Insert(MenuItem item)
        {
            context.Add(item);
            context.SaveChanges();
        }

        public void Update(MenuItem item)
        {
            MenuItem itemToUpdate = context.MenuItems.FirstOrDefault(d => d.Id == item.Id);

            itemToUpdate.Name = item.Name;
            itemToUpdate.Description = item.Description;
            itemToUpdate.Price = item.Price;

            context.Update(itemToUpdate);
            context.SaveChanges();
        }
    }
}
