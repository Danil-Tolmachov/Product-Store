
namespace ProductStore.Business.Models.Dto
{
	public class CartDto
	{
		public IEnumerable<CartItemDto> Items { get; set; } = new List<CartItemDto>();
		public decimal Total { get; set; }
	}
}
