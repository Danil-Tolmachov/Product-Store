
namespace StoreDAL.Entities
{
	public class Cart : BaseEntity
	{
		public long UserId { get; set; }

		public virtual User User { get; set; } = null!;

		public virtual IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();

		public Cart() : base(0) { }
		public Cart(long id) : base(id) { }
	}
}
