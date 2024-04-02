
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class CartItemModel
	{
		public int Quantity { get; set; }

		public CartModel Cart { get; set; } = null!;
		public ProductModel Product { get; set; } = null!;
	}
}
