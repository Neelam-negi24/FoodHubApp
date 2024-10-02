using FoodHubApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodHubApp.Controllers
{
    public class ProductsUnitTestController : Controller
    {
        public List<Product> GetProductsList()
        {
            return new List<Product>()
            {
             new Product { Id = 1, Name = "Margherita Pizza", Description = "Classic pizza with tomato sauce, mozzarella, and basil.", Price = 1.00m, Category = "Main Course" },
            new Product { Id = 2, Name = "Caesar Salad", Description = "Fresh romaine lettuce with Caesar dressing, croutons, and Parmesan.", Price = 1.00m, Category = "Appetizer" },
            new Product { Id = 3, Name = "Spaghetti Carbonara", Description = "Pasta with creamy sauce, pancetta, and Parmesan cheese.", Price = 1.00m, Category = "Main Course" }
            };
           
        }
        public IActionResult Index()
        {
            var products = from p in GetProductsList() select p;
            return View(products);

        }
    }
}
