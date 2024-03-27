using System.ComponentModel.DataAnnotations;
namespace CommunityEventPlanner.Contracts.DTO
{
    public class RegisterUserRequest
    {
        public string? Id { get; set; }

        public required string Name { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}