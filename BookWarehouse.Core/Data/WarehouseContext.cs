using System.Data.Entity;
using BookWarehouse.Core.Domain;

namespace BookWarehouse.Core.Data
{
    public class WarehouseContext : DbContext
    {
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
