using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Shared.Contract;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;

namespace CommunityEventPlanner.Service.Interface
{
    public interface ICommunityEventService
    {

        public Task<BaseResponse> UserRegistrationAsync(int communityEventId, Guid userId);
        public Task<List<CommunityEvent>> GetUpcomingAsync();
        public Task<List<int>> GetIdsByUserAsync(Guid userId);
        public Task<BaseResponse> CreateAsync(CommunityEventCreationRequest communityEventCreationRequest);
    }
}
