using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Models
{
	public class ContactModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

		public PersonModel Person { get; set; } = null!;
	}
}
