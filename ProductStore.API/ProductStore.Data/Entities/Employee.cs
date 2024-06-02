
namespace ProductStore.Data.Entities
{
	public class Employee : BaseEntity
	{
		public long UserId { get; set; }
		public long PositionId { get; set; }

		public virtual User User { get; set; } = null!;
		public virtual Position Position { get; set; } = null!;

		public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();

		public Employee(long id) : base(id) { }
	}
}
