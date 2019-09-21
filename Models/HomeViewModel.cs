using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace activities.Models
{
    public class HomeViewModel
    {
        public List<Event> Events { get; set; }
        public User User { get; set; }

        public Attending Attending { get; set; }

        public int EventToDelete { get; set; }
    }
}