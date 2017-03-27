using System.Collections.Generic;
using ShoppingCart.Domain;

namespace ShoppingCart.Abstractions
{
    public interface IShoppingBasketContext
    {
        void AddProduct(string cartName, int productId, int quantity);

        void Checkout(string cartName);

        List<Product> GetShoppingCart(string cartName);
    }
}
