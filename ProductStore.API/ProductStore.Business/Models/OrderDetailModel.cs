
namespace ProductStore.Business.Models
{
	public class OrderDetailModel
	{
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }

		public long OrderId { get; set; }
		public long ProductId { get; set; }

		public ProductModel? Product { get; set; }
		public OrderModel? Order { get; set; }
	}
}
