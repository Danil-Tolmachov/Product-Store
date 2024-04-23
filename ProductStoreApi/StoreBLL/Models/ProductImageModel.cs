
namespace StoreBLL.Models
{
	public class ProductImageModel
	{
		public long Id { get; set; }

		public required string ImagePath { get; set; }
		public string? Alt { get; set; }

		public ProductModel? Product { get; set; }
	}
}
