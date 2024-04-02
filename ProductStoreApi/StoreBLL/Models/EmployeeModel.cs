
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }
		public string Position { get; set; } = null!;

		public UserModel User { get; set; } = null!;

		public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();
	}
}
