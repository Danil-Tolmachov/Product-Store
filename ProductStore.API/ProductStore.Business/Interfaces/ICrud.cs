using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
	public interface ICrud<TModel>
	{
		Task<int> Count();

		Task Add(TModel model);

		Task<IEnumerable<TModel>> GetAll();

		Task<IEnumerable<TModel>> GetAll(int pageNumber, int rowCount);

		Task<TModel> GetById(long id);

		Task DeleteById(long id);

		void Delete(TModel model);

		void Update(TModel model);
	}
}
