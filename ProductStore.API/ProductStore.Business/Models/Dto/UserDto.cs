namespace StoreBLL.Models.Dto
{
	public class UserDto
	{
		public long Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Discount { get; set; } = string.Empty;
		public string? Address { get; set; }

		public CartDto? Cart { get; set; }
		public IEnumerable<ContactDto>? Contacts { get; set; }

		public IEnumerable<OrderBriefDto>? Orders { get; set; }
	}
}
