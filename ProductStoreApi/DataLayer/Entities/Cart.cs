using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities
{
	public class Cart : BaseEntity
	{
		[Column("user_id")]
		public long UserId { get; set; }

		public virtual User User { get; set; } = null!;

		public virtual IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();

		public Cart(long id) : base(id) { }
	}
}
