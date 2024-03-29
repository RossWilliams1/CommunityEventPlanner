using CommunityEventPlanner.Shared.Contract;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;

namespace CommunityEventPlanner.Shared.Service.Interface
{
    public interface IUserManagerService
    {
        Task<BaseResponse> RegisterUser(RegisterUserRequest RegisterUserRequest);
        Task<LoginResponse> LoginUser(LoginRequest LoginRequest);
    }
}