using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWarehouse.Core.Domain
{
    public class Title
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TitleId { get; set; }

        public long Isbn { get; set; }

        public int YearPublished { get; set; }
        
        public string Name { get; set; }
    }
}