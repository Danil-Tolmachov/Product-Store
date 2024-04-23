namespace StoreBLL.Models.Dto
{
	public class OrderDto
	{
		public int Id { get; set; }
		public long EmployeeId { get; set; }

		public string? UserComment { get; set; }
		public required string Status { get; set; }
		
		public IEnumerable<OrderDetailDto>? Details { get; set; }

	}
}
