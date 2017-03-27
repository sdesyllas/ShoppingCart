using System.Collections.Generic;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Business.Tests
{
    [TestFixture]
    public class ProductContextTests
    {
        private Mock<IConfig> _mockConfig;

        private Mock<IDataSource> _mockDataSource;

        private IProductContext _productContext;

        [OneTimeSetUp]
        public void SetupMocks()
        {
            _mockConfig = new Mock<IConfig>();
            _mockDataSource = new Mock<IDataSource>();
            _productContext = new ProductContext(_mockDataSource.Object, _mockConfig.Object);
        }

        [Test]
        public void GetProducts_CanReturn_AnyProductsFromDataSource()
        {
            //Arrange
            _mockConfig.Setup(x => x.GetDataSourcePath()).Returns(It.IsAny<string>());
            var expectedProducts = Builder<Product>.CreateListOfSize(10).Build();
            _mockDataSource.Setup(x => x.LoadProducts(It.IsAny<string>())).Returns(expectedProducts);

            //Act
            var products = _productContext.GetProducts();

            //Assert
            _mockConfig.Verify(x=>x.GetDataSourcePath(), Times.Once);
            _mockDataSource.Verify(x => x.LoadProducts(It.IsAny<string>()), Times.Once);
            products.ShouldAllBeEquivalentTo(expectedProducts);
        }
    }
}
