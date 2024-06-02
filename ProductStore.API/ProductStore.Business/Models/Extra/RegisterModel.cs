using System.ComponentModel.DataAnnotations;

namespace ProductStore.Business.Models.Extra
{
	public class RegisterModel
	{
		[Required]
		public string Username { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;

		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string LastName { get; set; } = string.Empty;

		public string? Address { get; set; }
		public string? Phone { get; set; }
	}
}
