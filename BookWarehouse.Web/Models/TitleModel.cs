using System;
using System.ComponentModel.DataAnnotations;

namespace BookWarehouse.Web.Models
{
    public class TitleModel
    {
        public Guid TitleId { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public long Isbn { get; set; }

        [Required]
        [Display(Name = "Year Published")]
        public int YearPublished { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}