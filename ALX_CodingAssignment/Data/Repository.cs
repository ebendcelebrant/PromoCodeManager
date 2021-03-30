using Microsoft.EntityFrameworkCore;
using ALX_CodingAssignment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ALX_CodingAssignment.Data
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger<Repository<TEntity, TKey>> _logger;
        private bool _disposed;

        public Repository()
        {
            _context = new DataContext();
            _dbSet = _context.Set<TEntity>();
        }

        protected Repository(DbContext context, ILogger<Repository<TEntity, TKey>> logger)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _logger = logger;
        }

        public virtual TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).SingleOrDefault();
        }

        public virtual TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);

            return entity;
        }

        public virtual bool AddRange(IQueryable<TEntity> entities)
        {
            _dbSet.AddRange(entities);

            return true;
        }

        public virtual bool Remove(TEntity entity)
        {
            _dbSet.Remove(entity);

            return true;
        }

        public virtual bool RemoveById(TKey id)
        {
            var t = GetById(id);
            _dbSet.Remove(t);

            return true;
        }

        public TEntity Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if ((entry.State == EntityState.Added) || (entry.State == EntityState.Unchanged))
                entry.State = EntityState.Detached;
            _dbSet.Attach(entity);
            entry.State = EntityState.Modified;

            return entity;
        }

        public void Detach(TEntity entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public virtual bool RemoveRange(IQueryable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return true;
        }

        public virtual bool Exists(TKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                return false;
            else
            {
                Detach(entity);
                return true;
            }
        }

        //This is an addition to add eager loading and bypass stored proc. If a big system, stored procs might be more efficient'

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

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
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (ValidationException validationError)
            {
                _logger.LogError($"Property: {validationError.Data} Error: {validationError.Message} Inner Exception: {validationError.InnerException}");

                Trace.TraceInformation($"Property: {validationError.Data} Error: {validationError.Message} Inner Exception: {validationError.InnerException}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            finally
            {
                Dispose();
            }
            return -99;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

    }
}