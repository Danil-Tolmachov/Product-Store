using System.ComponentModel.DataAnnotations;

namespace ProductStore.Business.Models.Extra
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
}
