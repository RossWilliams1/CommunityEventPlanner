namespace CommunityEventPlanner.Contracts.DTO
{
    public class ServiceResponses
    {
        public record class BaseResponse(bool Success, string Message);
        public record class LoginResponse(bool Success, string Token, string Message);
    }
}