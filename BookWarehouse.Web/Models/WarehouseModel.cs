using System;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.Web.Models
{
    public class WarehouseModel
    {
        public Guid WarehouseId { get; set; }

        [Required]
        [Display(Name = "Warehouse Name")]
        public string Name { get; set; }
    }
}
