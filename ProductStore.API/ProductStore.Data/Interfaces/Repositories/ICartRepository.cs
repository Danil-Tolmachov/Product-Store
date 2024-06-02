using ProductStore.Data.Entities;

namespace ProductStore.Data.Interfaces.Repositories
{
	public interface ICartRepository : ISingleKeyRepository<Cart>
	{
		Task ClearCartByUserId(long userId);
		Task<Cart> GetCartByUserId(long userId);
		Task<IEnumerable<CartItem>> GetUserProducts(long userId);
	}
}
