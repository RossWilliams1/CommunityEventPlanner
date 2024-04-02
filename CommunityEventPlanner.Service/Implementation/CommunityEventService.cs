using AutoMapper;
using CommunityEventPlanner.Data.Data;
using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Service.Interface;
using CommunityEventPlanner.Shared.Contract;
using Microsoft.EntityFrameworkCore;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;

namespace CommunityEventPlanner.Service.Implementation
{
    public class CommunityEventService(CommunityEventPlannerDbContext communityEventPlannerDbContext, IMapper mapper) : ICommunityEventService
    {
        private readonly CommunityEventPlannerDbContext _communityEventPlannerDbContext = communityEventPlannerDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<List<CommunityEvent>> GetUpcomingAsync()
        {
            return await _communityEventPlannerDbContext.CommunityEvents
                            .Where(x => x.StartDate > DateTime.Now)
                            .OrderByDescending(x => x.StartDate)
                            .ToListAsync();
        }

        public async Task<List<int>> GetIdsByUserAsync(Guid userId)
        {
            return await _communityEventPlannerDbContext.UserCommunityEvents.Include(x => x.CommunityEvent)
                            .Where(x => x.UserId == userId)
                            .OrderByDescending(x => x.CommunityEvent.StartDate).AsNoTracking()
                            .Select(x => x.CommunityEvent.Id)
                            .ToListAsync();
        }

        public async Task<BaseResponse> UserRegistrationAsync(int communityEventId, Guid userId)
        {
            bool recordExists = await _communityEventPlannerDbContext.UserCommunityEvents
                                    .AnyAsync(x => x.UserId == userId && x.CommunityEventId == communityEventId);
            if (!recordExists)
            {
                var userCommunityEvent = new UserCommunityEvent { CommunityEventId = communityEventId, UserId = userId };
                await _communityEventPlannerDbContext.UserCommunityEvents.AddAsync(userCommunityEvent);
                await _communityEventPlannerDbContext.SaveChangesAsync();

                return new BaseResponse(true, "Registered");
            }

            return new BaseResponse(false, "user already registered to event.");
        }

        public async Task<BaseResponse> CreateAsync(CommunityEventCreationRequest communityEvent)
        {
            var mappedEvent = _mapper.Map<CommunityEvent>(communityEvent);
            if (communityEvent.EndDate.HasValue && communityEvent.StartDate > communityEvent.EndDate.Value)
                return new BaseResponse(false, "Start Date must be before End Date.");

            if (string.IsNullOrEmpty(mappedEvent.Name))
                return new BaseResponse(false, "Name is required for an event.");

            await _communityEventPlannerDbContext.CommunityEvents.AddAsync(mappedEvent);
            await _communityEventPlannerDbContext.SaveChangesAsync();
            return new BaseResponse(true, "Event Created.");
        }
    }
}
