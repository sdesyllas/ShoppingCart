using System.Collections.Generic;
using ShoppingCart.Domain;

namespace ShoppingCart.Abstractions
{
    public interface IProductContext
    {
        IEnumerable<Product> GetProducts();
    }
}
