using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWarehouse.Core.Domain
{
    public class Title
    {
        [Key]
        public Guid TitleId { get; set; }

        public string Isbn { get; set; }

        public int YearPublished { get; set; }

        public decimal Price { get; set; }

        public int QuantityOnHand { get; set; }

        public string Edition { get; set; }

        public int Quality { get; set; }

        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }
    }
}