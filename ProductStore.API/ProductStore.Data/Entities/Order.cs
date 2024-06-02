using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Order : BaseEntity
	{
		public long UserId { get; set; }
		public long EmployeeId { get; set; }
		public long StatusId { get; set; }
		public string? UserComment { get; set; }

		public virtual User User { get; set; } = null!;
		public virtual Employee Employee { get; set; } = null!;
		public virtual Status Status { get; set; } = null!;

		public virtual IEnumerable<OrderDetail> Details { get; set; } = new List<OrderDetail>();

		public Order(long id) : base(id) { }
	}
}
