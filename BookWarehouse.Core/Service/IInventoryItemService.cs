using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;

namespace BookWarehouse.Core.Service
{
    public interface IInventoryItemService
    {
        void Create(InventoryItem item);
        void Delete(Guid id);
        InventoryItem Find(Expression<Func<InventoryItem, bool>> predicate);
        InventoryItem Get(Guid id);
        IEnumerable<InventoryItem> GetAll();
        void Update(InventoryItem item);
        IQueryable<InventoryItem> Where(Expression<Func<InventoryItem, bool>> predicate);
    }
}