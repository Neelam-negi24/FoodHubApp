using FoodHubApp.Controllers;
using FoodHubApp.Data;
using FoodHubApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace FoodHubApp.Tests
{
    [TestClass]
    public class OrdersControllerTests
    {
        private OrdersController _controller;
        private FoodHubAppContext _context;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FoodHubAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new FoodHubAppContext(options);
            _controller = new OrdersController(_context);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1; // Assuming this product does not exist

            // Act
            var result = await _controller.CreateOrder(productId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateOrder_RedirectsToOrderConfirmation_WhenModelStateIsValid()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, "testUser")
              }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var orderViewModel = new OrderViewModel
            {
                ProductId = 15,
                ProductName = "Caesar Salad",
                Quantity = 2,
                Price = 10.00m,
                Payment = new PaymentViewModel
                {
                    PaymentMethod = "Credit Card",
                    CardNumber = "4111111111111111",
                    CardHolderName = "John Smith"
                }
            };
            _controller.ModelState.Clear();
            // Act
            var result = await _controller.CreateOrder(orderViewModel);

          
            // Assert that the result is of type RedirectToActionResult
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult), "Expected RedirectToActionResult");

            // Cast the result to RedirectToActionResult
            var redirectResult = result as RedirectToActionResult;

            // Assert the cast result is not null
            Assert.IsNotNull(redirectResult);

            // Assert the action name is "OrderConfirmation"
            Assert.AreEqual("OrderConfirmation", redirectResult.ActionName);

        }


        [TestMethod]
        public async Task DeleteOrder_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteOrder(999); // Assuming this order does not exist

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteOrder_ShouldRedirectToOrderList_WhenOrderExists()
        {
            // Act
            var result = await _controller.DeleteOrder(999); // Assume this ID does not exist

            // Assert
        
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


    }
}
