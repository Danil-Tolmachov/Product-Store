
using Microsoft.VisualBasic;

namespace StoreDAL.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

		public Category(long id) : base(id) { }
	}
}
