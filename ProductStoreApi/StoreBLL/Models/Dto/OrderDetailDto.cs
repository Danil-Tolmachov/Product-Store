namespace StoreBLL.Models.Dto
{
	public class OrderDetailDto
	{
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public required ProductDto Product { get; set; }
	}
}
