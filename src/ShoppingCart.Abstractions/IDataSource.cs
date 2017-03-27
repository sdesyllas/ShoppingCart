using System.Collections.Generic;
using ShoppingCart.Domain;

namespace ShoppingCart.Abstractions
{
    public interface IDataSource
    {
        IEnumerable<Product> LoadProducts(string path);
    }
}
