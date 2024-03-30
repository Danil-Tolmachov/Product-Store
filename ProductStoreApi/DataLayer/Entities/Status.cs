using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Status : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public Status(int id) : base(id) { }
	}
}
