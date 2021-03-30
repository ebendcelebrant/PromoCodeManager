using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity GetById(TKey id);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);

        bool AddRange(IQueryable<TEntity> entities);

        bool Remove(TEntity entity);

        bool RemoveById(TKey id);

        TEntity Update(TEntity entity);

        bool RemoveRange(IQueryable<TEntity> entities);

        bool Exists(TKey id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = ""); 
        int Complete();
    }
}
