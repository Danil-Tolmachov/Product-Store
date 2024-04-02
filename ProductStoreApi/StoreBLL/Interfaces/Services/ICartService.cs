using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface ICartService
    {
		Task<IEnumerable<ProductModel>> GetUserProducts(long userId);
		Task ClearUserCart(long userId);

		Task AddProduct(CartItemModel item, long userId);
		Task RemoveProduct(CartItemModel item, long userId);
		Task SubmitOrder(long userId);

	}
}
