using Microsoft.EntityFrameworkCore;

namespace activities.Models
{
    public class ActivityContext : DbContext
    {
        public ActivityContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<Attending> Attendings { get; set; }
    }
}
