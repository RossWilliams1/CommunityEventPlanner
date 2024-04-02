using CommunityEventPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventPlanner.Data.Data
{
    public class CommunityEventPlannerDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CommunityEvent> CommunityEvents { get; set; }
        public DbSet<UserCommunityEvent> UserCommunityEvents { get; set; }
    }
}
