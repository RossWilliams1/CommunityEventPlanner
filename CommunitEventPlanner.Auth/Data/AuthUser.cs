using Microsoft.AspNetCore.Identity;

namespace IdentityManagerServerApi.Data
{
    public class AuthUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}