using NUnit.Framework;
using ShoppingCart.Abstractions;
using ShoppingCart.Business;
using ShoppingCart.Common;
using ShoppingCart.Service.Controllers;

namespace ShoppingCart.Service.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        private ProductsController _productsController;
        private ShoppingBasketController _shoppingBasketController;

        [OneTimeSetUp]
        public void SetUp()
        {
            IConfig config = new ShoppingCartConfig();
            IDataSource dataSource = new CsvDataSource();
            IProductContext productContext = new ProductContext(dataSource, config);
            IShoppingBasketContext shoppingBasketContext = new ShoppingBasketContext(productContext);
            _productsController = new ProductsController(productContext);
            _shoppingBasketController = new ShoppingBasketController(shoppingBasketContext);
        }

        [Test]
        public void Client_Integration_Scenario()
        {
            
        }
    }
}
