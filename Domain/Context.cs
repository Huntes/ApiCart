namespace ApiCart.Domain
{
    public class Context
    {
        public  IList<Cart> Carts = new List<Cart>();
        public  IList<Product> Products = new List<Product>();
    }
}
