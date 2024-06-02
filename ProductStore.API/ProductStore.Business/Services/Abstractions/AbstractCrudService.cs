using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreBLL.Interfaces;
using StoreBLL.Models;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces;

namespace StoreBLL.Services.Abstractions
{
    public abstract class AbstractCrudService<TEntity, TModel> : ICrud<TModel> where TEntity : class, IBaseEntity
    {
        private readonly IMapper _mapper;
        private readonly ISingleKeyRepository<TEntity> _repository;

        protected AbstractCrudService(IMapper mapper, ISingleKeyRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<int> Count()
        {
            return await _repository.Count();
        }

        public virtual async Task Add(TModel model)
        {
            await _repository.AddAsync(_mapper.Map<TEntity>(model));
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            List<TEntity> entities = (await _repository.GetAllAsync()).ToList();
            return _mapper.Map<IList<TModel>>(entities);
        }

        public virtual async Task<IEnumerable<TModel>> GetAll(int pageNumber, int rowCount)
        {
            List<TEntity> entities = (await _repository.GetAllAsync(pageNumber, rowCount)).ToList();
            return _mapper.Map<IList<TModel>>(entities);
        }

        public virtual async Task<TModel> GetById(long id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task DeleteById(long id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public virtual void Delete(TModel model)
        {
            _repository.Delete(_mapper.Map<TEntity>(model));
        }

        public virtual void Update(TModel model)
        {
            _repository.Update(_mapper.Map<TEntity>(model));
        }
    }
}
