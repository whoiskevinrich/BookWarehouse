using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWarehouse.Core.Domain
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogId { get; set; }

        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public Guid TitleId { get; set; }

        [ForeignKey("TitleId")]
        public virtual Title Title { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
