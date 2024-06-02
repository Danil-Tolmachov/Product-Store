
namespace ProductStore.Business.Models.Dto
{
	public class ProductDto
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public decimal OriginalPrice { get; set; }
		public string Description { get; set; } = string.Empty;
		public string UnitMeasure { get; set; } = string.Empty;

		public CategoryBriefDto? Category { get; set; }
		public IEnumerable<SpecificationDto>? Specifications { get; set; }
		public IEnumerable<ImageDto>? Images { get; set; }
	}
}
