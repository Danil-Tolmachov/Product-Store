using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Interfaces
{
    public interface ISingleKeyRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
	{
		Task<TEntity> GetByIdAsync(long id);
		Task DeleteByIdAsync(long id);
	}
}
