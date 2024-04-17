using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface ICartRepository : ISingleKeyRepository<Cart>
	{
		Task ClearCartByUserId(long userId);
		Task<Cart> GetCartByUserId(long userId);
		Task<IEnumerable<CartItem>> GetUserProducts(long userId);
	}
}
