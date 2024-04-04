
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class CartItemModel
	{
		public int Quantity { get; set; }

		public long CartId { get; set; }
		public long CartUserId { get; set; }
		public long ProductId { get; set; }

		public string ProductName { get; set; } = string.Empty;
		public string ProductDescription { get; set; } = string.Empty;
		public long ProductCategoryId { get; set; }
		public string ProductCategoryName { get; set; } = string.Empty;
		public decimal ProductPrice { get; set;}
		public decimal ProductDiscount { get; set; }
	}
}
