using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly ILogger _log;

        public InventoryItemService(IRepository<InventoryItem> items, ILogger log)
        {
            _items = items;
            _log = log;
        }

        public void Create(InventoryItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var titlecount = _items.SearchForMany(x => x.TitleId == item.TitleId).Count();
            _items.Add(item);
            _log.Log(LogAction.QuantityChange, item.TitleId, titlecount.ToString(), (titlecount + 1).ToString());
        }

        public void Delete(Guid id)
        {
            var item = _items.Find(id);
            var titlecount = _items.SearchForMany(x => x.TitleId == item.TitleId).Count();
            _items.Remove(id);
            _log.Log(LogAction.QuantityChange, item.TitleId, titlecount.ToString(), (titlecount - 1).ToString());
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
            var existing = _items.Find(item.InventoryItemId);
            if (existing == null)
            {
                _items.Add(item);
                return;
            }

            if (existing.WarehouseId != item.WarehouseId)
            {
                _log.Log(LogAction.WarehouseTransfer, item.TitleId, existing.WarehouseId.ToString(), item.WarehouseId.ToString());
            }

            if (existing.Price != item.Price)
            {
                _log.Log(LogAction.PriceChange, item.TitleId, existing.Price.ToString(CultureInfo.InvariantCulture),
                    item.Price.ToString(CultureInfo.InvariantCulture));
            }

            _items.Update(item);
        }

        public IQueryable<InventoryItem> Where(Expression<Func<InventoryItem, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _items.SearchForMany(predicate);
        }
    }
}