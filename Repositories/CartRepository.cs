using ApiCart.Domain;
using ApiCart.Repositories.Interfaces;

namespace ApiCart.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly Context _context;

        public CartRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Cart>> ListAll()
        {
            return await Task.FromResult(_context.Carts.ToList());
        }

        public async Task<Cart> GetByID(long id)
        {
            var cart = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            return await Task.FromResult(cart ?? new Cart { Id = 0});
        }

        public async Task<Cart> Create(Product product, long id)
        {
            var cart = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            if(cart == null)
            {
                cart = new Cart { Id = id, Products = new List<Product>()};
                cart.Products.Add(product);
                _context.Carts.Add(cart);
            }
            else
            {
                cart.Products?.Add(product);
            }

            return await Task.FromResult(cart);
        }

        public async Task DeleteProduct(long idCart, long idProduct)
        {
            var cart = _context.Carts.Where(c => c.Id == idCart).FirstOrDefault();
            if (cart == null) throw new FileNotFoundException("Carrinho não encontrado");
            var product = cart?.Products.Where(c => c.Id == idProduct).FirstOrDefault();
            if (product == null) throw new FileNotFoundException("Produto não encontrado");
            cart.Products.Remove(product);
            await Task.FromResult(true);
        }

        public async Task Delete(long id)
        {
            var cart = _context.Carts.Where(c => c.Id == id).FirstOrDefault();
            if (cart == null) throw new FileNotFoundException("Carrinho não encontrado");
            _context.Carts.Remove(cart);
            await Task.FromResult(true);
        }
    }
}
