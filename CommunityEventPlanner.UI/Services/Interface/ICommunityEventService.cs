using CommunityEventPlanner.Shared;
using CommunityEventPlanner.Shared.Contract;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;

namespace CommunityEventPlanner.UI.Services.Interface
{
    public interface ICommunityEventService
    {
        public Task<IEnumerable<CommunityEventView>> GetUpcomingAsync();

        public Task<IEnumerable<int>> GetIdsByUserAsync();

        public Task<BaseResponse> UserRegistrationAsync(int communityEventId);

        public Task<BaseResponse> CreateAsync(CommunityEventCreationRequest communityEventCreationRequest);
    }
}
