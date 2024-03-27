using CommunityEventPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventPlanner.Data.Data
{
    public class CommunityEventPlannerDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<CommunityEvent> CommunityEvents { get; set; }
    }
}
