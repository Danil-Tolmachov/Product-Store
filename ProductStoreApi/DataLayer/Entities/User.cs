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
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;

		public virtual Person Person { get; set; } = null!;
		public virtual Cart Cart { get; set; } = null!;
		public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();

		public User(long id) : base(id) { }
	}
}
