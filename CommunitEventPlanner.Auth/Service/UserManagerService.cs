using CommunityEventPlanner.Shared.Contract;
using CommunityEventPlanner.Auth.Service.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommunityEventPlanner.Shared.Service.Interface;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;

namespace CommunityEventPlanner.Auth.Service
{
    public class UserManagerService(UserManager<AuthUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserManagerService
    {
        public async Task<BaseResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            if (registerUserRequest == null) 
                return new BaseResponse(false, "Model is empty");

            if(registerUserRequest.Password != registerUserRequest.ConfirmPassword)
                return new BaseResponse(false, "Confirm Password does not match");

            var newUser = new AuthUser()
            {
                Name = registerUserRequest.Name,
                Email = registerUserRequest.Email,
                PasswordHash = registerUserRequest.Password,
                UserName = registerUserRequest.Email
            };
            var existingUser = await userManager.FindByEmailAsync(newUser.Email);

            if (existingUser != null) 
                return new BaseResponse(false, "Email Already Registered");

            var newUserResult = await userManager.CreateAsync(newUser!, registerUserRequest.Password);

            if (!newUserResult.Succeeded)
                return new BaseResponse(false, newUserResult?.Errors?.Select(x => x.Description)?.FirstOrDefault() ?? "Failed to Register");

            var checkUser = await roleManager.FindByNameAsync("User");

            if (checkUser == null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

            await userManager.AddToRoleAsync(newUser, "User");
            return new BaseResponse(true, "User Regisered");
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                return new LoginResponse(false, null!, "Login request is required");

            var getUser = await userManager.FindByEmailAsync(loginRequest.Email);
            if (getUser == null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginRequest.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login Successfull");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}