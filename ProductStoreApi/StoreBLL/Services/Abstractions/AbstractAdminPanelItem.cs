using AutoMapper;
using StoreBLL.Interfaces;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Services.Abstractions
{
	public class AbstractAdminPanelItem<TEntity, TModel> : AbstractCrudService<TEntity, TModel>, IAdminPanelItem<TModel>
		where TModel : class
		where TEntity : class, IBaseEntity
	{
		private readonly IRepository<TEntity> _repository;

		public AbstractAdminPanelItem(IMapper mapper, IRepository<TEntity> repository) : base(mapper, repository)
		{
			_repository = repository;
		}

		public virtual async Task<int> CountPages(int rowCount = 5)
		{
			var count = Math.Ceiling(await _repository.Count() / (double)rowCount);
			return count > 0 ? (int)count : 1;
		}

		public Type GetModelType()
		{
			return typeof(TModel);
		}
	}
}
