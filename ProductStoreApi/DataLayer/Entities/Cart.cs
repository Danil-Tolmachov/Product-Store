using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities
{
	public class Cart : BaseEntity
	{
		[Column("person_id")]
		public long PersonId { get; set; }

		public virtual Person Person { get; set; } = null!;

		public virtual IEnumerable<CartItem> CartItems { get; set; } = Enumerable.Empty<CartItem>();

		public Cart(int id) : base(id) { }
	}
}
