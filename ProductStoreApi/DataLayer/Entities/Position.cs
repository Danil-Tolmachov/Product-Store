using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Position : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public virtual IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

		public Position(long id) : base(id) { }
	}
}
