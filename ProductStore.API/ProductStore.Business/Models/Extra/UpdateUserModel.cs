
namespace ProductStore.Business.Models.Extra
{
	public class UpdateUserModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? Address { get; set; }
	}
}
