using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Person : BaseEntity
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public decimal Discount { get; set; }
		public string Address { get; set; } = string.Empty;

		public virtual User User { get; set; } = null!;
		public virtual IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();

		public Person(long id) : base(id) { }
	}
}
