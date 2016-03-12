using System;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.Core.Domain
{
    public class Title
    {
        [Key]
        public Guid TitleId { get; set; }

        public string Isbn { get; set; }

        public int YearPublished { get; set; }
        
        public int QuantityOnHand { get; set; }
    }
}