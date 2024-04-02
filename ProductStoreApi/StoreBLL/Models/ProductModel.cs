
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class ProductModel
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public string UnitMeasure { get; set; } = string.Empty;

		public CategoryModel Category { get; set; } = null!;
		public IEnumerable<SpecificationModel> Specifications { get; set; } = new List<SpecificationModel>();
		public IEnumerable<byte[]> Images { get; set; } = new List<byte[]>();
	}
}
