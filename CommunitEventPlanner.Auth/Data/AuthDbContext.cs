using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace IdentityManagerServerApi.Data
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<AuthUser>(options)
    {
    }
}