﻿
namespace ProductStore.Data.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

		public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

		public Category() : base(0) { }
		public Category(long id) : base(id) { }
	}
}
