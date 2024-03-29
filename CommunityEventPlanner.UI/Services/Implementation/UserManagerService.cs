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
        private readonly HttpClient _httpClient;
        private readonly string _path;

        public UserManagerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7146");
            _path = "api/UserManagement";
        }

        public async Task<BaseResponse> RegisterUser(RegisterUserRequest registerUserRequest)
        {
            var response = await _httpClient
                 .PostAsync($"{_path}/register",
                 TokenClaimService.GenerateStringContent(
                 TokenClaimService.SerializeObj(registerUserRequest)));

            if (!response.IsSuccessStatusCode)
                return new BaseResponse(false, "Failed to register");

            var apiResponse = await response.Content.ReadAsStringAsync();
            return TokenClaimService.DeserializeJsonString<BaseResponse>(apiResponse);
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            var response = await _httpClient
               .PostAsync($"{_path}/login",
               TokenClaimService.GenerateStringContent(
               TokenClaimService.SerializeObj(loginRequest)));

            if (!response.IsSuccessStatusCode)
                return new LoginResponse(false, null!, "Failed to login. Please try again.");

            var apiResponse = await response.Content.ReadAsStringAsync();
            return TokenClaimService.DeserializeJsonString<LoginResponse>(apiResponse);

        }
    }
}