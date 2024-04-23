namespace StoreBLL.Models.Dto
{
	public class CategoryDto
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ProductDto[]? Products { get; set; }
	}
}
