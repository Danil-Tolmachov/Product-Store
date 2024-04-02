
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public virtual IEnumerable<ProductModel> Products { get; set; } = new List<ProductModel>();
	}
}
