
namespace StoreDAL.Entities
{
	public class ProductImage : BaseEntity
	{
		public long ProductId { get; set; }
		public byte[] Image { get; set; } = Array.Empty<byte>();
		public string? Alt { get; set; }

		public virtual Product Product { get; set; } = null!;

		public ProductImage(long id) : base(id) { }
	}
}
