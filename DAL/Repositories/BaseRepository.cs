using DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly IDBUnitOfWork unitOfWork;
        internal DbSet<TEntity> dbSet;
        public BaseRepository(IDBUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            this.unitOfWork = unitOfWork;
            this.dbSet = this.unitOfWork.Db.Set<TEntity>();
        }

        public IDBUnitOfWork UnitOfWork { get { return this.unitOfWork; } }
        internal ProductModelContext Db { get { return this.unitOfWork.Db; } }

        public virtual IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;
            return query.ToList();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            Save();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            Save();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Db.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            Db.Set<TEntity>().Remove(entityToDelete);
            Save();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Db.Entry(entityToUpdate).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            unitOfWork.Save();
        }
    }
}
