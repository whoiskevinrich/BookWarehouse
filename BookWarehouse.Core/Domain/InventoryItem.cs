using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BookWarehouse.Core.Domain
{
    public class InventoryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InventoryItemId { get; set; }

        public string Edition { get; set; }

        public int Quality { get; set; }

        public decimal Price { get; set; }

        public Guid TitleId { get; set; }

        [ForeignKey("TitleId")]
        public virtual Title Title { get; set; }

        public Guid WarehouseId { get; set; }
        
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }
    }
}