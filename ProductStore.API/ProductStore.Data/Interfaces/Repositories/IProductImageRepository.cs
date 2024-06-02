using StoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IProductImageRepository : ISingleKeyRepository<ProductImage>
	{
		Task<IEnumerable<ProductImage>> GetAllByProductIdAsync(long productId);
	}
}
