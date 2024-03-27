using System.ComponentModel.DataAnnotations;

namespace CommunityEventPlanner.Contracts.DTO
{
    public class LoginRequest
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;
    }
}