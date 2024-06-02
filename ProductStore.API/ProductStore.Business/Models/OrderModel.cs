
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string UserComment { get; set; } = string.Empty;
		public virtual string Status { get; set; } = string.Empty;

		public long UserId { get; set; }
		public string UserUsername { get; set; } = string.Empty;

		public long EmployeeId { get; set; }

		public bool IsCanceled { get; set; }
		public bool IsCompleted { get; set; }

		public IEnumerable<OrderDetailModel> Details { get; set; } = new List<OrderDetailModel>();
	}
}
