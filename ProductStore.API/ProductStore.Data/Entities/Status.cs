
namespace ProductStore.Data.Entities
{
	public class Status : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public Status(long id) : base(id) { }
	}
}
