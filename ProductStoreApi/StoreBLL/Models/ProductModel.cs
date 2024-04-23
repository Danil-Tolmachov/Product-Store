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

		public string? CategoryId { get; set; }
		public string? CategoryName { get; set; }

		public IEnumerable<SpecificationModel> Specifications { get; set; } = new List<SpecificationModel>();
		public IEnumerable<ProductImageModel> Images { get; set; } = new List<ProductImageModel>();
	}
}
