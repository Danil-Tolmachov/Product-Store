
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class OrderDetailModel
	{
		public int Id { get; set; }

		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }

		public ProductModel Product { get; set; } = null!;
		public OrderModel Order { get; set; } = null!;
	}
}
