using CommunityEventPlanner.Contracts;
using CommunityEventPlanner.Contracts.DTO;

namespace CommunityEventPlanner.UI.Services.Interface
{
    public interface ICommunityEventService
    {
        public Task<IEnumerable<CommunityEvent>> GetCommunityEventsAsync();
    }
}
