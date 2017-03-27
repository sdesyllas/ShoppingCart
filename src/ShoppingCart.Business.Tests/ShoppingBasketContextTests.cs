using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Business.Tests
{
    [TestFixture]
    public class ShoppingBasketContextTests
    {
        private Mock<IProductContext> _mockProductContext;

        private IShoppingBasketContext _shoppingBasketContext;

        [SetUp]
        public void SetupMocks()
        {
            _mockProductContext = new Mock<IProductContext>();
            _shoppingBasketContext = new ShoppingBasketContext(_mockProductContext.Object);
        }

        [Test]
        public void AddProduct_Can_AddANewProductInTheCart()
        {
            //Arrange
            var testProducts = new List<Product>();
            var testProduct = Builder<Product>.CreateNew().With(x => x.Identifier = 1)
                .With(x => x.Stock = 1).Build();
            testProducts.Add(testProduct);
            _mockProductContext.Setup(x => x.GetProducts()).Returns(testProducts);

            //Act
            _shoppingBasketContext.AddProduct("testCart1", testProduct.Identifier, 1);

            //Assert
            var expectedBasket = _shoppingBasketContext.GetShoppingCart("testCart1");
            _mockProductContext.Verify(x => x.GetProducts(), Times.Once);
            expectedBasket.Count.Should().Be(1);
            expectedBasket.ShouldBeEquivalentTo(new List<Product> {testProduct});
        }

        [Test]
        public void AddProduct_WhenQuantityIsLessThanStock_DoNotAddToTheCart()
        {
            //Arrange
            var testProducts = new List<Product>();
            var testProduct = Builder<Product>.CreateNew().With(x => x.Identifier = 1)
                .With(x => x.Stock = 1).Build();
            testProducts.Add(testProduct);
            _mockProductContext.Setup(x => x.GetProducts()).Returns(testProducts);

            //Act
            _shoppingBasketContext.AddProduct("testCart2", testProduct.Identifier, 2);

            //Assert
            var expectedBasket = _shoppingBasketContext.GetShoppingCart("testCart2");
            expectedBasket.Should().BeNull();
        }

        [Test]
        public void AddProduct_WhenAddingQuantitiesOneByOneMoreThanStock_DoNotAddToTheCart()
        {
            //Arrange
            var testProducts = new List<Product>();
            var testProduct = Builder<Product>.CreateNew().With(x => x.Identifier = 1)
                .With(x => x.Stock = 1).Build();
            testProducts.Add(testProduct);
            _mockProductContext.Setup(x => x.GetProducts()).Returns(testProducts);

            //Act
            _shoppingBasketContext.AddProduct("testCart3", testProduct.Identifier, 1);
            _shoppingBasketContext.AddProduct("testCart3", testProduct.Identifier, 1);

            //Assert
            _mockProductContext.Verify(x => x.GetProducts(), Times.Exactly(2));
            var expectedBasket = _shoppingBasketContext.GetShoppingCart("testCart3");
            expectedBasket.Count.Should().Be(1);
        }

        [Test]
        public void Checkout_WhenCheckoutIsCalled_CheckThatStockQuantitiesAreUpdated()
        {
            //Arrange
            var testProducts = new List<Product>();
            var testProduct = Builder<Product>.CreateNew().With(x => x.Identifier = 1)
                .With(x => x.Stock = 3).Build();
            testProducts.Add(testProduct);
            _mockProductContext.Setup(x => x.GetProducts()).Returns(testProducts);

            //Act
            _shoppingBasketContext.AddProduct("testCart4", testProduct.Identifier, 1);
            _shoppingBasketContext.Checkout("testCart4");

            //Assert
            _mockProductContext.Verify(x => x.GetProducts(), Times.Exactly(3));
            testProduct.Stock.Should().Be(2);
        }

        [Test]
        public void Checkout_WhenCheckoutIsCalled_CheckThatCartIsDeleted()
        {
            //Arrange
            var testProducts = new List<Product>();
            var testProduct = Builder<Product>.CreateNew().With(x => x.Identifier = 1)
                .With(x => x.Stock = 3).Build();
            testProducts.Add(testProduct);
            _mockProductContext.Setup(x => x.GetProducts()).Returns(testProducts);

            //Act
            _shoppingBasketContext.AddProduct("testCart4", testProduct.Identifier, 1);
            _shoppingBasketContext.Checkout("testCart4");
            var cart = _shoppingBasketContext.GetShoppingCart("testCart4");

            //Assert
            _mockProductContext.Verify(x => x.GetProducts(), Times.Exactly(3));
            cart.Should().BeNull();
        }

        [Test]
        public void Checkout_WhenCheckoutWithMoreQuantityThanStock_CheckThatCheckoutIsNotHappened()
        {
            //Arrange
            var testProducts = new List<Product>();
            var testProduct = Builder<Product>.CreateNew().With(x => x.Identifier = 1)
                .With(x => x.Stock = 3).Build();
            testProducts.Add(testProduct);
            _mockProductContext.Setup(x => x.GetProducts()).Returns(testProducts);

            //Act
            _shoppingBasketContext.AddProduct("testCart5", testProduct.Identifier, 3);
            testProduct.Stock = 1;
            _shoppingBasketContext.Checkout("testCart5");
            var cart = _shoppingBasketContext.GetShoppingCart("testCart5");

            //Assert
            _mockProductContext.Verify(x => x.GetProducts(), Times.Exactly(2));
            testProduct.Stock.Should().Be(1);
            cart.Should().NotBeNull();
            cart.Count.Should().Be(1);
        }
    }
}
