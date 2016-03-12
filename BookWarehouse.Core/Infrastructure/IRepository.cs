using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookWarehouse.Core.Infrastructure
{
    public interface IRepository<T> 
        where T : class
    {
        /// <summary>
        /// Find entity based on key object. This implementation allows for greater flexibility in key types
        /// </summary>
        /// <param name="id">The identifier of the entity</param>
        /// <returns></returns>
        T Find(Guid id);

        /// <summary>
        /// Return all entries in the repository
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Returns a queryable set of results based on parameters passed
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> SearchForMany(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns a single expected instance of T where predicate is met
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T SearchFor(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add entity to repository
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Update an existing entity. The identifier is useful for seeding values.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="identifier"></param>
        void Update(T entity, Expression<Func<T, object>> identifier = null);

        /// <summary>
        /// Delete entity from the repository
        /// </summary>
        /// <param name="id"></param>
        void Remove(Guid id);
    }
}
