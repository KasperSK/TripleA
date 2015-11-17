using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CashRegister.Database;
using CashRegister.Log;

namespace CashRegister.Dal
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ILogger _logger = LogFactory.GetLogger(typeof (Repository<TEntity>));
        internal CashRegisterContext Context;
        internal DbSet<TEntity> DbSet;

        public Repository(CashRegisterContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            _logger.Debug("Generic Repository instantiatet");
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            foreach (var includeProperty in includeProperties ?? new string[0])
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }
    }
}