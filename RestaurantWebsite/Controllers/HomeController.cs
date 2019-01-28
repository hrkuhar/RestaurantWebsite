using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebsite.Helpers;
using RestaurantWebsite.Models;
using RestaurantWebsite.Services;

namespace RestaurantWebsite.Controllers
{
    public class HomeController : Controller
    {
        MenuItemService mService;
        OrderService oService;

        public HomeController(MenuItemService m, OrderService o)
        {
            mService = m;
            oService = o;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get<Cart>("Cart") == null)
            {
                HttpContext.Session.Set("Cart", new Cart());
            }

            return View();
        }

        public IActionResult Menu()
        {
            IEnumerable<MenuItem> items = mService.Get();
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }


        public RedirectToActionResult AddToCart(int dishId)
        {
            Cart c = GetCart();

            if ((c.cartItems.FirstOrDefault(i => i.Item.Id == dishId)) == null)
            {
                MenuItem dishToAdd = mService.Get().FirstOrDefault(d => d.Id == dishId);

                CartItem itemToAdd = new CartItem { Item = dishToAdd, Quantity = 1 };

                c.cartItems.Add(itemToAdd);
            }
            else
            {
                (c.cartItems.FirstOrDefault(i => i.Item.Id == dishId)).Quantity += 1;
            }

            SaveCart(c);

            return RedirectToAction("Menu");
        }

        public RedirectToActionResult RemoveCartItem(int dishId)
        {
            Cart c = GetCart();

            CartItem itemToRemove = c.cartItems.FirstOrDefault(i => i.Item.Id == dishId);
            if (itemToRemove.Quantity == 1)
            {
                c.cartItems.Remove(itemToRemove);
            }
            else
            {
                itemToRemove.Quantity -= 1;
            }


            SaveCart(c);

            return RedirectToAction("Menu");
        }

        public ViewResult Checkout()
        {
            return View();
        }

        public ViewResult ProcessCheckout(Order order)
        {
            order.Time = DateTime.Now;
            Cart cart = GetCart();
            double totalPrice = 0;
            foreach (CartItem item in cart.cartItems)
            {
                totalPrice += (item.Item.Price * item.Quantity);
            }

            order.TotalPrice = totalPrice;
            oService.AddOrder(order);

            HttpContext.Session.Set("Cart", new Cart());
            return View(order);
        }

        public RedirectToActionResult ClearCart()
        {
            HttpContext.Session.Set("Cart", new Cart());
            return RedirectToAction("Index");
        }

        [Authorize]
        public ViewResult OrdersList()
        {
            return View(oService.GetOrders());
        }

        [Authorize]
        public IActionResult AdminPage()
        {
            IEnumerable<MenuItem> items = mService.Get();
            return View(items);
        }

        [Authorize]
        public ViewResult AddItem()
        {
            return View();
        }

        [Authorize]
        public RedirectToActionResult Save(MenuItem item)
        {
            mService.Insert(item);

            return RedirectToAction("AdminPage");
        }

        [Authorize]
        public RedirectToActionResult Delete(int itemId)
        {
            mService.Delete(itemId);

            return RedirectToAction("AdminPage");
        }

        [Authorize]
        public ViewResult EditItem(int itemId)
        {
            MenuItem item = mService.Get().FirstOrDefault(d => d.Id == itemId);
            return View(item);
        }

        [Authorize]
        public RedirectToActionResult Update(MenuItem item)
        {
            mService.Update(item);
            return RedirectToAction("AdminPage");
        }
    }
}
