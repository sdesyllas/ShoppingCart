namespace ShoppingCart.Domain
{
    public class Product
    {
        public int Identifier { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
