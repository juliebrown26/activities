using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace activities.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Date:")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Time:")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Duration:")]
        public int numDuration { get; set; }

        [Required]
        public string strDuration { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        public string Coordinator { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Attending> Attending { get; set; }
    }
}