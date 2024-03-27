using CommunityEventPlanner.Contracts.DTO;
using static CommunityEventPlanner.Contracts.DTO.ServiceResponses;

namespace CommunityEventPlanner.Contracts
{
    public interface IUserManager
    {
        Task<BaseResponse> RegisterUser(RegisterUserRequest RegisterUserRequest);
        Task<LoginResponse> LoginUser(LoginRequest LoginRequest);
    }
}