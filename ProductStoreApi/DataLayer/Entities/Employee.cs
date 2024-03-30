using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
{
	public class Employee : BaseEntity
	{
		public long UserId { get; set; }
		public long PositionId { get; set; }

		public virtual User User { get; set; } = null!;
		public virtual Position Position { get; set; } = null!;

		public Employee(int id) : base(id) { }
	}
}
