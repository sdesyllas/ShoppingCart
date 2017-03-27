using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Abstractions;
using ShoppingCart.Business;
using ShoppingCart.Domain;
using ShoppingCart.Service.Controllers;

namespace ShoppingCart.Service.Test
{
    [TestFixture]
    public class ShoppingBasketControllerTests
    {
        private ShoppingBasketController _shoppingBasketController;

        private Mock<IShoppingBasketContext> _mockShoppingBasketContext;

        [OneTimeSetUp]
        public void SetupMocks()
        {
            _mockShoppingBasketContext = new Mock<IShoppingBasketContext>();
            _shoppingBasketController = new ShoppingBasketController(_mockShoppingBasketContext.Object);
        }

        [Test]
        public void Get_CanReturn_ShoppingCart()
        {
            //Arrange
            var products = Builder<Product>.CreateListOfSize(10).Build().ToList();
            _mockShoppingBasketContext.Setup(x => x.GetShoppingCart(It.IsAny<string>()))
                .Returns(products);

            //Act
            var response = _shoppingBasketController.Get("testCart");

            //Assert
            _mockShoppingBasketContext.Verify(x => x.GetShoppingCart(It.IsAny<string>()), Times.Once);
            response.Should().NotBeNull();
            response.Count().Should().Be(10);
        }

        [Test]
        public void CheckOut_CheckoutInContext_IsCalled()
        {
            //Arrange
            _mockShoppingBasketContext.Setup(x => x.Checkout(It.IsAny<string>()));
               
            //Act
            var response = _shoppingBasketController.CheckOut("testCart");

            //Assert
            _mockShoppingBasketContext.Verify(x => x.Checkout(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddProduct_AddProductInContext_IsCalled()
        {
            //Arrange
            _mockShoppingBasketContext.Setup(x => x.AddProduct(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));

            //Act
            var response = _shoppingBasketController.AddProduct("testCart", 1, 1);

            //Assert
            _mockShoppingBasketContext.Verify(x => x.AddProduct(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()),
                Times.Once);
        }
    }
}
