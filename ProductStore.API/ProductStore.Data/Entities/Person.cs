﻿
namespace ProductStore.Data.Entities
{
	public class Person : BaseEntity
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public decimal Discount { get; set; } = 0m;
		public string? Address { get; set; }

		public virtual User User { get; set; } = null!;
		public virtual IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();

		public Person(long id) : base(id) { }
	}
}
