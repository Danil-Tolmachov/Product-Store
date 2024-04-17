
namespace StoreDAL.Entities
{
	public class ProductImage : BaseEntity
	{
		public byte[] Image { get; set; } = Array.Empty<byte>();
		public long ProductId { get; set; }

		public virtual Product Product { get; set; } = null!;

		public ProductImage(long id) : base(id) { }
	}
}
