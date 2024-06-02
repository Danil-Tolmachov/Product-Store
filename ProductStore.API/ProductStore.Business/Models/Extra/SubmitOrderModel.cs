using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models.Extra
{
	public class SubmitOrderModel
	{
		public required long UserId { get; set; }
		public string? UserComment { get; set; }
		public IEnumerable<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
	}
}
