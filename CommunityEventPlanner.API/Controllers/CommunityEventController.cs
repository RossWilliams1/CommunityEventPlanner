using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventPlanner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunityEventController(ILogger<CommunityEventController> logger, ICommunityEventService communityEventService) : ControllerBase
    {
        private readonly ILogger<CommunityEventController> _logger = logger;
        private readonly ICommunityEventService _CommunityEventService = communityEventService;

        [HttpGet(Name = "GetEvents")]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<IEnumerable<CommunityEvent>> GetAsync()
        {
            return await _CommunityEventService.GetCommunityEventsAsync();
        }
    }
}
