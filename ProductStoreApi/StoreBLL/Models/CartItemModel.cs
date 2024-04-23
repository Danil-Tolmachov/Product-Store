
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class CartItemModel
	{
		public int Quantity { get; set; }

		public long CartId { get; set; }
		public long CartUserId { get; set; }
		public long ProductId { get; set; }

		public ProductModel Product { get; set; } = null!;

		public string ImagePath { get; set; } = string.Empty;
	}
}
