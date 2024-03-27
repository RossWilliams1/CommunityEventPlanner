namespace CommunityEventPlanner.Contracts.DTO
{
    public class ServiceResponses
    {
        public record class BaseResponse(bool Flag, string Message);
        public record class LoginResponse(bool Flag, string Token, string Message);
    }
}