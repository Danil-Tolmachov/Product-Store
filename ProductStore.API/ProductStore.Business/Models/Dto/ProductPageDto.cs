
namespace ProductStore.Business.Models.Dto
{
	public class ProductPageDto
	{
		public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

		public int? CurrentPage { get; set; }

		public int? TotalCount { get; set; }
		public int? PagesCount { get; set; }
		public int? PageSize { get; set; }
	}
}
