using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Data.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
		[Column("id")]
		public long Id { get; set; }

        protected BaseEntity(long id)
        {
            this.Id = id;
        }
    }
}
