using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunityEventController(ILogger<CommunityEventController> logger, ICommunityEventService communityEventService) : ControllerBase
    {
        private readonly ILogger<CommunityEventController> _logger = logger;
        private readonly ICommunityEventService _CommunityEventService = communityEventService;

        [HttpGet(Name = "GetEvents")]
        public async Task<IEnumerable<CommunityEvent>> GetAsync()
        {
            //var i = this.User.Claims.First(i => i.Type.Contains("nameidentifier")).Value;
            return await _CommunityEventService.GetCommunityEventsAsync();
        }
    }
}
