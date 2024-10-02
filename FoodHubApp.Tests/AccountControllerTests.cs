using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FoodHubApp.Controllers;
using FoodHubApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodHubApp.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<SignInManager<IdentityUser>> _mockSignInManager;
        private AccountController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);

            _mockSignInManager = new Mock<SignInManager<IdentityUser>>(
                _mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
                null, null, null, null);

            _controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [TestMethod]
        public void Register_ReturnsViewResult()
        {
            // Act
            var result = _controller.Register();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public async Task Login_PostValidCredentials_ReturnsRedirectToAction()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Password123!",
                RememberMe = false
            };

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Logout_RedirectsToHomeIndex()
        {
            // Arrange
            _mockSignInManager.Setup(x => x.SignOutAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Logout();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }

}