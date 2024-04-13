
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class OrderDetailModel
	{
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }

		public long OrderId { get; set; }
		public long ProductId { get; set; }

		public string ProductName { get; set; } = string.Empty;
		public string ProductDescription { get; set; } = string.Empty;
		public long ProductCategoryId { get; set; }
		public string ProductCategoryName { get; set; } = string.Empty;
	}
}
