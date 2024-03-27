using System.ComponentModel.DataAnnotations;

namespace CommunityEventPlanner.Contracts.DTO
{
    public class LoginRequest
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty!;
    }
}