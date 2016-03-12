using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Data;
using BookWarehouse.Core.Infrastructure;

namespace BookWarehouse.Core
{
    public class WarehouseRepository<T, TKey> : IRepository<T, TKey>
        where T : class
    {
        private readonly WarehouseContext _context;
        private readonly IDbSet<T> _dbSet;

        public WarehouseRepository(WarehouseContext context, IDbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public T Find(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return _dbSet.Find(key);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> SearchForMany(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _dbSet.Where(predicate);
        }

        public T SearchFor(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _dbSet.SingleOrDefault(predicate);
        }

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity, Expression<Func<T, object>> identifier = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (identifier == null)
            {
                _dbSet.AddOrUpdate(entity);
            }
            else
            {
                _dbSet.AddOrUpdate(identifier, entity);
            }
            _context.SaveChanges();
        }

        public void Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var record = Find(key);
            if (record == null) return;

            _dbSet.Remove(record);
            _context.SaveChanges();
        }
    }
}
