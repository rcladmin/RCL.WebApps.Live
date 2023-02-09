using Microsoft.EntityFrameworkCore;
using RCL.WebApps.Live.Models;

namespace RCL.WebApps.Live.DataContext
{
    public class LiveDbContext : DbContext
    {
        public LiveDbContext()
        { 
        }

        public LiveDbContext(DbContextOptions<LiveDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventAttendee> EventAttendees { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
