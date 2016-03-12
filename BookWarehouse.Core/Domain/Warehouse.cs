using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWarehouse.Core.Domain
{
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WarehouseId { get; set; }

        public string Name { get; set; }

        //other properties would go here (Location/Address/PhoneNumber/etc)
    }
}
