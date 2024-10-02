using FoodHubApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodHubApp.Controllers
{
    public class OrdersUnitTestController : Controller
    {
        public List<Order> GetOrdersList()
        {
            return new List<Order>()
            {
                new Order { Id = 1, UserId = "user1", ProductName = "Margherita Pizza", Price = 8.99m, Quantity = 2 },
                new Order { Id = 2, UserId = "user2", ProductName = "Caesar Salad", Price = 6.50m, Quantity = 3 },
                new Order { Id = 3, UserId = "user3", ProductName = "Spaghetti Carbonara", Price = 10.99m, Quantity = 1 }
            };
        }

        // Action method for testing
        public IActionResult Index()
        {
            // Fetch the list of orders
            var orders = from o in GetOrdersList() select o;

            // Return the orders to the view
            return View(orders);
        }

    
   }
}
