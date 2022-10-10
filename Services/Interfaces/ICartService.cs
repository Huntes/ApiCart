using ApiCart.Models;

namespace ApiCart.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartModel>> ListAll();
        Task<List<ProductModel>> GetByID(long id);
        Task<CartModel> Create(ProductModel product, long idCart);
        Task<FinishCartModel> Finish(int id);
        Task DeleteProduct(long idCart, long idProduct);
        Task Delete(int id);
    }
}
