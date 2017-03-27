using System.Collections.Generic;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Business
{
    public class ProductContext : IProductContext
    {
        private static IEnumerable<Product> _products;

        private IEnumerable<Product> Products
            => _products ?? (_products = _dataSource.LoadProducts(_config.GetDataSourcePath()));

        private readonly IDataSource _dataSource;

        private readonly IConfig _config;

        public ProductContext(IDataSource dataSource, IConfig config)
        {
            _dataSource = dataSource;
            _config = config;
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.Products;
        }
    }
}
