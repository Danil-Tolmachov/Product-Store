
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class PersonModel
	{
		public long Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public decimal Discount { get; set; }
		public string Address { get; set; } = string.Empty;

		public UserModel User { get; set; } = null!;
		public IEnumerable<ContactModel> Contacts { get; set; } = new List<ContactModel>();
	}
}
