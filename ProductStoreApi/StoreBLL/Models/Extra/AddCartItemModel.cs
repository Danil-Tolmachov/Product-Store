using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models.Extra
{
	public class AddCartItemModel
	{
		[Required]
		public long ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
	}
}
