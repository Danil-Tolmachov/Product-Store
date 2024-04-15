using System.ComponentModel.DataAnnotations;

namespace StoreBLL.Models.Extra
{
	public class RefreshModel
	{
		[Required]
		public string Token { get; set; } = null!;
	}
}
