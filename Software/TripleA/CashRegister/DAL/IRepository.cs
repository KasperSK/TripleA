using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CashRegister.Dal
{
    /// <summary>
    /// Generic repository implementation to facilitate database acces
    /// </summary>
    /// <typeparam name="TEntity">TEntity is a database model</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Is used to get a database entry by its id
        /// </summary>
        /// <param name="id">id of the entity to get</param>
        /// <returns>The requestet entity or null</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Is used to insert an entity to the database
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Is used to delete an entity from the database by id 
        /// </summary>
        /// <param name="id">Id of the entity to delete</param>
        void Delete(object id);

        /// <summary>
        /// Is used to delete an entity from the database by the entity itself
        /// </summary>
        /// <param name="entityToDelete">Entity to delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Is used to update an entity in the database
        /// </summary>
        /// <param name="entityToUpdate">Entity to update</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Get facilitates getting multiple item out from the dataase in list form
        /// </summary>
        /// <param name="filter">The filter to be applied to the query</param>
        /// <param name="orderBy">Is used to order the list</param>
        /// <param name="includeProperties">Navigation properties to be included in the query</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
    }
}