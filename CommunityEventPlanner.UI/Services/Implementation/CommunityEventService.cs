using Blazored.LocalStorage;
using CommunityEventPlanner.Shared;
using CommunityEventPlanner.Shared.Contract;
using CommunityEventPlanner.UI.Services.Interface;
using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using static CommunityEventPlanner.Shared.Contract.ServiceResponse;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text;
using CommunityEventPlanner.Shared.Service;

namespace CommunityEventPlanner.UI.Services.Implementation
{
    public class CommunityEventService : ICommunityEventService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _path = "CommunityEvent";
        private const string _communityEventService = "CommunityEventService";
        private ILocalStorageService _localStorageService;

        public CommunityEventService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService; 
        }

        public async Task<IEnumerable<CommunityEventView>> GetUpcomingAsync()
        {
            using HttpClient client = _httpClientFactory.CreateClient(_communityEventService);

            var response = await client.GetAsync($"{_path}/GetUpcoming");

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<CommunityEventView>>())!;
        }

        public async Task<IEnumerable<int>> GetIdsByUserAsync()
        {
            string token = await _localStorageService.GetItemAsStringAsync("token");

            if (!string.IsNullOrEmpty(token))
            {
                using HttpClient client = _httpClientFactory.CreateClient(_communityEventService);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{_path}/GetIdsByUser");

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadFromJsonAsync<IEnumerable<int>>())!;
            }

            return new List<int>();
        }

        public async Task<BaseResponse> UserRegistrationAsync(int communityEventId)
        {

            var regiserUserEventRequest = new RegisterUserEventRequest
            {
                CommunityEventId = communityEventId
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(regiserUserEventRequest), Encoding.UTF8, "application/json");
            string token = await _localStorageService.GetItemAsStringAsync("token");
            using HttpClient client = _httpClientFactory.CreateClient(_communityEventService);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"{_path}/UserRegistration", requestContent);

            response.EnsureSuccessStatusCode();

            return new BaseResponse(true, "User Registered to Event");
        }

        public async Task<BaseResponse> CreateAsync(CommunityEventCreationRequest communityEventCreationRequest)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(communityEventCreationRequest), Encoding.UTF8, "application/json");
            using HttpClient client = _httpClientFactory.CreateClient(_communityEventService);
            var response = await client.PostAsync($"{_path}/Create", requestContent);

            response.EnsureSuccessStatusCode();

            return new BaseResponse(true, "Event Created");
        }
    }
}