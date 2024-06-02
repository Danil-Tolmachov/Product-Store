
namespace ProductStore.Data.Entities
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

		public Product() : base(0) { }
		public Product(long id) : base(id) { }
	}
}
