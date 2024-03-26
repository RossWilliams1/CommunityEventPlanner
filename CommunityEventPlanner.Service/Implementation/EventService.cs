using CommunityEventPlanner.Data.Data;
using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventPlanner.Service.Implementation
{
    public class EventService(CommunityEventPlannerDbContext communityEventPlannerDbContext) : ICommunityEventService
    {
        private readonly CommunityEventPlannerDbContext _communityEventPlannerDbContext = communityEventPlannerDbContext;

        public async Task<List<CommunityEvent>> GetCommunityEventsAsync()
        {
            return await _communityEventPlannerDbContext.CommunityEvents.ToListAsync();
        }
    }
}
