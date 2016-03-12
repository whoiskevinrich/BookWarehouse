using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Service
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IRepository<InventoryItem> _items;

        public InventoryItemService(IRepository<InventoryItem> items)
        {
            _items = items;
        }

        public void Create(InventoryItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
        }

        public void Delete(Guid id)
        {
            _items.Remove(id);
        }

        public InventoryItem Find(Expression<Func<InventoryItem, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _items.SearchFor(predicate);
        }

        public InventoryItem Get(Guid id)
        {
            return _items.Find(id);
        }

        public IEnumerable<InventoryItem> GetAll()
        {
            return _items.GetAll();
        }

        public void Update(InventoryItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _items.Update(item);
        }

        public IQueryable<InventoryItem> Where(Expression<Func<InventoryItem, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _items.SearchForMany(predicate);
        }
    }
}