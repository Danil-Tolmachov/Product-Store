
namespace ProductStore.Business.Models.Extra
{
	public class SubmitOrderModel
	{
		public required long UserId { get; set; }
		public string? UserComment { get; set; }
		public IEnumerable<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
	}
}
