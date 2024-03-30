using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class User : BaseEntity
	{
		public long PersonId { get; set; }
		public bool IsActive { get; set; } = true;
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;

		public virtual Person Person { get; set; } = null!;

		public virtual IEnumerable<Order> Orders { get; set; } = Enumerable.Empty<Order>();

		public User(int id) : base(id) { }
	}
}
