using CommunityEventPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventPlanner.Data.Data
{
    public class CommunityEventPlannerDbContext : DbContext
    {
        public required DbSet<CommunityEvent> CommunityEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=CommunityEventPlanner;Trusted_Connection=True;TrustServerCertificate=true");
        }
    }
}
