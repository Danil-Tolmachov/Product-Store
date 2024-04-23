using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models.Dto
{
	public class ImageDto
	{
		public required string Path { get; set; }
		public string? Alt { get; set; }
	}
}
