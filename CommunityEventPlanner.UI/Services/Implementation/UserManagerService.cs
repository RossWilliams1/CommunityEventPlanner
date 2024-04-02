using Blazored.LocalStorage;
using CommunityEventPlanner.Shared.Service.Interface;
using CommunityEventPlanner.Shared.Contract;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;
using CommunityEventPlanner.Shared.Service;
using System.Net.Http;

namespace CommunityEventPlanner.UI.Services.Implementation
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _path;

        public UserManagerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _path = "UserManagement";
        }

        public async Task<BaseResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            using HttpClient client = _httpClientFactory.CreateClient("AuthService");

                var response = await client
                     .PostAsync($"{_path}/register",
                     TokenClaimService.GenerateStringContent(
                     TokenClaimService.SerializeObj(registerUserRequest)));

            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadAsStringAsync();
            return TokenClaimService.DeserializeJsonString<BaseResponse>(apiResponse);
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            using HttpClient client = _httpClientFactory.CreateClient("AuthService");

            var response = await client
           .PostAsync($"{_path}/login",
               TokenClaimService.GenerateStringContent(
               TokenClaimService.SerializeObj(loginRequest)));

            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadAsStringAsync();
            return TokenClaimService.DeserializeJsonString<LoginResponse>(apiResponse);

        }
    }
}