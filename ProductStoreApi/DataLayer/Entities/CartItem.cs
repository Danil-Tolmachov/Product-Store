
namespace StoreDAL.Entities
{
	public class CartItem : BaseEntity
	{
		public long CartId { get; set; }
		public long ProductId { get; set; }
		public int Quantity { get; set; }

		public virtual Cart Cart { get; set; } = null!;
		public virtual Product Product { get; set; } = null!;

		public CartItem(int id) : base(id) { }
	}
}
