using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Services
{
    public abstract class BaseService<TEntity> where TEntity : class
    {
        protected DAL.Repositories.IRepository<TEntity> genericRepository;

        public BaseService(DAL.Repositories.IRepository<TEntity> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return genericRepository.GetAll();
        }

        public IEnumerable<TEntity> Get( Expression<Func<TEntity, bool>> filter,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                  string includeProperties)
        {
           return genericRepository.Get(filter, orderBy, includeProperties);
        }

        public TEntity GetByID(object id)
        {
            return genericRepository.GetByID(id);
        }

        public void Insert(TEntity item)
        {
            genericRepository.Insert(item);
        }

        public void Update(TEntity item)
        {
            genericRepository.Update(item);
        }

        public void Delete(object id)
        {
            genericRepository.Delete(id);
        }

        public void Delete(TEntity item)
        {
            genericRepository.Delete(item);
        }
    }
}
