using System.Collections.Generic;
using System.Web.Http;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Service.Controllers
{
    public class ShoppingBasketController : ApiController
    {
        private readonly IShoppingBasketContext _shoppingBasketContext;

        public ShoppingBasketController(IShoppingBasketContext shoppingBasketContext)
        {
            _shoppingBasketContext = shoppingBasketContext;
        }

        [Route("api/ShoppingBasket/{cartname}")]
        [HttpGet]
        public IEnumerable<Product> Get(string cartName)
        {
            var shoppingBasket = _shoppingBasketContext.GetShoppingCart(cartName);
            return shoppingBasket;
        }

        [Route("api/ShoppingBasket/{cartname}/Checkout")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult CheckOut(string cartName)
        {
            _shoppingBasketContext.Checkout(cartName);
            return Ok();
        }

        [Route("api/ShoppingBasket/{cartname}/Add/{productId}/{quantity}")]
        [HttpGet]
        [HttpPut]
        public IHttpActionResult AddProduct(string cartName, int productId, int quantity)
        {
            _shoppingBasketContext.AddProduct(cartName, productId, quantity);
            return Ok();
        }
    }
}
