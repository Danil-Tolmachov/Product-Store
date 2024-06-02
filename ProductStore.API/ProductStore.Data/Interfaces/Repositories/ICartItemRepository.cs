using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface ICartItemRepository : IRepository<CartItem>
	{
		Task<CartItem> GetByIdAsync(long cartId, long productId);
		Task DeleteByIdAsync(long cartId, long productId);
	}
}
