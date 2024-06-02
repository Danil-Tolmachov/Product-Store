
namespace ProductStore.Data.Entities
{
	public class OrderDetail : BaseEntity
	{
		public long ProductId { get; set; }
		public long OrderId { get; set; }

		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }

		public virtual Product Product { get; set; } = null!;
		public virtual Order Order { get; set; } = null!;

		public OrderDetail() : base(0) { }
		public OrderDetail(long id) : base(id) { }
	}
}
