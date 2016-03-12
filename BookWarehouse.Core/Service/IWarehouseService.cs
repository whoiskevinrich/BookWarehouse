using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;

namespace BookWarehouse.Core.Service
{
    public interface IWarehouseService
    {
        void Create(Warehouse warehouse);
        void Delete(Guid id);
        Warehouse Find(Expression<Func<Warehouse, bool>> predicate);
        Warehouse Get(Guid id);
        IEnumerable<Warehouse> GetAll();
        void Update(Warehouse warehouse);
        IQueryable<Warehouse> Where(Expression<Func<Warehouse, bool>> predicate);
    }
}