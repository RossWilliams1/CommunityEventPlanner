using System.ComponentModel.DataAnnotations;
namespace CommunityEventPlanner.Shared.Contract
{
    public class RegisterUserRequest
    {
        public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}