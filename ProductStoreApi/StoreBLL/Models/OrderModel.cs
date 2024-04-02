
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string UserComment { get; set; } = string.Empty;
		public virtual string Status { get; set; } = string.Empty;

		public UserModel User { get; set; } = null!;
		public EmployeeModel Employee { get; set; } = null!;

		public IEnumerable<OrderDetailModel> Details { get; set; } = new List<OrderDetailModel>();
	}
}
