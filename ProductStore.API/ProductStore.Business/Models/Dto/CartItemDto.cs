
namespace ProductStore.Business.Models.Dto
{
	public class CartItemDto
	{
		public required ProductDto Product { get; set; }
		public required int Quantity { get; set; }
	}
}
