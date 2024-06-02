
namespace ProductStore.Data.Entities
{
	public class Position : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public virtual IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

		public Position(long id) : base(id) { }
	}
}
