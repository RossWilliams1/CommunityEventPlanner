namespace CommunityEventPlanner.Shared.Contract
{
    public class ServiceResponse
    {
        public record class BaseResponse(bool Success, string Message);
        public record class LoginResponse(bool Success, string Token, string Message);
    }
}