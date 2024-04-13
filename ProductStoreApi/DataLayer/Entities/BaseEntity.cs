using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Entities
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
