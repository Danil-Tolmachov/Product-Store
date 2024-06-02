using System.ComponentModel.DataAnnotations;

namespace ProductStore.Business.Models.Extra
{
	public class AddCartItemModel
	{
		[Required]
		public long ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
	}
}
