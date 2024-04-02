
using StoreDAL.Entities;

namespace StoreBLL.Models
{
	public class SpecificationModel
	{
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

		public IEnumerable<ProductModel> Products { get; set; } = new List<ProductModel>();
	}
}
