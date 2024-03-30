
using Microsoft.VisualBasic;

namespace StoreDAL.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public virtual IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

		public Category(int id) : base(id) { }
	}
}
