using Microsoft.AspNetCore.Mvc;
using CommunityEventPlanner.Shared.Service.Interface;
using CommunityEventPlanner.Shared.Contract;
using Azure;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using LoginRequest = CommunityEventPlanner.Shared.Contract.LoginRequest;
using Microsoft.AspNetCore.Cors;
namespace CommunityEventPlanner.Auth.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController(IUserManagerService userManagerService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
        {
            var response = await userManagerService.RegisterUser(registerUserRequest);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await userManagerService.LoginUser(loginRequest);
            return Ok(response);
        }
    }
}