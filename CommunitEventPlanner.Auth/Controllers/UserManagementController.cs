using Microsoft.AspNetCore.Mvc;
using CommunityEventPlanner.Contracts;
using CommunityEventPlanner.Contracts.DTO;
using Azure;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using LoginRequest = CommunityEventPlanner.Contracts.DTO.LoginRequest;
namespace IdentityManagerServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController(IUserManager userAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
        {
            var response = await userAccount.RegisterUser(registerUserRequest);
            //
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var response = await userAccount.LoginUser(loginRequest);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("loginAuth")]
        public async Task<IActionResult> LoginAuth(LoginRequest loginRequest)
        {
            var i = this.User.Claims.First(i => i.Type.Contains("nameidentifier")).Value;
            var response = await userAccount.LoginUser(loginRequest);
            return Ok(response);
        }

    }
}