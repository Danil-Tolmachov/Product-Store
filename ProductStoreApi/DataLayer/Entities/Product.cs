using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public string UnitMeasure { get; set; } = string.Empty;
		public long CategoryId { get; set; }

		public virtual Category Category { get; set; } = null!;
		public virtual IEnumerable<Specification> Specifications { get; set; } = new List<Specification>();
		public virtual IEnumerable<ProductImage> Images { get; set; } = new List<ProductImage>();

		public Product(long id) : base(id) { }
	}
}
