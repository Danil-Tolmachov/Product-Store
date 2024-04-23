using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models.Dto
{
	public class OrderBriefDto
	{
		public int Id { get; set; }
		public long EmployeeId { get; set; }

		public string? UserComment { get; set; }
		public required string Status { get; set; }
	}
}
