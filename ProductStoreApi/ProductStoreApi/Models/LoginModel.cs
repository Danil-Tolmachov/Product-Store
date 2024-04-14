using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductStoreApi.Models
{
	public class LoginModel
	{
		[Required]
		[JsonInclude]
		public readonly string username;
		[Required]
		[JsonInclude]
		public readonly string password;

		public LoginModel(string username, string password)
		{
			this.username = username;
			this.password = password;
		}
	}
}
