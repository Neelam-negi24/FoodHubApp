using FoodHubApp.Data;
using FoodHubApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodHubApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FoodHubAppContext _context;

        public OrdersController(FoodHubAppContext context)
        {
            _context = context;
        }
        [Authorize]
        public async Task<IActionResult> OrderList(string sortOrder)
        {
          
            var userId = User.Identity.Name;

           // sorting by order date
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

    
            var orders = from o in _context.Orders
                         where o.UserId == userId
                         select o;

            //fetching lists based on sort order
            switch (sortOrder)
            {
                case "date_desc":
                    orders = orders.OrderByDescending(o => o.OrderDate);
                    break;
                default:
                    orders = orders.OrderBy(o => o.OrderDate);
                    break;
            }

            return View(await orders.AsNoTracking().ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            //setting up the view
            var orderViewModel = new OrderViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1 ,
                OrderDate = DateTime.Now
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        {
            if (model?.Payment == null || !ModelState.IsValid)
            {
                return View(model); // Return the view with the model if invalid
            }
       
            var payment = new Payment
            {
                PaymentMethod = model.Payment.PaymentMethod,
                CardNumber = model.Payment.CardNumber,
                CardHolderName = model.Payment.CardHolderName,
                PaymentDate = DateTime.Now,
                Amount = model.Price * model.Quantity
            };

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            var order = new Order
            {
                ProductName = model.ProductName,
                Quantity = model.Quantity,
                Price = model.Price,
                UserId = User.Identity.Name,
                PaymentId = payment.Id
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }
        [Authorize]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            //fetching order to diaply on confirmation page
            var order = await _context.Orders.Include(o => o.Payment).FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        //Removing Order 
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            // removing the order with specific id from the db
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("OrderList");
        }



    }
}
