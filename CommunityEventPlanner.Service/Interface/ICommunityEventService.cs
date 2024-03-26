using CommunityEventPlanner.Data.Models;

namespace CommunityEventPlanner.Service.Interface
{
    public interface ICommunityEventService
    {
        public Task<List<CommunityEvent>> GetCommunityEventsAsync();
    }
}
