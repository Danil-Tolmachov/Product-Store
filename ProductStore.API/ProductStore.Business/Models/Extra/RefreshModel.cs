using System.ComponentModel.DataAnnotations;

namespace ProductStore.Business.Models.Extra
{
	public class RefreshModel
	{
		[Required]
		public string Token { get; set; } = null!;
	}
}
