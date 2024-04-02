
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class UserModel
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;

		public PersonModel Person { get; set; } = null!;
		public CartModel Cart { get; set; } = null!;
		public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();
	}
}
