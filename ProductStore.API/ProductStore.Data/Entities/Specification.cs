
namespace ProductStore.Data.Entities
{
	public class Specification : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

		public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

		public Specification(long id) : base(id) { }
	}
}
