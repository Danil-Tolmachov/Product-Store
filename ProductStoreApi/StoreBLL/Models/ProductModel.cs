
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

		public string CategoryId { get; set; } = null!;
		public string CategoryName { get; set; } = string.Empty;

		public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();
		public IEnumerable<string> ImagePaths { get; set; } = new List<string>();
	}
}
