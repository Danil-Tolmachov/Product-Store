using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Contact : BaseEntity
	{
		public long PersonId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

		public virtual Person Person { get; set; } = null!;

		public Contact(long id) : base(id) { }
	}
}
