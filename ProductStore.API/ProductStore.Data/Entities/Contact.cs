
namespace ProductStore.Data.Entities
{
	public class Contact : BaseEntity
	{
		public long PersonId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

		public virtual Person Person { get; set; } = null!;

		public Contact(long id) : base(id) { }
	}
}
