using System.Diagnostics;
using FoodHubApp.Data;
using FoodHubApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodHubApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FoodHubAppContext _context;
        // intializing logger and db context
        public HomeController(ILogger<HomeController> logger, FoodHubAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        //main home page
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            products = products.Take(3).ToList();
			return View(products);
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        
        [HttpGet]
        public async Task<IActionResult> Products(string searchString)
        {   
            // getting products list
            var products = from p in _context.Products
                           select p;

            // filtering products based on search
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString; 
            return View(await products.ToListAsync());
        }



        //Error Handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
