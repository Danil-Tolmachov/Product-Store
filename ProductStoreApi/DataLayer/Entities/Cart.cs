using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities
{
	public class Cart : BaseEntity
	{
		[Column("user_id")]
		public long UserId { get; set; }

		public virtual User User { get; set; } = null!;

		public virtual IEnumerable<CartItem> CartItems { get; set; } = Enumerable.Empty<CartItem>();

		public Cart(int id) : base(id) { }
	}
}
