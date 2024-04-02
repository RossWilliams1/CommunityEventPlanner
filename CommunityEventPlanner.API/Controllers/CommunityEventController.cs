using CommunityEventPlanner.Service.Interface;
using CommunityEventPlanner.Shared.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityEventPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommunityEventController(ILogger<CommunityEventController> logger, ICommunityEventService communityEventService) : ControllerBase
    {
        private const string ControllerName = nameof(CommunityEventController);
        private readonly ILogger<CommunityEventController> _logger = logger;
        private readonly ICommunityEventService _CommunityEventService = communityEventService;

        [HttpGet(Name = "GetUpcoming")]
        public async Task<IActionResult> GetUpcomingAsync()
        {
            _logger.LogInformation("{ControllerName} GetAsync called", ControllerName);

            var events = await _CommunityEventService.GetUpcomingAsync();

            return Ok(events);
        }

        [HttpGet(Name = "GetIdsByUser")]
        [Authorize]
        public async Task<IActionResult> GetIdsByUserAsync()
        {
            _logger.LogInformation("{ControllerName} GetAsync called", ControllerName);

            Guid userGuid;
            var userId = this?.User?.Claims?.First(i => i.Type.Contains("nameidentifier"))?.Value ?? "";

            try
            {
                userGuid = new Guid(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, "Failed to retreive User Guid from nameidentifier Claim: {userId}", userId);

                return StatusCode(500);
            }

            var events = await _CommunityEventService.GetIdsByUserAsync(userGuid);

            return Ok(events);
        }


        [HttpPost(Name = "UserRegistration")]
        [Authorize]
        public async Task<IActionResult> UserRegistrationAsync(RegisterUserEventRequest registerUserEvent)
        {
            _logger.LogInformation("{ControllerName} UserRegistrationAsync called with {registerUserEvent}", ControllerName, registerUserEvent);

            Guid userGuid;
            var userId = this?.User?.Claims?.First(i => i.Type.Contains("nameidentifier"))?.Value ?? "";

            try
            {
                userGuid = new Guid(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, "Failed to retreive User Guid from nameidentifier Claim: {userId}", userId);

                return StatusCode(500);
            }

           var response = await _CommunityEventService.UserRegistrationAsync(registerUserEvent.CommunityEventId, userGuid);

            
            return Ok(response);
        }

        [HttpPost(Name = "Create")]
        public async Task<IActionResult> CreateAsync(CommunityEventCreationRequest communityEventCreationRequest)
        {
            _logger.LogInformation("{ControllerName} GetAsync called", ControllerName);

            await _CommunityEventService.CreateAsync(communityEventCreationRequest);

            return Ok();
        }
    }
}
