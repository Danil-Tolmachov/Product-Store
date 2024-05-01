using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models.Extra
{
	public class UpdateUserModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? Address { get; set; }
	}
}
