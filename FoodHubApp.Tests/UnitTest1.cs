using FoodHubApp.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace FoodHubApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
            public void ProductsUnitTest()
            {
                ProductsUnitTestController controllerTests = new ProductsUnitTestController();
                IActionResult result = controllerTests.Index();
                Assert.IsNotNull(result);
            }
        [TestMethod]
        public void OrdersUnitTest()
        {
            OrdersUnitTestController controllerTests = new OrdersUnitTestController();
            IActionResult result = controllerTests.Index();
            Assert.IsNotNull(result);
        }

    }
}