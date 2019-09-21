using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace activities.Models
{
    public class EventViewModel
    {
        public List<User> Users { get; set; }
        public Event Event { get; set; }
        public Attending Attending { get; set; }
    }
}