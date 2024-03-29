using CommunityEventPlanner.Shared;
using CommunityEventPlanner.Shared.Contract;

namespace CommunityEventPlanner.UI.Services.Interface
{
    public interface ICommunityEventService
    {
        public Task<IEnumerable<CommunityEvent>> GetCommunityEventsAsync();
    }
}
