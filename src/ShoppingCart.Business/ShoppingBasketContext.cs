using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Business
{
    public class ShoppingBasketContext : IShoppingBasketContext
    {
        private static Dictionary<string, List<Product>> _baskets;

        private Dictionary<string, List<Product>> Baskets
            => _baskets ?? (_baskets = new Dictionary<string, List<Product>>());

        private readonly IProductContext _productContext;

        public ShoppingBasketContext(IProductContext productContext)
        {
            _productContext = productContext;
        }

        public void AddProduct(string cartName, int productId, int quantity)
        {
            var availableProduct = this._productContext.GetProducts().FirstOrDefault(x => x.Identifier == productId);
            if (quantity > availableProduct?.Stock)
                return;
            
            if (this.Baskets.ContainsKey(cartName))
            {
                var existingProduct = this.Baskets[cartName].FirstOrDefault(x => x.Identifier == productId);

                if (existingProduct != null)
                {
                    this.Baskets[cartName].Remove(existingProduct);
                    quantity += existingProduct.Stock;
                }
                if (availableProduct == null) return;
                Product cartProduct = CreateCartProduct(availableProduct, quantity);
                this.Baskets[cartName].Add(cartProduct);
            }
            else
            {
                Product cartProduct = CreateCartProduct(availableProduct, quantity);
                this.Baskets.Add(cartName, new List<Product> { cartProduct });
            }
        }

        public void Checkout(string cartName)
        {
            if (!this.Baskets.ContainsKey(cartName))
                return;

            var shoppingBasket = this.Baskets[cartName];

            if (!CanCheckout(shoppingBasket))
                return;

            UpdateQuantities(shoppingBasket);

            ClearShoppingCart(cartName);
        }


        private bool CanCheckout(List<Product> shoppingBasket)
        {
            foreach (var cartProduct in shoppingBasket)
            {
                var realProduct = _productContext.GetProducts().FirstOrDefault(x => x.Identifier == cartProduct.Identifier);
                if (realProduct == null)
                    return false;

                if (realProduct.Stock < cartProduct.Stock)
                    return false;
            }
            return true;
        }

        private void UpdateQuantities(List<Product> shoppingBasket)
        {
            foreach (var cartProduct in shoppingBasket)
            {
                var realProduct =
                    _productContext.GetProducts().FirstOrDefault(x => x.Identifier == cartProduct.Identifier);
                if (realProduct != null) realProduct.Stock -= cartProduct.Stock;
            }
        }

        private void ClearShoppingCart(string cartName)
        {
            this.Baskets.Remove(cartName);
        }

        private Product CreateCartProduct(Product product, int quantity)
        {
            //TODO: use an object clone technique -- Automapper maybe
            Product cartProduct = new Product
            {
                Stock = quantity,
                Description = product.Description,
                Identifier = product.Identifier,
                Name = product.Name,
                Price = product.Price
            };
            return cartProduct;
        }

        public List<Product> GetShoppingCart(string cartName)
        {
            return !this.Baskets.ContainsKey(cartName) ? null : this.Baskets[cartName];
        }
    }
}
