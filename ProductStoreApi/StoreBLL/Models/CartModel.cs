
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class CartModel
	{
		public User User { get; set; } = null!;

		public IEnumerable<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
	}
}
