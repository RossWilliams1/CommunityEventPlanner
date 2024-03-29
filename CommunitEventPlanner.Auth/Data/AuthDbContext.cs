using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CommunityEventPlanner.Auth.Service.Data
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<AuthUser>(options)
    {
    }
}