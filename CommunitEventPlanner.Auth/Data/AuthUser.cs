using Microsoft.AspNetCore.Identity;

namespace CommunityEventPlanner.Auth.Service.Data
{
    public class AuthUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}