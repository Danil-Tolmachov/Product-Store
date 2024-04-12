using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models
{
	public class ProductImageModel
	{
		public long Id { get; set; }
		public string ImagePath { get; set; } = string.Empty;
		public long ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
	}
}
