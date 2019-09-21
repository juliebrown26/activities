using System.ComponentModel.DataAnnotations;
using System;

namespace activities.Models
{
    public class Attending
    {
        [Key]
        public int AttendingId { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public User User { get; set; }

        public Event Event { get; set; }
    }
}