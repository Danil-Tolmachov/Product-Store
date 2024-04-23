using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models.Dto
{
	public class CartDto
	{
		public IEnumerable<CartItemDto> Items { get; set; } = new List<CartItemDto>();
	}
}
