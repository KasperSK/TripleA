using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CashRegister.Database;
using CashRegister.Log;

namespace CashRegister.Dal
{
    /// <summary>
    /// Actual implementation of the generic repository
    /// </summary>
    /// <typeparam name="TEntity">TEntity should be a database model</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Used of loggin events
        /// </summary>
        private readonly ILogger _logger = LogFactory.GetLogger(typeof (Repository<TEntity>));

        /// <summary>
        /// The database context in use
        /// </summary>
        internal CashRegisterContext Context;

        /// <summary>
        /// The DbSet we are working on
        /// </summary>
        internal DbSet<TEntity> DbSet;

        /// <summary>
        /// Constructor that takes a context and set the DbSet to the one we are working on
        /// </summary>
        /// <param name="context"></param>
        public Repository(CashRegisterContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            _logger.Debug("Generic Repository instantiatet");
        }

        /// <summary>
        /// Implementation of get by id
        /// </summary>
        /// <param name="id">id of the entity to get</param>
        /// <returns>The entity or null</returns>
        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Implemtation of insert that adds to the dbset
        /// </summary>
        /// <param name="entity">entity to insert</param>
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Implemetation of delete by id. It tjeks to see if there is an entity by the id in the database and then calls delete by entity
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Implementation of delete by entity, Set the entity state to Attached if it is not in the DbSet and then calls remove
        /// </summary>
        /// <param name="entityToDelete">Entity to delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
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