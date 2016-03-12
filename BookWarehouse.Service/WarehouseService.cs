using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IRepository<Warehouse> _warehouses;

        public WarehouseService(IRepository<Warehouse> warehouses)
        {
            _warehouses = warehouses;
        }

        public void Create(Warehouse warehouse)
        {
            if (warehouse == null) throw new ArgumentNullException(nameof(warehouse));
            _warehouses.Add(warehouse);
        }

        public void Update(Warehouse warehouse)
        {
            if (warehouse == null) throw new ArgumentNullException(nameof(warehouse));
            _warehouses.Update(warehouse);
        }

        public void Delete(Guid id)
        {
            _warehouses.Remove(id);
        }

        public Warehouse Get(Guid id)
        {
            return _warehouses.Find(id);
        }

        public Warehouse Find(Expression<Func<Warehouse, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _warehouses.SearchFor(predicate);
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return _warehouses.GetAll();
        }

        public IQueryable<Warehouse> Where(Expression<Func<Warehouse, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _warehouses.SearchForMany(predicate);
        }
    }
}
