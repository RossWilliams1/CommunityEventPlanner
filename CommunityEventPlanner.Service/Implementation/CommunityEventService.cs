using CommunityEventPlanner.Data.Data;
using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventPlanner.Service.Implementation
{
    public class CommunityEventService(CommunityEventPlannerDbContext communityEventPlannerDbContext) : ICommunityEventService
    {
        private readonly CommunityEventPlannerDbContext _communityEventPlannerDbContext = communityEventPlannerDbContext;

        public async Task<List<CommunityEvent>> GetCommunityEventsAsync()
        {
            return await _communityEventPlannerDbContext.CommunityEvents.ToListAsync();
        }
    }
}
