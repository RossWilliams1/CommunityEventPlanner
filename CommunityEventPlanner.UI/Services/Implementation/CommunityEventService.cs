using Blazored.LocalStorage;
using CommunityEventPlanner.Shared;
using CommunityEventPlanner.Shared.Contract;
using CommunityEventPlanner.UI.Services.Interface;
using System.Net.Http;
using System.Net.Http.Json;

namespace CommunityEventPlanner.UI.Services.Implementation
{
    public class CommunityEventService : ICommunityEventService
    {
        private readonly HttpClient _httpClient;
        private readonly string _path;
        private ILocalStorageService _localStorageService;

        public CommunityEventService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7046");
            _path = "api/CommunityEvent";
            _localStorageService = localStorageService; 
        }

        public async Task<IEnumerable<CommunityEvent>> GetCommunityEventsAsync()
        {
            string token = await _localStorageService.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(_path);

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<CommunityEvent>>())!;
        }
    }
}