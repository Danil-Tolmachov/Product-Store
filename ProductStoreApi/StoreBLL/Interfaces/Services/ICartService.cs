using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface ICartService
    {
		Task<IEnumerable<CartItemModel>> GetUserProducts(long userId);
		Task ClearUserCart(long userId);

		Task AddProduct(CartItemModel item, long userId);
		Task RemoveProduct(ProductModel product, long userId);
		Task SubmitOrder(long userId, string? userComment);

	}
}
