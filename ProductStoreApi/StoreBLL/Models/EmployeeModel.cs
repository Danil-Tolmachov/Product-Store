
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }
		public string Position { get; set; } = string.Empty;

		public long UserId { get; set; }
		public string Username { get; set; } = string.Empty;

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();
	}
}
