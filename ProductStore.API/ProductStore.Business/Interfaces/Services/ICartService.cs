using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface ICartService
    {
		Task<IEnumerable<CartItemModel>> GetUserProducts(long userId);
		Task<CartModel> GetUserCart(long userId);
		Task ClearUserCart(long userId);

		Task AddProduct(CartItemModel model, long userId);
		Task RemoveProduct(ProductModel product, long userId);
		Task SubmitOrder(long userId, string? userComment);

	}
}
