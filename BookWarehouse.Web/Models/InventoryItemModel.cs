using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookWarehouse.Web.Models
{
    public class InventoryItemModel
    {
        public Guid InventoryItemId { get; set; }

        [Required]
        public string Edition { get; set; }

        [Required]
        [Range(1,5,ErrorMessage = "Quality must be 1-5")]
        public int Quality { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.00, 9999999999999999999, ErrorMessage = "Must be positive price")]
        public decimal Price { get; set; }

        [Required]
        public Guid TitleId { get; set; }

        public TitleModel Title { get; set; }

        [Required]
        public Guid WarehouseId { get; set; }

        public WarehouseModel Warehouse { get; set; }
    }
}
