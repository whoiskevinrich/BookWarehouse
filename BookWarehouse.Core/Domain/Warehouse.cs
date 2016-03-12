using System;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.Core.Domain
{
    public class Warehouse
    {
        [Key]
        public Guid WarehouseId { get; set; }

        public string Name { get; set; }

        //other properties would go here (Location/Address/PhoneNumber/etc)
    }
}
